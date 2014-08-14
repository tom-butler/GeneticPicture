using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GeneticPictureMaker
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public void LoadText(string stop, string gen, string loop)
        {
            tbxStopDir.Text = stop;
            tbxGenDir.Text = gen;
            tbxLoopDir.Text = loop;
        }
        private void SetSaveDir(object sender, EventArgs e)
        {
            Button clicked = (Button) sender;
            string name = clicked.Name;
            string result = Form1.form1.FileBrowser(name);
            if (name == "btnMasterDir")
            {
                tbxMasterDir.Text = result;
                tbxStopDir.Text = result + "Stop" + @"\";
                tbxGenDir.Text = result + "Gen" + @"\";
                tbxLoopDir.Text = result + "Loop" + @"\";
            }
            if (name == "btnStopDir")
            {
                tbxStopDir.Text = result;
            }
            if (name == "btnGenDir")
            {
                tbxGenDir.Text = result;
            }
            if (name == "btnLoopDir")
            {
                tbxLoopDir.Text = result;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = Form1.form1.SetDirectories(tbxStopDir.Text, tbxGenDir.Text, tbxLoopDir.Text);
            if (result)
            {
                this.Close();
            }
        }

        private void openHelp(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            Form2 form2 = new Form2(clicked.Name);

            form2.ShowDialog();
        }

    }
}
