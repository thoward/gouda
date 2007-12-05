using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Gouda.Api
{
    /// <summary>
    /// A simple stdio handler for interation with the console. 
    /// </summary>
    public class ConsoleStdioHandler : GhostscriptStdioHandlerBase
    {
        protected override int StdInHandler(IntPtr handle, IntPtr str, int count)
        {            
            char[] buffer = new char[count];

            Console.In.ReadBlock(buffer, 0, count);

            str = Marshal.StringToHGlobalAnsi(new string(buffer));
            
            return buffer.Length;
        }

        protected override int StdOutHandler(IntPtr handle, IntPtr str, int count)
        {
            string foo = Marshal.PtrToStringAnsi(str, count);
            Console.Out.Write(foo);
            return count;
        }

        protected override int StdErrHandler(IntPtr handle, IntPtr str, int count)
        {
            string foo = Marshal.PtrToStringAnsi(str, count);
            Console.Error.Write(foo);
            return count;
        }
    }
}
