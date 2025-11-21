using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.Domain;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Infrastructure.Repositories;

namespace SimpleBarber.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _repository;

        public CustomerController(CustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
        {
            var client = new Customer()
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                BirthDate = customerDto.BirthDate
            };

            await _repository.CreateAsync(client);
            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateClientDto dto)
        {
            var client = await _repository.GetByIdAsync(id);

            client.Update(
                dto.Name,
                dto.Phone,
                dto.Email,
                dto.BirthDate);

            await _repository.UpdateAsync(client);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _repository.GetAllAsync();
            
            return Ok(clients);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            
            return Ok(client);
        }
    }
}