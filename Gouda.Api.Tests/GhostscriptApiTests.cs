using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Gouda.Api;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Gouda.Api.Tests
{
    [TestFixture]
    public class GhostscriptApiTests
    {
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

        
    }

    public class TestOutput
    {
        public static void PrintRevision(GS_REVISION revision)
        {
            printObject(revision);
        }

        private static void printObject(object obj)
        {
            Type objType = obj.GetType();
            MemberInfo[] mia = objType.GetMembers();

            foreach (MemberInfo mi in mia)
            {
                string output = string.Empty;

                switch (mi.MemberType)
                {
                    case MemberTypes.All:
                        break;
                    case MemberTypes.Constructor:
                        break;
                    case MemberTypes.Custom:
                        break;
                    case MemberTypes.Event:
                        break;
                    case MemberTypes.Field:
                        FieldInfo fi = objType.GetField(mi.Name);
                        output = fi.Name.PadRight(16, ' ') + ": " + fi.GetValue(obj).ToString();                    
                        break;
                    case MemberTypes.Method:
                        break;
                    case MemberTypes.NestedType:
                        break;
                    case MemberTypes.Property:
                        PropertyInfo pi = objType.GetProperty(mi.Name);
                        output = pi.Name.PadRight(16, ' ') + ": " + pi.GetValue(obj, null).ToString();                    
                        break;
                    case MemberTypes.TypeInfo:
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(output))
                {
                    Console.WriteLine(output);
                }
            }
        }

    }
}
