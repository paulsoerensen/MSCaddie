namespace MSCaddie.Client.Models
{
    public class MatchPlayResultModel
    {
        public int LeagueId { get; set; }
        public string League { get; set; }
        public int PlayRound { get; set; }
        public string TeamName1 { get; set; }
        public string TeamName2 { get; set; }
        public string ResultText { get; set; }
        public int MatchResult { get; set; }
    }
}
