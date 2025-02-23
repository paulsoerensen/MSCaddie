using MSCaddie.Shared.Models;

namespace MSCaddie.Shared.Containers;
public class SearchStateContainerList : ContainerListBase<MatchResultModel>
{
    private readonly MatchResultContainerList _MatchResultContainerList;
    private string _searchTerm = "";

    private List<MatchResultModel>? FilteredMatchResults => _MatchResultContainerList.Content?
        .Where(model => model.Fullname != null &&
                        model.Fullname.Contains(_searchTerm, StringComparison.InvariantCultureIgnoreCase))
        .ToList();

    public SearchStateContainerList(MatchResultContainerList MatchResultContainerList)
    {
        _MatchResultContainerList = MatchResultContainerList;

        // Automatically update filtered MatchResults when the MatchResult Container is updated
        _MatchResultContainerList.OnChange += SetFilteredMatchResults;
    }

    public void SetSearchTermAndNotify(string searchTerm)
    {
        _searchTerm = searchTerm;
        SetFilteredMatchResults();
    }

    protected override Task<List<MatchResultModel>> FetchContent()
    {
        return Task.FromResult(FilteredMatchResults ?? new List<MatchResultModel>());
    }

    private void SetFilteredMatchResults()
    {
        Content = FilteredMatchResults;
    }
}