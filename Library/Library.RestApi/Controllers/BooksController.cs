using Library.Services.Books.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [ApiController,Route("api/books")]
    public class BooksController : Controller
    {
        private readonly BookServices services;

        public BooksController(BookServices services)
        {
            this.services = services;
        }

        [HttpPost]
        public int Add(AddBookDto dto)
        {
            return services.Add(dto);
        }

        [HttpPut("id")]
        public void Update(int id,UpdateBookDto dto)
        {
             services.Update(id, dto);
        }

        [HttpGet("bookCategoryId")]
        public List<GetByBookCategoryDto> GetByBookCategory(int bookCategoryId)
        {
            return services.GetByBookCategory(bookCategoryId);
        }
    }
}
