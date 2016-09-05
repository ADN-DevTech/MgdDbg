//
// (C) Copyright 2006 by Autodesk, Inc. 
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Threading;
using AcApp = Autodesk.AutoCAD.ApplicationServices;
using AcDb  = Autodesk.AutoCAD.DatabaseServices;
using AcRx  = Autodesk.AutoCAD.Runtime;
using System.Text.RegularExpressions;

namespace MgdDbg.Snoop.Forms {

    /// <summary>
    /// This is a tabbed form that has all the information needed for a SnoopEd command.
    /// It is divided into 4 pages (Documents, Classes, Commands, Host Application Services).
    /// </summary>
    public partial class Editor : Form {

        private Snoop.Collectors.Objects m_snoopCollectorObjs = new MgdDbg.Snoop.Collectors.Objects();

        private ArrayList m_classArray       = new ArrayList();
        private ArrayList m_placedNodes      = new ArrayList();
        private ArrayList m_treeNodes        = new ArrayList();
        private ListView  m_lvCur            = null;
        private int       m_selNode;
        private bool      m_classesTabInited = false;
        private Int32[]   m_maxWidths;
        private Int32     m_currentPrintItem = 0;
        private String    selectedTab        = "Docs";



        // other apps can extend this by adding their own assemblies
        public static     ArrayList assemblyNamesToLoad = new ArrayList();

        /// <summary>
        ///  Initialize components(wizard) and tab pages
        /// </summary>
        public Editor()
        {
            InitializeComponent();

            this.m_tvClasses.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TreeNodeSelected);
            this.m_tvClasses.AfterSelect += new TreeViewEventHandler(this.TreeNodeSelected);
            this.m_txtBoxSearch.TextChanged += new System.EventHandler(this.m_txtBoxSearch_TextChanged);

