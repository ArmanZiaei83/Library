using FluentAssertions;
using Library.Entities;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Books;
using Library.Persistence.EF.Loans;
using Library.Persistence.EF.Members;
using Library.Services.BookCategories.TestTools;
using Library.Services.Books.Contracts;
using Library.Services.Books.TestTools;
using Library.Services.Loans;
using Library.Services.Loans.Contracts;
using Library.Services.Loans.TestTools;
using Library.Services.Members.Contracts;
using Library.Services.Members.TestTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.Tests.Specs.Loans.Add
{
    public class Successful
    {
        private EFDataContext _context;
        private Book _book;
        private Member _member;
        private LoanRepository _repository;
        private UnitOfWork _unitOfWork;
        private BookRepository _bookRepository;
        private MemberRepository _memberRepository;
        private LoanServices _sut;
        private int _actual;
        private AddLoanDto _dto;

        public Successful()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _repository = new EFLoanRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _bookRepository = new EFBookRepository(_context);
            _memberRepository = new EFMemberReository(_context);
            _sut = new LoanAppServices(_unitOfWork, _repository,_bookRepository,_memberRepository);
        }
        //یک کتاب با نام اثر مرکب و نویسندگی دارن هاردی و رنج سنی +15 سال
        //در فهرست دسته بندی کتاب ها در دسته بندی خودیاری وجود دارد
        // و یک شخص به نام ونام خانوادگی علی علینقی پور و سن 25 سال و
        // آدرس صدرا  عضو کتابخانه است

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

            _member = new MemberBuilder()
                            .WithFullName("علی علینقی پور")
                            .WithAdderss("صدرا")
                            .WithAge(25)
                            .Generate();
            _context.Manipulate(_ => _.Members.Add(_member));
        }


        //می خواهم به  عضو کتابخانه با نام ونام خانوادگی علی علینقی پور و سن 25 سال و  آدرس صدرا
        //، کتاب با نام اثر مرکب و نویسندگی دارن هاردی و رنج سنی +15  سال
        //در دسته  بندی خودیاری را ، تا تاریخ 24/2/1399 به امانت بسپارم
        private void When()
        {
            _dto = new LoanBuilder()
                        .WithReturnDate(DateTime.Now.AddDays(1))
                        .WithMemberIdForDto(_member.Id)
                        .WithBookIdForDto(_book.Id)
                        .GenerateAddDto();

            _actual = _sut.Add(_dto);
        }

        //باید کتاب با نام اثر مرکب و نویسندگی دارن هاردی و رنج سنی +15 سال
        //در دسته  بندی خودیاری ، تا تاریخ 24/2/1399
        //به عضو کتابخانه به نام ونام خانوادگی علی علینقی پور و سن 25 سال
        //و  آدرس صدرا  ،  امانت سپرده باشم
        private void Then()
        {
            var expected = _context.Loans.Single(_ => _.Id == _actual);
            expected.ReturnDate.Should().Be(_dto.ReturnDate);
            expected.BookId.Should().Be(_dto.BookId);
            expected.MemeberId.Should().Be(_dto.MemeberId);
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
