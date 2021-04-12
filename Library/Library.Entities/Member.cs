using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    public class Member
    {

        public Member()
        {
            Loans = new HashSet<Loan>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public HashSet<Loan> Loans { get; set; }
    }
}
