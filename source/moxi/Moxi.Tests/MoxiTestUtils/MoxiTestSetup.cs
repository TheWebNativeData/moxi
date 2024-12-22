// <copyright file="MoxiTestSetup.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi.Tests.MoxiTestUtils;

using FluentAssertions;
using Moxi.Tests.MoxiTestUtils.Model;
using System.Linq;
using static MoxiTest;

/// <summary>
/// Basic maintenance tests for moxi setup.
/// </summary>
[TestClass]
public sealed class MoxiSetupTests
{
    /// <summary>
    /// Gets the MaxCountOfTests that should be stored historically.
    /// </summary>
    public const int MaxTestResults = 200;

    /// <summary>
    /// Gets or sets the TestContext injected by the framework.
    /// </summary>
    public TestContext TestContext { get; set; } = null!;

    /// <summary>
    /// Tests for checking compliant namings in the project.
    /// </summary>
    [TestMethod]
    public void Given_TestSuite_When_CheckingTests_Then_NamesShouldBeCompliant()
    {
        RunMoxiTest(
            given: () => MoxiTestName.GetAllTestNames(),
            when: (testNames) => testNames!.Select(name => name.ValidateTestName()),
            then:
            [
                result => result.IsSuccessful.Should().BeTrue(),
                result => result.Value.Should().AllBeOfType<bool>(),
                result => result.Value.Should().NotContain(false)
            ],
            options: MoxiTestOptions.Maintenance);
    }

    /// <summary>
    /// Tests for checking test result archive size.
    /// </summary>
    [TestMethod]
    public void Given_TestSuite_When_CheckingEnvironment_Then_FolderShouldNotExceedLimit()
    {
        RunMoxiTest(
            given: () => Path.Combine(this.TestContext.TestRunDirectory ?? ".", ".."),
            when: folder => Directory.GetDirectories(folder!).Length,
            then: [count => count.Value.Should().BeLessThan(MaxTestResults)],
            options: MoxiTestOptions.Maintenance);
    }
}
