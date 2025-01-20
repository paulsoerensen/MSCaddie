using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCaddie.Shared.Dtos
{
    public class CompetitionResultDto
    {
        public int CompetitionResultId { get; set; }
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int CompetitionId { get; set; }
        public int VgcNo { get; set; }
        public int MembershipId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CompetitionText { get; set; }


    }
}
