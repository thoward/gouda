using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Api
{
    /// <summary>
    /// A call back for polling. 
    /// 
    /// If gsapi_set_poll is called, pointing to a delegate of this type, the 
    /// function will be called from ghostscript as a check to see if the external 
    /// environment wants the ghostscript instance to abort. 
    /// The polling function should return 0 if all is well, and negative if it 
    /// wants ghostscript to abort.
    /// 
    /// The polling function must be fast.
    /// </summary>
    /// <param name="handle">The caller's handle</param>
    /// <returns>The polling function should return 0 if all is well, and negative if it 
    /// wants ghostscript to abort.</returns>
    public delegate IntPtr PollCallBack(IntPtr handle);
}
