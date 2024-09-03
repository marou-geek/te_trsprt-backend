using Microsoft.AspNetCore.Mvc;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public interface IGuardPostService
    {
        Task<ActionResult<IEnumerable<GuardPost>>> GetGuardPosts();
        Task<bool> UpdateGuardPost(GuardPostDTO guardPost, long id);
        Task<bool> DeleteGuardPost(long id);
        Task<IEnumerable<GuardPost>> GetGuardPostByRequestId(long reqid);
        Task<bool> AddGuardPost(GuardPostDTO guardPost);
    }
}
