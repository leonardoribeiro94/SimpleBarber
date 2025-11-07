using Microsoft.AspNetCore.Mvc;
using SimpleBarber.Api.Domain;
using SimpleBarber.Api.DTO;
using SimpleBarber.Api.Infrastructure.Repositories;

namespace SimpleBarber.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly ServiceRepository _serviceRepository;

    public ServiceController(ServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _serviceRepository.GetServices();

        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _serviceRepository.GetServiceById(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServiceDto dto)
    {
        var service = new Service()
        {
            Name = dto.Name,
            Description = dto.Description,
            EstimateTime = dto.EstimateTime,
            Price = dto.Price
        };

        await _serviceRepository.CreateAsync(service);

        return Created();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ServiceDto dto)
    {
        var service = await _serviceRepository.GetServiceById(id);
        
        service.Update(dto.Name, dto.Description, dto.EstimateTime, dto.Price);
        await _serviceRepository.UpdateAsync(service);

        return Ok();
    }
}