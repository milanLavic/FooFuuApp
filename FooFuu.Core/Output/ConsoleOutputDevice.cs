using FooFuu.Core.Interfaces;

namespace FooFuu.Core.Output;

/// <summary>
/// Outputs messages to the console.
/// </summary>
public class ConsoleOutputDevice : IOutputDevice
{
    public async Task WriteAsync(IEnumerable<string> data)
    {
        foreach (var item in data)
        {
            await Task.Run(() => Console.WriteLine(item));
        }
    }
}