using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;
using TE_trsprt_remake.Services;

namespace TE_trsprt_remake.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardPostController : ControllerBase
    {
        private readonly IGuardPostService _guardPostService;

        public GuardPostController(IGuardPostService guardPostService)
        {
            _guardPostService = guardPostService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuardPost>>> GetGuardPosts()
        {
            var result = await _guardPostService.GetGuardPosts();
            return Ok(result);
        }

        
        [HttpGet("request/{requestId}")]
        public async Task<ActionResult<IEnumerable<GuardPost>>> GetGuardPostsByRequestId(long requestId)
        {
            var result = await _guardPostService.GetGuardPostByRequestId(requestId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        
        [HttpPost]
        public async Task<ActionResult> AddGuardPost([FromBody] GuardPost guardPost)
        {
            var success = await _guardPostService.AddGuardPost(guardPost);
            if (!success)
            {
                return BadRequest("Could not create Guard Post");
            }
            return CreatedAtAction(nameof(GetGuardPostsByRequestId), new { requestId = guardPost.RequestId }, guardPost);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGuardPost(long id, [FromBody] GuardPostDTO guardPostDto)
        {
            var success = await _guardPostService.UpdateGuardPost(guardPostDto, id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGuardPost(long id)
        {
            var success = await _guardPostService.DeleteGuardPost(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
