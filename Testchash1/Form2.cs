using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace GeneticPictureMaker
{
    public partial class Form2 : Form
    {

        
        string marker = "";
        
        public Form2()
        {
            InitializeComponent();
            marker = "";
        }               

        public Form2( string s)
        {
            InitializeComponent();
            marker = s;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string helptext = "";
            string fname = Util.findFile("HelpText1.txt");
            //no marker show all help
            if (marker == "")
            {
                string helptextAll = File.ReadAllText(fname);
                textBox1.Text = FormatHelp(helptextAll);
                return;
            }
            else 
            {
                string[] helptextMaster = File.ReadAllLines(fname);
                //marker is cached
                if (context.HelpIndex.ContainsKey(marker))
                {
                    int index = context.HelpIndex[marker];
                    while (!helptextMaster[index].Contains("</" + marker + ">"))
                    {
                        helptext += helptextMaster[index] + "\r\n";
                        index++;
                    }
                    textBox1.Text = FormatHelp(helptext);
                    return;

                }
                else if (marker != "") //marker not cached
                {
                    bool section = false;
                    int count = 0;
                    foreach (string s in helptextMaster)
                    {
                        if (s.Contains("<" + marker + ">"))
                        {
                            context.HelpIndex.Add(marker, count);
                            helptext += s + "\r\n";
                            section = true;
                        }

                        if (section)
                        {
                            helptext += s + "\r\n";
                        }

                        if (s.Contains("</" + marker + ">"))
                        {
                            helptext += s + "\r\n";
                            section = false;
                            textBox1.Text = FormatHelp(helptext);
                            return;
                        }
                        count++;
                    }
                }
            }
            
            
        }
        private string FormatHelp(string help)
        {
            //split into lines
            string temp = "";
            string[] lines = help.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            bool heading = false;
            foreach (string l in lines)
            {
                if (heading && l[0] != '<') //heading
                {
                    //remove big space on first help
                    if (temp == "")
                    {
                        temp += l;
                    }
                    else
                    {
                        temp += "\r\n\r\n" + l;
                    }
                    temp += "\r\n" + "~~~~~~~~~~~~~~~~~~";
                    heading = false;
                }
                else if (l[0] == '<') //tag
                {
                    if (l[1] != '/')
                    {
                        heading = true;
                    }
                }
                else //regular line
                {
                    temp += "\r\n" + l;
                }
            }
            return temp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
