using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime ReturnDate { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
        public int MemeberId { get; set; }
        public Member Member { get; set; }

    }
}
