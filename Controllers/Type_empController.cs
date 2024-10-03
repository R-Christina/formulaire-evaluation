using formulaire.Data.DbContexts;
using formulaire.Models;
using Microsoft.AspNetCore.Mvc;

namespace formulaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Type_empController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;

        public Type_empController(IConfiguration configuration, AppDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Type_emp>> GetTypeEmps()
        {
            var typeEmps = _dbContext.type_emp.ToList();
            return Ok(typeEmps);
        }

        [HttpGet("/type")]
        public IActionResult GetType_empById(int type_emp_id)
        {
            var type_emp = _dbContext.type_emp.Where(e => e.type_emp_id == type_emp_id).ToList();
            return Ok(type_emp);
        }

    }
}