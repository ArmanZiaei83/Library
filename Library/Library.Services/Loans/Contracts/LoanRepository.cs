using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Loans.Contracts
{
    public interface LoanRepository
    {
        void Add(Loan loan);
        Loan FindById(int id);
    }
}
