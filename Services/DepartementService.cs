using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public class DepartementService : IDepartementService
    {
        private readonly AppDbContext _context;

        public DepartementService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Departement>>> GetDepartements()
        {
            return await _context.Departements.ToListAsync();
        }

        public async Task<bool> AddDepartement(DepartementDTO departement)
        {

            var dep = new Departement
            {
                Name = departement.Name,
            };

            _context.Departements.Add(dep);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDepartement(long id)
        {
            var dep = await _context.Departements.FirstOrDefaultAsync(dep => dep.Id == id);
            if (dep == null)
            {
                return false;
            }
            _context.Departements.Remove(dep);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateDepartement(DepartementDTO departement, long id)
        {
            var existingdep = await _context.Departements.FirstOrDefaultAsync(dep => dep.Id == id);
            if (existingdep == null)
            {
                return false;
            }

            existingdep.Name = departement.Name;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
