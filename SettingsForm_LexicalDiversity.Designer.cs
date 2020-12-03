namespace LexicalDiversity
{
    partial class SettingsForm_LexicalDiversity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm_LexicalDiversity));
            this.OKButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.WordWindowTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mtldThresholdUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.mtldThresholdUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(141, 170);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(118, 40);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "MATTR Word Window Size = ";
            // 
            // WordWindowTextbox
            // 
            this.WordWindowTextbox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WordWindowTextbox.Location = new System.Drawing.Point(267, 43);
            this.WordWindowTextbox.Name = "WordWindowTextbox";
            this.WordWindowTextbox.Size = new System.Drawing.Size(89, 26);
            this.WordWindowTextbox.TabIndex = 0;
            this.WordWindowTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(61, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "MTLD Threshold = ";
            // 
            // mtldThresholdUpDown
            // 
            this.mtldThresholdUpDown.DecimalPlaces = 5;
            this.mtldThresholdUpDown.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtldThresholdUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.mtldThresholdUpDown.Location = new System.Drawing.Point(236, 94);
            this.mtldThresholdUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mtldThresholdUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.mtldThresholdUpDown.Name = "mtldThresholdUpDown";
            this.mtldThresholdUpDown.Size = new System.Drawing.Size(120, 25);
            this.mtldThresholdUpDown.TabIndex = 8;
            this.mtldThresholdUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtldThresholdUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            // 
            // SettingsForm_LexicalDiversity
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 222);
            this.Controls.Add(this.mtldThresholdUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.WordWindowTextbox);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm_LexicalDiversity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plugin Name";
            ((System.ComponentModel.ISupportInitialize)(this.mtldThresholdUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox WordWindowTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown mtldThresholdUpDown;
    }
}