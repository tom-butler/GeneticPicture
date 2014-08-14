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
    /// Gene pool
    /// </summary>
    public class Population
    {
        public List<Genome> pop;
        public int popSize;
        int bestIx;
        float bestVal;

        /// <summary>
        /// Creates a population of genomes with random value genes.
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="popSize">Number of genomes.</param>
        /// <param name="GenomeSize">Number of genes in each genome.</param>
        public Population(Random rnd, int popSize, int GenomeSize)
        {
            pop = new List<Genome>();
            int i;
            for (i = 0; i < popSize; i++)
            {
                pop.Add(new Genome(rnd, GenomeSize));
            }
            this.popSize = popSize;
        }
        /// <summary>
        /// Gets the genome at index i.
        /// Note: pop[i] does the same thing.
        /// </summary>
        /// <param name="i">Index of the genome in this population.</param>
        /// <returns></returns>
        public Genome getGenome(int i)
        {
            return pop[i];
        }
        /// <summary>
        /// Evaluate every genome and sets their scores.
        /// </summary>
        /// <param name="refPic">PictureBoxTemplate</param>
        /// <param name="genomePic">PictureBoxResult</param>
        /// <param name="c">Global variables</param>
        public void evaluate(PictureBox refPic, PictureBox genomePic)
        {
            for (int i = 0; i < popSize; i++)
            {
                pop[i].eval(refPic, genomePic);
                Application.DoEvents();
            }
        }
        /// <summary>
        /// Returns the index of the best matching genome.
        /// </summary>
        public int bestIndex()
        {
            bestIx = 0;
            bestVal = pop[0].score;
            for (int i = 0; i < popSize; i++)
            {
                float s = pop[i].score;
                if ((s < bestVal && s != -1) || (bestVal<0))
                {
                    bestIx = i;
                    bestVal = pop[i].score;
                }
            }
            if (bestVal < 1)
            {
                MessageBox.Show("Crap Error 446 or a perfect soloution (unlikley)- ", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return bestIx;
        }
        /// <summary>
        /// Returns the index of the worst matching genome.
        /// </summary>
        public int worstIndex()
        {
            int worstIx = 0;
            float worstVal = getGenome(0).score;
            for (int i = 0; i < popSize; i++)
            {
                if (getGenome(i).score > worstVal && getGenome(i).score > 0)
                {
                    worstIx = i;
                    worstVal = getGenome(i).score;
                }
            }
            return worstIx;
        }

        public void reapComment()
        {
            //MessageBox.Show("Debug >> Reap " + context.Generation.ToString());
            Form1.showMessageAtBottom("Reap " + context.Generation.ToString());
        }

        public void nextGeneration()
        {
            // what crap code is this - try a switch statement - code only runs once a generation speed is less important
            if (context.selectionType == "Kill10%") { nextGenerationStandard(10);} else
            if (context.selectionType == "Kill20%") { nextGenerationStandard(20);} else
            if (context.selectionType == "Kill30%") { nextGenerationStandard(30);} else
            if (context.selectionType == "Kill40%") { nextGenerationStandard(40);} else
            if (context.selectionType == "Kill50%") { nextGenerationStandard(50);} else
            if (context.selectionType == "Kill60%") { nextGenerationStandard(60);} else
            if (context.selectionType == "Kill70%") { nextGenerationStandard(70);} else
            if (context.selectionType == "Kill80%") { nextGenerationStandard(80);} else
            if (context.selectionType == "Kill90%") { nextGenerationStandard(90);} else
            if (context.selectionType == "Kill100%") { nextGenerationStandard(100);} else
            if (context.selectionType == "Tournament") { nextGenerationCompetitive(100); } else
            if (context.selectionType == "Tournament75") { nextGenerationCompetitive(75); }
            /* Experimental
             * 
             * if (context.selectionType == "Kill50%&80%E20") { if (context.Generation > 10 && context.Generation % 20 == 0) { reapComment(); nextGenerationStandard(80); } else nextGenerationStandard(50); }
                else
                    if (context.selectionType == "Kill50%&90%E100") { if (context.Generation > 10 && context.Generation % 100 == 0) { reapComment(); nextGenerationStandard(90);} else nextGenerationStandard(50); }
                    else
             */

        }

         public void nextGenerationCompetitive(float percent)
         {
             int fighter1 = context.rnd.Next(0,popSize);
             int fighter2 = context.rnd.Next(0,popSize);
             int breedNum = popSize / 2;
             for (int i = 0; i < breedNum; i++)
             {
                 while (getGenome(fighter1).score == -1) fighter1 = context.rnd.Next(0, popSize);
                 while (getGenome(fighter2).score == -1) fighter2 = context.rnd.Next(0, popSize);
                 int looser;
                 if (percent == 100) // this if might make it faster (by saving a single random number roll)
                 {
                     if (getGenome(fighter2).score > getGenome(fighter1).score) looser = fighter2; else looser = fighter1;
                 }
                 else
                 {
                     if (getGenome(fighter2).score > getGenome(fighter1).score && context.rnd.Next(0,100) < percent) looser = fighter2; else looser = fighter1;
                 }
                 breed(looser);
             }
         }
 
        /// <summary>
        /// Half the population breeds.
        /// This replaces the worst matching half of the population with the children.
        /// </summary>
        public void nextGenerationStandard(float percent)
        {
            int worstIx;
            int breedNum = (int)(popSize * percent / 100);
            for (int i = 0; i < breedNum; i++)
            {
                worstIx = worstIndex();
                //getGenome(worstIx).score = -1;
                breed(worstIx);
            }
        }

        /// <summary>
        /// Removes duplicate scores - we previously removed duplicate genes, but gene range is above phenome resoloution so
        /// two genes can have diferent genes but the same actual picture
        /// Keep best at all times
        /// </summary>
        public void removeDuplicateScores(int best)
        {
            for (int i = 0; i < popSize-1; i++)
            {
                Genome g1 = getGenome(i); 
                if (g1.score < 0 ) continue;
                for (int k=i+1; k < popSize; k++)
                {
                Genome g2 = getGenome(k);

                if (g2 == g1)
                {
                    MessageBox.Show("Debug >> Crap: Population has 2 cells pointing to the same geneome");
                }
                if ((int)g1.score == (int)g2.score)
                   {
                       if (best != k)
                       {
                           g2.mutate();
                           //g2.score = -1; //done in mutate anyway
                           context.duplicateScores++; 
                       }
                       else
                       {
                           //MessageBox.Show("Clean skip >>> debug");
                       }
                   }
                }
            }
        }

        /// <summary>
        /// Select a random genome from the entire population.
        /// If genome has been evaluated then return its index. 
        /// If after 100 attempts no evaluated genomes were found then 
        /// return the index of the first genome that has been evalutated. 
        /// </summary>
        /// <param name="rnd"></param>
        /// <returns>Index of a genome that has been evaluated at least once.</returns>
        public int selectMate(Random rnd)
        {
            int count = 0;
            int sel = rnd.Next(0, popSize);
            while (getGenome(sel).score == -1 && count < 100)
            {
                sel = rnd.Next(0, popSize);
                count++;
            }
            // if not found
            if (count >= 100)
            {
                for (int i = 0; i < popSize; i++)
                    if (getGenome(i).score != -1)
                        return i;
            }
            return sel;
        }
        /// <summary>
        /// Create a new genome made from random genes from random parents.
        /// Mutate the new genome until it is unique.
        /// </summary>
        /// <param name="worstIx"></param>
        /// <param name="c"></param>
        public void breed(int worstIx)
        {
            Genome kid;
            int mum = selectMate(context.rnd);
            int dad = selectMate(context.rnd);
            if (mum == dad)
            {
                kid = new Genome(getGenome(mum), new Genome(context.rnd, context.GenomeSize));
            }
            else
            {
                kid = new Genome(getGenome(mum), getGenome(dad));
            }
            //kid.score = -1;
            pop[worstIx] = kid;
            if (context.removeDuplicateGenomes)
            {
                while (isDuplicate(worstIx))
                {
                    Genome worst = getGenome(worstIx);
                    worst.mutate();
                    worst.score = -1;
                    context.duplicateGenomes++;
                }
            }
        }
        /// <summary>
        /// Return true if there are any genomes in this population 
        /// that matches the genome at genomeIndex.
        /// </summary>
        /// <param name="genomeIndex">Index of the genome to be compared.</param>
        /// <returns>False if genome is unique.</returns>
        public bool isDuplicate(int genomeIndex)
        {
            for (int i = 0; i < popSize; i++)
            {
                if (i != genomeIndex)
                {
                    if (getGenome(genomeIndex).equal(getGenome(i)))
                        return true;
                }
            }
            return false;
        }
    }
}
