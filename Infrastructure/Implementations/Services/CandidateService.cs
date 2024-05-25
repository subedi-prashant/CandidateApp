using Application.DTOs.Candidate;
using Application.Interfaces.GenericRepository;
using Application.Interfaces.Services;
using Entities.Models;

namespace Infrastructure.Implementations.Services
{
    public class CandidateService(IGenericRepository _genericRepository) : ICandidateService
    {
        public async Task<bool> AddUpdateCandidate(CandidateInfoDTO candidate)
        {
            var candidateInfo = await _genericRepository.GetFirstOrDefaultAsync<CandidateInfo>(x => x.Email == candidate.Email);

            if (candidateInfo == null)
            {
                var newCandidate = new CandidateInfo()
                {
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    PhoneNumber = candidate.PhoneNumber,
                    Email = candidate.Email,
                    TimeIntervalToCall = candidate.TimeIntervalToCall,
                    LinkedInProfile = candidate.LinkedInProfile,
                    GitHubProfile = candidate.GitHubProfile,
                    Comment = candidate.Comment
                };

                await _genericRepository.InsertAsync(newCandidate);
            }
            else
            {
                candidateInfo.FirstName = candidate.FirstName;
                candidateInfo.LastName = candidate.LastName;
                candidateInfo.PhoneNumber = candidate.PhoneNumber;
                candidateInfo.Email = candidate.Email;
                candidateInfo.TimeIntervalToCall = candidate.TimeIntervalToCall;
                candidateInfo.LinkedInProfile = candidate.LinkedInProfile;
                candidateInfo.GitHubProfile = candidate.GitHubProfile;
                candidateInfo.Comment = candidate.Comment;
                candidateInfo.LastModifiedAt = DateTime.Now;

                await _genericRepository.UpdateAsync(candidateInfo);
            }
            return true;
        }
    }
}
