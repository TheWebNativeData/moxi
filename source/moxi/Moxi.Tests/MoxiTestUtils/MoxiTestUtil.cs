// <copyright file="MoxiTestUtil.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi.Tests.MoxiTestUtils;

/// <summary>
/// Helper functions for creating tests.
/// </summary>
public static class MoxiTestUtil
{
    /// <summary>
    /// Executes a test and return true if ok.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <returns>True if ok and else throws exception.</returns>
    /// <exception cref="InvalidOperationException">Throws exception on any kind of error.</exception>
    public static bool IsSafe(Action action)
    {
        try
        {
            action();
            return true;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Exception occured when Safe operation expected", e);
        }
    }
}
