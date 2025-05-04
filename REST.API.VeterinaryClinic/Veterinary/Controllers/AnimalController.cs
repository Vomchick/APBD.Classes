using Microsoft.AspNetCore.Mvc;
using Veterinary.Contracts.Requests.Animal;
using Veterinary.Contracts.Requests.Visit;
using Veterinary.Data;
using Veterinary.Model;
using Veterinary.Models;

namespace Veterinary.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalController : ControllerBase
    {
        private readonly List<Animal> _animals = AnimalsRepository.GetTestAnimals();
        private readonly List<Visit> _visits = VisitsRepository.GetTestVisits();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_animals);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var animal = _animals.FirstOrDefault(x => x.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAnimalRequest request)
        {
            var id = _animals.Max(x => x.Id) + 1;
            var newAnimal = new Animal { 
                Id = id, 
                FurColor = request.FurColor, 
                Name = request.Name, Category = request.Category, 
                Weight = request.Weight 
            };
            _animals.Add(newAnimal);

            return CreatedAtAction(nameof(GetById), new { id }, request);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateAnimalRequest request)
        {
            var animal = _animals.FirstOrDefault(u => u.Id == id);
            if (animal == null)
                return NotFound();

            animal.Name = request.Name;
            animal.Weight = request.Weight;

            return Ok(animal);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _animals.FirstOrDefault(u => u.Id == id);
            if (user is null)
                return NotFound();

            _animals.Remove(user);

            return NoContent();
        }

        [HttpGet("{animalId}/visits")]
        public IActionResult GetAllAnimalVisist(int animalId)
        {
            var visits = _visits.Where(x => x.AnimalId == animalId);
            if (!visits.Any())
            {
                return NotFound();
            }

            return Ok(visits);
        }

        [HttpPost("{animalId}/visits")]
        public IActionResult AddAnimalVisit(int animalId, CreateVisitRequest request)
        {
            if (!_animals.Any(x => x.Id == animalId))
            {
                return NotFound();
            }

            var id = _visits.Max(x => x.Id) + 1;
            var newVisit = new Visit
            {
                Id = id,
                AnimalId = animalId,
                Description = request.Description,
                DateOfVisit = request.DateOfVisit,
                Price = request.Price,
            };
            _visits.Add(newVisit);

            return Ok(newVisit);
        }
    }
}
