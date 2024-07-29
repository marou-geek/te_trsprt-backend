using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TE_trsprt_remake.Services
{
    public class ApprovalService : IApprovalService
    {
        private readonly AppDbContext _context;

        public ApprovalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Approval>>> GetApprovals()
        {
            return await _context.Approvals.ToListAsync();
        }

        public async Task<bool> UpdateApproval(ApprovalDTO approvalDto, long id)
        {
            var existingApproval = await _context.Approvals.FirstOrDefaultAsync(a => a.Id == id);
            if (existingApproval == null)
            {
                return false;
            }

            existingApproval.ApproverId = approvalDto.ApproverId;
            existingApproval.RequestId = approvalDto.RequestId;
            existingApproval.Position = approvalDto.Position;
            existingApproval.Status = approvalDto.Status;
            existingApproval.Comment = approvalDto.Comment;
            existingApproval.CreatedAt = approvalDto.CreatedAt;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteApproval(long id)
        {
            var approval = await _context.Approvals.FirstOrDefaultAsync(a => a.Id == id);
            if (approval == null)
            {
                return false;
            }

            _context.Approvals.Remove(approval);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddApproval(ApprovalDTO approvalDto)
        {
            var approval = new Approval
            {
                ApproverId = approvalDto.ApproverId,
                RequestId = approvalDto.RequestId,
                Position = approvalDto.Position,
                Status = approvalDto.Status,
                Comment = approvalDto.Comment,
                CreatedAt = approvalDto.CreatedAt
            };

            _context.Approvals.Add(approval);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetApprovalStatus(string status, long id)
        {
            var approval = await _context.Approvals.FirstOrDefaultAsync(a => a.Id == id);
            if (approval == null)
            {
                return false;
            }

            approval.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
