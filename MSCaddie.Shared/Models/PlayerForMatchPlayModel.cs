using System.Globalization;

namespace MSCaddie.Shared.Models;

public class PlayerForMatchPlayModel
{
    public int LeagueTeamId { get; set; }
    public int VgcNo { get; set; }
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

    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int LeagueId { get; set; }
    public string League
    {
        get
        {
            return LeagueId switch
            {
                1 => "A",
                2 => "B",
                _ => ""
            };
        }
        set => LeagueId = value switch
        {
            "A" => 1,
            "B" => 2,
            _ => 0
        };
    }

    public int Season { get; set; }
    public string? TeamName { get; set; }
    public int? VgcNoPartner { get; set; }
    public string Firstname2 { get; set; }
    public string Lastname2 { get; set; }
    public string Fullname2
    {
        get
        {
            if (string.IsNullOrEmpty(Firstname2))
                return this?.Lastname2;
            if (string.IsNullOrEmpty(Lastname2))
                return this?.Firstname2;

            return string.Format(CultureInfo.InstalledUICulture, $"{Firstname2?.Trim()} {Lastname2?.Trim()}");
        }
        set {; }
    }
}
