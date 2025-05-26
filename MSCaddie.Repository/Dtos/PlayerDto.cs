using System.Globalization;
using MSCaddie.Shared.Models;

namespace MSCaddie.Repository.Dtos;

public class PlayerDto
{
    public PlayerDto()
    {
        HcpUpdated = new DateTime(2000, 1, 1);
    }
    public int PlayerId { get; set; }
    public int VgcNo { get; set; }
    public int MemberShipId { get; set; }
    public bool IsMale { get; set; } = true;
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public bool Sponsor { get; set; }
    public decimal HcpIndex { get; set; }
    public DateTime HcpUpdated { get; set; }
    public string? Phone { get; set; }
    public int NameGroup { get; set; }
    //public bool Auth { get; set; }
    public int? Season { get; set; }
    public DateTime LastUpdate { get; set; }

}
