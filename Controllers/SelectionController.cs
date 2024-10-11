using formulaire.Data.DbContexts;
using formulaire.Models;
using Microsoft.AspNetCore.Mvc;

namespace formulaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelectionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SelectionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("liste")]
        public ActionResult<IEnumerable<Selection>> GetSelection()
        {
            var selection = _context.selection.ToList();
            return Ok(selection);
        }
    }
}