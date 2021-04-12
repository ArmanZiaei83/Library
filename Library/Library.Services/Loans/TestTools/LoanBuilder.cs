using Library.Entities;
using Library.Services.Loans.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Loans.TestTools
{
    public class LoanBuilder
    {
        private Member _member = new Member();
        private Book _book = new Book();
        private int _memberId = 1;
        private int _bookId = 1;
        private DateTime _returnDate = DateTime.Now.AddDays(1);

        public Loan Generate()
        {
            return new Loan
            {
                Member = _member,
                Book = _book,
                ReturnDate=_returnDate
            };
        }

        public LoanBuilder WithMember(Member member)
        {
            _member = member;
            return this;
        }

        public LoanBuilder WithReturnDate(DateTime returnDate)
        {
            _returnDate = returnDate;
            return this;
        }

        public LoanBuilder WithBook(Book book)
        {
            _book = book;
            return this;
        }

        public AddLoanDto GenerateAddDto()
        {
            return new AddLoanDto
            {
                MemeberId = _memberId,
                BookId = _bookId,
                ReturnDate = _returnDate
            };
        }

        public LoanBuilder WithMemberIdForDto(int memberId)
        {
            _memberId = memberId;
            return this;
        }
        public LoanBuilder WithBookIdForDto(int bookId)
        {
            _bookId = bookId;
            return this;
        }
    }
}
