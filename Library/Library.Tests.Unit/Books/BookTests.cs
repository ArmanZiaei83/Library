using FluentAssertions;
using Library.Entities;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.BookCategories;
using Library.Persistence.EF.Books;
using Library.Services.BookCategories.Contracts;
using Library.Services.BookCategories.Exceptions;
using Library.Services.BookCategories.TestTools;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.Services.Books.TestTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.Test.Unit.Books
{
    public class BookTests
    {

        private EFDataContext _context;
        private EFDataContext _readDatacontext;
        private BookCategroyRepository _bookCategroyRepository;
        private BookRepository _repository;
        private UnitOfWork _unitOfWork;
        private BookAppServices _sut;

        public BookTests()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _readDatacontext = database.CreateDataContext<EFDataContext>();
            _bookCategroyRepository = new EFBookCategroyRepository(_context);
            _repository = new EFBookRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new BookAppServices(_unitOfWork, _repository, _bookCategroyRepository);

        }


        [Fact]
        public void Add_add_a_book_properly()
        {
            var bookCategroy = new BookCategoryFactory().Generate();
            _context.Manipulate(_ => _.BookCategories.Add(bookCategroy));
            var dto = new BookBuilder()
                         .WithByBookCategroyId(bookCategroy.Id)
                         .GenerateAddBookDto();

            var actual = _sut.Add(dto);

            var expected = _readDatacontext.Books.Single(_ => _.Id == actual);
            expected.Name.Should().Be(dto.Name);
            expected.Author.Should().Be(dto.Author);
            expected.AgeGroup.Should().Be(dto.AgeGroup);
            expected.BookCategoryId.Should().Be(dto.BookCategoryId);
        }

        [Fact]
        public void Add_throw_exception_when_book_categroy_not_found()
        {
            var bookCategroy = new BookCategoryFactory().Generate();
            var dto = new BookBuilder()
                         .WithByBookCategroyId(bookCategroy.Id)
                         .GenerateAddBookDto();

            Action expected = () => _sut.Add(dto);

            expected.Should().Throw<BookCategroyNotFoundException>();
        }
    }
}
