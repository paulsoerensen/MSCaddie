using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MSCaddie.Shared.Models
{
    public class ResultMatch
    {
        public ResultMatch()
        {
            Id = 0;
        }
        public int Id { get; set; }

        public int MatchId { get; set; }
        public int MemberShipId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Tee { get; set; }
        public string ClubName { get; set; }
        public string CourseName { get; set; }
        public string Matchform { get; set; }
        public int MatchformId { get; set; }
        public bool Official { get; set; }
        public int Par { get; set; }
        public int VgcNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int HcpGroup { get; set; }
        public int Rank { get; set; }
        public int No { get; set; }
        public int OverallWinner { get; set; }
        public bool Dining { get; set; }
        public int Hcp { get; set; }

        public int MaxA { get; set; }

        public decimal HcpIndex { get; set; }
        public int? Brutto { get; set; }
        public int? Netto { get; set; }
        public int? Hallington { get; set; }
        public int Points { get; set; }
        public int DamstahlPoints { get; set; }
        public int? Puts { get; set; }
        public int Birdies { get; set; }
        public int? ShootOut { get; set; }
    }
}
