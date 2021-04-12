using Library.Entities;
using Library.Services.Members.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Members.TestTools
{
    public class MemberBuilder
    {
        private string _fullName = "dummy-fullName";
        private string _addrss = "dummy-addrss";
        private int _age = 1;

        public Member Generate()
        {
            return new Member
            {
                FullName = _fullName,
                Address = _addrss,
                Age = _age,
            };
        }

        public MemberBuilder WithFullName(string fullName)
        {
            _fullName = fullName;
            return this;
        }
        public MemberBuilder WithAdderss(string addrss)
        {
            _addrss = addrss;
            return this;
        }
        public MemberBuilder WithAge(int age)
        {
            _age = age;
            return this;
        }

        public AddMemberDto GenerateDto()
        {
            return new AddMemberDto
            {
                FullName = _fullName,
                Address = _addrss,
                Age = _age,
            };
        }
    }
}
