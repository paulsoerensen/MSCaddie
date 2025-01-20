
using System.Globalization;

namespace MSCaddie.Shared.Models
{
    public class MatchBirdieResult
    {
        public int MatchId { get; set; }
        public int MemberShipId { get; set; }
        public int VgcNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
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
        public int Birdies { get; set; }
        public string BirdieString => (Birdies > 1) ? $"{Fullname} ({Birdies})" : Fullname;
    }
}