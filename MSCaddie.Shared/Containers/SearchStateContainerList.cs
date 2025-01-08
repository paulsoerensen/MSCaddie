using MSCaddie.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCaddie.Shared.Containers;
    public class SearchStateContainerList : ContainerListBase<MatchResultDto>
{
    private readonly MatchResultContainerList _MatchResultContainerList;
    private string _searchTerm = "";

    private List<MatchResultDto>? FilteredMatchResults => _MatchResultContainerList.Content?
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

    protected override Task<List<MatchResultDto>> FetchContent()
    {
        return Task.FromResult(FilteredMatchResults ?? new List<MatchResultDto>());
    }

    private void SetFilteredMatchResults()
    {
        Content = FilteredMatchResults;
    }
}