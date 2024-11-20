using FooFuu.Core.Errors;
using FooFuu.Core.Interfaces;
using NLog;

namespace FooFuu.Core.Helper;

/// <summary>
/// Base class for processing numbers with error handling and logging, now async.
/// </summary>
public abstract class NumberProcessorBase : INumberProcessor
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public IEnumerable<int> InputNumbers { get; private set; }

    /// <summary>
    /// Sets the range of input numbers to be processed.
    /// Throws ValidationException if the range is invalid.
    /// </summary>
    public void SetInputRange(int start, int end)
    {
        if (start <= 0 || end <= 0)
        {
            Logger.Error("Start and end values must be greater than 0.");
            throw new ValidationException("Invalid range: start and end must be greater than 0.");
        }

        if (start > end)
        {
            Logger.Error("Start number must be less than or equal to the end number.");
            throw new ValidationException("Invalid range: start must be less than or equal to end.");
        }

        InputNumbers = Enumerable.Range(start, end - start + 1);
        Logger.Info($"Input range set: {start} to {end}");
    }

    public async Task ProcessNumbersAsync(int start, int end, IOutputDevice outputDevice)
    {
        try
        {
            SetInputRange(start, end);
        }
        catch (ValidationException ex)
        {
            Logger.Error(ex, "Failed to set input range.");
            throw;
        }

        await ProcessNumbersAsync(outputDevice);
    }

    public async Task ProcessNumbersAsync(IOutputDevice outputDevice)
    {
        if (outputDevice == null)
        {
            Logger.Error("Output device cannot be null.");
            throw new ValidationException("Output device is required.");
        }

        if (InputNumbers == null)
        {
            Logger.Error("Input numbers are not set.");
            throw new ValidationException("Input numbers must be set before processing.");
        }

        // Prepare the list of processed results
        var results = new List<string>();

        foreach (var number in InputNumbers)
        {
            try
            {
                var result = ProcessNumber(number);
                results.Add(result);
                Logger.Info($"Processed number {number}: {result}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error processing number {number}: {ex.Message}");
                results.Add($"Error: {ex.Message}"); // You can customize this behavior as needed
            }
        }

        // Write all results asynchronously to the output device
        await outputDevice.WriteAsync(results);
    }

    protected abstract string ProcessNumber(int number);
}
