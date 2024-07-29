using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;
using TE_trsprt_remake.Services;

namespace TE_trsprt_remake.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApprovalController : ControllerBase
    {
        private readonly IApprovalService _approvalService;

        public ApprovalController(IApprovalService approvalService)
        {
            _approvalService = approvalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Approval>>> GetApprovals()
        {
            var approvals = await _approvalService.GetApprovals();
            return Ok(approvals);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApproval(long id)
        {
            bool isDeleted = await _approvalService.DeleteApproval(id);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{status}/{id}")]
        public async Task<IActionResult> SetApprovalStatus(string status, long id)
        {
            bool isStatusChanged = await _approvalService.SetApprovalStatus(status, id);
            if (isStatusChanged)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApproval(long id, ApprovalDTO approvalDto)
        {
            if (approvalDto == null)
            {
                return BadRequest("Approval data is null.");
            }

            bool isUpdated = await _approvalService.UpdateApproval(approvalDto, id);
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
        public async Task<IActionResult> AddApproval(ApprovalDTO approvalDto)
        {
            if (approvalDto == null)
            {
                return BadRequest("Approval data is null.");
            }

            var result = await _approvalService.AddApproval(approvalDto);
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
