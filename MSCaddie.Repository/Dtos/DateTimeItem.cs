namespace MSCaddie.Repository.Dtos;

public class DateTimeItem
{
    public DateTimeItem()
    {
    }

    public DateTimeItem(int keyId, DateTime keyValue)
    {
        KeyId = keyId;
        KeyValue = keyValue;
    }

    public int KeyId { get; set; } = default!;
    public DateTime KeyValue { get; set; } = default!;
}
