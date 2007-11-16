using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Api
{
    /// <summary>
    /// A delegate definition for the callback functions used by ghostscript to allow interaction with the stdio streams, stdin, stout, and stderr.
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="str"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public delegate IntPtr StdIoCallBack(IntPtr handle, IntPtr str, Int32 count);
}
