using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooFuuApp.Helper;

internal class PathHelper
{
    /// <summary>
    /// Check if path is valid
    /// </summary>
    /// <param name="path">input path</param>
    /// <returns></returns>
    public static bool IsValidPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        // Check if path is a fully qualified path
        if (!Path.IsPathFullyQualified(path))
            return false;

        try
        {
            // Try to get a valid file or directory name from the path
            string fullPath = Path.GetFullPath(path);
            return true;
        }
        catch (Exception)
        {
            // If an exception occurs, it's an invalid path
            return false;
        }
    }
}
