
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
using System.Windows.Forms;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.EditorInput;

using AcRx = Autodesk.AutoCAD.Runtime;
using AcadApp = Autodesk.AutoCAD.ApplicationServices;

using MgdDbg.Utils;
using MgdDbg.ObjTests.Forms;

namespace MgdDbg.Test
{
	/// <summary>
	/// Summary description for QueryDbTests.
	/// </summary>
	public class DbTests : MgdDbgTestFuncs
	{
        private Database    m_db = null;

		public
		DbTests()
		{           
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Xref graph (with ghosts)", "Display the graph of Xrefs for the current drawing", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(XrefGraphGhosts), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Xref graph (without ghosts)", "Display the graph of Xrefs for the current drawing", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(XrefGraphNoGhosts), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Count hard references", "See which objects have a hard reference to a selected object", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(CountHardReferences), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Attach xref", "Attaches an Xref file", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(AttachXref), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Overlay xref", "Overlays an Xref file", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(OverlayXref), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Detach xref", "Detaches an Xref file", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(DetachXref), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Deep clone", "Deep clones selected objects with Model Space as owner", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(DeepClone), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Insert (in block table)", "Creates a BlockTableRecord from external DWG's ModelSpace", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(InsertInBlockTable), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Insert (explode into model space)", "Inserts entities from ModelSpace of external DWG (INSERT*)", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(InsertInModelSpace), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Insert (copy block)", "Inserts contents of BlockTableRecord from one DWG to a new BlockTable Record in current DWG", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(InsertBlock), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Purge", "Queries if an object is purgable", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(Purge), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Wblock entire database", "Creates new db; clones in everything referenced from Model_Space", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(Wblock), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Wblock objects1", "Creates a new db; clones in a block or objects", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(WblockObjects1), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Wblock objects2", "Clones in objects to an existing db's Model Space", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(WblockObjects2), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Wblock clone objects", "Clones in objects from source to dest db", typeof(Database), new MgdDbgTestFuncInfo.TestFunc(WblockCloneObjects), MgdDbgTestFuncInfo.TestType.Create));
        }

        #region Tests

        public void
        XrefGraphGhosts()
        {
            m_db = Utils.Db.GetCurDwg();

            XrefGraph xrefGraph = m_db.GetHostDwgXrefGraph(true);

            Snoop.Forms.XrefGraph form = new Snoop.Forms.XrefGraph(xrefGraph);
            form.ShowDialog();
        }

        public void
        XrefGraphNoGhosts()
        {
            m_db = Utils.Db.GetCurDwg();

            XrefGraph xrefGraph = m_db.GetHostDwgXrefGraph(false);

            Snoop.Forms.XrefGraph form = new Snoop.Forms.XrefGraph(xrefGraph);
            form.ShowDialog();
        }
        
        public void
        CountHardReferences()
        {            
            Snoop.ObjIdSet objSet = Snoop.Utils.GetSnoopSet();
            if (objSet == null)
                return;
            
            int[] countArray = new int[objSet.Set.Count];

            if (countArray.Length > 0) {
                objSet.Db.CountHardReferences(objSet.Set, countArray);
            }
            
            for (int i=0; i<objSet.Set.Count; i++) {
                AcadUi.PrintToCmdLine(string.Format("\n{0,-30}: {1:d}", AcadUi.ObjToTypeAndHandleStr(objSet.Set[i]), countArray[i]));
            }
        }


        /// <summary>
        /// Note that this is not the same as the Xref command
        /// The Xref command does some further work to put a 
        /// block ref instance in the model space
        /// </summary>
        
        public void
        AttachXref ()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;

