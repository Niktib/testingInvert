using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace testingInvert
{
    class searchingData
    {
        static private string[] docArray;
        static private string[] dictionaryArray;
        static private string[] postingArray;

        private int termID { get; set; }

        public searchingData()
        {
            docArray = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\titlesAndAbstracts.txt").ToArray();
            dictionaryArray = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\dictionary.txt").ToArray();
            postingArray = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\postings.txt").ToArray();

        }

        public List<docInfoHolder> findDocID(string term)
        {
            termID = -1;
            for (int i = 0; i < dictionaryArray.Length; i++)
            {
                if (dictionaryArray[i].Split(' ')[0] == term.ToLower())
                {
                    termID = i;
                }
            }
            if (termID == -1) { return null; }
            return grabDocumentInfo();
        }
        private List<docInfoHolder> grabDocumentInfo()
        {
            List<docInfoHolder> finalList = new List<docInfoHolder>();
            string[] infoArray = postingArray[termID].Split('|');
            string[] postingBreakdown, titleAndAbstractBreakdown;
            int docID;
            for(int i = 0; i <  infoArray.Length-1; i++)
            {
                postingBreakdown = infoArray[i].Split('\t');
                docID = Convert.ToInt32(postingBreakdown[0]) - 1;
                titleAndAbstractBreakdown = docArray[docID].Split('\t');
                finalList.Add(new docInfoHolder(postingBreakdown[0], titleAndAbstractBreakdown[1], postingBreakdown[1], postingBreakdown[2], titleAndAbstractBreakdown[2]));
            }
            return finalList;
        }
    }
}
