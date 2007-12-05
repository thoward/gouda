using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;

using Gouda.Api.DisplayDevice;

namespace Gouda.Api
{
    /* Implemented gs32dll.dll export functions are:
     * 
     * External API
     * ---------------
     * gsapi_delete_instance
     * gsapi_exit
     * gsapi_init_with_args
     * gsapi_new_instance
     * gsapi_revision
     * gsapi_run_file
     * gsapi_run_string
     * gsapi_run_string_begin
     * gsapi_run_string_continue
     * gsapi_run_string_end
     * gsapi_run_string_with_length
     * gsapi_set_display_callback
     * gsapi_set_poll
     * gsapi_set_stdio
     * 
     * 
     * Non-Implemented gs32dll.dll export functions are:
     * ---------------
     *  
     * gsapi_set_visual_tracer - Documentation says that this shouldn't be built into release builds, 
     * so it may or may not be in the dll.
     * 
     * The following functions are exposed in the current DLL build, but according to the documentation they are "obsolete",
     * and will be removed in the future, so we will not include them in the wrapper.
     * 
     * Platform-independent DLL functions 
     * ----------------------------------
     * gsdll_revision
     * gsdll_init
     * gsdll_execute_begin
     * gsdll_execute_cont
     * gsdll_execute_end
     * gsdll_exit
     * 
     * gsdll_lock_device         
     * 
     * Platform Specific DLL Functions
     * -------------------------------
     * gsdll_copy_dib
     * gsdll_copy_palette
     * gsdll_draw
     * gsdll_get_bitmap_row
     * 
     */

    /// <summary>
    /// Includes the definitions for the Ghostscript C API.
    /// </summary>
    public static class Ghostscript
    {
        #region Constructor

