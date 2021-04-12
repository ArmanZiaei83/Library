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

namespace Library.Test.Unit.BookCategries
{
    public class BookCategroyTests
    {

        private readonly EFDataContext _context;
        private readonly EFDataContext _readDataContext;
        private readonly BookCategroyRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly BookCategroyServices _sut;

        public BookCategroyTests()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _readDataContext = database.CreateDataContext<EFDataContext>();
            _repository = new EFBookCategroyRepository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new BookCategoryAppServices(_unitOfWork, _repository);
        }
        [Fact]
        public void Add_add_a_bookCategroy_properly()
        {
            var dto = new BookCategoryFactory()
                        .GenerateDto();

            var actual = _sut.Add(dto);

            var expected = _context.BookCategories.Single(_ => _.Id == actual);
            expected.Title.Should().Be(dto.Title);
        }
    }
}
