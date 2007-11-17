using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Api.DisplayDevice
{
    /// <summary>
    /// 
    /// </summary>
    public enum DISPLAY_FORMAT_COLOR
    {
        DISPLAY_COLORS_NATIVE = (1 << 0),
        DISPLAY_COLORS_GRAY = (1 << 1),
        DISPLAY_COLORS_RGB = (1 << 2),
        DISPLAY_COLORS_CMYK = (1 << 3),
        DISPLAY_COLORS_SEPARATION = (1 << 19)
    }
}
