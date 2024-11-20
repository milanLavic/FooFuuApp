using FooFuu.Core.Interfaces;
using NLog;

namespace FooFuu.Core.Output;

/// <summary>
/// Outputs messages to a file.
/// </summary>
public class FileOutputDevice : IOutputDevice
{
    private readonly string _filePath;
    private readonly ILogger _logger;

    public FileOutputDevice(string filePath, ILogger logger)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task WriteAsync(IEnumerable<string> data)
    {
        try
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await File.WriteAllLinesAsync(_filePath, data);
            _logger.Info($"Data written to file {_filePath}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error writing data to file.");
            throw new Exception("Failed to write to file.", ex);
        }
    }
}