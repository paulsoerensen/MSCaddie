using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Models;
using MSCaddie.Shared.Services;
using Radzen;

namespace MSCaddie.Components.Matchplay;

public partial class MatchplayGameBase : ComponentBase
{
    [Inject] public IMatchplayService matchPlaySvc { get; set; } = default!;

    public IEnumerable<MatchTeamModel>? teams;

    public int FromRound { get; set; } = 0;
    public int ToRound { get; set; } = 1;
    public IList<int> values = new int[] { 1, 2 };
    public TabPosition tabPosition = TabPosition.Top;

    protected override async Task OnInitializedAsync()
    {
        teams = await matchPlaySvc.GetTeamsForMatchplay(1);
        await base.OnInitializedAsync();
    }

    public async Task OnChange(int index)
    {
        if (0 < index && index < 4)
        {
            teams = await matchPlaySvc.GetTeamsForMatchplay(index);
        }
        else
        {
            teams = null;
        }
    }

    // Filter items by zone value
    //public Func<MatchTeamModel, RadzenDropZone<MatchTeamModel>, bool> ItemSelector = (item, zone) => item.PlayRound == (int)zone.Value && item.Status != Status.Deleted;

    //Func<RadzenDropZoneItemEventArgs<MatchTeamModel>, bool> CanDrop = request =>
    //{
    //    // Allow item drop only in the same zone, in "Deleted" zone or in the next/previous zone.
    //    return request.FromZone.Value. <  request.ToZone || (Status)request.ToZone.Value == Status.Deleted ||
    //        Math.Abs((int)request.Item.Status - (int)request.ToZone.Value) == 1;
    //};

    public void OnItemRender(RadzenDropZoneItemRenderEventArgs<MatchTeamModel> args)
    {
        // Customize item appearance
        if (args.Item.PlayRound == 0)
        {
            args.Attributes["draggable"] = "false";
            args.Attributes["style"] = "cursor:not-allowed";
            args.Attributes["class"] = "rz-card rz-variant-flat rz-background-color-primary-lighter rz-color-on-primary-lighter";
        }
        else
        {
            args.Attributes["class"] = "rz-card rz-variant-filled rz-background-color-primary-light rz-color-on-primary-light";
        }

        // Do not render item if deleted
//        args.Visible = args.Item.Status != Status.Deleted;
    }

    //public void OnDrop(RadzenDropZoneItemEventArgs<MatchTeamModel> args)
    //{
    //    if (args.FromZone != args.ToZone)
    //    {
    //        // update item zone
    //        args.Item.Status = (Status)args.ToZone.Value;
    //    }

    //    if (args.ToItem != null && args.ToItem != args.Item)
    //    {
    //        // reorder items in same zone or place the item at specific index in new zone
    //        data.Remove(args.Item);
    //        data.Insert(data.IndexOf(args.ToItem), args.Item);
    //    }
    //}
}
