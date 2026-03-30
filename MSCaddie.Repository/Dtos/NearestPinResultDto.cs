using System.Globalization;

namespace MSCaddie.Repository.Dtos;

public class NearestPinResultDto
{
    public int NearestPinId { get; set; }

    public int MatchId { get; set; }

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

    public string PinName { get; set; }

    public string CourseName { get; set; }

    public int DistanceInCM { get; set; }
}

