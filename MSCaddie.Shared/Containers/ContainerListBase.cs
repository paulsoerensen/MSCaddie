using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCaddie.Shared.Containers;
public abstract class ContainerListBase<T> : ContainerBase<List<T>>
{
    protected override async void Init()
    {
        if (IsInitialized) return;

        IsInitialized = true;

        Content = await FetchContent();
    }

    public async void RefreshCache()
    {
        Content = await FetchContent();
    }

    protected void UpdateLocalCacheWithItem(T? item, Predicate<T> findIndexPredicate)
    {
        if (Content is null || item is null)
            return;

        var index = Content.FindIndex(findIndexPredicate);

        if (index == -1)
        {
            Content.Add(item);
        }
        else
        {
            Content[index] = item;
        }

        NotifyStateChanged();
    }

    protected abstract Task<List<T>> FetchContent();
}
