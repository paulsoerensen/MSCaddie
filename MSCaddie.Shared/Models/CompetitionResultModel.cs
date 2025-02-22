using System.Globalization;
namespace MSCaddie.Shared.Models;

public class CompetitionResultModel
{
    public int CompetitionResultId { get; set; }
    public int MatchId { get; set; }
    public int CompetitionId { get; set; }
    public int VgcNo { get; set; }
    public string Fullname { get; set; }
    public string CompetitionText { get; set; }
}
