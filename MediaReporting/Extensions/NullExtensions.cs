namespace MediaReporting.Extensions
{
    public static class NullExtensions
    {
        public static T ThrowIfNull<T>(this T? source, string errorMessage)
        {
            if (source != null)
            {
                return source;
            }

            throw new ArgumentNullException(nameof(source), errorMessage);
        }
    }
}
