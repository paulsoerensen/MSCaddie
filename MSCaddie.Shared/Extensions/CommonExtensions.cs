namespace MSCaddie.Shared.Extensions;

public static class CommonExtensions
{
    public static bool IsDebug()
    {
#if DEBUG
        return true;
#else
        return false;
#endif
    }

    public static DateTime CustomDateTimeNow(this DateTime dateTime)
    {
#if DEBUG
        return new DateTime(2024, 6, 6);
#else
        return DateTime.Now;
#endif
    }
}
