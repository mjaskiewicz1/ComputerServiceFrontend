using System.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.Exstensions;

public static class PuppeteerExtensions
{
    public static byte[] ReadAllBytes(this Stream instream)
    {
        if (instream is MemoryStream)
            return ((MemoryStream)instream).ToArray();

        using (var memoryStream = new MemoryStream())
        {
            instream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public static IEnumerable Errors(this ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
            return modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                        .Select(e => e.ErrorMessage).ToArray())
                .Where(m => m.Value.Any());
        return null;
    }
}