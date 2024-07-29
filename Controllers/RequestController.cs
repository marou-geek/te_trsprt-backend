using Microsoft.AspNetCore.Mvc;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;
using TE_trsprt_remake.Services;

namespace TE_trsprt_remake.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            var requests = await _requestService.GetRequests();
            return Ok(requests);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(long id)
        {
            bool isDeleted = await _requestService.DeleteRequest(id);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{requestStatus}/{id}")]
        public async Task<IActionResult> SetRequestStatus(string requestStatus, long id)
        {
            bool isRequestStatusChanged = await _requestService.SetRequestStatus(requestStatus, id);
            if (isRequestStatusChanged)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(long id,RequestDTO requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Request data is null.");
            }

            bool isUpdated = await _requestService.UpdateRequest(requestDto, id);
            if (isUpdated)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest( RequestDTO requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Request data is null.");
            }

            var result = await _requestService.AddRequest(requestDto);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
