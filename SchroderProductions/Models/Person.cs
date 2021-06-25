using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchroderProductions.Models
{
    public class Person
    {
        [Required(ErrorMessage ="Vul een naam in")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Vul een achternaam in")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Vul een geldig emailadres in")]
        [EmailAddress(ErrorMessage ="Vul een geldig emailadres in")]
        
        public string Email { get; set; }

        [Required(ErrorMessage ="Vul een datum in")]
        public DateTime BirthDate { get; set; }

        public string Country { get; set; }
        
        public string Subject { get; set; }

    }
}
