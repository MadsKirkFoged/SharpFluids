# CoolProp native libraries

SharpFluids calls CoolProp through CoolProp's SWIG C# wrapper. The native libraries live in `../SharpFluids` and are packed into the NuGet package's `build/` folder, where `SharpFluids.targets` copies them next to the consumer's binaries.

| File | Platform | CoolProp | Where it comes from |
|---|---|---|---|
| `CoolProp64.dll` | Windows x64 | 8.0.0 | CoolProp's official build, [`8.0.0/Csharp/Windows_64bit/CoolPropCsharp.dll`](https://sourceforge.net/projects/coolprop/files/CoolProp/8.0.0/Csharp/Windows_64bit/), renamed |
| `CoolProp32.dll` | Windows x86 | 8.0.0 | `build-coolprop.sh win32` (CoolProp doesn't publish an x86 C# build) |
| `libCoolProp64.so` | Linux x64 | 8.0.0 | `build-coolprop.sh linux64` |

All of them are CoolProp `v8.0.0` (commit `ae81610e7d23efc57f9d051c8e70a4d66e87537f`).

## Building

You need Windows with Git for Windows (the script runs in Git Bash), 7-Zip, and Visual Studio 2026 or 2022 with the *Desktop development with C++* workload; the script uses its MSVC, CMake and Ninja. The script downloads SWIG 4.4.1, Python 3.14.8 (needed by CoolProp's header generation) and Zig 0.16.0 (the Linux cross-compiler) and checks their SHA-256.

```bash
./build-coolprop.sh                           # win32 and linux64
./build-coolprop.sh --install win32 linux64   # and copy the results into ../SharpFluids
./build-coolprop.sh win64                     # reproduces CoolProp's official x64 build
```

Each target takes roughly 15-20 minutes. The work folder defaults to `%LOCALAPPDATA%\Temp\cpb` (set `COOLPROP_BUILD_DIR` to move it); keep it short, because CoolProp's dependency checkouts break Windows' 260 character path limit when it is nested deeply.

## How the libraries are built

- **Windows**: CoolProp's own CMake build with `-DCOOLPROP_CSHARP_MODULE=ON` and `-A Win32` or `-A x64`, the same as CoolProp's CI (which only builds x64). MSVC links the C runtime statically, so the DLLs only import `KERNEL32.dll`. The x86 DLL exports stdcall-decorated names (`_CSharp_xxx@N`), which .NET resolves for the `DllImport`s in `CoolPropPINVOKE.cs`. Built this way, the x64 DLL gives bit-identical results to the official one.
- **Linux**: cross-compiled on Windows with Zig's clang (`zig-linux-x86_64.cmake`) against glibc 2.28, with libc++ linked statically. The library only depends on libc, libm, libdl and libpthread and needs glibc 2.18 or newer (CoolProp's own 8.0.0 Linux build needs glibc 2.38 and the libstdc++ of GCC 13). `csharp-exports.map` exports only the C# entry points, so the static libc++ stays private to the library. `cmake-modules/FindCsharp.cmake` replaces CoolProp's search for Mono, which CoolProp only uses for its own examples.

## Checking a build

Run the tests with the new libraries in `../SharpFluids`:

```bash
dotnet test UnitTest5/UnitTest5.csproj -f net10.0
```

```bash
dotnet test UnitTest5/UnitTest5.csproj -f net470 --arch x86
```

For the Linux library, run the first command on Linux (or WSL).

SharpFluids passes its `Parameters` and `input_pairs` enums (`../SharpFluids/CoolPropFiles/parameters.cs` and `input_pairs.cs`) to CoolProp as plain integers, and CoolProp renumbers these enums between versions. They are therefore CoolProp's SWIG-generated enums for the shipped version, and `CoolPropEnumTests` fails if any member doesn't match the loaded library.

## Upgrading CoolProp

1. Change `COOLPROP_TAG` and `COOLPROP_COMMIT` in `build-coolprop.sh`, build `win32` and `linux64`, and download CoolProp's official x64 build.
2. Replace `parameters.cs` and `input_pairs.cs` with the ones in the build's `platform-independent.7z` (the enum there is called `parameters`; keep the name `Parameters`), and update the version in `CoolPropEnumTests.LoadsCoolProp8`.
3. Run the tests. Expect property values to move between CoolProp versions; compare failing tests against CoolProp's changelog before updating their expected values.
