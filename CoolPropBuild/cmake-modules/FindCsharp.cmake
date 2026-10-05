# CoolProp only uses a C# compiler to build its own example tests. When cross-compiling the
# Linux library from Windows there is no Mono, so report a placeholder instead of failing.
set(CSHARP_TYPE "none")
set(CSHARP_VERSION "0")
set(CSHARP_COMPILER "csc-not-used")
set(CSHARP_INTERPRETER "")
set(Csharp_FOUND TRUE)
set(CSHARP_FOUND TRUE)
