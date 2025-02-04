using System.Globalization;
using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Dtos;

public class MembershipDto
{
    public int MembershipId { get; set; }
    public int VgcNo { get; set; }
    public int Season { get; set; }
    public DateTime LastUpdate { get; set; }

}
