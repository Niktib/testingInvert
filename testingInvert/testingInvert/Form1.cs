using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testingInvert
{
    public partial class Form1 : Form
    {
        List<double> secondsTaken;
        searchingData dataSearcher = new searchingData();
        AnswerBox startUp;

        public Form1()
        {
            InitializeComponent();
            secondsTaken = new List<double>();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            DateTime timeStart = DateTime.Now;
            List<docInfoHolder> allFoundInfo = dataSearcher.findDocID(termBox.Text);
            if (allFoundInfo != null)
            {
                startUp = new AnswerBox();
                startUp.Visible = true;
                List<string> allStringsFound = new List<string>();
                int i = 1;
                foreach (var foundDocument in allFoundInfo)
                {
                    string[] thisIsStupid = ("Document " + i++ + "\n" + foundDocument.printOut()).Split('\n');
                    foreach (string parts in thisIsStupid)
                    {
                        allStringsFound.Add(parts);
                    }
                }
                startUp.infoShown(allStringsFound);
                secondsTaken.Add((DateTime.Now - timeStart).TotalMilliseconds);
                MessageBox.Show(String.Format("It took {0} Milliseconds to find your documents", secondsTaken.LastOrDefault().ToString()));
            }
            else
            {
                MessageBox.Show("Could not find your exact term");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double TotalTime = 0;
            foreach (double i in secondsTaken)
            {
                TotalTime += i;
            }
            double averageTime = TotalTime / secondsTaken.Count;
            MessageBox.Show(String.Format("It took {0} Milliseconds on average to find your documents", averageTime.ToString()));
        }
    }
}
