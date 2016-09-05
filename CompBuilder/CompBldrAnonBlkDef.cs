
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
	/// Derived component builder to direct the made entities into an 
	/// Anonymous block def.
	/// </summary>
	
	public class CompBldrAnonBlkDef : CompBldr
	{
	    
		public
		CompBldrAnonBlkDef(Database db)
		:   base(db)
		{
		}

        /// <summary>
        /// Make the new Anonymous block table record to add our entities to.
        /// </summary>
        
        protected override void
        SetCurrentBlkTblRec()
        {
            Debug.Assert(m_trans != null);
            
                // if we've already been started before, then we don't want to create another
                // anonymous block, so just re-use the existing one.
            if (m_blkDefId.IsNull) {
                m_blkRec = DefineNewAnonymousBlockRec();
            }
            else {
                m_blkRec = (BlockTableRecord)m_trans.GetObject(m_blkDefId, OpenMode.ForWrite);
            }
        }
        
        /// <summary>
        /// If they are calling Reset, we need to end this block and start another one
        /// </summary>
        public override void
        Reset()
        {
            base.Reset();

            m_blkDefId = ObjectId.Null;   // will force new block def
        }
        
        /// <summary>
        /// Default properties for entities within a block def should be:
        ///   Layer = 0;
        ///   Color = ByBlock;
        ///   Linetype = ByBlock;
        ///
        /// This allows the entities nested in the block def to be controlled by
        /// the properties of the individual BlockReference.
        /// </summary>
        /// <param name="ent">The Entity to set the properties for</param>
        
        public override void
        SetToDefaultProps(Entity ent)
        {
            ent.LayerId = m_db.LayerZero;
            ent.ColorIndex = 0;     // ByBlock
            ent.LinetypeId = m_db.ByBlockLinetype;
        }

    }
}

