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

        public async Task<bool> SetApprovalStatus(string status, long id, string comment = null)
        {
            var approval = await _context.Approvals.FirstOrDefaultAsync(a => a.Id == id);
            if (approval == null)
            {
                return false;
            }

            approval.Status = status;
            approval.Comment = comment;

            await _context.SaveChangesAsync();

            switch (status)
            {
                case "SvApproved":
                    await HandleSvApproved(approval.RequestId, comment);
                    break;
                case "SvRejected":
                    await HandleSvRejected(approval.RequestId, comment);
                    break;
                case "HrApproved":
                    await HandleHrApproved(approval.RequestId, comment);
                    break;
                case "HrRejected":
                    await HandleHrRejected(approval.RequestId, comment);
                    break;
            }

            return true;
        }

        public async Task HandleSvApproved(int requestId, string comment)
        {
            var approvals = await _context.Approvals.Where(a => a.RequestId == requestId).ToListAsync();
            var svApproval = approvals.FirstOrDefault(a => a.Position == "SV");

            if (svApproval != null)
            {
                svApproval.Status = "Approved";
                svApproval.Comment = comment;
                await _context.SaveChangesAsync();
            }

            await CreateHrApproval(requestId);
        }

        public async Task HandleSvRejected(int requestId, string comment)
        {
            var approvals = await _context.Approvals.Where(a => a.RequestId == requestId).ToListAsync();
            var svApproval = approvals.FirstOrDefault(a => a.Position == "SV");

            if (svApproval != null)
            {
                svApproval.Status = "Rejected";
                svApproval.Comment = comment;
                await _context.SaveChangesAsync();
            }

            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);
            if (request != null)
            {
                request.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
        }

        public async Task HandleHrApproved(int requestId, string comment)
        {
            var hrApproval = await _context.Approvals
                .Where(a => a.RequestId == requestId && a.Position == "HR")
                .FirstOrDefaultAsync();

            if (hrApproval != null)
            {
                hrApproval.Status = "Approved";
                hrApproval.Comment = comment;
                await _context.SaveChangesAsync();
            }

            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);
            if (request != null)
            {
                request.Status = "Accepted";
                await _context.SaveChangesAsync();
            }
        }

        public async Task HandleHrRejected(int requestId, string comment)
        {
            var approvals = await _context.Approvals.Where(a => a.RequestId == requestId).ToListAsync();
            var hrApproval = approvals.FirstOrDefault(a => a.Position == "HR");

            if (hrApproval != null)
            {
                hrApproval.Status = "Rejected";
                hrApproval.Comment = comment;
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
                    Status = "Pending",
                    Comment = "",
                    CreatedAt = DateTime.UtcNow,
                };

                _context.Approvals.Add(hrApproval);
                await _context.SaveChangesAsync();


                var subject = "New Request for Approval";
                var body = $@"
                            Dear {hrUser.FullName},<br><br>
                            You have received a new request that requires your approval. Below are the details of the request:<br><br>
                            <strong>Request Details:</strong><br>
                            <strong>From:</strong> {request.FromDestination}<br>
                            <strong>To:</strong> {request.ToDestination}<br>
                            <strong>From Date:</strong> {request.FromDate:MMMM dd, yyyy}<br>
                            <strong>To Date:</strong> {request.ToDate:MMMM dd, yyyy}<br><br>
                            <strong>Request Created At:</strong> {request.CreatedAt}<br><br>
                            Please log in to the system for more details about the request.<br><br>
                            If you have any questions or need further assistance, please contact the Administrations.<br><br>
                            Best regards,<br>
                            TE Connectivity
                            ";
                await _emailService.SendEmailAsync(hrUser.Email, subject, body);
            }
        }


    }
}