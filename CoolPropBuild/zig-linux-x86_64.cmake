# CMake toolchain for cross-compiling CoolProp to Linux x86_64 from Windows with Zig's clang.
# build-coolprop.sh sets ZIG_EXE and ZIG_TARGET (e.g. x86_64-linux-gnu.2.28, which picks the glibc version to link against).
# Zig links libc++ statically, so the library only depends on glibc.
set(CMAKE_SYSTEM_NAME Linux)
set(CMAKE_SYSTEM_PROCESSOR x86_64)

set(ZIG "$ENV{ZIG_EXE}")
set(ZIG_TARGET "$ENV{ZIG_TARGET}")
set(CMAKE_C_COMPILER "${ZIG}" cc -target ${ZIG_TARGET})
set(CMAKE_CXX_COMPILER "${ZIG}" c++ -target ${ZIG_TARGET})
set(CMAKE_AR "${CMAKE_CURRENT_LIST_DIR}/zig-ar.cmd" CACHE FILEPATH "" FORCE)
set(CMAKE_RANLIB "${CMAKE_CURRENT_LIST_DIR}/zig-ranlib.cmd" CACHE FILEPATH "" FORCE)

set(CMAKE_FIND_ROOT_PATH_MODE_PROGRAM NEVER)

# Export only the C# entry points, which also hides the static libc++ (Zig doesn't support --exclude-libs).
# Zig's linker honours only the last --version-script, so it goes in with the standard libraries: they come last
# on the link line, after CoolProp's own script (which only hides the JSON libraries, a subset of ours).
set(CMAKE_CXX_STANDARD_LIBRARIES_INIT "-Wl,--version-script=${CMAKE_CURRENT_LIST_DIR}/csharp-exports.map")
# Strip the debug info
set(CMAKE_MODULE_LINKER_FLAGS_INIT "-s")
set(CMAKE_SHARED_LINKER_FLAGS_INIT "-s")
