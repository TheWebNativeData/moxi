// <copyright file="ProgramTest.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi.Tests;

using System.Text;

[TestClass]
public sealed class ProgramTest
{
    [TestMethod]
    public void Given_Program_When_CallingMain_Then_HelloWorldPrinted()
    {
        // Arrange
        var data = new StringBuilder();
        using var textWriter = new StringWriter(data);

        // Act
        Program.MainOut(textWriter);

        // Assert
        Assert.AreEqual("Hello, World!" + Environment.NewLine, data.ToString());
    }
}
