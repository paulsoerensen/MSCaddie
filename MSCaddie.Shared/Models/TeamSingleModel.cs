namespace MSCaddie.Shared.Models;

public class TeamSingleModel
{
    public int VgcNo { get; set; }
    public int Season { get; set; }
    public string? TeamName { get; set; }
    public int? TeamSingleId { get; set; }
    public char? League { get; set; }
    public int? LeagueInt { get; set; }
    //public char? LeagueDisplay => LeagueInt switch
    //{
    //    1 => 'A',
    //    2 => 'B',
    //    3 => 'X'
    //};

}
