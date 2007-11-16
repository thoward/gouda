using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Gouda.Api
{    
    /// <summary>
    /// Includes the definitions for the Ghostscript C API.
    /// </summary>
    public class GhostscriptExports
    {
        /* gs32dll.dll exports the following functions:
         * 
         * External API
         * ---------------
         * -gsapi_delete_instance
         * -gsapi_exit
         * -gsapi_init_with_args
         * -gsapi_new_instance
         * -gsapi_revision
         * -gsapi_run_file
         * gsapi_run_string
         * gsapi_run_string_begin
         * gsapi_run_string_continue
         * gsapi_run_string_end
         * gsapi_run_string_with_length
         * gsapi_set_display_callback
         * gsapi_set_poll
         * gsapi_set_stdio
         * gsapi_set_visual_tracer
         * 
         * 
         * Internal DLL functions (public)
         * --------------------------------
         * gsdll_copy_dib
         * gsdll_copy_palette
         * gsdll_draw
         * gsdll_execute_begin
         * gsdll_execute_cont
         * gsdll_execute_end
         * gsdll_exit
         * gsdll_get_bitmap_row
         * gsdll_init
         * gsdll_lock_device
         * gsdll_revision
         * 
         */
        
        [DllImport("gsdll32.dll", EntryPoint="gsapi_revision")]
        public static extern IntPtr Revision(GS_REVISION revisionInfo, Int32 length);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_new_instance")]
        public static extern IntPtr NewInstance(IntPtr instance, IntPtr callerHandle);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_set_stdio")]
        public static extern IntPtr SetStdio(IntPtr instance, StdIoCallBack stdin, StdIoCallBack stdout, StdIoCallBack stderr);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_delete_instance")]
        public static extern void DeleteInstance(IntPtr instance);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_init_with_args")]
        public static extern IntPtr InitWithArgs(IntPtr instance, Int32 argumentCount, IntPtr arguments);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_file")]
        public static extern IntPtr RunFile(IntPtr instance, String filename, Int32 errors, Int32 exitCode);
                
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_string")]
        public static extern IntPtr RunString(IntPtr instance, String postscriptText, Int32 errors, Int32 exitCode);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_string_with_length")]
        public static extern IntPtr RunStringWithLength(IntPtr instance, String postscriptText, Int32 length, Int32 errors, Int32 exitCode);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_string_begin")]
        public static extern IntPtr RunStringBegin(IntPtr instance, Int32 errors, Int32 exitCode);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_string_continue")]
        public static extern IntPtr RunStringContinue(IntPtr instance, String postscriptText, Int32 length, Int32 errors, Int32 exitCode);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_run_string_end")]
        public static extern IntPtr RunStringEnd(IntPtr instance, Int32 errors, Int32 exitCode);
                
        [DllImport("gsdll32.dll", EntryPoint = "gsapi_set_display_callback")]
        public static extern IntPtr SetDisplayCallback(IntPtr instance, DISPLAY_CALLBACK callback);


        [DllImport("gsdll32.dll", EntryPoint = "gsapi_set_poll")]
        public static extern IntPtr SetPoll(IntPtr instance, PollCallBack pollFunction);

        [DllImport("gsdll32.dll", EntryPoint = "gsapi_exit")]
        public static extern IntPtr Exit(IntPtr instance);   

        // according to the documentation this should not be included in release builds, 
        // only in debug, so we're not going to include it here. 

        //[DllImport("gsdll32.dll", EntryPoint = "gsapi_set_visual_tracer")]        
    }



    // misc note from ghostscript code...
    /*
     * Note that for Windows, the display callback functions are 
     * cdecl, not stdcall.  This differs from those in iapi.h.
     */
}
