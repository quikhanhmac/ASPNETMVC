using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class TacheVM
    {
        public List<Tache> Taches { get; set; }
        public string TSelect { get; set; }
    }
}
