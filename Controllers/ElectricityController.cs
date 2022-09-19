using ElectricityAPI1.Interfaces;
using ElectricityAPI1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectricityAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityController : Controller
    {
        private readonly IElectricityRepository _electricityRepository;

        public ElectricityController(IElectricityRepository electricityRepository)
        {
            _electricityRepository = electricityRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Electricity>))]
        public IActionResult GetElectricities()
        {
            var electricities = _electricityRepository.GetElectricities();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(electricities);
        }
    }
}
