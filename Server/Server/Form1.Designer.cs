namespace Server
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbServerInfo = new System.Windows.Forms.TextBox();
            this.btnServerStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbServerInfo
            // 
            this.tbServerInfo.Location = new System.Drawing.Point(12, 12);
            this.tbServerInfo.Multiline = true;
            this.tbServerInfo.Name = "tbServerInfo";
            this.tbServerInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbServerInfo.Size = new System.Drawing.Size(393, 309);
            this.tbServerInfo.TabIndex = 0;
            // 
            // btnServerStart
            // 
            this.btnServerStart.Location = new System.Drawing.Point(12, 344);
            this.btnServerStart.Name = "btnServerStart";
            this.btnServerStart.Size = new System.Drawing.Size(393, 23);
            this.btnServerStart.TabIndex = 1;
            this.btnServerStart.Text = "Server Start";
            this.btnServerStart.UseVisualStyleBackColor = true;
            this.btnServerStart.Click += new System.EventHandler(this.btnServerStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 389);
            this.Controls.Add(this.btnServerStart);
            this.Controls.Add(this.tbServerInfo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbServerInfo;
        private Button btnServerStart;
    }
}