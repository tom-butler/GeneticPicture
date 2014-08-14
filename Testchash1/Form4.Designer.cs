namespace GeneticPictureMaker
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.btnMasterDir = new System.Windows.Forms.Button();
            this.tbxMasterDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoopDir = new System.Windows.Forms.Button();
            this.btnGenDir = new System.Windows.Forms.Button();
            this.btnStopDir = new System.Windows.Forms.Button();
            this.tbxLoopDir = new System.Windows.Forms.TextBox();
            this.tbxGenDir = new System.Windows.Forms.TextBox();
            this.tbxStopDir = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.HelpMasterDir = new System.Windows.Forms.Button();
            this.HelpStopDir = new System.Windows.Forms.Button();
            this.HelpGenDir = new System.Windows.Forms.Button();
            this.HelpLoopDir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMasterDir
            // 
            this.btnMasterDir.Location = new System.Drawing.Point(609, 7);
            this.btnMasterDir.Name = "btnMasterDir";
            this.btnMasterDir.Size = new System.Drawing.Size(55, 23);
            this.btnMasterDir.TabIndex = 49;
            this.btnMasterDir.Text = "Browse";
            this.btnMasterDir.UseVisualStyleBackColor = true;
            this.btnMasterDir.Click += new System.EventHandler(this.SetSaveDir);
            // 
            // tbxMasterDir
            // 
            this.tbxMasterDir.Location = new System.Drawing.Point(93, 9);
            this.tbxMasterDir.Name = "tbxMasterDir";
            this.tbxMasterDir.Size = new System.Drawing.Size(510, 20);
            this.tbxMasterDir.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Master Directory";
            // 
            // btnLoopDir
            // 
            this.btnLoopDir.Location = new System.Drawing.Point(609, 84);
            this.btnLoopDir.Name = "btnLoopDir";
            this.btnLoopDir.Size = new System.Drawing.Size(55, 23);
            this.btnLoopDir.TabIndex = 46;
            this.btnLoopDir.Text = "Browse";
            this.btnLoopDir.UseVisualStyleBackColor = true;
            this.btnLoopDir.Click += new System.EventHandler(this.SetSaveDir);
            // 
            // btnGenDir
            // 
            this.btnGenDir.Location = new System.Drawing.Point(609, 59);
            this.btnGenDir.Name = "btnGenDir";
            this.btnGenDir.Size = new System.Drawing.Size(55, 23);
            this.btnGenDir.TabIndex = 46;
            this.btnGenDir.Text = "Browse";
            this.btnGenDir.UseVisualStyleBackColor = true;
            this.btnGenDir.Click += new System.EventHandler(this.SetSaveDir);
            // 
            // btnStopDir
            // 
            this.btnStopDir.Location = new System.Drawing.Point(609, 33);
            this.btnStopDir.Name = "btnStopDir";
            this.btnStopDir.Size = new System.Drawing.Size(55, 23);
            this.btnStopDir.TabIndex = 46;
            this.btnStopDir.Text = "Browse";
            this.btnStopDir.UseVisualStyleBackColor = true;
            this.btnStopDir.Click += new System.EventHandler(this.SetSaveDir);
            // 
            // tbxLoopDir
            // 
            this.tbxLoopDir.Location = new System.Drawing.Point(93, 86);
            this.tbxLoopDir.Name = "tbxLoopDir";
            this.tbxLoopDir.Size = new System.Drawing.Size(510, 20);
            this.tbxLoopDir.TabIndex = 1;
            // 
            // tbxGenDir
            // 
            this.tbxGenDir.Location = new System.Drawing.Point(93, 61);
            this.tbxGenDir.Name = "tbxGenDir";
            this.tbxGenDir.Size = new System.Drawing.Size(510, 20);
            this.tbxGenDir.TabIndex = 1;
            // 
            // tbxStopDir
            // 
            this.tbxStopDir.Location = new System.Drawing.Point(93, 35);
            this.tbxStopDir.Name = "tbxStopDir";
            this.tbxStopDir.Size = new System.Drawing.Size(510, 20);
            this.tbxStopDir.TabIndex = 1;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(13, 86);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(79, 13);
            this.label32.TabIndex = 0;
            this.label32.Text = "Save on Loopy";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(13, 63);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Save on Gen";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 38);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Save on Stop";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(319, 112);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 50;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // HelpMasterDir
            // 
            this.HelpMasterDir.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.HelpMasterDir.FlatAppearance.BorderSize = 0;
            this.HelpMasterDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HelpMasterDir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.HelpMasterDir.Location = new System.Drawing.Point(670, 7);
            this.HelpMasterDir.Name = "HelpMasterDir";
            this.HelpMasterDir.Size = new System.Drawing.Size(15, 22);
            this.HelpMasterDir.TabIndex = 57;
            this.HelpMasterDir.Text = "?";
            this.HelpMasterDir.UseVisualStyleBackColor = false;
            this.HelpMasterDir.Click += new System.EventHandler(this.openHelp);
            // 
            // HelpStopDir
            // 
            this.HelpStopDir.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.HelpStopDir.FlatAppearance.BorderSize = 0;
            this.HelpStopDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HelpStopDir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.HelpStopDir.Location = new System.Drawing.Point(670, 33);
            this.HelpStopDir.Name = "HelpStopDir";
            this.HelpStopDir.Size = new System.Drawing.Size(15, 22);
            this.HelpStopDir.TabIndex = 58;
            this.HelpStopDir.Text = "?";
            this.HelpStopDir.UseVisualStyleBackColor = false;
            this.HelpStopDir.Click += new System.EventHandler(this.openHelp);
            // 
            // HelpGenDir
            // 
            this.HelpGenDir.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.HelpGenDir.FlatAppearance.BorderSize = 0;
            this.HelpGenDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HelpGenDir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.HelpGenDir.Location = new System.Drawing.Point(670, 58);
            this.HelpGenDir.Name = "HelpGenDir";
            this.HelpGenDir.Size = new System.Drawing.Size(15, 22);
            this.HelpGenDir.TabIndex = 59;
            this.HelpGenDir.Text = "?";
            this.HelpGenDir.UseVisualStyleBackColor = false;
            this.HelpGenDir.Click += new System.EventHandler(this.openHelp);
            // 
            // HelpLoopDir
            // 
            this.HelpLoopDir.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.HelpLoopDir.FlatAppearance.BorderSize = 0;
            this.HelpLoopDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HelpLoopDir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.HelpLoopDir.Location = new System.Drawing.Point(670, 84);
            this.HelpLoopDir.Name = "HelpLoopDir";
            this.HelpLoopDir.Size = new System.Drawing.Size(15, 22);
            this.HelpLoopDir.TabIndex = 60;
            this.HelpLoopDir.Text = "?";
            this.HelpLoopDir.UseVisualStyleBackColor = false;
            this.HelpLoopDir.Click += new System.EventHandler(this.openHelp);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 141);
            this.Controls.Add(this.HelpLoopDir);
            this.Controls.Add(this.HelpGenDir);
            this.Controls.Add(this.HelpStopDir);
            this.Controls.Add(this.HelpMasterDir);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnMasterDir);
            this.Controls.Add(this.tbxMasterDir);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.btnLoopDir);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.btnGenDir);
            this.Controls.Add(this.tbxStopDir);
            this.Controls.Add(this.btnStopDir);
            this.Controls.Add(this.tbxGenDir);
            this.Controls.Add(this.tbxLoopDir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form4";
            this.Text = "Directories";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoopDir;
        private System.Windows.Forms.Button btnGenDir;
        private System.Windows.Forms.Button btnStopDir;
        private System.Windows.Forms.TextBox tbxLoopDir;
        private System.Windows.Forms.TextBox tbxGenDir;
        private System.Windows.Forms.TextBox tbxStopDir;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMasterDir;
        private System.Windows.Forms.TextBox tbxMasterDir;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button HelpMasterDir;
        private System.Windows.Forms.Button HelpStopDir;
        private System.Windows.Forms.Button HelpGenDir;
        private System.Windows.Forms.Button HelpLoopDir;
    }
}