namespace MSCaddie.Shared.Dtos;

public class ListEntryDto
{
    public ListEntryDto()
    {
    }

    public ListEntryDto(int keyId, string keyValue)
    {
        KeyId = keyId;
        KeyValue = keyValue;
    }

    public int KeyId { get; set; } = default!;
    public string KeyValue { get; set; } = default!;
}