            //  to start collecting info on an object when an item
            //  in the tree view is selected
            this.m_tvDocs.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TreeNodeSelected);
            this.m_tvDocs.AfterSelect += new TreeViewEventHandler(this.TreeNodeSelected);
            this.m_tvClasses.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TreeNodeSelected);
            this.m_tvClasses.AfterSelect += new TreeViewEventHandler(this.TreeNodeSelected);
            this.m_tvCmds.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TreeNodeSelected);
            this.m_tvCmds.AfterSelect += new TreeViewEventHandler(this.TreeNodeSelected);

            //  hook up event handlers to affect a drill down
            this.m_lvDocs.Click += new System.EventHandler(this.DataItemSelected);
            this.m_lvDocs.DoubleClick += new System.EventHandler(this.DataItemSelected);

            this.m_lvCmds.Click += new System.EventHandler(this.DataItemSelected);
            this.m_lvCmds.DoubleClick += new System.EventHandler(this.DataItemSelected);

            this.m_lvClasses.Click += new System.EventHandler(this.DataItemSelected);
            this.m_lvClasses.DoubleClick += new System.EventHandler(this.DataItemSelected);

            //  Initialise tab pages
            InitDocsTab();
            InitSystemTab();
        }
        
        #region Common Event Handlers

        /// <summary>
        ///  This call back is used for the Documents, Commands
        ///  and Classes tab pages, but we need to know which so 
        ///  we can use the correct ListView control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        TreeNodeSelected(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (sender == m_tvDocs)
                m_lvCur = m_lvDocs;
            else if (sender == m_tvCmds)
                m_lvCur = m_lvCmds;
            else
                if (sender == m_tvClasses) {
                    if (m_tvClasses.SelectedNode != null) {
                        m_selNode = m_tvClasses.SelectedNode.Index;
                    }
                    m_lvCur = m_lvClasses;
                    System.Type tmpClass = (System.Type)e.Node.Tag;
                }
                else
                    Debug.Assert(false);  

            m_snoopCollectorObjs.Collect(e.Node.Tag);

            // now display it in the list view
            Snoop.Utils.Display(m_lvCur, m_snoopCollectorObjs);
        }

        /// <summary>
        ///  This call back is used for the Documents, Commands
        ///  and Classes tab pages, but we need to know which so 
        ///  we can use the correct ListView control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        TreeNodeSelected (object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (sender == m_tvDocs)
                m_lvCur = m_lvDocs;
            else if (sender == m_tvCmds)
                m_lvCur = m_lvCmds;
            else
                if (sender == m_tvClasses) {
                    if (m_tvClasses.SelectedNode != null) {
                        m_selNode = m_tvClasses.SelectedNode.Index;
                    }
                    m_lvCur = m_lvClasses;
                    System.Type tmpClass = (System.Type)e.Node.Tag;
                }
                else
                    Debug.Assert(false);

            m_snoopCollectorObjs.Collect(e.Node.Tag);

            // now display it in the list view
            Snoop.Utils.Display(m_lvCur, m_snoopCollectorObjs);
        }
        
        /// <summary>
        ///  To affect a drill down when a cell is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        DataItemSelected(object sender, System.EventArgs e)
        {
            ListView m_lvCur = sender as ListView;
            Snoop.Utils.DataItemSelected(m_lvCur);
        }

        #endregion
        
        #region Documents Tab
        /// <summary>
        ///  Collects all document names and stuffs them in the tree view  
        ///  jai 02.03.06
        /// </summary>
        private void
        InitDocsTab()
        {
            m_tvDocs.BeginUpdate();

            // get all documents
            AcApp.DocumentCollection docs = AcApp.Application.DocumentManager;
            int count = docs.Count;
            foreach (AcApp.Document doc in docs) {
                TreeNode treeNode = new TreeNode(doc.Name);
                treeNode.Tag = (AcApp.Document)doc;
                m_tvDocs.Nodes.Add(treeNode);
            }
            // initial select condition
            m_tvDocs.SelectedNode = m_tvDocs.Nodes[0];
            m_tvDocs.EndUpdate();
        }

        #endregion

        #region Classes Tab
        /// <summary>
        /// Collects all type names and stuffs them in the tree view  
        /// </summary>
        private void
        InitClassesTab ()
        {
            CollectClasses();
            m_rdBtnSys.Checked = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void
        CollectClasses ()
        {
            ArrayList assembliesLoaded = new ArrayList();
            try {
                foreach (string assemblyName in assemblyNamesToLoad) {
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(assemblyName);
                    assembliesLoaded.Add(assembly);
                }

                foreach (System.Reflection.Assembly assembly in assembliesLoaded) {
                    m_classArray.AddRange(assembly.GetTypes());
                }
            }

            catch (System.Reflection.ReflectionTypeLoadException e) {
                Exception[] ex = e.LoaderExceptions;
                MessageBox.Show(e.Message);
            }
        }

        #region System NameSpace Classes

        /// <summary>
        /// background thread for processing
        /// </summary>
        private Thread m_thread;

        /// <summary>
        /// Toggle between diff. Namespace enumerations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_rdBtnSys_CheckedChanged (object sender, EventArgs e)
        {
            if (m_rdBtnSys.Checked) {

                if (m_classArray.Count == 0)
                    CollectClasses();

                m_rdBtnAdsk.Enabled = false;
                m_tvClasses.Nodes.Clear();

                m_progressBar.Visible = true;
                m_progressBar.Minimum = 0;
                m_progressBar.Maximum = m_classArray.Count;
                m_progressBar.Value = 0;

                /// Spawn background thread to do the processing
                /// so that it doesnt freeze the UI
                m_thread = new Thread(new ThreadStart(BuildClassTree));
                m_thread.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsProgressComplete ()
        {
            return (m_progressBar.Value == m_progressBar.Maximum) ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void
        BuildClassTree ()
        {
            // now hook them up in the tree
            m_placedNodes.Clear();
            m_treeNodes.Clear();

            m_placedNodes.Add(null);      // add in root node
            m_treeNodes.Add(null);

            TreeNode parentNode = null;
            TreeNode newNode = null;
            int index = 0;

            while (Regress(ref index, ref parentNode)) {
                newNode = AddTreeItem((System.Type)m_classArray[index], parentNode);
                
                m_treeNodes.Add(newNode);
                m_placedNodes.Add(m_classArray[index]);
                m_classArray.RemoveAt(index);
            }
            
            m_classArray.Clear();
            this.Invoke(new EventHandler(UpdateUIEnd));
        }

        
        /// <summary>
        /// This is a brute-force walk of the array to find out who everyone's parent
        /// is.  There is probably a more clever way to figure this out, but this will
        /// work for now.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="parentNode"></param>
        /// <returns="bool"></returns>
        private bool
        Regress (ref int index, ref TreeNode parentNode)
        {
            System.Type tmpClass;
            int nodeLen;

            int len = m_classArray.Count;
            for (int i = 0; i < len; i++) {
                if (m_classArray[i] != null) {
                    tmpClass = (System.Type)m_classArray[i];
                    nodeLen = m_placedNodes.Count;
                    System.Type parent = tmpClass.BaseType;
                    for (int j = 0; j < nodeLen; j++) {
                        if (parent == (System.Type)m_placedNodes[j]) {
                            index = i;
                            parentNode = (TreeNode)m_treeNodes[j];
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// delegate to add tree node
        /// </summary>
        /// <param name="treeNodes"></param>
        /// <param name="index"></param>
        public delegate void AddNode (TreeNodeCollection treeNodes, TreeNode treeNode);

        /// <summary>
        /// Adds a node into the tree and returns that node
        /// </summary>
        /// <param name="classObj"></param>
        /// <param name="parentNode"></param>
        /// <returns="TreeNode"></returns>
        private TreeNode
        AddTreeItem (System.Type classObj, TreeNode parentNode)
        {
            TreeNode newNode = new TreeNode(classObj.FullName);
            newNode.Name = classObj.FullName;
            newNode.Tag = classObj;

            TreeNodeCollection tempNodeColl;

            if (parentNode == null)
                tempNodeColl = m_tvClasses.Nodes;
            else
                tempNodeColl = parentNode.Nodes;

            this.Invoke(new AddNode(AddNodeMethod), tempNodeColl, newNode);
            return newNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tempNodeColl"></param>
        /// <param name="treeNode"></param>
        public void AddNodeMethod (TreeNodeCollection tempNodeColl, TreeNode treeNode)
        {
            tempNodeColl.Add(treeNode);
            m_progressBar.PerformStep();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateUIEnd (object sender, EventArgs e)
        {
            /// the value is not always equal to max., so force it 
            m_progressBar.Value = m_progressBar.Maximum;
            m_progressBar.Visible = false;
            // initial select condition
            m_tvClasses.SelectedNode = m_tvClasses.Nodes[0];
            m_rdBtnAdsk.Enabled = true;
        }

        #endregion

        #region Autodesk NameSpace Classes

        /// <summary>
        /// Toggle between diff. Namespace enumerations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_rdBtnAdsk_CheckedChanged (object sender, EventArgs e)
        {
            if (m_rdBtnAdsk.Checked) {

                m_rdBtnSys.Enabled = false;

                m_progressBar.Visible = true;
                m_progressBar.Minimum = 0;
                m_progressBar.Maximum = m_tvClasses.GetNodeCount(true);
                m_progressBar.Value = 0;

                // to minimise flickering
                m_tvClasses.CollapseAll();

                /// Spawn background thread to do the processing
                /// so that it doesnt freeze the UI
                Thread thread = new Thread(new ParameterizedThreadStart(NameSpaceFilter));
                thread.Start(m_tvClasses.Nodes);
            }
        }

        /// <summary>
        /// delegate to delete tree node
        /// </summary>
        /// <param name="treeNodes"></param>
        /// <param name="index"></param>
        public delegate void DeleteNode (TreeNodeCollection treeNodes, int index);

        /// <summary>
        /// Filter out non "Autodesk" namespace types
        /// (except for parents containing "Autodesk" types)
        /// </summary>
        /// <param name="treeNodes"></param>
        private void NameSpaceFilter (object treeNodesObj)
        {
            TreeNodeCollection treeNodes = treeNodesObj as TreeNodeCollection;
            for (int i = 0; i < treeNodes.Count; i++) {
                TreeNode node = treeNodes[i];
                if (node != null) {
                    System.Type type = (System.Type)node.Tag;
                    bool done = false;
                    while (node.Nodes.Count > 0 && !done) {
                        NameSpaceFilter(node.Nodes);
                        done = true;
                    }
                    if (!Regex.IsMatch(type.FullName.ToLower(), "autodesk") && node.Nodes.Count == 0) {
                        this.Invoke(new DeleteNode(DeleteNodeMethod), treeNodes, i);
                        i--;
                    }
                }
                this.Invoke(new EventHandler(ProgressBarStep));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void ProgressBarStep (object obj, EventArgs e)
        {
            m_progressBar.PerformStep();
            
            if (m_progressBar.Value == m_progressBar.Maximum) {
                m_progressBar.Visible = false;

                // initial select condition
                m_tvClasses.SelectedNode = m_tvClasses.Nodes[0];

                m_rdBtnSys.Enabled = true;
                m_rdBtnAdsk.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeNodes"></param>
        /// <param name="index"></param>
        public void DeleteNodeMethod (TreeNodeCollection treeNodes, int index)
        {
            treeNodes.RemoveAt(index);
        }

        #endregion

        #endregion

        #region System Tab

        /// <summary>
        ///  Collects all the system objects  
        /// </summary>
        private void
        InitSystemTab()
        {
            m_tvCmds.BeginUpdate();

                // add Application objects (Application is static so we can't use it as an object to snoop
            TreeNode appNode = new TreeNode();
            appNode.Tag = null;
            appNode.Text = "Application";
            m_tvCmds.Nodes.Add(appNode);

            AddSystemObjectTreeNode("Document Manager", AcApp.Application.DocumentManager, appNode);
            AddSystemObjectTreeNode("Main Window", AcApp.Application.MainWindow, appNode);
            AddSystemObjectTreeNode("Menu Bar", AcApp.Application.MenuBar, appNode);
            AddSystemObjectTreeNode("Menu Groups", AcApp.Application.MenuGroups, appNode);
            AddSystemObjectTreeNode("Preferences", AcApp.Application.Preferences, appNode);
            AddSystemObjectTreeNode("Publisher", AcApp.Application.Publisher, appNode);
            AddSystemObjectTreeNode("Status Bar", AcApp.Application.StatusBar, appNode);
            AddSystemObjectTreeNode("User Configuration Manager", AcApp.Application.UserConfigurationManager, appNode);

                // add other System objects
            AddSystemObjectTreeNode("Class Dictionary", AcRx.SystemObjects.ClassDictionary, null);
            AddSystemObjectTreeNode("Service Dictionary", AcRx.SystemObjects.ServiceDictionary, null);
            AddSystemObjectTreeNode("Dynamic Linker", AcRx.SystemObjects.DynamicLinker, null);
            AddSystemObjectTreeNode("Host Application Services", AcDb.HostApplicationServices.Current, null);

            m_tvCmds.EndUpdate();
        }

        /// <summary>
        /// Recursively walk through a service object node (if its a Dictionary)
        /// </summary>
        /// <param name="name">Label to use for the TreeNode</param>
        /// <param name="obj">Object to display if TreeNode selected</param>
        /// <param name="parent">Parent node of tree (or null for root)</param>
        
        private void
        AddSystemObjectTreeNode(string name, object obj, TreeNode parent)
        {
            TreeNode newNode = new TreeNode();
            newNode.Tag = obj;
            newNode.Text = name;
            if (parent == null)
                m_tvCmds.Nodes.Add(newNode);
            else
                parent.Nodes.Add(newNode);

            AcRx.Dictionary dict = obj as AcRx.Dictionary;
            if (dict != null) {
                IEnumerable enumerable = dict as IEnumerable;
                if (enumerable != null) {
                    IEnumerator enumerator = enumerable.GetEnumerator();
                    while (enumerator.MoveNext()) {
                        this.AddSystemObjectTreeNode( ((DictionaryEntry)enumerator.Current).Key.ToString(), ((DictionaryEntry)enumerator.Current).Value, newNode);
                    }
                }
            }
        }

        #endregion

        #region Comparison feature

        /// <summary>
        /// A comparative list of classes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_bnCompList_Click (object sender, EventArgs e)
        {
            ArrayList tempClassArray = new ArrayList();
            foreach (TreeNode node in m_treeNodes) {
                if (node != null)
                    tempClassArray.Add((System.Type)node.Tag);
            }
            Snoop.Forms.ClassesComp form = new Snoop.Forms.ClassesComp(tempClassArray);
            form.ShowDialog();
        }

        #endregion

        #region Search Feature
        /// <summary>
        ///  Finds a class name dynamically as you 
        ///  type along.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_txtBoxSearch_TextChanged (object sender, EventArgs e)
        {
            for (int i = 0; i < m_tvClasses.Nodes.Count; i++) {
                if (Search(m_tvClasses.Nodes[i], true, m_txtBoxSearch.Text))
                    break;
            }
        }

        /// <summary>
        ///  Small help feature to find a class buried somewhere in the
        ///  tree expeditiously. 
        /// </summary>
        /// <param name="treeNode">node and its children to search</param>
        /// <param name="includeNode">to include the Node itself or not</param>
        /// <param name="searchStr">the string to search</param>
        /// <returns></returns>
        private bool Search (TreeNode treeNode, bool includeNode, string searchStr)
        {
            System.Type type;
            // search the node itself
            if (includeNode) {
                type = (System.Type)treeNode.Tag;
                if (Regex.IsMatch(type.FullName.ToLower(), searchStr.ToLower())) {
                    m_tvClasses.SelectedNode = treeNode;
                    return true;
                }
            }
            // search the node's children
            for (int i = 0; i < treeNode.Nodes.Count; i++) {
                TreeNode node = treeNode.Nodes[i];
                if (node != null) {
                    type = (System.Type)node.Tag;
                    bool done = false;
                    if (Regex.IsMatch(type.FullName.ToLower(), searchStr.ToLower())) {
                        m_tvClasses.SelectedNode = node;
                        return true;
                    }
                    while (node.Nodes.Count > 0 && !done) {
                        if (Search(node, true, searchStr))
                            return true;
                        done = true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To find the next match
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_btnNext_Click (object sender, EventArgs e)
        {
            if (m_tvClasses.SelectedNode == null)
                return;

            if (m_tvClasses.SelectedNode.Nodes.Count > 0) {
                if (Search(m_tvClasses.SelectedNode, false, m_txtBoxSearch.Text))
                    return;
            }
            else {
                TreeNode siblingNode = m_tvClasses.SelectedNode.NextNode;
                while (siblingNode != null) {
                    if (Search(siblingNode, true, m_txtBoxSearch.Text))
                        return;
                    siblingNode = siblingNode.NextNode;
                }

                TreeNode bkTrackNode = GetBackTrackNode(m_tvClasses.SelectedNode);
                if (bkTrackNode != null) {
                    if (BackTrack(bkTrackNode))
                        return;
                }
            }
            // invoke the textchanged event instead of this
            for (int i = 0; i < m_tvClasses.Nodes.Count; i++) {
                if (Search(m_tvClasses.Nodes[i], true, m_txtBoxSearch.Text))
                    break;
            }
        }

        /// <summary>
        /// Get the node to go back to
        /// </summary>
        /// <param name="node">the node to go back from</param>
        /// <returns>the node to go back to</returns>
        private TreeNode GetBackTrackNode (TreeNode node)
        {
            if (node == null)
                return null;

            if (node.Parent == null)
                return null;

            if (node.Parent.NextNode == null)
                return null;

            TreeNode bkTrackNode = node.Parent.NextNode;
            if (bkTrackNode != null)
                return bkTrackNode;
            else {
                if (node.Parent != null)
                    return GetBackTrackNode(node.Parent);
                else
                    return null;
            }
        }

        /// <summary>
        /// Get back in the tree to search for the string
        /// This is a depth first search. First search in
        /// the siblings. If not found, then go to the parent.
        /// </summary>
        /// <param name="parent">the parent node</param>
        /// <returns>pass along success/failure of Search()</returns>
        private bool BackTrack (TreeNode parent)
        {
            if (parent != null) {
                if (Search(parent, true, m_txtBoxSearch.Text))
                    return true;
                else {
                    TreeNode siblingNode = parent.NextNode;
                    while (siblingNode != null) {
                        if (Search(siblingNode, true, m_txtBoxSearch.Text))
                            return true;
                        siblingNode = siblingNode.NextNode;
                    }
                    if (parent.Parent != null)
                        return BackTrack(parent.Parent.NextNode);
                    else
                        return false;
                }
            }
            return false;
        }
        #endregion
        
        /// <summary>
        /// The Classes tab takes a ton of time so don't init it until someone
        /// selects it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        TabSelected(object sender, System.EventArgs e)
        {
            TabControl tabCtrl = (TabControl)sender;

            /// progress bar is only relevant to the Classes tab
            if ((tabCtrl.SelectedTab == m_tabSystem) || (tabCtrl.SelectedTab == m_tabDocs)) {
                m_progressBar.Visible = false;

                if (tabCtrl.SelectedTab == m_tabDocs) {
                    selectedTab = "Docs";
                }
                else {
                    selectedTab = "System";
                }
            }
            else {
                /// show progress bar only if it is still active
                m_progressBar.Visible = !IsProgressComplete();
            }
            
            if (tabCtrl.SelectedTab == m_tabClasses) {
                if (m_classesTabInited == false) {
                    m_classesTabInited = true;
                    InitClassesTab();
                }
                selectedTab = "Classes";
            }
        }

        #region Copy Events
        private void
        CopyListViewToolStripMenuItem_Click (object sender, System.EventArgs e)
        {           
            if (m_lvDocs.SelectedItems.Count > 0) {
                Utils.CopyToClipboard(m_lvDocs.SelectedItems[0]);
                m_lvDocs.SelectedItems.Clear();
            }
            else if (m_lvCmds.SelectedItems.Count > 0) {
                Utils.CopyToClipboard(m_lvCmds.SelectedItems[0]);
                m_lvCmds.SelectedItems.Clear();
            }
            else if (m_lvClasses.SelectedItems.Count > 0) {
                Utils.CopyToClipboard(m_lvClasses.SelectedItems[0]);
                m_lvClasses.SelectedItems.Clear();
            }
            else {
                Clipboard.Clear();
            }
        }

        private void
        CopyTreeViewToolStripMenuItem_Click (object sender, System.EventArgs e)
        {
            Clipboard.Clear();

            if (selectedTab == "Docs") {
                if (m_tvDocs.SelectedNode != null) {
                    Utils.CopyToClipboard(m_lvDocs);
                }
            }
            else if (selectedTab == "System") {
                if (m_tvCmds.SelectedNode != null) {
                    Utils.CopyToClipboard(m_lvCmds);
                }
            }
            else if (selectedTab == "Classes") {
                if (m_tvClasses.SelectedNode != null) {
                    Utils.CopyToClipboard(m_lvClasses);
                }
            }           
        }
        #endregion


        #region TBD

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classDef"></param>
        private void
        DisplayCurrentProxyInfo (System.Type classDef)
        {
            // TBD:
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classDef"></param>
        private void
        DisplayCurrentProtocolExtInfo (System.Type classDef)
        {
            // TBD: Protocol Extension objects.
        }
        #endregion

        /// <summary>
        /// if user escapes out of form , kill the processing thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_FormClosing (object sender, FormClosingEventArgs e)
        {
            if (m_thread != null)
            {
                m_thread.Abort();
            }
        }


        #region Print Events
        private void
        PrintMenuItem_Click (object sender, EventArgs e)
        {
            if (selectedTab == "Docs") {
                Utils.UpdatePrintSettings(m_printDocument, m_tvDocs, m_lvDocs, ref m_maxWidths);
                Utils.PrintMenuItemClick(m_printDialog, m_tvDocs);
            }
            else if (selectedTab == "System") {
                Utils.UpdatePrintSettings(m_printDocument, m_tvCmds, m_lvCmds, ref m_maxWidths);
                Utils.PrintMenuItemClick(m_printDialog, m_tvCmds);
            }
            else if (selectedTab == "Classes") {
                Utils.UpdatePrintSettings(m_printDocument,m_tvClasses, m_lvClasses, ref m_maxWidths);
                Utils.PrintMenuItemClick(m_printDialog, m_tvClasses);
            }
        }

        private void
        PrintPreviewMenuItem_Click (object sender, EventArgs e)
        {
            if (selectedTab == "Docs") {
                Utils.UpdatePrintSettings(m_printDocument, m_tvDocs, m_lvDocs, ref m_maxWidths);
                Utils.PrintPreviewMenuItemClick(m_printPreviewDialog, m_tvDocs);
            }
            else if (selectedTab == "System") {
                Utils.UpdatePrintSettings(m_printDocument, m_tvCmds, m_lvCmds, ref m_maxWidths);
                Utils.PrintPreviewMenuItemClick(m_printPreviewDialog, m_tvCmds);
            }
            else if (selectedTab == "Classes") {
                Utils.UpdatePrintSettings(m_printDocument, m_tvClasses, m_lvClasses, ref m_maxWidths);
                Utils.PrintPreviewMenuItemClick(m_printPreviewDialog, m_tvClasses);
            }            
        }

        private void
        PrintDocument_PrintPage (object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (selectedTab == "Docs") {
                m_currentPrintItem = Utils.Print(m_tvDocs.SelectedNode.Text, m_lvDocs, e, m_maxWidths[0], m_maxWidths[1], m_currentPrintItem);
            }
            else if (selectedTab == "System") {
                m_currentPrintItem = Utils.Print(m_tvCmds.SelectedNode.Text, m_lvCmds, e, m_maxWidths[0], m_maxWidths[1], m_currentPrintItem);
            }
            else if (selectedTab == "Classes") {
                m_currentPrintItem = Utils.Print(m_tvClasses.SelectedNode.Text, m_lvClasses, e, m_maxWidths[0], m_maxWidths[1], m_currentPrintItem);
            }
        }
        #endregion
    }
}
