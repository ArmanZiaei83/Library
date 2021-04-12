using Library.Entities;
using Library.Services.Members.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Persistence.EF.Members
{
    public class EFMemberReository:MemberRepository
    {
        private readonly EFDataContext _context;

        public EFMemberReository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Member member)
        {
            _context.Members.Add(member);
        }
    }
}
