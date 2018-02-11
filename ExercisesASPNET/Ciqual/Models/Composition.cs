using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ciqual.Models
{
    public partial class Composition
    {
        public int IdAliment { get; set; }
        public int IdConstituant { get; set; }
        [Display(Name ="Teneur Moy")]
        public string ValeurMoy { get; set; }
        [Display(Name = "Teneur Min")]
        public double? ValeurMin { get; set; }
        [Display(Name = "Teneur Max")]
        public double? ValeurMax { get; set; }
        public string NoteConfiance { get; set; }

        public Aliment IdAlimentNavigation { get; set; }
        public Constituant IdConstituantNavigation { get; set; }
    }
}
