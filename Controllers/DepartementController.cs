using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartementsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartementsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departement>>> GetPlants()
        {
            return await _context.Departements.ToListAsync();
        }
    }
}
