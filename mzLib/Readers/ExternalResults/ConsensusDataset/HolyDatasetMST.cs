using Readers.BaseClasses;
using System.Diagnostics;
using System.Data.SQLite;

namespace Readers.ConsensusDataset
{
    public class HolyDatasetMST
    {
        private string outPath;
        private string exePath;
        private string spectraPath;
        private string dataPath;

        /*
         * Returns output given by outPath, user Defined
         */
        public HolyDatasetMST(string exePath, string spectraPath, string dataPath,
            string outPath) //TODO given this has never ran to completion, more tests needed.
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
            string exeParams = "-s " + spectraPath + " -d " + dataPath + " -o " +
                               outPath +" -ic 2 -f 20 -MinLength 7 -MaxLength 1000000 -MinCharge 1 -MaxCharge 60 -MinFragCharge 1 -MaxFragCharge 10 -MinMass 0 -MaxMass 30000 -tda 1";
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
            process.WaitForExit();
        }

        /*
         * OutPath left empty, use for result handling purposes
         */
        public HolyDatasetMST(string exePath, string spectraPath, string dataPath) : this(exePath, spectraPath,
            dataPath,
            Path.GetDirectoryName(@"" + spectraPath)) //out path is the same as the directory of the spectraPath
        {
            outPath = Path.ChangeExtension(spectraPath, "_IcTda.tsv");
            string outMessage = outPath.ToString();
            //result handling
            if (!File.Exists(outPath))
            {
                throw new FileNotFoundException("Actual outPath:" + outMessage);
            }

            MsPathFinderTResultFile result =
                new MsPathFinderTResultFile(Path.ChangeExtension(spectraPath,
                    "_IcTda.tsv")); //suppose spectraPath.raw -> spectraPath_IcTda.tsv. TODO double check. Just a guess.
            var dict = FileToList(result);

        }
        public static List<IResult> FileToList(MsPathFinderTResultFile resultFile) => new List<IResult>(resultFile);
    }
}
