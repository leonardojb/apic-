using api3teste.Data;
using api3teste.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api3teste.Controllers
{
    [Route("api/v1/car")]
    [ApiController]

    public class CarController : ControllerBase
    {
        private readonly DataContext _context;
        public CarController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("getCar")]
         public async Task<ActionResult<List<Car>>> GetAllCars()
        {
            var cars = await _context.Car.ToListAsync();
            return Ok(cars);
        }

        [HttpGet("getCar/{id}")]
        public async Task<ActionResult> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound($"Não foi encontrado nenhum carro com id {id}");
            }
            return Ok(car);
        }

        [HttpPost("addCar")]
        public async Task<ActionResult> PostCar(Car car)
        {
            _context.Car.Add(car);
            await _context.SaveChangesAsync();
            return Ok(car);
        }

        [HttpPut("changeCar")]
        public async Task<ActionResult> PutCar(Car updatedCar)
        {
            var dbCar = await _context.Car.FindAsync(updatedCar.Id);
            if (dbCar == null)
                return NotFound("Não existe pessoa com o id informado");
            dbCar.Name = updatedCar.Name;
            dbCar.Brand = updatedCar.Brand;
            dbCar.Color = updatedCar.Color;
            dbCar.Price = updatedCar.Price;
            
            await _context.SaveChangesAsync(); 
            return Ok(updatedCar);
        }

        [HttpDelete("deleteCar")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var dbCar = await _context.Car.FindAsync(id);
            if (dbCar == null)
                return NotFound($"Não foi encontrado nenhum carro com id {id}");

            _context.Car.Remove(dbCar);
            await _context.SaveChangesAsync();

            return Ok($"O carro com id {id} foi removido");
        }
    }
}