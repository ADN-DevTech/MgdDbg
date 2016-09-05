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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using AcadApp = Autodesk.AutoCAD.ApplicationServices;

using MgdDbg.Utils;

namespace MgdDbg.Test
{
    public partial class ObjectDiff : Form
    {
        private Database m_db = null;
        private TransactionHelper m_trHlpr = null;
        private Object m_tmpObj = null;
        private Object m_obj1 = null;
        private Object m_obj2 = null;
        private ObjectCompare m_compareObjs;

        public ObjectDiff (TransactionHelper trHlpr)
        {
            InitializeComponent();

            m_db = MgdDbg.Utils.Db.GetCurDwg();
            m_trHlpr = trHlpr;
            
            InitializeTreeView();
        }


        /// <summary>
        /// Initialise tree with all symbol tables and NOD
        /// </summary>
        private void InitializeTreeView ()
        {
            m_treeView.BeginUpdate();

            AddSymbolTableToTree("Block Table", m_db.BlockTableId);
            AddSymbolTableToTree("Dimension Style Table", m_db.DimStyleTableId);
            AddSymbolTableToTree("Layer Table", m_db.LayerTableId);
            AddSymbolTableToTree("Linetype Table", m_db.LinetypeTableId);
            AddSymbolTableToTree("Reg App Table", m_db.RegAppTableId);
            AddSymbolTableToTree("Text Style Table", m_db.TextStyleTableId);
            AddSymbolTableToTree("View Table", m_db.ViewTableId);
            AddSymbolTableToTree("Viewport Table", m_db.ViewportTableId);
            AddSymbolTableToTree("UCS Table", m_db.UcsTableId);

            TreeNode rootNode = new TreeNode("Named Objects Dictionary");
            rootNode.Tag = m_db.NamedObjectsDictionaryId;
            m_treeView.Nodes.Add(rootNode);
            AddDictionaryToTree(m_db.NamedObjectsDictionaryId, rootNode, m_trHlpr.Transaction);

            m_treeView.Sorted = true;
            
            m_treeView.EndUpdate();
        }

		/// <summary>
		/// Add a single Symbol Table to the tree and iterate over all its SymbolTableRecords,
		/// adding each one of them as a sub-node
		/// </summary>
		/// <param name="tblName">Name of the table</param>
		/// <param name="tblId">ObjectId of the table</param>
		
