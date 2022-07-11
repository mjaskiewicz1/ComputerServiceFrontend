using Humanizer;

namespace Common.Helppers;

public static class NumberToTextHelper
{
    private static readonly Dictionary<string, string[]> Currency = new()
    {
        //Formy podawane według wzorca: jeden-dwa-pięć, np.
        //(jeden) złoty, (dwa) złote, (pięć) złotych
        { "PLN", new[] { "złoty", "złote", "złotych" } },
        { ".PLN", new[] { "grosz", "grosze", "groszy" } }
    };

    public static string MonetaryUnitsInWords(int number, string currencyCode)
    {
        var key = Currency[currencyCode];
        return key[SetMonetaryUnits(number)];
    }

    private static int SetMonetaryUnits(int number)
    {
        if (number == 1)
            return 0;

        var pair = number % 100;
        if (pair >= 10 && pair < 20)
            return 2;

        var unit = number % 10;
        if (unit >= 2 && unit <= 4)
            return 1;

        return 2;
    }

    public static string TotalInWords(decimal total, string currencyCode = "PLN")
    {
        var integerPart = (int)total;
        var fraction = (int)(total * 100) % 100;
        return
            $"{integerPart.ToWords()} {MonetaryUnitsInWords(integerPart, currencyCode)}, {fraction.ToWords()} {MonetaryUnitsInWords(fraction, "." + currencyCode)}";
    }
}