using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCaddie.Shared.Containers;
public abstract class ContainerBase<T>
{
    protected bool IsInitialized;

    public event Action? OnChange;

    private T? _content;
    public T? Content
    {
        get
        {
            Init();
            return _content;
        }
        set
        {
            _content = value;
            IsInitialized = true;
            NotifyStateChanged();
        }
    }

    protected void NotifyStateChanged() => OnChange?.Invoke();
    protected abstract void Init();
}
