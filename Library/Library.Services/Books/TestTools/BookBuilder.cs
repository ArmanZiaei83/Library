using Library.Entities;
using Library.Services.Books.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Books.TestTools
{
    public class BookBuilder
    {
        private string _name = "dummy-name";
        private string _author = "dummy-author";
        private int _ageGroup = 1;
        private int _bookCategroyId = 1;
        private BookCategory _bookCategory = new BookCategory { Title = "dummy" };

        public Book Generate()
        {
            return new Book
            {
                Name = _name,
                Author = _author,
                AgeGroup = _ageGroup,
                BookCategoryId = _bookCategroyId,
                BookCategory=_bookCategory
            };
        }

        public BookBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public BookBuilder WithAuthor(string author)
        {
            _author = author;
            return this;
        }

        public BookBuilder WithAgeGroup(int ageGroup)
        {
            _ageGroup = ageGroup;
            return this;
        }

        public BookBuilder WithBookCategroy(BookCategory bookCategory)
        {
            _bookCategory = bookCategory;
            return this;
        }

        public BookBuilder WithByBookCategroyId(int bookCategroyId)
        {
            _bookCategroyId = bookCategroyId;
            return this;
        }
        public AddBookDto GenerateAddBookDto()
        {
            return new AddBookDto
            {
                Name = "dummy-name",
                Author = "dummy-author",
                AgeGroup = 1,
                BookCategoryId = _bookCategroyId
            };
        }

    }
}
