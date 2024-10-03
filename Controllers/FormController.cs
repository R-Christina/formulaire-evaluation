using formulaire.Data.DbContexts;
using formulaire.Models;
using Microsoft.AspNetCore.Mvc;

    namespace formulaire.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class FormController : ControllerBase
        {
            private readonly IConfiguration _configuration;
            private readonly AppDbContext _dbContext;

            public FormController(IConfiguration configuration, AppDbContext dbContext, ILogger<FormController> logger)
            {
                _configuration = configuration;
                _dbContext = dbContext;
            }

            [HttpGet("list")]
            public ActionResult<IEnumerable<Form>> GetForm()
            {
                var form = _dbContext.form.ToList();
                return Ok(form);
            }

            [HttpPost]
            public async Task<IActionResult> PostForm([FromBody] Form form)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _dbContext.form.Add(form);  
                        await _dbContext.SaveChangesAsync(); 
                        return Ok(new { message = "Formulaire inséré avec succès", data = form });
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, new { message = "Erreur lors de l'insertion", error = ex.Message });
                    }
                }

                return BadRequest(ModelState);
            }

            [HttpPost("header")]
            public async Task<IActionResult> PostFormWithHeaderSelection([FromBody] form_header_request request)
            {
                if (ModelState.IsValid)
                {
                    using var transaction = await _dbContext.Database.BeginTransactionAsync();
                    try
                    {
                        // 1. Insertion du nouveau formulaire
                        var newForm = new Form
                        {
                            type_formulaire = request.form.type_formulaire,
                            titre_formulaire = request.form.titre_formulaire
                        };

                        _dbContext.form.Add(newForm);
                        await _dbContext.SaveChangesAsync();

                        // 2. Récupérer l'ID du formulaire inséré
                        int newFormId = newForm.form_id;
                        

                        // 3. Insertion des enregistrements dans Header_form pour chaque header_selection_id
                        foreach (var header_selection_id in request.header_selection_id)
                        {
                            var newHeaderForm = new Header_form
                            {
                                form_id = newFormId,
                                header_selection_id = header_selection_id
                            };
                            _dbContext.header_form.Add(newHeaderForm);
                        }

                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return Ok(new { message = "Formulaire et Header_form insérés avec succès" });
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return StatusCode(500, new { message = "Erreur lors de l'insertion", error = ex.Message });
                    }
                }

                return BadRequest(ModelState);
            }


            [HttpDelete("header/{formId}")]
            public async Task<IActionResult> DeleteFormWithHeaderSelection(int formId)
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    // 1. Récupérer le formulaire à supprimer
                    var form = await _dbContext.form.FindAsync(formId);
                    if (form == null)
                    {
                        return NotFound(new { message = "Formulaire non trouvé" });
                    }

                    // 2. Supprimer les enregistrements correspondants dans Header_form
                    var headerForms = _dbContext.header_form.Where(hf => hf.form_id == formId);
                    _dbContext.header_form.RemoveRange(headerForms);

                    // 3. Supprimer le formulaire
                    _dbContext.form.Remove(form);

                    // 4. Sauvegarder les modifications
                    await _dbContext.SaveChangesAsync();

                    // 5. Commit de la transaction
                    await transaction.CommitAsync();

                    return Ok(new { message = "Formulaire et Header_form supprimés avec succès" });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { message = "Erreur lors de la suppression", error = ex.Message });
                }
            }

        }
    }