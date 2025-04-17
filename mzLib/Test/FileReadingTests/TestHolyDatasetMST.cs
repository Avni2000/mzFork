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
    class TestHolyDatasetMST
    {
        [Test]
        public static void TestMSTInit()
        {
            Handler handle = new Handler(@"C:\Users\avnib\Desktop\SEOutput\RAW\Ecoli_SEC4_F6.raw -t",
                @"C:\Users\avnib\Desktop\Databases\uniprotkb_proteome_UP000005640_2025_03_11.fasta\uniprotkb_proteome_UP000005640_2025_03_11.fasta");

        }
    }
}
