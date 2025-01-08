namespace MSCaddie.Server.Models;

public class BreadCrumbItem
{
    public BreadCrumbItem(string url, string name) 
    {
        Url = url;
        Name = name;
    }

    public string Url { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}