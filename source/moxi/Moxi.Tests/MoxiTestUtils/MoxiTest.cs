// <copyright file="MoxiTest.cs" company="TheWebNativeData">
// Copyright (c) TheWebNativeData. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.
// </copyright>

namespace Moxi.Tests.MoxiTestUtils;

using FluentAssertions;
using Moxi.Tests.MoxiTestUtils.Model;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

/// <summary>
/// The main class that should be included statically to run moxi tests.
/// </summary>
public static class MoxiTest
{
    /// <summary>
    /// Run Moxi tests.
    /// </summary>
    /// <typeparam name="TInput">Input type.</typeparam>
    /// <typeparam name="TOutput">Output type.</typeparam>
    /// <param name="debug">Objects to debug. Calculate as string.</param>
    /// <param name="given">Arrange data.</param>
    /// <param name="when">Act code to test.</param>
    /// <param name="then">Assert results.</param>
    /// <param name="options">Test execution options.</param>
    /// <param name="memberName">Caller member name.</param>
    /// <param name="sourceFilePath">Caller source file path.</param>
    /// <param name="sourceLineNumber">Caller source code line number in file path.</param>
    public static void RunMoxiTest<TInput, TOutput>(
        Func<string>? debug = null,
        Func<TInput?>? given = null,
        Func<TInput?, TOutput?>? when = null,
        IEnumerable<Action<MoxiResult<TOutput?>>?>? then = null,
        MoxiTestOptions? options = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        // pre-conditions
        var result = new MoxiResult<TOutput?>()
        {
            CallerMemberName = memberName,
            CallerSourceFilePath = sourceFilePath,
            CallerSourceLineNumber = sourceLineNumber,
        };
        Log($"Execute test {result.CallerMemberName}");
        Log($"  {Path.GetFileName(result.CallerSourceFilePath)}:{result.CallerSourceLineNumber} - {result.CallerSourceFilePath}");
        LogSeparator();

        options ??= MoxiTestOptions.Default;
        if (options.CheckNamingConventions)
        {
            Log($"PRE  : Checking TestName");
            var testName = new MoxiTestName(memberName);
            testName.ValidateTestName();
            Log($"PRE  : TestName checked successfully");
            LogSeparator();

            var (givenPart, whenPart, thenPart) = testName.GetNameParts();
            Log($"NAME : GIVEN {givenPart}");
            Log($"NAME : WHEN  {whenPart}");
            Log($"NAME : THEN  {thenPart}");
            LogSeparator();
        }

        if (options.IsPreparedOnly)
        {
            Log("PRE  : IsPrepared only... report inconclusive");
            LogSeparator();
            Assert.Inconclusive();
        }

        // debug
        if (debug != null)
        {
            Log($"DEBUG: '{debug()}'");
            LogSeparator();
        }

        // arrange
        TInput? item = default;
        if (given != null)
        {
            Log("GIVEN: Creating test data");
            item = given();
            Log($"GIVEN: Test data created: '{item?.ToString() ?? "<null>"}' [{item?.GetType()?.ToString() ?? "<null>"}]");
            LogSeparator();
        }

        // debug
        if (debug != null)
        {
            Log($"DEBUG: '{debug()}'");
            LogSeparator();
        }

        // act
        Stopwatch stopwatch = Stopwatch.StartNew();

        try
        {
            result.IsConclusive = false;
            if (when != null)
            {
                Log("WHEN : Executing test");
                result.Value = when(item);
                result.IsConclusive = true;
                result.IsSuccessful = true;
                Log("WHEN : Test succeeded");
            }
        }
        catch (Exception exception)
        {
            Log($"WHEN : Test failed: {exception.Message}");
            result.Exception = new MoxiExecutionException("Execution failed", exception);
            result.IsConclusive = true;
            result.IsSuccessful = false;
        }

        stopwatch.Stop();
        result.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        Log($"WHEN : Execution took: {result.ElapsedMilliseconds}ms");
        LogSeparator();

        // debug
        if (debug != null)
        {
            Log($"DEBUG: '{debug()}'");
            LogSeparator();
        }

        // assert
        try
        {
            if (then != null && result.IsSuccessful)
            {
                int runId = 1;
                foreach (var check in then)
                {
                    Log($"THEN : {runId:00} - Checking");
                    check?.Invoke(result);
                    Log($"THEN : {runId:00} - checked");
                    runId++;
                }
            }
        }
        catch (Exception exception)
        {
            result.Exception = new MoxiExecutionException("Check failed", exception);
            result.IsSuccessful = false;
        }

        if (!result.IsConclusive)
        {
            Log($"THEN : Result is Inconclusive");
            Assert.Inconclusive();
        }

        Log($"THEN : TestResult: Succesful={result.IsSuccessful} Expected={options.ShouldSucceed}");
        result.IsSuccessful.Should().Be(options.ShouldSucceed);
    }

    private static void LogSeparator()
    {
        Log("---------------------------------------------------------");
    }

    private static void Log(string message, string ident = "")
    {
        Console.WriteLine(ident + message);
    }
}
