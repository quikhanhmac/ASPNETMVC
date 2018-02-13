using System;
using System.Collections.Generic;

namespace CiqualAPI.Models
{
    public partial class Aliment
    {
        public Aliment()
        {
            Composition = new HashSet<Composition>();
        }

        public int IdAliment { get; set; }
        public string Nom { get; set; }
        public string CodeFamille { get; set; }

        public Famille CodeFamilleNavigation { get; set; }
        public ICollection<Composition> Composition { get; set; }
    }
}
