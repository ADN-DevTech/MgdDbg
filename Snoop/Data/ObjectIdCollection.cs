
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
	/// Summary description for SnoopDataObjectIdCollection.
	/// </summary>
	public class ObjectIdCollection : Data
	{
	    protected Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection    m_val = null;
	    
		public
		ObjectIdCollection(string label, Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection val)
		:   base(label)
		{
		    m_val = val;
		}
		
        public
        ObjectIdCollection(string label, Autodesk.AutoCAD.DatabaseServices.ObjectId[] val)
        :   base(label)
        {
            if (val.Length == 0)
                m_val = new Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection();
            else
                m_val = new Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection(val);
        }

        public override string
        StrValue()
        {
			return Snoop.Utils.ObjToTypeStr(m_val);
        }
        
        public override bool
        HasDrillDown
        {
            get {
                if ((m_val == null) || (m_val.Count == 0))
                    return false;
                else
                    return true;
            }
        }
        
        public override void
        DrillDown()
        {
            if ((m_val != null) && (m_val.Count > 0)) {
                using (TransactionHelper trHlp = new TransactionHelper(m_val[0].Database)) {
                    trHlp.Start();
                    
                    Snoop.Forms.DBObjects form = new Snoop.Forms.DBObjects(m_val, trHlp);
                    form.ShowDialog();
                    
                    trHlp.Commit();
                }
            }
        }
	}
}
