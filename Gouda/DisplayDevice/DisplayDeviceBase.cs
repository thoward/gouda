using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Gouda.Api.DisplayDevice
{
    /// <summary>
    /// A base class for building a custom display device for use with the Ghostscript API.
    /// </summary>
    public abstract class DisplayDeviceBase
    {
        public DisplayDeviceBase()
        {
            setupDelegates();
            buildCallBackStruct();
        }

        /// <summary>
        /// A DISPLAY_CALLBACK struct for this class to pass to the SetDisplayDevice API call.
        /// </summary>
        protected DISPLAY_CALLBACK _callbackStruct;

        /// <summary>
        /// Gets a DISPLAY_CALLBACK struct for this class to pass to the SetDisplayDevice API call.
        /// </summary>
        public DISPLAY_CALLBACK CallbackStruct
        {
            get
            {
                return _callbackStruct;
            }
        }

        /// <summary>
        /// Major version of this structure. 
        /// The major version number will change if this structure changes.
        /// </summary>
        public int MajorVersion
        {
            get { return 2; }
        }

        /// <summary>
        /// Minor version of this structure. 
        /// The minor version number will change if new features are added
        /// without changes to this structure.  For example, a new color
        /// format.
        /// </summary>
        public int MinorVersion
        {
            get { return 0; }
        }

        private DisplayOpenCallback _displayOpen;
        private DisplayPreCloseCallback _displayPreClose;
        private DisplayCloseCallback _displayClose;
        private DisplayPreSizeCallback _displayPreSize;
        private DisplaySizeCallback _displaySize;
        private DisplaySyncCallback _displaySync;
        private DisplayPageCallback _displayPage;
        private DisplayUpdateCallback _displayUpdate;
        private DisplayMemAllocCallback _displayMemAlloc;
        private DisplayMemFreeCallback _displayMemFree;
        private DisplaySeperationCallback _displaySeperation;

        private void setupDelegates()
        {
            _displayOpen = new DisplayOpenCallback(DisplayOpen);
            _displayPreClose = new DisplayPreCloseCallback(DisplayPreClose);
            _displayClose = new DisplayCloseCallback(DisplayClose);
            _displayPreSize = new DisplayPreSizeCallback(DisplayPreSize);
            _displaySize = new DisplaySizeCallback(DisplaySize);
            _displaySync = new DisplaySyncCallback(DisplaySync);
            _displayPage = new DisplayPageCallback(DisplayPage);
            _displayUpdate = new DisplayUpdateCallback(DisplayUpdate);
            _displayMemAlloc = new DisplayMemAllocCallback(DisplayMemAlloc);
            _displayMemFree = new DisplayMemFreeCallback(DisplayMemFree);
            _displaySeperation = new DisplaySeperationCallback(DisplaySeperation);
        }


        /// <summary>
        /// Build a DISPLAY_CALLBACK struct for this class to pass to the SetDisplayDevice API call. 
        /// </summary>
        /// <returns></returns>
        //public DISPLAY_CALLBACK buildCallBackStruct()
        public void buildCallBackStruct()
        {
            _callbackStruct = new DISPLAY_CALLBACK();

            // get version information
            _callbackStruct.VersionMajor = this.MajorVersion;
            _callbackStruct.VersionMinor = this.MinorVersion;            
            
            // setup delegates 
            _callbackStruct.DisplayOpen = new DisplayOpenCallback(DisplayOpen);

            _callbackStruct.DisplayPreClose = new DisplayPreCloseCallback(DisplayPreClose);
            _callbackStruct.DisplayClose = new DisplayCloseCallback(DisplayClose);

            _callbackStruct.DisplayPreSize = new DisplayPreSizeCallback(DisplayPreSize);
            _callbackStruct.DisplaySize = new DisplaySizeCallback(DisplaySize);
            _callbackStruct.DisplaySync = new DisplaySyncCallback(DisplaySync);
            _callbackStruct.DisplayPage = new DisplayPageCallback(DisplayPage);
            _callbackStruct.DisplayUpdate = null;// new DisplayUpdateCallback(DisplayUpdate);
            _callbackStruct.DisplayMemAlloc = null; // new DisplayMemAllocCallback(DisplayMemAlloc);
            _callbackStruct.DisplayMemFree = null; // new DisplayMemFreeCallback(DisplayMemFree);
            _callbackStruct.DisplaySeperation = null;// new DisplaySeperationCallback(DisplaySeperation);

             // calculate size
            _callbackStruct.Size = 0;
            _callbackStruct.Size = Marshal.SizeOf(_callbackStruct); 
        }

        /// <summary>
        /// New device has been opened This is the first event from this device. 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        public abstract int DisplayOpen(IntPtr handle, int device);

        /// <summary>
        /// Device is about to be closed.
        /// Device will not be closed until this function returns. 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        public abstract int DisplayPreClose(IntPtr handle, IntPtr device);

        /// <summary>
        /// Device has been closed.
        /// This is the last event from this device.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        public abstract int DisplayClose(IntPtr handle, IntPtr device);

        /// <summary>
        /// Device is about to be resized.
        /// 
        /// Resize will only occur if this function returns 0.
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <param name="width">Width of device page.</param>
        /// <param name="height">Height of device page.</param>
        /// <param name="raster">The byte count of a single row.</param>
        /// <param name="format">Color format. ?</param>
        /// <returns></returns>
        public abstract int DisplayPreSize(IntPtr handle, IntPtr device, Int32 width, Int32 height, Int32 raster, UInt32 format);

        /// <summary>
        /// This callback is fired when the device has been resized. 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <param name="width">Width of device page.</param>
        /// <param name="height">Height of device page.</param>
        /// <param name="raster">The byte count of a single row.</param>
        /// <param name="format">Color format. ?</param>
        /// <param name="pimage">New pointer to raster returned in pimage.</param>
        /// <returns></returns>
        public abstract int DisplaySize(IntPtr handle, IntPtr device, Int32 width, Int32 height, Int32 raster, UInt32 format, IntPtr pimage);

        /// <summary>
        /// This callback is fired on the postscript flushpage command.
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        public abstract int DisplaySync(IntPtr handle, IntPtr device);

        /// <summary>
        /// This callback is fired on the postscript showpage command.
        /// <para>If you want to pause on showpage, then don't return immediately</para>
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <param name="copies">?</param>
        /// <param name="flush">?</param>
        /// <returns></returns>
        public abstract int DisplayPage(IntPtr handle, IntPtr device, Int32 copies, Int32 flush);


        /// <summary>
        /// Notify the caller whenever a portion of the raster is updated.
        /// <para>
        /// This can be used for cooperative multitasking or for progressive update of the display.
        /// This function pointer may be set to NULL if not required.
        /// </para>
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <param name="x">The x coordinate of the top left corner of update rectangle.</param>
        /// <param name="y">The y coordinate of the top left corner of update rectangle.</param>
        /// <param name="w">The width in pixels of the update rectangle.</param>
        /// <param name="h">The height in pixels of the update rectangle.</param>
        /// <returns></returns>
        public abstract int DisplayUpdate(IntPtr handle, IntPtr device, Int32 x, Int32 y, Int32 w, Int32 h);

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
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public abstract IntPtr DisplayMemAlloc(IntPtr handle, IntPtr device, Int32 size);

        /// <summary>
        /// Free memory for bitmap. 
        /// If this is NULL, the Ghostscript memory device will free the bitmap 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <param name="mem">Pointer to memory location to free.</param>
        /// <returns></returns>
        public abstract int DisplayMemFree(IntPtr handle, IntPtr device, IntPtr mem);


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
        /// <param name="handle"></param>
        /// <param name="device"></param>
        /// <param name="component">The color seperation component.</param>
        /// <param name="componentName">The name of the color seperation component.</param>
        /// <param name="c">Cyan value. 65535 = 1.0</param>
        /// <param name="m">Magenta value. 65535 = 1.0</param>
        /// <param name="y">Yellow value. 65535 = 1.0</param>
        /// <param name="k">Key (Black) value. 65535 = 1.0</param>
        /// <returns></returns>
        public abstract int DisplaySeperation(IntPtr handle, IntPtr device, Int32 component, String componentName, UInt16 c, UInt16 m, UInt16 y, UInt16 k);
    }
}
