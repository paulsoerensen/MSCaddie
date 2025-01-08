namespace MSCaddie.Shared.Dtos
{ 

    public class LeagueTeam
	{
        public int LeagueId { get; set; }

        public string TeamLeagueName { get; set; }

        public bool IsSingle { get { return LeagueId == 1 || LeagueId == 2; } }

        public int LeagueTeamId { get; set; }

        public int Season { get; set; }

        public string TeamName { get; set; }

        public int VgcNo { get; set; }

        public int? VgcNoPartner { get; set; }
    }
}
