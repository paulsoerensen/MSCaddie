
namespace MSCaddie.Shared.Models;

public class MatchplayGameModel
{
    public int MatchplayGameId { get; set; }
    public int MatchResult { get; set; }
    public string ResultText { get; set; }
    public char League { get; set; }
    public int PlayRound { get; set; }
    public int TeamId1 { get; set; }
    public string TeamName1 { get; set; }
    public int TeamId2 { get; set; }
    public string TeamName2 { get; set; }
    public DateTime LastUpdate { get; set; }
}
