using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public class RequestService : IRequestService
    {

        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public RequestService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<bool> DeleteRequest(long id)
        {
            var req = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
            if (req == null)
            {
                return false;
            }
            _context.Requests.Remove(req);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<bool> SetRequestStatus(string RequestStatus, long id)
        {
            var req = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
            if (req == null)
            {
                return false;
            }
            req.Status = RequestStatus;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRequest(RequestDTO request, long id)
        {
            var existingRequest = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRequest == null)
            {
                return false;
            }

            existingRequest.RequesterId = request.RequesterId;
            existingRequest.CarId = request.CarId;
            existingRequest.FromDate = request.FromDate;
            existingRequest.ToDate = request.ToDate;
            existingRequest.Raison = request.Raison;
            existingRequest.Status = request.Status;
            existingRequest.FromDestination = request.FromDestination;
            existingRequest.ToDestination = request.ToDestination;
            existingRequest.CreatedAt = request.CreatedAt;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRequest(RequestDTO requestDto)
        {


            var request = new Request
            {
                RequesterId = requestDto.RequesterId,
                CarId = requestDto.CarId,
                FromDate = requestDto.FromDate,
                ToDate = requestDto.ToDate,
                Raison = requestDto.Raison,
                Status = "Pending",
                FromDestination = requestDto.FromDestination,
                ToDestination = requestDto.ToDestination,
                CreatedAt = requestDto.CreatedAt
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var requester = await _context.Users.FindAsync(request.RequesterId);
            if (requester != null && !string.IsNullOrEmpty(requester.SvEmail))
            {
                var subject = "New Request Created";
                var body = $"Dear {requester.FullName},<br><br>Your request has been created successfully.<br>Details:<br>From: {request.FromDestination}<br>To: {request.ToDestination}<br>From Date: {request.FromDate}<br>To Date: {request.ToDate}<br><br>Best regards,<br>Your Team";
                await _emailService.SendEmailAsync(requester.SvEmail, subject, body);
                var Sv = await _context.Users.FirstOrDefaultAsync(u => u.Email == requester.SvEmail);
                var Approval = new Approval
                {
                    ApproverId = Sv.Id,
                    RequestId = request.Id,
                    Position = "SV",
                    Status = "SVPending",
                    Comment = "",
                    CreatedAt = request.CreatedAt,
                };
                _context.Approvals.Add(Approval);
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}