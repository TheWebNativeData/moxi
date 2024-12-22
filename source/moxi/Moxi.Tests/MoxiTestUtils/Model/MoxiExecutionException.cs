// <copyright file="MoxiExecutionException.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi.Tests.MoxiTestUtils.Model;

/// <summary>
/// Initializes a new instance of the <see cref="MoxiExecutionException"/> class.
/// </summary>
/// <param name="message">Textual description.</param>
/// <param name="exception">Inner exception.</param>
public class MoxiExecutionException(string? message, Exception? exception)
    : Exception(message, exception)
{
}
