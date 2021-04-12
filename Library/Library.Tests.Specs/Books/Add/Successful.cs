using FluentAssertions;
using Library.Entities;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.BookCategories;
using Library.Persistence.EF.Books;
using Library.Services.BookCategories.Contracts;
using Library.Services.BookCategories.TestTools;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.Services.Books.TestTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.Test.Specs.Books.Add
{
    public class Successful
    {
        private EFDataContext _context;
        private BookCategory _bookCategory;
        private int _actual;
        private AddBookDto _dto;
        private BookCategroyRepository _bookCategroyRepository;
        private BookRepository _repository;
        private UnitOfWork _unitOfWork;
        private BookServices _sut;

        public Successful()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _bookCategroyRepository = new EFBookCategroyRepository(_context);
            _repository = new EFBookRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new BookAppServices(_unitOfWork, _repository, _bookCategroyRepository);

        }
        //در فهرست دسته بندی کتاب ، یک دسته بندی خودیاری دارم
        private void Given()
        {
            _bookCategory = new BookCategoryFactory()
                                            .WithTitle("خودیاری")
                                            .Generate();
            _context.Manipulate(_ => _.BookCategories.Add(_bookCategory));
        }

        //یک کتاب با نام اثر مرکب ونویسندگی دارن هاردی و رنج سنی +15 سال
        //در دسته بندی خودیاری تعریف میکنم.
        private void When()
        {
            _dto = new BookBuilder()
                            .WithName("اثر مرکب")
                            .WithAuthor("دارن هاردی")
                            .WithAgeGroup(15)
                            .WithBookCategroy(_bookCategory)
                            .GenerateAddBookDto();

            _actual = _sut.Add(_dto);
        }


        //باید یک کتاب با نام اثر مرکب و نویسندگی دارن هاردی و رنج سنی +15 سال
        //در دسته بندی خودیاری وجود داشته باشد
        private void Then()
        {
            var expected = _context.Books.Single(_ => _.Id == _actual);
            expected.Name.Should().Be(_dto.Name);
            expected.Author.Should().Be(_dto.Author);
            expected.AgeGroup.Should().Be(_dto.AgeGroup);
            expected.BookCategoryId.Should().Be(_bookCategory.Id);
        }

        [Fact]
        public void Run()
        {
            Given();
            When();
            Then();
        }
    }
}
