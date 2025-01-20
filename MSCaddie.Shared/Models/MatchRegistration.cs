
namespace MSCaddie.Shared.Models;

public class MatchRegistration
{
    public int? MatchRegistrationId { get; set; }

    public int MatchId { get; set; }
    public int VgcNo { get; set; }
    public bool NearestPin { get; set; }
    public bool Dining { get; set; }
    public bool Birdies { get; set; }
}