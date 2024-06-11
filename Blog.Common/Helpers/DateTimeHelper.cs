namespace Blog.Common.Helpers;

public static class DateTimeHelper
{
  public static string ConvertUtcToIstanbul(this DateTime date)
  {
    var istanbulZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Istanbul");
    return TimeZoneInfo.ConvertTimeFromUtc(date, istanbulZone)
      .ToString("dd-MM-yyyy HH:mm");
  }
}