using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TE_trsprt_remake.Services
{
    public class ApprovalService : IApprovalService
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        public ApprovalService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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

            if (status == "SvApproved")
            {
                await HandleSvApproved(approval.RequestId);
            }
            else if (status == "SvRejected")
            {
                await HandleSvRejected(approval.RequestId);
            }
            else if (status == "HrApproved")
            {
                await HandleHrApproved(approval.RequestId);
            }
            else if (status == "HrRejected")
            {
                await HandleHrRejected(approval.RequestId);
            }

            return true;
        }

        public async Task HandleSvApproved(int requestId)
        {
            var approvals = await _context.Approvals.Where(a => a.RequestId == requestId).ToListAsync();
            var svApproval = approvals.FirstOrDefault(a => a.Position == "SV");

            if (svApproval != null)
            {
                svApproval.Status = "SvApproved";
                await _context.SaveChangesAsync();
            }

            await CreateHrApproval(requestId);
        }

        public async Task HandleSvRejected(int requestId)
        {
            var approvals = await _context.Approvals.Where(a => a.RequestId == requestId).ToListAsync();
            var svApproval = approvals.FirstOrDefault(a => a.Position == "SV");

            if (svApproval != null)
            {
                svApproval.Status = "SvRejected";
                await _context.SaveChangesAsync();
            }

            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);
            if (request != null)
            {
                request.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
        }

        public async Task HandleHrApproved(int requestId)
        {
            var hrApproval = await _context.Approvals
                .Where(a => a.RequestId == requestId && a.Position == "HR")
                .FirstOrDefaultAsync();

            if (hrApproval != null)
            {
                hrApproval.Status = "HrApproved";
                await _context.SaveChangesAsync();
            }

            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);
            if (request != null)
            {
                request.Status = "Accepted";
                await _context.SaveChangesAsync();
            }
        }

        public async Task HandleHrRejected(int requestId)
        {
            var approvals = await _context.Approvals.Where(a => a.RequestId == requestId).ToListAsync();
            var hrApproval = approvals.FirstOrDefault(a => a.Position == "HR");

            if (hrApproval != null)
            {
                hrApproval.Status = "HrRejected";
                await _context.SaveChangesAsync();
            }

            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);
            if (request != null)
            {
                request.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateHrApproval(int requestId)
        {
            var request = await _context.Requests
                .Include(r => r.User)
                .ThenInclude(u => u.UserPlants)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
            {
                return;
            }

            var hrUser = await _context.Users
                .Where(u => u.UserPlants.Any(up => up.PlantId == request.User.UserPlants.First().PlantId) && u.Role == "HR")
                .FirstOrDefaultAsync();

            if (hrUser != null)
            {
                var hrApproval = new Approval
                {
                    ApproverId = hrUser.Id,
                    RequestId = request.Id,
                    Position = "HR",
                    Status = "HrPending",
                    Comment = "",
                    CreatedAt = DateTime.UtcNow,
                };

                _context.Approvals.Add(hrApproval);
                await _context.SaveChangesAsync();

                
                var subject = "New Request for Approval";
                var body = $"Dear {hrUser.FullName},<br><br>You have a new request to approve.<br>Details:<br>From: {request.FromDestination}<br>To: {request.ToDestination}<br>From Date: {request.FromDate}<br>To Date: {request.ToDate}<br><br>Best regards,<br>Your Team";
                await _emailService.SendEmailAsync(hrUser.Email, subject, body);
            }
        }

        
    }
}
