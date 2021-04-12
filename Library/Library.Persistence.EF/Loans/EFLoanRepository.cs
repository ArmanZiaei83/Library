using Library.Entities;
using Library.Services.Loans.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.EF.Loans
{
    public class EFLoanRepository: LoanRepository
    {
        private readonly EFDataContext _context;

        public EFLoanRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Loan loan)
        {
            _context.Loans.Add(loan);
        }

        public Loan FindById(int id)
        {
            return _context.Loans
                           .Find(id);
        }
    }
}
