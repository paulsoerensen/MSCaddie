namespace MSCaddie.Repository.Models;

public class TourModel
{
    public int TourId { get; set; }
    public DateTime TourDate { get; set; }
    public string Description { get; set; }
    public DateTime? LastRegistrationDate { get; set; }
    public bool? OpenForSignUp { get; set; }
    public int? MaxNoOfMembers { get; set; }
    public int UrlDescription { get; set; }
    public int NoOfMembers { get; set; }
    public int? MatchId { get; set; }
    public int SponsorLogoId { get; set; }
    public string UrlRegistration { get; set; }
}
