using AuthorsAPI.Helpers.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuthorsAPI.Entities
{
    public class Author //: IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public int Age { get; set; }
        public List<Book> Books{ get; set; }
        public DateTime DOB { get; set; }

        //[CreditCard]
        //public string CreditCard { get; set; }

        //[Url]
        //public string Url { get; set; }

        //Model validation
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (!string.IsNullOrEmpty(Name))
        //    {
        //        var firstLetter = Name[0].ToString();
        //        if (firstLetter != firstLetter.ToUpper())
        //            yield return new ValidationResult("First letter should be Uppercase");
        //    }
        //}
    }
}
