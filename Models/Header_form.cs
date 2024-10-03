using System.ComponentModel.DataAnnotations;

namespace formulaire.Models
{
    public class Header_form
    {
        [Key]
        public int header_form_id {get; set;}
        public required int form_id {get; set;}
        public required int header_selection_id {get; set;}
        public Form form {get; set;}
    }
}