using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Readers;
using Readers.BaseClasses;
using Readers.ConsensusDataset;

namespace Test.FileReadingTests
{
    class TestHolyDatasetMM
    {
        [Test]
        public static void HolyDatasetMMInit()
        {
            //test run w/o exception
            try
            {
                Handler handle = new Handler(@"C:\Users\avnib\Desktop\SEOutput\RAW\Ecoli_SEC4_F6.raw",
                    @"C:\Users\avnib\Desktop\Databases\uniprotkb_proteome_UP000005640_2025_03_11.fasta\uniprotkb_proteome_UP000005640_2025_03_11.fasta");
            }
            catch(Exception e)
            { Console.WriteLine(e); }
        }

        [Test]
        public static void HolyDatasetMMOutput()
        {
            //output @ "C:\Users\avnib\Desktop\Task1SearchTask\AllPSMs.psmtsv"
            var result = new PsmFromTsvFile(@"C:\Users\avnib\Desktop\Task1SearchTask\AllPSMs.psmtsv");
            var iList = HolyDatasetMM.FileToList(result);
            Assert.That(iList.Count.Equals(1076));
            Assert.That(iList[0].FullSequence.Equals("MATPPPERYFVLSNGTLVVWSPGWNKRLKRGGQAIKTMNLLGSKEVEQPDLPEPTGTDSRFGKAQPQLVDNNRKLTQTGEMSGPNTESRSRRPPKWSKCGTLSNIYFCRGSNPDLHQEWADLRQLLPCAPGPPPSRPCRRLDVLNCYVPPES"));
            Assert.That(iList[1075].FullSequence.Equals("MRIGSMSSQASVSYTTAVVPTGNNASYGQRLTEFYSAQVPKHYQLVKIADEIKFWERKRGINVSDEWDELVETVILVYVYTRHKREQNEFIGVLRGLTGKVGAEECVERVAAVSPEEEPEMGGGPVIWRDPHRSSSVLLVEEESESRFCLCAARKKYGDGDYTRTQNSKLKM"));
        }
    }
}
