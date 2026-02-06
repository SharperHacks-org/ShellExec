// Copyright and trademark notices at the end of this file.

using Microsoft.Extensions.Logging;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

// ToDo: Change this namespace!
namespace SharperHacks.CoreLibs.Miscellaneous;

// Wrapper class for synchronously running separate processes, and capturing
// their output.
//
public class ShellExec : ShellExecBase
{
    #region public

    // The captured  stdout from the last command execution.
    //
    public string StdOutput { get; private set; }

    // The captured stderr from the last command execution.
    //
    public string StdError { get; private set; }

    // Run Cmd with Args, synchronously.
    //
    // Returns: Process exit code.
    //
    public int RunSync()
    {
        TraceStart();

        _ = Process.Start();
        StdOutput = Process.StandardOutput.ReadToEnd();
        StdError = Process.StandardError.ReadToEnd();
        Process.WaitForExit();

        TraceStop();

        return Process.ExitCode;
    }

    #region Constructors

    // Constructor.
    //
    // Parameters:
    //  @cmd   Executable to run.
    //  @args  The argument string to pass to executable.
    //  @workingDir The processes working directory.
    //  @useShellExecute See ProcessStartInfo.UseShellExecute.
    //  @logger An ILogger for trace logging.
    //
    public ShellExec(
        string cmd,
        string args,
        string? workingDir = null,
        bool useShellExecute = false,
        ILogger? logger = null)
        : base(cmd, args, workingDir, useShellExecute, logger)
    {
        StdOutput = string.Empty;
        StdError = string.Empty;
    }

    // Construct an instance from an initialized ProcessStartInfo object.
    //
    // Parameters:
    //  @psi The ProcessStartInfo to pass to the Process instance.
    //  @logger An ILogger for trace logging.
    //
    public ShellExec(ProcessStartInfo psi, ILogger? logger = null)
        : base(psi, logger)
    {
        StdOutput = string.Empty;
        StdError = string.Empty;
    }

    #endregion Constructors

    #endregion public

    #region private

    [SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "It only changes if the code is recompiled.")]
    [SuppressMessage("Performance", "CA1848:Use LoggerMessage delegates", Justification = "Performance gain not required")]
    private void TraceStart(
        [CallerMemberName] in string memberName = "",
        [CallerFilePath] in string fileName = "",
        [CallerLineNumber] in int lineNumber = 0)
    {
        if (_log is null) return;

        var sourceLineInfo = $"{fileName}({lineNumber})";
        var classMemberInfo = $"{nameof(ShellExec)}.{memberName}";
        var msgFormat = $"Trace entry: {classMemberInfo}('{{Cmd}} {{Args}}') @ {{sourceLineInfo}}";

        _log.LogTrace(msgFormat, Cmd, Args, sourceLineInfo);

        _stopwatch.Start();
    }

    [SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "It only changes if the code is recompiled.")]
    [SuppressMessage("Performance", "CA1848:Use LoggerMessage delegates", Justification = "Performance gain not required")]
    private void TraceStop(
        [CallerMemberName] in string memberName = "",
        [CallerFilePath] in string fileName = "",
        [CallerLineNumber] in int lineNumber = 0)
    {
        if (_log is null) return;

        var sourceLineInfo = $"{fileName}({lineNumber})";
        var classMemberInfo = $"{nameof(ShellExec)}.{memberName}";
        var msgFormat = $"Trace exit: {classMemberInfo}('{{Cmd}} {{Args}}') @ {{sourceLineInfo}}, Elapsed:{{elapsed}}";

        _stopwatch.Stop();
        _log.LogTrace(msgFormat, Cmd, Args, sourceLineInfo, _stopwatch.Elapsed);
    }

    #endregion private
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

