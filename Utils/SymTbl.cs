
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
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MgdDbg.Utils
{
	/// <summary>
	/// Summary description for SymTbl.
	/// </summary>
	public class SymTbl
	{
		public SymTbl()
		{
		}
 
        /// <summary>
        /// Add a newly allocated Entity to the Current Space (ModelSpace or PaperSpace).
        /// Use this function as a simple way to add an isolated entity.  For "real" situations
        /// where you need to add several entities (like when creating a BlockTableRecord),
        /// use a CompBldr object.
        /// </summary>
        /// <param name="ent">Entity that has not yet been added to the database</param>
        /// <param name="db">Database to add to</param>
        /// <param name="tr">Active Transaction</param>
        /// <returns>The ObjectId of the new entity</returns>
        
        public static ObjectId
		AddToCurrentSpace(Entity ent, Database db, Transaction tr)
		{
            ObjectId objId;
        
            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
            objId = btr.AppendEntity(ent);
            tr.AddNewlyCreatedDBObject(ent, true);
                            
            return objId;
  		}
		
		/// <summary>
        /// Add newly allocated Entites to the Current Space (ModelSpace or PaperSpace).
        /// Use this function as a simple way to add isolated entities.  For "real" situations
        /// where you need to add several entities (like when creating a BlockTableRecord),
        /// use a CompBldr object.
        /// </summary>
        /// <param name="ent">Entities that have not yet been added to the database</param>
        /// <param name="db">Database to add to</param>
        /// <param name="tr">Active Transaction</param>
        
        public static void
        AddToCurrentSpace(DBObjectCollection ents, Database db, Transaction tr)
        {
            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
            foreach (Entity tmpEnt in ents) {
                btr.AppendEntity(tmpEnt);
                tr.AddNewlyCreatedDBObject(tmpEnt, true);
            }
        }
		
		/// <summary>
        /// Add a newly allocated Entity to the Current Space (ModelSpace or PaperSpace).
        /// Use this function as a simple way to add an isolated entity.  For "real" situations
        /// where you need to add several entities (like when creating a BlockTableRecord),
        /// use a CompBldr object.  This version will Start and Commit a Transaction.
		/// </summary>
		/// <param name="ent">An entity which is not yet in the database</param>
		/// <param name="db">The Database to add it to</param>
		/// <returns>The ObjectId of the newly added Entity</returns>
		
        public static ObjectId
        AddToCurrentSpaceAndClose(Entity ent, Database db)
        {
            ObjectId objId;
            
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = db.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
		        objId = AddToCurrentSpace(ent, db, tr);
                tr.Commit();
            }
                            
            return objId;
        }
        
        /// <summary>
        /// Add newly allocated Entites to the Current Space (ModelSpace or PaperSpace).
        /// Use this function as a simple way to add isolated entities.  For "real" situations
        /// where you need to add several entities (like when creating a BlockTableRecord),
        /// use a CompBldr object.  This version will Start and Commit a Transaction.
		/// </summary>
		/// <param name="ent">An entities which are not yet in the database</param>
		/// <param name="db">The Database to add it to</param>
		/// <returns>The ObjectId of the newly added Entity</returns>

        public static void
        AddToCurrentSpaceAndClose(DBObjectCollection ents, Database db)
        {
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = db.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                AddToCurrentSpace(ents, db, tr);
                tr.Commit();
            }
        }
        
        /// <summary>
        /// Iterate over a BlockTableRecord and collect all the ObjectIds of the
        /// entities it contains.
        /// </summary>
        /// <param name="blkRec">BlockTableRecord to iterate over</param>
        /// <returns>The collection of objects within the BlockTableRecord</returns>
        
        public static ObjectIdCollection
        CollectBlockEnts(BlockTableRecord blkRec)
        {
            ObjectIdCollection objIds = new ObjectIdCollection();
            
            foreach (ObjectId id in blkRec) {
                objIds.Add(id);
            }
            
            return objIds;
        }
        
        /// <summary>
        /// Given a type of SymbolTableRecord, look up the appropriate SymbolTable
        /// for this type of object.  This is used to programmatically find owning
        /// symbol tables when we don't know ahead of time what type of object we
        /// are dealing with.
        /// </summary>
        /// <param name="classType">System.Type of the SymbolTableRecord</param>
        /// <param name="db">Specific database we are looking at</param>
        /// <returns>ObjectId of the SymbolTable that owns this type of SymbolTableRecord.</returns>
        
        public static ObjectId
        GetSymbolTableId(System.Type classType, Database db)
        {
            Debug.Assert(classType != null);
            Debug.Assert(db != null);
            
            if (classType == typeof(BlockTableRecord))
                return db.BlockTableId;
            else if (classType == typeof(DimStyleTableRecord))
                return db.DimStyleTableId;
            else if (classType == typeof(LayerTableRecord))
                return db.LayerTableId;
            else if (classType == typeof(LinetypeTableRecord))
                return db.LinetypeTableId;
            else if (classType == typeof(TextStyleTableRecord))
                return db.TextStyleTableId;
            else if (classType == typeof(RegAppTableRecord))
                return db.RegAppTableId;
            else if (classType == typeof(UcsTableRecord))
                return db.UcsTableId;
            else if (classType == typeof(ViewTableRecord))
                return db.ViewTableId;
            else if (classType == typeof(ViewportTableRecord))
                return db.ViewportTableId;
            else {
                Debug.Assert(false);
                ObjectId nullObj = new ObjectId();
                return nullObj;
            }
        }      
        
        /// <summary>
        /// Get the ObjectId of a named SymbolTableRecord
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="symName"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        
        public static ObjectId
        GetSymbolTableRecId(System.Type classType, string symName, Database db)
        {
            ObjectId tblId = GetSymbolTableId(classType, db);
            ObjectId recId = new ObjectId();
            
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = db.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                SymbolTable tbl = (SymbolTable)tr.GetObject(tblId, OpenMode.ForRead);
                if (tbl.Has(symName))   // TBD: should indexer return ObjectId.null instead of throwing exception
                    recId = tbl[symName];
                tr.Commit();
            }
            
            return recId;
        }
        
        /// <summary>
        /// See if a SymbolTableRecord of the given name already exists
        /// </summary>
        /// <param name="classType">Type of the SymbolTableRecord (e.g., LayerTableRecord)</param>
        /// <param name="name">Name of the symbol</param>
        /// <returns>True if the symbol already exists, False if it doesn't</returns>
        
        public static bool
        SymbolTableRecExists(System.Type classType, string symName, Database db)
        {
            bool doesExist = false;
            
            ObjectId tblId = GetSymbolTableId(classType, db);
            
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = db.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                SymbolTable tbl = (SymbolTable)tr.GetObject(tblId, OpenMode.ForRead);
                doesExist = tbl.Has(symName);
                tr.Commit();
            }
            
            return doesExist;
        }

        
        /// <summary>
        /// Get the ObjectId of a named Linetype.  If the Linetype does not already exist
        /// in the drawing, it will attempt to load it from the standard AutoCAD Linetype
        /// files.
        /// </summary>
        /// <param name="ltypeName">Name of the Linetype</param>
        /// <param name="db">Specific database we are using</param>
        /// <returns>ObjectId of the named linetype.  Returns CONTINUOUS if not found and could not load.</returns>
        
        public static ObjectId
        GetOrLoadLinetypeId(string ltypeName, Database db)
        {
            ObjectId ltypeId = GetSymbolTableRecId(typeof(LinetypeTableRecord), ltypeName, db);
            
            if (ltypeId.IsNull) {
                string[] ltypeFiles = {"acad.lin", "acadiso.lin", "ltypeshp.lin"};

                int len = ltypeFiles.Length;
                for (int i=0; i<len; i++) {
                        // try to load the linetype from the external file
                    db.LoadLineTypeFile(ltypeName, ltypeFiles[i]);
                    
                    ltypeId = GetSymbolTableRecId(typeof(LinetypeTableRecord), ltypeName, db);
                    if (ltypeId.IsNull == false)
                        return ltypeId;
                }

                Utils.AcadUi.PrintToCmdLine(string.Format("\nERROR: Could not load linetype \"{0}\", using CONTINUOUS instead.", ltypeName));
                ltypeId = db.ContinuousLinetype;
            }
            
            return ltypeId;
        }
        
    }
}
