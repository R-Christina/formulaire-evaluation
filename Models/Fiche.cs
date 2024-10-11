using System.ComponentModel.DataAnnotations;

namespace formulaire.Models
{
    public class Fiche
    {
        [Key]
        public int fiche_id {get; set;}
        public DateTime? creation {get;set;}

        public ICollection<Header_selection> header_selections { get; set; } = new List<Header_selection>();
        public ICollection<Th>? ths { get; set; }
        public ICollection<Td_principale>? td_principales { get; set; }
    }
}