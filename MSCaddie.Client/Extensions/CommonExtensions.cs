namespace MSCaddie.Client.Extensions
{
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

    }
}
