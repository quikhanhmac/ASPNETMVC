﻿using System;
using System.Collections.Generic;

namespace Ciqual.Models
{
    public partial class Composition
    {
        public int IdAliment { get; set; }
        public int IdConstituant { get; set; }
        public string ValeurMoy { get; set; }
        public double? ValeurMin { get; set; }
        public double? ValeurMax { get; set; }
        public string NoteConfiance { get; set; }

        public Aliment IdAlimentNavigation { get; set; }
        public Constituant IdConstituantNavigation { get; set; }
    }
}
