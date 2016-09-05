
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
	public class XrefGraph : System.Windows.Forms.Form
	{
	        // member vars
        protected System.Windows.Forms.ListView         m_lvData;
        protected System.Windows.Forms.Button           m_bnOK;
        protected System.Windows.Forms.ColumnHeader     m_lvCol_label;
        protected System.Windows.Forms.ColumnHeader     m_lvCol_value;
        protected System.Windows.Forms.TreeView         m_tvObjs;
        protected System.Windows.Forms.MenuItem         m_mnuItemBrowseReflection;
        protected System.Windows.Forms.ContextMenu      m_cntxMenuTree;
       
        protected Snoop.Collectors.Objects              m_snoopCollector = new Snoop.Collectors.Objects();
        protected AcDb.XrefGraph                        m_xrefGraph;
        protected object                                m_curObj = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		protected System.ComponentModel.Container components = null;
		
		protected
		XrefGraph()
		{
		        // this constructor is for derived classes to call
            InitializeComponent();
		}

		public
		XrefGraph(AcDb.XrefGraph xrefGraph)
		{
            m_xrefGraph = xrefGraph;

			    // Required for Windows Form Designer support
			InitializeComponent();

            m_tvObjs.BeginUpdate();

            AddGraphToTree(m_xrefGraph);
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
            this.m_tvObjs = new System.Windows.Forms.TreeView();
            this.m_cntxMenuTree = new System.Windows.Forms.ContextMenu();
            this.m_mnuItemBrowseReflection = new System.Windows.Forms.MenuItem();
            this.m_lvData = new System.Windows.Forms.ListView();
            this.m_lvCol_label = new System.Windows.Forms.ColumnHeader();
            this.m_lvCol_value = new System.Windows.Forms.ColumnHeader();
            this.m_bnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_tvObjs
            // 
            this.m_tvObjs.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left);
            this.m_tvObjs.ContextMenu = this.m_cntxMenuTree;
            this.m_tvObjs.HideSelection = false;
            this.m_tvObjs.ImageIndex = -1;
            this.m_tvObjs.Location = new System.Drawing.Point(16, 16);
            this.m_tvObjs.Name = "m_tvObjs";
            this.m_tvObjs.SelectedImageIndex = -1;
            this.m_tvObjs.Size = new System.Drawing.Size(240, 472);
            this.m_tvObjs.TabIndex = 0;
            this.m_tvObjs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeNodeSelected);
            // 
            // m_cntxMenuTree
            // 
            this.m_cntxMenuTree.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                           this.m_mnuItemBrowseReflection});
            // 
            // m_mnuItemBrowseReflection
            // 
            this.m_mnuItemBrowseReflection.Index = 0;
            this.m_mnuItemBrowseReflection.Text = "Browse Using Reflection...";
            this.m_mnuItemBrowseReflection.Click += new System.EventHandler(this.ContextMenuClick_BrowseReflection);
            // 
            // m_lvData
            // 
            this.m_lvData.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.m_lvData.AutoArrange = false;
            this.m_lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                       this.m_lvCol_label,
                                                                                       this.m_lvCol_value});
            this.m_lvData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lvData.FullRowSelect = true;
            this.m_lvData.GridLines = true;
            this.m_lvData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvData.HideSelection = false;
            this.m_lvData.Location = new System.Drawing.Point(272, 16);
            this.m_lvData.MultiSelect = false;
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.Size = new System.Drawing.Size(504, 472);
            this.m_lvData.TabIndex = 1;
            this.m_lvData.View = System.Windows.Forms.View.Details;
            this.m_lvData.Click += new System.EventHandler(this.DataItemSelected);
            this.m_lvData.DoubleClick += new System.EventHandler(this.DataItemSelected);
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
            // m_bnOK
            // 
            this.m_bnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_bnOK.Location = new System.Drawing.Point(359, 504);
            this.m_bnOK.Name = "m_bnOK";
            this.m_bnOK.TabIndex = 2;
            this.m_bnOK.Text = "OK";
            // 
            // XrefGraph
            // 
            this.AcceptButton = this.m_bnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_bnOK;
            this.ClientSize = new System.Drawing.Size(792, 535);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.m_bnOK,
                                                                          this.m_lvData,
                                                                          this.m_tvObjs});
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(575, 225);
            this.Name = "XrefGraph";
            this.ShowInTaskbar = false;
            this.Text = "Snoop Xref Graph";
            this.ResumeLayout(false);

        }
		#endregion

        protected void
        AddGraphToTree(AcDb.XrefGraph xrefGraph)
        {
            TreeNode rootTreeNode = new TreeNode("Xref Graph");
            rootTreeNode.Tag = xrefGraph;
            m_tvObjs.Nodes.Add(rootTreeNode);

            //AddGraphNodeToTree(xrefGraph.RootNode, rootTreeNode); // TBD: should be the same, but only gives base class GraphNode
            AddGraphNodeToTree(xrefGraph.HostDrawing, rootTreeNode);
            
            m_tvObjs.Sorted = false;
            rootTreeNode.Expand();
            m_tvObjs.SelectedNode = m_tvObjs.Nodes[0];
        }

        private void
        AddGraphNodeToTree(AcDb.GraphNode graphNode, TreeNode parentTreeNode)
        {
                // TBD: for some reason, these are returning GraphNodes, not XrefGraphNodes
            string name = "*UNNAMED*";
            AcDb.XrefGraphNode xNode = graphNode as AcDb.XrefGraphNode;
            if (xNode != null)
                name = xNode.Name;

            TreeNode newTreeNode = new TreeNode(name);
            newTreeNode.Tag = graphNode;
            parentTreeNode.Nodes.Add(newTreeNode);

            for (int i=0; i<graphNode.NumOut; i++) {
                AddGraphNodeToTree(graphNode.Out(i), newTreeNode);   
            }
        }
        
        protected void
        TreeNodeSelected(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            m_curObj = e.Node.Tag;
            
            AcDb.GraphNode gNode = m_curObj as AcDb.GraphNode;
            if (gNode != null) {
                m_snoopCollector.Collect(gNode);
                Snoop.Utils.Display(m_lvData, m_snoopCollector);
                return;
            }
            
            AcDb.Graph graph = m_curObj as AcDb.Graph;
            if (graph != null) {
                m_snoopCollector.Collect(graph);
                Snoop.Utils.Display(m_lvData, m_snoopCollector);
                return;
            }
        }

        protected void
        DataItemSelected(object sender, System.EventArgs e)
        {
            Snoop.Utils.DataItemSelected(m_lvData);
        }
        
        private void
        ContextMenuClick_BrowseReflection(object sender, System.EventArgs e)
        {
            Snoop.Utils.BrowseReflection(m_curObj);
        }

	}
}
