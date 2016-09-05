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

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using AcadApp = Autodesk.AutoCAD.ApplicationServices;

using MgdDbg.Utils;

namespace MgdDbg.ObjTests.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Objects : Form
    {
        private Database m_db = null;
        private TransactionHelper m_trHlpr = null;
        private ObjectId m_objectId = ObjectId.Null;

        /// <summary>
        /// 
        /// </summary>
        public ObjectId
        ObjectId
        {
            get
            {
                return m_objectId;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trHlpr"></param>
        public Objects (TransactionHelper trHlpr)
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

            //AddSymbolTableToTree("Block Table", m_db.BlockTableId);
            //AddSymbolTableToTree("Dimension Style Table", m_db.DimStyleTableId);
            //AddSymbolTableToTree("Layer Table", m_db.LayerTableId);
            //AddSymbolTableToTree("Linetype Table", m_db.LinetypeTableId);
            //AddSymbolTableToTree("Reg App Table", m_db.RegAppTableId);
            //AddSymbolTableToTree("Text Style Table", m_db.TextStyleTableId);
            //AddSymbolTableToTree("View Table", m_db.ViewTableId);
            //AddSymbolTableToTree("Viewport Table", m_db.ViewportTableId);
            //AddSymbolTableToTree("UCS Table", m_db.UcsTableId);

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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictId"></param>
        /// <param name="parentNode"></param>
        /// <param name="tr"></param>
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
        private void m_treeView_AfterSelect (object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null) {
                m_objectId = (ObjectId)e.Node.Tag;
            }
        }
    }
}