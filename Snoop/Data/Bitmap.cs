
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
	/// Summary description for SnoopDataIcon.
	/// </summary>
	public class Bitmap : Data
	{
	    protected System.Drawing.Bitmap    m_val;
	    
		public
		Bitmap(string label, System.Drawing.Bitmap val)
		:   base(label)
		{
		    m_val = val;
		}
		
        public override string
        StrValue()
        {
            if (m_val != null)
                return string.Format("< {0} >", m_val.GetType().Name);
            else
                return "(null)";
        }
        
        public override bool
        HasDrillDown
        {
            get {
                return m_val != null;
            }
        }
        
        public override void
        DrillDown()
        {
            if (m_val != null) {
                MgdDbg.Snoop.Forms.Bitmap form = new MgdDbg.Snoop.Forms.Bitmap(m_val);
                form.ShowDialog();
            }
        }
	}
}
