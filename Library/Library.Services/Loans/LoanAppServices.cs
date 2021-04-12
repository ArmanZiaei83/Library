﻿using Library.Entities;
using Library.Infrastructure.Application;
using Library.Services.BookCategories.Contracts;
using Library.Services.Books.Contracts;
using Library.Services.Books.Exceptions;
using Library.Services.Loans.Contracts;
using Library.Services.Loans.Exceptions;
using Library.Services.Members.Contracts;
using Library.Services.Members.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Loans
{
    public class LoanAppServices : LoanServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly LoanRepository _repository;
        private readonly BookRepository _bookRepository;
        private readonly MemberRepository _memberRepository;

        public LoanAppServices(
            UnitOfWork unitOfWork,
            LoanRepository repository,
            BookRepository bookRepository,
            MemberRepository memberRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }
        public int Add(AddLoanDto dto)
        {
            var book = _bookRepository.FindById(dto.BookId);
            ThorwExceptionWhenBookNotFound(book);

            var member = _memberRepository.FindById(dto.MemeberId);
            ThorwExceptionWhenMemberNotFound(member);

            ThorwExceptionWhenMemberAgeNotAllowed(member.Age, book.AgeGroup);

            Loan loan = new Loan
            {
                ReturnDate = dto.ReturnDate,
                BookId = dto.BookId,
                MemeberId = dto.MemeberId
            };

            _repository.Add(loan);
            _unitOfWork.Complete();

            return loan.Id;
        }

        private void ThorwExceptionWhenMemberAgeNotAllowed(int memberAge, int bookAgeGroup)
        {
            if (bookAgeGroup > memberAge)
            {
                throw new ThisBookIsNotAllowedForYourAgeException();
            }
        }

        private void ThorwExceptionWhenMemberNotFound(Member member)
        {
            if (member == null)
            {
                throw new MembrNotFoundException();
            }
        }

        private void ThorwExceptionWhenBookNotFound(Book book)
        {
            if (book == null)
            {
                throw new BookNotFoundException();
            }
        }

    }
}