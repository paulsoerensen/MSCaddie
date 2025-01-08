namespace MSCaddie.Shared.Dtos;

public class ListIntItem
{
    public ListIntItem()
    {
    }

    public ListIntItem(int keyId, int keyValue)
    {
        KeyId = keyId;
        KeyValue = keyValue;
    }

    public int KeyId { get; set; } = default!;
    public int KeyValue { get; set; } = default!;
}
