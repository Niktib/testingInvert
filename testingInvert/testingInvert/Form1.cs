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
        List<double> timeTaken;
        searchingData dataSearcher = new searchingData();
        AnswerBox startUp;
        public Form1()
        {
            InitializeComponent();
            timeTaken = new List<double>();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startUp = new AnswerBox();
            startUp.Visible = true;
            DateTime timeStart = DateTime.Now;
            List<docInfoHolder> allFoundInfo = dataSearcher.findDocID(termBox.Text);
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
            timeTaken.Add((timeStart - DateTime.Now).TotalMinutes);
        }
    }
}
