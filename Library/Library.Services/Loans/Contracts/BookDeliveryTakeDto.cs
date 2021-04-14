using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.Loans.Contracts
{
    public class BookDeliveryTakeDto
    {
        [Required]
        public DateTime ReturnDate { get; set; }
    }
}
