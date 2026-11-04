using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Domain.Models;

namespace Project.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientsController(AppDbContext context)
    {
        _context = context;
    }

    public class CreateClientRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    [HttpPost]
    public async Task<ActionResult<Client>> Create([FromBody] CreateClientRequest request)
    {
        var client = new Client
        {
            Name = request.Name,
            Phone = request.Phone
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return Ok(client);
    }
}