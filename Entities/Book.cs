﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAPI.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Author Id cannot be 0.")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
