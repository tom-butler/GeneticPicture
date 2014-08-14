using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.IO;

namespace GeneticPictureMaker
{
    /// <summary>
    /// The only GUI
    /// </summary>
    public partial class Form1 : Form
    {

        Form3 form3;
        public static Form1 form1;
        bool paused = false;
        string CurrentFile;
        Bitmap bmpResult;
        //public context c;
        private const string PATH = @"Templates\", 
                             SAVE_FOLDERS_DIRECTORY_NAME = @"runs\", 
                             DEFAULT_TEMPLATE_FILE_NAME = "default.png";
        private readonly string FirstDirectory;

        /// <summary>
        /// Image size (default is 256)
        /// </summary>
        public int imageSizeX=128;
        public int imageSizeY=128;

        int best = 0; // current best genome
        //display of the genome
        Graphics graphic;
        DateTime startTime;
        TimeSpan duration;
        String titleText;
        int loopycache;
        public Form1()
        {
            InitializeComponent();
            titleText = this.Text;
            bmpResult = new Bitmap(imageSizeX, imageSizeY);
            //form3.pictureBoxResultQ().Image = bmpResult;
            //form3.pictureBoxTemplateQ().Image = (Bitmap)Bitmap.FromFile(PATH + DEFAULT_TEMPLATE_FILE_NAME, true);
            FirstDirectory = System.IO.Directory.GetCurrentDirectory();
            form1 = this;

            //setup help button
            HelpRndSeed.Click += new EventHandler(this.openHelp);

        }
        #region window control events

        private void setUploadTemplateDialog()
        {            
            System.IO.Directory.SetCurrentDirectory(FirstDirectory);
            // configure the open file dialog to point to some
            // common (usable) image file formats
            openFileDialog.Title = "Open Template File";
            openFileDialog.Filter = "Bitmap Files|*.bmp" +
                    "|Enhanced Windows MetaFile|*.emf" +
                    "|Exchangeable Image File|*.exif" +
                    "|Gif Files|*.gif|JPEG Files|*.jpg" +
                    "|PNG Files|*.png|TIFF Files|*.tif|Windows MetaFile|*.wmf";

            openFileDialog.InitialDirectory = PATH;
            openFileDialog.DefaultExt = "png";
            openFileDialog.FilterIndex = 6;
            openFileDialog.FileName = "";
            openFileDialog.ShowDialog();
        }

