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

namespace Library.Tests.Specs.Books.GetByBookCategory
{
    public class Successful
    {

        private EFDataContext _context;
        private BookCategroyRepository _bookCategroyRepository;
        private Book _book;
        private BookRepository _repository;
        private UnitOfWork _unitOfWork;
        private BookServices _sut;
        private List<GetByBookCategoryDto> _actual;

        public Successful()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _bookCategroyRepository = new EFBookCategroyRepository(_context);
            _repository = new EFBookRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new BookAppServices(_unitOfWork, _repository, _bookCategroyRepository);
        }
        //یک کتاب با نام اثر مرکب و نویسندگی دارن هاردی و رنج سنی +15 سال
        //در فهرست دسته بندی کتاب ها در دسته بندی خودیاری وجود دارد
        private void Given()
        {
            var bookCategroy = new BookCategoryFactory()
                                    .WithTitle("خودیاری")
                                    .Generate();
            _book = new BookBuilder()
                            .WithName("اثر مرکب")
                            .WithAuthor("دارن هاردی")
                            .WithAgeGroup(15)
                            .WithBookCategroy(bookCategroy)
                            .Generate();
            _context.Manipulate(_ => _.Books.Add(_book));
        }

        //می خواهم لیست کتاب هایی که در دسته بندی خودیاری وجود دارد را مشاهده کنم
        private void When()
        {
            _actual = _sut.GetByBookCategory(_book.BookCategoryId);
        }

        //باید در دسته بندی خودیاری ، کتاب با نام اثر مرکب و نویسندگی دارن هاردی و رنج سنی +15 سال
        //وجود داسته باشد
        private void Then()
        {
            var expected = _context.Books
                                 .Where(_ => _.BookCategoryId == _book.BookCategoryId)
                                 .Select(_ => new GetByBookCategoryDto
                                 {
                                     Name = _book.Name,
                                     Author = _book.Author,
                                     AgeGroup = _book.AgeGroup,
                                     Id = _book.Id
                                 })
                                 .ToList();
            expected.Should().HaveCount(_actual.Count);
            expected.Select(_ => _.Author).Should().BeEquivalentTo(_actual.Select(_ => _.Author));
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
