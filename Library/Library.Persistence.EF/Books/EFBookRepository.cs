using Library.Entities;
using Library.Services.Books.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Persistence.EF.Books
{
    public class EFBookRepository : BookRepository
    {
        private readonly EFDataContext _context;

        public EFBookRepository(EFDataContext context)
        {
            _context = context;
        }
        public void Add(Book book)
        {
            _context.Books
                    .Add(book);
        }

    }
}
