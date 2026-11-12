using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Services.Interfaces;
using Project.Domain.Models;

namespace Project.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Manager")] 
public class OperationsController : ControllerBase
{
    private readonly IOperationService _operationService;

    public OperationsController(IOperationService operationService)
    {
        _operationService = operationService;
    }

    
    [HttpGet]
    public async Task<ActionResult<List<Operation>>> GetHistory(
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate)
    {
        try
        {
            var history = await _operationService.GetHistoryAsync(fromDate, toDate);
            return Ok(history);
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