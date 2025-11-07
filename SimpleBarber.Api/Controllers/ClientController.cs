using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.Domain;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Infrastructure.Repositories;

namespace SimpleBarber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientRepository _repository;

        public ClientController(ClientRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientDto clientDto)
        {
            var client = new Client()
            {
                Name = clientDto.Name,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                BirthDate = clientDto.BirthDate
            };

            await _repository.CreateAsync(client);
            return Ok(clientDto);
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