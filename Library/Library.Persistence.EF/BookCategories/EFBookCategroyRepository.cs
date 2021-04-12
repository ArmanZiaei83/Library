using Library.Entities;
using Library.Services.BookCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Persistence.EF.BookCategories
{
    public class EFBookCategroyRepository:BookCategroyRepository
    {
        private readonly EFDataContext _context;

        public EFBookCategroyRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(BookCategory bookCategory)
        {
            _context.BookCategories
                    .Add(bookCategory);
        }

        public bool IsExistsById(int bookCategoryId)
        {
            return _context.BookCategories
                           .Any(_ => _.Id == bookCategoryId);
        }
    }
}
