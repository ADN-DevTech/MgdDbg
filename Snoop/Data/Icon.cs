
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
	public class Icon : Data
	{
	    protected System.Drawing.Icon    m_val;
	    
		public
		Icon(string label, System.Drawing.Icon val)
		:   base(label)
		{
		    m_val = val;
		}
		
        public override string
        StrValue()
        {
            if (m_val == null)
                return "(null)";
            else
                return string.Format("< {0} >", m_val.GetType().Name);
        }
        
        public override bool
        HasDrillDown
        {
            get {
                return (m_val != null);
            }
        }
        
        public override void
        DrillDown()
        {
            if (m_val != null) {
                MgdDbg.Snoop.Forms.Icon form = new MgdDbg.Snoop.Forms.Icon(m_val);
                form.ShowDialog();
            }
        }
	}
}
