using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Api.DisplayDevice
{
    /* Defines the delegates used by the display device callbacks. */

    /// <summary>
    /// New device has been opened This is the first event from this device. 
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="device"></param>
    /// <returns></returns>
    public delegate int DisplayOpenCallback(IntPtr handle, IntPtr device);

    /// <summary>
    /// Device is about to be closed.
    /// Device will not be closed until this function returns. 
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="device"></param>
    /// <returns></returns>
    public delegate int DisplayPreCloseCallback(IntPtr handle, IntPtr device);

    /// <summary>
    /// Device has been closed.
    /// This is the last event from this device.
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="device"></param>
    /// <returns></returns>
    public delegate int DisplayCloseCallback(IntPtr handle, IntPtr device);

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
    public delegate int DisplayPreSizeCallback(IntPtr handle, IntPtr device, Int32 width, Int32 height, Int32 raster, UInt32 format);

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
    public delegate int DisplaySizeCallback(IntPtr handle, IntPtr device, Int32 width, Int32 height, Int32 raster, UInt32 format, IntPtr pimage);

    /// <summary>
    /// This callback is fired on the postscript flushpage command.
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="device"></param>
    /// <returns></returns>
    public delegate int DisplaySyncCallback(IntPtr handle, IntPtr device);

    /// <summary>
    /// This callback is fired on the postscript showpage command.
    /// <para>If you want to pause on showpage, then don't return immediately</para>
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="device"></param>
    /// <param name="copies">?</param>
    /// <param name="flush">?</param>
    /// <returns></returns>
    public delegate int DisplayPageCallback(IntPtr handle, IntPtr device, Int32 copies, Int32 flush);



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
    public delegate int DisplayUpdateCallback(IntPtr handle, IntPtr device, Int32 x, Int32 y, Int32 w, Int32 h);

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
    public delegate IntPtr DisplayMemAllocCallback(IntPtr handle, IntPtr device, Int32 size);

    /// <summary>
    /// Free memory for bitmap. 
    /// If this is NULL, the Ghostscript memory device will free the bitmap 
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="device"></param>
    /// <param name="mem">Pointer to memory location to free.</param>
    /// <returns></returns>
    public delegate int DisplayMemFreeCallback(IntPtr handle, IntPtr device, IntPtr mem);


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
    public delegate int DisplaySeperationCallback(IntPtr handle, IntPtr device, Int32 component, String componentName, UInt16 c, UInt16 m, UInt16 y, UInt16 k);    
}
