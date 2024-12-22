// <copyright file="MoxiResult.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi.Tests.MoxiTestUtils.Model;

/// <summary>
/// Result of a when-block that can be check in then-block.
/// </summary>
/// <typeparam name="TOutput"></typeparam>
public record MoxiResult<TOutput>
{
    /// <summary>
    /// Gets or sets the member name.
    /// </summary>
    public string? CallerMemberName { get; set; }

    /// <summary>
    /// Gets or sets the file name of the calling test.
    /// </summary>
    public string? CallerSourceFilePath { get; set; }

    /// <summary>
    /// Gets or sets the code line of the calling test in the file.
    /// </summary>
    public int? CallerSourceLineNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether it expects to be a successful test or not.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the value that is returned in when-code.
    /// </summary>
    public TOutput? Value { get; set; }

    /// <summary>
    /// Gets or sets the exception occuring during test.
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the test result is trustworthy.
    /// </summary>
    public bool IsConclusive { get; set; }

    /// <summary>
    /// Gets or sets the elapsed miliseconds in when-part.
    /// </summary>
    public long ElapsedMilliseconds { get; set; }
}