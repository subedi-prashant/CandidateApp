using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Candidate
{
    public class CandidateInfoDTO
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public string? TimeIntervalToCall { get; set; }

        public string? LinkedInProfile { get; set; }

        public string? GitHubProfile { get; set; }

        [Required]
        public required string Comment { get; set; }
    }
}
