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



    // POST: api/packages/register
    [HttpPost("register")]
    public async Task<ActionResult<Package>> Register([FromBody] RegisterPackageRequest request)
    {
        try
        {
            var package = await _packageService.RegisterPackageAsync(request.Number, request.Zone, request.Cell);
            return Ok(package);
        }
        catch (Exception ex)
        {
            
            return BadRequest(new { message = ex.Message });
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
        catch (Exception ex)
        {

            return BadRequest(new { message = ex.Message });
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
        catch (Exception ex)
        {
            
            return NotFound(new { message = ex.Message });
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
        catch (Exception ex)
        {

            return NotFound(new { message = ex.Message });
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
        catch (Exception ex)
        {

            return NotFound(new { message = ex.Message });
        }
    }


}