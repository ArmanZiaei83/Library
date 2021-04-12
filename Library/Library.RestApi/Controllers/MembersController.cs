using Library.Services.Members.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [ApiController,Route("api/members")]
    public class MembersController : Controller
    {
        private readonly MemberServices services;

        public MembersController(MemberServices services)
        {
            this.services = services;
        }
        
        [HttpPost]
        public int Add(AddMemberDto dto)
        {
            return services.Add(dto);
        }
    }
}
