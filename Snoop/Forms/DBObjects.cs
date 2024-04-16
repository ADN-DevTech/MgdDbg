
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
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;

using AcDb = Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.Snoop.Forms
{
	/// <summary>
	/// Summary description for DBObjects.
	/// </summary>
	public class DBObjects : System.Windows.Forms.Form
	{
	        // member vars
        protected System.Windows.Forms.ListView             m_lvData;
        protected System.Windows.Forms.Button               m_bnOK;
        protected System.Windows.Forms.ColumnHeader         m_lvCol_label;
        protected System.Windows.Forms.ColumnHeader         m_lvCol_value;
        protected System.Windows.Forms.TreeView             m_tvObjs;
        protected System.Windows.Forms.ContextMenuStrip          m_cntxMenuObjId;
        protected System.Windows.Forms.ToolStripMenuItem             m_mnuItemObjectDbInfo;
        protected System.Windows.Forms.ToolStripMenuItem             m_mnuItemBrowseReflection;
        protected System.Windows.Forms.ToolStripMenuItem             m_mnuItemSeparator1;
        protected System.Windows.Forms.ToolStripMenuItem             m_mnuItemAddToSnoopObjSet;
        protected System.Windows.Forms.ToolStripMenuItem             m_mnuItemRemoveFromSnoopObjSet;
        protected System.Windows.Forms.ToolStripMenuItem             m_mnuItemShowSnoopObjSet;
       
        protected Snoop.Collectors.Objects                  m_snoopCollector           = new Snoop.Collectors.Objects();
        protected AcDb.ObjectId                             m_curObjId;
        protected TransactionHelper                         m_trans                    = null;
        private   ContextMenuStrip                          listViewContextMenuStrip;
        private   ToolStripMenuItem                         copyToolStripMenuItem;
        private   ToolStripMenuItem                                  m_mnuItemCopy;
        private   ToolStrip                                 toolStrip1;
        private   ToolStripButton                           toolStripButton1;
        private   ToolStripButton                           toolStripButton2;
        private   PrintDialog                               m_printDialog;
        private   PrintPreviewDialog                        m_printPreviewDialog;
        private   System.Drawing.Printing.PrintDocument     m_printDocument;
        private   IContainer                                components;
        private   Int32[]                                   m_maxWidths;
        private   ToolStripButton                           toolStripButton3;
        private   Int32                                     m_currentPrintItem         = 0;
		
		protected
		DBObjects(TransactionHelper tr)
		{
		    m_trans = tr;
		    
		        // this constructor is for derived classes to call
            InitializeComponent();
		}

		public
		DBObjects(AcDb.ObjectIdCollection objIds, TransactionHelper tr)
		{
		    m_trans = tr;
		    
			    // Required for Windows Form Designer support
			InitializeComponent();

            m_tvObjs.BeginUpdate();

            AddObjectIdCollectionToTree(objIds);
            m_tvObjs.ExpandAll();
            
            m_tvObjs.EndUpdate();
   		}

		public
		DBObjects(ObjectId[] objIds, TransactionHelper tr)
		{
		    m_trans = tr;
		    
			    // Required for Windows Form Designer support
			InitializeComponent();

            m_tvObjs.BeginUpdate();

            AddObjectIdCollectionToTree(objIds);
            m_tvObjs.ExpandAll();
            
            m_tvObjs.EndUpdate();
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
		protected void
		InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBObjects));
            this.m_tvObjs = new System.Windows.Forms.TreeView();
            this.m_cntxMenuObjId = new System.Windows.Forms.ContextMenuStrip();
            this.m_mnuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuItemObjectDbInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuItemBrowseReflection = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuItemSeparator1 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuItemAddToSnoopObjSet = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuItemRemoveFromSnoopObjSet = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuItemShowSnoopObjSet = new System.Windows.Forms.ToolStripMenuItem();
            this.m_lvData = new System.Windows.Forms.ListView();
            this.m_lvCol_label = new System.Windows.Forms.ColumnHeader();
            this.m_lvCol_value = new System.Windows.Forms.ColumnHeader();
            this.listViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_bnOK = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.m_printDialog = new System.Windows.Forms.PrintDialog();
            this.m_printDocument = new System.Drawing.Printing.PrintDocument();
            this.m_printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.listViewContextMenuStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tvObjs
            // 
            this.m_tvObjs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_tvObjs.ContextMenuStrip = this.m_cntxMenuObjId;
            this.m_tvObjs.HideSelection = false;
            this.m_tvObjs.Location = new System.Drawing.Point(20, 41);
            this.m_tvObjs.Name = "m_tvObjs";
            this.m_tvObjs.ShowNodeToolTips = true;
            this.m_tvObjs.Size = new System.Drawing.Size(240, 459);
            this.m_tvObjs.TabIndex = 0;                   
            this.m_tvObjs.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TreeNodeSelected);
            this.m_tvObjs.AfterSelect += new TreeViewEventHandler(this.TreeNodeSelected);
            // 
            // m_cntxMenuObjId
            // 
            MenuStrip ms  = new MenuStrip();           


            this.ContextMenuStrip = m_cntxMenuObjId;
     
            ms.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.m_mnuItemCopy,
            this.m_mnuItemObjectDbInfo,
            this.m_mnuItemBrowseReflection,
            this.m_mnuItemSeparator1,
            this.m_mnuItemAddToSnoopObjSet,
            this.m_mnuItemRemoveFromSnoopObjSet,
            this.m_mnuItemShowSnoopObjSet
            });

            this.m_cntxMenuObjId.Opening += new System.ComponentModel.CancelEventHandler(this.OnContextMenuPopup);
            // 
            // m_mnuItemCopy
            // 
           
            this.m_mnuItemCopy.Text = "Copy";
            this.m_mnuItemCopy.Click += new System.EventHandler(this.ContextMenuClick_Copy);
            // 
            // m_mnuItemObjectDbInfo
            // 
            
            this.m_mnuItemObjectDbInfo.Text = "Show ObjectID Info...";
            this.m_mnuItemObjectDbInfo.Click += new System.EventHandler(this.ContextMenuClick_ObjIdInfo);
            // 
            // m_mnuItemBrowseReflection
            // 
            
            this.m_mnuItemBrowseReflection.Text = "Browse Using Reflection...";
            this.m_mnuItemBrowseReflection.Click += new System.EventHandler(this.ContextMenuClick_BrowseReflection);
            // 
            // m_mnuItemSeparator1
            // 
           
            this.m_mnuItemSeparator1.Text = "-";
            // 
            // m_mnuItemAddToSnoopObjSet
            // 
           
            this.m_mnuItemAddToSnoopObjSet.Text = "Add To Snoop Object Set";
            this.m_mnuItemAddToSnoopObjSet.Click += new System.EventHandler(this.ContextMenuClick_AddToSnoopObjSet);
            // 
            // m_mnuItemRemoveFromSnoopObjSet
            // 
           
            this.m_mnuItemRemoveFromSnoopObjSet.Text = "Remove From Snoop Object Set";
            this.m_mnuItemRemoveFromSnoopObjSet.Click += new System.EventHandler(this.ContextMenuClick_RemoveFromSnoopObjSet);
            // 
            // m_mnuItemShowSnoopObjSet
            // 
            
            this.m_mnuItemShowSnoopObjSet.Text = "Show Snoop Object Set...";
            this.m_mnuItemShowSnoopObjSet.Click += new System.EventHandler(this.ContextMenuClick_ShowSnoopObjSet);
            // 
            // m_lvData
            // 
            this.m_lvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvData.AutoArrange = false;
            this.m_lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_lvCol_label,
            this.m_lvCol_value});
            this.m_lvData.ContextMenuStrip = this.listViewContextMenuStrip;
            this.m_lvData.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_lvData.FullRowSelect = true;
            this.m_lvData.GridLines = true;
            this.m_lvData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvData.HideSelection = false;
            this.m_lvData.Location = new System.Drawing.Point(276, 41);
            this.m_lvData.MultiSelect = false;
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.ShowItemToolTips = true;
            this.m_lvData.Size = new System.Drawing.Size(504, 459);
            this.m_lvData.TabIndex = 1;
            this.m_lvData.UseCompatibleStateImageBehavior = false;
            this.m_lvData.View = System.Windows.Forms.View.Details;
            this.m_lvData.DoubleClick += new System.EventHandler(this.DataItemSelected);
            this.m_lvData.Click += new System.EventHandler(this.DataItemSelected);
            // 
            // m_lvCol_label
            // 
            this.m_lvCol_label.Text = "Field";
            this.m_lvCol_label.Width = 200;
            // 
            // m_lvCol_value
            // 
            this.m_lvCol_value.Text = "Value";
            this.m_lvCol_value.Width = 300;
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
            // m_bnOK
            // 
            this.m_bnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_bnOK.Location = new System.Drawing.Point(359, 517);
            this.m_bnOK.Name = "m_bnOK";
            this.m_bnOK.Size = new System.Drawing.Size(75, 23);
            this.m_bnOK.TabIndex = 2;
            this.m_bnOK.Text = "OK";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 3;
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
            this.m_printPreviewDialog.Name = "printPreviewDialog1";
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
            // DBObjects
            // 
            this.AcceptButton = this.m_bnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_bnOK;
            this.ClientSize = new System.Drawing.Size(792, 548);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.m_bnOK);
            this.Controls.Add(this.m_lvData);
            this.Controls.Add(this.m_tvObjs);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(575, 225);
            this.Name = "DBObjects";
            this.ShowInTaskbar = false;
            this.Text = "Snoop Objects";
            this.listViewContextMenuStrip.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objIds"></param>
        protected void
        AddObjectIdCollectionToTree(AcDb.ObjectIdCollection objIds)
        {
                // initialize the tree control with the ObjectId of each item		
            for (int i=0; i<objIds.Count; i++) {
                TreeNode tmpNode = new TreeNode(MgdDbg.Utils.AcadUi.ObjToTypeAndHandleStr(objIds[i]));
                tmpNode.Tag = objIds[i];
                m_tvObjs.Nodes.Add(tmpNode);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objIds"></param>
        protected void
        AddObjectIdCollectionToTree(ObjectId[] objIds)
        {
                // initialize the tree control with the ObjectId of each item		
            for (int i=0; i<objIds.Length; i++) {
                TreeNode tmpNode = new TreeNode(MgdDbg.Utils.AcadUi.ObjToTypeAndHandleStr(objIds[i]));
                tmpNode.Tag = objIds[i];
                m_tvObjs.Nodes.Add(tmpNode);
            }
        }


        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void
        TreeNodeSelected(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null) {
                m_curObjId = (AcDb.ObjectId)e.Node.Tag;

                AcDb.DBObject dbObj = m_trans.Transaction.GetObject(m_curObjId, OpenMode.ForRead);

                    // collect the data about this object
                m_snoopCollector.Collect(dbObj);
                
                    // display it
                Snoop.Utils.Display(m_lvData, m_snoopCollector);
            }
            else {
                m_curObjId = AcDb.ObjectId.Null;
                m_lvData.Items.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void
        TreeNodeSelected (object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Tag != null) {
                m_curObjId = (AcDb.ObjectId)e.Node.Tag;

                AcDb.DBObject dbObj = m_trans.Transaction.GetObject(m_curObjId, OpenMode.ForRead);

                // collect the data about this object
                m_snoopCollector.Collect(dbObj);

                // display it
                Snoop.Utils.Display(m_lvData, m_snoopCollector);
            }
            else {
                m_curObjId = AcDb.ObjectId.Null;
                m_lvData.Items.Clear();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void
        DataItemSelected(object sender, System.EventArgs e)
        {
            Snoop.Utils.DataItemSelected(m_lvData);
        }

        #region TreeView Context Menu Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void
        ContextMenuClick_Copy (object sender, System.EventArgs e)
        {
            if (m_tvObjs.SelectedNode != null) {
                Utils.CopyToClipboard(m_lvData);
            }           
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void
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
        OnContextMenuPopup(object sender, System.ComponentModel.CancelEventArgs e)
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
        CopyToolStripMenuItem_Click (object sender, System.EventArgs e)
        {
            if (m_lvData.SelectedItems.Count > 0) {
                Utils.CopyToClipboard(m_lvData.SelectedItems[0]);
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
        PrintDocument_PrintPage (object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            m_currentPrintItem = Utils.Print(m_tvObjs.SelectedNode.Text, m_lvData, e, m_maxWidths[0], m_maxWidths[1], m_currentPrintItem);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        PrintMenuItem_Click (object sender, EventArgs e)
        {
            Utils.UpdatePrintSettings(m_printDocument, m_tvObjs, m_lvData, ref m_maxWidths);
            Utils.PrintMenuItemClick(m_printDialog, m_tvObjs);           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        PrintPreviewMenuItem_Click (object sender, EventArgs e)
        {
            Utils.UpdatePrintSettings(m_printDocument, m_tvObjs, m_lvData, ref m_maxWidths);
            Utils.PrintPreviewMenuItemClick(m_printPreviewDialog, m_tvObjs);
        }
        #endregion

        #endregion
    }
}
