using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Gouda.Api
{
    /// <summary>
    /// A struct that holds the ghostscript revision information returned from a call to GetRevision.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GS_REVISION
    {
        /// <summary>
        /// Product name
        /// </summary>
        public string Product;

        /// <summary>
        /// Copyright statement
        /// </summary>
        public string Copyright;

        /// <summary>
        /// Revision number (eg 806)
        /// </summary>
        public Int32 Revision;

        /// <summary>
        /// Revision date
        /// </summary>
        public Int32 RevisionDate;
    }
}
