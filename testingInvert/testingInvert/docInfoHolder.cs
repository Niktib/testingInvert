using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testingInvert
{
    class docInfoHolder
    {
        private string docID { get; set; }
        private string docTitle { get; set; }
        private string termFreq { get; set; }
        private string positions { get; set; }
        private string summary { get; set; }
        private string fullAbstract { get; set; }

        public docInfoHolder(string _docID, string _docTitle, string _termFreq,  string _positions, string _fullAbstract)
        {
            docID = _docID;
            docTitle = _docTitle;
            termFreq = _termFreq;
            positions = _positions;
            fullAbstract = _fullAbstract;
        }
        private void abstractToSummary()
        {
            //Needs to find the words 5 before and after each position (position is an array of integers divided by commas, or just a single integer)
        }

        public string printOut()
        {
            //Needs to return a string of all the relevant info. Divide multiple summaries with "..."
            return "";
        }
    }
}
