
namespace MSCaddie.Shared.Dtos;

public class MatchplayTeamDto
{
    public int LeagueTeamId { get; set; }
    public int LeagueId { get; set; }
    public int Season { get; set; }
    public string TeamName { get; set; } = string.Empty;  // Assuming TeamName is never null.
    public string TeamName2 { get; set; } = string.Empty;  // Assuming TeamName is never null.
    public int VgcNo { get; set; }
    public int? VgcNoPartner { get; set; }  // Nullable because the VgcNoPartner can be NULL in the database.
                                            // This is the logic you want to implement for VgcNo1.
    public int VgcNo1
    {
        get
        {
            if (VgcNoPartner == null)
            {
                return VgcNo;  // If VgcNoPartner is null, VgcNo1 will be VgcNo.
            }
            else
            {
                // If VgcNoPartner is not null, set VgcNo1 to the minimum of VgcNo and VgcNoPartner.
                return Math.Min(VgcNo, VgcNoPartner.Value);
            }
        }
    }

    // This property will be VgcNo2, set to the max of VgcNo and VgcNoPartner when VgcNoPartner is not null.
    public int? VgcNo2
    {
        get
        {
            if (VgcNoPartner == null)
            {
                return null;  // If VgcNoPartner is null, VgcNo2 will also be null.
            }
            else
            {
                // If VgcNoPartner is not null, set VgcNo2 to the maximum of VgcNo and VgcNoPartner.
                return Math.Max(VgcNo, VgcNoPartner.Value);
            }
        }
    }


}
