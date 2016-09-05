
//
// (C) Copyright 2004 by Autodesk, Inc. 
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to 
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using Autodesk.AutoCAD.DatabaseServices;

using AcDb = Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.Snoop.Forms
{
	/// <summary>
	/// This is a tabbed form that has all the information that can appear in an AcDbDatabase.
	/// It is divided into 3 pages (Symbol Tables, Dictionaries, and Other Database Data).
	/// </summary>
	public class Database : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button           m_bnOk;
        private System.Windows.Forms.TabControl       m_tabCtrl;
        private System.Windows.Forms.TabPage          m_tabSymTbls;
        private System.Windows.Forms.TabPage          m_tabDicts;
        private System.Windows.Forms.TabPage          m_tabDatabase;
        private System.Windows.Forms.TreeView         m_tvSymTbls;
        private System.Windows.Forms.ListView         m_lvSymTbl;
        private System.Windows.Forms.ColumnHeader     m_colSymTblField;
        private System.Windows.Forms.ColumnHeader     m_colSymTblValue;
        private System.Windows.Forms.TreeView         m_tvDicts;
        private System.Windows.Forms.ListView         m_lvDict;
        private System.Windows.Forms.ColumnHeader     m_colDictField;
        private System.Windows.Forms.ColumnHeader     m_colDictValue;
        private System.Windows.Forms.ListView         m_lvDb;
        private System.Windows.Forms.ColumnHeader     m_colDbField;
        private System.Windows.Forms.ColumnHeader     m_colDbValue;
        private System.Windows.Forms.ContextMenu      m_cntxMenuObjId;
        private System.Windows.Forms.MenuItem         m_mnuItemObjectDbInfo;
        private System.Windows.Forms.MenuItem         m_mnuItemBrowseReflection;
        private System.Windows.Forms.MenuItem         m_mnuItemSeparator1;
        private System.Windows.Forms.MenuItem         m_mnuItemAddToSnoopObjSet;
        private System.Windows.Forms.MenuItem         m_mnuItemRemoveFromSnoopObjSet;
        private System.Windows.Forms.MenuItem         m_mnuItemShowSnoopObjSet;
        
        private Snoop.Collectors.Objects              m_snoopCollector   = new Snoop.Collectors.Objects();
        private AcDb.Database                         m_db               = null;
        private bool                                  m_dictTabInited    = false;
        private bool                                  m_dbTabInited      = false;
        private AcDb.ObjectId                         m_curObjId;
        private TransactionHelper                     m_trans            = null;
        private ContextMenuStrip                      listViewContextMenuStrip;
        private ToolStripMenuItem                     copyToolStripMenuItem;
        private MenuItem                              m_mnuItemCopy;
        private ToolStrip                             toolStrip1;
        private ToolStripButton                       toolStripButton1;
        private ToolStripButton                       toolStripButton2;
        private PrintDialog                           m_printDialog;
        private System.Drawing.Printing.PrintDocument m_printDocument;
        private PrintPreviewDialog                    m_printPreviewDialog;
        private IContainer                            components;
        private Int32[]                               m_maxWidths;
        private Int32                                 m_currentPrintItem  = 0;
        private ToolStripButton                       toolStripButton3;
        private String                                selectedTab         = "SymTbl";

		public
		Database(AcDb.Database db, TransactionHelper tr)
		{
		    m_db = db;
		    m_trans = tr;
		    
			InitializeComponent();

                // We could initialize all the tabs here, but just for fun, we
                // will do delayed-initialization on the other two tabs.  This will
                // help us out in cases where we have a lot of data on each tab to show.
            InitSymbolTableTab();
		}
		

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void
		Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Database));
            this.m_bnOk = new System.Windows.Forms.Button();
            this.m_tabCtrl = new System.Windows.Forms.TabControl();
            this.m_tabSymTbls = new System.Windows.Forms.TabPage();
            this.m_lvSymTbl = new System.Windows.Forms.ListView();
            this.m_colSymTblField = new System.Windows.Forms.ColumnHeader();
            this.m_colSymTblValue = new System.Windows.Forms.ColumnHeader();
            this.listViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tvSymTbls = new System.Windows.Forms.TreeView();
            this.m_cntxMenuObjId = new System.Windows.Forms.ContextMenu();
            this.m_mnuItemCopy = new System.Windows.Forms.MenuItem();
            this.m_mnuItemObjectDbInfo = new System.Windows.Forms.MenuItem();
            this.m_mnuItemBrowseReflection = new System.Windows.Forms.MenuItem();
            this.m_mnuItemSeparator1 = new System.Windows.Forms.MenuItem();
            this.m_mnuItemAddToSnoopObjSet = new System.Windows.Forms.MenuItem();
            this.m_mnuItemRemoveFromSnoopObjSet = new System.Windows.Forms.MenuItem();
            this.m_mnuItemShowSnoopObjSet = new System.Windows.Forms.MenuItem();
            this.m_tabDicts = new System.Windows.Forms.TabPage();
            this.m_lvDict = new System.Windows.Forms.ListView();
            this.m_colDictField = new System.Windows.Forms.ColumnHeader();
            this.m_colDictValue = new System.Windows.Forms.ColumnHeader();
            this.m_tvDicts = new System.Windows.Forms.TreeView();
            this.m_tabDatabase = new System.Windows.Forms.TabPage();
            this.m_lvDb = new System.Windows.Forms.ListView();
            this.m_colDbField = new System.Windows.Forms.ColumnHeader();
            this.m_colDbValue = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.m_printDialog = new System.Windows.Forms.PrintDialog();
            this.m_printDocument = new System.Drawing.Printing.PrintDocument();
            this.m_printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.m_tabCtrl.SuspendLayout();
            this.m_tabSymTbls.SuspendLayout();
            this.listViewContextMenuStrip.SuspendLayout();
            this.m_tabDicts.SuspendLayout();
            this.m_tabDatabase.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_bnOk
            // 
            this.m_bnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_bnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_bnOk.Location = new System.Drawing.Point(374, 531);
            this.m_bnOk.Name = "m_bnOk";
            this.m_bnOk.Size = new System.Drawing.Size(75, 23);
            this.m_bnOk.TabIndex = 0;
            this.m_bnOk.Text = "OK";
            // 
            // m_tabCtrl
            // 
            this.m_tabCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabCtrl.Controls.Add(this.m_tabSymTbls);
            this.m_tabCtrl.Controls.Add(this.m_tabDicts);
            this.m_tabCtrl.Controls.Add(this.m_tabDatabase);
            this.m_tabCtrl.Location = new System.Drawing.Point(20, 37);
            this.m_tabCtrl.Name = "m_tabCtrl";
            this.m_tabCtrl.SelectedIndex = 0;
            this.m_tabCtrl.ShowToolTips = true;
            this.m_tabCtrl.Size = new System.Drawing.Size(800, 488);
            this.m_tabCtrl.TabIndex = 1;
            this.m_tabCtrl.SelectedIndexChanged += new System.EventHandler(this.TabSelected);
            // 
            // m_tabSymTbls
            // 
            this.m_tabSymTbls.Controls.Add(this.m_lvSymTbl);
            this.m_tabSymTbls.Controls.Add(this.m_tvSymTbls);
            this.m_tabSymTbls.Location = new System.Drawing.Point(4, 22);
            this.m_tabSymTbls.Name = "m_tabSymTbls";
            this.m_tabSymTbls.Size = new System.Drawing.Size(792, 462);
            this.m_tabSymTbls.TabIndex = 0;
            this.m_tabSymTbls.Text = "Symbol Tables";
            // 
            // m_lvSymTbl
            // 
            this.m_lvSymTbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvSymTbl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colSymTblField,
            this.m_colSymTblValue});
            this.m_lvSymTbl.ContextMenuStrip = this.listViewContextMenuStrip;
            this.m_lvSymTbl.FullRowSelect = true;
            this.m_lvSymTbl.GridLines = true;
            this.m_lvSymTbl.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvSymTbl.HideSelection = false;
            this.m_lvSymTbl.Location = new System.Drawing.Point(293, 16);
            this.m_lvSymTbl.MultiSelect = false;
            this.m_lvSymTbl.Name = "m_lvSymTbl";
            this.m_lvSymTbl.ShowItemToolTips = true;
            this.m_lvSymTbl.Size = new System.Drawing.Size(496, 432);
            this.m_lvSymTbl.TabIndex = 1;
            this.m_lvSymTbl.UseCompatibleStateImageBehavior = false;
            this.m_lvSymTbl.View = System.Windows.Forms.View.Details;
            this.m_lvSymTbl.DoubleClick += new System.EventHandler(this.DataItemSelected);
            this.m_lvSymTbl.Click += new System.EventHandler(this.DataItemSelected);
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
            // listViewContextMenuStrip
            // 
            this.listViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.listViewContextMenuStrip.Name = "listViewContextMenuStrip";
            this.listViewContextMenuStrip.Size = new System.Drawing.Size(100, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::MgdDbg.Properties.Resources.COPY1;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // m_tvSymTbls
            // 
            this.m_tvSymTbls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_tvSymTbls.ContextMenu = this.m_cntxMenuObjId;
            this.m_tvSymTbls.HideSelection = false;
            this.m_tvSymTbls.Location = new System.Drawing.Point(16, 16);
            this.m_tvSymTbls.Name = "m_tvSymTbls";
            this.m_tvSymTbls.ShowNodeToolTips = true;
            this.m_tvSymTbls.Size = new System.Drawing.Size(256, 432);
            this.m_tvSymTbls.TabIndex = 0;
            this.m_tvSymTbls.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TreeNodeSelected);
            this.m_tvSymTbls.AfterSelect += new TreeViewEventHandler(this.TreeNodeSelected);
            // 
            // m_cntxMenuObjId
            // 
            this.m_cntxMenuObjId.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_mnuItemCopy,
            this.m_mnuItemObjectDbInfo,
            this.m_mnuItemBrowseReflection,
            this.m_mnuItemSeparator1,
            this.m_mnuItemAddToSnoopObjSet,
            this.m_mnuItemRemoveFromSnoopObjSet,
            this.m_mnuItemShowSnoopObjSet});
            this.m_cntxMenuObjId.Popup += new System.EventHandler(this.OnContextMenuPopup);
            // 
            // m_mnuItemCopy
            // 
            this.m_mnuItemCopy.Index = 0;
            this.m_mnuItemCopy.Text = "Copy";
            this.m_mnuItemCopy.Click += new System.EventHandler(this.ContextMenuClick_Copy);
            // 
            // m_mnuItemObjectDbInfo
            // 
            this.m_mnuItemObjectDbInfo.Index = 1;
            this.m_mnuItemObjectDbInfo.Text = "Show ObjectID Info...";
            this.m_mnuItemObjectDbInfo.Click += new System.EventHandler(this.ContextMenuClick_ObjIdInfo);
            // 
            // m_mnuItemBrowseReflection
            // 
            this.m_mnuItemBrowseReflection.Index = 2;
            this.m_mnuItemBrowseReflection.Text = "Browse Using Reflection...";
            this.m_mnuItemBrowseReflection.Click += new System.EventHandler(this.ContextMenuClick_BrowseReflection);
            // 
            // m_mnuItemSeparator1
            // 
            this.m_mnuItemSeparator1.Index = 3;
            this.m_mnuItemSeparator1.Text = "-";
            // 
            // m_mnuItemAddToSnoopObjSet
            // 
            this.m_mnuItemAddToSnoopObjSet.Index = 4;
            this.m_mnuItemAddToSnoopObjSet.Text = "Add To Snoop Object Set";
            this.m_mnuItemAddToSnoopObjSet.Click += new System.EventHandler(this.ContextMenuClick_AddToSnoopObjSet);
            // 
            // m_mnuItemRemoveFromSnoopObjSet
            // 
            this.m_mnuItemRemoveFromSnoopObjSet.Index = 5;
            this.m_mnuItemRemoveFromSnoopObjSet.Text = "Remove From Snoop Object Set";
            this.m_mnuItemRemoveFromSnoopObjSet.Click += new System.EventHandler(this.ContextMenuClick_RemoveFromSnoopObjSet);
            // 
            // m_mnuItemShowSnoopObjSet
            // 
            this.m_mnuItemShowSnoopObjSet.Index = 6;
            this.m_mnuItemShowSnoopObjSet.Text = "Show Snoop Object Set...";
            this.m_mnuItemShowSnoopObjSet.Click += new System.EventHandler(this.ContextMenuClick_ShowSnoopObjSet);
            // 
            // m_tabDicts
            // 
            this.m_tabDicts.Controls.Add(this.m_lvDict);
            this.m_tabDicts.Controls.Add(this.m_tvDicts);
            this.m_tabDicts.Location = new System.Drawing.Point(4, 22);
            this.m_tabDicts.Name = "m_tabDicts";
            this.m_tabDicts.Size = new System.Drawing.Size(792, 462);
            this.m_tabDicts.TabIndex = 1;
            this.m_tabDicts.Text = "Dictionaries";
            // 
            // m_lvDict
            // 
            this.m_lvDict.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvDict.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colDictField,
            this.m_colDictValue});
            this.m_lvDict.ContextMenuStrip = this.listViewContextMenuStrip;
            this.m_lvDict.FullRowSelect = true;
            this.m_lvDict.GridLines = true;
            this.m_lvDict.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvDict.HideSelection = false;
            this.m_lvDict.Location = new System.Drawing.Point(288, 16);
            this.m_lvDict.MultiSelect = false;
            this.m_lvDict.Name = "m_lvDict";
            this.m_lvDict.ShowItemToolTips = true;
            this.m_lvDict.Size = new System.Drawing.Size(496, 432);
            this.m_lvDict.TabIndex = 3;
            this.m_lvDict.UseCompatibleStateImageBehavior = false;
            this.m_lvDict.View = System.Windows.Forms.View.Details;
            this.m_lvDict.DoubleClick += new System.EventHandler(this.DataItemSelected);
            this.m_lvDict.Click += new System.EventHandler(this.DataItemSelected);
            // 
            // m_colDictField
            // 
            this.m_colDictField.Text = "Field";
            this.m_colDictField.Width = 200;
            // 
            // m_colDictValue
            // 
            this.m_colDictValue.Text = "Value";
            this.m_colDictValue.Width = 300;
            // 
            // m_tvDicts
            // 
            this.m_tvDicts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_tvDicts.ContextMenu = this.m_cntxMenuObjId;
            this.m_tvDicts.FullRowSelect = true;
            this.m_tvDicts.HideSelection = false;
            this.m_tvDicts.Location = new System.Drawing.Point(16, 16);
            this.m_tvDicts.Name = "m_tvDicts";
            this.m_tvDicts.ShowNodeToolTips = true;
            this.m_tvDicts.Size = new System.Drawing.Size(256, 432);
            this.m_tvDicts.TabIndex = 2;
            this.m_tvDicts.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TreeNodeSelected);
            this.m_tvDicts.AfterSelect += new TreeViewEventHandler(this.TreeNodeSelected);
            // 
            // m_tabDatabase
            // 
            this.m_tabDatabase.Controls.Add(this.m_lvDb);
            this.m_tabDatabase.Location = new System.Drawing.Point(4, 22);
            this.m_tabDatabase.Name = "m_tabDatabase";
            this.m_tabDatabase.Size = new System.Drawing.Size(792, 462);
            this.m_tabDatabase.TabIndex = 2;
            this.m_tabDatabase.Text = "Database";
            // 
            // m_lvDb
            // 
            this.m_lvDb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvDb.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colDbField,
            this.m_colDbValue});
            this.m_lvDb.ContextMenuStrip = this.listViewContextMenuStrip;
            this.m_lvDb.FullRowSelect = true;
            this.m_lvDb.GridLines = true;
            this.m_lvDb.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvDb.Location = new System.Drawing.Point(16, 15);
            this.m_lvDb.MultiSelect = false;
            this.m_lvDb.Name = "m_lvDb";
            this.m_lvDb.ShowItemToolTips = true;
            this.m_lvDb.Size = new System.Drawing.Size(760, 432);
            this.m_lvDb.TabIndex = 4;
            this.m_lvDb.UseCompatibleStateImageBehavior = false;
            this.m_lvDb.View = System.Windows.Forms.View.Details;
            this.m_lvDb.DoubleClick += new System.EventHandler(this.DataItemSelected);
            this.m_lvDb.Click += new System.EventHandler(this.DataItemSelected);
            // 
            // m_colDbField
            // 
            this.m_colDbField.Text = "Field";
            this.m_colDbField.Width = 300;
            // 
            // m_colDbValue
            // 
            this.m_colDbValue.Text = "Value";
            this.m_colDbValue.Width = 500;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(832, 25);
            this.toolStrip1.TabIndex = 2;
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
            this.m_printPreviewDialog.Name = "m_printPreviewDialog";
            this.m_printPreviewDialog.Visible = false;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::MgdDbg.Properties.Resources.COPY1;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Copy To Clipboard";
            this.toolStripButton3.Click += new System.EventHandler(this.ContextMenuClick_Copy);
            // 
            // Database
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(832, 559);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.m_tabCtrl);
            this.Controls.Add(this.m_bnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(720, 250);
            this.Name = "Database";
            this.Text = "Snoop Database";
            this.m_tabCtrl.ResumeLayout(false);
            this.m_tabSymTbls.ResumeLayout(false);
            this.listViewContextMenuStrip.ResumeLayout(false);
            this.m_tabDicts.ResumeLayout(false);
            this.m_tabDatabase.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion
		
		/// <summary>
		/// Collect all the Symbol Table information and populate the tree control
		/// </summary>
		
		private void
		InitSymbolTableTab()
		{
            m_tvSymTbls.BeginUpdate();

            AddSymbolTableToTree("Block Table", m_db.BlockTableId);
            AddSymbolTableToTree("Dimension Style Table", m_db.DimStyleTableId);
            AddSymbolTableToTree("Layer Table", m_db.LayerTableId);
            AddSymbolTableToTree("Linetype Table", m_db.LinetypeTableId);
            AddSymbolTableToTree("Reg App Table", m_db.RegAppTableId);
            AddSymbolTableToTree("Text Style Table", m_db.TextStyleTableId);
            AddSymbolTableToTree("View Table", m_db.ViewTableId);
            AddSymbolTableToTree("Viewport Table", m_db.ViewportTableId);
            AddSymbolTableToTree("UCS Table", m_db.UcsTableId);
            
            m_tvSymTbls.Sorted = true;
            m_tvSymTbls.SelectedNode = m_tvSymTbls.Nodes[0];
            
            m_tvSymTbls.EndUpdate();
		}
		
		/// <summary>
		/// Add a single Symbol Table to the tree and iterate over all its SymbolTableRecords,
		/// adding each one of them as a sub-node
		/// </summary>
		/// <param name="tblName">Name of the table</param>
		/// <param name="tblId">ObjectId of the table</param>
		
        private void
        AddSymbolTableToTree(string tblName, AcDb.ObjectId tblId)
        {
            AcDb.DBObject tmpObj = m_trans.Transaction.GetObject(tblId, OpenMode.ForRead);
            SymbolTable tbl = tmpObj as SymbolTable;
            if (tbl != null) {
                TreeNode mainTblNode = new TreeNode(tblName);
                mainTblNode.Tag = tblId;
                m_tvSymTbls.Nodes.Add(mainTblNode);

                    // iterate over each TblRec in the SymbolTable
                foreach (AcDb.ObjectId tblRecId in tbl) {
                    TreeNode recNode = new TreeNode(m_trans.SymbolIdToName(tblRecId));
                    recNode.Tag = tblRecId;
                    mainTblNode.Nodes.Add(recNode);
                }
            }
        }
        
        /// <summary>
        /// Recursively iterate through all the dictionaries that are under the
        /// root "Named Objects Dictionary".
        /// </summary>
        private void
        InitDictionariesTab()
        {
            m_tvDicts.BeginUpdate();

            TreeNode rootNode = new TreeNode("Named Objects Dictionary");
            rootNode.Tag = m_db.NamedObjectsDictionaryId;
            m_tvDicts.Nodes.Add(rootNode);

            AddDictionaryToTree(m_db.NamedObjectsDictionaryId, rootNode, m_trans.Transaction);
            
            m_tvDicts.Sorted = true;
            rootNode.Expand();
            m_tvDicts.SelectedNode = m_tvDicts.Nodes[0];
            
            m_tvDicts.EndUpdate();
        }
        
        private void
        AddDictionaryToTree(AcDb.ObjectId dictId, TreeNode parentNode, Transaction tr)
        {
            // NOTE: when recursively processing items in a dictionary
            // we may encounter things that are not derived from DBDictionary.
            // In that case, the cast to type DBDictionary will fail and
            // we'll just return without adding any nested items.
            AcDb.DBObject tmpObj = tr.GetObject(dictId, OpenMode.ForRead);
            DBDictionary dbDict = tmpObj as DBDictionary;
            if (dbDict != null) {
                foreach (DictionaryEntry curEntry in dbDict) {
                    TreeNode newNode = new TreeNode((string)curEntry.Key);
                    newNode.Tag = curEntry.Value;
                    parentNode.Nodes.Add(newNode);
                        
                        // if this is a dictionary, it will recursively add
                        // all of its children to the tree
                    AddDictionaryToTree((AcDb.ObjectId)curEntry.Value, newNode, tr);   
                }
            }
        }
        
        /// <summary>
        /// Get all the data that is directly accessible from a Database
        /// </summary>
        private void
        InitDatabaseTab()
        {
            m_lvDb.BeginUpdate();

            m_snoopCollector.Collect(m_db);
            Snoop.Utils.Display(m_lvDb, m_snoopCollector);

            m_lvDb.EndUpdate();
        }

        #region Events
        private void
        TreeNodeSelected (object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
                // this call back is used for both the SymTbl and Dict tab pages,
                // but we need to know which so we can use the correct ListView
                // control.
            ListView lvCur = null;
            if (sender == m_tvSymTbls)
                lvCur = m_lvSymTbl;
            else if (sender == m_tvDicts)
                lvCur = m_lvDict;
            else
                Debug.Assert(false);    // Why not?
                        
            m_curObjId = (AcDb.ObjectId)e.Node.Tag;
            

            AcDb.DBObject dbObj = m_trans.Transaction.GetObject(m_curObjId, OpenMode.ForRead);

                // collect all the data about the object
            m_snoopCollector.Collect(dbObj);
            
                // now display it
            Snoop.Utils.Display(lvCur, m_snoopCollector);
        }

        private void
        TreeNodeSelected (object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            // this call back is used for both the SymTbl and Dict tab pages,
            // but we need to know which so we can use the correct ListView
            // control.
            ListView lvCur = null;
            if (sender == m_tvSymTbls)
                lvCur = m_lvSymTbl;
            else if (sender == m_tvDicts)
                lvCur = m_lvDict;
            else
                Debug.Assert(false);    // Why not?

            m_curObjId = (AcDb.ObjectId)e.Node.Tag;


            AcDb.DBObject dbObj = m_trans.Transaction.GetObject(m_curObjId, OpenMode.ForRead);

            // collect all the data about the object
            m_snoopCollector.Collect(dbObj);

            // now display it
            Snoop.Utils.Display(lvCur, m_snoopCollector);
        }

        private void
        DataItemSelected(object sender, System.EventArgs e)
        {
            ListView lvCur = sender as ListView;        // which ListView sent us this?

            Snoop.Utils.DataItemSelected(lvCur);
        }       

        /// <summary>
        /// Handle delayed initialization of non-current tabs.  In other words,
        /// they aren't initialized unless they are actually selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void
        TabSelected(object sender, System.EventArgs e)
        {
            TabControl tabCtrl = (TabControl)sender;
            
            if (tabCtrl.SelectedTab == m_tabSymTbls) {
                ;   // we've already inited in the constructor, so do nothing
                selectedTab = "SymTbl";
            }
            else if (tabCtrl.SelectedTab == m_tabDicts) {
                if (m_dictTabInited == false) {
                    m_dictTabInited = true;
                    InitDictionariesTab();
                }
                selectedTab = "Dict";
            }
            else if (tabCtrl.SelectedTab == m_tabDatabase) {
                if (m_dbTabInited == false) {
                    m_dbTabInited = true;
                    InitDatabaseTab();
                }
                selectedTab = "Db";
            }
            else
                Debug.Assert(false);
        }


        #region TreeView Context Menu Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        ContextMenuClick_ObjIdInfo(object sender, System.EventArgs e)
        {
            Snoop.Utils.ShowObjIdInfo(m_curObjId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        ContextMenuClick_Copy (object sender, System.EventArgs e)
        {
            if (selectedTab == "Dict") {
                if (m_tvDicts.SelectedNode != null) {
                    Utils.CopyToClipboard(m_lvDict);
                }
            }
            else if (selectedTab == "SymTbl") {
                if (m_tvSymTbls.SelectedNode != null) {
                    Utils.CopyToClipboard(m_lvSymTbl);
                }
            }
            else {
                Clipboard.Clear();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        ContextMenuClick_BrowseReflection(object sender, System.EventArgs e)
        {
            Snoop.Utils.BrowseReflection(m_curObjId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        ContextMenuClick_AddToSnoopObjSet(object sender, System.EventArgs e)
        {
            if (Snoop.Utils.CurrentSnoopSet != null) {
                Snoop.Utils.CurrentSnoopSet.AddToSet(m_curObjId);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        ContextMenuClick_RemoveFromSnoopObjSet(object sender, System.EventArgs e)
        {
            if (Snoop.Utils.CurrentSnoopSet != null) {
                Snoop.Utils.CurrentSnoopSet.RemoveFromSet(m_curObjId);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        ContextMenuClick_ShowSnoopObjSet(object sender, System.EventArgs e)
        {
            if (Snoop.Utils.CurrentSnoopSet != null) {
                Snoop.Forms.DBObjects form = new Snoop.Forms.DBObjects(Snoop.Utils.CurrentSnoopSet.Set, m_trans);
                form.Text = "Snoop Object Set";
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        OnContextMenuPopup(object sender, System.EventArgs e)
        {
            bool enabled = (Snoop.Utils.CurrentSnoopSet == null) ? false : true;
            m_mnuItemAddToSnoopObjSet.Enabled = enabled;
            m_mnuItemRemoveFromSnoopObjSet.Enabled = enabled;
            m_mnuItemShowSnoopObjSet.Enabled = enabled;
        }
        #endregion

        #region Copy Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        CopyToolStripMenuItem_Click (object sender, EventArgs e)
        {
            if (m_lvDb.SelectedItems.Count > 0) {
                Utils.CopyToClipboard(m_lvDb.SelectedItems[0]);
            }
            else if (m_lvDict.SelectedItems.Count > 0) {
                Utils.CopyToClipboard(m_lvDict.SelectedItems[0]);
            }
            else if (m_lvSymTbl.SelectedItems.Count > 0) {
                Utils.CopyToClipboard(m_lvSymTbl.SelectedItems[0]);
            }
            else {
                Clipboard.Clear();
            }
        }
        #endregion

        #region Print Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        PrintMenuItem_Click (object sender, EventArgs e)
        {           
            if (selectedTab == "SymTbl")
            {
                Utils.UpdatePrintSettings(m_printDocument, m_tvSymTbls, m_lvSymTbl, ref m_maxWidths);
                Utils.PrintMenuItemClick(m_printDialog, m_tvSymTbls);
            }
            else if (selectedTab == "Dict")
            {
                Utils.UpdatePrintSettings(m_printDocument, m_tvDicts, m_lvDict, ref m_maxWidths);
                Utils.PrintMenuItemClick(m_printDialog, m_tvDicts);
            }
            else if (selectedTab == "Db")
            {
                Utils.UpdatePrintSettings(m_lvDb, ref m_maxWidths);
                Utils.PrintMenuItemClick(m_printDialog);
            }                        
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        PrintPreviewMenuItem_Click (object sender, EventArgs e)
        {
            if (selectedTab == "SymTbl")
            {
                Utils.UpdatePrintSettings(m_printDocument, m_tvSymTbls, m_lvSymTbl, ref m_maxWidths);
                Utils.PrintPreviewMenuItemClick(m_printPreviewDialog, m_tvSymTbls);
            }
            else if (selectedTab == "Dict")
            {
                Utils.UpdatePrintSettings(m_printDocument, m_tvDicts, m_lvDict, ref m_maxWidths);
                Utils.PrintPreviewMenuItemClick(m_printPreviewDialog, m_tvDicts);                
            }
            else if (selectedTab == "Db")
            {
                Utils.UpdatePrintSettings(m_lvDb, ref m_maxWidths);
                Utils.PrintPreviewMenuItemClick(m_printPreviewDialog, m_lvDb);
            }                  
        }        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        PrintDocument_PrintPage (object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {          
            if (selectedTab == "SymTbl")
            {               
                m_currentPrintItem = Utils.Print(m_tvSymTbls.SelectedNode.Text, m_lvSymTbl, e, m_maxWidths[0], m_maxWidths[1], m_currentPrintItem);
            }
            else if (selectedTab == "Dict")
            {                
                m_currentPrintItem = Utils.Print(m_tvDicts.SelectedNode.Text, m_lvDict, e, m_maxWidths[0], m_maxWidths[1], m_currentPrintItem);
            }
            else if (selectedTab == "Db")
            {
                m_currentPrintItem = Utils.Print("", m_lvDb, e, m_maxWidths[0], m_maxWidths[1], m_currentPrintItem);               
            }
        }
        #endregion       
        #endregion
    }
}
