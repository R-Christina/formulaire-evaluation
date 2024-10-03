using formulaire.Data.DbContexts;
using formulaire.Models;
using Microsoft.AspNetCore.Mvc;

namespace formulaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Header_selectionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;

        public Header_selectionController(IConfiguration configuration, AppDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Header_selection>> GetHeaderselection()
        {
            var headerselection = _dbContext.header_selection.ToList();
            return Ok(headerselection);
        }
    }
}