using Blazored.SessionStorage;
using System.Text;
using System.Text.Json;

namespace MSCaddie.Shared.Extensions;

public static class SessionStorageServiceExtension
{
    public static async Task SaveItemEncrypytedAsync<T>(this ISessionStorageService svc, string key, T item)
    {
        var itemJson = JsonSerializer.Serialize(item);
        var itemBytes = Encoding.UTF8.GetBytes(itemJson);
        var base64 = Convert.ToBase64String(itemBytes);
        await svc.SetItemAsync(key, base64);
    }
    public static async Task<T> ReadEncryptedItemAsync<T>(this ISessionStorageService svc, string key)
    {
        var base64 = await svc.GetItemAsync<string>(key);
        var itemBytes = Convert.FromBase64String(base64);
        var itemJson = Encoding.UTF8.GetString(itemBytes);
        var item = JsonSerializer.Deserialize<T>(itemJson);
        return item;
    }


}