        private void
        AddSymbolTableToTree(string tblName, ObjectId tblId)
        {
            DBObject tmpObj = m_trHlpr.Transaction.GetObject(tblId, OpenMode.ForRead);
            SymbolTable tbl = tmpObj as SymbolTable;
            if (tbl != null) {
                TreeNode mainTblNode = new TreeNode(tblName);
                mainTblNode.Tag = tblId;
                m_treeView.Nodes.Add(mainTblNode);

                    // iterate over each TblRec in the SymbolTable
                foreach (ObjectId tblRecId in tbl) {
                    TreeNode recNode = new TreeNode(m_trHlpr.SymbolIdToName(tblRecId));
                    recNode.Tag = tblRecId;
                    mainTblNode.Nodes.Add(recNode);
                }
            }
        }
        
        
        private void
        AddDictionaryToTree(ObjectId dictId, TreeNode parentNode, Transaction tr)
        {
            // NOTE: when recursively processing items in a dictionary
            // we may encounter things that are not derived from DBDictionary.
            // In that case, the cast to type DBDictionary will fail and
            // we'll just return without adding any nested items.
            DBObject tmpObj = tr.GetObject(dictId, OpenMode.ForRead);
            DBDictionary dbDict = tmpObj as DBDictionary;
            if (dbDict != null) {
                foreach (DictionaryEntry curEntry in dbDict) {
                    TreeNode newNode = new TreeNode((string)curEntry.Key);
                    newNode.Tag = curEntry.Value;
                    parentNode.Nodes.Add(newNode);
                        
                        // if this is a dictionary, it will recursively add
                        // all of its children to the tree
                    AddDictionaryToTree((ObjectId)curEntry.Value, newNode, tr);   
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        m_treeView_AfterSelect (object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null) {

                m_tmpObj = m_trHlpr.Transaction.GetObject((ObjectId)e.Node.Tag, OpenMode.ForRead);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 
        addAsComparerToolStripMenuItem_Click (object sender, EventArgs e)
        {
            m_obj1 = m_tmpObj;
            PopulateCompareBox(m_compareBx1, (ObjectId)m_treeView.SelectedNode.Tag, m_treeView.SelectedNode.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        addAsCompareeToolStripMenuItem_Click (object sender, EventArgs e)
        {
            m_obj2 = m_tmpObj;
            PopulateCompareBox(m_compareBx2, (ObjectId)m_treeView.SelectedNode.Tag, m_treeView.SelectedNode.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 
        m_btmCompare_Click (object sender, EventArgs e)
        {
            if (m_obj1 == null || m_obj2 == null) {
                MessageBox.Show("Please choose two objects to compare");
                return;
            }
            Compare();
        }

        /// <summary>
        /// 
        /// </summary>
        private void
        Compare ()
        {
            m_compareObjs = new ObjectCompare(m_obj1, m_obj2);
            PopulateListView();
        }

        /// <summary>
        /// Get the common hierarchy of the 2 objects
        /// and then get the properties at each level
        /// </summary>
        private void
        PopulateListView ()
        {
            m_listView.Items.Clear();

            ArrayList hierarchy = m_compareObjs.CommonHierarchy;

            foreach (Object typeObj in hierarchy) {

                Type type = typeObj as Type;
                Hashtable propsTable = m_compareObjs.GetPropsAtLevel(type);
                /// display it
                DisplayDiff(type.Name, propsTable);
            }
        }


        /// <summary>
        /// Drill down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void
        DataItemSelected (object sender, System.EventArgs e)
        {
            Debug.Assert((m_listView.SelectedItems.Count > 1) == false);

            if (m_listView.SelectedItems.Count != 0) {

                ListViewItem item = m_listView.SelectedItems[0];
                if (item.SubItems.Count != 3)
                    return;

                Object[] objs = new Object[2];
                objs[0] = item.SubItems[1].Tag;
                objs[1] = item.SubItems[2].Tag;
                if (objs[0] == null || objs[1] == null)
                    return;

                EntityDiff dbox = new EntityDiff(objs[0], objs[1]);
                AcadApp.Application.ShowModalDialog(dbox);
            }
        }

        /// <summary>
        /// Display the diff between the properties
        /// of the two objects
        /// </summary>
        /// <param name="listViewItem">class separator string</param>
        /// <param name="propTable"> 
        /// key   --> property name
        /// value --> an arraylist of 2 items, each item containing 
        /// the value of the current property for the respective object
        /// </param>
        public void
        DisplayDiff (string listViewItem, Hashtable propTable)
        {
            if (propTable == null)
                return;

            m_listView.BeginUpdate();

            System.Drawing.Font boldFont = new System.Drawing.Font(m_listView.Font, System.Drawing.FontStyle.Bold);
            string text = string.Format("--- {0} ---", listViewItem);
            ListViewItem lvItemSeparator = new ListViewItem(text);
            /// show class separator
            lvItemSeparator.BackColor = Color.LightBlue;

            m_listView.Items.Add(lvItemSeparator);

            IDictionaryEnumerator iDictEnum = propTable.GetEnumerator();

            while (iDictEnum.MoveNext()) {

                DictionaryEntry dictEntry = (DictionaryEntry)iDictEnum.Current;
                string key = dictEntry.Key as string;
                ArrayList arrList = dictEntry.Value as ArrayList;
                object obj1 = arrList[0];
                object obj2 = arrList[1];

                ListViewItem lvItem = new ListViewItem(key);

                ListViewItem.ListViewSubItem lvsItem1 = new ListViewItem.ListViewSubItem();
                lvsItem1.Text = obj1.ToString();

                ListViewItem.ListViewSubItem lvsItem2 = new ListViewItem.ListViewSubItem();
                lvsItem2.Text = obj2.ToString();

                lvItem.SubItems.Add(lvsItem1);
                lvItem.SubItems.Add(lvsItem2);

                Type type = null;
                if (obj1.GetType() == typeof(System.DBNull)) {
                    type = obj2.GetType();
                }
                else
                    type = obj1.GetType();

                /// provide a drill down only on worthwhile data
                if (IsDrillDown(type)) {

                    lvsItem1.Tag = obj1;
                    lvsItem2.Tag = obj2;

                    /// visual cue to drill down
                    lvItem.Font = boldFont;
                }

                /// this may happen when there is a collection object comparison
                if (obj1.GetType() != obj2.GetType()) {
                    /// we know this is diff., so highlight it
                    lvItem.BackColor = Color.Red;
                }
                else {
                    /// regular occurence of unequal objects
                    if (!obj1.Equals(obj2)) {
                        /// we know this is diff., so highlight it
                        lvItem.BackColor = Color.Red;
                    }
                }

                m_listView.Items.Add(lvItem);
            }

            m_listView.EndUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Boolean
        IsDrillDown (Type type)
        {
            if (type.IsPrimitive == false &&
                type.IsEnum == false &&
                type != typeof(String) &&
                type != typeof(DBNull)) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 
        m_btnEntChoose_Click (object sender, EventArgs e)
        {
            Editor ed = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;

            EditorUserInteraction userInteraction = ed.StartUserInteraction(this);

            try
            {
                PromptEntityResult res = ed.GetEntity("\nSelect first entity");
                if (res.Status != PromptStatus.OK)
                    return;
                ObjectId objId1 = res.ObjectId;

                res = ed.GetEntity("\nSelect second entity");
                if (res.Status != PromptStatus.OK)
                    return;
                ObjectId objId2 = res.ObjectId;

                m_obj1 = m_trHlpr.Transaction.GetObject(objId1, OpenMode.ForRead);
                m_obj2 = m_trHlpr.Transaction.GetObject(objId2, OpenMode.ForRead);

                PopulateCompareBox(m_compareBx1, objId1, string.Empty);
                PopulateCompareBox(m_compareBx2, objId2, string.Empty);

                Compare();
            }
            /// dont leave editor paralysed if user cancels out
            finally
            {
                userInteraction.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        m_treeView_ItemDrag (object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        m_compareBx1_DragEnter (object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 
        m_compareBx1_DragDrop (object sender, DragEventArgs e)
        {
            TreeNode treeNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (treeNode != null) {
                m_obj1 = m_trHlpr.Transaction.GetObject((ObjectId)treeNode.Tag, OpenMode.ForRead);
                PopulateCompareBox(m_compareBx1, (ObjectId)treeNode.Tag, treeNode.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 
        m_compareBx2_DragEnter (object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 
        m_compareBx2_DragDrop (object sender, DragEventArgs e)
        {
            TreeNode treeNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;
            if (treeNode != null) {
                m_obj2 = m_trHlpr.Transaction.GetObject((ObjectId)treeNode.Tag, OpenMode.ForRead);
                PopulateCompareBox(m_compareBx2, (ObjectId)treeNode.Tag, treeNode.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 
        m_treeView_MouseClick (object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                TreeNode treeNode = m_treeView.GetNodeAt(e.Location);
                if (treeNode.Tag != null) {
                    m_treeView.SelectedNode = treeNode;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compareBox"></param>
        /// <param name="objId"></param>
        /// <param name="name"></param>
        private void 
        PopulateCompareBox (object compareBox, ObjectId objId, string name)
        {
            DBObject tempObj = m_trHlpr.Transaction.GetObject(objId, OpenMode.ForRead);

            String type = String.Format("Type : {0}", tempObj.GetType().Name);
            String id = String.Format("ObjectId : {0}", objId.ToString());

            if (compareBox == m_compareBx1) {
                m_compareBx1.Items.Clear();
                m_compareBx1.Items.Add(name);
                m_compareBx1.Items.Add(type);
                m_compareBx1.Items.Add(id);
            }

            if (compareBox == m_compareBx2) {
                m_compareBx2.Items.Clear();
                m_compareBx2.Items.Add(name);
                m_compareBx2.Items.Add(type);
                m_compareBx2.Items.Add(id);
            }
        }          
    }
}
