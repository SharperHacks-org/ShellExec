![SharperHacks logo](https://raw.githubusercontent.com/SharperHacks-org/Assets/main/Images/SHLLC-Logo.png)
# SharperHacks.CoreLibs.Miscellaneous
## ShellExec

A shell execution wrapper w/console capture.

Licensed under the Apache License, Version 2.0. See [LICENSE](LICENSE).

Contact: joseph@sharperhacks.org

Project URL: https://github.com/SharperHacks-org/Miscellaneous

Nuget: https://www.nuget.org/packages/SharperHacks.CoreLibs.ShellExec

### Targets
- net8.0
- net9.0
- net10.0

### Builder's notes

* Unit tests depend on [TestDummy](https://github.com/SharperHacks-org/TestDummy),
  and its dependencies, being on the path or in the execution directory, at runtime.

### Classes

#### ShellExec
Thin wrapper class for running separate processes, synchronously, and capturing their output.

