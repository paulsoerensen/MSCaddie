using AutoMapper;
using MSCaddie.Client.Pages;
using MSCaddie.Shared.Containers;
using Microsoft.AspNetCore.Components;

namespace MSCaddie.Client.Shared.MatchResults
{
    public class PlayerResultBase : MatchResultPage
    {
        [Inject] public ILogger<PlayerResultBase> logger { get; set; } = default!;

        protected bool _showModal = false;
        //protected Validations _validations { get; set; }


        protected override async Task OnInitializedAsync()
        {
            logger.LogInformation($"OnInitializedAsync player");
            var lst = await service.MatchResultForRegistration(Match.MatchId);

            if (Match.IsHallington)
                results = lst?.OrderBy(x => x.Hallington).ToList();
            else if (Match.IsStrokePlay)
                results = lst?.OrderBy(x => x.Brutto).ToList();
            else
                results = lst?.OrderBy(x => x.Points).ToList();

            logger.LogInformation($"OnInitializedAsync player done");
            await base.OnInitializedAsync();
        }

        protected async Task HandleValidSubmit()
        {
            logger.LogInformation($"HandleValidSubmit {result}");
            bool b = await service.UpsertResultMatch(result);
            if (!b)
            {
                //await NotificationService.Warning("Der var ikke helt iorden. Prøv igen", "Ups");
            }
            else
            {
                _searchTerm = string.Empty;
                result = null;

                //await NotificationService.Success("Resultat gemt.", "Succes");
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }
    }
}
