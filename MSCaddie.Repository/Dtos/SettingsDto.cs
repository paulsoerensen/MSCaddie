
namespace MSCaddie.Repository.Dtos;

public class SettingsDto
{
    public int SettingsId { get; set; }
    public int? Season { get; set; }
    public DateTime? SeasonStart { get; set; }
    public DateTime? SeasonEnd { get; set; }
    public DateTime? SeasonStartDamstahl { get; set; }
    public DateTime? SeasonEndDamstahl { get; set; }
    public string? MensSectionLogoUrl { get; set; }
    public string? MensSectionShort { get; set; }  
    public string? RyderCupSponsor { get; set; }  
    public string? GBAccount { get; set; }  
    public string? GBUsername { get; set; } 
    public string? GBPassword { get; set; } 
    public string? GBGuid { get; set; }  
    public int? NoOfRoundsRankings { get; set; }  
    public int? MaxHcpA { get; set; }  
    public int? MaxHcpB { get; set; }
    public string? Database { get; set; }
    public string? DatabaseServer { get; set; }
}
