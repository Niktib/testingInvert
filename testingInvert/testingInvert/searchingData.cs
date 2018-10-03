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
        private string[] docArray;
        private string[] dictionaryArray;
        private string[] postingArray;

        private int termID { get; set; }

        public searchingData()
        {
            string[] docArray = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\cacm.all").ToArray();
            string[] dictionaryArray = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\dictionary.txt").ToArray();
            string[] postingArray = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\postings").ToArray();

        }

        public string findDocID(string term)
        {
            for (int i = 0; i < dictionaryArray.Length; i++)
            {
                if (dictionaryArray[i].Split(' ')[0] == term.ToLower()) { termID = i; }
            }

            return "";
        }
        private List<docInfoHolder> grabDocumentInfo()
        {
            List<docInfoHolder> finalList = new List<docInfoHolder>();
            string[] infoArray = postingArray[termID].Split('|');
            string[] postBreakdown;
            int startOfDocument;
            foreach (string entry in infoArray)
            {
                postBreakdown = entry.Split('\t');
                startOfDocument = documentSearcher(postBreakdown[0]);
                finalList.Add(new docInfoHolder(postBreakdown[0], titleFinder(startOfDocument), postBreakdown[1], postBreakdown[2], abstractFinder(startOfDocument)));
            }
            return finalList;
        }
        private string titleFinder(int i)
        {
            Regex r = new Regex(".T");
            for (int j = i; j < docArray.Length; j++)
            {
                if (r.IsMatch(docArray[j])) return docArray[j + 1];
                if (docArray[j].Substring(0, 2) == ".I") break;
            }
            return "No Title Found";
        }
        private int documentSearcher(string docID)
        {
            Regex r = new Regex(".I " + docID);
            for (int i = 0; i < docArray.Length; i++) { if (r.IsMatch(docArray[i])) return i; }
            return 0;
        }
        private string abstractFinder(int i)
        {
            int j, k;
            string fullAbstract = "No Abstract Found";
            Regex r = new Regex(".W");
            for (j = i; j < docArray.Length; j++)
            {
                if (r.IsMatch(docArray[j])) break;
                if (docArray[j].Substring(0, 2) == ".I") return fullAbstract;
            }
            fullAbstract = "";
            r = new Regex("^.[ATBNXWKI]$");
            for (k = j; k < docArray.Length; k++)
            {
                if (r.IsMatch(docArray[k])) break;
                fullAbstract = fullAbstract + " " + docArray[k];
            }
            return fullAbstract;
        }
    }
}
