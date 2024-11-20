namespace FooFuu.Core.Interfaces;

/// <summary>
/// Interface for output devices (console, file, etc.).
/// </summary>
public interface IOutputDevice
{
    Task WriteAsync(IEnumerable<string> data);
}