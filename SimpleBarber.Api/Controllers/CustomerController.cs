using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.Domain;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Infrastructure.Repositories;
using SimpleBarber.Api.Services;

namespace SimpleBarber.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _repository;
        private readonly AuthService _userRepository;

        public CustomerController(CustomerRepository repository,
            AuthService userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CustomerDto dto)
        {
            var client = new Customer()
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                BirthDate = dto.BirthDate
            };
            
            var userDto = new UserDto(dto.Email, dto.Email, dto.Email, null);
            var user = await _userRepository.RegisterAsync(userDto);

            if (user is null)
                return BadRequest();
            
            client.UserId = user.Id;
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