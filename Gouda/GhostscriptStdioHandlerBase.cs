using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Api
{
    /// <summary>
    /// An abstract base class to handle Ghostscript Stdio callback calls.
    /// </summary>
    public abstract class GhostscriptStdioHandlerBase
    {
        public GhostscriptStdioHandlerBase()
        {
            Init();
        }

        private void Init()
        {
            _stdInCallback = new StdIoCallBack(StdInHandler);
            _stdOutCallback = new StdIoCallBack(StdOutHandler);
            _stdErrCallback = new StdIoCallBack(StdErrHandler);
        }

        private StdIoCallBack _stdInCallback;
        private StdIoCallBack _stdOutCallback;
        private StdIoCallBack _stdErrCallback;

        public StdIoCallBack StdInCallBack
        {
            get { return _stdInCallback; }
        }

        public StdIoCallBack StdOutCallBack
        {
            get { return _stdOutCallback; }
        }

        public StdIoCallBack StdErrCallBack
        {
            get { return _stdErrCallback; }
        }

        protected abstract int StdInHandler(IntPtr handle, IntPtr str, Int32 count);
        protected abstract int StdOutHandler(IntPtr handle, IntPtr str, Int32 count);
        protected abstract int StdErrHandler(IntPtr handle, IntPtr str, Int32 count);
    }
}
