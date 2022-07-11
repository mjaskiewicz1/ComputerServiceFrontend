using System.ComponentModel.DataAnnotations;

namespace Common.Helppers.ValidationAttributes;

public class Nip : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not string) return false;

        var nip = value as string;
        nip = nip.Replace("-", string.Empty);

        if (nip.Length != 10 || nip.Any(chr => !char.IsDigit(chr)))
            return false;

        int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7, 0 };
        var sum = nip.Zip(weights, (digit, weight) => (digit - '0') * weight).Sum();

        return sum % 11 == nip[9] - '0';
    }
}