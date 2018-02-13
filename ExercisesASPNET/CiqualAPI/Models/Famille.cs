using System;
using System.Collections.Generic;

namespace CiqualAPI.Models
{
    public partial class Famille
    {
        public Famille()
        {
            Aliment = new HashSet<Aliment>();
        }

        public string CodeFamille { get; set; }
        public string Nom { get; set; }

        public ICollection<Aliment> Aliment { get; set; }
    }
}
