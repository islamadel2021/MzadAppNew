namespace MzadService;

public static class Extensions
{
    public static string CapitalizeFirstLetter(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }
        return char.ToUpper(input[0]) + input[1..];
    }
}
