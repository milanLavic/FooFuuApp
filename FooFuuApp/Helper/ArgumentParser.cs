using FooFuuApp.Models;

namespace FooFuuApp.Helper;

public static class ArgumentParser
{
    public static ArgumentModel ParseArguments(string[] args)
    {
        var model = new ArgumentModel();

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
                case "/start":
                case "/s":
                    if (i + 1 < args.Length && int.TryParse(args[i + 1], out int start))
                    {
                        model.Start = start;
                    }
                    break;

                case "/end":
                case "/e":
                    if (i + 1 < args.Length && int.TryParse(args[i + 1], out int end))
                    {
                        model.End = end;
                    }
                    break;

                case "/output":
                case "/o":
                    if (i + 1 < args.Length)
                    {
                        model.OutputDevice = args[i + 1];
                    }
                    break;

                case "/filepath":
                case "/fp":
                    if (i + 1 < args.Length)
                    {
                        model.FilePath = args[i + 1];
                    }
                    break;
            }
        }

        return model;
    }
}