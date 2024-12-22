// <copyright file="InvalidMoxiTestNameException.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi.Tests.MoxiTestUtils.Model;

/// <summary>
/// Represents an exception if a test-method has no proper Given_X_When_Y_Then_Z setup.
/// </summary>
/// <param name="message">Validation problem.</param>
public class InvalidMoxiTestNameException(string? message)
    : Exception(message)
{
}
