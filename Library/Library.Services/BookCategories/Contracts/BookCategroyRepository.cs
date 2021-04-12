using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.BookCategories.Contracts
{
    public interface BookCategroyRepository
    {
        void Add(BookCategory bookCategory);
    }
}
