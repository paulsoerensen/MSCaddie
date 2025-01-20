namespace MSCaddie.Shared.Models
{
    public class CompetitionUpsert
    {
        public int CompetitionResultId { get; set; }
        public int MatchId { get; set; }
        public int CompetitionId { get; set; }
        public int VgcNo { get; set; }
        public int MembershipId { get; set; }
    }
}
