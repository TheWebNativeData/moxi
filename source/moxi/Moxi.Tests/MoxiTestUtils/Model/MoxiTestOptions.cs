// <copyright file="MoxiTestOptions.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi.Tests.MoxiTestUtils.Model;

/// <summary>
/// Class representing the options.
/// </summary>
public record MoxiTestOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether the output of the test should be successful if true or fail if false.
    /// </summary>
    public bool ShouldSucceed { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the test name should be checked.
    /// </summary>
    public bool CheckNamingConventions { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the test is a placeholder for later or not.
    /// </summary>
    public bool IsPreparedOnly { get; set; }

    /// <summary>
    /// Gets the default test option object.
    /// </summary>
    public static MoxiTestOptions Default { get; } = new MoxiTestOptions()
    {
        ShouldSucceed = true,
        CheckNamingConventions = true,
        IsPreparedOnly = false,
    };

    /// <summary>
    /// Gets the Maintenance options object.
    /// </summary>
    public static MoxiTestOptions Maintenance { get; } = new MoxiTestOptions()
    {
        ShouldSucceed = true,
        CheckNamingConventions = true,
        IsPreparedOnly = false,
    };

    /// <summary>
    /// Gets the test options object for consistently failing tests.
    /// </summary>
    public static MoxiTestOptions DefaultFailing { get; } = new MoxiTestOptions()
    {
        ShouldSucceed = false,
        CheckNamingConventions = true,
        IsPreparedOnly = false,
    };

    /// <summary>
    /// Gets the test options object for only prepared tests.
    /// </summary>
    public static MoxiTestOptions PreparedOnly { get; } = new MoxiTestOptions()
    {
        ShouldSucceed = true,
        CheckNamingConventions = true,
        IsPreparedOnly = true,
    };
}
