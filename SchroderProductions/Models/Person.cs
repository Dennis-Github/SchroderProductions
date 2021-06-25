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
        public string Name { get; set; }
        [Required(ErrorMessage ="Vul een achternaam in")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage ="Vul een geldig Email in")]
        public string Email { get; set; }
        public string Country { get; set; }
        public string Subject { get; set; }

    }
}
