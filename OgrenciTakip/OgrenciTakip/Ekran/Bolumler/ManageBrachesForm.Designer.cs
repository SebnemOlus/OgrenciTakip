using System;

namespace OgrenciTakip.Ekran.Bolumler
{
    partial class ManageBrachesForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewBranchhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BranchesDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BranchesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.addNewBranchhToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(744, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Close";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem1.Text = "|";
            // 
            // addNewBranchhToolStripMenuItem
            // 
            this.addNewBranchhToolStripMenuItem.Name = "addNewBranchhToolStripMenuItem";
            this.addNewBranchhToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.addNewBranchhToolStripMenuItem.Text = "Yeni Bölüm Ekle";
            this.addNewBranchhToolStripMenuItem.Click += new System.EventHandler(this.addNewBranchhToolStripMenuItem_Click);
            // 
            // BranchesDataGridView
            // 
            this.BranchesDataGridView.AllowUserToAddRows = false;
            this.BranchesDataGridView.AllowUserToDeleteRows = false;
            this.BranchesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BranchesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BranchesDataGridView.Location = new System.Drawing.Point(12, 27);
            this.BranchesDataGridView.Name = "BranchesDataGridView";
            this.BranchesDataGridView.ReadOnly = true;
            this.BranchesDataGridView.Size = new System.Drawing.Size(720, 433);
            this.BranchesDataGridView.TabIndex = 3;
            this.BranchesDataGridView.DoubleClick += new System.EventHandler(this.BranchesDataGridView_DoubleClick);
            // 
            // ManageBrachesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 462);
            this.Controls.Add(this.BranchesDataGridView);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ManageBrachesForm";
            this.Text = "Bölümleri Yönet";
            this.Load += new System.EventHandler(this.ManageBrachesForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BranchesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addNewBranchhToolStripMenuItem;
        private System.Windows.Forms.DataGridView BranchesDataGridView;
        
    }
}