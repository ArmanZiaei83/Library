using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Books.Contracts
{
    public interface BookRepository
    {
        void Add(Book book);
        Book FindById(int id);
    }
}
