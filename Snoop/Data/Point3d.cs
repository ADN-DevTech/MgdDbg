
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

namespace MgdDbg.Snoop.Data
{
	/// <summary>
	/// Summary description for SnoopDataPoint3d.
	/// </summary>
	public class Point3d : Data
	{
	    protected Autodesk.AutoCAD.Geometry.Point3d    m_val;
	    
		public
		Point3d(string label, Autodesk.AutoCAD.Geometry.Point3d val)
		:   base(label)
		{
		    m_val = val;
		}
		
        public override string
        StrValue()
        {
            return MgdDbg.Utils.AcadUi.PtToStr(m_val);
        }
	}
}
