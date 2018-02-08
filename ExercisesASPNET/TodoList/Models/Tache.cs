using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Models
{
    public class Tache: IValidatableObject
    {
        public int Id { get; set; }
        [Required,MaxLength(250),DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateEcheance { get; set; }
        public bool Terminee { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DateEcheance< DateCreation)
            {
                yield return new ValidationResult("dsfb", new string[] { "DateEcheance" });
            }
        }
    }
}
