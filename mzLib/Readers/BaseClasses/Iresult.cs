using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readers.BaseClasses
{
    public interface Iresult
    {    /// <summary>
        /// The scan number of the identification
        /// </summary>
        public int OneBasedScanNumber { get; }
        /// <summary>
        /// Primary Sequence
        /// </summary>
        public string BaseSequence { get; }
        /// <summary>
        /// Modified Sequence in MetaMorpheus format
        /// </summary>
        public string FullSequence { get; }
        /// <summary>
        /// The accession (unique identifier) of the identification
        /// </summary>
        public string Accession { get; }
        /// <summary>
        /// If the given Spectral Match is a decoy
        /// </summary>
        public bool IsDecoy { get; }

        public int Charge { get; }

        public double Mass { get; }

        public string Modifications { get; }
    }
}
