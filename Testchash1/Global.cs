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
    /// Holds global variables.
    /// </summary>
    public static class context
    {
        /// <summary>
        /// Number of genomes in population.
        /// </summary>
        public static int PopSize;
        /// <summary>
        /// Number of genes in genome.
        /// </summary>
        public static int GenomeSize;
        /// <summary>
        /// Number of times half the population will be replaced with child genomes.
        /// </summary>
        public static int Generations;
        /// <summary>
        /// The current generation number.
        /// </summary>
        public static int Generation;
        /// <summary>
        /// The probability that a genome will mutate at birth.
        /// </summary>
        public static float MutationRate;
        /// <summary>
        /// Next(inclusive, exclusive)
        /// </summary>
        public static Random rnd;
        /// <summary>
        /// Counter for number of all mutations of types 1 to 4 that occured in this run.
        /// </summary>
        public static int Mutations;
        /// <summary>
        /// Counter for number case 1 mutations.
        /// </summary>
        public static int mut1;
        /// <summary>
        /// Counter for number case 2 mutations.
        /// </summary>
        public static int mut2;
        /// <summary>
        /// Counter for number case 3 mutations.
        /// </summary>
        public static int mut3;
        /// <summary>
        /// Counter for number case 4 mutations.
        /// </summary>
        public static int mut4;
        /// <summary>
        /// Counter for number mutations triggered as a result of a duplicated gene in a single genome.
        /// </summary>
        public static int duplicateGenes;
        /// <summary>
        /// fage to set the checkj for duplicate genes
        /// </summary>
        public static bool checkDuplicateGenes;

        /// <summary>
        /// Mirrors the checkbox of the same name
        /// </summary>
        public static bool removeDuplicateGenomes = false;
        /// <summary>
        /// Counter for number mutations triggered as a result of a duplicated genome (rare).
        /// </summary>
        public static int duplicateGenomes;

        /// <summary>
        /// Mirrors the checkbox of the same name 
        /// 0 = Never
        /// 1 = Every Gen
        /// 5 = Every 5th Gen
        /// 10 = Every 10th Gen
        /// 50 = Every 50th Gen
        /// 100 = Every 100th Gen
        /// </summary>
        public static int removeDuplicateScores = 1;

        /// <summary>
        /// Counter for number mutations triggered as a result of a duplicated genome (rare).
        /// </summary>
        public static int duplicateScores;

        /// <summary>
        /// The percentage chance of a given mutation
        /// </summary>
        public static int mutationPercent1 = 25;
        public static int mutationPercent2 = 25;
        public static int mutationPercent3 = 25;
        public static int mutationPercent4 = 25;

        /// <summary>
        /// Size of a small change to the genome (it moves buy up to half this amount)
        /// </summary>
        public static int mutationSize = 80;

        /// <summary>
        /// speedX is n where every nth pixel on the x-axis is compared to the template's matching pixel.
        /// </summary>
        public static int speedX;
        /// <summary>
        /// Same as speedX except on y-axis.
        /// </summary>
        public static int speedY;
        
        /// <summary>
        /// The current running gene pool.
        /// </summary>
        public static Population pop;
        /// <summary>
        /// Show every genome picture if true.
        /// </summary>
        public static bool show;
        /// <summary>
        /// False if program will stop when the generation next ticks over.
        /// </summary>
        public static bool run;
        /// <summary>
        /// The method for distance measure 0=Manhattan, 1=Euclidian, 2=MAXManhatten, 3=diff of averages (Grey)
        /// </summary>
        public static int distanceMeasure;

        /// <summary>
        /// Weightings for each colour  for the distance measure 
        /// </summary>
        public static float WeightRed = 1;
        public static float WeightBlue = 1;
        public static float WeightGreen = 1;
        
        /// <summary>
        /// When distance is near enough any value less than this is 0
        /// </summary>
        public static float nearEnoughRange = 15;
        
        /// <summary>
        /// The maximum number of pixels on one side of a gene.
        /// </summary>
        public static int maxSize;
        public static int minSize;

        /// <summary>
        /// Image size (default is 256)
        /// Just a copy of the form variables of the same name for convenience
        /// </summary>
        public static int imageSizeX;
        public static int imageSizeY;

        /// <summary>
        /// This does vary from run to run
        /// </summary>
        public static int geneSize = 7;

        /// <summary>
        /// Drawing type from the combobox
        /// </summary>
        public static int drawType = 0; //0=ovals

        /// <summary>
        /// Selection Criteria
        /// </summary>
        public static string selectionType = "Standard";

        /// <summary>
        /// List of brushes 
        /// </summary>
        public static Bitmap[] brushes=null;

        /// <summary>
        /// Number of brush files loaded
        /// </summary>
        public static int numOfBrushes;

        /// <summary>
        /// timestring for file names
        /// </summary>
        public static string saveNameTimeStamp;

        /// <summary>
        /// Counter for save loops and re-seed
        /// </summary>
        public static int loopy=0;

        /// <summary>
        /// a global ini file
        /// </summary>
        public static IniFile ini;

        /// <summary>
        /// Directiry to save end of run images into
        /// </summary>
        public static string directorySaveLoopyImages=@".\";

        /// <summary>
        /// Directory to save partial run images into
        /// </summary>
        public static string directorySaveGenImages = @".\";

        /// <summary>
        /// Directory to save stop images into
        /// </summary>
        public static string directorySaveStopImages = @".\";

        /// <summary>
        /// Counter for generation file names diferent from generation
        /// </summary>
        public static int genSaveCounter = 0;
        public static int genSaveCounterMax =0;
        /// <summary>
        /// Below are variables related to the saving of images each generation
        /// </summary>
        public static int saveEachTill = 0;
        public static int saveNth = 10;
        public static int thenSaveEachTill = 1000;
        public static int thenSaveNth = 100; 

        /// <summary>
        /// The background colour
        /// </summary>
        public static Color theBackColor;// = Color.White();

        public static Dictionary<string, int> HelpIndex = new Dictionary<string, int>();
    }

}
