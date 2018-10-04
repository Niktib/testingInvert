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
        private int[] positions { get; set; }
        private string positionsButAString { get; set; }
        private string summary { get; set; }
        private string[] fullAbstract { get; set; }

        public docInfoHolder(string _docID, string _docTitle, string _termFreq,  string _positions, string _fullAbstract)
        {
            docID = _docID;
            docTitle = _docTitle;
            termFreq = _termFreq;
            positionsButAString = _positions;
            if (_positions.Contains(','))
            {
                string[] positionsArray = _positions.Split(',');
                positions = new int[positionsArray.Length];
                for (int i = 0; i < positionsArray.Length; i++)
                {
                    positions[i] = Convert.ToInt32(positionsArray[i])-1;
                }
            }
            else
            {
                positions = new int[1];
                positions[0] = Convert.ToInt32(_positions)-1;
            }
            fullAbstract = _fullAbstract.Split(' ');
            abstractToSummary();
        }
        private void abstractToSummary()
        {
            summary = "";
            foreach (int wordLocation in positions)
            {
                int startReadingHere = wordLocation - 5;
                if (startReadingHere + 10 > fullAbstract.Length) { startReadingHere = fullAbstract.Length - 10; }
                if (startReadingHere < 0) { startReadingHere = 0; }
                if (startReadingHere != 0) { summary = summary + "..."; }
                for (int i = startReadingHere; i < fullAbstract.Length; i++)
                {
                    summary = summary + " " + fullAbstract[i];
                    if (i == startReadingHere + 10) { break; }
                }
            }
            
        }

        public string printOut()
        {
            return String.Format("Document ID: {0}\nDocument Title: {1}\nTerm Frequency: {2}\nPosition(s) in Document: {3}\nSummary in Document: {4}\n",docID,docTitle,termFreq,positionsButAString,summary);
        }
    }
}