            string fileName = Utils.Dialog.OpenFileDialog(".dwg", "Choose dwg to attach as Xref", "Dwg files (*.dwg)|*.dwg");
            if (fileName.Length == 0)
                return;
            string blockName = System.IO.Path.GetFileNameWithoutExtension(fileName);
            if (blockName.Length == 0)
                return;
            if (blockName.ToLower() == System.IO.Path.GetFileNameWithoutExtension(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Name).ToLower()) {               
                MessageBox.Show("External file name used could lead to a circular reference.", "MgdDbg", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ObjectId objId = m_db.AttachXref(fileName, blockName);

            using (TransactionHelper trHlp = new TransactionHelper(m_db)) {
                trHlp.Start();

                BlockTableRecord btr = (BlockTableRecord)trHlp.Transaction.GetObject(objId, OpenMode.ForRead);
                Database xrefDb = btr.GetXrefDatabase(true);

                BlockTable blkTbl = (BlockTable)trHlp.Transaction.GetObject(m_db.BlockTableId, OpenMode.ForRead);

                ObjectIdCollection blkRecIds = new ObjectIdCollection();
                blkRecIds.Add(btr.ObjectId);

                foreach (ObjectId tblRecId in blkTbl) {
                    BlockTableRecord blkRec = (BlockTableRecord)trHlp.Transaction.GetObject(tblRecId, OpenMode.ForRead);
                    if (blkRec.Database == xrefDb)
                        blkRecIds.Add(tblRecId);
                }

                Snoop.Forms.DBObjects form = new Snoop.Forms.DBObjects(blkRecIds, trHlp);
                form.Text = "Xref Block Table Records";
                form.ShowDialog();
                trHlp.Commit();
            }
        }

        /// <summary>
        /// Note that this is not the same as the Xref command
        /// The Xref command does some further work to put a 
        /// block ref instance in the model space
        /// </summary>
        
        public void
        OverlayXref ()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;

            string fileName = Utils.Dialog.OpenFileDialog(".dwg", "Choose dwg to overlay as Xref", "Dwg files (*.dwg)|*.dwg");
            if (fileName.Length == 0)
                return;

            string blockName = System.IO.Path.GetFileNameWithoutExtension(fileName);
            if (blockName.Length == 0)
                return;

            ObjectId objId = m_db.OverlayXref(fileName, blockName);
            using (TransactionHelper trHlp = new TransactionHelper(m_db)) {
                trHlp.Start();

                ObjectIdCollection objIds = new ObjectIdCollection();
                objIds.Add(objId);

                Snoop.Forms.DBObjects objs = new Snoop.Forms.DBObjects(objIds, trHlp);
                objs.Text = "Xrefd Block";
                objs.ShowDialog();

                trHlp.Commit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void
        DetachXref ()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;

            ObjectIdCollection selSet = null;
            if (Utils.AcadUi.GetSelSetFromUser(out selSet) == false)
                return;

            using (TransactionHelper trHlp = new TransactionHelper(m_db)) {
                trHlp.Start();

                foreach (ObjectId bRefId in selSet) {
                    BlockReference bRef = trHlp.Transaction.GetObject(bRefId, OpenMode.ForRead) as BlockReference;
                    if (bRef == null)
                        continue;
                    ObjectId xrefId = bRef.BlockTableRecord;
                    BlockTableRecord btr = trHlp.Transaction.GetObject(xrefId, OpenMode.ForRead) as BlockTableRecord;
                    if (btr.XrefStatus == XrefStatus.Resolved)
                        m_db.DetachXref(xrefId);
                }

                trHlp.Commit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void
        DeepClone ()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;

            ObjectIdCollection selSet = null;
            if (Utils.AcadUi.GetSelSetFromUser(out selSet) == false)
                return;
            if (selSet.Count == 0)
                return;

            
            ObjectId ownerId = new ObjectId();
            IdMapping idMap = new IdMapping();

            /// check to make sure the objects all have same owner
            /// if not set up as many objIdCollections
            Hashtable ownerIdMap = new Hashtable();
            using (TransactionHelper trHlp = new TransactionHelper(m_db)) {
                trHlp.Start();

                foreach (ObjectId tempId in selSet) {
                    DBObject tempObj = trHlp.Transaction.GetObject(tempId, OpenMode.ForRead);
                    
                    if (ownerIdMap.ContainsKey(tempObj.OwnerId)) {
                        ObjectIdCollection objIds = ownerIdMap[tempObj.OwnerId] as ObjectIdCollection;
                        objIds.Add(tempId);
                    }
                    else {
                        /// if a new owner if found, stuff it in map
                        ObjectIdCollection objIds = new ObjectIdCollection();
                        objIds.Add(tempId);
                        ownerIdMap.Add(tempObj.OwnerId, objIds);
                    }
                }
                /// make the model space the owner by default
                BlockTable blkTbl = (BlockTable)trHlp.Transaction.GetObject(m_db.BlockTableId, OpenMode.ForRead);
                ownerId = blkTbl["*MODEL_SPACE"]; 

                trHlp.Commit();
            }

            ICollection keys = ownerIdMap.Keys;
            int keyCount = ownerIdMap.Keys.Count;
            int i=0;
            bool deferXlation = true;


            using (TransactionHelper trHlp = new TransactionHelper(m_db)) {
                trHlp.Start();

                /// if objects to be cloned have different owners
                /// DeepCloneObjects should be called with
                /// deferXlation set to true for all except last time
                foreach (object key in keys) {
                    i++;
                    if (i == keyCount)
                        deferXlation = false;
                    ObjectIdCollection objectIds = ownerIdMap[key] as ObjectIdCollection;
                    m_db.DeepCloneObjects(objectIds, ownerId, idMap, deferXlation);

                    Snoop.Forms.DBObjects objs = new Snoop.Forms.DBObjects(objectIds, trHlp);
                    objs.Text = "Deep cloned objects";
                    objs.ShowDialog();
                }
                trHlp.Commit();
            }
        }

        /// <summary>
        /// Select an external .dwg file.  Then copy the contents of its MODEL_SPACE into a BlockTableRecord
        /// in the current drawing.  Equivalent to the INSERT command, but doesn't create the final BlockReference
        /// </summary>
        
        public void
        InsertInBlockTable()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;

            string dwgName = Utils.Dialog.OpenFileDialog(".dwg", "Choose dwg to insert from", "Dwg files (*.dwg)|*.dwg");
            if (dwgName.Length == 0)
                return;

            try {
                Database extDb = new Database(false, true);
                extDb.ReadDwgFile(dwgName, System.IO.FileShare.Read, true, null);

                ObjectId objId = m_db.Insert("MgdDbg_InsertedBlock", extDb, true);     // TBD: should allow user to name the destination block
                using (TransactionHelper trHlp = new TransactionHelper(m_db)) {
                    trHlp.Start();

                    ObjectIdCollection objIds = new ObjectIdCollection();
                    objIds.Add(objId);

                    Snoop.Forms.DBObjects objs = new Snoop.Forms.DBObjects(objIds, trHlp);
                    objs.Text = "Inserted Block";
                    objs.ShowDialog();

                    trHlp.Commit();
                }
            }
            catch (AcRx.Exception e) {
                AcadUi.PrintToCmdLine(string.Format("\nERROR: {0}", ((AcRx.ErrorStatus)e.ErrorStatus).ToString()));
            }
        }

        /// <summary>
        /// Insert contents of external dwg's model space into our model space.
        /// </summary>
        
        public void
        InsertInModelSpace ()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;

            string dwgName = Utils.Dialog.OpenFileDialog(".dwg", "Choose dwg to insert from", "Dwg files (*.dwg)|*.dwg");
            if (dwgName.Length == 0)
                return;

            Database extDb = new Database(false, true);
            extDb.ReadDwgFile(dwgName, System.IO.FileShare.Read, true, null);

            if (extDb == null)
                return;

            try {
                m_db.Insert(Matrix3d.Identity, extDb, true);
            }
            catch (AcRx.Exception e) {
                AcadUi.PrintToCmdLine(string.Format("\nERROR: {0}", ((AcRx.ErrorStatus)e.ErrorStatus).ToString()));
            }
        }

