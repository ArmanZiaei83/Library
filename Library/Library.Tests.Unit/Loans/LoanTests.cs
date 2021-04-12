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
using Library.Services.Books.Exceptions;
using Library.Services.Books.TestTools;
using Library.Services.Loans;
using Library.Services.Loans.Contracts;
using Library.Services.Loans.Exceptions;
using Library.Services.Loans.TestTools;
using Library.Services.Members.Contracts;
using Library.Services.Members.Exceptions;
using Library.Services.Members.TestTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.Tests.Unit.Loans
{
    public class LoanTests
    {
        private EFDataContext _context;
        private LoanRepository _repository;
        private UnitOfWork _unitOfWork;
        private BookRepository _bookRepository;
        private MemberRepository _memberRepository;
        private LoanServices _sut;

        public LoanTests()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _repository = new EFLoanRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _bookRepository = new EFBookRepository(_context);
            _memberRepository = new EFMemberReository(_context);
            _sut = new LoanAppServices(_unitOfWork, _repository,_bookRepository,_memberRepository);
        }

        [Fact]
        public void Add_add_a_loan_properly()
        {
            var bookCategroy = new BookCategoryFactory()
                                   .Generate();
            var book = new BookBuilder()
                            .WithBookCategroy(bookCategroy)
                            .Generate();
            _context.Manipulate(_ => _.Books.Add(book));
            var member = new MemberBuilder()
                            .Generate();
            _context.Manipulate(_ => _.Members.Add(member));
            var dto = new LoanBuilder()
                        .WithReturnDate(DateTime.Now.AddDays(1))
                        .WithMemberIdForDto(member.Id)
                        .WithBookIdForDto(book.Id)
                        .GenerateAddDto();

            var actual = _sut.Add(dto);

            var expected = _context.Loans.Single(_ => _.Id == actual);
            expected.ReturnDate.Should().Be(dto.ReturnDate);
            expected.BookId.Should().Be(dto.BookId);
            expected.MemeberId.Should().Be(dto.MemeberId);
        }

        [Fact]
        public void Add_throw_exception_when_book_not_found()
        {
            var book = new BookBuilder()
                            .Generate();
            var member = new MemberBuilder()
                            .Generate();
            _context.Manipulate(_ => _.Members.Add(member));
            var dto = new LoanBuilder()
                        .WithReturnDate(DateTime.Now.AddDays(1))
                        .WithMemberIdForDto(member.Id)
                        .WithBookIdForDto(book.Id)
                        .GenerateAddDto();

            Action expected = () => _sut.Add(dto);

            expected.Should().Throw<BookNotFoundException>();
        }

        [Fact]
        public void Add_throw_exception_when_member_not_found()
        {
            var book = new BookBuilder()
                            .Generate();
            _context.Manipulate(_ => _.Books.Add(book));
            var member = new MemberBuilder()
                            .Generate();
            var dto = new LoanBuilder()
                        .WithReturnDate(DateTime.Now.AddDays(1))
                        .WithMemberIdForDto(member.Id)
                        .WithBookIdForDto(book.Id)
                        .GenerateAddDto();

            Action expected = () => _sut.Add(dto);

            expected.Should().Throw<MembrNotFoundException>();
        }

        [Fact]
        public void Add_throw_exception_when_there_is_an_age_limit()
        {
            var bookCategroy = new BookCategoryFactory()
                                   .Generate();
            var book = new BookBuilder()
                            .WithAgeGroup(2)
                            .WithBookCategroy(bookCategroy)
                            .Generate();
            _context.Manipulate(_ => _.Books.Add(book));
            var member = new MemberBuilder()
                            .WithAge(1)
                            .Generate();
            _context.Manipulate(_ => _.Members.Add(member));
            var dto = new LoanBuilder()
                        .WithReturnDate(DateTime.Now.AddDays(1))
                        .WithMemberIdForDto(member.Id)
                        .WithBookIdForDto(book.Id)
                        .GenerateAddDto();

            Action expected = () => _sut.Add(dto);

            expected.Should().Throw<ThisBookIsNotAllowedForYourAgeException>();
        }

        [Fact]
        public void GetBook_throw_exception_when_was_daley()
        {
            var book = new BookBuilder()
                            .Generate();
            var member = new MemberBuilder()
                            .Generate();
            var loan = new LoanBuilder()
                            .WithMember(member)
                            .WithBook(book)
                            .WithReturnDate(DateTime.Now)
                            .Generate();
            _context.Manipulate(_ => _.Loans.Add(loan));

            Action expected = () => _sut.GetBook(loan.Id);

            expected.Should().Throw<DelayInBookDeliveryException>();
        }

        [Fact]
        public void GetBook_thorw_exception_when_loan_not_found()
        {
            var loan = new LoanBuilder()
                            .Generate();
            //var getBookDto = new GetBookDto
            //{
            //    ReturnDate = loan.ReturnDate.AddDays(1)
            //};

            Action expected = () => _sut.GetBook(loan.Id);

            expected.Should().Throw<LoanNotFoundException>();
        }

    }
}
