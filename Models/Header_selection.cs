using System.ComponentModel.DataAnnotations;

namespace formulaire.Models
{
    public class Header_selection
    {
        [Key]
        public int header_selection_id {get; set;}
        public required string field_name {get; set;}
        public required string label {get; set;}
    }
}