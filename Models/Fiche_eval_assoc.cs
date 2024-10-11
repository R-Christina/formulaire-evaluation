using System.ComponentModel.DataAnnotations;

namespace formulaire.Models
{
    public class Fiche_eval_assoc
    {
        [Key]
        public int fiche_eval_assoc {get; set;}
        public required int fiche_id {get;set;}
        public required int eval_id {get;set;}
        public Fiche? fiche {get; set;}
        public Evaluation? eval {get; set;}
    }
}