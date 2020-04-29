using AuthorsAPI.Helpers.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAPI.Models.DTO
{
    public class AuthorCreateDTO
    {
        [Required]
        [CapitalWordAttribute]
        [StringLength(10, ErrorMessage = "Name must contain at least {1} characters or less.")]
        public string Name { get; set; }

        [Required]
        [Range(18, 99)]

        public int Age { get; set; }

        public DateTime DOB { get; set; }
    }
}
