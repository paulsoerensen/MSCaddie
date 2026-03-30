using MSCaddie.Repository.Models;
using Microsoft.Extensions.Logging;
using Serilog;
using MSCaddie.Repository.Interfaces;

namespace MSCaddie.Containers;

public class MatchResultContainerList : ContainerListBase<MatchResultModel>
{
    public int MatchId { get; set; }
    private readonly IMatchService _dataService;
    private ILogger<MatchResultContainerList> _logger { get; set; } = default!;

    //private readonly SpinnerService _spinnerService;

    public MatchResultContainerList(IMatchService dataService
        , ILogger<MatchResultContainerList> logger)
    {
        _dataService = dataService;
        _logger = logger;
        //_spinnerService = spinnerService;
    }

    protected override async Task<List<MatchResultModel>?>? FetchContent()
    {
        _logger.LogInformation("FetchContent called");
        var lst = (await _dataService.MatchResultForRegistration(matchId: MatchId)).ToList();
        return lst;
    }
}