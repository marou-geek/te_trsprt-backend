using Microsoft.AspNetCore.Mvc;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;
using TE_trsprt_remake.Services;


namespace TE_trsprt_remake.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICarService _carservice;


        public CarController(AppDbContext context, ICarService carservice)
        {
            _context = context;
            _carservice = carservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetDepartements()
        {
            return await _carservice.GetCars();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CarDTO carDto)
        {
            var result = await _carservice.AddCar(carDto);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(long id)
        {

            bool isDeleted = await _carservice.DeleteCar(id);
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

        public async Task<IActionResult> UpdateCar(long id, [FromBody] CarDTO car)
        {
            if (car == null)
            {
                return BadRequest("Departement data is null.");
            }

            bool isUpdated = await _carservice.UpdateCar(car, id);
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
