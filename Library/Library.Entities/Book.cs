using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    public class Book
    {

        public Book()
        {
            Loans = new HashSet<Loan>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int AgeGroup { get; set; }

        public int BookCategoryId { get; set; }
        public BookCategory BookCategory { get; set; }
        public HashSet<Loan> Loans { get; set; }
    }
}
