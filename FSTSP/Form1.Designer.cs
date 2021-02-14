namespace FSTSP
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.numberOfCustomersTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.areaSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.buttonTSPtruck = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.areaSizeUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "km2";
            // 
            // numberOfCustomersTextBox
            // 
            this.numberOfCustomersTextBox.Location = new System.Drawing.Point(11, 69);
            this.numberOfCustomersTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numberOfCustomersTextBox.Name = "numberOfCustomersTextBox";
            this.numberOfCustomersTextBox.Size = new System.Drawing.Size(91, 20);
            this.numberOfCustomersTextBox.TabIndex = 2;
            this.numberOfCustomersTextBox.Text = "15";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Area";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Number of customers";
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(11, 108);
            this.buttonRun.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(126, 19);
            this.buttonRun.TabIndex = 5;
            this.buttonRun.Text = "FSTSP";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(176, 22);
            this.outputTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(416, 335);
            this.outputTextBox.TabIndex = 6;
            this.outputTextBox.Text = "";
            // 
            // areaSizeUpDown
            // 
            this.areaSizeUpDown.Location = new System.Drawing.Point(9, 24);
            this.areaSizeUpDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.areaSizeUpDown.Name = "areaSizeUpDown";
            this.areaSizeUpDown.Size = new System.Drawing.Size(90, 20);
            this.areaSizeUpDown.TabIndex = 7;
            this.areaSizeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonTSPtruck
            // 
            this.buttonTSPtruck.Location = new System.Drawing.Point(11, 142);
            this.buttonTSPtruck.Name = "buttonTSPtruck";
            this.buttonTSPtruck.Size = new System.Drawing.Size(126, 23);
            this.buttonTSPtruck.TabIndex = 8;
            this.buttonTSPtruck.Text = "TSP 1 truck";
            this.buttonTSPtruck.UseVisualStyleBackColor = true;
            this.buttonTSPtruck.Click += new System.EventHandler(this.buttonTSPtruck_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.buttonTSPtruck);
            this.Controls.Add(this.areaSizeUpDown);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numberOfCustomersTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainWindow";
            this.Text = "FSTSP v0.1";
            ((System.ComponentModel.ISupportInitialize)(this.areaSizeUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox numberOfCustomersTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.NumericUpDown areaSizeUpDown;
        private System.Windows.Forms.Button buttonTSPtruck;
    }
}

