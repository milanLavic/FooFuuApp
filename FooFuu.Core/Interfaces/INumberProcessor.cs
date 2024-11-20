namespace FooFuu.Core.Interfaces;

/// <summary>
/// Defines a contract for number processors.
/// </summary>
public interface INumberProcessor
{
    /// <summary>
    /// Processes and outputs numbers based on rules.
    /// </summary>
    Task ProcessNumbersAsync(int start, int end, IOutputDevice outputDevice);
}