using Library.Services.Loans.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [ApiController,Route("api/loans")]
    public class LoansController : Controller
    {
        private readonly LoanServices services;

        public LoansController(LoanServices services)
        {
            this.services = services;
        }

        [HttpPost]
        public int Add(AddLoanDto dto)
        {
            return services.Add(dto);
        }

        [HttpPost("LoanId")]
        public void BookDeliveryTake(int LoanId,BookDeliveryTakeDto dto)
        {
            services.BookDeliveryTake(LoanId,dto);
        }
    }
}
