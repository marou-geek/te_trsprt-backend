using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TE_trsprt_remake.Data;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public class GuardPostService : IGuardPostService
    {
        private readonly AppDbContext _context;

        public GuardPostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<GuardPost>>> GetGuardPosts()
        {
            return await _context.GuardPosts.ToListAsync();
        }

        public async Task<IEnumerable<GuardPost>> GetGuardPostByRequestId(long reqid)
        {
            return await _context.GuardPosts
                                 .Where(gp => gp.RequestId == reqid)
                                 .ToListAsync();
        }

        public async Task<bool> AddGuardPost(GuardPostDTO guardPost)
        {

            var newgardPost = new GuardPost
            {
                RequestId = guardPost.RequestId,
                PredKms = guardPost.PredKms,
                RealKms = guardPost.RealKms,
                NbrPersons = guardPost.NbrPersons,
                Company = guardPost.Company,
                FuelLevel = guardPost.FuelLevel,
                RegCard = guardPost.RegCard,
                Insurance = guardPost.Insurance,
                CarHealthCert = guardPost.CarHealthCert,
                Vignette = guardPost.Vignette,
                FuelCard = guardPost.FuelCard,
                Accessories = guardPost.Accessories,
                ExCondition = guardPost.ExCondition,
                IntCondition = guardPost.IntCondition,
                MechCondition = guardPost.MechCondition,
                Type = guardPost.Type,
                CheckDate = guardPost.CheckDate,
                CreatedAt = guardPost.CreatedAt,
            };

            _context.GuardPosts.Add(newgardPost);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateGuardPost(GuardPostDTO guardPost, long id)
        {
            var existingGuardPost = await _context.GuardPosts.FindAsync(id);

            if (existingGuardPost == null)
            {
                return false;
            }

            existingGuardPost.PredKms = guardPost.PredKms;
            existingGuardPost.RealKms = guardPost.RealKms;
            existingGuardPost.NbrPersons = guardPost.NbrPersons;
            existingGuardPost.Company = guardPost.Company;
            existingGuardPost.FuelLevel = guardPost.FuelLevel;
            existingGuardPost.RegCard = guardPost.RegCard;
            existingGuardPost.Insurance = guardPost.Insurance;
            existingGuardPost.CarHealthCert = guardPost.CarHealthCert;
            existingGuardPost.Vignette = guardPost.Vignette;
            existingGuardPost.FuelCard = guardPost.FuelCard;
            existingGuardPost.Accessories = guardPost.Accessories;
            existingGuardPost.ExCondition = guardPost.ExCondition;
            existingGuardPost.IntCondition = guardPost.IntCondition;
            existingGuardPost.MechCondition = guardPost.MechCondition;
            existingGuardPost.Type = guardPost.Type;
            existingGuardPost.CreatedAt = guardPost.CreatedAt;
            existingGuardPost.CheckDate = guardPost.CheckDate;

            _context.GuardPosts.Update(existingGuardPost);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGuardPost(long id)
        {
            var guardPost = await _context.GuardPosts.FindAsync(id);

            if (guardPost == null)
            {
                return false;
            }

            _context.GuardPosts.Remove(guardPost);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
