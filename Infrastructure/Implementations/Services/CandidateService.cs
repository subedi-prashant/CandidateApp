using Application.DTOs.Candidate;
using Application.Interfaces.GenericRepository;
using Application.Interfaces.Services;
using Entities.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Implementations.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);

        public CandidateService(IGenericRepository genericRepository, IMemoryCache cache)
        {
            _genericRepository = genericRepository;
            _cache = cache;
        }
        public async Task<bool> AddUpdateCandidate(CandidateInfoDTO candidate)
        {
            var cacheKey = $"Candidate-{candidate.Email}";

            if (!_cache.TryGetValue(cacheKey, out CandidateInfo? candidateInfo))
            {
                candidateInfo = await _genericRepository.GetFirstOrDefaultAsync<CandidateInfo>(x => x.Email == candidate.Email);
                if (candidateInfo != null)
                {
                    _cache.Set(cacheKey, candidateInfo, _cacheExpiration);
                }
            }

            if (candidateInfo == null)
            {
                var newCandidate = new CandidateInfo()
                {
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    PhoneNumber = candidate.PhoneNumber ?? string.Empty,
                    Email = candidate.Email,
                    TimeIntervalToCall = candidate.TimeIntervalToCall ?? string.Empty,
                    LinkedInProfile = candidate.LinkedInProfile ?? string.Empty,
                    GitHubProfile = candidate.GitHubProfile ?? string.Empty,
                    Comment = candidate.Comment
                };

                await _genericRepository.InsertAsync(newCandidate);
            }
            else
            {
                candidateInfo.FirstName = candidate.FirstName;
                candidateInfo.LastName = candidate.LastName;
                candidateInfo.PhoneNumber = candidate.PhoneNumber ?? string.Empty;
                candidateInfo.Email = candidate.Email;
                candidateInfo.TimeIntervalToCall = candidate.TimeIntervalToCall ?? string.Empty;
                candidateInfo.LinkedInProfile = candidate.LinkedInProfile ?? string.Empty;
                candidateInfo.GitHubProfile = candidate.GitHubProfile ?? string.Empty;
                candidateInfo.Comment = candidate.Comment;
                candidateInfo.LastModifiedAt = DateTime.Now;

                await _genericRepository.UpdateAsync(candidateInfo);
            }
            return true;
        }
    }
}
