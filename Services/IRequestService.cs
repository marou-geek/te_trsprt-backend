using Microsoft.AspNetCore.Mvc;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public interface IRequestService
    {
        Task<ActionResult<IEnumerable<Request>>> GetRequests();
        Task<bool> UpdateRequest(RequestDTO request, long id);
        Task<bool> DeleteRequest(long id);
        Task<bool> AddRequest(RequestDTO request);
        Task<bool> SetRequestStatus(String AccountStatus, long id);
    }
}
