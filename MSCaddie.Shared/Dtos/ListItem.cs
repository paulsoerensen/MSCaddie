namespace MSCaddie.Shared.Dtos;

public class ListItem
{
    public ListItem()
    {
    }

    public ListItem(int keyId, string keyValue)
    {
        KeyId = keyId;
        KeyValue = keyValue;
    }

    public int KeyId { get; set; } = default!;
    public string KeyValue { get; set; } = default!;
}
