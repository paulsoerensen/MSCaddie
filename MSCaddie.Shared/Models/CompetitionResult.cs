using System.Globalization;

namespace MSCaddie.Shared.Models
{
    public class CompetitionResult
    {
        public int CompetitionResultId { get; set; }
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int CompetitionId { get; set; }
        public int VgcNo { get; set; }
        public int MembershipId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CompetitionText { get; set; }

        public string Fullname
        {
            get
            {
                if (string.IsNullOrEmpty(Firstname))
                    return this?.Lastname;
                if (string.IsNullOrEmpty(Lastname))
                    return this?.Firstname;

                return string.Format(CultureInfo.InstalledUICulture, $"{Firstname?.Trim()} {Lastname?.Trim()}");
            }
            set {; }
        }
    }
}
