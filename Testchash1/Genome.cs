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
    /// Gene container
    /// </summary>
    public class Genome
    {
        public List<Gene> dna;
        /// <summary>
        /// Number of genes in this genome.
        /// </summary>
        int dnaSize;
        /// <summary>
        /// Measures inaccuracy of the genome to the template image.
        /// </summary>
        public float score;
        /// <summary>
        /// The sum of all elements for every gene in this genome.
        /// Used to compare equality of genomes.
        /// </summary>
        public int hash;

        /// <summary>
        /// Creates a genome with no genes.
        /// </summary>
        public Genome()
        {
            dna = new List<Gene>();
            dnaSize = 0;
            score = -1;
            computeHash();
        }
        /// <summary>
        /// Creates a genome with GenomeSize number of genes.
        /// Each gene has its elements set randomly.
        /// </summary>
        public Genome(Random rnd, int GenomeSize)
        {
            dna = new List<Gene>();

            for (int i = 0; i < GenomeSize; i++)
            {
                dna.Add(new Gene(rnd));
            }
            dnaSize = GenomeSize;
            score = -1;
            computeHash();
        }
        /// <summary>
        /// Creates a genome with genes copied from mum and dad randomly.
        /// Length of kid's genome set to length of mum's.
        /// </summary>
        public Genome(Genome mum, Genome dad)
        {
            dna = new List<Gene>();
            score = -1;

            for (int i = 0; i < mum.dnaSize; i++)
            {
                if (context.rnd.Next(0, 100) < 50)
                    dna.Add(new Gene(mum.getGene(i)));
                else
                    dna.Add(new Gene(dad.getGene(i)));
            }
            dnaSize = mum.dnaSize;
            // now mutate if needed
            if (context.rnd.Next(0, 10000) < context.MutationRate * 100)
            {
                mutate();
            }
            computeHash();
        }
        /// <summary>
        /// Returns true if this genome matches otherGenome on every gene.
        /// </summary>
        public bool equal(Genome otherGenome)
        {
            if (hash != otherGenome.hash)
                return false;
            for (int i = 0; i < dnaSize; i++)
                if (!(getGene(i).equal(otherGenome.getGene(i))))
                    return false;
            return true;
        }
        /// <summary>
        /// Select a random type of mutation and mutate this genome.
        /// </summary>
        public void mutate()
        {
            // kind of mutation
            int mutType = 4;
            int mut100 = context.rnd.Next(0, 100); // 0-99

            if (mut100 < context.mutationPercent1) { mutType = 1; } else
            if (mut100 < context.mutationPercent1+context.mutationPercent2) {mutType = 2;} else
            if (mut100 < context.mutationPercent1+context.mutationPercent2+context.mutationPercent3) {mutType = 3;} // else defaults to 4
            switch (mutType)
            {
                case 1:
                    //mutate 1 to dnaSize new random genes
                    int t = context.rnd.Next(0, dnaSize);
                    for (int jj = 0; jj < t; jj++)
                    {
                        int i = context.rnd.Next(0, dnaSize);
                        dna[i] = new Gene(context.rnd);
                    }
                    context.Mutations++;
                    context.mut1++;
                    break;
                case 2:
                    // creep 1 to 4 gene values + or - 80
                    int creepVal = context.mutationSize;
                    int numAttrToMutate = context.rnd.Next(1, 7) - 2;
                    if (numAttrToMutate < 1)
                        numAttrToMutate = 1;
                    for (int z = 0; z < numAttrToMutate; z++)
                    {
                        //Creep one gene value 
                        int geneIx = context.rnd.Next(0, dnaSize);
                        int attributeIx = context.rnd.Next(0, Gene.GeneSize);
                        int adjustment = context.rnd.Next(0, creepVal*2+1) - creepVal;
                        int newVal = getGene(geneIx).getVal(attributeIx) + adjustment;
                        if (newVal > Gene.MAX_GENE_VAL)
                            newVal = newVal - Gene.MAX_GENE_VAL - 1;
                        if (newVal < 0)
                            newVal = newVal + Gene.MAX_GENE_VAL + 1;
                        getGene(geneIx).setVal(attributeIx, newVal);
                    }
                    context.mut2++;
                    context.Mutations++;
                    break;
                case 3:
                    //swap gene order 
                    int ii = context.rnd.Next(0, dnaSize);
                    int kk = context.rnd.Next(0, dnaSize);
                    Gene hold = dna[kk];
                    dna[kk] = dna[ii];
                    dna[ii] = hold;
                    context.Mutations++;
                    context.mut3++;
                    break;
                case 4:
                    //pick a gene 
                    int gIx = context.rnd.Next(0, dnaSize);
                    creepVal = context.mutationSize;
                    //create a copy of that gene
                    Gene gn = new Gene(getGene(gIx));
                    gIx = context.rnd.Next(0, dnaSize);
                    dna[gIx] = gn;

                    //mutate that gene
                    numAttrToMutate = context.rnd.Next(1, 7) - 2;
                    if (numAttrToMutate < 1) numAttrToMutate = 1;
                    for (int z = 0; z < numAttrToMutate; z++)
                    {
                        int attributeIndex = context.rnd.Next(0, Gene.GeneSize);
                        int n = context.rnd.Next(0, creepVal*2+1) - creepVal;
                        int q = getGene(gIx).getVal(attributeIndex) + n;
                        if (q > Gene.MAX_GENE_VAL)
                            q = q - Gene.MAX_GENE_VAL - 1;
                        if (q < 0)
                            q = q + Gene.MAX_GENE_VAL + 1;
                        getGene(gIx).setVal(attributeIndex, q);
                    }
                    context.Mutations++;
                    context.mut4++;
                    break;
            }

            //check for duplicate genes
            if (context.checkDuplicateGenes)
            {
                for (int thisGene = 0; thisGene < dnaSize; thisGene++)
                    for (int thatGene = thisGene + 1; thatGene < dnaSize; thatGene++)
                        //if (d != w)
                        while (getGene(thisGene).equal(getGene(thatGene)))
                        {
                            //Creep gene value s
                            if (dna[thisGene] == dna[thatGene])
                            {
                                MessageBox.Show("Debug >> Crap: Genome had 2 cells pointing to the same gene");
                            }
                            int numAttrToChange = context.rnd.Next(1, 7) - 2;
                            if (numAttrToChange < 1)
                                numAttrToChange = 1;
                            for (int z = 0; z < numAttrToChange; z++)
                            {
                                //int i = c.rnd.Next(0, dnaSize);
                                int k = context.rnd.Next(0, Gene.GeneSize);
                                int n = context.rnd.Next(0, context.mutationSize) - context.mutationSize/2;
                                int q = getGene(thatGene).getVal(k) + n;
                                if (q > Gene.MAX_GENE_VAL)
                                    q = q - Gene.MAX_GENE_VAL - 1;
                                if (q < 0)
                                    q = q + Gene.MAX_GENE_VAL + 1;
                                getGene(thatGene).setVal(k, q);
                            }
                            context.duplicateGenes++;
                        }
            }
            computeHash();
            score = -1;
        }
        /// <summary>
        /// Sums all elements of every gene in this genome.
        /// </summary>
        public void computeHash()
        {
            hash = 0;
            for (int i = 0; i < dnaSize; i++)
            {
                hash = hash + getGene(i).getHash();
            }
        }
        /// <summary>
        /// Gets the gene at index i.
        /// </summary>
        public Gene getGene(int i)
        {
            return dna[i];
        }

        /// <summary>
        /// Returns the sum of differences between c1 and c2.
        /// </summary>
        public float fdist0(Color c1, Color c2)
        {
            float rr = Math.Abs(c1.R - c2.R) * context.WeightRed;
            float gg = Math.Abs(c1.G - c2.G) * context.WeightGreen;
            float bb = Math.Abs(c1.B - c2.B) * context.WeightBlue;
            return rr + gg + bb;
        }
        /// <summary>
        /// Returns the euclidian of dist between c1 and c2.
        /// </summary>
        public float fdist1(Color c1, Color c2)
        {
            float rr = (c1.R - c2.R) * context.WeightRed;
            float gg = (c1.G - c2.G) * context.WeightGreen;
            float bb = (c1.B - c2.B) * context.WeightBlue;
            return (float)Math.Sqrt((rr) * (rr) + (gg) * (gg) + (bb) * (bb));
        }
        /// <summary>
        /// Returns the max of the manhattan diferences.
        /// </summary>
        public float fdist2(Color c1, Color c2)
        {
            float rr = Math.Abs(c1.R - c2.R) * context.WeightRed;
            float gg = Math.Abs(c1.G - c2.G) * context.WeightGreen;
            float bb = Math.Abs(c1.B - c2.B) * context.WeightBlue;
            return Math.Max(rr, Math.Max(gg, bb));
        }
        /// <summary>
        /// Returns the diff of the sum of rgb (sort of grey scale).
        /// </summary>
        public float fdist3(Color c1, Color c2)
        {
            float ans1 = (c1.R*context.WeightRed + c1.G*context.WeightGreen + c1.B*context.WeightBlue);
            float ans2 = (c2.R*context.WeightRed + c2.G*context.WeightGreen + c2.B*context.WeightBlue);
            return Math.Abs(ans1 - ans2);
        }

        /// <summary>
        /// NearEnough
        /// Returns the sum of differences between c1 and c2 But all close numbers are said to be the same
        /// </summary>
        public float fdist4(Color c1, Color c2)
        {
            //float nearEnoughRange = 15;
            float rr = Math.Abs(c1.R - c2.R)*context.WeightRed;
            float gg = Math.Abs(c1.G - c2.G)*context.WeightGreen;
            float bb = Math.Abs(c1.B - c2.B)*context.WeightBlue;
            if (rr < context.nearEnoughRange) rr = 0;
            if (gg < context.nearEnoughRange) gg = 0;
            if (bb < context.nearEnoughRange) bb = 0;
            return rr + gg + bb;
        }


        /// <summary>
        /// Returns the FastManhattan fastest manhattan system
        /// </summary>
        public float fdist5(Color c1, Color c2)
        {
            return Math.Abs(c1.R - c2.R) + Math.Abs(c1.G - c2.G) + Math.Abs(c1.B - c2.B);
        }
        
        // /// <summary>
        ///// Returns the sum of differences between c1 and c2 Ignoring Green
        ///// </summary>
        //public float fdist6(Color c1, Color c2)
        //{
        //    return Math.Abs(c1.R - c2.R) + Math.Abs(c1.B - c2.B);
        //}
        // /// <summary>
        ///// Returns the sum of differences between c1 and c2 Ignoring Blue
        ///// </summary>
        //public float fdist7(Color c1, Color c2)
        //{
        //    return Math.Abs(c1.G - c2.G) + Math.Abs(c1.R - c2.R);
        //}
            
        /// <summary>
        /// Draws every gene in this genome to the picturebox.
        /// </summary>
        public void draw(Graphics g)
        {
            //Brush myBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            Brush myBrush = new SolidBrush(context.theBackColor);
            g.FillRectangle(myBrush, 0, 0, context.imageSizeX, context.imageSizeY);

            for (int i = 0; i < dnaSize; i++)
            {
                dna[i].draw(g);
            }
        }
        /// <summary>
        /// Compares every "speed"th pixel of the 2 pictureboxes.
        /// Adds the sum of differences to the genome's score.
        /// </summary>
        /// <param name="refPic">PictureBoxTemplate</param>
        /// <param name="genomePic">PictureBoxResult</param>
        public void eval(PictureBox refPic, PictureBox genomePic)
        {
            if (score > 0) return; // already done

            Bitmap bitmapRefPic = (Bitmap)refPic.Image;
            Bitmap bitmapGenomePic = (Bitmap)genomePic.Image;

            // now clear my image 
            Graphics g = Graphics.FromImage(bitmapGenomePic);
            draw(g);
            //Brush myBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
//            Brush myBrush = new SolidBrush(context.theBackColor);
            //Brush myBrush = new SolidBrush(Color.Black);
            //g.FillRectangle(myBrush, 0, 0, context.imageSizeX, context.imageSizeY);

            //for (int i = 0; i < dnaSize; i++)
            //{
            //    dna[i].draw(g);
            //}

            Color colorRef;
            Color colorGenome;
            float dist = 0;

            score = 0;
            for (int x = 0; x < context.imageSizeX; x = x + context.speedX)
            {
                for (int y = 0; y < context.imageSizeY; y = y + context.speedY)
                {
                    colorRef = bitmapRefPic.GetPixel(x, y);
                    colorGenome = bitmapGenomePic.GetPixel(x, y);
                    switch (context.distanceMeasure)
                    {
                        case 0: dist = fdist0(colorRef, colorGenome); break;
                        case 1: dist = fdist1(colorRef, colorGenome); break;
                        case 2: dist = fdist2(colorRef, colorGenome); break;
                        case 3: dist = fdist3(colorRef, colorGenome); break;
                        case 4: dist = fdist4(colorRef, colorGenome); break;
                        case 5: dist = fdist5(colorRef, colorGenome); break;
                        //case 6: dist = fdist6(colorRef, colorGenome); break;
                        //case 7: dist = fdist7(colorRef, colorGenome); break;
                    }
                    score = score + dist;
                }
            }
            if (context.show)
                genomePic.Refresh();
        }
    }

}
