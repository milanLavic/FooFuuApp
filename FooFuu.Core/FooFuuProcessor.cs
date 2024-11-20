using FooFuu.Core.Helper;

namespace FooFuu.Core;

/// <summary>
/// Processes numbers based on divisibility rules for "foo" and "fuu".
/// </summary>
public class FooFuuProcessor : NumberProcessorBase
{
    private const int FooFuuNumber = 4;
    private const int FooNumber = 2;
    private const string FooFuu = "foofuu";
    private const string Foo = "foo";
    
    protected override string ProcessNumber(int number)
    {
        if (IsFooFuu(number)) return FooFuu;
        if (IsFoo(number)) return Foo;
        return number.ToString();
    }

    public bool IsFooFuu(int number)
    {
        return number % FooFuuNumber == 0;
    }
    
    public bool IsFoo(int number)
    {
        return number % FooNumber == 0;
    }
}