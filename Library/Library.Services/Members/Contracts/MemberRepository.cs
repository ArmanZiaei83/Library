using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Members.Contracts
{
    public interface MemberRepository
    {
        void Add(Member member);
    }
}
