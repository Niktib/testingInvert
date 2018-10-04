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
    public partial class AnswerBox : Form
    {
        public AnswerBox()
        {
            InitializeComponent();
        }

        public void infoShown(List<string> allFoundInfo)
        {            
            textBox1.Text = String.Join(Environment.NewLine, allFoundInfo);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            textBox1.Text = "";
        }
    }
}
