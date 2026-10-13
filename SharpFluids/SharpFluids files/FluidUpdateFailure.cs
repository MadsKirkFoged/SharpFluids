using Serilog;
using System;
using System.Diagnostics;
using System.Text;

namespace SharpFluids
{
    /// <summary>
    /// What a <see cref="Fluid"/> does when CoolProp cannot do an Update (fx UpdatePT on 0 Pa and 0 K). See <see cref="Fluid.OnUpdateFailure"/>
    /// </summary>
    public enum UpdateFailureMode
    {
        /// <summary>Log the failure (with the stack trace of the caller) and set <see cref="Fluid.FailState"/> - the fluid keeps its old values</summary>
        Log,

        /// <summary>Set <see cref="Fluid.FailState"/> and throw a <see cref="FluidUpdateException"/> - stops on the exact line that did the update</summary>
        Throw,
    }

    /// <summary>
    /// Thrown by the Update* methods when <see cref="Fluid.OnUpdateFailure"/> is <see cref="UpdateFailureMode.Throw"/> and CoolProp could not do the update.
    /// <see cref="Exception.InnerException"/> is the CoolProp error.
    /// </summary>
    public class FluidUpdateException : Exception
    {
        /// <summary>The Update method that failed, fx "UpdatePT"</summary>
        public string Method { get; }

        /// <summary>The inputs given to the Update method, fx "0 Pa and 0 K"</summary>
        public string Inputs { get; }

        /// <summary>The fluid the update was done on, fx "Ammonia"</summary>
        public string? FluidName { get; }

        public FluidUpdateException(string method, string inputs, string? fluidName, string message, Exception inner) : base(message, inner)
        {
            Method = method;
            Inputs = inputs;
            FluidName = fluidName;
        }
    }

    public partial class Fluid
    {
        /// <summary>
        /// What all <see cref="Fluid"/>s do when CoolProp cannot do an Update (fx UpdatePT on 0 Pa and 0 K)<br></br>
        /// <see cref="UpdateFailureMode.Log"/> (default): the failure is logged with the stack trace of the caller and <see cref="FailState"/> is set - good for a solver that
        /// is expected to guess outside the fluid limits now and then<br></br>
        /// <see cref="UpdateFailureMode.Throw"/>: a <see cref="FluidUpdateException"/> is thrown instead - good for finding out where a failing update is called from
        /// </summary>
        public static UpdateFailureMode OnUpdateFailure { get; set; } = UpdateFailureMode.Log;

        /// <summary>
        /// The one place a failed Update ends up: sets <see cref="FailState"/> and then logs or throws depending on <see cref="OnUpdateFailure"/>.
        /// <paramref name="unexpected"/> is for errors that are not the normal CoolProp "out of range" (<see cref="ApplicationException"/>) - they are logged as errors
        /// </summary>
        private void ReportUpdateFailure(string methodName, string argsDescription, Exception e, bool unexpected)
        {
            FailState = true;

            string message = unexpected
                ? $"SharpFluid -> {methodName} -> Report this on https://github.com/MadsKirkFoged/SharpFluids -  CoolProp returned unexpected result! {argsDescription}"
                : $"SharpFluid -> {methodName} -> CoolProp could not return your request on {argsDescription}";

            if (OnUpdateFailure == UpdateFailureMode.Throw)
                throw new FluidUpdateException(methodName, argsDescription, Media?.InternalName, $"{message} ({Media?.InternalName}): {e.Message}", e);

            //The exception only knows the frames from CoolProp up to here - the frames that called the Update are what tells you where it happened
            message = $"{message} and returns the followering error: {e}{Environment.NewLine}Called from:{Environment.NewLine}{CallerStackTrace()}";

            if (unexpected)
                Log.Error(message);
            else
                Log.Warning(message);
        }

        /// <summary>
        /// The current stack trace without the frames inside SharpFluids - only what called the Update is left. Only taken when an update failed, so it costs nothing otherwise
        /// </summary>
        private static string CallerStackTrace()
        {
            var sharpFluids = typeof(Fluid).Assembly;
            var result = new StringBuilder();

            foreach (StackFrame frame in new StackTrace(fNeedFileInfo: true).GetFrames() ?? Array.Empty<StackFrame>())
            {
                var method = frame.GetMethod();

                if (method is null || method.DeclaringType?.Assembly == sharpFluids)
                    continue;

                result.Append("   at ").Append(method.DeclaringType?.FullName).Append('.').Append(method.Name);

                if (frame.GetFileName() is string file)
                    result.Append(" in ").Append(file).Append(":line ").Append(frame.GetFileLineNumber());

                result.AppendLine();
            }

            return result.ToString();
        }
    }
}
