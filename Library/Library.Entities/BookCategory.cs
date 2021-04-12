using System.Collections.Generic;

namespace Library.Entities
{
    public class BookCategory
    {
        public BookCategory()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public HashSet<Book> Books { get; set; }
    }
}