namespace MSCaddie.Repository.Dtos;

public class DateTimeItem
{
    public DateTimeItem()
    {
    }

    public DateTimeItem(int keyId, DateTime value)
    {
        KeyId = keyId;
        Value = value;
    }

    public int KeyId { get; set; } = default!;
    public DateTime Value { get; set; } = default!;
}
