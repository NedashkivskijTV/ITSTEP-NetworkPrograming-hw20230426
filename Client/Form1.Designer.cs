namespace Client
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
            this.tbIndex = new System.Windows.Forms.TextBox();
            this.dgvStreetsCollection = new System.Windows.Forms.DataGridView();
            this.btnSendIndex = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStreetsCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // tbIndex
            // 
            this.tbIndex.Location = new System.Drawing.Point(12, 26);
            this.tbIndex.Name = "tbIndex";
            this.tbIndex.PlaceholderText = "Enter street index";
            this.tbIndex.Size = new System.Drawing.Size(270, 23);
            this.tbIndex.TabIndex = 0;
            this.tbIndex.TextChanged += new System.EventHandler(this.tbIndex_TextChanged);
            // 
            // dgvStreetsCollection
            // 
            this.dgvStreetsCollection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStreetsCollection.Location = new System.Drawing.Point(12, 68);
            this.dgvStreetsCollection.Name = "dgvStreetsCollection";
            this.dgvStreetsCollection.RowTemplate.Height = 25;
            this.dgvStreetsCollection.Size = new System.Drawing.Size(754, 298);
            this.dgvStreetsCollection.TabIndex = 1;
            // 
            // btnSendIndex
            // 
            this.btnSendIndex.Location = new System.Drawing.Point(691, 25);
            this.btnSendIndex.Name = "btnSendIndex";
            this.btnSendIndex.Size = new System.Drawing.Size(75, 23);
            this.btnSendIndex.TabIndex = 2;
            this.btnSendIndex.Text = "Send Index";
            this.btnSendIndex.UseVisualStyleBackColor = true;
            this.btnSendIndex.Click += new System.EventHandler(this.btnSendIndex_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 379);
            this.Controls.Add(this.btnSendIndex);
            this.Controls.Add(this.dgvStreetsCollection);
            this.Controls.Add(this.tbIndex);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStreetsCollection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbIndex;
        private DataGridView dgvStreetsCollection;
        private Button btnSendIndex;
    }
}