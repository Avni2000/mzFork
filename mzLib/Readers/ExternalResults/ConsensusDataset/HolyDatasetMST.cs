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

        /*
         * OutPath left empty, use for result handling purposes
         */
        public HolyDatasetMST(string exePath, string spectraPath, string dataPath) : this(exePath, spectraPath,
            dataPath,
            Path.GetDirectoryName(@"" + spectraPath)) //out path is the same as the directory of the spectraPath
        {
            outPath = Path.ChangeExtension(spectraPath, "_IcTda.tsv");
            //result handling
            MsPathFinderTResultFile result =
                new MsPathFinderTResultFile(Path.ChangeExtension(spectraPath,
                    "_IcTda.tsv")); //suppose spectraPath.raw -> spectraPath_IcTda.tsv. TODO double check. Just a guess.
            var dict = FileToList(result);
            SQL(dict);

        }
        public List<IResult> FileToList(MsPathFinderTResultFile resultFile) => new List<IResult>(resultFile);

        public void SQL(List<IResult> IresultList)
        {
            var connection = new SQLiteConnection("Data Source=myDatabase.db;Version=3;");
            connection.Open();

            // Create table and insert data
            var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS People (Name TEXT, Age INT);", connection);
            command.ExecuteNonQuery();
        }
    }
}
