namespace MSCaddie.Shared.Models;

public class TourPlayerModel
{
    public int TourId { get; set; }
    public int? VgcNo { get; set; }
    public bool SignedUp { get; set; }
    public string Tilmeldt => SignedUp ? "ja" : "nej";
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string Fullname { get; set; }
    public string? LastUpdateBy { get; set; }
    public DateTime? LastUpdate { get; set; }
    public string LastUpdateDisplay => $"{LastUpdate:dd MMM, yyyy}";

}