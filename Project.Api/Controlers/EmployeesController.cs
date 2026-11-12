using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.Models;
using Project.Api.Services;
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

    private readonly ITokenService _tokenService;
    public class LoginResponse
    {
        public Employee Employee { get; set; }
        public string Token { get; set; }
    }



    public EmployeesController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<Employee>> Login([FromBody] LoginRequest request)
    {

        try
        {

            var employee = await _authService.LoginAsync(request.Login, request.Password);


            var token = _tokenService.GenerateToken(employee);

            return Ok(new LoginResponse { Employee = employee, Token = token });
        }
        catch (AppException ex)
        {

            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
        catch (Exception ex)
        {

            return StatusCode(500, new { message = "Внутренняя ошибка сервера", details = ex.Message });
        }


    }


}