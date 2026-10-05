#!/usr/bin/env bash
# Builds CoolProp's SWIG C# native library for SharpFluids on Windows (run it from Git Bash).
#
#   win32   -> CoolProp32.dll    MSVC, needs Visual Studio 2026 with "Desktop development with C++"
#   win64   -> CoolProp64.dll    MSVC. SharpFluids ships CoolProp's official build; this reproduces it
#   linux64 -> libCoolProp64.so  cross-compiled with Zig's clang against glibc 2.28 and a static libc++
#
# Usage: ./build-coolprop.sh [--install] [win32] [win64] [linux64]     (default: win32 linux64)
#   --install  copies the results into ../SharpFluids
#
# SWIG, Python (needed by CoolProp's header generation) and Zig are downloaded and checked against the
# SHA-256 below. 7-Zip must be installed. The work folder defaults to %LOCALAPPDATA%\Temp\cpb; set
# COOLPROP_BUILD_DIR to change it, but keep it short: CoolProp's dependency checkouts break the
# 260 character path limit when the folder is nested deeply.
set -euo pipefail

COOLPROP_TAG=v8.0.0
COOLPROP_COMMIT=ae81610e7d23efc57f9d051c8e70a4d66e87537f
SWIG_VERSION=4.4.1
SWIG_SHA256=ce01474c81120eab381491d8d45cbcce4768fd1e5c23ffc7654b522702769598
PYTHON_VERSION=3.14.8
PYTHON_SHA256=ce85f674d9a63029f709cbff7a3da1c6bc5bfcfaefdd9999f98fa0290470c454
ZIG_VERSION=0.16.0
ZIG_SHA256=68659eb5f1e4eb1437a722f1dd889c5a322c9954607f5edcf337bc3684a75a7e
GLIBC_VERSION=2.28

HERE="$(cd "$(dirname "$0")" && pwd)"
WORK="${COOLPROP_BUILD_DIR:-$(cygpath -u "$LOCALAPPDATA")/Temp/cpb}"

INSTALL=0
TARGETS=()
for arg in "$@"; do
  case "$arg" in
    --install) INSTALL=1 ;;
    win32|win64|linux64) TARGETS+=("$arg") ;;
    *) echo "Unknown argument: $arg" >&2; exit 1 ;;
  esac
done
[ ${#TARGETS[@]} -gt 0 ] || TARGETS=(win32 linux64)

mkdir -p "$WORK/out"
cd "$WORK"

fetch() { # url file sha256
  if [ ! -f "$2" ]; then
    echo "Downloading $2"
    curl -sSfL --retry 5 --retry-all-errors -o "$2.part" "$1"
    mv "$2.part" "$2"
  fi
  if ! echo "$3 *$2" | sha256sum -c --quiet -; then
    rm -f "$2"
    echo "Checksum mismatch for $2 (deleted it, run again to download it again)" >&2
    exit 1
  fi
}

# --- Tools
SEVENZIP="$(command -v 7z || echo "/c/Program Files/7-Zip/7z.exe")"
[ -x "$SEVENZIP" ] || { echo "7-Zip not found" >&2; exit 1; }

VSWHERE="/c/Program Files (x86)/Microsoft Visual Studio/Installer/vswhere.exe"
vswhere_cpp() { "$VSWHERE" -latest -products '*' -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property "$1" | tr -d '\r'; }
[ -n "$(vswhere_cpp installationPath)" ] || { echo "No Visual Studio with the C++ workload found" >&2; exit 1; }
VS="$(cygpath -u "$(vswhere_cpp installationPath)")"
CMAKE="$VS/Common7/IDE/CommonExtensions/Microsoft/CMake/CMake/bin/cmake.exe"
NINJA="$VS/Common7/IDE/CommonExtensions/Microsoft/CMake/Ninja/ninja.exe"
case "$(vswhere_cpp installationVersion)" in
  18.*) VS_GENERATOR="Visual Studio 18 2026" ;;
  17.*) VS_GENERATOR="Visual Studio 17 2022" ;;
  *) echo "Unsupported Visual Studio version $(vswhere_cpp installationVersion)" >&2; exit 1 ;;
esac

fetch "https://sourceforge.net/projects/swig/files/swigwin/swigwin-$SWIG_VERSION/swigwin-$SWIG_VERSION.zip/download" "swigwin-$SWIG_VERSION.zip" "$SWIG_SHA256"
[ -d "swigwin-$SWIG_VERSION" ] || "$SEVENZIP" x -y "swigwin-$SWIG_VERSION.zip" > /dev/null
SWIG="$WORK/swigwin-$SWIG_VERSION/swig.exe"

