using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Api.DisplayDevice;

namespace Gouda.Api.Tests
{
    public class TestDisplay : DisplayDeviceBase
    {
        public override int DisplayOpen(IntPtr handle, int device)
        {
            return 0;
        }

        public override int DisplayPreClose(IntPtr handle, IntPtr device)
        {
            return 0;
        }

        public override int DisplayClose(IntPtr handle, IntPtr device)
        {
            return 0;
        }

        public override int DisplayPreSize(IntPtr handle, IntPtr device, int width, int height, int raster, uint format)
        {
            return 0;
        }

        public override int DisplaySize(IntPtr handle, IntPtr device, int width, int height, int raster, uint format, IntPtr pimage)
        {
            return 0;
        }

        public override int DisplaySync(IntPtr handle, IntPtr device)
        {
            return 0;
        }

        public override int DisplayPage(IntPtr handle, IntPtr device, int copies, int flush)
        {
            return 0;
        }

        public override int DisplayUpdate(IntPtr handle, IntPtr device, int x, int y, int w, int h)
        {
            return 0;
        }

        public override IntPtr DisplayMemAlloc(IntPtr handle, IntPtr device, int size)
        {
            return IntPtr.Zero;
        }

        public override int DisplayMemFree(IntPtr handle, IntPtr device, IntPtr mem)
        {
            return 0;
        }

        public override int DisplaySeperation(IntPtr handle, IntPtr device, int component, string componentName, ushort c, ushort m, ushort y, ushort k)
        {
            return 0;
        }
    }
}
