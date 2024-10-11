using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace formulaire.Models
{
    public class Td_principale
    {
        [Key]
        public int td_principale_id {get; set;}
        public required int td_principale_num {get;set;}
        public required string td_principale_nom {get;set;}
        public required int ligne {get;set;}

        [ForeignKey("fiche")]
        public int fiche_id {get;set;}
        public Fiche? fiche {get; set;}
    }
}