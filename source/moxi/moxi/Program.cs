using System.Diagnostics.CodeAnalysis;

namespace Moxi;

/// <summary>
/// Entry point class.
/// </summary>
public class Program
{
    /// <summary>
    /// Program entry point method.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static void Main()
    {
        MainOut(Console.Out);
    }

    /// <summary>
    /// Program logic.
    /// </summary>
    /// <param name="consoleOut"></param>
    public static void MainOut(TextWriter consoleOut)
    {
        consoleOut.WriteLine("Hello, World!");
    }
}