        private void Legacy_load_fixed_size_btnLoadTemplate_Click(object sender, EventArgs e)
        {

            setUploadTemplateDialog();
            // if the user did not select a file, return
            if (openFileDialog.FileName == "") return;

            // update the current file and form caption text
            CurrentFile = openFileDialog.FileName.ToString();

            //this.Text = "Watermark Utility: " + CurrentFile.ToString();

            try
            {
                // open the image into the picture box
                //bmpTemplate = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, true);
                Image img = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, true);
                if (img.Width != form3.pictureBoxTemplateQ().Width || img.Height != form3.pictureBoxTemplateQ().Height)
                {
                    MessageBox.Show("please load images that match the dimensions of the picturebox:\n" + form3.pictureBoxTemplateQ().Width + "x" + form3.pictureBoxTemplateQ().Height, "Wrong size picture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                form3.pictureBoxTemplateQ().Image = img; //bmpTemplate;

                // resize the picture box to support scrolling
                // large images

                //picContainer.Size = img.Size;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Open Error");
            }
            form3.Show();
        }

        public static void showMessageAtBottom(string mes)
        {
             form1.label33.Text = mes;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2();

            form2.ShowDialog();
        }
            
        private void btnTest_Click_old(object sender, EventArgs e)
        {
        }

        public void setDistanceMeasure()
        {
            if (comboBox1.Text == "Manhattan") { context.distanceMeasure = 0; }
            if (comboBox1.Text == "Euclidian") { context.distanceMeasure = 1; }
            if (comboBox1.Text == "MAXMan") { context.distanceMeasure = 2; }
            if (comboBox1.Text == "Grey") { context.distanceMeasure = 3; }
            if (comboBox1.Text == "NearEnough") { context.distanceMeasure = 4; }
            if (comboBox1.Text == "FastManhattan") { context.distanceMeasure = 5; }
            //if (comboBox1.Text == "IgnoreGreen") { context.distanceMeasure = 6; }
            //if (comboBox1.Text == "IgnoreBlue") { context.distanceMeasure = 7; }
 
        }

        public void setDrawType()
        {
            if (comboBox5.Text == "Oval") { context.drawType = 0; context.geneSize = 7; }
            if (comboBox5.Text == "Circle") { context.drawType = 3; context.geneSize = 6;}
            if (comboBox5.Text == "Rectangle") { context.drawType = 2; context.geneSize = 7;}
            if (comboBox5.Text == "BrushList") { context.drawType = 1; context.geneSize = 3;}
            if (comboBox5.Text == "BrushReSize") { context.drawType = 4; context.geneSize = 5;}
            if (comboBox5.Text == "Square") { context.drawType = 5; context.geneSize = 6;}
        }

        public void setDuplicateScoreHandling()
        {
         context.removeDuplicateScores = 1;
         if (comboBox9.Text=="Never") context.removeDuplicateScores = 0;                /// 0 = Never
         if (comboBox9.Text=="Every Gen") context.removeDuplicateScores = 1;/// 1 = Every Gen
         if (comboBox9.Text=="Every 5th Gen") context.removeDuplicateScores = 5;/// 5 = Every 5th Gen
         if (comboBox9.Text=="Every 10th Gen") context.removeDuplicateScores = 10;/// 10 = Every 10th Gen
         if (comboBox9.Text=="Every 50th Gen") context.removeDuplicateScores = 50;/// 50 = Every 50th Gen
         if (comboBox9.Text=="Every 100th Gen") context.removeDuplicateScores = 100;/// 100 = Every 100th Gen
        }

        public void setSelectionType()
        {
            context.selectionType = comboBox4.Text;
        }

        public void readBrushlist()
        {
            context.numOfBrushes = 0;
            context.brushes = new Bitmap[100];

            string CurrentDir = @"Brushes\"+comboBox6.Text+@"\";

            for (int j = 0; j < 100; j++)
            {
                //this.Text = "Watermark Utility: " + CurrentFile.ToString();
                string fname = CurrentDir + "Brush" + context.numOfBrushes.ToString() + ".png";

                try
                {
                    // open the image into the picture box
                    //bmpTemplate = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, true);
                    Image img = (Bitmap)Bitmap.FromFile(fname, true);
                    context.brushes[context.numOfBrushes] = (Bitmap)img;
                    context.numOfBrushes++;
                }
                catch (Exception ex)
                {
                    if (context.numOfBrushes < 2) {MessageBox.Show(ex.Message, "File Open Error");}
                    break;
                    
                }
            } // end of for loop 
            label33.Text = "num of Brushes = " + context.numOfBrushes.ToString();
        }


        private void btnInitialise_Click(object sender, EventArgs e)
        {
            initialise_All();
        }

        private void initialise_All()
        {
            grpUserInput.Enabled = false;
            duration = new TimeSpan();
            //label18.Text = "";
            fixMutationPercents();
            this.Text = titleText;
            //btnTest.Enabled = true;
            btnRun.Enabled = true;
            if (chkSaveEvery.Checked == false) { btnSave.Enabled = true; }
               

            context.rnd = new Random(Int32.Parse(txtRndSeed.Text));
            context.PopSize = Int32.Parse(txtMaxPopulation.Text);
            context.GenomeSize = Int32.Parse(txtGenomeSize.Text);

            context.maxSize = Int32.Parse(txtMaxImgSizeOfGen.Text);
            context.minSize = Int32.Parse(txtMinImgSizeOfGen.Text);

            context.WeightRed = float.Parse(maskedTextBox1.Text);
            context.WeightGreen = float.Parse(maskedTextBox2.Text);
            context.WeightBlue = float.Parse(maskedTextBox3.Text);
            context.nearEnoughRange = Int32.Parse(maskedTextBox4.Text);
            context.checkDuplicateGenes = checkBox2.Checked;
            context.theBackColor = panelBackgroundColor.BackColor;

            context.saveNameTimeStamp = GetStringDateTimeNow();

            context.imageSizeX = imageSizeX;
            context.imageSizeY = imageSizeY;

            context.mut1 = 0;
            context.mut2 = 0;
            context.mut3 = 0;
            context.mut4 = 0;
            context.duplicateGenes = 0;
            context.duplicateGenomes = 0;
            context.duplicateScores = 0;

            context.removeDuplicateGenomes = chkRemoveDuplicateGenomes.Checked;
            //context.removeDuplicateScores = chkRemoveDuplicateScores.Checked;

            context.mutationSize = Int32.Parse(maskedTextBox13.Text);

            context.Generation = 0;
            lblCurrGenerationNum.Text = context.Generation.ToString();
            context.Mutations = 0;
            lblSumOfMut1To4.Text = context.Mutations.ToString();

            context.MutationRate = (float)Double.Parse(numericUpDown1.Text);
            if (context.MutationRate > 99) context.MutationRate = 99;
            if (context.MutationRate <0 ) context.MutationRate = 0;
            context.saveNameTimeStamp = GetStringDateTimeNow();

            context.saveEachTill = Int32.Parse(maskedTextBox5.Text);
            context.saveNth = Int32.Parse(maskedTextBox6.Text);
            context.thenSaveEachTill = Int32.Parse(maskedTextBox7.Text);
            context.thenSaveNth = Int32.Parse(maskedTextBox8.Text);
  
            setSelectionType();
            setDrawType();
            setDuplicateScoreHandling();
            setDistanceMeasure();
            
            if (context.drawType == 1 || context.drawType == 4)readBrushlist();

            context.pop = new Population(context.rnd, context.PopSize, context.GenomeSize);

            ClearResultImage();
        }

        
        private void btnRun_Click(object sender, EventArgs e)
        {
            //Random rloopy = new Random(); 
            btnStop.Enabled = true;
            groupBox1.Enabled = false;
            groupBox6.Enabled = false;
            groupBox8.Enabled = false;
            context.loopy = Int32.Parse(comboBox7.Text);
            if (context.loopy != 0 && context.loopy != 1)
            {
                while (context.loopy > 0)
                {
                    label21.Text = context.loopy.ToString();
                    int seed = (Int32.Parse(txtRndSeed.Text) + 1);
                    txtRndSeed.Text = seed.ToString();
                    initialise_All();
                    runGA();

                    saveImageOnLoopy();
                    context.loopy--;
                }
                label21.Text = context.loopy.ToString();
                //comboBox7.Text = "0"; // loopy
                if (paused != true)
                {
                    MessageBox.Show("The loopy run is complete");
                }
            }
            else
            {
                initialise_All();
                btnRun.Enabled = false;
                runGA();
            }
        }

        /// <summary>
        /// Run simulation.
        /// Pre: btnInitialise_Click
        /// </summary>
        private void runGA()
        {
            //chkSaveEvery.Enabled = true;
            form3.BringToFront();
            this.Text = titleText + " - Running";
            lblCurrentState.Text = "Running";
            SwapRunState();
            

            context.speedX = Int32.Parse(txtSpeedX.Text);
            context.speedY = Int32.Parse(txtSpeedY.Text);
            context.show = chkShowAllGenomes.Checked;
            context.run = true;
            context.Generations = Int32.Parse(txtMaxGenerations.Text);
            //----20080429
            //Set scores to -1 in case fidelity has changed.
            for (int i = 0; i < context.pop.popSize; i++)
            {
                context.pop.getGenome(i).score = -1;
                //if (getGenome(i).score != -1) return i;
            }
            //----

            context.pop.evaluate(form3.pictureBoxTemplateQ(), form3.pictureBoxResultQ());

            best = context.pop.bestIndex();
            label10.Text = context.pop.getGenome(best).score.ToString("N0");
            graphic = Graphics.FromImage(bmpResult);
            context.pop.getGenome(best).draw(graphic);
            form3.pictureBoxResultQ().Refresh();


            //Display evolution of population
            graphic = GALoop(graphic);
            if (paused != true)
            {
                btnStop_Click(null, null);
            }

        }
        private Graphics GALoop(Graphics g)
        {
            startTime = DateTime.Now;
            DateTime last = DateTime.Now;
            
            //Display evolution of population
            while (context.Generation < context.Generations && context.run)
            {
                context.Generation++;

                context.pop.nextGeneration();

                context.show = chkShowAllGenomes.Checked;

                context.pop.evaluate(form3.pictureBoxTemplateQ(), form3.pictureBoxResultQ());
                best = context.pop.bestIndex();
                //float mmm = context.pop.getGenome(best).score;
                //context.pop.getGenome(best).score = -1;
                //context.pop.getGenome(best).eval(pictureBoxTemplate, pictureBoxResult);
                //if (context.pop.getGenome(best).score != mmm)
                // {
                //     MessageBox.Show("Debug >> Crap: error in population");
                // }
                context.pop.getGenome(best).draw(g);

                //timecounter

                TimeSpan run = DateTime.Now - last;
                last = DateTime.Now;
                duration = run + duration;
                form3.updateTime(duration);



                form3.pictureBoxResultQ().Refresh();
                Application.DoEvents();

                if (context.removeDuplicateScores != 0)
                {
                    if (context.removeDuplicateScores == 1) { context.pop.removeDuplicateScores(best); }
                    else
                    {
                        if (context.Generation % context.removeDuplicateScores == 0) { context.pop.removeDuplicateScores(best); }
                    }
                }

                //label10.Text = best.ToString("N0") + " " + context.pop.getGenome(best).score.ToString("N0");
                label10.Text = context.pop.getGenome(best).score.ToString("N0");

                if (chkSaveEvery.Checked)
                {
                    saveImageOnGenerationRun();
                }

                labelCase1Count.Text = context.mut1.ToString();
                labelCase2Count.Text = context.mut2.ToString();
                labelCase3Count.Text = context.mut3.ToString();
                labelCase4Count.Text = context.mut4.ToString();
                lblSumOfMut1To4.Text = context.Mutations.ToString();
                lblCurrGenerationNum.Text = context.Generation.ToString();

                labelDuplicateGeneCount.Text = context.duplicateGenes.ToString();
                labelDuplicateGenomeCount.Text = context.duplicateGenomes.ToString();
                labelDuplicateScore.Text = context.duplicateScores.ToString();

                Application.DoEvents();
            }


            this.Text = titleText + " - Paused";
            lblCurrentState.Text = "Paused";

            // now reset loop processing for next run 

            //comboBox7.Text = "0"; // loopy

            SwapRunState();
            if (context.run)
            {
                //btnPause.Enabled = false;
                if (checkBox1.Checked) saveImageOnStop();
               // if (context.loopy <= 0) MessageBox.Show("Done\nduration: " + strDuration, "Finished", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
               // MessageBox.Show("Paused\nduration: " + strDuration, "Stopped", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return g;
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            paused = true;
            this.Text = titleText + " - Pausing...";
            lblCurrentState.Text = "Pausing...";
            btnContinue.Enabled = true;
            //btnPause.Enabled = false;
            context.run = false;
            loopycache = context.loopy;
            context.loopy = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(FirstDirectory);
            saveFileDialog.Title = "Save Image File";
            saveFileDialog.Filter = "PNG Files|*.png";
            saveFileDialog.DefaultExt = "png";
            saveFileDialog.FilterIndex = 6;

            try
            {
                
                //System.IO.Directory.CreateDirectory(PATH + SAVE_FOLDERS_DIRECTORY_NAME + context.saveDirName);
                //saveFileDialog.InitialDirectory = PATH + SAVE_FOLDERS_DIRECTORY_NAME + context.saveDirName;

                int diffNumDigits = context.Generations.ToString().Length - context.Generation.ToString().Length;
                string zeroes = "";
                //for (int i = 0; i < diffNumDigits; i++)
                //    zeroes += "0";
                saveFileDialog.FileName = zeroes + context.Generation + "." + saveFileDialog.DefaultExt;

                // if the user did not select a file, return
                DialogResult dialogResult = saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName == "") return;
                if (dialogResult == DialogResult.Cancel)
                {
                    //if (System.IO.Directory.Exists(PATH + SAVE_FOLDERS_DIRECTORY_NAME + saveDirName))
                    //    System.IO.Directory.Delete(PATH + SAVE_FOLDERS_DIRECTORY_NAME + saveDirName);
                    return;
                }

                SaveCurrentImage(saveFileDialog.FileName);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Save Error");
            }
        }

        private void chkSaveEvery_CheckedChanged(object sender, EventArgs e)
        {
            //if (context.run == false)
            //    btnSave.Enabled = true;
            //if (chkSaveEvery.Checked)
            //    btnSave.Enabled = false;
        }

        private void btnPlayback_Click(object sender, EventArgs e)
        {
            if (context.genSaveCounterMax<1)
            {
                MessageBox.Show("Sorry No Images saved in last run"," Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Text =  titleText + " - Playing Back";
            lblCurrentState.Text = "Playing Back";
            bool isPlaying = true;
            context.genSaveCounter = 0;
            progressBarPlayback.Maximum = context.genSaveCounterMax;
            progressBarPlayback.Minimum = 0;
            progressBarPlayback.Value = 0;
            progressBarPlayback.Enabled = true;
            this.Cursor = Cursors.WaitCursor;
            string fileName = "";
            int gen = 0;
            int ticketyBoo = 0;
            while  (isPlaying)
            {
                gen = gen + 1;
                if (gen > context.Generations) { this.Text = titleText + " - Ending PlayBack"; isPlaying = false; continue; }
                context.genSaveCounter = getSaveGenNumber(gen);
                fileName = getSaveGenNumberFilename(gen);
                if (!System.IO.File.Exists(fileName))
                {
                    ticketyBoo++;
                    if (ticketyBoo >= 10)
                    {
                        this.Text = titleText + " - NoMoreFiles";
                        lblCurrentState.Text = "No More Files";
                        isPlaying = false;
                    }
                    continue;
                }
                    
                form3.pictureBoxResultQ().Image = (Bitmap)Bitmap.FromFile(fileName, true);
                form3.pictureBoxResultQ().Refresh();
                progressBarPlayback.Value = Math.Min(gen, progressBarPlayback.Maximum);
                Thread.Sleep(20);
                ticketyBoo++;
            }

            btnPlayback.Enabled = true;
            progressBarPlayback.Enabled = false;
            this.Cursor = Cursors.Default;
            this.Text = titleText;
        }
        #endregion
        #region private methods
        /// <summary>
        /// Swaps the enabled state for controls needed or not needed during a run.
        /// </summary>
        private void SwapRunState()
        {
            //btnRun.Enabled = !btnRun.Enabled;
            //btnContinue.Enabled = !btnContinue.Enabled;
            btnPause.Enabled = !btnPause.Enabled;
            //btnStop.Enabled = !btnStop.Enabled;

            //btnLoadTemplate.Enabled = !btnLoadTemplate.Enabled;
           // btnInitialise.Enabled = !btnInitialise.Enabled;
           //btnTest.Enabled = !btnTest.Enabled;
            //grpUserInput.Enabled = !grpUserInput.Enabled;
            btnPlayback.Enabled = !btnPlayback.Enabled;
            chkSaveEvery.Enabled = !chkSaveEvery.Enabled;
        }
        /// <summary>
        /// Returns a single string of the date and time format "yyyymmdd_hhmm" (24hrs).
        /// </summary>
        private string GetStringDateTimeNow()
        {
            string result = DateTime.Now.Year.ToString();
            if (DateTime.Now.Month < 10)
                result += "0";
            result += DateTime.Now.Month.ToString();
            if (DateTime.Now.Day < 10) 
                result += "0";
            result += DateTime.Now.Day.ToString() + "_";
            if (DateTime.Now.Hour < 10) 
                result += "0";
            result += DateTime.Now.Hour.ToString();
            if (DateTime.Now.Minute < 10) 
                result += "0";
            result += DateTime.Now.Minute.ToString();
            return result;
        }

        private void saveImageOnStop()
        {
            string fname;
            fname = context.directorySaveStopImages+"Stop_" + context.saveNameTimeStamp + "_" + context.Generation + ".png";
            SaveCurrentImage(fname);
        }

        private int getSaveGenNumber(int genNum)
        {

            if (genNum <= context.saveEachTill)
            {
                context.genSaveCounter++;
                return context.genSaveCounter;
            }

            if (genNum > context.saveEachTill && context.Generation <= context.thenSaveEachTill)
            {
                if (genNum % context.saveNth == 0)
                {
                    context.genSaveCounter++;
                    return context.genSaveCounter;
                }
            }

            if (genNum > context.saveEachTill)
            {
                if (genNum % context.thenSaveNth == 0)
                {
                    context.genSaveCounter++;
                    return context.genSaveCounter;
                }
            }

        return context.genSaveCounter;
        }

        private string getSaveGenNumberFilename(int genNum)
        {
            string fname = "";
            int diffNumDigits = 5 - context.genSaveCounter.ToString().Length;
            string zeroes = "";
            for (int i = 0; i < diffNumDigits; i++) zeroes += "0";          
            fname = context.directorySaveGenImages+comboBox8.Text+"_" +zeroes+ context.genSaveCounter + ".png";
            if (comboBox8.Text == "Both")
            {
                fname = context.directorySaveGenImages + "Both_" + zeroes + context.genSaveCounter + "_" + genNum.ToString() + ".png";
            }
            if (comboBox8.Text == "Gen")
            {
                fname = context.directorySaveGenImages + "Gen_" + genNum.ToString() + ".png";
            }
            return fname;
        }

        private void saveImageOnGenerationRun()
        {
            string fname;
            int temp = context.genSaveCounter;
            context.genSaveCounter = getSaveGenNumber(context.Generation);
            context.genSaveCounterMax = context.genSaveCounter;
            if (temp != context.genSaveCounter)
            {
                fname = getSaveGenNumberFilename(context.Generation);
                SaveCurrentImage(fname);
            }
        }

        private void saveImageOnLoopy()
        {
            string fname;
            fname = context.directorySaveLoopyImages+comboBox8.Text + context.saveNameTimeStamp + "_" + context.loopy + ".png";
            SaveCurrentImage(fname);
            //int diffNumDigits = context.Generations.ToString().Length - context.Generation.ToString().Length;
            //string zeroes = "";
            //for (int i = 0; i < diffNumDigits; i++) zeroes += "0";
        }


        /// <summary>
        /// Saves the image in PictureBoxResult to file using the saveDialog used at btnSave_Clicked.
        /// Pre: must have used btnSave_Clicked to initialise the saveDialog.
        /// </summary>
        private void SaveCurrentImage(string fname)
        {
            Bitmap bmp = null;
            label33.Text = "Saving "+fname;
            if (checkBox3.Checked)
            {
                bmp = new Bitmap(form3.pictureBoxResultQ().Image);
                for (int x = 0; x < context.imageSizeX; x = x + 1)
                {
                    for (int y = 0; y < context.imageSizeY; y = y + 1)
                    {
                        Color c = bmp.GetPixel(x, y);
                        int delta = Int32.Parse(maskedTextBox14.Text);
                        if (Math.Abs(c.R - panelBackgroundColor.BackColor.R) <= delta  &&
                            Math.Abs(c.G - panelBackgroundColor.BackColor.G) <= delta &&
                            Math.Abs(c.B - panelBackgroundColor.BackColor.B) <= delta) 
                        {
                            Color cc = Color.FromArgb(0, c);
                            bmp.SetPixel(x, y, cc);
                        }
                    }
                }
                System.IO.FileStream fs = new System.IO.FileStream(fname, System.IO.FileMode.Create);
                bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                fs.Close();
            }
            else
            {
                //label33.Text = "Saving "+fname;
                System.IO.FileStream fs = new System.IO.FileStream(fname, System.IO.FileMode.Create);
                form3.pictureBoxResultQ().Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                fs.Close();
            }
            label33.Text = fname + " saved";
        }
        /// <summary>
        /// Clears the image in PictureBoxResult.
        /// </summary>
        private void ClearResultImage()
        {
            Graphics g = Graphics.FromImage((Bitmap)form3.pictureBoxResultQ().Image);
            Brush myBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            g.FillRectangle(myBrush, 0, 0, imageSizeX, imageSizeY);
            form3.pictureBoxResultQ().Refresh();
        }
        #endregion

        public void setPictureSize()
        {
            form3.pictureBoxTemplateQ().Width = imageSizeX;
            form3.pictureBoxTemplateQ().Height = imageSizeY;
            form3.pictureBoxResultQ().Width = imageSizeX;
            form3.pictureBoxResultQ().Height = imageSizeY;
            context.imageSizeX = imageSizeX;
            context.imageSizeY = imageSizeY;
            bmpResult = new Bitmap(imageSizeX, imageSizeY);
            form3.pictureBoxResultQ().Image = bmpResult;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            form3 = new Form3();
            form3.pictureBoxResultQ().Image = bmpResult;
            form3.pictureBoxTemplateQ().Image = (Bitmap)Bitmap.FromFile(PATH + DEFAULT_TEMPLATE_FILE_NAME, true);
            setPictureSize();
            form3.Show();
            context.ini = new IniFile("GeneticPictureMaker.ini");
            context.directorySaveLoopyImages = context.ini.IniReadValue("main", "directorySaveLoopyImages", @"Runs\Loop\");
            context.directorySaveGenImages = context.ini.IniReadValue("main", "directorySaveGenImages", @"Runs\Gen\");
            context.directorySaveStopImages = context.ini.IniReadValue("main","directorySaveStopImages",@"Runs\Stop\");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageSizeX = Convert.ToInt32(label23.Text);
           setPictureSize();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageSizeY = Convert.ToInt32(label34.Text);
            setPictureSize();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDistanceMeasure();
            //if (comboBox1.Text == "Manhattan") { context.distanceMeasure = 0; }
            //if (comboBox1.Text == "Euclidian") { context.distanceMeasure = 1; }
            //if (comboBox1.Text == "MAXMan") { context.distanceMeasure = 2; }
            //if (comboBox1.Text == "Grey") { context.distanceMeasure = 3; }
        }

        private void buttonLoadtemplateResz_Click(object sender, EventArgs e)
        {
        setUploadTemplateDialog();
        openFileDialog.Title = "Open Template File Resize";
            // if the user did not select a file, return
            if (openFileDialog.FileName == "") return;

            // update the current file and form caption text
            CurrentFile = openFileDialog.FileName.ToString();

            try
            {
                // open the image into the picture box
                //bmpTemplate = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, true);
                Image img = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, true);

                if (img.Width > 1024 || img.Height >1024 || img.Width <8 || img.Height <8)
                {
                    MessageBox.Show("please load images that have sensible size:\n", "Wrong size picture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                imageSizeX=img.Width;
                imageSizeY=img.Height;
                setPictureSize();
                label23.Text = imageSizeX.ToString();
                label34.Text = imageSizeY.ToString();


                form3.pictureBoxTemplateQ().Image = img; //bmpTemplate;

                // resize the picture box to support scrolling
                // large images

                //picContainer.Size = img.Size;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Open Error");
            }
            form3.Show();
            form3.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Random rloopy = new Random(); 
            context.loopy=Int32.Parse(comboBox7.Text);
            while (context.loopy > 0)
            {
                label21.Text = context.loopy.ToString();
                int seed = (Int32.Parse(txtRndSeed.Text) + 1);
                txtRndSeed.Text = seed.ToString();
                initialise_All();
                runGA();

                saveImageOnLoopy();
                context.loopy--;
            }
            label21.Text = context.loopy.ToString();
            comboBox7.Text = "0"; // loopy
            MessageBox.Show("Seriously Dude the loopy run is complete");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form3.Show();
            form3.BringToFront();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {
        
        }

        public string FileBrowser(string name)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                    return folderBrowserDialog1.SelectedPath + @"\";
            }
            return "";
        }
        public bool SetDirectories(string stop, string gen, string loop)
        {
            List<string> dirs = new List<string>();
            if (!Directory.Exists(stop))
            {
                dirs.Add(stop);
            }
            if (!Directory.Exists(gen))
            {
                dirs.Add(gen);
            }
            if (!Directory.Exists(loop))
            {
                dirs.Add(loop);
            }
            
            if (dirs.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Directories do not exist, would you like to create them", "Error", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        foreach (string s in dirs)
                        {
                            Directory.CreateDirectory(s);
                        }
                    }
                    catch (IOException ioex)
                    {
                        MessageBox.Show(ioex.Message);
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    return false;
                }
            }
            context.directorySaveStopImages = stop;
            context.directorySaveGenImages = gen;
            context.directorySaveLoopyImages = loop;
            return true;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Text = titleText + " - Stopping...";
            lblCurrentState.Text = "Stopping...";
            //btnPause.Enabled = false;
            context.run = false;
            context.loopy = 0;
            context.ini.IniWriteValue("main", "directorySaveLoopyImages", context.directorySaveLoopyImages);
            context.ini.IniWriteValue("main", "directorySaveGenImages", context.directorySaveGenImages);
            context.ini.IniWriteValue("main", "directorySaveStopImages", context.directorySaveStopImages);
        }

        private void maskedTextBox9_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox9_TextChanged(object sender, EventArgs e)
        {
            checkMutationPercents();
        }

        private void maskedTextBox10_TextChanged(object sender, EventArgs e)
        {
            checkMutationPercents();
        }

        private void maskedTextBox11_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox11_TextChanged(object sender, EventArgs e)
        {
            checkMutationPercents();
        }

        private void maskedTextBox12_TextChanged(object sender, EventArgs e)
        {
            checkMutationPercents();
        }

        public void fixMutationPercents()
        {
            

                int m1 = Int32.Parse(maskedTextBox9.Text);
                int m2 = Int32.Parse(maskedTextBox10.Text);
                int m3 = Int32.Parse(maskedTextBox11.Text);
                int m4 = Int32.Parse(maskedTextBox12.Text);

                while (m1+m2+m3+m4 !=100)
                {
                    if (m1 > 100) { m1 = 100; }
                    if (m1 + m2 > 100) { m2 = 100 - m1; }
                    if (m1 + m2 + m3 > 100) { m3 = 100 - m1 - m2; }
                    if (m1 + m2 + m3 + m4 > 100) { m4 = 100 - m1 - m2 - m3; }

                    if (m1 + m2 + m3 + m4 < 100 && m4 < 100) { m4 = m4 + 1; }
                    if (m1 + m2 + m3 + m4 < 100 && m3 < 100) { m3 = m3 + 1; }
                    if (m1 + m2 + m3 + m4 < 100 && m2 < 100) { m2 = m2 + 1; }
                    if (m1 + m2 + m3 + m4 < 100 && m1 < 100) { m1 = m1 + 1; }
                }
                maskedTextBox9.Text = m1.ToString();
                maskedTextBox10.Text = m2.ToString();
                maskedTextBox11.Text = m3.ToString();
                maskedTextBox12.Text = m4.ToString();                
                
                context.mutationPercent1 = m1;
                context.mutationPercent2 = m2;
                context.mutationPercent3 = m3;
                context.mutationPercent4 = m4;

            
        }

        private void checkMutationPercents()
        {
            // also sets mutation percents in global context
            int m1=0,m2=0,m3=0,m4=0;
            try { m1 = Int32.Parse(maskedTextBox9.Text); }
            catch { m1 = 0; }
            try { m2 = Int32.Parse(maskedTextBox10.Text); }
            catch { m2 = 0; }
            try { m3 = Int32.Parse(maskedTextBox11.Text); }
            catch { m3 = 0; }
            try { m4 = Int32.Parse(maskedTextBox12.Text); }
            catch { m4 = 0; }
                      
            int r = m1 + m2 + m3 + m4;

            if (r != 100)
            {
                grpMutations.BackColor = Color.Red;
            }
            else
            {
                grpMutations.BackColor = grpUserInput.BackColor;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            txtRndSeed.Text = r.Next(0, 999999).ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                panelBackgroundColor.BackColor = colorDialog1.Color;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)form3.pictureBoxTemplateQ().Image;
            panelBackgroundColor.BackColor = bmp.GetPixel(0, 0);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panelBackgroundColor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string temp = comboBox8.Text;
            comboBox8.Text = "D" + GetStringDateTimeNow();
            if (temp == comboBox8.Text + "c") comboBox8.Text = comboBox8.Text + "d";
            if (temp == comboBox8.Text + "b") comboBox8.Text = comboBox8.Text + "c";
            if (temp == comboBox8.Text + "a") comboBox8.Text = comboBox8.Text + "b";
            if (temp == comboBox8.Text) comboBox8.Text = comboBox8.Text + "a";
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        public void openHelp(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            Form2 form2 = new Form2(clicked.Name);

            form2.ShowDialog();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            paused = false;
            //finish run
            btnContinue.Enabled = false;
            context.run = true;
            context.loopy = loopycache;
            this.Text = titleText + " - Running";
            lblCurrentState.Text = "Running";
            SwapRunState();
            graphic = GALoop(graphic);
            if (paused != true)
            
            //if loopy run next loop
            if (context.loopy != 0 && context.loopy != 1)
            {
                while (context.loopy > 0)
                {
                    //label21.Text = context.loopy.ToString();
                    int seed = (Int32.Parse(txtRndSeed.Text) + 1);
                    txtRndSeed.Text = seed.ToString();
                    initialise_All();
                    runGA();

                    saveImageOnLoopy();
                    context.loopy--;
                }
                //label21.Text = context.loopy.ToString();
                //comboBox7.Text = "0"; // loopy
                MessageBox.Show("The loopy run is complete");
            }
            if (paused != true)
            {
                btnStop_Click(null, null);
            }


        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            paused = false;
            this.Text = titleText + " - Stopping...";
            lblCurrentState.Text = "Stopping...";
            grpUserInput.Enabled = true;
            groupBox1.Enabled = true;
            groupBox6.Enabled = true;
            groupBox8.Enabled = true;
            btnStop.Enabled = false;
            btnContinue.Enabled = false;
            btnRun.Enabled = true;
            context.run = false;
            context.loopy = 0;
            chkSaveEvery.Checked = false;
            //initialise_All();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.LoadText(context.directorySaveStopImages, context.directorySaveGenImages, context.directorySaveLoopyImages);
            form4.ShowDialog();
            
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void labelCase2_Click(object sender, EventArgs e)
        {

        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }

    
    }

