using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Gouda.Api
{
    public class Win32
    {
        [DllImport("kernel32.dll")]
        public static extern bool SetDllDirectory(string path);
    }
}
