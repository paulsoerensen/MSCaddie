using Microsoft.AspNetCore.Components;
using MSCaddie.Shared;
using MSCaddie.Server.Models;
using MSCaddie.Shared.Extensions;

namespace MSCaddie.Services;

public class BreadCrumbPathBuilder
{
    private readonly NavigationManager _navigationManager;

    public BreadCrumbPathBuilder(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public List<BreadCrumbItem> BuildItemsForCurrentPath(Dictionary<string, string?>? idNameMap = null)
    {
        var currentUrl = "/";
        string currentName = "";
        
        var urlFragments = _navigationManager.ToBaseRelativePath(_navigationManager.Uri).Split('/');
        var items = new List<BreadCrumbItem> {new BreadCrumbItem(url: currentUrl, name: "Home")};

        foreach (var fragment in urlFragments)
        {
            if (fragment is null) continue;

            currentName = fragment.FirstCharToUpper();
            currentUrl += $"{fragment}/";

            if (idNameMap is not null && idNameMap.TryGetValue(fragment, out var mappedName))
            {
                currentName = mappedName ?? "";
            }

            items.Add(new BreadCrumbItem(url: currentUrl, name: currentName));
        }

        items.Last().IsActive = true;

        return items;
    }
}