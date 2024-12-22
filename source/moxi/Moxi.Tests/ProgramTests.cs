// <copyright file="ProgramTests.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi.Tests;

using Moxi.Tests.MoxiTestUtils;
using System.Text;
using static Moxi.Tests.MoxiTestUtils.MoxiTest;

#pragma warning disable IDE0079 // Unnötige Unterdrückung entfernen
#pragma warning disable CS1591 // Fehledes XML-Kommentar für öffentlich sichtbaren Typ oder Element
#pragma warning disable SA1600 // Elements should be documented
#pragma warning restore IDE0079 // Unnötige Unterdrückung entfernen

[TestClass]
public sealed class ProgramTests
{
    [TestMethod]
    public void Given_Program_When_CallingMain_Then_HelloWorldPrinted()
    {
        var data = new StringBuilder();
        using var textWriter = new StringWriter(data);

        RunMoxiTest(
            debug: () => data.ToString(),
            given: () => textWriter,
            when: writer => MoxiTestUtil.IsSafe(() => Program.MainOut(textWriter)),
            then: [
                (result) => Assert.AreEqual("Hello, World!" + Environment.NewLine, data.ToString())
            ]);
    }
}
