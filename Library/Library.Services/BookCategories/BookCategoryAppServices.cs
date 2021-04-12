using Library.Entities;
using Library.Infrastructure.Application;
using Library.Services.BookCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.BookCategories
{
    public class BookCategoryAppServices: BookCategroyServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly BookCategroyRepository _repository;

        public BookCategoryAppServices(UnitOfWork unitOfWork,BookCategroyRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public int Add(AddBookCategroyDto dto)
        {
            var bookCategory = new BookCategory
            {
                Title = dto.Title
            };

            _repository.Add(bookCategory);
            _unitOfWork.Complete();

            return bookCategory.Id;
        }
    }
}
