using Microsoft.AspNetCore.Mvc;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public interface ICarService
    {
        Task<ActionResult<IEnumerable<Car>>> GetCars();
        Task<bool> UpdateCar(CarDTO car, long id);
        Task<bool> DeleteCar(long id);
        Task<bool> AddCar(CarDTO car);

    }
}
