using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.Books.Contracts
{
    public class AddBookDto
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1,150)]
        public int AgeGroup { get; set; }
        [Required]
        public int BookCategoryId { get; set; }
    }
}
