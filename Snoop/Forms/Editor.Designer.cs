namespace MgdDbg.Snoop.Forms {
    partial class Editor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.m_bnOK = new System.Windows.Forms.Button();
            this.m_bnCancel = new System.Windows.Forms.Button();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.m_tabSystem = new System.Windows.Forms.TabPage();
            this.m_lvCmds = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.listViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyListViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tvCmds = new System.Windows.Forms.TreeView();
            this.treeViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTreeViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tabClasses = new System.Windows.Forms.TabPage();
            this.m_labelNameSpc = new System.Windows.Forms.Label();
            this.m_rdBtnAdsk = new System.Windows.Forms.RadioButton();
            this.m_rdBtnSys = new System.Windows.Forms.RadioButton();
            this.m_bnCompList = new System.Windows.Forms.Button();
            this.m_lvClasses = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.m_btnNext = new System.Windows.Forms.Button();
            this.m_tvClasses = new System.Windows.Forms.TreeView();
            this.m_txtBoxSearch = new System.Windows.Forms.TextBox();
            this.m_labelSearch = new System.Windows.Forms.Label();
            this.m_tabDocs = new System.Windows.Forms.TabPage();
            this.m_lvDocs = new System.Windows.Forms.ListView();
            this.m_colSymTblField = new System.Windows.Forms.ColumnHeader();
            this.m_colSymTblValue = new System.Windows.Forms.ColumnHeader();
            this.m_tvDocs = new System.Windows.Forms.TreeView();
            this.m_tabControl = new System.Windows.Forms.TabControl();
            this.m_progressBar = new System.Windows.Forms.ProgressBar();
            this.m_printDialog = new System.Windows.Forms.PrintDialog();
            this.m_printDocument = new System.Drawing.Printing.PrintDocument();
            this.m_printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.m_tabSystem.SuspendLayout();
            this.listViewContextMenuStrip.SuspendLayout();
            this.treeViewContextMenuStrip.SuspendLayout();
            this.m_tabClasses.SuspendLayout();
            this.m_tabDocs.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_bnOK
            // 
            this.m_bnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_bnOK.Location = new System.Drawing.Point(490, 561);
            this.m_bnOK.Name = "m_bnOK";
            this.m_bnOK.Size = new System.Drawing.Size(75, 23);
            this.m_bnOK.TabIndex = 1;
            this.m_bnOK.Text = "OK";
            this.m_bnOK.UseVisualStyleBackColor = true;
            // 
            // m_bnCancel
            // 
            this.m_bnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bnCancel.Location = new System.Drawing.Point(580, 561);
            this.m_bnCancel.Name = "m_bnCancel";
            this.m_bnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_bnCancel.TabIndex = 2;
            this.m_bnCancel.Text = "Cancel";
            this.m_bnCancel.UseVisualStyleBackColor = true;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Field";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 300;
            // 
            // m_tabSystem
            // 
            this.m_tabSystem.Controls.Add(this.m_lvCmds);
            this.m_tabSystem.Controls.Add(this.m_tvCmds);
            this.m_tabSystem.Location = new System.Drawing.Point(4, 22);
            this.m_tabSystem.Name = "m_tabSystem";
            this.m_tabSystem.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabSystem.Size = new System.Drawing.Size(787, 473);
            this.m_tabSystem.TabIndex = 2;
            this.m_tabSystem.Text = "System";
            this.m_tabSystem.UseVisualStyleBackColor = true;
            // 
            // m_lvCmds
            // 
            this.m_lvCmds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvCmds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.m_lvCmds.ContextMenuStrip = this.listViewContextMenuStrip;
            this.m_lvCmds.FullRowSelect = true;
            this.m_lvCmds.GridLines = true;
            this.m_lvCmds.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvCmds.HideSelection = false;
            this.m_lvCmds.Location = new System.Drawing.Point(281, 20);
            this.m_lvCmds.MultiSelect = false;
            this.m_lvCmds.Name = "m_lvCmds";
            this.m_lvCmds.ShowItemToolTips = true;
            this.m_lvCmds.Size = new System.Drawing.Size(496, 437);
            this.m_lvCmds.TabIndex = 3;
            this.m_lvCmds.UseCompatibleStateImageBehavior = false;
            this.m_lvCmds.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Field";
            this.columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Value";
            this.columnHeader6.Width = 300;
            // 
            // listViewContextMenuStrip
            // 
            this.listViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyListViewToolStripMenuItem});
            this.listViewContextMenuStrip.Name = "listViewContextMenuStrip";
            this.listViewContextMenuStrip.Size = new System.Drawing.Size(100, 26);
            // 
            // copyListViewToolStripMenuItem
            // 
            this.copyListViewToolStripMenuItem.Image = global::MgdDbg.Properties.Resources.COPY1;
            this.copyListViewToolStripMenuItem.Name = "copyListViewToolStripMenuItem";
            this.copyListViewToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.copyListViewToolStripMenuItem.Text = "Copy";
            this.copyListViewToolStripMenuItem.Click += new System.EventHandler(this.CopyListViewToolStripMenuItem_Click);
            // 
            // m_tvCmds
            // 
            this.m_tvCmds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_tvCmds.ContextMenuStrip = this.treeViewContextMenuStrip;
            this.m_tvCmds.HideSelection = false;
            this.m_tvCmds.Location = new System.Drawing.Point(9, 20);
            this.m_tvCmds.Name = "m_tvCmds";
            this.m_tvCmds.ShowNodeToolTips = true;
            this.m_tvCmds.Size = new System.Drawing.Size(256, 437);
            this.m_tvCmds.TabIndex = 2;
            // 
            // treeViewContextMenuStrip
            // 
            this.treeViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTreeViewToolStripMenuItem});
            this.treeViewContextMenuStrip.Name = "treeViewContextMenuStrip";
            this.treeViewContextMenuStrip.Size = new System.Drawing.Size(100, 26);
            // 
            // copyTreeViewToolStripMenuItem
            // 
            this.copyTreeViewToolStripMenuItem.Image = global::MgdDbg.Properties.Resources.COPY1;
            this.copyTreeViewToolStripMenuItem.Name = "copyTreeViewToolStripMenuItem";
            this.copyTreeViewToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.copyTreeViewToolStripMenuItem.Text = "Copy";
            this.copyTreeViewToolStripMenuItem.Click += new System.EventHandler(this.CopyTreeViewToolStripMenuItem_Click);
            // 
            // m_tabClasses
            // 
            this.m_tabClasses.Controls.Add(this.m_labelNameSpc);
            this.m_tabClasses.Controls.Add(this.m_rdBtnAdsk);
            this.m_tabClasses.Controls.Add(this.m_rdBtnSys);
            this.m_tabClasses.Controls.Add(this.m_bnCompList);
            this.m_tabClasses.Controls.Add(this.m_lvClasses);
            this.m_tabClasses.Controls.Add(this.m_btnNext);
            this.m_tabClasses.Controls.Add(this.m_tvClasses);
            this.m_tabClasses.Controls.Add(this.m_txtBoxSearch);
            this.m_tabClasses.Controls.Add(this.m_labelSearch);
            this.m_tabClasses.Location = new System.Drawing.Point(4, 22);
            this.m_tabClasses.Name = "m_tabClasses";
            this.m_tabClasses.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabClasses.Size = new System.Drawing.Size(787, 473);
            this.m_tabClasses.TabIndex = 1;
            this.m_tabClasses.Text = "Classes";
            this.m_tabClasses.UseVisualStyleBackColor = true;
            // 
            // m_labelNameSpc
            // 
            this.m_labelNameSpc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_labelNameSpc.AutoSize = true;
            this.m_labelNameSpc.Location = new System.Drawing.Point(15, 497);
            this.m_labelNameSpc.Name = "m_labelNameSpc";
            this.m_labelNameSpc.Size = new System.Drawing.Size(134, 13);
            this.m_labelNameSpc.TabIndex = 17;
            this.m_labelNameSpc.Text = "NameSpace Enumeration :";
            // 
            // m_rdBtnAdsk
            // 
            this.m_rdBtnAdsk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_rdBtnAdsk.AutoSize = true;
            this.m_rdBtnAdsk.Enabled = false;
            this.m_rdBtnAdsk.Location = new System.Drawing.Point(249, 495);
            this.m_rdBtnAdsk.Name = "m_rdBtnAdsk";
            this.m_rdBtnAdsk.Size = new System.Drawing.Size(70, 17);
            this.m_rdBtnAdsk.TabIndex = 16;
            this.m_rdBtnAdsk.Text = "Autodesk";
            this.m_rdBtnAdsk.UseVisualStyleBackColor = true;
            this.m_rdBtnAdsk.CheckedChanged += new System.EventHandler(this.m_rdBtnAdsk_CheckedChanged);
            // 
            // m_rdBtnSys
            // 
            this.m_rdBtnSys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_rdBtnSys.AutoSize = true;
            this.m_rdBtnSys.Location = new System.Drawing.Point(162, 495);
            this.m_rdBtnSys.Name = "m_rdBtnSys";
            this.m_rdBtnSys.Size = new System.Drawing.Size(59, 17);
            this.m_rdBtnSys.TabIndex = 15;
            this.m_rdBtnSys.Text = "System";
            this.m_rdBtnSys.UseVisualStyleBackColor = true;
            this.m_rdBtnSys.CheckedChanged += new System.EventHandler(this.m_rdBtnSys_CheckedChanged);
            // 
            // m_bnCompList
            // 
            this.m_bnCompList.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnCompList.Location = new System.Drawing.Point(495, 472);
            this.m_bnCompList.Name = "m_bnCompList";
            this.m_bnCompList.Size = new System.Drawing.Size(208, 23);
            this.m_bnCompList.TabIndex = 7;
            this.m_bnCompList.Text = "Show Comparative List...";
            this.m_bnCompList.Click += new System.EventHandler(this.m_bnCompList_Click);
            // 
            // m_lvClasses
            // 
            this.m_lvClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvClasses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.m_lvClasses.ContextMenuStrip = this.listViewContextMenuStrip;
            this.m_lvClasses.FullRowSelect = true;
            this.m_lvClasses.GridLines = true;
            this.m_lvClasses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvClasses.HideSelection = false;
            this.m_lvClasses.Location = new System.Drawing.Point(457, 20);
            this.m_lvClasses.MultiSelect = false;
            this.m_lvClasses.Name = "m_lvClasses";
            this.m_lvClasses.ShowItemToolTips = true;
            this.m_lvClasses.Size = new System.Drawing.Size(320, 443);
            this.m_lvClasses.TabIndex = 3;
            this.m_lvClasses.UseCompatibleStateImageBehavior = false;
            this.m_lvClasses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Field";
            this.columnHeader3.Width = 118;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Value";
            this.columnHeader4.Width = 300;
            // 
            // m_btnNext
            // 
            this.m_btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnNext.Location = new System.Drawing.Point(344, 447);
            this.m_btnNext.Name = "m_btnNext";
            this.m_btnNext.Size = new System.Drawing.Size(52, 23);
            this.m_btnNext.TabIndex = 14;
            this.m_btnNext.Text = "Next";
            this.m_btnNext.UseVisualStyleBackColor = true;
            this.m_btnNext.Click += new System.EventHandler(this.m_btnNext_Click);
            // 
            // m_tvClasses
            // 
            this.m_tvClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_tvClasses.ContextMenuStrip = this.treeViewContextMenuStrip;
            this.m_tvClasses.HideSelection = false;
            this.m_tvClasses.Location = new System.Drawing.Point(9, 20);
            this.m_tvClasses.Name = "m_tvClasses";
            this.m_tvClasses.ShowNodeToolTips = true;
            this.m_tvClasses.Size = new System.Drawing.Size(430, 418);
            this.m_tvClasses.TabIndex = 2;
            // 
            // m_txtBoxSearch
            // 
            this.m_txtBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_txtBoxSearch.Location = new System.Drawing.Point(140, 449);
            this.m_txtBoxSearch.Name = "m_txtBoxSearch";
            this.m_txtBoxSearch.Size = new System.Drawing.Size(198, 20);
            this.m_txtBoxSearch.TabIndex = 12;
            // 
            // m_labelSearch
            // 
            this.m_labelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_labelSearch.AutoSize = true;
            this.m_labelSearch.Location = new System.Drawing.Point(15, 452);
            this.m_labelSearch.Name = "m_labelSearch";
            this.m_labelSearch.Size = new System.Drawing.Size(119, 13);
            this.m_labelSearch.TabIndex = 13;
            this.m_labelSearch.Text = "Dynamic Class Search :";
            // 
            // m_tabDocs
            // 
            this.m_tabDocs.Controls.Add(this.m_lvDocs);
            this.m_tabDocs.Controls.Add(this.m_tvDocs);
            this.m_tabDocs.Location = new System.Drawing.Point(4, 22);
            this.m_tabDocs.Name = "m_tabDocs";
            this.m_tabDocs.Padding = new System.Windows.Forms.Padding(3);
            this.m_tabDocs.Size = new System.Drawing.Size(787, 473);
            this.m_tabDocs.TabIndex = 0;
            this.m_tabDocs.Text = "Documents";
            this.m_tabDocs.UseVisualStyleBackColor = true;
            // 
            // m_lvDocs
            // 
            this.m_lvDocs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvDocs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colSymTblField,
            this.m_colSymTblValue});
            this.m_lvDocs.ContextMenuStrip = this.listViewContextMenuStrip;
            this.m_lvDocs.FullRowSelect = true;
            this.m_lvDocs.GridLines = true;
            this.m_lvDocs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvDocs.HideSelection = false;
            this.m_lvDocs.Location = new System.Drawing.Point(275, 20);
            this.m_lvDocs.MultiSelect = false;
            this.m_lvDocs.Name = "m_lvDocs";
            this.m_lvDocs.Size = new System.Drawing.Size(496, 447);
            this.m_lvDocs.TabIndex = 3;
            this.m_lvDocs.UseCompatibleStateImageBehavior = false;
            this.m_lvDocs.View = System.Windows.Forms.View.Details;
            // 
            // m_colSymTblField
            // 
            this.m_colSymTblField.Text = "Field";
            this.m_colSymTblField.Width = 200;
            // 
            // m_colSymTblValue
            // 
            this.m_colSymTblValue.Text = "Value";
            this.m_colSymTblValue.Width = 300;
            // 
            // m_tvDocs
            // 
            this.m_tvDocs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_tvDocs.ContextMenuStrip = this.treeViewContextMenuStrip;
            this.m_tvDocs.HideSelection = false;
            this.m_tvDocs.Location = new System.Drawing.Point(9, 20);
            this.m_tvDocs.Name = "m_tvDocs";
            this.m_tvDocs.ShowNodeToolTips = true;
            this.m_tvDocs.Size = new System.Drawing.Size(256, 447);
            this.m_tvDocs.TabIndex = 2;
            // 
            // m_tabControl
            // 
            this.m_tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabControl.Controls.Add(this.m_tabDocs);
            this.m_tabControl.Controls.Add(this.m_tabSystem);
            this.m_tabControl.Controls.Add(this.m_tabClasses);
            this.m_tabControl.Location = new System.Drawing.Point(12, 38);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.Size = new System.Drawing.Size(795, 499);
            this.m_tabControl.TabIndex = 0;
            this.m_tabControl.SelectedIndexChanged += new System.EventHandler(this.TabSelected);
            // 
            // m_progressBar
            // 
            this.m_progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_progressBar.Location = new System.Drawing.Point(12, 543);
            this.m_progressBar.Maximum = 700;
            this.m_progressBar.Name = "m_progressBar";
            this.m_progressBar.Size = new System.Drawing.Size(443, 12);
            this.m_progressBar.Step = 1;
            this.m_progressBar.TabIndex = 3;
            this.m_progressBar.Visible = false;
            // 
            // m_printDialog
            // 
            this.m_printDialog.Document = this.m_printDocument;
            this.m_printDialog.UseEXDialog = true;
            // 
            // m_printDocument
            // 
            this.m_printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument_PrintPage);
            // 
            // m_printPreviewDialog
            // 
            this.m_printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.m_printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.m_printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.m_printPreviewDialog.Document = this.m_printDocument;
            this.m_printPreviewDialog.Enabled = true;
            this.m_printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("m_printPreviewDialog.Icon")));
            this.m_printPreviewDialog.Name = "printPreviewDialog1";
            this.m_printPreviewDialog.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(819, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::MgdDbg.Properties.Resources.Print1;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Print";
            this.toolStripButton1.Click += new System.EventHandler(this.PrintMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::MgdDbg.Properties.Resources.Preview1;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Print Preview";
            this.toolStripButton2.Click += new System.EventHandler(this.PrintPreviewMenuItem_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::MgdDbg.Properties.Resources.COPY1;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Copy To Clipboard";
            this.toolStripButton3.Click += new System.EventHandler(this.CopyTreeViewToolStripMenuItem_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 596);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.m_tabControl);
            this.Controls.Add(this.m_progressBar);
            this.Controls.Add(this.m_bnOK);
            this.Controls.Add(this.m_bnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(575, 225);
            this.Name = "Editor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snoop Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_FormClosing);
            this.m_tabSystem.ResumeLayout(false);
            this.listViewContextMenuStrip.ResumeLayout(false);
            this.treeViewContextMenuStrip.ResumeLayout(false);
            this.m_tabClasses.ResumeLayout(false);
            this.m_tabClasses.PerformLayout();
            this.m_tabDocs.ResumeLayout(false);
            this.m_tabControl.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_bnOK;
        private System.Windows.Forms.Button m_bnCancel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage m_tabSystem;
        private System.Windows.Forms.ListView m_lvCmds;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TreeView m_tvCmds;
        private System.Windows.Forms.TabPage m_tabClasses;
        private System.Windows.Forms.Label m_labelNameSpc;
        private System.Windows.Forms.RadioButton m_rdBtnAdsk;
        private System.Windows.Forms.RadioButton m_rdBtnSys;
        private System.Windows.Forms.Button m_bnCompList;
        private System.Windows.Forms.ListView m_lvClasses;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button m_btnNext;
        private System.Windows.Forms.TreeView m_tvClasses;
        private System.Windows.Forms.TextBox m_txtBoxSearch;
        private System.Windows.Forms.Label m_labelSearch;
        private System.Windows.Forms.TabPage m_tabDocs;
        private System.Windows.Forms.ListView m_lvDocs;
        private System.Windows.Forms.ColumnHeader m_colSymTblField;
        private System.Windows.Forms.ColumnHeader m_colSymTblValue;
        private System.Windows.Forms.TreeView m_tvDocs;
        private System.Windows.Forms.TabControl m_tabControl;
        private System.Windows.Forms.ProgressBar m_progressBar;
        private System.Windows.Forms.ContextMenuStrip listViewContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip treeViewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyListViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTreeViewToolStripMenuItem;
        private System.Windows.Forms.PrintDialog m_printDialog;
        private System.Windows.Forms.PrintPreviewDialog m_printPreviewDialog;
        private System.Drawing.Printing.PrintDocument m_printDocument;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}
