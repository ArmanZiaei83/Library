using Library.Entities;
using Library.Infrastructure.Application;
using Library.Services.BookCategories.Contracts;
using Library.Services.BookCategories.Exceptions;
using Library.Services.Books.Contracts;
using Library.Services.Books.Exceptions;
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
            ThrowExceptionWhenBookCategroyNotFound(dto.BookCategoryId);

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

        private void ThrowExceptionWhenBookCategroyNotFound(int bookCategoryId)
        {
            if (!_bookCategroyRepository.IsExistsById(bookCategoryId))
            {
                throw new BookCategroyNotFoundException();
            }
        }

        public void Update(int id, UpdateBookDto dto)
        {
            var book = _repository.FindById(id);
            ThrowExceptionWhenBookNotFound(book);
            ThrowExceptionWhenBookCategroyNotFound(dto.BookCategoryId);

            book.Name = dto.Name;
            book.Author = dto.Author;
            book.AgeGroup = dto.AgeGroup;
            book.BookCategoryId = dto.BookCategoryId;

            _unitOfWork.Complete();

        }

        private void ThrowExceptionWhenBookNotFound(Book book)
        {
            if (book == null)
            {
                throw new BookNotFoundException();
            }
        }

        public List<GetByBookCategoryDto> GetByBookCategory(int bookCategoryId)
        {
            ThrowExceptionWhenBookCategroyNotFound(bookCategoryId);
            return _repository.GetByBookCategory(bookCategoryId);
        }
    }
}
