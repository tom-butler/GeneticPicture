using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace GeneticPictureMaker
{
    public class Util
    {
        /// <summary>
        /// Search directories for file
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        public static string findFile(string fname)
        {
            string p0 = @".\" + fname;
            string p1 = @"..\" + fname;
            string p2 = @"..\..\" + fname;
            string p3 = @"..\..\..\" + fname;
            //string p4 = @".\" + fname;
            if (File.Exists(p0)) { return p0; }
            if (File.Exists(p1)) { return p1; }
            if (File.Exists(p2)) { return p2; }
            if (File.Exists(p3)) { return p3; }
            
            return "";
        }
    }
}
