using Library.Entities;
using Library.Infrastructure.Application;
using Library.Services.Members.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Members
{
    public class MemberAppServices : MemberServices
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly MemberRepository _repository;

        public MemberAppServices(UnitOfWork unitOfWork,MemberRepository repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public int Add(AddMemberDto dto)
        {
            var member = new Member
            {
                FullName = dto.FullName,
                Address = dto.Address,
                Age = dto.Age,
            };

            _repository.Add(member);
            _unitOfWork.Complete();

            return member.Id;
        }
    }
}
