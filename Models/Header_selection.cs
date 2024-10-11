using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace formulaire.Models
{
    public class Header_selection
    {
        [Key]
        public int header_selection_id { get; set; }

        [ForeignKey("fiche")]
        public int fiche_id { get; set; }

        [ForeignKey("selection")]
        public int selection_id { get; set; }

        public Fiche? fiche { get; set; }
        public Selection? selection { get; set; }
    }
}