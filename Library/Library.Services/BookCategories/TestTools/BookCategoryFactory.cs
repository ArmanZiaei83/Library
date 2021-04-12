using Library.Entities;
using Library.Services.BookCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.BookCategories.TestTools
{
    public class BookCategoryFactory
    {
        private string _title = "dummy";

        public  BookCategory Generate()
        {
            return new BookCategory { Title = _title };
        }

        public AddBookCategroyDto GenerateDto()
        {
            return new AddBookCategroyDto { Title = _title };
        }

        public BookCategoryFactory WithTitle(string title)
        {
            _title = title;
            return this;
        }
    }
}
