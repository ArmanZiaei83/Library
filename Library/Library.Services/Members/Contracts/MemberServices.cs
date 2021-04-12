using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Members.Contracts
{
    public interface MemberServices
    {
        int Add(AddMemberDto dto);
    }
}
