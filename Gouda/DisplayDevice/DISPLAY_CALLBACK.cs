using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Gouda.Api.DisplayDevice
{

    /*
     * Note that for Windows, the display callback functions are 
     * cdecl, not stdcall.  This differs from those in iapi.h.
     */

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DISPLAY_CALLBACK
    {
        #region Struct Informational Members
        /// <summary>
        /// Size of this structure. Used for checking if we have been handed a valid structure.
        /// </summary>
        public Int32 Size;

        /// <summary>
        /// Major version of this structure. 
        /// The major version number will change if this structure changes.
        /// </summary>
        public Int32 VersionMajor;
        
        /// <summary>
        /// Minor version of this structure. 
        /// The minor version number will change if new features are added
        /// without changes to this structure.  For example, a new color
        /// format.
        /// </summary>
        public Int32 VersionMinor;
        #endregion

        #region Delegates to Display CallBack Functions

        /// <summary>
        /// New device has been opened This is the first event from this device. 
        /// </summary>
        public DisplayOpenCallback DisplayOpen;

        /// <summary>
        /// Device is about to be closed.
        /// Device will not be closed until this function returns. 
        /// </summary>
        public DisplayPreCloseCallback DisplayPreClose;

        /// <summary>
        /// Device has been closed.
        /// This is the last event from this device.
        /// </summary>
        public DisplayCloseCallback DisplayClose;

        /// <summary>
        /// Device is about to be resized.
        /// Resize will only occur if this function returns 0.
        /// </summary>
        public DisplayPreSizeCallback DisplayPreSize;

        /// <summary>
        /// This callback is fired when the device has been resized. 
        /// </summary>
        public DisplaySizeCallback DisplaySize;

        /// <summary>
        /// This callback is fired on the postscript flushpage command.
        /// </summary>
        public DisplaySyncCallback DisplaySync;

        /// <summary>
        /// This callback is fired on the postscript showpage command.
        /// <para>If you want to pause on showpage, then don't return immediately</para>
        /// </summary>
        public DisplayPageCallback DisplayPage;

        /// <summary>
        /// Notify the caller whenever a portion of the raster is updated.
        /// <para>
        /// This can be used for cooperative multitasking or for progressive update of the display.
        /// This function pointer may be set to NULL if not required.
        /// </para>
        /// </summary>
        public DisplayUpdateCallback DisplayUpdate;

        /// <summary>
        /// Allocate memory for bitmap
        /// <para>
        /// This is provided in case you need to create memory in a special
        /// way, e.g. shared.  If this is NULL, the Ghostscript memory device 
        /// allocates the bitmap. This will only be called to allocate the
        /// image buffer. The first row will be placed at the address 
        /// returned by display_memalloc.
        /// </para>
        /// </summary>
        public DisplayMemAllocCallback DisplayMemAlloc;

        /// <summary>
        /// Free memory for bitmap. 
        /// If this is NULL, the Ghostscript memory device will free the bitmap 
        /// </summary>
        public DisplayMemFreeCallback DisplayMemFree;


        /// <summary>
        /// Added in V2.
        /// <para> 
        /// When using separation color space (DISPLAY_COLORS_SEPARATION),
        /// give a mapping for one separation component.
        /// This is called for each new component found.
        /// It may be called multiple times for each component.
        /// It may be called at any time between display_size 
        /// and display_close.
        /// The client uses this to map from the separations to CMYK
        /// and hence to RGB for display.
        /// GS must only use this callback if version_major >= 2.
        /// The unsigned short c,m,y,k values are 65535 = 1.0.
        /// </para>
        /// This function pointer may be set to NULL if not required.
        /// </summary>
        public DisplaySeperationCallback DisplaySeperation;        
        #endregion 
    }


}



