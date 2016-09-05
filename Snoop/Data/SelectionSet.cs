
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
using System.Windows.Forms;

namespace MgdDbg.Snoop.Data
{
	/// <summary>
	/// Summary description for SelectionSet.
	/// </summary>
	public class SelectionSet : Data
	{
	    protected Autodesk.AutoCAD.EditorInput.SelectionSet    m_val;
	    
		public
		SelectionSet(string label, Autodesk.AutoCAD.EditorInput.SelectionSet val)
		:   base(label)
		{
		    m_val = val;
		}
		
        public override string
        StrValue()
        {
            return "< SelectionSet >";
        }
        
        public override bool
        HasDrillDown
        {
            get {
                if (m_val.Count == 0)
                    return false;
                else
                    return true;
            }
        }
        
        public override void
        DrillDown()
        {
            if (m_val.Count > 0) {
                Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection objIds = new Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection(m_val.GetObjectIds());
                using (TransactionHelper trHlp = new TransactionHelper(objIds[0].Database)) {
                    trHlp.Start();
                    
                    Snoop.Forms.DBObjects form = new Snoop.Forms.DBObjects(objIds, trHlp);
                    form.ShowDialog();
                    
                    trHlp.Commit();
                }
            }
        }
	}
}

