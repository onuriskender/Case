namespace Blog.Common.Helpers;

public static class StringHelper
{
  public static string ConvertTurkishCharsToEnglish(this string text)
  {
    text = text.Replace("ç", "c");
    text = text.Replace("Ç", "C");
    text = text.Replace("ğ", "g");
    text = text.Replace("Ğ", "G");
    text = text.Replace("ı", "i");
    text = text.Replace("İ", "I");
    text = text.Replace("ö", "o");
    text = text.Replace("Ö", "O");
    text = text.Replace("ş", "s");
    text = text.Replace("Ş", "S");
    text = text.Replace("ü", "u");
    text = text.Replace("Ü", "U");

    return text;
  }
  
  public static string RemoveSpaces(this string text)
  {
    return text.Replace(" ", string.Empty);
  }
}