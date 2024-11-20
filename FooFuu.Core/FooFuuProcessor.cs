using FooFuu.Core.Helper;

namespace FooFuu.Core;

/// <summary>
/// Processes numbers based on divisibility rules for "foo" and "fuu".
/// </summary>
public class FooFuuProcessor : NumberProcessorBase
{
    protected override string ProcessNumber(int number)
    {
        if (number % 4 == 0) return "foofuu";
        if (number % 2 == 0) return "foo";
        return number.ToString();
    }
}