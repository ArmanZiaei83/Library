using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.Members.Contracts
{
    public class AddMemberDto
    {
        [Required]
        public string FullName { get; set; }
        [Range(1, 150)]
        [Required]
        public int Age { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
