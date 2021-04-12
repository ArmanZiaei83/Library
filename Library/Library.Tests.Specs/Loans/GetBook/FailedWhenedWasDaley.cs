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
using Library.Services.Loans.Exceptions;
using Library.Services.Loans.TestTools;
using Library.Services.Members.Contracts;
using Library.Services.Members.TestTools;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Library.Tests.Specs.Loans.GetBook
{
    public class FailedWhenedWasDaley
    {

        private EFDataContext _context;
        private Loan _loan;
        private LoanRepository _repository;
        private UnitOfWork _unitOfWork;
        private BookRepository _bookRepository;
        private MemberRepository _memberRepository;
        private LoanServices _sut;
        private Action _expected;

        public FailedWhenedWasDaley()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _repository = new EFLoanRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _bookRepository = new EFBookRepository(_context);
            _memberRepository = new EFMemberReository(_context);
            _sut = new LoanAppServices(_unitOfWork, _repository, _bookRepository, _memberRepository);
        }

        //یک کتاب با نام اثر مرکب و نویسندگی دارن هاردی و رنج سنی +15 سال
        //در فهرست دسته بندی کتاب ها در دسته بندی خودیاری را
        //به عضو کتابخانه با با نام ونام خانوادگی علی علینقی پور و سن 25 سال
        //و  آدرس صدرا   ،تا تاریخ 27/03/1400 به امانت سپرده ام.

        private void Given()
        {
            var bookCategory = new BookCategoryFactory()
                                    .WithTitle("خودیاری")
                                    .Generate();
            var book = new BookBuilder()
                            .WithName("اثر مرکب")
                            .WithAuthor("دارن هاردی")
                            .WithAgeGroup(15)
                            .WithBookCategroy(bookCategory)
                            .Generate();
            var member = new MemberBuilder()
                            .WithFullName("علی علینقی پور")
                            .WithAge(25)
                            .WithAdderss("صدرا")
                            .Generate();
            _loan = new LoanBuilder()
                            .WithMember(member)
                            .WithBook(book)
                            .WithReturnDate(DateTime.Now)
                            .Generate();
            _context.Manipulate(_ => _.Loans.Add(_loan));
        }


        //عضو کتابخانه با نام ونام خانوادگی علی علینقی پور و سن 25 سال و  آدرس صدرا   
        //، کتاب با نام اثر مرکب و نویسندگی دارن هاردی و رنج سنی +15  سال
        //در دسته بندی خودیاری را در تاریخ 28/03/1400 برای تحویل اورد

        private void When()
        {
            _expected = () => _sut.GetBook(_loan.Id);
        }

        //باید خطایی با عنوان "تاخیر در تحویل کتاب" رخ دهد
        private void Then()
        {
            _expected.Should().Throw<DelayInBookDeliveryException>();
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
