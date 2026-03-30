
using System.Globalization;

namespace MSCaddie.Repository.Models;

public class MatchResultModel
{
    public int? MatchResultId { get; set; }
    public int MemberShipId { get; set; }
    public int MatchId { get; set; }
    public int MatchformId { get; set; }
    public int VgcNo { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string Fullname { get; set; }
    public string WinnerText => Rank == 1 ? "Overall vinder" : "";
    public bool Dining { get; set; }
    public int HcpGroup { get; set; }
    public int Hcp { get; set; }
    public int MaxA { get; set; }
    public int MaxB { get; set; }
    public decimal HcpIndex { get; set; }
    public int? Brutto { get; set; }
    public int? Netto { get; set; }
    public int? Puts { get; set; }
    public int? Points { get; set; }
    public int? DamstahlPoints { get; set; }
    public int? Hallington { get; set; }
    public int? Birdies { get; set; }
    public string? BirdieString { get; set; }
    public bool InBirdies { get; set; }
    public bool InNearestPin { get; set; }
    public int? Shootout { get; set; }
    public int Rank { get; set; }
    public bool Official { get; set; }
    public int No { get; set; }
    public bool IsStrokePlay { get; set; }
    public bool IsHallington { get; set; }
    public int Par { get; set; } = 72;
    public int? ShootOut { get; set; }

    public string ScoreText { get; set; }
    public int? ToPar { get; set; }
}