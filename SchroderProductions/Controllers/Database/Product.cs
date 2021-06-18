using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchroderProductions.Controllers.Database
{
    public class Festival
    {
        public int Id { get; set; }

        public string Naam { get; set; }

        public int Prijs { get; set; }

        public string Plaats { get; set; }

        public string Afbeelding { get; set;}

        public string Logo { get; set; }
    }
}
