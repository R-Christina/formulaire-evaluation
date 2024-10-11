using formulaire.Data.DbContexts;
using formulaire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace formulaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FicheController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FicheController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFiche([FromBody] FicheDto ficheDto)
        {
            if (ficheDto == null || ficheDto.fiche == null)
            {
                return BadRequest("FicheDto data is null.");
            }

            // 1. Insérer dans la table Fiche
            ficheDto.fiche.creation = DateTime.Now;  // Définir l'heure de création
            _context.fiche.Add(ficheDto.fiche);
            await _context.SaveChangesAsync();

            // Récupérer l'ID de la fiche nouvellement créée
            int ficheId = ficheDto.fiche.fiche_id;

            // 2. Insérer dans Header_selection avec les autres champs
            foreach (var header in ficheDto.header_selections)
            {
                header.fiche_id = ficheId;  // Assign fiche ID to header_selection
                _context.header_selection.Add(header);
            }


            // 3. Insérer dans Th avec les autres champs
            foreach (var th in ficheDto.ths)
            {
                th.fiche_id = ficheId; // Assignez l'ID de la fiche
                _context.th.Add(th);
            }

            // 4. Insérer dans Td_principale avec les autres champs
            foreach (var td in ficheDto.td_principales)
            {
                td.fiche_id = ficheId; // Assignez l'ID de la fiche
                _context.td_principale.Add(td);
            }

            await _context.SaveChangesAsync();

            // Retourner un message de succès
            return Ok(new { message = "Fiche and related data inserted successfully", ficheId });
        }

        [HttpGet("liste/{ficheId}")]
        public async Task<IActionResult> GetFicheWithDetails(int ficheId)
        {
            var ficheWithDetails = await _context.fiche
                .Where(f => f.fiche_id == ficheId)
                .Include(f => f.header_selections)   
                .Include(f => f.ths)         
                .Include(f => f.td_principales)
                .FirstOrDefaultAsync();

            if (ficheWithDetails == null)
            {
                return NotFound("Fiche not found.");
            }

            return Ok(ficheWithDetails);
        }

        [HttpGet("liste")]
        public async Task<IActionResult> GetAllFichesWithDetails()
        {
            var fichesWithDetails = await _context.fiche
                .Include(f => f.header_selections)
                    .ThenInclude(hs => hs.selection)
                .Include(f => f.ths)
                .Include(f => f.td_principales)
                .Select(f => new
                {
                    f.fiche_id,
                    f.creation,
                    header_selections = f.header_selections.Select(hs => new
                    {
                        hs.header_selection_id,
                        hs.selection_id,
                        SelectionLabel = hs.selection.label
                    }),
                    ths = f.ths.Select(th => new
                    {
                        th.th_id,
                        th.th_num,
                        th.th_nom
                    }),
                    Td_principales = f.td_principales.Select(td => new
                    {
                        td.td_principale_id,
                        td.td_principale_num,
                        td.td_principale_nom,
                        td.ligne
                    })
                })
                .ToListAsync();

            if (fichesWithDetails == null || !fichesWithDetails.Any())
            {
                return NotFound("Aucune fiche trouvée.");
            }

            return Ok(fichesWithDetails);
        }

    }
}
