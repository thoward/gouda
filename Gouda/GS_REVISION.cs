using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Gouda.Api
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GS_REVISION
    {
        public IntPtr Product;
        public IntPtr Copyright;
        public Int32 Revision;
        public Int32 RevisionDate;
    }
}
