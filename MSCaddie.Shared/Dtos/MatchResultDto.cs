
using System.Globalization;

namespace MSCaddie.Shared.Dtos;

public class MatchResultDto
{
    public int? MatchResultId { get; set; }

    public int MatchId { get; set; }
    public int MemberShipId { get; set; }
    public DateTime MatchDate { get; set; }
    public string Tee { get; set; }
    public string ClubName { get; set; }
    public string CourseName { get; set; }
    public string Matchform { get; set; }
    public int MatchformId { get; set; }
    public bool Official { get; set; }
    public int Par { get; set; }
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
    public int HcpGroup { get; set; }
    public int Rank { get; set; }
    public int No { get; set; }
    public int OverallWinner { get; set; }
    public bool Dining { get; set; }
    public bool InNearestPin { get; set; }
    public bool InBirdies { get; set; }
    public string? BirdieString => (Birdies?? 0) == 0 ? null : ((Birdies > 1) ? $"{Fullname} ({Birdies})" : Fullname);

    public int Hcp { get; set; }
    public int MaxA { get; set; }
    public decimal HcpIndex { get; set; }
    public int? Brutto { get; set; }
    public int? Netto { get; set; }
    public int? Hallington { get; set; }
    public int? Points { get; set; }
    public int? DamstahlPoints { get; set; }
    public int? Puts { get; set; }
    public int? Birdies { get; set; }
    public int? ShootOut { get; set; }
    public string ScoreText
    {
        get
        {
            switch (MatchformId)
            {
                case 1:
                    return $"Brutto/Netto: {Brutto}/{Netto} ({ToPar})";
                case 3:
                    return $"Hallington: {Hallington}";
                default:
                    return $"Points: {Points}";
            }
        }
    }
    public int? ToPar
    {
        get
        {
            if (Brutto != null)
                return Brutto - Par + 72;
            return null;
        }
    }

}
