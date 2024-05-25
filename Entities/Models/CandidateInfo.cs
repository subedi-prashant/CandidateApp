using Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class CandidateInfo : BaseEntity<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string TimeIntervalToCall { get; set; }

        public string LinkedInProfile { get; set; }

        public string GitHubProfile { get; set; }

        [Required]
        public string Comment { get; set; }

    }
}
