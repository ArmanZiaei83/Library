using Library.Services.BookCategories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [ApiController,Route("api/book-categrories")]
    public class BookCategroriesController : Controller
    {
        private readonly BookCategroyServices services;

        public BookCategroriesController(BookCategroyServices services)
        {
            this.services = services;
        }

        [HttpPost]
        public int Add(AddBookCategroyDto dto)
        {
            return services.Add(dto);
        }
    }
}
