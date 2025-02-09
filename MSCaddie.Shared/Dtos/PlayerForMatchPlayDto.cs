
namespace MSCaddie.Shared.Dtos;

public class PlayerForMatchPlayDto
{
    public int LeagueTeamId { get; set; }
    public int VgcNo { get; set; }
    public string Firstname { get; set; } 
    public string Lastname { get; set; } 
    public int? LeagueId { get; set; } 
    public int? Season { get; set; } 
    public string? TeamName { get; set; } 
    public int? VgcNoPartner { get; set; }
    public string? Firstname2 { get; set; }
    public string? Lastname2 { get; set; } 
}
