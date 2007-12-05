using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Api;
using System.IO;
using Gouda.Api.DisplayDevice;
using System.Runtime.InteropServices;

namespace GoudaTestAppCLI
{
    /// <summary>
    /// A simple example application.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string _testFilesDir = @"C:\dev\svn\GoudaProject\Gouda.Api.Tests\TestDocs";

            string testPsFile = Path.Combine(_testFilesDir, "test.ps");

            IntPtr instance = new IntPtr();

            lock (Ghostscript.syncroot)
            {
                try
                {
                    int result = Ghostscript.NewInstance(out instance, IntPtr.Zero);
                                        
                    if (result == 0)
                    {
                        ConsoleStdioHandler hand = new ConsoleStdioHandler();
                        
                        Ghostscript.SetStdio(instance, hand.StdInCallBack, hand.StdOutCallBack, hand.StdErrCallBack);
                        
                        Ghostscript.SetPoll(instance, new PollCallBack(Poll));

                        string[] gsArgs = new string[] {
                            "-sDEVICE=display"                            
                        };

                        int init = Ghostscript.InitWithArgs(instance, gsArgs.Length, gsArgs);

                        Console.WriteLine("Init returns: " + init);

                        int exitCode = 0;

                        Ghostscript.RunFile(instance, testPsFile, 0, out exitCode);

                        Console.WriteLine("RunFile exits: " + exitCode);

                        Ghostscript.Exit(instance);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);                    
                }
                finally
                {
                    Ghostscript.DeleteInstance(instance);
                }

                Console.ReadKey();
            }
        }
    

        public static int Poll(IntPtr handle)
        {
            return 0;
        }
    }
}