fetch "https://www.nuget.org/api/v2/package/python/$PYTHON_VERSION" "python-$PYTHON_VERSION.nupkg" "$PYTHON_SHA256"
[ -d "python-$PYTHON_VERSION" ] || "$SEVENZIP" x -y -o"python-$PYTHON_VERSION" "python-$PYTHON_VERSION.nupkg" > /dev/null
PYTHON="$WORK/python-$PYTHON_VERSION/tools/python.exe"

# --- CoolProp source
if [ ! -d CoolProp ]; then
  git -c advice.detachedHead=false clone --depth 1 --branch "$COOLPROP_TAG" https://github.com/CoolProp/CoolProp.git CoolProp
fi
[ "$(git -C CoolProp rev-parse HEAD)" = "$COOLPROP_COMMIT" ] || { echo "CoolProp checkout is not $COOLPROP_TAG ($COOLPROP_COMMIT)" >&2; exit 1; }

# 7z is used by a CoolProp post-build step, Python by its header generation
export PATH="$(dirname "$SEVENZIP"):$(dirname "$PYTHON"):$PATH"
export CPM_SOURCE_CACHE="$WORK/cpm_cache"
# Some dependency checkouts exceed the path limit; enable long paths for git in this build only
export GIT_CONFIG_COUNT=1 GIT_CONFIG_KEY_0=core.longpaths GIT_CONFIG_VALUE_0=true

COMMON_OPTIONS=(-DCOOLPROP_CSHARP_MODULE=ON -DCMAKE_BUILD_TYPE=Release -DSWIG_EXECUTABLE="$SWIG" -DPython_EXECUTABLE="$PYTHON")

BUILT=()

build_msvc() { # platform installed-subfolder output-name
  "$CMAKE" -S CoolProp -B "build_$1" -G "$VS_GENERATOR" -A "$1" "${COMMON_OPTIONS[@]}" -DCOOLPROP_INSTALL_PREFIX="$WORK/install_$1"
  "$CMAKE" --build "build_$1" --target install --config Release -- -m
  cp "install_$1/Csharp/$2/CoolPropCsharp.dll" "out/$3"
  BUILT+=("$3")
}

build_linux64() {
  fetch "https://ziglang.org/download/$ZIG_VERSION/zig-x86_64-windows-$ZIG_VERSION.zip" "zig-$ZIG_VERSION.zip" "$ZIG_SHA256"
  [ -d "zig-x86_64-windows-$ZIG_VERSION" ] || "$SEVENZIP" x -y "zig-$ZIG_VERSION.zip" > /dev/null
  export ZIG_EXE="$(cygpath -w "$WORK/zig-x86_64-windows-$ZIG_VERSION/zig.exe")"
  export ZIG_TARGET="x86_64-linux-gnu.$GLIBC_VERSION"
  export ZIG_GLOBAL_CACHE_DIR="$WORK/zigcache" ZIG_LOCAL_CACHE_DIR="$WORK/zigcache"

  "$CMAKE" -S CoolProp -B build_linux64 -G Ninja -DCMAKE_MAKE_PROGRAM="$NINJA" \
    -DCMAKE_TOOLCHAIN_FILE="$HERE/zig-linux-x86_64.cmake" -DCMAKE_MODULE_PATH="$HERE/cmake-modules" \
    "${COMMON_OPTIONS[@]}" -DCOOLPROP_INSTALL_PREFIX="$WORK/install_linux64"
  "$CMAKE" --build build_linux64 --target install
  cp install_linux64/Csharp/Linux_64bit/libCoolPropCsharp.so out/libCoolProp64.so
  BUILT+=(libCoolProp64.so)
}

for target in "${TARGETS[@]}"; do
  case "$target" in
    win32) build_msvc Win32 Windows_32bit CoolProp32.dll ;;
    win64) build_msvc x64 Windows_64bit CoolProp64.dll ;;
    linux64) build_linux64 ;;
  esac
done

echo
echo "CoolProp $COOLPROP_TAG ($COOLPROP_COMMIT) built into $WORK/out:"
(cd out && sha256sum "${BUILT[@]}")

if [ $INSTALL = 1 ]; then
  (cd out && cp "${BUILT[@]}" "$HERE/../SharpFluids/")
  echo "Copied ${BUILT[*]} into $HERE/../SharpFluids"
fi
