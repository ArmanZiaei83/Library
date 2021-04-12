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

namespace Library.Test.Specs.Books.Update
{
    public class Successful
    {
        private EFDataContext _context;
        private BookRepository _repository;
        private UnitOfWork _unitOfWork;
        private BookCategroyRepository _bookCategroyRepository;
        private BookServices _sut;
        private Book _book;
        private BookCategory _bookCategroyKhodBavari;
        private UpdateBookDto _dto;
        public Successful()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _repository = new EFBookRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _bookCategroyRepository = new EFBookCategroyRepository(_context);
            _sut = new BookAppServices(_unitOfWork, _repository, _bookCategroyRepository);
        }

        //یک کتاب با نام اثر مرکب و نویسندگی دارن هاردی و رنج سنی +15 سال
        //در فهرست دسته بندی کتاب ها در دسته بندی خودیاری وجود دارد
       // و یک دسته بندی دیگر به نام خودباوری وجود دارد

        private void Given()
        {
            var bookCategroyKhodYari =new BookCategoryFactory()
                                            .WithTitle("خودیاری")                               
                                            .Generate();
            _book = new BookBuilder()
                            .WithName("اثر مرکب")
                            .WithAuthor("دارن هاردی")
                            .WithAgeGroup(15)
                            .WithBookCategroy(bookCategroyKhodYari)
                            .WithByBookCategroyId(bookCategroyKhodYari.Id)
                            .Generate();
            _context.Manipulate(_ => _.Books.Add(_book));
            _bookCategroyKhodBavari = new BookCategoryFactory()
                                            .WithTitle("خودباوری")
                                            .Generate();
            _context.Manipulate(_ => _.BookCategories.Add(_bookCategroyKhodBavari));
        }


        //می خواهم کتاب با نام اثر مرکب و نویسندگی دارن هاردی و رنج سنی +15 سال
        //در دسته بندی خودیاری را به کتابی با نام امسال تکرار نخواهد شد
        //و نویسندگی هاردی و رنج سنی +18 سال در دسته بندی خودباوری ، ویرایش کنم
        private void When()
        {
            _dto= new BookBuilder()
                            .WithName("امسال تکرار نخواهد شد")
                            .WithAuthor("هردی")
                            .WithAgeGroup(18)
                            .WithByBookCategroyId(_bookCategroyKhodBavari.Id)
                            .GenerateUpdateBookDto();
            _sut.Update(_book.Id, _dto);
        }


        //باید فقط یک کتاب با نام امسال تکرار نخواهد شد ونویسندگی دارن هاردی و رنج سنی +18 سال
        //در دسته بندی خودباوری وجود داشته باشد
        private void Then()
        {
            var expected = _context.Books.Single(_ => _.Id == _book.Id);
            expected.Name.Should().Be(_dto.Name);
            expected.Author.Should().Be(_dto.Author);
            expected.AgeGroup.Should().Be(_dto.AgeGroup);
            expected.BookCategoryId.Should().Be(_dto.BookCategoryId);
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
