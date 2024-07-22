using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public class CarService
    {
        private readonly AppDbContext _context;

        public CarService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<bool> AddCar(CarDTO car)
        {

            var c = new Car
            {
                Brand = car.Brand,
                Type = car.Type,
                LicensePlate = car.LicensePlate,
                PlantId = car.PlantId,
                Condition = car.Condition,
            };

            _context.Cars.Add(c);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCar(long id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(car => car.Id == id);
            if (car == null)
            {
                return false;
            }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateCar(CarDTO car, long id)
        {
            var existingcar = await _context.Cars.FirstOrDefaultAsync(car => car.Id == id);
            if (existingcar == null)
            {
                return false;
            }

            existingcar.Brand = car.Brand;
            existingcar.Type = car.Type;
            existingcar.LicensePlate = car.LicensePlate;
            existingcar.PlantId = car.PlantId;
            existingcar.Condition = car.Condition;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
