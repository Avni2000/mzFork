using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Readers;
using Readers.ConsensusDataset;

namespace Test.FileReadingTests
{
    
    class TestHandler
    {

        [Test]
        public static void TestFindEXECMD()
        {
            string result = Handler.FindExePath("CMD.exe");
            Console.Write(result);
            Assert.That(result.Equals(@"C:\Program Files\MetaMorpheus\CMD.exe"));
        }

        [Test]
        public static void TestFindEXEMST()
        {
            Assert.That(Handler.FindExePath("MSPathFinderT.exe").Equals(@"C:\Program Files\Informed-Proteomics\MSPathFinderT.exe"));
        }

        //More tests to come for intersection, for cross checking files...

    }
}
