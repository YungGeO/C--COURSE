## Repo snapshot

- Single-console C# solution: `ConsoleAppPractise.sln` with one project at `ConsoleAppPractise/ConsoleAppPractise.csproj`.
- Target framework: net9.0 (see `TargetFramework` in the .csproj).
- Build output: `bin/Debug/net9.0/` (runtime/assets in `obj/Debug/net9.0/`).

## Quick dev commands (PowerShell)

- Build: `dotnet build "ConsoleAppPractise/ConsoleAppPractise.csproj" -c Debug`
- Run: `dotnet run --project "ConsoleAppPractise/ConsoleAppPractise.csproj" -c Debug`
- Clean: `dotnet clean "ConsoleAppPractise/ConsoleAppPractise.csproj"`

Notes: the workspace is a simple single-project solution. Use `dotnet` CLI for CI-like steps or open the solution in Visual Studio for the debugger.

## Project-specific patterns & gotchas

- Top-level program: `Program.cs` contains the entry point and is the primary place for change in this repo.
- Implicit usings and nullable are enabled in the project file (`ImplicitUsings` and `Nullable`) so generated global usings live under `obj/Debug/net9.0/ConsoleAppPractise.GlobalUsings.g.cs`.
- Example pattern to notice: short procedural code that manipulates arrays directly. Look for common off-by-one issues (e.g., iterating `for (int i = 0; i <= numbers.Length; i++)` will throw). Refer to `Program.cs` for a concrete example.

## What an AI code agent should do first

1. Open `ConsoleAppPractise/Program.cs` and `ConsoleAppPractise/ConsoleAppPractise.csproj` to confirm the target framework and runtime behavior.
2. Run the build commands above to detect compile/runtime errors before making edits.
3. When editing, prefer minimal, single-purpose patches (fix bug, add log, or refactor a small function). Run the build after each change.

## Examples (concrete, discoverable)

- Bug class: off-by-one indexing in `Program.cs` — the loop uses `<= numbers.Length` which accesses past the end of the array. Fix by changing to `i < numbers.Length` or iterate with `foreach`.
- Build target & runtime: if adding new SDK features, update `TargetFramework` in `ConsoleAppPractise.csproj` and ensure `dotnet` SDK on CI supports `net9.0`.

## Integration points & tests

- There are no external integrations or unit tests present. If you add tests, place them in a standard xUnit/NUnit/MSTest project and add it to the solution file.

## Quick checklist for PRs

- Build passes: `dotnet build` (Debug) — required.
- Small, focused changes with tests if behavior changes.
- Update README or add a short comment in `Program.cs` if you change app behavior or I/O.

If anything in this file is unclear or you want more specific examples (unit tests, CI steps, or debugging recipes), tell me what to expand and I will update this file.
