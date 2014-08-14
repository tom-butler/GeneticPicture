using System;
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
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    /// <summary>
    /// Hereditry unit bytes in range 0-MAX_GENE_VAL 
    /// 
    /// For Ovals drawType = 0 and Rectangle drawType = 2
    /// [0] = red 
    /// [1] = green 
    /// [2] = blue 
    /// [3] = xpos 
    /// [4] = ypos 
    /// [5] = width 
    /// [6] = height
    /// 
    /// For Brushes drawType = 1
    /// [0] = xpos 
    /// [1] = ypos 
    /// [2] = brushNumber 
    /// 
    /// For Circle drawType = 3 and square drawType = 5 
    /// [0] = red 
    /// [1] = green 
    /// [2] = blue 
    /// [3] = xpos 
    /// [4] = ypos 
    /// [5] = radius 
    /// 
    /// For Brushes drawType = 4
    /// [0] = xpos 
    /// [1] = ypos 
    /// [2] = brushNumber 
    /// [3] = width 
    /// [4] = height 
    /// 
    ///        if (comboBox5.Text == "Oval") { context.drawType = 0; }
    ///        if (comboBox5.Text == "Circle") { context.drawType = 3; }
    ///        if (comboBox5.Text == "Rectangle") { context.drawType = 2; }
    ///        if (comboBox5.Text == "BrushList") { context.drawType = 1; }
    ///        if (comboBox5.Text == "BrushReSize") { context.drawType = 4; context.geneSize = 5;}
    ///        if (comboBox5.Text == "Square") { context.drawType = 5; context.geneSize = 6;}
    /// 
    /// </summary>
    public class Gene
    {
        /// <summary>
        /// Number of attributes in the gene.
        /// </summary>

        public const int MAX_GENE_VAL = 1023;

        int[] g = new int[context.geneSize];
        private int hash = -1;
        /// <summary>
        /// Initialises all elements to 0.
        /// </summary>
        public Gene()
        {
            for (int i = 0; i < context.geneSize; i++)
                g[i] = 0;
            hash = -1;
        }
        /// <summary>
        /// Sets all elements to the same as geneToCopyFrom.
        /// Pre: geneToCopyFrom has same number of attributes.
        /// </summary>
        public Gene(Gene geneToCopyFrom)
        {
            for (int i = 0; i < context.geneSize; i++)
                g[i] = geneToCopyFrom.g[i];
            hash = -1;
        }
        /// <summary>
        /// Sets all elements to random numbers between 0 and 256.
        /// </summary>
        public Gene(Random rnd)
        {
            for (int i = 0; i < context.geneSize; i++)
            {
                g[i] = rnd.Next(0, MAX_GENE_VAL + 1);
            }
            //g[3] = rnd.Next(0, c.imageSizeX);
            //g[4] = rnd.Next(0, c.imageSizeY);
            //g[5] = rnd.Next(c.minSize,c.maxSize+1);
            //g[6] = rnd.Next(c.minSize, c.maxSize + 1);

            hash = -1;
        }
        /// <summary>
        /// Gets the value of the element at index i.
        /// </summary>
        /// <param name="i">pre: 0 to 7</param>
        /// <returns>Integer element at that index.</returns>
        public int getVal(int i)
        {
            return g[i];
        }
        /// <summary>
        /// Sets the value of the element at index i to val.
        /// </summary>
        /// <param name="i">pre: 0 to 7</param>
        /// <param name="val">pre: 0 to 256</param>
        public void setVal(int i, int val)
        {
            g[i] = val;
            hash = -1;
        }
        /// <summary>
        /// Returns the sum of all element values used for speed of comparison.
        /// </summary>
        public int getHash()
        {
            if (hash == -1)
            {
                hash = 0;
                for (int i = 0; i < context.geneSize; i++)
                    hash = hash + g[i];
            }
            return hash;
        }
        /// <summary>
        /// Returns true if all elements of geneToCompare are equal to all elements of this gene.
        /// </summary>
        /// <param name="geneToCompare">pre: must be same size as this gene</param>
        /// <returns></returns>
        public bool equal(Gene geneToCompare)
        {
            if (getHash() != geneToCompare.getHash()) return false;
            for (int i = 0; i < context.geneSize; i++)
            {
                if (g[i] != geneToCompare.g[i]) return false;
            }
            return true;
        }
        /// <summary>
        /// Creates a new brush for graphics and fills a shape with a color.
        /// Shape and color based on this gene's elements.
        /// </summary>
        /// <param name="graphics">Picture box graphics object.</param>
        /// <param name="c">Global struct that holds all global variables.</param>
        public void draw(Graphics graphics)
        {
            switch (context.drawType)
            {
                case 0 : // oval
                    Brush myBrush = new SolidBrush(Color.FromArgb((int)(g[0] * 255.0 / Gene.MAX_GENE_VAL), (int)(g[1] * 255.0 / Gene.MAX_GENE_VAL), (int)(g[2] * 255.0 / Gene.MAX_GENE_VAL)));
                    int tWidth = (int)((float)context.minSize + g[5] * (context.maxSize - context.minSize) / Gene.MAX_GENE_VAL);
                    int tHeight = (int)((float)context.minSize + g[6] * (context.maxSize - context.minSize) / Gene.MAX_GENE_VAL);
                    int x = (int)(g[3] * context.imageSizeX / MAX_GENE_VAL) - tWidth / 2;
                    int y = (int)(g[4] * context.imageSizeY / MAX_GENE_VAL) - tHeight / 2;
                    graphics.FillEllipse(myBrush,x ,y , tWidth, tHeight);
                break;    
                
                case 1: // brush list
                    int brushnum = (int)((float)g[2] * (context.numOfBrushes-1) / MAX_GENE_VAL+0.99);
                    x = (int)(g[0] * context.imageSizeX / MAX_GENE_VAL);
                    y = (int)(g[1] * context.imageSizeY / MAX_GENE_VAL);
                    graphics.DrawImageUnscaled(context.brushes[brushnum],x,y);
                break;
                
                case 2: // rectangle
                    myBrush = new SolidBrush(Color.FromArgb((int)(g[0] * 255.0 / Gene.MAX_GENE_VAL), (int)(g[1] * 255.0 / Gene.MAX_GENE_VAL), (int)(g[2] * 255.0 / Gene.MAX_GENE_VAL)));
                    tWidth = (int)((float)context.minSize + g[5] * (context.maxSize - context.minSize) / Gene.MAX_GENE_VAL);
                    tHeight = (int)((float)context.minSize + g[6] * (context.maxSize - context.minSize) / Gene.MAX_GENE_VAL);
                    x = (int)(g[3] * context.imageSizeX / MAX_GENE_VAL) - tWidth / 2;
                    y = (int)(g[4] * context.imageSizeY / MAX_GENE_VAL) - tHeight / 2;
                    graphics.FillRectangle(myBrush,x,y, tWidth, tHeight);

                break;

                case 3: // circle
                    myBrush = new SolidBrush(Color.FromArgb((int)(g[0] * 255.0 / Gene.MAX_GENE_VAL), (int)(g[1] * 255.0 / Gene.MAX_GENE_VAL), (int)(g[2] * 255.0 / Gene.MAX_GENE_VAL)));
                    tWidth = (int)((float)context.minSize + g[5] * (context.maxSize - context.minSize) / Gene.MAX_GENE_VAL);
                    x = (int)(g[3] * context.imageSizeX / MAX_GENE_VAL) - tWidth / 2;
                    y = (int)(g[4] * context.imageSizeY / MAX_GENE_VAL) - tWidth / 2;
                    graphics.FillEllipse(myBrush, x,y, tWidth, tWidth);
                break;

                case 4: // brush list
                    brushnum = (int)((float)g[2] * (context.numOfBrushes-1) / MAX_GENE_VAL+0.99);
                    x = (int)(g[0] * context.imageSizeX / MAX_GENE_VAL);
                    y = (int)(g[1] * context.imageSizeY / MAX_GENE_VAL);
                    tWidth = (int)((float)context.minSize + g[3] * (context.maxSize - context.minSize) / Gene.MAX_GENE_VAL);
                    tHeight = (int)((float)context.minSize + g[4] * (context.maxSize - context.minSize) / Gene.MAX_GENE_VAL);
                    Rectangle destRect = new Rectangle(x,y, tWidth, tHeight);
                    graphics.DrawImage(context.brushes[brushnum], destRect);
                break;

                case 5: // square
                    myBrush = new SolidBrush(Color.FromArgb((int)(g[0] * 255.0 / Gene.MAX_GENE_VAL), (int)(g[1] * 255.0 / Gene.MAX_GENE_VAL), (int)(g[2] * 255.0 / Gene.MAX_GENE_VAL)));
                    tWidth = (int)((float)context.minSize + g[5] * (context.maxSize - context.minSize) / Gene.MAX_GENE_VAL);
                    x = (int)(g[3] * context.imageSizeX / MAX_GENE_VAL) - tWidth / 2;
                    y = (int)(g[4] * context.imageSizeY / MAX_GENE_VAL) - tWidth / 2;
                    graphics.FillRectangle(myBrush, x, y, tWidth, tWidth);
                break;

            //if (comboBox5.Text == "BrushReSize") { context.drawType = 4; context.geneSize = 5;}
            //if (comboBox5.Text == "Square") { context.drawType = 5; context.geneSize = 6;}

                default:
                MessageBox.Show("Serious Error 5512 - programmer is at fault:" + context.drawType.ToString(), "Finished", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
            }
        }
        /// <summary>
        /// Number of attributes in any gene.
        /// </summary>
        public static int GeneSize
        {
            get
            {
                return context.geneSize;
            }
        }
    }
}
