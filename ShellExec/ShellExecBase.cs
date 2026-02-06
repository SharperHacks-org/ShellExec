// Copyright and trademark notices at the end of this file.

using Microsoft.Extensions.Logging;

using SharperHacks.CoreLibs.Constraints;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

// ToDo: Change this namespace!
namespace SharperHacks.CoreLibs.Miscellaneous;

public abstract class ShellExecBase
{
    // Get or set the args, if any, to run Cmd with.
    //
    public string Args { get; set; }

    // The command to execute.
    //
    public string Cmd { get; set; }

    // Get the effective command line to be executed.
    public string CmdLine => $"{Cmd} {Args}";

    // The ProcessStartInfo used to execute the command.
    //
    // Initialized by the constructors. Can be modified before calling
    // RunSync() or RunAsync().
    //
    public ProcessStartInfo ProcessStartInfo { get; set; }

    // The Process object used to execute the command.
    //
    // Initialized by the constructors. Can be modified before calling
    // any Run* methods.
    //
    public Process Process { get; set; }

    protected ShellExecBase(
        string cmd,
        string args,
        string? workingDir,
        bool useShellExecute,
        ILogger? logger)
    {
        Initialize(cmd, args, workingDir ?? string.Empty, useShellExecute, logger);
    }

    protected ShellExecBase(ProcessStartInfo psi, ILogger? logger)
    {
        Initialize(psi, logger);
    }

    protected ILogger? _log { get; private set; }
    protected Stopwatch _stopwatch { get; } = new();

    [MemberNotNull(
    nameof(Args),
    nameof(Cmd),
    nameof(Process),
    nameof(ProcessStartInfo)
    )]
    protected void Initialize(ProcessStartInfo psi, ILogger? logger)
    {
        _log = logger;

        Verify.IsNotNull(psi);

        Cmd = psi.FileName;
        Args = psi.Arguments;
        ProcessStartInfo = psi;

        Process = new Process
        {
            StartInfo = ProcessStartInfo
        };
    }

    [MemberNotNull(
    nameof(Args),
    nameof(Cmd),
    nameof(Process),
    nameof(ProcessStartInfo)
    )]
    protected void Initialize(
    string cmd,
    string args,
    string workingDir,
    bool useShellExecute,
    ILogger? logger = null)
    {
        _log = logger;

        Verify.IsNotNull(cmd);
        Verify.IsNotNull(args);

        Cmd = cmd;
        Args = args;

        var psi = new ProcessStartInfo(Cmd, Args)
        {
            Arguments = Args,
            CreateNoWindow = true, // Execute in background (no window).
            WorkingDirectory = workingDir,
            FileName = Cmd,
            RedirectStandardOutput = true, // Capture output.
            RedirectStandardError = true,
            RedirectStandardInput = true, // Allow input.
            UseShellExecute = useShellExecute, // No graphical shell.
        };

        ProcessStartInfo = psi;

        Process = new Process
        {
            StartInfo = ProcessStartInfo
        };
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
