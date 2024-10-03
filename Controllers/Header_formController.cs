using formulaire.Data.DbContexts;
using formulaire.Models;
using Microsoft.AspNetCore.Mvc;

namespace formulaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Header_formController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;

        public Header_formController(IConfiguration configuration, AppDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }
    }
}    