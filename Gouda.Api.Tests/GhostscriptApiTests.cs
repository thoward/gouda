using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Gouda.Api;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using Gouda.Api.DisplayDevice;

namespace Gouda.Api.Tests
{
    [TestFixture]
    public class GhostscriptApiTests
    {
        private string _testFilesDir;

        [TestFixtureSetUp]
        public void Init()
        {                              
            _testFilesDir = @"C:\dev\svn\GoudaProject\Gouda.Api.Tests\TestDocs";
        }


        [Test]
        public void Test_GetRevision()
        {
            GS_REVISION revision;                  
            Ghostscript.Revision(out revision, Marshal.SizeOf(typeof(GS_REVISION)));

            Assert.IsNotNull(revision);

            // on my machine I have GPL Ghostscript 8.60 installed. 
            // modify this test to suit your environment

            Assert.AreEqual(860, revision.Revision);
            Assert.AreEqual(20070801, revision.RevisionDate);
            Assert.AreEqual("GPL Ghostscript", revision.Product);
            Assert.AreEqual("Copyright (C) 2007 Artifex Software, Inc.  All rights reserved.", revision.Copyright);

            TestOutput.PrintRevision(revision);
        }

        [Test]
        public void TestDoSomeThings()
        {
            string testPsFile = Path.Combine(_testFilesDir, "test.ps");

            IntPtr instance = new IntPtr();
            lock (Ghostscript.syncroot)
            {
                try
                {
                    int result = Ghostscript.NewInstance(out instance, IntPtr.Zero);

                    Assert.AreEqual(0, result);

                    if (result == 0)
                    {
                        ConsoleStdioHandler consoleHandler = new ConsoleStdioHandler();

                        Ghostscript.SetStdio(instance, consoleHandler.StdInCallBack, consoleHandler.StdOutCallBack, consoleHandler.StdErrCallBack);
                        
                        string[] args = new string[] {
                            "-sDEVICE=display"                            
                        };

                        int init = Ghostscript.InitWithArgs(instance, args.Length, args);

                        Console.WriteLine("Init returns: " + init);

                        int exitCode = 0;

                        Ghostscript.RunFile(instance, testPsFile, 0, out exitCode);

                        Console.WriteLine("RunFile exits: " + exitCode);

                        Ghostscript.Exit(instance);                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw ex;
                }
                finally
                {
                    
                    Ghostscript.DeleteInstance(instance);
                }
            }
            
        }        
    }


}
