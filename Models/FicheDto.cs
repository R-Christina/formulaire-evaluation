namespace formulaire.Models
{
    public class FicheDto
    {
        public Fiche fiche { get; set; }  // Fiche à créer
        public List<Header_selection> header_selections { get; set; } = new List<Header_selection>();  // Liste des Header_selection
        public List<Th> ths { get; set; } = new List<Th>();  // Liste des Th
        public List<Td_principale> td_principales { get; set; } = new List<Td_principale>();  // Liste des Td_principale
    }
}