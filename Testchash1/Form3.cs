using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GeneticPictureMaker
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
          
        }

        public System.Windows.Forms.PictureBox pictureBoxResultQ()
        {
            return pictureBoxResult;
        }
 
        public System.Windows.Forms.PictureBox pictureBoxTemplateQ()
        {
            return pictureBoxTemplate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }
        public void updateTime(TimeSpan time)
        {
            lblTime.Text = time.ToString();
        }

        private void Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
