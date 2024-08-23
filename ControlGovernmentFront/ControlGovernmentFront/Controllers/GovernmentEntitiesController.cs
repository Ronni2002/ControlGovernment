using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ControlGovernmentFront.Models;
using ControlGovernmentFront.Services;

namespace ControlGovernmentFront.Controllers
{
    public class GovernmentEntitiesController : Controller
    {
        private readonly IGovernmentEntityService _service;

        public GovernmentEntitiesController(IGovernmentEntityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Json(entities);
        }

        public async Task<IActionResult> Index()
        {
            var entities = await _service.GetAllAsync();
            if (entities == null)
            {
                entities = new List<GovernmentEntity>();
            }
            return View(entities);
        }

        public async Task<IActionResult> Details(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GovernmentEntity entity)
        {
            
                await _service.CreateAsync(entity);
                return Json(entity);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Json(new
            {
                id = entity.Id,
                name = entity.Name,
                type = entity.Type,
                city = entity.City
            });
        }
        public async Task<IActionResult> Edit(int id, GovernmentEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(entity);
                    return Json(new
                    {
                        success = true,
                        message = "Entity updated successfully",
                        entity = new
                        {
                            id = entity.Id,
                            name = entity.Name,
                            type = entity.Type,
                            city = entity.City
                        }
                    });
                }
                catch (Exception ex)
                {
                   
                    return Json(new { success = false, message = ex.Message });
                }
            }

            var errors = ModelState.SelectMany(ms => ms.Value.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToList();

            return Json(new { success = false, errors = errors });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
