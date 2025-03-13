using System.Globalization;

namespace MSCaddie.Shared.Dtos;

public class TourPlayerDto
{
    public int TourId { get; set; }
    public int? VgcNo { get; set; }
    public bool SignedUp { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
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
    public string? LastUpdateBy { get; set; }
    public DateTime? LastUpdate { get; set; }
}