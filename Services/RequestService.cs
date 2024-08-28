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
                var Car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == request.CarId);
                var Sv = await _context.Users.FirstOrDefaultAsync(u => u.Email == requester.SvEmail);
                var subject = "New Request Created";
                var body = $@"
                        Dear {Sv.FullName},<br><br>
                        A new request has been submitted by {requester.FullName} and requires your review and approval. Below are the details of the request:<br><br>
                        <strong>Request Details:</strong><br>
                        <strong>From:</strong> {request.FromDestination}<br>
                        <strong>To:</strong> {request.ToDestination}<br>
                        <strong>From Date:</strong> {request.FromDate:MMMM dd, yyyy}<br>
                        <strong>To Date:</strong> {request.ToDate:MMMM dd, yyyy}<br><br>
                        <strong>Request Created At:</strong> {request.CreatedAt}<br><br>
                        Please log in to the system for more details of the request.<br><br>
                        If you have any questions or need further assistance, please contact the Administration.<br><br>
                        Best regards,<br>
                        TE Connectivity
                        ";
                await _emailService.SendEmailAsync(requester.SvEmail, subject, body);
                var Approval = new Approval
                {
                    ApproverId = Sv.Id,
                    RequestId = request.Id,
                    Position = "SV",
                    Status = "Pending",
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