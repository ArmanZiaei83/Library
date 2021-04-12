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

namespace Library.Tests.Specs.Members.Add
{
    public class MemberTests
    {
        private EFDataContext _context;
        private MemberRepository _repository;
        private UnitOfWork _unitOfWork;
        private MemberServices _sut;
        private int _actual;
        private AddMemberDto _dto;

        public MemberTests()
        {
            var database = new EFInMemoryDatabase();
            _context = database.CreateDataContext<EFDataContext>();
            _repository = new EFMemberReository(_context);
            _unitOfWork = new EFUnitOfWork(_context);
            _sut = new MemberAppServices(_unitOfWork, _repository);
        }

        //هیچکس در عضویت کتابخانه ، نیست
        private void Given()
        {

        }

        //می خواهم یک شخص به نام ونام خانوادگی علی علینقی پور و سن 25 سال
        //و  آدرس صدرا  عضو کتابخانه کنم را 
        private void When()
        {
            _dto = new MemberBuilder()
                        .WithFullName("علی علینقی پور")
                        .WithAdderss("صدرا")
                        .WithAge(1)
                        .GenerateDto();
            _actual = _sut.Add(_dto);
        }

        //باید یک  شخص به نام ونام خانوادگی علی علینقی پور و سن 25 سال
        //و  آدرس صدرا  در عضویت کتابخانه باشد
        private void Then()
        {
            var expected = _context.Members.Single(_ => _.Id == _actual);
            expected.FullName.Should().Be(_dto.FullName);
            expected.Age.Should().Be(_dto.Age);
            expected.Address.Should().Be(_dto.Address);
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
