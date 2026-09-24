# UDP

VB.NET VS 2008 UDP send/receive library plus a LocalMessenger LAN chat. The UDP library exposes Client, Server, and a TraceListener that forwards System.Diagnostics.Trace over UDP port 8080. LocalMessenger is a WinForms chat that uses that library on the same port.

**Source last updated:** 2010-02-21 · **Language:** VB.NET · **Target:** .NET Framework 3.5 · **Output:** class library + WinForms exe

## Solution structure

| Project | Language | Type | Purpose |
|---------|----------|------|---------|
| `UDP` | VB.NET | Class library | UDP receive (`Client`), send (`Server`), and `TraceListener` that prefixes `My.Computer.Name` and sends on port 8080 |
| `LocalMessenger` | VB.NET | WinForms exe | LAN chat using `UDP.Client` + `UDP.Server` on port 8080, with ignore-self checkbox (`AssemblyTitle` Messenger) |

Sibling Historical Dev repo [Tracing](https://github.com/VaderConsulting/Tracing) references `UDP.vbproj` and uses `UDP.TraceListener`.

## How to open

Open `UDP.sln` in Visual Studio 2008 (solution format 10.00). Both projects target .NET Framework 3.5.

## Requirements

- Visual Studio 2008, .NET Framework 3.5

## Attribution and provenance

Working copy from my Historical Dev folder `UDP`. `AssemblyCompany` / `AssemblyCopyright` are Microsoft 2010, the Visual Studio 2008 project template default, not Microsoft-owned sample code.

## License

MIT © 2026 VaderConsulting. See `LICENSE`.
