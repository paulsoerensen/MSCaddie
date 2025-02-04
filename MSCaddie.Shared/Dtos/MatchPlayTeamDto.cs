
namespace MSCaddie.Shared.Dtos;

public class MatchPlayTeamDto
{
    public int LeagueTeamId { get; set; }
    public int LeagueId { get; set; }
    public int Season { get; set; }
    public string TeamName { get; set; } = string.Empty;  // Assuming TeamName is never null.
    public int VgcNo { get; set; }
    public int? VgcNoPartner { get; set; }  // Nullable because the VgcNoPartner can be NULL in the database.

}
