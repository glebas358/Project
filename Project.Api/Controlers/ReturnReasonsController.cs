using Microsoft.AspNetCore.Mvc;
using Project.Api.Repositories.Interfaces;

namespace Project.Api.Controlers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReturnReasonsController : ControllerBase
    {
        private readonly IReturnReasonRepository _repository;

        public ReturnReasonsController(IReturnReasonRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveReasons()
        {
            var reasons = await _repository.GetAllActiveAsync();
            return Ok(reasons);
        }
    }
}
