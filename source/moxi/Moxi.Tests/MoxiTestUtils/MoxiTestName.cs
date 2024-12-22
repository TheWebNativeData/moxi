// <copyright file="MoxiTestName.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi.Tests.MoxiTestUtils;

using FluentAssertions.Execution;
using Moxi.Tests.MoxiTestUtils.Model;
using System.Reflection;

/// <summary>
/// Checks the test name.
/// </summary>
/// <param name="name">Plain text method name of the test.</param>
public class MoxiTestName(string name)
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets all test names.
    /// </summary>
    /// <returns>All test names as <see cref="MoxiTestName"/> objects.</returns>
    /// <exception cref="InvalidOperationException">Unrealistic. Happens if assembly of current class not found.</exception>
    public static IEnumerable<MoxiTestName> GetAllTestNames()
    {
        return (Assembly
            .GetAssembly(typeof(MoxiTestName)) ?? throw new InvalidOperationException("GetAssembly returns null"))
            .GetTypes()
            .Where(type => type.GetCustomAttributes<TestClassAttribute>().Any())
            .SelectMany(type => type.GetMethods()
                .Where(method => method.GetCustomAttributes<TestMethodAttribute>().Any()))
            .Select(methodInfo => methodInfo.Name)
            .Select(name => new MoxiTestName(name));
    }

    /// <summary>
    /// Validates a test name.
    /// </summary>
    /// <param name="testName">The name of the test.</param>
    /// <returns>True if the name is valid, else  throws exception.</returns>
    public static bool ValidateTestName(string testName)
    {
        var nameparts = testName.Split('_', StringSplitOptions.RemoveEmptyEntries);

        if (nameparts.Length != 6)
        {
            throw new InvalidMoxiTestNameException("Given_X_When_Y_Then_Z - naming convention has not right count of elements");
        }

        if (nameparts[0] != "Given" || nameparts[2] != "When" || nameparts[4] != "Then")
        {
            throw new InvalidMoxiTestNameException("Given_X_When_Y_Then_Z - naming convention has not used right keywords");
        }

        var givenDescription = nameparts[1];
        var whenDescription = nameparts[3];
        var thenDescription = nameparts[5];

        if (!char.IsUpper(givenDescription[0]))
        {
            throw new InvalidMoxiTestNameException("Given_X_When_Y_Then_Z - naming convention Given-Description does not start with an upper case character.");
        }

        if (!char.IsUpper(whenDescription[0]))
        {
            throw new InvalidMoxiTestNameException("Given_X_When_Y_Then_Z - naming convention When-Description does not start with an upper case character.");
        }

        if (!char.IsUpper(thenDescription[0]))
        {
            throw new InvalidMoxiTestNameException("Given_X_When_Y_Then_Z - naming convention Then-Description does not start with an upper case character.");
        }

        return true;
    }

    /// <summary>
    /// Manipulates the testname to add a scenario name.
    /// </summary>
    /// <param name="testName">Original test name.</param>
    /// <param name="scenario">Scenario to extend.</param>
    /// <returns>The fullly compiled name.</returns>
    public static string AddScenarioToTestName(string testName, string scenario)
    {
        if (string.IsNullOrWhiteSpace(scenario))
        {
            return testName;
        }

        var newName = testName.Replace(
                "_Then_",
                "With" + scenario + "_Then_",
                StringComparison.InvariantCulture);
        return newName;
    }

    /// <summary>
    /// Validates a test name.
    /// </summary>
    /// <returns>True if the name is valid, else  throws exception.</returns>
    public bool ValidateTestName()
    {
        return ValidateTestName(this.Name);
    }

    /// <summary>
    /// Calculates the parts of a Given-When-Then based test name.
    /// </summary>
    /// <returns>The 3 name parts as tuple.</returns>
    public (string Given, string When, string Then) GetNameParts()
    {
        this.ValidateTestName();
        var nameparts = this.Name.Split('_', StringSplitOptions.RemoveEmptyEntries);
        return (nameparts[1], nameparts[3], nameparts[5]);
    }

    /// <summary>
    /// Manipulates the testname to add a scenario name.
    /// </summary>
    /// <param name="scenario">Scenario to extend.</param>
    /// <returns>The fullly compiled name.</returns>
    public string AddScenarioToTestName(string scenario)
    {
        return AddScenarioToTestName(this.Name, scenario);
    }
}
