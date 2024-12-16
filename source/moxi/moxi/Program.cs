// <copyright file="Program.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Entry point class.
/// </summary>
public static class Program
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
    /// <param name="consoleOut">Text writer.</param>
    public static void MainOut(TextWriter consoleOut)
    {
        consoleOut.WriteLine("Hello, World!");
    }
}
