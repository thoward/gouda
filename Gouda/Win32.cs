using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Gouda.Api
{
    /// <summary>
    /// A class that holds win32 extern calls.
    /// </summary>
    public static class Win32
    {
        [DllImport("kernel32.dll", SetLastError=true, CharSet=CharSet.Ansi)]
        public static extern bool SetDllDirectory([MarshalAs(UnmanagedType.LPTStr)] string path);
    }
}
