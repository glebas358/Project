using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Models;
using Project.Api.Services.Interfaces;
using Project.Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace Project.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IAuthService _authService;

    
    public EmployeesController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<Employee>> Login([FromBody] LoginRequest request)
    {
        
        var employee = await _authService.LoginAsync(request.Login, request.Password);

        if (employee == null)
        {
            return Unauthorized("Неверный логин или пароль");
        }

        return Ok(employee);
    }


}