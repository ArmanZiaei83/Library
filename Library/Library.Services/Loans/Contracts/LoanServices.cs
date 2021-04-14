﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Loans.Contracts
{
    public interface LoanServices
    {
        int Add(AddLoanDto dto);
        void BookDeliveryTake(int id,BookDeliveryTakeDto dto);
    }
}
