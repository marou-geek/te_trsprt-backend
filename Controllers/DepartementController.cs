using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;
using TE_trsprt_remake.Services;

namespace TE_trsprt_remake.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartementController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IDepartementService _depservice;


        public DepartementController(AppDbContext context , IDepartementService depservice)
        {
            _context = context;
            _depservice = depservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departement>>> GetDepartements()
        {
            return await _depservice.GetDepartements();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartement(DepartementDTO depDto)
        {
            var result = await _depservice.AddDepartement(depDto);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartement(long id)
        {

            bool isDeleted = await _depservice.DeleteDepartement(id);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateDepartement(long id, [FromBody] DepartementDTO depDto)
        {
            if (depDto == null)
            {
                return BadRequest("Departement data is null.");
            }

            bool isUpdated = await _depservice.UpdateDepartement(depDto, id);
            if (isUpdated)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
