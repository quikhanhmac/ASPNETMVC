using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ciqual.Models
{
    public partial class Famille
    {
        public Famille()
        {
            Aliment = new HashSet<Aliment>();
        }
        [Display(Name ="Code")]
        public string CodeFamille { get; set; }
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        public ICollection<Aliment> Aliment { get; set; }
    }
}
