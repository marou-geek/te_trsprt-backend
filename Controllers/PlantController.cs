using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;
using TE_trsprt_remake.Services;

[ApiController]
[Route("api/[controller]")]
public class PlantController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IPlantService _plantservice;


    public PlantController(AppDbContext context , IPlantService plantservice)
    {
        _context = context;
        _plantservice = plantservice;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Plant>>> GetPlants()
    {
        return await _context.Plants.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddPlant(PlantDTO plant)
    {
        var result = await _plantservice.AddPlant(plant);
        if (!result)
        {
            return BadRequest();
        }

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlant(long id)
    {

        bool isDeleted = await _plantservice.DeletePlant(id);
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

    public async Task<IActionResult> UpdatePlant(long id, [FromBody] PlantDTO plant)
    {
        if (plant == null)
        {
            return BadRequest("Departement data is null.");
        }

        bool isUpdated = await _plantservice.UpdatePlant(plant, id);
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
    