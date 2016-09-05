
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
using System.Collections;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MgdDbg
{
	/// <summary>
	/// With the removal of ObjectId.Open/Close, it means that you need to
	/// have a Transaction active to do just about anything.  This is the 
	/// base class for a number of Helper objects that help manage a Transaction
	/// and a set of operations around them.  For instance, when creating a
	/// BlockTableRecord, its nice to have someone manage the containing block,
	/// Xforms to the WCS, etc.
	/// </summary>
	
	public class TransactionHelper : IDisposable
	{
	        // member variables
        protected Database      m_db    = null;
        protected Transaction   m_trans = null;

		public
		TransactionHelper()
		{
            m_db = Utils.Db.GetCurDwg();
		}
		
		public
		TransactionHelper(Database db)
		{
		    Debug.Assert(db != null);
		    
            m_db = db;
		}
		
		~TransactionHelper()
		{
		    Dispose(false);
		}
		
		public void
		Dispose()
		{
		    Dispose(true);
		    GC.SuppressFinalize(this);
		}
		
		protected virtual void
		Dispose(bool disposing)
		{
		    if (disposing) {
                if (m_trans != null) {
                    m_trans.Dispose();
                }
		    }
		}
		
		public virtual void
		Start()
		{
		    Debug.Assert(m_trans == null);
		    
            m_trans = m_db.TransactionManager.StartTransaction();
		}
		
        public virtual void
        Commit()
        {
            Debug.Assert(m_trans != null);
            
            m_trans.Commit();
            m_trans = null;
        }


        public virtual void
        Abort()
        {
            Debug.Assert(m_trans != null);
            
            m_trans.Abort();
            m_trans = null;
        }
        
        /// <summary>
        /// return the underlying Transaction
        /// </summary>
        
        public Autodesk.AutoCAD.DatabaseServices.Transaction
        Transaction
        {
            get { return m_trans;  }
        }

        /// <summary>
        /// return the Database that this TransactionHelper goes with
        /// </summary>
        
        public Autodesk.AutoCAD.DatabaseServices.Database
        Database
        {
            get { return m_db;  }
        }
        
        /// <summary>
        /// See if a SymbolTableRecord of the given name already exists
        /// </summary>
        /// <param name="classType">Type of the SymbolTableRecord (e.g., LayerTableRecord)</param>
        /// <param name="name">Name of the symbol</param>
        /// <returns>True if the symbol already exists, False if it doesn't</returns>
        
        public bool
        SymbolTableRecExists(System.Type classType, string name)
        {
            ObjectId tblId = Utils.SymTbl.GetSymbolTableId(classType, m_db);
            
            SymbolTable tbl = (SymbolTable)m_trans.GetObject(tblId, OpenMode.ForRead);
            return tbl.Has(name);
        }
        
        /// <summary>
        /// Add a new SymbolTableRecord to the database (and the current transaction)
        /// </summary>
        /// <param name="newRec">A newly allocated SymbolTableRecord which hasn't yet been added to the database</param>
        
        public virtual void
        AddNewSymbolRec(SymbolTableRecord newRec)
        {
            Debug.Assert(m_trans != null);
            
            ObjectId tblId = Utils.SymTbl.GetSymbolTableId(newRec.GetType(), m_db);
            
            SymbolTable tbl = (SymbolTable)m_trans.GetObject(tblId, OpenMode.ForWrite);
            Debug.Assert(tbl.Has(newRec.Name) == false);
                
            tbl.Add(newRec);
            m_trans.AddNewlyCreatedDBObject(newRec, true);
        }

	    /// <summary>
	    /// Define a new named block and add it to the BlockTable.  If the block
	    /// definition already exists, its contents will be emptied out so that
	    /// the block can be re-defined.
	    /// </summary>
	    /// <param name="blkName">Name of the BlockDef</param>
	    /// <param name="blkRec">New or existing BlockTableRecord</param>
	    /// <param name="db">Specific database we are using</param>
	    
        public BlockTableRecord
        DefineNewBlockRec(string blkName)
        {
            Debug.Assert(m_trans != null);
            
            BlockTableRecord blkRec = null;
            
            BlockTable tbl = (BlockTable)m_trans.GetObject(m_db.BlockTableId, OpenMode.ForWrite);
                // if it already exists, we are re-defining it and we need to
                // erase all the existing entities
            if (tbl.Has(blkName)) {
                blkRec = (BlockTableRecord)m_trans.GetObject(tbl[blkName], OpenMode.ForWrite);
                
                    // erase all
                DBObject tmpObj = null;
                foreach (ObjectId objId in blkRec) {
                    tmpObj = m_trans.GetObject(objId, OpenMode.ForWrite);
                    if (tmpObj != null) {
                        tmpObj.Erase(true);
                    }
                }
            }
            else {
                blkRec = new BlockTableRecord();
                blkRec.Name = blkName;
                
                tbl.Add(blkRec);
                m_trans.AddNewlyCreatedDBObject(blkRec, true);
            }
            
            return blkRec;
        }
        
        /// <summary>
        /// Define a new Anonymous block and add it to the BlockTable
        /// </summary>
        /// <param name="db"></param>
        /// <returns>an open BlockTableRecord, ready to add Entities to</returns>
        
        public BlockTableRecord
        DefineNewAnonymousBlockRec()
        {
            Debug.Assert(m_trans != null);
            
            BlockTableRecord blkRec = null;
            
            BlockTable tbl = (BlockTable)m_trans.GetObject(m_db.BlockTableId, OpenMode.ForWrite);
            blkRec = new BlockTableRecord();
            blkRec.Name = "*U";
                
            tbl.Add(blkRec);
            m_trans.AddNewlyCreatedDBObject(blkRec, true);
            
            return blkRec;
        }
        
        /// <summary>
        /// Given the ObjectId of a SymbolTableRecord, get its name
        /// </summary>
        /// <param name="symId">ObjectId of the SymbolTableRecord</param>
        /// <returns>name of the SymbolTableRecord</returns>
        
        public string
		SymbolIdToName(ObjectId symId)
		{
		    Debug.Assert(m_trans != null);
		    
		    string str = null;
		    
            DBObject tmpObj = m_trans.GetObject(symId, OpenMode.ForRead);
            SymbolTableRecord rec = tmpObj as SymbolTableRecord;
            if (rec != null)
                str = rec.Name;
            
            return str;
        }
        
        public bool
        IsOnLockedLayer(Entity ent, bool printMsg)
        {
            Debug.Assert(m_trans != null);
            
            LayerTableRecord layer = (LayerTableRecord)m_trans.GetObject(ent.LayerId, OpenMode.ForRead);
            
            if (printMsg && layer.IsLocked) {
                Utils.AcadUi.PrintToCmdLine("\nSelected entity is on a locked layer.");
            }
            
            return layer.IsLocked;
        }
        
        public ObjectIdCollection
        CollectBlockIds(bool excludeMsPs, bool excludeXref, bool excludeXrefDep, bool excludeAnonymous)
        {
            Debug.Assert(m_trans != null);
            
            BlockTable blkTbl = (BlockTable)m_trans.GetObject(m_db.BlockTableId, OpenMode.ForRead);
            
                // iterate over each BlockTableRecord in the BlockTable
            ObjectIdCollection blkRecIds = new ObjectIdCollection();
            foreach (ObjectId tblRecId in blkTbl) {
                BlockTableRecord blkRec = (BlockTableRecord)m_trans.GetObject(tblRecId, OpenMode.ForRead);
                
                if (excludeMsPs && blkRec.IsLayout) {
                    ;    // do nothing
                }
                else if ((excludeXrefDep) && blkRec.IsDependent) {
                    ;    // do nothing
                }
                else if ((excludeXref) &&
                        ((blkRec.IsFromExternalReference) ||
                        (blkRec.IsFromOverlayReference))) {
                    ;   // do nothing
                }
                else if (excludeAnonymous && blkRec.IsAnonymous) {
                    ;   // do nothing
                }
                else {
                    blkRecIds.Add(tblRecId);
                }

            }
            
            return blkRecIds;
        }

    }
}


