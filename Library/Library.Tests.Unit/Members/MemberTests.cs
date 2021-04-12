using FluentAssertions;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Members;
using Library.Services.Members;
using Library.Services.Members.Contracts;
using Library.Services.Members.TestTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.Tests.Unit.Members
{
    public class MemberTests
    {
        private EFDataContext _context;
        private MemberRepository _repository;
        private UnitOfWork _unitOfWork;
        private MemberServices _sut;
        public MemberTests()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _repository = new EFMemberReository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new MemberAppServices(_unitOfWork, _repository);
        }

        [Fact]
        public void Add_add_a_member_properly()
        {
             var dto = new MemberBuilder()
                         .WithFullName("علی علینقی پور")
                         .WithAdderss("صدرا")
                         .WithAge(1)
                         .GenerateDto();

            var actual = _sut.Add(dto);

            var expected = _context.Members.Single(_ => _.Id == actual);
            expected.FullName.Should().Be(dto.FullName);
            expected.Age.Should().Be(dto.Age);
            expected.Address.Should().Be(dto.Address);
        }
    }
}