        /// <summary>
        /// Copy the contents of a selected block in external drawing and create/replace one
        /// in the current drawing.
        /// </summary>
        
        public void
        InsertBlock()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;

            string dwgName = Utils.Dialog.OpenFileDialog(".dwg", "Choose dwg to insert from", "Dwg files (*.dwg)|*.dwg");
            if (dwgName.Length == 0)
                return;

            Database extDb = new Database(false, true);
            extDb.ReadDwgFile(dwgName, System.IO.FileShare.Read, true, null);

            if (extDb == null)
                return;

            // TBD: should allow user to select source block and name the destination block

            ObjectId objId = m_db.Insert("*Model_Space", "MgdDbg_InsertedBlock2", extDb, true);
            using (TransactionHelper trHlp = new TransactionHelper(m_db)) {
                trHlp.Start();

                ObjectIdCollection objIds = new ObjectIdCollection();
                objIds.Add(objId);

                Snoop.Forms.DBObjects objs = new Snoop.Forms.DBObjects(objIds, trHlp);
                objs.Text = "Inserted Block";
                objs.ShowDialog();

                trHlp.Commit();
            }
        }

        /// <summary>
        /// Test the Purge function.  Collect a set of selectable entities and non-graphic
        /// Objects.  Then call Purge and see which ones we are allowed to erase.
        /// </summary>
        
