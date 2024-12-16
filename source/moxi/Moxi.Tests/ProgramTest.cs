using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;

namespace Moxi.Tests;

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
