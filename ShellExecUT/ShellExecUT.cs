// Copyright and trademark notices at the end of this file.

using SharperHacks.CoreLibs.AppUtilities;
using SharperHacks.CoreLibs.LogWrappers;

using System.Diagnostics.CodeAnalysis;

namespace SharperHacks.CoreLibs.Miscellaneous.ShellExecUT;

[TestClass]
[ExcludeFromCodeCoverage]
public class ShellExecUT
{
    [TestMethod]
    public void SmokeShellExec()
    {
        var telemetry = new Telemetry(logToConsole:true);
//        telemetry.ConsoleLoggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Information;

        var appToRun = new ShellExec("TestDummy.exe", $"-ss:TestDummy", logger: telemetry.Logger);

        var result = appToRun.RunSync();

        Assert.AreEqual(0, result);
        Assert.Contains("TestDummy", appToRun.StdOutput);

        Console.WriteLine($"Log file: {telemetry.LogPathFileName}");
        Console.WriteLine($"Product name: {AppConfig.ProductName}");

        Console.WriteLine(appToRun.StdOutput);
    }

    [TestMethod]
    public void WithReturnCodeAndErrorOutput()
    {
        var errMsg = "Standard error output.";
        var expectedCode = 42;

        var appToRun = new ShellExec("TestDummy.exe", $"-ss:TestDummy -es:\"{errMsg}\" -rc:{expectedCode}");

        var result = appToRun.RunSync();

        Console.WriteLine($"stdout: {appToRun.StdOutput}");
        Console.WriteLine($"stderr: {appToRun.StdError}");

        Assert.AreEqual(42, result);
        Assert.Contains("TestDummy", appToRun.StdOutput);
        Assert.Contains(errMsg, appToRun.StdError);
    }
}

// Copyright Joseph W Donahue and Sharper Hacks LLC (US-WA)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// SharperHacks is a trademark of Sharper Hacks LLC (US-Wa), and may not be
// applied to distributions of derivative works, without the express written
// permission of a registered officer of Sharper Hacks LLC (US-WA).
