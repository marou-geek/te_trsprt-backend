using Microsoft.AspNetCore.Mvc;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public interface IDepartementService
    {
        Task<ActionResult<IEnumerable<Departement>>> GetDepartements();
        Task<bool> UpdateDepartement(DepartementDTO departement, long id);
        Task<bool> DeleteDepartement(long id);
        Task<bool> AddDepartement(DepartementDTO departement);
    }
}
