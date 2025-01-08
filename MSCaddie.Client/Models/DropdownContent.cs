using MSCaddie.Shared.Dtos;

namespace MSCaddie.Server.Models;

public class DropdownContent
{
    public DropdownContent(List<ListItem> municipalities, List<ListItem> balanceAreas, List<ListItem> reservations, List<ListItem> netCompanies, List<ListItem> locations, List<ListItem> facilityCategories)
    {
        Municipalities = municipalities;
        BalanceAreas = balanceAreas;
        Reservations = reservations;
        NetCompanies = netCompanies;
        Locations = locations;
        FacilityCategories = facilityCategories;
    }

    public List<ListItem> Municipalities { get; set; }
    public List<ListItem> BalanceAreas { get; set; }
    public List<ListItem> Reservations { get; set; }
    public List<ListItem> NetCompanies { get; set; }
    public List<ListItem> Locations { get; set; }
    public List<ListItem> FacilityCategories { get; set; }
}