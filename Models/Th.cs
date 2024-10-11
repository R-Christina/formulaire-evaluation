using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace formulaire.Models
{
    public class Th
    {
        [Key]
        public int th_id {get; set;}
        public required int th_num {get;set;}
        public required string th_nom {get;set;}
        
        [ForeignKey("fiche")]
        public int fiche_id {get;set;}        
        public Fiche? fiche {get; set;}
    }
}