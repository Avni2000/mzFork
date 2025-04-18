using System;
using System.Collections.Generic;
using System.IO;
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
            Handler handle = new Handler(@"C:\Users\avnib\Desktop\SEOutput\RAW\Ecoli_SEC4_F6.raw",
                @"C:\Users\avnib\Desktop\Databases\uniprotkb_proteome_UP000005640_2025_03_11.fasta\uniprotkb_proteome_UP000005640_2025_03_11.fasta");

        }

        [Test]
        public static void TestMSTList()
        {

            //output @ "C:\Users\avnib\Desktop\Task1SearchTask\AllPSMs.psmtsv"
            //     var result = new PsmFromTsvFile(@"C:\Users\avnib\Desktop\Task1SearchTask\AllPSMs.psmtsv");
            //   var iList = HolyDatasetMM.FileToList(result);
            MsPathFinderTResultFile result =
                new MsPathFinderTResultFile(Path.ChangeExtension(@"C:\\Users\\avnib\\Desktop\\SEOutput\\RAW\\Ecoli_SEC4_F6.raw\"
                    ,"_IcTda.tsv")); //suppose spectraPath.raw -> spectraPath_IcTda.tsv. TODO double check. Just a guess.
            var iList = HolyDatasetMST.FileToList(result);
            Assert.That(iList.Count.Equals(1076));
                Assert.That(iList[0].FullSequence.Equals("MATPPPERYFVLSNGTLVVWSPGWNKRLKRGGQAIKTMNLLGSKEVEQPDLPEPTGTDSRFGKAQPQLVDNNRKLTQTGEMSGPNTESRSRRPPKWSKCGTLSNIYFCRGSNPDLHQEWADLRQLLPCAPGPPPSRPCRRLDVLNCYVPPES"));
                Assert.That(iList[1075].FullSequence.Equals("MRIGSMSSQASVSYTTAVVPTGNNASYGQRLTEFYSAQVPKHYQLVKIADEIKFWERKRGINVSDEWDELVETVILVYVYTRHKREQNEFIGVLRGLTGKVGAEECVERVAAVSPEEEPEMGGGPVIWRDPHRSSSVLLVEEESESRFCLCAARKKYGDGDYTRTQNSKLKM"));
            
        }
    }
}
