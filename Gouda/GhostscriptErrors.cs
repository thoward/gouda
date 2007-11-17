using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Api
{
    /// <summary>
    /// A list of possible Ghostscript error codes.
    /// </summary>
    public enum GhostscriptErrorCode
    {
        /// <summary>
        /// Indicates success, or at least, no error.. ;)
        /// </summary>
        Okay = 0,

        #region Postscript Level 1 Errors
        /// <summary>
        /// Unknown error 
        /// </summary>
        Unknown = -1, 
        
        /// <summary>
        /// Ghostscript Operation Interrupted by polling callback.
        /// </summary>
        Interrupt = -6,
        
        /// <summary>
        /// Invalid access.
        /// </summary>
        InvalidAccess = -7,
        
        /// <summary>
        /// Invalid file access
        /// </summary>
        InvalidFileAccess = -9,
        
        /// <summary>
        /// Invalid Font
        /// </summary>
        InvalidFont = -10,
        
        /// <summary>
        /// IO Error
        /// </summary>
        IOError = -12,
        
        /// <summary>
        /// Limit check failed.
        /// </summary>
        LimitCheck = -13,
        
        /// <summary>
        /// Postscript no current point error.
        /// </summary>
        NoCurrentPoint = -14,

        /// <summary>
        /// Postscript range check error.
        /// </summary>
        RangeCheck = -15,
        
        /// <summary>
        /// Postscript type check error.
        /// </summary>
        TypeCheck = -20,
        
        /// <summary>
        /// Postscript undefined error.
        /// </summary>
        Undefined = -21,
        
        /// <summary>
        /// Postscript Undefined Filename error.
        /// </summary>
        UndefinedFilename = -22,
        
        /// <summary>
        /// Postscript undefined result error.
        /// </summary>
        UndefinedResult = -23,
        
        /// <summary>
        /// Ghostscript Virtual Machine error. 
        /// </summary>
        VMerror = -25,

        #endregion 
        
        #region Additional Level 2 and DPS errors

        /// <summary>
        /// Postscript Level 2 Error: configuration error
        /// </summary>
        ConfigurationError = -26,
        
        /// <summary>
        /// Postscript Level 2 Error: invalid context
        /// </summary>
        InvalidContext = -27,
        
        
        /// <summary>
        /// <para>Postscript Level 2 Error: undefined resource</para>
        /// 
        /// <para>Note: In the Ghostscript source code, there are two files, 
        /// ierrors.h and gserrors.h which define the error codes used. In 
        /// gserrors.h, -28 is defined as Unregistered (with nothing defined 
        /// as -29), however in ierrors.h, -28 is Undefined Resource, and -29 
        /// is Unregistered. I am unclear as to the context for these errors, 
        /// or which is most appropriate. I'm following ierrors.h, as it seems 
        /// to be more high-level, and is probably the correct error for the 
        /// API return value... -thoward</para>
        /// </summary>
        UndefinedResource = -28,
        
        /// <summary>
        /// Postscript Level 2 Error: unregistered
        /// 
        /// <para>Note: In the Ghostscript source code, there are two files, 
        /// ierrors.h and gserrors.h which define the error codes used. In 
        /// gserrors.h, -28 is defined as Unregistered (with nothing defined 
        /// as -29), however in ierrors.h, -28 is Undefined Resource, and -29 
        /// is Unregistered. I am unclear as to the context for these errors, 
        /// or which is most appropriate. I'm following ierrors.h, as it seems 
        /// to be more high-level, and is probably the correct error for the 
        /// API return value... -thoward</para>
        /// </summary>
        Unregistered = -29,


        /// <summary>
        /// Postscript Level 2 Error: either invalidid or last (invalidid is for the NeXT DPS extension).
        /// </summary>
        LastOrInvalidId = -30,

        #endregion

        /// <summary>
        /// Hit Detected error.
        /// </summary>
        HitDetected = -99,

        #region Fatal Errors
        
        /*
         * Anything over -100 (other than NeedInput (-106) is considered a fatal error 
         * 
         */        

        /// <summary>
        /// Fatal error.
        /// </summary>
        Fatal = -100,

        /// <summary>
        /// Internal code for the postscript .quit operator.
        /// The real quit code is an integer on the operand stack.
        /// gs_interpret returns this only for a .quit with a zero exit code.
        /// </summary>
        Quit = -101,

        /// <summary>
        /// Internal code for a normal exit from the interpreter.
        /// Do not use outside of interp.c.
        /// </summary>
        InterpreterExit = -102,
        

        /// <summary>
        /// Internal code that indicates that a procedure has been stored in the
        /// remap_proc of the graphics state, and should be called before retrying 
        /// the current token.  This is used for color remapping involving a call
        /// back into the interpreter -- inelegant, but effective.
        /// </summary>
        RemapColor = -103,
        
        /// <summary>
        /// Internal code to indicate we have underflowed the top block of the e-stack.
        /// </summary>
        ExecStackUnderflow = -104,

        /// <summary>
        /// Internal code for the vmreclaim operator with a positive operand. We need 
        /// to handle this as an error because otherwise the interpreter won't reload 
        /// enough of its state when the operator returns.
        /// </summary>
        VMReclaim = -105,

        /// <summary>
        /// Internal code for requesting more input from run_string.
        /// </summary>
        NeedInput = -106,

        /// <summary>
        /// Internal code for stdin callout.
        /// </summary>
        NeedStdin = -107,
        
        /// <summary>
        /// Internal code for stdout callout.
        /// </summary>
        NeedStdout = -108,

        /// <summary>
        /// Internal code for stderr callout.
        /// </summary>
        NeedStderr = -109,

        /// <summary>
        /// Internal code for a normal exit when usage info is displayed. 
        /// This allows Window versions of Ghostscript to pause until the
        /// message can be read.
        /// </summary>
        Info = -110

        #endregion 
    }
}
