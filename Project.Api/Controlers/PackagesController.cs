using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.Data;
using Project.Api.DTOs;
using Project.Api.Services.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Controllers;

[ApiController]

[Route("api/[controller]")] 
public class PackagesController : ControllerBase
{

    private readonly IPackageService _packageService;


    public PackagesController(IPackageService packageService)
    {
        _packageService = packageService;
    }



    
    [HttpPost("register")]
    public async Task<ActionResult<Package>> Register([FromBody] RegisterPackageRequest request)
    {
        try
        {
            var package = await _packageService.RegisterPackageAsync(request.Number, request.Zone, request.Cell);
            return Ok(package);
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

    [HttpPost("searchByNumber/{number}")]
    public async Task<ActionResult<Package>> SearchByNumber([FromBody] SearchPackageRequest request)
    {
        try
        {
            var package = await _packageService.SearchPackageByNumberAsync(request.Number);
            return Ok(package);
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

    [HttpPost("searchByPhone/{phone}")]
    public async Task<ActionResult<List<Package>>> GetByPhone([FromBody] SearchPackageRequest request)
    {
        try
        {
           
            var packages = await _packageService.SearchPackagesByPhone(request.Phone);

            
            return Ok(packages);
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

    [HttpPost("searchByName/{name}")]
    public async Task<ActionResult<List<Package>>> GetByName([FromBody] SearchPackageRequest request)
    {
        try
        {

            var packages = await _packageService.SearchPackagesByName(request.Name);


            return Ok(packages);
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

    [HttpPost("extradition")]
    public async Task<ActionResult<Package>> ExtraditiomPackage([FromBody] ExtraditionPackageRequest request)
    {
        try
        {

            var package = await _packageService.ExtraditionPackageAsync(request.Number);


            return Ok(package);
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

    [HttpPost("return")]

    public async Task<IActionResult> ReturnPackage([FromBody] ReturnPackageRequest request)
    {
        try
        {
            var employeeIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(employeeIdStr)) return Unauthorized();

            long employeeId = long.Parse(employeeIdStr);

            
            var success = await _packageService.ReturnPackageAsync(request.Number, request.ReturnReasonId, employeeId);

            if (success)
                return Ok(new { message = "Возврат успешно оформлен" });
            else
                return BadRequest(new { message = "Не удалось оформить возврат" });
        }
        catch (AppException ex)
        {
            
            return StatusCode(ex.StatusCode, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, new { message = "Ошибка сервера", details = ex.Message });
        }
    }

}