        public void
        Purge()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;

            Snoop.ObjIdSet objSet = Snoop.Utils.GetSnoopSet();
            if (objSet == null)
                return;

                // set the purgeableIds to everything selected
            ObjectIdCollection purgableIds = new ObjectIdCollection();
            foreach (ObjectId objId in objSet.Set)
                purgableIds.Add(objId);

            ObjectIdCollection nonPurgableIds = new ObjectIdCollection();

            try {
                m_db.Purge(purgableIds);

                    // see which ones were non-purgeable by seeing which ones got taken out of the array
                foreach (ObjectId objId in objSet.Set) {
                    if (!purgableIds.Contains(objId))
                        nonPurgableIds.Add(objId);
                }

                using (TransactionHelper trHlp = new TransactionHelper(m_db)) {
                    trHlp.Start();

                    if (purgableIds.Count == 0)
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("No purgable objects");
                    else {
                        Snoop.Forms.DBObjects objs = new Snoop.Forms.DBObjects(purgableIds, trHlp);
                        objs.Text = "Purgable objects";
                        objs.ShowDialog();
                    }

                    if (nonPurgableIds.Count == 0)
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("No non-purgable objects");
                    else {
                        Snoop.Forms.DBObjects objs = new Snoop.Forms.DBObjects(nonPurgableIds, trHlp);
                        objs.Text = "Non-purgable objects";
                        objs.ShowDialog();
                    }

                    trHlp.Commit();
                }
            }
            catch (AcRx.Exception e) {
                AcadUi.PrintToCmdLine(string.Format("\nERROR: {0}", ((AcRx.ErrorStatus)e.ErrorStatus).ToString()));
            }
        }

        /// <summary>
        /// Wblock entire database
        /// </summary>
        
        public void
        Wblock()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;

            try {
                Database db = m_db.Wblock();
                using (TransactionHelper trHlp = new TransactionHelper(db)) {
                    trHlp.Start();
                 
                    Snoop.Forms.Database dbForm = new MgdDbg.Snoop.Forms.Database(db, trHlp);
                    dbForm.Text = "Destination Database (In memory only)";
                    dbForm.ShowDialog();

                    trHlp.Commit();
                }
            }
            catch (AcRx.Exception e) {
                AcadUi.PrintToCmdLine(string.Format("\nERROR: {0}", ((AcRx.ErrorStatus)e.ErrorStatus).ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
 
        public void
        WblockObjects1 ()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;
            
            PromptKeywordOptions prOpts = new PromptKeywordOptions("\nWblock a block or objects");
            prOpts.Keywords.Add("Block", "Block", "Block", true, true);
            prOpts.Keywords.Add("Objects", "Objects", "Objects", true, true);

            Editor ed = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptResult prRes = ed.GetKeywords(prOpts);
            if (prRes.Status != PromptStatus.OK)
                return;

            Database db = null;

            using (TransactionHelper trHlp = new TransactionHelper(m_db)) {
                trHlp.Start();
        
                ObjectIdCollection objIds;
                if (!Utils.AcadUi.GetSelSetFromUser(out objIds))
                    return;

                if (prRes.StringResult == "Block") {
                    if (objIds.Count > 1) {
                        ed.WriteMessage("\nSelect only one block");
                        return;
                    }
                    DBObject obj = trHlp.Transaction.GetObject(objIds[0], OpenMode.ForRead);
                    BlockReference bRef = obj as BlockReference;
                    if (bRef != null) {
                        db = m_db.Wblock(bRef.BlockTableRecord);
                    }
                }

                if (prRes.StringResult == "Objects") {
                    db = m_db.Wblock(objIds, new Point3d(0, 0, 0));
                }

                trHlp.Commit();
            }

            if (db == null)
                return;

            using (TransactionHelper trHlp = new TransactionHelper(db)) {
                trHlp.Start();

                Snoop.Forms.Database dbForm = new MgdDbg.Snoop.Forms.Database(db, trHlp);
                dbForm.Text = "Destination Database (In memory only)";
                dbForm.ShowDialog();

                trHlp.Commit();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void
        WblockObjects2 ()
        {
            m_db = Utils.Db.GetCurDwg();
            if (m_db == null)
                return;

            ObjectIdCollection objIds;
            if (!Utils.AcadUi.GetSelSetFromUser(out objIds))
                return;

            String message = "\nDuplicate Record Cloning Options:";
            Array enums = Enum.GetValues(typeof(DuplicateRecordCloning));
            foreach (int option in enums) {
                message += string.Format("\n{0} = {1}", option,
                                        Enum.GetName(typeof(DuplicateRecordCloning), option));
            }

            PromptIntegerOptions prOpts = new PromptIntegerOptions(message);
            prOpts.LowerLimit = 0;
            prOpts.UpperLimit = enums.Length;

            Editor ed = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptIntegerResult prRes = ed.GetInteger(prOpts);
            if (prRes.Status != PromptStatus.OK)
                return;

            Database db = new Database(true, true);

            DuplicateRecordCloning drc = (DuplicateRecordCloning)prRes.Value;
            if (drc == DuplicateRecordCloning.NotApplicable ||
                drc == DuplicateRecordCloning.RefMangleName) {
                ed.WriteMessage("Invalid Input");
                return;
            }

            using (TransactionHelper trHlp = new TransactionHelper(m_db)) {
                trHlp.Start();

                m_db.Wblock(db, objIds, new Point3d(0, 0, 0), drc);

                trHlp.Commit();
            }

            using (TransactionHelper trHlp = new TransactionHelper(db)) {
                trHlp.Start();

                Snoop.Forms.Database dbForm = new MgdDbg.Snoop.Forms.Database(db, trHlp);
                dbForm.Text = "Destination Database (In memory only)";
                dbForm.ShowDialog();

                trHlp.Commit();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void
        WblockCloneObjects ()
        {
            try {
                m_db = Utils.Db.GetCurDwg();
                if (m_db == null)
                    return;

                using (TransactionHelper trHlpr = new TransactionHelper()) {
                    trHlpr.Start();

                    /// get the object to clone
                    Objects objs = new Objects(trHlpr);
                    if (objs.ShowDialog() != DialogResult.OK) {
                        trHlpr.Abort();
                        return;
                    }

                    DBObject objToClone = trHlpr.Transaction.GetObject(objs.ObjectId, OpenMode.ForRead);

                    if (objToClone is DBDictionary)
                        throw new System.Exception("Please select a record in a dictionary");

                    AcadApp.Document activeDoc = AcadApp.Application.DocumentManager.MdiActiveDocument;

                    /// get cloning options...
                    /// 

                    //String message = "\nDuplicate Record Cloning Options:";
                    //Array enums = Enum.GetValues(typeof(DuplicateRecordCloning));
                    //foreach (int option in enums) {
                    //    message += string.Format("\n{0} = {1}",
                    //                            option,
                    //                            Enum.GetName(typeof(DuplicateRecordCloning), option));
                    //}

                    //PromptIntegerOptions prOpts = new PromptIntegerOptions(message);
                    //prOpts.LowerLimit = 0;
                    //prOpts.UpperLimit = enums.Length;

                    
                    //Editor ed = activeDoc.Editor;
                    //PromptIntegerResult prRes = ed.GetInteger(prOpts);
                    //if (prRes.Status != PromptStatus.OK)
                    //    return;

                    //DuplicateRecordCloning drc = (DuplicateRecordCloning)prRes.Value;
                    //if (drc == DuplicateRecordCloning.NotApplicable ||
                    //    drc == DuplicateRecordCloning.RefMangleName) {
                    //    ed.WriteMessage("Invalid Input");
                    //    return;
                    //}

                    /// .... or not
                    DuplicateRecordCloning drc = DuplicateRecordCloning.Ignore;

                    if (objToClone.OwnerId == ObjectId.Null) /// object to clone is a root object, can't clone
                        return;

                    /// get the destination db
                    Documents docs = new Documents();
                    docs.Text = "Destination database";
                    
                    if (docs.ShowDialog() != DialogResult.OK)
                        return;

                    Database dbSrc = activeDoc.Database;
                    Database dbDest = docs.Document.Database;

                    if (dbDest == dbSrc) {
                        throw new System.Exception("Please pick a destination database other than the source");
                    }

                    /// find out parent dictionary
                    ObjectId owningDictId = objToClone.OwnerId;
                    DBDictionary owningDictSrc = trHlpr.Transaction.GetObject(owningDictId, OpenMode.ForRead) as DBDictionary;

                    /// might be nested
                    Stack owningDictNames = new Stack();

                    while (owningDictSrc.OwnerId != ObjectId.Null) {

                        owningDictSrc = trHlpr.Transaction.GetObject(owningDictSrc.OwnerId, OpenMode.ForRead) as DBDictionary;
                        String owningDictName = owningDictSrc.NameAt(owningDictId);
                        owningDictNames.Push(owningDictName);

                        owningDictId = owningDictSrc.ObjectId;
                    }

                    /// check if parent dictionary exists in dest.
                    DBDictionary owningDictDest = null;

                    using (Transaction trDest = dbDest.TransactionManager.StartTransaction()) {

                        AcadApp.Application.DocumentManager.GetDocument(dbDest).LockDocument();
                        DBDictionary parentDictDest = trDest.GetObject(dbDest.NamedObjectsDictionaryId, OpenMode.ForRead) as DBDictionary;

                        String owningDictName = owningDictNames.Peek().ToString();
                        
                        if (parentDictDest.Contains(owningDictName)) {

                            while (owningDictNames.Count != 0) {

                                owningDictName = owningDictNames.Pop().ToString();
                                owningDictDest = trDest.GetObject(parentDictDest.GetAt(owningDictName), OpenMode.ForRead) as DBDictionary;
                                parentDictDest = owningDictDest;
                            }
                        }
                        else {
                            /// dest doesnt have same structure , create it
                            while (owningDictNames.Count != 0) {

                                owningDictName = owningDictNames.Pop().ToString();
                                parentDictDest.UpgradeOpen();
                                owningDictDest = new DBDictionary();
                                parentDictDest.SetAt(owningDictName, owningDictDest);
                                trDest.AddNewlyCreatedDBObject(owningDictDest, true);
                                parentDictDest = owningDictDest;
                            }
                        }

                        trDest.Commit();
                    }

                    /// clone the objects over
                    ObjectIdCollection objIds = new ObjectIdCollection();
                    objIds.Add(objToClone.ObjectId);

                    IdMapping idMap = new IdMapping();
                    idMap.DestinationDatabase = dbDest;

                    m_db.WblockCloneObjects(objIds, owningDictDest.ObjectId, idMap, drc, false);

                    trHlpr.Commit();
                }
            }

            catch(AcRx.Exception ex)
            {
                if (ex.ErrorStatus == AcRx.ErrorStatus.FileNotFound)
                    MessageBox.Show("No documents found in current session");
                else
                    MessageBox.Show(ex.Message);
            }

            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
	}
}
