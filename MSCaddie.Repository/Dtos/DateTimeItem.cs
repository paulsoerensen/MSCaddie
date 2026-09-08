namespace MSCaddie.Repository.Dtos;

public class DateTimeItem
{
    public DateTimeItem()
    {
    }

    public DateTimeItem(int key, DateTime value)
    {
        Key = key;
        Value = value;
    }

    public int Key { get; set; } = default!;
    public DateTime Value { get; set; } = default!;
}
