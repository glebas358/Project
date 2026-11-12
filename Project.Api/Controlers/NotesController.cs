using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.DTOs;
using Project.Api.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Manager")] 
public class NotesController : ControllerBase
{
    private readonly IManagerNoteService _noteService;

    public NotesController(IManagerNoteService noteService)
    {
        _noteService = noteService;
    }



   

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notes = await _noteService.GetAllAsync();
        return Ok(notes);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteRequest request)
    {
        var employeeIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        long employeeId = long.Parse(employeeIdStr!);

        await _noteService.AddAsync(request.Text, employeeId);
        return Ok(new { message = "Заметка добавлена" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] CreateNoteRequest request)
    {
        var employeeIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        long employeeId = long.Parse(employeeIdStr!);

        await _noteService.UpdateAsync(id, request.Text, employeeId);
        return Ok(new { message = "Заметка обновлена" });
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var employeeIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        long employeeId = long.Parse(employeeIdStr!);

        await _noteService.DeleteAsync(id, employeeId);
        return Ok(new { message = "Заметка удалена" });
    }
}