﻿using Microsoft.AspNetCore.Mvc;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.Services
{
    public interface IApprovalService
    {
        Task<ActionResult<IEnumerable<Approval>>> GetApprovals();
        Task<bool> UpdateApproval(ApprovalDTO approval, long id);
        Task<bool> DeleteApproval(long id);
        Task<bool> AddApproval(ApprovalDTO approvalDto);
        Task<bool> SetApprovalStatus(string status, long id , string comment);
        Task HandleSvRejected(int requestId , string comment);
        Task HandleSvApproved(int requestId , string comment);
        Task HandleHrRejected(int requestId , string comment);
        Task HandleHrApproved(int requestId , string comment);
        Task CreateHrApproval(int requestId);
    }
}
