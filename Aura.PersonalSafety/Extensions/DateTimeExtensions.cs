namespace Aura.PersonalSafety.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToUtcIso8601(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
