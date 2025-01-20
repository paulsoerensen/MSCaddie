namespace MSCaddie.Shared.Models
{
    public class LeagueMatch
    {
        public int LeagueId { get; set; }

        public string LeagueName { get; set; }

        public int Serie { get; set; }

        public int Cup { get; set; }

        public int Playround { get; set; }

        public int Season { get; set; }

        public int LeagueMatchId { get; set; }

        public int MatchResult { get; set; }

        public string ResultText { get; set; }

        public string TeamName1 { get; set; }

        public int LeagueTeamId1 { get; set; }

        public string TeamName2 { get; set; }

        public int LeagueTeamId2 { get; set; }
    }
}
