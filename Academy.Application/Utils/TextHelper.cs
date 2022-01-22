
namespace Academy.Application.Utils
{
   public static class TextHelper
    {
        public static string ToSlug(this string value)
        {
            return value.Trim().ToLower()
                .Replace("~", "")
                .Replace("`", "")
                .Replace("@", "")
                .Replace("$", "")
                .Replace("%", "")
                .Replace("^", "")
                .Replace("&", "")
                .Replace("*", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("+", "")
                .Replace("_", "")
                .Replace(@"\", "")
                .Replace("|", "")
                .Replace("/", "")
                .Replace(">", "")
                .Replace("<", "")
                .Replace(" ", "-");

        }
    }
}
