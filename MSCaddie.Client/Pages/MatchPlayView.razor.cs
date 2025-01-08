using MSCaddie.Shared.Dtos;
using MSCaddie.Client.Services;
using Microsoft.AspNetCore.Components;
using MSCaddie.Shared.Services;
using MSCaddie.Client.Components;

namespace MSCaddie.Client.Pages
{

    public class MatchPlayViewBase : ComponentBase
    {
        [Inject] public ILogger<MatchPlayViewBase> _logger { get; set; } = default!;
        [Inject] public IMatchPlayService matchSvc { get; set; } = default!;
        //[Inject] public IModalService? ModalService { get; set; }

        private string formularyMessage = "";

        protected List<LeagueMatch>? allMatches;
        protected override async Task OnInitializedAsync()
        {
            var res = await matchSvc.GetMatchplays();
            allMatches = res?.ToList();
            ////matchForms = (await matchSvc.GetMatchforms()).ToList();
            //Message = $"* {allSeason}, {DateTime.Now.Second}";
        }

        protected async Task EditObject(int id)
        {
            // Call your method to handle the edit action with the given object ID
            // For example, you can navigate to a different page passing the ID as a parameter
            // or open a modal dialog for editing the object
            LeagueMatch? m = allMatches?.Where(x => x.LeagueMatchId == id).FirstOrDefault();

            var parameters = new Dictionary<string, object>();
            parameters.Add("Match", m);

            //modal.Show<MatchPlayResult>(title: "Hulspils resultat", parameters: parameters);

            Console.WriteLine($"Edit object with ID: {id}");
        }

        protected async Task ShowMatchPlayResult()
        {

        }
    }
}
