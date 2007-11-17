using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Api
{
    /// <summary>
    /// This is the subset of Ghostscript errors that can be returned by API calls. 
    /// </summary>
    public enum GhostScriptApiErrorCode
    {
        /// <summary>
        /// No errors
        /// </summary>
        Okay = GhostscriptErrorCode.Okay,

        /// <summary>
        /// "quit" has been executed. This is not an error. gsapi_exit() must be called next.
        /// </summary>
        Quit = GhostscriptErrorCode.Quit,

        /// <summary>
        /// The polling callback function returned a negative value, requesting Ghostscript to abort.
        /// </summary>
        Interrupt = GhostscriptErrorCode.Interrupt,

        /// <summary>
        /// More input is needed by gsapi_run_string_continue(). This is not an error.
        /// </summary>
        NeedInput = GhostscriptErrorCode.NeedInput,

        /// <summary>
        /// "gs -h" has been executed. This is not an error. gsapi_exit() must be called next.
        /// </summary>
        Info = GhostscriptErrorCode.Info,
        
        /// <summary>
        /// Error 
        /// </summary>
        Unknown = GhostscriptErrorCode.Unknown,

        /// <summary>
        /// Fatal error. gs_api_exit() must be called next.
        /// </summary>
        Fatal = GhostscriptErrorCode.Fatal
    }
}
