
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
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using AcDb = Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.Snoop.Forms
{
	/// <summary>
	/// Derived Form to show References to other objects
	/// </summary>
	public class References : DBObjects
	{
		public
		References(AcDb.ObjectIdCollection hardPointerIds, AcDb.ObjectIdCollection softPointerIds,
                    AcDb.ObjectIdCollection hardOwnerIds, AcDb.ObjectIdCollection softOwnerIds, TransactionHelper tr)
		:   base(tr)
		{
            m_tvObjs.BeginUpdate();

            AddRefsToTree("Hard Pointers", hardPointerIds);
            AddRefsToTree("Soft Pointers", softPointerIds);
            AddRefsToTree("Hard Ownership", hardOwnerIds);
            AddRefsToTree("Soft Ownership", softOwnerIds);
            
            m_tvObjs.ExpandAll();            
            m_tvObjs.EndUpdate();
        }
        
        protected void
        AddRefsToTree(string categoryStr, AcDb.ObjectIdCollection objIds)
        {
            TreeNode categoryNode = new TreeNode(categoryStr);
            categoryNode.Tag = null;
            m_tvObjs.Nodes.Add(categoryNode);

                // initialize the tree control with the ObjectId of each item		
            for (int i=0; i<objIds.Count; i++) {
                TreeNode tmpNode = new TreeNode(MgdDbg.Utils.AcadUi.ObjToTypeAndHandleStr(objIds[i]));
                tmpNode.Tag = objIds[i];
                categoryNode.Nodes.Add(tmpNode);
            }
        }

	}
}

