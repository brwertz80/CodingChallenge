using CodingChallenge.Properties;

namespace CodingChallenge
{
    partial class CodingChallengeForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodingChallengeForm));
            this.importedDataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemOpenLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenRoute = new System.Windows.Forms.ToolStripMenuItem();
            this.txtAddressSearch = new System.Windows.Forms.TextBox();
            this.lblAddressSearch = new System.Windows.Forms.Label();
            this.lblNumRows = new System.Windows.Forms.Label();
            this.lblResults = new System.Windows.Forms.Label();
            this.resultsDataGridView = new System.Windows.Forms.DataGridView();
            this.resultsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.importedDataGridView)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // importedDataGridView
            // 
            this.importedDataGridView.AllowUserToAddRows = false;
            this.importedDataGridView.AllowUserToDeleteRows = false;
            this.importedDataGridView.AllowUserToResizeColumns = false;
            this.importedDataGridView.AllowUserToResizeRows = false;
            this.importedDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.importedDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.importedDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.importedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.importedDataGridView.ContextMenuStrip = this.contextMenuStrip;
            this.importedDataGridView.Location = new System.Drawing.Point(13, 62);
            this.importedDataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.importedDataGridView.MultiSelect = false;
            this.importedDataGridView.Name = "importedDataGridView";
            this.importedDataGridView.ReadOnly = true;
            this.importedDataGridView.RowHeadersWidth = 20;
            this.importedDataGridView.RowTemplate.Height = 24;
            this.importedDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.importedDataGridView.Size = new System.Drawing.Size(570, 345);
            this.importedDataGridView.TabIndex = 0;
            this.importedDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDown);
            this.importedDataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellMouseLeave);
            this.importedDataGridView.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseMove);
            this.importedDataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.ImportedDataGridView_DataBindingComplete);
            this.importedDataGridView.SelectionChanged += new System.EventHandler(this.ImportedDataGridView_SelectionChanged);
            this.importedDataGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataGridView_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenLocation,
            this.toolStripMenuItemOpenRoute});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(285, 52);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            // 
            // toolStripMenuItemOpenLocation
            // 
            this.toolStripMenuItemOpenLocation.Name = "toolStripMenuItemOpenLocation";
            this.toolStripMenuItemOpenLocation.Size = new System.Drawing.Size(284, 24);
            this.toolStripMenuItemOpenLocation.Text = global::CodingChallenge.Properties.Resources.ToolStripMenuLocation;
            this.toolStripMenuItemOpenLocation.Click += new System.EventHandler(this.ToolStripMenuItemOpenLocation_Click);
            // 
            // toolStripMenuItemOpenRoute
            // 
            this.toolStripMenuItemOpenRoute.Name = "toolStripMenuItemOpenRoute";
            this.toolStripMenuItemOpenRoute.Size = new System.Drawing.Size(284, 24);
            this.toolStripMenuItemOpenRoute.Text = global::CodingChallenge.Properties.Resources.ToolStripMenuRoute;
            this.toolStripMenuItemOpenRoute.Click += new System.EventHandler(this.ToolStripMenuItemOpenRoute_Click);
            // 
            // txtAddressSearch
            // 
            this.txtAddressSearch.Location = new System.Drawing.Point(156, 20);
            this.txtAddressSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAddressSearch.Name = "txtAddressSearch";
            this.txtAddressSearch.Size = new System.Drawing.Size(257, 28);
            this.txtAddressSearch.TabIndex = 1;
            this.txtAddressSearch.TextChanged += new System.EventHandler(this.TxtAddressSearch_TextChanged);
            // 
            // lblAddressSearch
            // 
            this.lblAddressSearch.AutoSize = true;
            this.lblAddressSearch.Location = new System.Drawing.Point(10, 23);
            this.lblAddressSearch.Name = "lblAddressSearch";
            this.lblAddressSearch.Size = new System.Drawing.Size(140, 21);
            this.lblAddressSearch.TabIndex = 2;
            this.lblAddressSearch.Text = "Search Addresses: ";
            // 
            // lblNumRows
            // 
            this.lblNumRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumRows.AutoSize = true;
            this.lblNumRows.Location = new System.Drawing.Point(422, 23);
            this.lblNumRows.Name = "lblNumRows";
            this.lblNumRows.Size = new System.Drawing.Size(0, 21);
            this.lblNumRows.TabIndex = 3;
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(10, 418);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(460, 21);
            this.lblResults.TabIndex = 4;
            this.lblResults.Text = "10 Closest Locations to the selected Location above (closest first)";
            // 
            // resultsDataGridView
            // 
            this.resultsDataGridView.AllowUserToAddRows = false;
            this.resultsDataGridView.AllowUserToDeleteRows = false;
            this.resultsDataGridView.AllowUserToResizeColumns = false;
            this.resultsDataGridView.AllowUserToResizeRows = false;
            this.resultsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.resultsDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.resultsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsDataGridView.ContextMenuStrip = this.contextMenuStrip;
            this.resultsDataGridView.Location = new System.Drawing.Point(13, 441);
            this.resultsDataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resultsDataGridView.MultiSelect = false;
            this.resultsDataGridView.Name = "resultsDataGridView";
            this.resultsDataGridView.ReadOnly = true;
            this.resultsDataGridView.RowHeadersWidth = 20;
            this.resultsDataGridView.RowTemplate.Height = 24;
            this.resultsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultsDataGridView.Size = new System.Drawing.Size(570, 265);
            this.resultsDataGridView.TabIndex = 5;
            this.resultsDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDown);
            this.resultsDataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellMouseLeave);
            this.resultsDataGridView.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseMove);
            this.resultsDataGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataGridView_MouseClick);
            // 
            // resultsBindingSource
            // 
            this.resultsBindingSource.DataSource = typeof(CodingChallenge.Location);
            // 
            // CodingChallengeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(596, 720);
            this.Controls.Add(this.resultsDataGridView);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.lblNumRows);
            this.Controls.Add(this.lblAddressSearch);
            this.Controls.Add(this.txtAddressSearch);
            this.Controls.Add(this.importedDataGridView);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodingChallengeForm";
            this.Text = "Coding Challenge Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.importedDataGridView)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView importedDataGridView;
        private System.Windows.Forms.TextBox txtAddressSearch;
        private System.Windows.Forms.Label lblAddressSearch;
        private System.Windows.Forms.Label lblNumRows;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.DataGridView resultsDataGridView;
        private System.Windows.Forms.BindingSource resultsBindingSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenLocation;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenRoute;
    }
}

