
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

namespace MgdDbg.Snoop.Forms
{
	/// <summary>
	/// Summary description for ExtDict.
	/// </summary>
	public class ExtDict : DBObjects
	{
		public
		ExtDict(Autodesk.AutoCAD.DatabaseServices.ObjectId extDictId, TransactionHelper tr)
		:   base(tr)
		{
            m_tvObjs.BeginUpdate();

            TreeNode rootNode = new TreeNode("Extension Dictionary");
            rootNode.Tag = extDictId;
            m_tvObjs.Nodes.Add(rootNode);

            AddDictionaryToTree(extDictId, rootNode);
            
            m_tvObjs.Sorted = true;
            rootNode.Expand();
            m_tvObjs.SelectedNode = m_tvObjs.Nodes[0];
            
            m_tvObjs.EndUpdate();
        }
        
        private void
        AddDictionaryToTree(Autodesk.AutoCAD.DatabaseServices.ObjectId dictId, TreeNode parentNode)
        {
                // NOTE: when recursively processing items in a dictionary
                // we may encounter things that are not derived from DBDictionary.
                // In that case, the cast to type DBDictionary will fail and
                // we'll just return without adding any nested items.
            Autodesk.AutoCAD.DatabaseServices.DBObject tmpObj = m_trans.Transaction.GetObject(dictId, OpenMode.ForRead);
            DBDictionary dbDict = tmpObj as DBDictionary;
            if (dbDict != null) {
                foreach (DictionaryEntry curEntry in dbDict) {
                    TreeNode newNode = new TreeNode((string)curEntry.Key);
                    newNode.Tag = curEntry.Value;
                    parentNode.Nodes.Add(newNode);
                    
                        // if this is a dictionary, it will recursively add
                        // all of its children to the tree
                    AddDictionaryToTree((Autodesk.AutoCAD.DatabaseServices.ObjectId)curEntry.Value, newNode);
                }
            }
        }
	}
}
