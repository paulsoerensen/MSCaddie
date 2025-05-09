namespace MSCaddie.Shared.Models;

public class NearestPinResultModel
{
    public int NearestPinId { get; set; }

    public int MatchId { get; set; }

    public int VgcNo { get; set; }

    public string Fullname { get; set; }

    public string PinName { get; set; }

    public string CourseName { get; set; }

    public int DistanceInCM { get; set; }
}

