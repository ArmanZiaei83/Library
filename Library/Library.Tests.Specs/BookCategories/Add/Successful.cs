using FluentAssertions;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.BookCategories;
using Library.Services.BookCategories;
using Library.Services.BookCategories.Contracts;
using Library.Services.BookCategories.TestTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.Test.Specs.BookCategories
{
    public class Successful
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readDataContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly BookCategroyRepository _repository;
        private readonly BookCategroyServices _sut;
        private int _actual;
        private  AddBookCategroyDto _dto;

        public Successful()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _readDataContext = database.CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_context);
            _repository = new EFBookCategroyRepository(_context);
            _sut = new BookCategoryAppServices(_unitOfWork, _repository);
        }

        //در فهرست دسته بندی کتاب ها ، دسته بندی خاصی وجود ندارد
        private void Given()
        {

        }

        //می خواهم یک دسته بندی با عنوان خودیاری تعریف کنم
        private void When()
        {
            _dto = new BookCategoryFactory()
                        .WithTitle("خودیاری")
                        .GenerateDto();

            _actual = _sut.Add(_dto);
        }

        //باید یک دسته بندی با عنوان خودیاری ، در فهرست دسته بندی کتاب ها موجود باشد
        private void Then()
        {
            var expceted = _context.BookCategories.Single(_ => _.Id == _actual);
            expceted.Title.Should().Be(_dto.Title);
        }

        [Fact]
        public void Run()
        {
            Given();
            When();
            Then();
        }
    }
}
