﻿namespace Library.Services.Books.Contracts
{
    public class GetByBookCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int AgeGroup { get; set; }
    }
}