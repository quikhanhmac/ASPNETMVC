using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ciqual.Models
{
    public partial class Aliment
    {
        public Aliment()
        {
            Composition = new List<Composition>();
        }

        public int IdAliment { get; set; }
        public string Nom { get; set; }
        public string CodeFamille { get; set; }
        [Display(Name ="Famille")]
        public Famille CodeFamilleNavigation { get; set; }
        public List<Composition> Composition { get; set; }
        [NotMapped]
        [Display(Name = "Constituants")]
        public int NbConstituants { get; set; }
    }
}
