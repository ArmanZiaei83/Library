using Library.Entities;
using Library.Infrastructure.Application;
using Library.Services.BookCategories.Contracts;
using Library.Services.BookCategories.Exceptions;
using Library.Services.Books.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Books
{
    public class BookAppServices : BookServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly BookRepository _repository;
        private readonly BookCategroyRepository _bookCategroyRepository;

        public BookAppServices(
            UnitOfWork unitOfWork,
            BookRepository repository,
            BookCategroyRepository bookCategroyRepository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _bookCategroyRepository = bookCategroyRepository;
        }
        public int Add(AddBookDto dto)
        {
            CheckedExistsBookCategoryById(dto.BookCategoryId);

            var book = new Book
            {
                Name = dto.Name,
                Author = dto.Author,
                AgeGroup = dto.AgeGroup,
                BookCategoryId = dto.BookCategoryId
            };

            _repository.Add(book);
            _unitOfWork.Complete();

            return book.Id;
        }

        private void CheckedExistsBookCategoryById(int bookCategoryId)
        {
            if (!_bookCategroyRepository.IsExistsById(bookCategoryId))
            {
                throw new BookCategroyNotFoundException();
            }
        }
    }
}
