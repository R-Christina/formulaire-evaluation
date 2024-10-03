using System.ComponentModel.DataAnnotations;

namespace formulaire.Models
{
    public class Form
    {
        [Key]
        public int form_id {get; set;}
        public int type_formulaire {get; set;}
        public string? titre_formulaire {get; set;}
        public virtual ICollection<Header_form>? header_form { get; set; }

    }
}