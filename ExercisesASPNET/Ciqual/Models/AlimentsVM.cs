using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ciqual.Models
{
    public class AlimentsVM
    {
        public List<Aliment> Aliments { get; set; }
        public List<Famille> Familles { get; set; }
        public string FSelect { get; set; }
       
    }
}
