
namespace MSCaddie.Shared.Dtos;

public class MatchplayTeamDto
{
    public int TeamId { get; set; }
    public char League { get; set; }
    public int Season { get; set; }
    public string TeamName { get; set; } = string.Empty;  // Assuming TeamName is never null.
}
