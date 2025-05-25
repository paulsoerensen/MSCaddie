namespace MSCaddie.Shared.Models;

public class NearestPinResultModel
{
    public int NearestPinId { get; set; }

    public int MatchId { get; set; } = 0;

    public int VgcNo { get; set; } = 0;

    public string Fullname { get; set; }

    public string PinName { get; set; }

    public string CourseName { get; set; }

    public int DistanceInCM { get; set; } = 0;
}

