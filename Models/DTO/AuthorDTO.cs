using AuthorsAPI.Helpers.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAPI.Models.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
