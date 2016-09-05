
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
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

using AcDb = Autodesk.AutoCAD.DatabaseServices;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace MgdDbg.Test
{
	/// <summary>
	/// Summary description for QueryEntTests.
	/// </summary>
	public class QueryEntTests : MgdDbgTestFuncs
	{
        private Database    m_db = null;

		public
		QueryEntTests()
		{		    
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get Stretch Points", "Add Point entities where stretch points are", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(StretchPoints), MgdDbgTestFuncInfo.TestType.Query));
            //m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get Grip Points", "Add Point entities where grip points are", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(GripPoints), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get Geometry Extents", "Add a block of Faces equal to extents cube", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(Extents), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Intersect With", "Add Point entities where intersection points are", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(IntersectWith), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Bounding Box Intersect With", "Add Point entities where intersection points are", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(BoundingBoxIntersectWith), MgdDbgTestFuncInfo.TestType.Query));
        }

        #region Tests


        /// <summary>
        /// Add Point entities where stretch points are
        /// </summary>
        public void
        StretchPoints()
        {
            m_db      = Utils.Db.GetCurDwg();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            Point3dCollection pts = new Point3dCollection();

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            while (true) {
                PromptEntityResult prEntRes = ed.GetEntity("\nSelect entity to show stretch points");
                if (prEntRes.Status != PromptStatus.OK)
                    break;
                    
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    AcDb.Entity ent = (AcDb.Entity)tr.GetObject(prEntRes.ObjectId, OpenMode.ForRead);
                    
                    pts.Clear();    // reset to no points each time through loop
                    ent.GetStretchPoints(pts);
                    
                    if (pts.Count == 0)
                        Utils.AcadUi.PrintToCmdLine("\nNo stretch points specified!");
                    else {
                        for (int i=0; i<pts.Count; i++) {
                            Utils.AcadUi.PrintToCmdLine(string.Format("\nSTRETCH[{0:d}]: {1}", i, Utils.AcadUi.PtToStr(pts[i])));
                            MakePointEnt(pts[i], 1, tr);
                        }
                    }
                    
                    tr.Commit();
                }
            }
        }
        

        /// <summary>
        /// Add Point entities where grip points are
        /// </summary>
        public void
        GripPoints()
        {
            m_db      = Utils.Db.GetCurDwg();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            Point3dCollection pts = new Point3dCollection();
            IntegerCollection osnapModes = new IntegerCollection();
            IntegerCollection geomIds = new IntegerCollection();

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            while (true) {
                PromptEntityResult prEntRes = ed.GetEntity("\nSelect entity to show grip points");
                if (prEntRes.Status != PromptStatus.OK)
                    break;

                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    AcDb.Entity ent = (AcDb.Entity)tr.GetObject(prEntRes.ObjectId, OpenMode.ForRead);

                    pts.Clear();    // reset to no points each time through loop
                    osnapModes.Clear();
                    geomIds.Clear();
                    
                    //TBD:  Throws a "Value cannot be null exception"
                    ent.GetGripPoints(pts, osnapModes, geomIds);
                    
                    if (pts.Count == 0)
                        Utils.AcadUi.PrintToCmdLine("\nNo grip points specified!");
                    else {
                        for (int i=0; i<pts.Count; i++) {
                            Utils.AcadUi.PrintToCmdLine(string.Format("\nGRIP[{0:d}]: {1}", i, Utils.AcadUi.PtToStr(pts[i])));
                            MakePointEnt(pts[i], 2, tr);
                        }
                    }

                    if (osnapModes.Count == 0)
                        Utils.AcadUi.PrintToCmdLine("\nNo osnap modes specified!");
                    else {
                        for (int i=0; i<osnapModes.Count; i++) {
                            Utils.AcadUi.PrintToCmdLine(string.Format("\nOSNAP MODE[{0:d}]: {1}", i, osnapModes[i]));
                        }
                    }
                    
                    if (geomIds.Count == 0)
                        Utils.AcadUi.PrintToCmdLine("\nNo geometry IDs specified!");
                    else {
                        for (int i=0; i<geomIds.Count; i++) {
                            Utils.AcadUi.PrintToCmdLine(string.Format("\nGEOM ID[{0:d}]: {1}", i, geomIds[i].ToString()));
                        }
                    }
                    
                    tr.Commit();
                }
            }
        }      
        
        
        /// <summary>
        /// Add a block of Faces equal to extents cube
        /// </summary>
        public void
        Extents()
        {
            m_db      = Utils.Db.GetCurDwg();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            while (true) {
                PromptEntityResult prEntRes = ed.GetEntity("\nSelect entity to show extents");
                if (prEntRes.Status != PromptStatus.OK)
                    break;
                    
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    AcDb.Entity ent = (AcDb.Entity)tr.GetObject(prEntRes.ObjectId, OpenMode.ForRead);

                    Extents3d ext = ent.GeometricExtents;
                    Point3d centerPt = Utils.Ge.Midpoint(ext.MinPoint, ext.MaxPoint);
                    
                    Utils.AcadUi.PrintToCmdLine(string.Format("\nEXTMIN:    {0}", Utils.AcadUi.PtToStr(ext.MinPoint)));
                    Utils.AcadUi.PrintToCmdLine(string.Format("\nEXTMAX:    {0}", Utils.AcadUi.PtToStr(ext.MaxPoint)));
                    Utils.AcadUi.PrintToCmdLine(string.Format("\nCENTER PT: {0}", Utils.AcadUi.PtToStr(centerPt)));
                    
                    tr.Commit();
                    
                    MakeExtentsBlock(ext);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ext"></param>
        public void
        MakeExtentsBlock(Extents3d ext)
        {
            m_db          = Utils.Db.GetCurDwg();
            
            double deltaX = Math.Abs(ext.MaxPoint.X - ext.MinPoint.X);
            double deltaY = Math.Abs(ext.MaxPoint.Y - ext.MinPoint.Y);
            double deltaZ = Math.Abs(ext.MaxPoint.Z - ext.MinPoint.Z);

            Point3d[] pts = new Point3d[8];
            
            pts[0] = ext.MinPoint;
            pts[6] = ext.MaxPoint;

                // make bottom face
            pts[1] = new Point3d(pts[0].X + deltaX, pts[0].Y, pts[0].Z);
            pts[2] = new Point3d(pts[1].X, pts[1].Y + deltaY, pts[1].Z);
            pts[3] = new Point3d(pts[0].X, pts[0].Y + deltaY, pts[0].Z);

                // project up by Z
            pts[4] = new Point3d(pts[0].X, pts[0].Y, pts[0].Z + deltaZ);
            pts[5] = new Point3d(pts[1].X, pts[1].Y, pts[1].Z + deltaZ);
            pts[7] = new Point3d(pts[3].X, pts[3].Y, pts[3].Z + deltaZ);

            Vector3d offset = ext.MinPoint.GetAsVector();

                // move points so that they are centered at WCS origin
                // for block creation.  Express everything in WCS since
                // that is what Entity.Extents works in.
            for (int i=0; i<pts.Length; i++) {
                pts[i] -= offset;
            }

            DBObjectCollection faceEnts = new DBObjectCollection();
            faceEnts.Add(new Face(pts[0], pts[1], pts[2], pts[3], true, true, true, true));  // bottom face
            faceEnts.Add(new Face(pts[4], pts[5], pts[6], pts[7], true, true, true, true));  // top face
            faceEnts.Add(new Face(pts[0], pts[1], pts[5], pts[4], true, true, true, true));  // front face
            faceEnts.Add(new Face(pts[1], pts[2], pts[6], pts[5], true, true, true, true));  // right side face
            faceEnts.Add(new Face(pts[2], pts[3], pts[7], pts[6], true, true, true, true));  // back side face
            faceEnts.Add(new Face(pts[3], pts[0], pts[4], pts[7], true, true, true, true));  // left side face
            
            CompBldrAnonBlkDef compBldr = new CompBldrAnonBlkDef(m_db);
            compBldr.Start();
            
            foreach (Entity ent in faceEnts) {
                compBldr.SetToDefaultProps(ent);
                compBldr.AddToDb(ent);
            }
            
            compBldr.Commit();
            
            BlockReference blkRef = new BlockReference(ext.MinPoint, compBldr.BlockDefId);
            blkRef.ColorIndex = 4;
            Utils.SymTbl.AddToCurrentSpaceAndClose(blkRef, compBldr.Database);
            
        }
        
        /// <summary>
        /// Add Point entities where intersection points are
        /// </summary>
        public void
        IntersectWith()
        {
            m_db      = Utils.Db.GetCurDwg();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                // get intersection type from user
            PromptKeywordOptions prIntType = new PromptKeywordOptions("\nExtend objects");
            prIntType.Keywords.Add("None");
            prIntType.Keywords.Add("First");
            prIntType.Keywords.Add("Second");
            prIntType.Keywords.Add("Both");
            
            PromptResult prIntTypeRes = ed.GetKeywords(prIntType);
            if (prIntTypeRes.Status != PromptStatus.OK)
                return;

            //Intersect intType = Intersect.ExtendBoth;
            //if (prIntTypeRes.StringResult == "None")
            //    intType = Intersect.OnBothOperands;
            //else if (prIntTypeRes.StringResult == "First")
            //    intType = Intersect.ExtendThis;
            //else if (prIntTypeRes.StringResult == "Second")
            //    intType = Intersect.ExtendArgument;
            //else
            //    intType = Intersect.ExtendBoth;

            PromptEntityOptions prEnt1 = new PromptEntityOptions("\nSelect first entity");
            PromptEntityOptions prEnt2 = new PromptEntityOptions("\nSelect intersecting entity");
            Point3dCollection pts = new Point3dCollection();
            
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            while (true) {
                PromptEntityResult prEnt1Res = ed.GetEntity(prEnt1);
                if (prEnt1Res.Status != PromptStatus.OK)
                    return;
                
                PromptEntityResult prEnt2Res = ed.GetEntity(prEnt2);   
                if (prEnt2Res.Status != PromptStatus.OK)
                    return;
                
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    AcDb.Entity ent1 = (AcDb.Entity)tr.GetObject(prEnt1Res.ObjectId, OpenMode.ForRead);
                    AcDb.Entity ent2 = (AcDb.Entity)tr.GetObject(prEnt2Res.ObjectId, OpenMode.ForRead);

                    pts.Clear();
                    //ent1.IntersectWith(ent2, intType, pts, 0, 0); // TBD: Fix JMA
                    if (pts.Count == 0)
                        Utils.AcadUi.PrintToCmdLine("\nObjects do not intersect!");
                    else {
                        for (int i=0; i<pts.Count; i++) {
                            Utils.AcadUi.PrintToCmdLine(string.Format("\nINTERSECT PT:  {0}", Utils.AcadUi.PtToStr(pts[i])));
                            MakePointEnt(pts[i], 3, tr);
                        }
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        /// <summary>
        /// Add Point entities where intersection points are
        /// </summary>
        public void
        BoundingBoxIntersectWith()
        {
            m_db      = Utils.Db.GetCurDwg();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                // get intersection type from user
            PromptKeywordOptions prIntType = new PromptKeywordOptions("\nExtend objects");
            prIntType.Keywords.Add("None");
            prIntType.Keywords.Add("First");
            prIntType.Keywords.Add("Second");
            prIntType.Keywords.Add("Both");
            
            PromptResult prIntTypeRes = ed.GetKeywords(prIntType);
            if (prIntTypeRes.Status != PromptStatus.OK)
                return;

            //Intersect intType = Intersect.ExtendBoth;
            //if (prIntTypeRes.StringResult == "None")
            //    intType = Intersect.OnBothOperands;
            //else if (prIntTypeRes.StringResult == "First")
            //    intType = Intersect.ExtendThis;
            //else if (prIntTypeRes.StringResult == "Second")
            //    intType = Intersect.ExtendArgument;
            //else
            //    intType = Intersect.ExtendBoth;

            PromptEntityOptions prEnt1 = new PromptEntityOptions("\nSelect first entity");
            PromptEntityOptions prEnt2 = new PromptEntityOptions("\nSelect intersecting entity");
            Point3dCollection pts = new Point3dCollection();
            
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            while (true) {
                PromptEntityResult prEnt1Res = ed.GetEntity(prEnt1);
                if (prEnt1Res.Status != PromptStatus.OK)
                    return;
                
                PromptEntityResult prEnt2Res = ed.GetEntity(prEnt2);   
                if (prEnt2Res.Status != PromptStatus.OK)
                    return;

                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    AcDb.Entity ent1 = (AcDb.Entity)tr.GetObject(prEnt1Res.ObjectId, OpenMode.ForRead);
                    AcDb.Entity ent2 = (AcDb.Entity)tr.GetObject(prEnt2Res.ObjectId, OpenMode.ForRead);
                    
                    MakeExtentsBlock(ent1.GeometricExtents);

                    pts.Clear();
                    //ent1.BoundingBoxIntersectWith(ent2, intType, pts, 0, 0);      // TBD: Fix JMA
                    if (pts.Count == 0)
                        Utils.AcadUi.PrintToCmdLine("\nObjects do not intersect!");
                    else {
                        for (int i=0; i<pts.Count; i++) {
                            Utils.AcadUi.PrintToCmdLine(string.Format("\nINTERSECT PT:  {0}", Utils.AcadUi.PtToStr(pts[i])));
                            MakePointEnt(pts[i], 3, tr);
                        }
                    }
                    
                    tr.Commit();
                }
            }
        }

        #endregion


        #region Utils

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="colorIndex"></param>
        /// <param name="tr"></param>
        private void
        MakePointEnt (Point3d pt, int colorIndex, Transaction tr)
        {
            m_db = Utils.Db.GetCurDwg();

            short mode = (short)AcadApp.GetSystemVariable("pdmode");
            if (mode == 0)
                AcadApp.SetSystemVariable("pdmode", 99);

            using (DBPoint dbPt = new DBPoint(Utils.Db.WcsToUcs(pt))) {
                dbPt.ColorIndex = colorIndex;
                Utils.Db.TransformToWcs(dbPt, m_db);
                Utils.SymTbl.AddToCurrentSpace(dbPt, m_db, tr);
            }
        }

        #endregion
    }
}
