namespace Restion.Extensions
{
    internal static class ByteArrayExtensions
    {
        internal static bool IsNullOrEmpty(this byte[] source)
        {
            return source == null || source.Length == 0;
        }
    }
}