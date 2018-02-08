using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class Calcul
    {
        [DataType(DataType.Date)]
        public DateTime DateDeb { get; set; }
        [Range(1,9999,ErrorMessage="entre 1 et 9999")]
        public int Nbj { get; set; }
        [DataType(DataType.Date)]
        public DateTime Result { get; set; }
        public string Operateur { get; set; }
    }
}
