using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testingInvert
{
    public partial class Form1 : Form
    {
        searchingData dataSearcher = new searchingData();
        AnswerBox startUp;
        static PorterStemming ps;

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {            
            ps = new PorterStemming();
            StopWords sw = new StopWords();
            List<string> termFixing = new List<string>();
            string[] StringArray = termBox.Text.ToLower().Replace(".", "").Replace(",", "").Replace("!", "").Replace("?", "").Replace("(", "").Replace(")", "").Replace("=", "").Replace('\n', ' ').Split(' ');
            foreach (string SingleTerms in StringArray)
            {
                if (!StopBox.Checked || (StopBox.Checked && !sw.StopMatching(SingleTerms)))
                {
                    if (checkBox1.Checked)
                    {
                        termFixing.Add(ps.StemWord(SingleTerms));
                    }
                    else if (SingleTerms != "")
                    {
                        termFixing.Add(SingleTerms);
                    }
                }
            }
            string terms = " " + String.Join(" ", termFixing.ToArray()) + " ";
            List<int> QueryVector = new List<int>();
            List<string> QueryTerms = new List<string>();
            termFixing.Sort();
            while (termFixing.Count > 0)
            { 
                QueryVector.Add(Regex.Matches(terms, String.Format(" {0} ", termFixing[0])).Count);
                QueryTerms.Add(termFixing[0]);
                for (int i = 0; i < QueryVector.LastOrDefault(); i++) { termFixing.Remove(termFixing[0]); }
            }

            SortedDictionary<double, List<int>> FinalNumbers = dataSearcher.FindFinalVectors(QueryVector.ToArray(), QueryTerms.ToArray());
            terms = "";
            startUp = new AnswerBox();
            startUp.Visible = true;
            List<string> allStringsFound = new List<string>();
            var items = from pair in FinalNumbers
                        orderby pair.Key descending
                        select pair;
            
            foreach (KeyValuePair<double, List<int>> pair in items)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
            allStringsFound.Add("Documents in Relevancy Order: \n");
            allStringsFound.Add("Doc \t\t\t Relevancy Score \n");
            foreach (KeyValuePair<double, List<int>> pair in items)
            {
                allStringsFound.Add(String.Format("{0}:\t\t {1}\n", String.Join(", ", pair.Value.ToArray()), pair.Key));
            }
            startUp.infoShown(allStringsFound);
        }
    }
}
