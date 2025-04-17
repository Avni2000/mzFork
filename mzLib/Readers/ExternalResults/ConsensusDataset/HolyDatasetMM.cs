using CsvHelper;
using Easy.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Readers.BaseClasses;

namespace Readers.ConsensusDataset
{
    public class HolyDatasetMM
    {
        private bool init = false;
        private bool ended = false;
        private string outPath;

        /// <summary>
        /// Usually unused constructor that can return output to a user defined path if need be
        /// </summary>
        /// <param name="exePath"></param>
        /// <param name="spectraPath"></param>
        /// <param name="dataPath"></param>
        /// <param name="outPath"></param>
        public HolyDatasetMM(string exePath, string spectraPath, string dataPath,
            string outPath) 
        {
            //initialize to class variables


            this.outPath = outPath;
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.Parent.FullName;
            string settingsPath = Path.Combine(projectDirectory, "Readers", "ExternalResults", "ConsensusDataset", "settings.toml");
          //  exePath = @""+ exePath;
          //  spectraPath = @"" + spectraPath;
     //       dataPath = @"" + dataPath;
      //      outPath = @"" + outPath;
            Console.WriteLine(settingsPath);

            if (!File.Exists(settingsPath))
            {
                throw new FileNotFoundException($"settings.toml not found at: {settingsPath}");
            }
            string exeParams = $"-d " + dataPath + " -o " +  outPath + " -s " + spectraPath + " -t " + settingsPath;
            var process = new Process
            {
                StartInfo =
                {
                    FileName = exePath,
                    Arguments = exeParams,
                    WorkingDirectory = Path.GetDirectoryName(exePath),
                    UseShellExecute = true,        
                    CreateNoWindow = false, 
                    WindowStyle = ProcessWindowStyle.Normal 
                }
            };

            process.Start();
            process.WaitForExit(300000);
            if(process.HasExited) ended = true;
            if (process.ExitCode != 0)
            {
                throw new Exception($"MetaMorpheus failed with exit code: {process.ExitCode}");
            }
        }

        /// <summary>
        /// Constructor that fulfillls the MM requirements of the Consensus dataset project.
        /// </summary>
        /// <param name="exePath"></param>
        /// <param name="spectraPath"></param>
        /// <param name="dataPath"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public HolyDatasetMM(string exePath, string spectraPath, string dataPath) : this(exePath, spectraPath,
            dataPath,
            Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(spectraPath))) ??
            throw new ArgumentNullException(nameof(spectraPath)))
        //out path is the same as the directory of the spectraPath
        // {
        //     if !(File.Exists(@"C:\Users\avnib\Desktop\Task1SearchTask\AllPSMs.psmtsv"))
        //     {
        //         WaitForChangedResult(File.Exists(@"C:\Users\avnib\Desktop\Task1SearchTask\AllPSMs.psmtsv"));
        //     }
        //     
        //
        //
        //     var outX = Path.GetDirectoryName(Path.GetDirectoryName(spectraPath) ??
        //                                      throw new ArgumentNullException("why is spectra path null..."));
        //         var result = new PsmFromTsvFile(Path.ChangeExtension(Path.GetDirectoryName(outX),
        //                                             @"\Task1SearchTask\AllPSMs.psmtsv") ??
        //                                         throw new ArgumentNullException(
        //                                             "Not detected")); //all psms or all prot?
        //
        //     FileToList(result);
        //     
        // }
        {

            string expectedFile = Path.Combine(outPath, "Task1SearchTask", "ALLPSMs.psmtsv");

            WaitForFile(expectedFile, 3000000); //maybe check how much that is lol

            var result = new PsmFromTsvFile(expectedFile);
            var iList = FileToList(result);
            for (int i = 0; i < iList.Count; i++)
            {
                Console.WriteLine(iList[i].BaseSequence);
            }
        }

        /*
         * Given: \Desktop as output, the output file is named "C:\Users\avnib\Desktop\Task1SearchTask\AllPSMs.psmtsv"
         * As such, +\Task1SearchTask\AllPSMs.psmtsv -> TODO I need to find a way to delete the Task1SearchTask folder at some point, it has the potential to "hide" lots of data.
         *
         */


        private static void WaitForFile(string fullPath, int timeoutMilliseconds)
        {
            var sw = Stopwatch.StartNew();
            while (!File.Exists(fullPath))
            {
                if (sw.ElapsedMilliseconds > timeoutMilliseconds)
                    throw new TimeoutException($"Timed out waiting for file to appear: {fullPath}");
                Thread.Sleep(500);
            }
        }

        public static List<IResult> FileToList(PsmFromTsvFile resultFile) => new List<IResult> { resultFile };


       // public void SQL(List<>)
    }
}
