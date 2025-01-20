
using System.Globalization;

namespace MSCaddie.Shared.Models;

public class Player
{
    public int PlayerId { get; set; }
    public int VgcNo { get; set; }
    public int MemberShipId { get; set; }
    public bool IsMale { get; set; }
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
    public string Role { get; set; } = Roles.User;
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public bool Sponsor { get; set; }
    public decimal HcpIndex { get; set; }
    public DateTime HcpUpdated { get; set; }
    public string? Phone { get; set; }
    public int NameGroup { get; set; }
    public int Season { get; set; }
    public bool NearestFlag { get; set; }
    public bool Dining { get; set; }
    public bool Authorized => Roles.Admin.Contains(Role, StringComparison.InvariantCultureIgnoreCase);
    public DateTime LastUpdate { get; set; }

}
