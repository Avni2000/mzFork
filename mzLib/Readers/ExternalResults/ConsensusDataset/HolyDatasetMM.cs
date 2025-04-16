using CsvHelper;
using Easy.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Readers.BaseClasses;

namespace Readers.ConsensusDataset
{
    public class HolyDatasetMM
    {
        private string outPath;
        private string exePath;
        private string spectraPath;
        private string dataPath;

        /// <summary>
        /// Usually unused constructor that can return output to a user defined path if need be
        /// </summary>
        /// <param name="exePath"></param>
        /// <param name="spectraPath"></param>
        /// <param name="dataPath"></param>
        /// <param name="outPath"></param>
        public HolyDatasetMM(string exePath, string spectraPath, string dataPath,
            string outPath) //TODO TEST
        {
            //initialize to class variables
            this.exePath = exePath;
            this.spectraPath = spectraPath;
            this.dataPath = dataPath;
            this.outPath = outPath;

            exePath = @"" + exePath;
            spectraPath = @"" + spectraPath;
            dataPath = @"" + dataPath;
            outPath = @"" + outPath;
            string exeParams = " -s " + "\"" + spectraPath + "\"" + " -d " + "\"" + dataPath + "\"" + " -o " + "\"" +
                               outPath + "\"" +
                               " -ic 2 -f 20 -MinLength 7 -MaxLength 1000000 -MinCharge 1 -MaxCharge 60 -MinFragCharge 1 -MaxFragCharge 10 -MinMass 0 -MaxMass 30000 -tda 1"; //100000
            var process = new Process
            {
                StartInfo =
                {
                    FileName = exePath,
                    WorkingDirectory = Path.GetDirectoryName(exePath),
                    Arguments = exeParams,
                }
            };

            process.Start();
            process.WaitForExit();
        }
        /// <summary>
        /// Constructor that fulfillls the MM requirements of the Consensus dataset project.
        /// </summary>
        /// <param name="exePath"></param>
        /// <param name="spectraPath"></param>
        /// <param name="dataPath"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public HolyDatasetMM(string exePath, string spectraPath, string dataPath) : this(exePath, spectraPath,
            dataPath, Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(spectraPath))) ?? throw new ArgumentNullException(nameof(spectraPath))) 
        //out path is the same as the directory of the spectraPath
        {
            outPath = Path.GetDirectoryName(spectraPath) ?? throw new ArgumentNullException("why is spectra path null..."); 
            MsPathFinderTResultFile result = new MsPathFinderTResultFile(Path.ChangeExtension(Path.GetDirectoryName(spectraPath), @"\Task1SearchTask\AllPSMs.psmtsv"));  //all psms or all prot?
            var dict = FileToList(result);

            /*
             * Given: \Desktop as output, the output file is named "C:\Users\avnib\Desktop\Task1SearchTask\AllPSMs.psmtsv"
             * As such, +\Task1SearchTask\AllPSMs.psmtsv -> TODO I need to find a way to delete the Task1SearchTask folder at some point, it has the potential to "hide" lots of data. 
             */
        }

        public List<IResult> FileToList(MsPathFinderTResultFile resultFile) => new List<IResult> { resultFile };

    }
}
