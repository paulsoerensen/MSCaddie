using System.ComponentModel.DataAnnotations;

namespace MSCaddie.Shared.Dtos
{
     public class MatchViewModel
    {
        public MatchViewModel()
        {
        }

        public int MatchId { get; set; }
        public int ClubId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM, yyyy}")]
        public DateTime MatchDate { get; set; }

        public int MatchformId { get; set; }

        public string Matchform { get; set; }

        public string? MatchText { get; set; }
        public string? CourseText => string.Equals(CourseName, ClubName) ? ClubName : $"{ClubName} ({CourseName})";

        public bool IsHallington { get; set; }

        public bool IsStrokePlay { get; set; }

        public string? Sponsor { get; set; } = "Mens Section";

        public int SponsorLogoId { get; set; }

        public int CourseDetailId { get; set; }

        public string? CourseName { get; set; }

        public int Par { get; set; }

        public string? Tee { get; set; }

        public decimal CourseRating { get; set; }

        public int Slope { get; set; }

        public string? Remarks { get; set; }

        public bool Official { get; set; }

        public bool Shootout { get; set; }
        public bool NotShootout => !Shootout;

        public string? ClubName { get; set; }
    }
}
