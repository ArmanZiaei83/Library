using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.Loans.Contracts
{
    public class AddLoanDto
    {
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int MemeberId { get; set; }
    }
}