        /// <summary>
        /// Static constructor.. 
        /// Looks up the Ghostscript DLL location in the registry, and inserts it into 
        /// the DLLImport search path. If the gsDLL is in the same directory as the
        /// wrapper DLL, it will load that one first...
        /// </summary>
        static Ghostscript()
        {
            string gsDllPath = string.Empty;

            // figure out the location of the ghostscript library. 
            gsDllPath = findGhostscriptDLLPathInRegistry();
            
            // debug, hardcoding path
            //gsdllPath = @"C:\Program Files\gs\gs8.60\bin\gsdll32.dll";
                        
            if (!string.IsNullOrEmpty(gsDllPath))
            {
                // use SetDllDirectory to put it in the DLLImport search path

                Win32.SetDllDirectory(Path.GetDirectoryName(gsDllPath));
            }
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Looks for the Ghostscript installation location in the registry.
        /// </summary>
        /// <returns></returns>
        private static string findGhostscriptDLLPathInRegistry()
        {
            string gsDllPath = string.Empty;

            // look for a GPL install first.
            RegistryKey gsKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\GPL Ghostscript");
            
            // if that's not there look AFPL.
            if (null == gsKey)
            {
                gsKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\AFPL Ghostscript");
            }
            
            if (null != gsKey)
            {
                string[] gsSubKeyNames = gsKey.GetSubKeyNames();

                // grab the most recent install 
                // (since we're looping through the subkey names.. Those names are the version numbers
                // however, they don't come back sorted if you have multiple installed versions.. 
                // they come back in the order you installed them. (i think)... So, in theory, 
                // the last assignment to gsDLLPath will be the most reently installed version. 
                foreach (string subkeyName in gsSubKeyNames)
                {
                    RegistryKey versionSubKey = gsKey.OpenSubKey(subkeyName);

                    object regKeyValue = versionSubKey.GetValue("GS_DLL", null);

                    if (null != regKeyValue)
                    {
                        string gsDLLPathInRegistry = Convert.ToString(regKeyValue);
                        if (File.Exists(gsDLLPathInRegistry))
                        {
                            
                            gsDllPath = Path.GetDirectoryName((string)regKeyValue);
                        }
                    }
                }
            }

            return gsDllPath;
        }
        #endregion 

        public static object syncroot = new object();

        #region Exported Ghostscript API Functions

        /// <summary>
        /// Get version numbers and strings.
        /// This is safe to call at any time.
        /// You should call this first to make sure that the correct version
        /// of the Ghostscript is being used.
        /// </summary>
        /// <param name="revisionInfo">A pointer to a revision structure.</param>
        /// <param name="length">The size of the revisionInfo structure in bytes.</param>
        /// <returns>
        /// Returns 0 if OK, or if length is too small (additional parameters
        /// have been added to the structure) it will return the required size of the structure. 
        /// </returns>
        [DllImport("gsdll32.dll", EntryPoint="gsapi_revision", CharSet=CharSet.Ansi)]
        public static extern GhostScriptApiErrorCode Revision(out GS_REVISION revisionInfo, Int32 length);

        /// <summary>
        /// Create a new instance of Ghostscript. 
        /// <para>
        /// WARNING:
        /// Ghostscript supports only one instance.
        /// The current implementation uses a global static instance 
        /// counter to make sure that only a single instance is used.
        /// If you try to create two instances, the second attempt
        /// will return less then 0 and set instance to NULL.
        /// </para>
        /// 
        /// <para>
        /// The instance pointer is passed to most other API functions.
        /// The caller_handle will be provided to callback functions.
        /// </para>
        /// 
        /// </summary>
        /// <param name="instance">An handle to the Ghostscript instance.</param>
        /// <param name="callerHandle">Unused... Just pass null.</param>
        /// <returns></returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_new_instance", CharSet = CharSet.Ansi)]
        public static extern int NewInstance(out IntPtr instance, IntPtr callerHandle);

        /// <summary>
        /// <para>
        /// 
        /// Destroy an instance of Ghostscript
        /// 
        /// </para>
        /// 
        /// Before you call this, Ghostscript must have finished.
        /// If Ghostscript has been initialised, you must call gsapi_exit() before gsapi_delete_instance.
        /// 
        /// <para><bold>
        /// 
        /// WARNING:
        ///  Ghostscript supports only one instance.
        ///  The current implementation uses a global static instance counter to make sure that only a single instance is used.
        /// 
        /// </bold></para>
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_delete_instance", CharSet = CharSet.Ansi)]
        public static extern void DeleteInstance(IntPtr instance);


        /// <summary>
        /// Set the callback functions for stdio. 
        /// <para>
        /// The stdin callback function should return the number of characters read, 0 for EOF, or -1 for error.
        /// The stdout and stderr callback functions should return the number of characters written.
        /// If a callback address is NULL, the real stdio will be used.
        /// </para>
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <param name="stdin">A StdIoCallBack delegate to a function that reads from standard input.</param>
        /// <param name="stdout">A StdIoCallBack delegate to a function that writes to standard output.</param>
        /// <param name="stderr">A StdIoCallBack delegate to a function that writes to standard error.</param>
        /// <returns>Nothing of interest.</returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_set_stdio", CharSet = CharSet.Ansi)]
        public static extern GhostScriptApiErrorCode SetStdio(IntPtr instance, StdIoCallBack stdin, StdIoCallBack stdout, StdIoCallBack stderr);

        /// <summary>
        /// Set the callback function for polling.
        /// <para>
        /// This is used for handling window events or cooperative multitasking. 
        /// </para>
        /// <para>
        /// This function will only be called if Ghostscript was compiled with CHECK_INTERRUPTS as described in gpcheck.h.
        /// The polling function should return 0 if all is well, and negative if it wants ghostscript to abort.
        /// </para>
        /// <para>The polling function must be fast.</para>
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <param name="pollFunction">A PollCallBack delegate to a function that checks to see if the Ghostscript process needs to be interrupted. This can also be used for things like a calling DoEvents, or other such things. This must be a fast function. The polling function should return 0 if all is well, and negative if it wants ghostscript to abort.</param>
        /// <returns></returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_set_poll", CharSet = CharSet.Ansi)]
        public static extern GhostScriptApiErrorCode SetPoll(IntPtr instance, PollCallBack pollFunction);

        /// <summary>
        /// Set the display device callback structure. (A structure containing a number of delegates to various display callback functions).
        /// <para>
        /// If the display device is used, this must be called after gsapi_new_instance() and before gsapi_init_with_args().
        /// </para>
        /// See gdevdisp.h for more details.
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <param name="callback">A DISPLAY_CALLBACK structure containing a number of delegates to various display callback functions</param>
        /// <returns></returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_set_display_callback", CharSet = CharSet.Ansi)]
        public static extern int SetDisplayCallback(IntPtr instance, ref DISPLAY_CALLBACK displayCallbacks);        

        /// <summary>
        /// Initialise the interpreter.
        /// <para>
        /// This calls gs_main_init_with_args() in imainarg.c
        /// 1. If quit or EOF occur during gsapi_init_with_args(), 
        /// the return value will be e_Quit.  This is not an error. 
        /// You must call gsapi_exit() and must not call any other
        /// gsapi_XXX functions.
        /// 2. If usage info should be displayed, the return value 
        /// will be e_Info which is not an error.  Do not call gsapi_exit().
        /// 3. Under normal conditions this returns 0.  You would then 
        /// call one or more gsapi_run_*() functions and then finish
        /// with gsapi_exit().
        /// </para>
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <param name="argumentCount">The count of elements in the argument array.</param>
        /// <param name="arguments">The array of arguments.</param>
        /// <returns></returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_init_with_args", CharSet = CharSet.Ansi)]
        public static extern int InitWithArgs(IntPtr instance, Int32 argumentCount, [In, Out] String[] arguments);

        /* 
         * The gsapi_run_* functions are like gs_main_run_* except
         * that the error_object is omitted.
         * 
         * If these functions return <= -100, either quit or a fatal
         * error has occured.  You then call gsapi_exit() next.
         * 
         * The only exception is gsapi_run_string_continue()
         * which will return e_NeedInput (-106) if all is well.
         * 
         * The address passed in pexit_code will be used to return the 
         * exit code for the interpreter in case of a quit or fatal error. 
         * The user_errors argument is normally set to zero to indicate 
         * that errors should be handled through the normal mechanisms 
         * within the interpreted code. If set to a negative value, the 
         * functions will return an error code directly to the caller, 
         * bypassing the interpreted language. The interpreted language's 
         * error handler is bypassed, regardless of user_errors parameter, 
         * for the e_interrupt generated when the polling callback returns 
         * a negative value. A positive user_errors is treated the same as 
         * zero.
         * 
         * There is a 64 KB length limit on any buffer submitted to a 
         * gsapi_run_* function for processing. If you have more than 
         * 65535 bytes of input then you must split it into smaller 
         * pieces and submit each in a separate gsapi_run_string_continue() 
         * call. 
         * 
         */


        /// <summary>
        /// Set up the interpretter for a suspendable execution session. This is the first part of a three stage .
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <param name="errors">User errors.</param>
        /// <param name="exitCode">The exit code from the interpretter. See GhostscriptExitCode enum for details.</param>
        /// <returns></returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_string_begin", CharSet = CharSet.Ansi)]
        public static extern GhostScriptApiErrorCode RunStringBegin(IntPtr instance, Int32 errors, out Int32 exitCode);

        /// <summary>
        /// Continue running a string with the option of suspending.
        /// 
        /// This is the second stage of the three stage string processing. To process a series of postscript commands, first call RunStringBegin to start the interpretter in suspendable mode. Then for each line of postscript code, call RunStringContinue with the code to run. After running the code, the interrepter will suspend itself until either the next RunStringContinue call, or RunStringEnd is called to indicate that you are finished sending it postscript code to interpret. If you send an empty string, it will consider this to be an EOF as well. 
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <param name="postscriptText">The postscript source code to execute. Empty string signals EOF. The string length may not be longer than 65535 bytes. (note: That means, if it's a unicode string (ie: 2 bytes wide) that the string length can't be longer than 32767)</param>
        /// <param name="length">The byte length of the string of data (note: if this is a unicode string (2 bytes per characters), use string.Length * 2 instead of string.Length).</param>
        /// <param name="errors">User errors.</param>
        /// <param name="exitCode">The exit code from the interpretter. See GhostscriptExitCode enum for details.</param>
        /// <returns>Returns GhostscriptErrors.NeedInput (-106) on success. Otherwise, if the return code is less than -100, it's considered a fatal error.</returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_string_continue", CharSet = CharSet.Ansi)]
        public static extern GhostScriptApiErrorCode RunStringContinue(IntPtr instance, String postscriptText, Int32 length, Int32 errors, out Int32 exitCode);

        /// <summary>
        /// The final stage of three stage continuous string processing. This function can be called at anytime after RunStringBegin has been called, to indicate that you are finished feeding the interpreter strings of postscript code to execute. 
        /// 
        /// Signals EOF to the suspended Ghostscript interpretter.
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <param name="errors">User errors.</param>
        /// <param name="exitCode">The exit code from the interpretter. See GhostscriptExitCode enum for details.</param>
        /// <returns></returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_string_end", CharSet = CharSet.Ansi)]
        public static extern GhostScriptApiErrorCode RunStringEnd(IntPtr instance, Int32 errors, out Int32 exitCode);

        /// <summary>
        /// Runs a string. This is basically an overload that calls RunStringBegin, RunStringContinue, and RunStringEnd for you. Use this is you only have a single line of code to execute, to save yourself the trouble of calling three functions...
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <param name="postscriptText">The postscript source code to execute. The string length may not be longer than 65535 bytes. (note: That means, if it's a unicode string (ie: 2 bytes wide) that the string length can't be longer than 32767)</param>
        /// <param name="errors">User errors.</param>
        /// <param name="exitCode">The exit code from the interpretter. See GhostscriptExitCode enum for details.</param>
        /// <returns></returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_string", CharSet = CharSet.Ansi)]
        public static extern IntPtr RunString(IntPtr instance, String postscriptText, Int32 errors, out Int32 exitCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <param name="postscriptText">The postscript source code to execute. The string length may not be longer than 65535 bytes. (note: That means, if it's a unicode string (ie: 2 bytes wide) that the string length can't be longer than 32767)</param>
        /// <param name="length">The byte length of the string of data (note: if this is a unicode string (2 bytes per characters), use string.Length * 2 instead of string.Length).</param>
        /// <param name="errors">User errors.</param>
        /// <param name="exitCode">The exit code from the interpretter. See GhostscriptExitCode enum for details.</param>
        /// <returns></returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_string_with_length", CharSet = CharSet.Ansi)]
        public static extern GhostScriptApiErrorCode RunStringWithLength(IntPtr instance, String postscriptText, Int32 length, Int32 errors, out Int32 exitCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <param name="filename">The filename of the postscript file to process. The filename may not be longer than 65535 bytes.</param>
        /// <param name="errors">User errors.</param>
        /// <param name="exitCode">The exit code from the interpretter. See GhostscriptExitCode enum for details.</param>
        /// <returns></returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_file", CharSet = CharSet.Ansi)]
        public static extern GhostScriptApiErrorCode RunFile(IntPtr instance, String filename, Int32 errors, out Int32 exitCode);


        /// <summary>
        /// Exit the interpreter.
        /// This must be called on shutdown if InitWithArgs has been called, 
        /// and just before DeleteInstance.
        /// </summary>
        /// <param name="instance">The Ghostscript instance handle.</param>
        /// <returns></returns>
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_exit", CharSet = CharSet.Ansi)]
        public static extern GhostScriptApiErrorCode Exit(IntPtr instance);   

        // according to the documentation this should not be included in release builds, 
        // only in debug, so we're not going to include it here. 

        //[DllImport("gsdll32.dll", EntryPoint = "gsapi_set_visual_tracer")]
        //public static extern GhostScriptApiErrorCode SetVisualTracer(IntPtr instance);   

        #endregion

    }
}
