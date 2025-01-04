namespace PopularRadioSongs.Application.Extensions
{
    public static class DateTimeOffsetExtension
    {
        public static DateTimeOffset ToLastFullHour(this DateTimeOffset dateTimeOffset)
        {
            return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, 0, 0, dateTimeOffset.Offset);
        }
    }
}