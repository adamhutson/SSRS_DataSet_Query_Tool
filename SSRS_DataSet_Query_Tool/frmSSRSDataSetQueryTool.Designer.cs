namespace SSRS_DataSet_Query_Tool
{
    partial class frmSSRSDataSetQueryTool
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
            this.txtFolders = new System.Windows.Forms.TextBox();
            this.btn_GetFolder = new System.Windows.Forms.Button();
            this.fbdReportsFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFolders
            // 
            this.txtFolders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolders.Location = new System.Drawing.Point(12, 15);
            this.txtFolders.Name = "txtFolders";
            this.txtFolders.ReadOnly = true;
            this.txtFolders.Size = new System.Drawing.Size(730, 20);
            this.txtFolders.TabIndex = 1;
            this.txtFolders.Text = "Select a folder of SSRS files -->";
            // 
            // btn_GetFolder
            // 
            this.btn_GetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GetFolder.Location = new System.Drawing.Point(748, 13);
            this.btn_GetFolder.Name = "btn_GetFolder";
            this.btn_GetFolder.Size = new System.Drawing.Size(24, 23);
            this.btn_GetFolder.TabIndex = 0;
            this.btn_GetFolder.Text = "...";
            this.btn_GetFolder.UseVisualStyleBackColor = true;
            this.btn_GetFolder.Click += new System.EventHandler(this.btn_GetFolder_Click);
            // 
            // dgvResults
            // 
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(12, 41);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(760, 510);
            this.dgvResults.TabIndex = 2;
            // 
            // frmSSRSDataSetQueryTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.btn_GetFolder);
            this.Controls.Add(this.txtFolders);
            this.Name = "frmSSRSDataSetQueryTool";
            this.Text = "SSRS DataSet Query Tool";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFolders;
        private System.Windows.Forms.Button btn_GetFolder;
        private System.Windows.Forms.FolderBrowserDialog fbdReportsFolder;
        private System.Windows.Forms.DataGridView dgvResults;
    }
}

