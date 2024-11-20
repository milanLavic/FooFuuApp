using FooFuu.Core;
using FooFuu.Core.Errors;
using FooFuu.Core.Interfaces;
using FooFuu.Core.Output;
using FooFuuApp.Helper;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace FooFuuApp;

class Program
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public static async Task Main(string[] args)
    {
        try
        {
            LogManager.LoadConfiguration("nlog.config");

            // Parse arguments using ArgumentParser
            var model = ArgumentParser.ParseArguments(args);

            // Set default start and end values
            int start = model.Start ?? 1;
            int end = model.End ?? 100;

            // Default to ConsoleOutputDevice if no output is provided
            IOutputDevice outputDevice;

            if (string.IsNullOrEmpty(model.OutputDevice) || model.OutputDevice.Equals("console", StringComparison.OrdinalIgnoreCase))
            {
                outputDevice = new ConsoleOutputDevice();
            }
            else if (model.OutputDevice.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                if (!PathHelper.IsValidPath(model.FilePath))
                {
                    Logger.Fatal("Valid file path must be provided when output device is file.");
                    throw new ArgumentException("Valid file path is required when output is set to file.");
                }

                outputDevice = new FileOutputDevice(model.FilePath,Logger);
            }
            else
            {
                throw new ArgumentException($"Unsupported output device: {model.OutputDevice}");
            }

            // Create a processor and process numbers
            var processor = new FooFuuProcessor();
            await processor.ProcessNumbersAsync(start, end, outputDevice);

            Logger.Info("Processing complete.");
        }
        catch (ValidationException ex)
        {
            Logger.Error(ex, "Input validation failed.");
            Console.WriteLine($"Validation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Logger.Fatal(ex, "An unexpected error occurred.");
            Console.WriteLine($"Critical error: {ex.Message}");
        }
    }
}
