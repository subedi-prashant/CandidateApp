using Application.DTOs.Candidate;

namespace Application.Interfaces.Services
{
    public interface ICandidateService
    {
        Task<bool> AddUpdateCandidate(CandidateInfoDTO candidate);
    }
}
