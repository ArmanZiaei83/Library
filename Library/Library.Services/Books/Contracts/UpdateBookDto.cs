using System.ComponentModel.DataAnnotations;

namespace Library.Services.Books.Contracts
{
    public class UpdateBookDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1, 150)]
        public int AgeGroup { get; set; }
        [Required]
        public int BookCategoryId { get; set; }
    }
}