
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

namespace MgdDbg.Test
{
	/// <summary>
	/// Summary description for ModifyEntTests.
	/// </summary>
	public class ModifyEntTests : MgdDbgTestFuncs
	{
	    private Database    m_db = null;

		public ModifyEntTests()
		{	    
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Xform - Move", "Move selected objects", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(XformMove), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Xform - Rotate", "Rotate selected objects", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(XformRotate), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Xform - Scale", "Scale selected objects", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(XformScale), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Xform - Mirror", "Mirror selected objects", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(XformMirror), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Xform - WCS Origin", "Transform selected objects to WCS origin (needed when making a block out of a set of entities)", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(XformWcsOrigin), MgdDbgTestFuncInfo.TestType.Modify));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Highlight Entity", "Higlight entity using handle", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(HighlightEntityFromHandle), MgdDbgTestFuncInfo.TestType.Modify));
        }

        #region Tests

        public void
        XformMove()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            ObjectIdCollection selSet = null;
            if (Utils.AcadUi.GetSelSetFromUser(out selSet) == false)
                return;
             
            PromptPointOptions prBasePtOpts = new PromptPointOptions("\nBase point or displacement");
            PromptPointResult prBasePtRes = ed.GetPoint(prBasePtOpts);
            if (prBasePtRes.Status != PromptStatus.OK)
                return;
                
            Vector3d transVec = new Vector3d();

            PromptPointOptions prToPtOpts = new PromptPointOptions("\nTo point or <use first point as displacement>");
            prToPtOpts.UseBasePoint = true;
            prToPtOpts.BasePoint = prBasePtRes.Value;
            prToPtOpts.AllowNone = true;
            
            PromptPointResult prToPtRes = ed.GetPoint(prToPtOpts);
            if (prToPtRes.Status == PromptStatus.OK) 
                transVec = prToPtRes.Value - prBasePtRes.Value;
            else if (prToPtRes.Status == PromptStatus.None)
                transVec = prBasePtRes.Value.GetAsVector();
            else
                return; // was a cancel
            
            DoXform(selSet, Matrix3d.Displacement(Utils.Db.UcsToWcs(transVec)));
        }
        
        public void
        XformRotate()
        {
            ObjectIdCollection selSet = null;
            if (Utils.AcadUi.GetSelSetFromUser(out selSet) == false)
                return;
            
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptPointResult prBasePtRes = ed.GetPoint("\nBase point");
            if (prBasePtRes.Status == PromptStatus.OK) {
                PromptDoubleResult prRotAngRes = ed.GetAngle("\nRotation angle");
                if (prRotAngRes.Status == PromptStatus.OK) {
                    Vector3d axis = Utils.Db.GetUcsZAxis(Utils.Db.GetCurDwg());
                    DoXform(selSet, Matrix3d.Rotation(prRotAngRes.Value, axis, Utils.Db.UcsToWcs(prBasePtRes.Value)));
                }
            }
        }
        
        public void
        XformScale()
        {
            ObjectIdCollection selSet = null;
            if (Utils.AcadUi.GetSelSetFromUser(out selSet) == false)
                return;
                
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
             
            PromptPointResult prBasePtRes = ed.GetPoint("\nBase point");
            if (prBasePtRes.Status == PromptStatus.OK) {
                PromptDistanceOptions prScaleOpts = new PromptDistanceOptions("\nScale factor");
                prScaleOpts.AllowZero = false;
                prScaleOpts.AllowNegative = false;
                
                PromptDoubleResult prScaleRes = ed.GetDistance(prScaleOpts);

                if (prScaleRes.Status == PromptStatus.OK) {
                    DoXform(selSet, Matrix3d.Scaling(prScaleRes.Value, Utils.Db.UcsToWcs(prBasePtRes.Value)));
                }
            }
        }
        
        public void
        XformMirror()
        {
            m_db                      = Utils.Db.GetCurDwg();
            ObjectIdCollection selSet = null;

            if (Utils.AcadUi.GetSelSetFromUser(out selSet) == false)
                return;
                
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
             
            PromptPointResult prFirstPtRes = ed.GetPoint("\nFirst point on mirror line");
            if (prFirstPtRes.Status != PromptStatus.OK)
                return;
                
            PromptPointOptions prSecondPtOpts = new PromptPointOptions("\nSecond point on mirror line");
            prSecondPtOpts.UseBasePoint = true;
            prSecondPtOpts.BasePoint = prFirstPtRes.Value;
            
            PromptPointResult prSecondPtRes = ed.GetPoint(prSecondPtOpts);
            if (prSecondPtRes.Status != PromptStatus.OK)
                return;

            Line3d mirrorLine = new Line3d(Utils.Db.UcsToWcs(prFirstPtRes.Value), Utils.Db.UcsToWcs(prSecondPtRes.Value));

            bool doCopy;
            if (Utils.AcadUi.PromptYesNo("\nCopy the original entities", true, out doCopy) != PromptStatus.OK)
                return;
                
            if (doCopy)
                Utils.Db.CloneAndXformObjects(m_db, selSet, m_db.CurrentSpaceId, Matrix3d.Mirroring(mirrorLine));
            else
                DoXform(selSet, Matrix3d.Mirroring(mirrorLine));
        }
        
        public void
        XformWcsOrigin()
        {
            ObjectIdCollection selSet = null;
            if (Utils.AcadUi.GetSelSetFromUser(out selSet) == false)
                return;
                
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
             
            PromptPointResult prBasePtRes = ed.GetPoint("\nBase point");
            if (prBasePtRes.Status != PromptStatus.OK)
                return;
            
            Matrix3d xform = Utils.Db.GetUcsToWcsOriginMatrix(Utils.Db.UcsToWcs(prBasePtRes.Value), Utils.Db.GetCurDwg());
            DoXform(selSet, xform);
        }

        private void
        DoXform(ObjectIdCollection objIds, Matrix3d m)
        {
            if (objIds.Count == 0)
                return;
                
            Database db = objIds[0].Database;
            
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = db.TransactionManager;

            using (Transaction tr = tm.StartTransaction()) {
                Entity tmpEnt;
                foreach (ObjectId tmpId in objIds) {
                    tmpEnt = (Entity)tr.GetObject(tmpId, OpenMode.ForWrite);
                    tmpEnt.TransformBy(m);
                }
                
                tr.Commit();
            }
        }

        public void
        HighlightEntityFromHandle ()
        {
            ObjectId id = ObjectId.Null;
            DialogResult dlgResult;

            Database db = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;


            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = db.TransactionManager;

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptStringOptions options = new PromptStringOptions("Enter Handle");

            String str = ed.GetString(options).StringResult;

            if (str != String.Empty) {

                Handle h = Utils.Db.StringToHandle(str);

                try {
                    id = Utils.Db.HandleToObjectId(db, h);

                    using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                        Entity ent = tr.GetObject(id, OpenMode.ForWrite) as Entity;

                        if (ent != null) {
                            ent.Highlight();
                        }
                        else {
                            Utils.AcadUi.PrintToCmdLine((tr.GetObject(id, OpenMode.ForWrite)).GetType().FullName);
                        }
                        tr.Commit();
                    }

                    dlgResult = MessageBox.Show("Do you want to continue?", "MgdDbg", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes) {
                        using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                            Entity ent = tr.GetObject(id, OpenMode.ForWrite) as Entity;

                            if (ent != null) {
                                ent.Unhighlight();
                            }
                            else {
                                Utils.AcadUi.PrintToCmdLine((tr.GetObject(id, OpenMode.ForWrite)).GetType().FullName);
                            }
                            tr.Commit();
                        }
                    }

                }
                catch (Autodesk.AutoCAD.Runtime.Exception x) {
                    if (x.ErrorStatus != Autodesk.AutoCAD.Runtime.ErrorStatus.UnknownHandle) {
                        throw x;
                    }
                }
            }
        }

        public static Handle
        StringToHandle (String strHandle)
        {
            Handle handle = new Handle();

            try {
                Int64 nHandle = Convert.ToInt64(strHandle, 16);
                handle = new Handle(nHandle);
            }
            catch (System.FormatException) {
            }
            return handle;
        }

        #endregion
    }
}

