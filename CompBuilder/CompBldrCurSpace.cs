
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
using Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg
{
	/// <summary>
	/// Derived component builder to direct the entities into the current
	/// ModelSpace or PaperSpace block def.
	/// </summary>
	public class CompBldrCurSpace : CompBldr
	{
		public CompBldrCurSpace(Database db)
		:   base(db)
		{
		}


        protected override void
        SetCurrentBlkTblRec()
        {
            Debug.Assert(m_trans != null);
            
            m_blkRec = (BlockTableRecord)m_trans.GetObject(m_db.CurrentSpaceId, OpenMode.ForWrite, false);
        }
    }
}

