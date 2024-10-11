using System.ComponentModel.DataAnnotations;

namespace formulaire.Models
{
    public class Selection
    {
        [Key]
        public int selection_id {get; set;}
        public required string field_name {get; set;}
        public required string label {get; set;}

    }
}