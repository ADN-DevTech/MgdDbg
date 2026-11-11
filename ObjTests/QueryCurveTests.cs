
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
using Autodesk.AutoCAD.Runtime;

using AcDb = Autodesk.AutoCAD.DatabaseServices;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace MgdDbg.Test
{
	/// <summary>
	/// Summary description for QueryCurveTests.
	/// </summary>
	public class QueryCurveTests : MgdDbgTestFuncs
	{
        private Database    m_db = null;
        private Editor      m_ed = null;

		public
		QueryCurveTests()
		{	               
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Area", "Get the area of the curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(Area), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Is closed", "Test whether the curve is closed", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(IsClosed), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Is periodic", "Test whether the curve is periodic", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(IsPeriodic), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get start/end points", "Add Point entity where the start and end points are", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(StartEndPoints), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get start/end params", "Get start and end params of the curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(StartEndParams), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get point at param", "Get point at a given parameter of the curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(PointAtParam), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get param at point", "Get param at a given point on the curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(ParamAtPoint), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get distance at param", "Get distance at a given param of the curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(DistanceAtParam), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get param at distance", "Get param at a given distance along the curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(ParamAtDistance), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get distance at point", "Get distance at a given point on the curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(DistanceAtPoint), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get point at distance", "Get point at a given distance along the curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(PointAtDistance), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get derivatives at param", "Get first and second derivatives", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(DerivativesAtParam), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get derivatives at point", "Get first and second derivatives", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(DerivativesAtPoint), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get closest point", "Get point on the curve closest to point picked", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(ClosestPointTo), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get closest point using direction", "Get point on the curve closest to point picked (in the direction specified)", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(ClosestPointToDirection), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get offset curves", "Get the offset of the given curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(OffsetCurves), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get offset curves given plane normal", "Get the offset of the given curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(OffsetCurvesGivenPlaneNormal), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get projected curve", "Get the projected curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(ProjectedCurve), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get ortho projected curve", "Get the projected curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(OrthoProjectedCurve), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get split curves from params", "Get the sub-curves given certain params", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(SplitCurvesParams), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Get split curves from points", "Get the sub-curves given certain points", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(SplitCurvesPoints), MgdDbgTestFuncInfo.TestType.Query));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Spline", "Get a Spline entity representing the curve", typeof(Curve), new MgdDbgTestFuncInfo.TestFunc(Spline), MgdDbgTestFuncInfo.TestType.Query));
        }

        #region Tests

        public void
        Area()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    m_ed.WriteMessage(string.Format("\nAREA: {0}", Converter.DistanceToString(curve.Area, DistanceUnitFormat.Decimal, -1)));

                    tr.Commit();
                }
            }
        }
        
        public void
        IsClosed()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    m_ed.WriteMessage(string.Format("\nCLOSED: {0}", curve.Closed.ToString()));

                    tr.Commit();
                }
            }
        }
        
        public void
        IsPeriodic()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    m_ed.WriteMessage(string.Format("\nPERIODIC: {0}", curve.IsPeriodic.ToString()));

                    tr.Commit();
                }
            }
        }

        public void
        StartEndPoints()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                        // get StartPoint (if appliacable) and make a point entity in Red
                        // to graphically depict it.
                    try {
                        Point3d startPt = curve.StartPoint;
                        m_ed.WriteMessage(string.Format("\nSTART POINT: {0}", Utils.AcadUi.PtToStr(startPt)));
                        Utils.Db.MakePointEnt(startPt, 1, tr, m_db);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.NotApplicable) 
                            m_ed.WriteMessage("\nSTART POINT: Not Applicable");
                        else
                            throw;
                    }

                        // get EndPoint (if appliacable) and make a point entity in Yellow
                        // to graphically depict it.
                    try {
                        Point3d endPt = curve.EndPoint;
                        m_ed.WriteMessage(string.Format("\nEND POINT:   {0}", Utils.AcadUi.PtToStr(endPt)));
                        Utils.Db.MakePointEnt(endPt, 2, tr, m_db);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.NotApplicable)
                            m_ed.WriteMessage("\nEND POINT: Not Applicable");
                        else
                            throw;
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        StartEndParams()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    try {
                        double startParam = curve.StartParam;
                        m_ed.WriteMessage(string.Format("\nSTART PARAM (as double): {0}", Converter.DistanceToString(startParam)));
                        m_ed.WriteMessage(string.Format("\nSTART PARAM (as angle):  {0}", Converter.AngleToString(startParam)));
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.NotApplicable) 
                            m_ed.WriteMessage("\nSTART PARAM: Not Applicable");
                        else
                            throw;
                    }

                    try {
                        double endParam = curve.EndParam;
                        m_ed.WriteMessage(string.Format("\nEND PARAM   (as double): {0}", Converter.DistanceToString(endParam)));
                        m_ed.WriteMessage(string.Format("\nEND PARAM   (as angle):  {0}", Converter.AngleToString(endParam)));
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.NotApplicable)
                            m_ed.WriteMessage("\nEND PARAM: Not Applicable");
                        else
                            throw;
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        PointAtParam()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    PrintParamInterval(curve);
                    
                    double param;
                    if (GetCurveParam(out param)) {
                        try {
                            Point3d pt = curve.GetPointAtParameter(param);
                            m_ed.WriteMessage(string.Format("\nPOINT: {0}", Utils.AcadUi.PtToStr(pt)));
                            Utils.Db.MakePointEnt(pt, 3, tr, m_db);
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception e) {
                            m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                        }
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        ParamAtPoint()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                                        
                    try {
                            // the point picked is not necessarily on the curve (unless we used
                            // an OSNAP that forced it on the curve).  We will first get the closest
                            // point on the curve from the picked point and then use that.
                        Point3d ptOnCurve = curve.GetClosestPointTo(Utils.Db.UcsToWcs(pickPt), false);
                        double param = curve.GetParameterAtPoint(ptOnCurve);
                        
                        m_ed.WriteMessage(string.Format("\nPARAM (as double): {0}", Converter.DistanceToString(param)));
                        m_ed.WriteMessage(string.Format("\nPARAM (as angle):  {0}", Converter.AngleToString(param)));
                        Utils.Db.MakePointEnt(ptOnCurve, 3, tr, m_db);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        DistanceAtParam()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    PrintParamInterval(curve);
                    
                    double param;
                    if (GetCurveParam(out param)) {
                        try {
                            double dist = curve.GetDistanceAtParameter(param);
                            m_ed.WriteMessage(string.Format("\nDISTANCE: {0}", Converter.DistanceToString(dist)));
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception e) {
                            m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                        }
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        ParamAtDistance()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                    // first prompt for distance along the curve
                PromptDoubleResult res = m_ed.GetDistance("\nDistance along curve");
                if (res.Status != PromptStatus.OK) {
                    return;
                }

                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    try {
                        double param = curve.GetParameterAtDistance(res.Value);
                        m_ed.WriteMessage(string.Format("\nPARAM (as double): {0}", Converter.DistanceToString(param)));
                        m_ed.WriteMessage(string.Format("\nPARAM (as angle):  {0}", Converter.AngleToString(param)));
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        DistanceAtPoint()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                                        
                    try {
                            // the point picked is not necessarily on the curve (unless we used
                            // an OSNAP that forced it on the curve).  We will first get the closest
                            // point on the curve from the picked point and then use that.
                        Point3d ptOnCurve = curve.GetClosestPointTo(Utils.Db.UcsToWcs(pickPt), false);
                        double dist = curve.GetDistAtPoint(ptOnCurve);     // TBD: function should be called GetDistanceAtPoint()
                        
                        m_ed.WriteMessage(string.Format("\nDISTANCE: {0}", Converter.DistanceToString(dist)));
                        Utils.Db.MakePointEnt(ptOnCurve, 3, tr, m_db);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        PointAtDistance()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                    // first prompt for distance along the curve
                PromptDoubleResult res = m_ed.GetDistance("\nDistance along curve");
                if (res.Status != PromptStatus.OK) {
                    return;
                }

                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    try {
                        Point3d ptOnCurve = curve.GetPointAtDist(res.Value);     // TBD: function should be called GetPointAtDistance()
                        m_ed.WriteMessage(string.Format("\nPOINT: {0}", Utils.AcadUi.PtToStr(ptOnCurve)));
                        Utils.Db.MakePointEnt(ptOnCurve, 3, tr, m_db);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        DerivativesAtParam()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    PrintParamInterval(curve);
                    
                    double param;
                    if (GetCurveParam(out param)) {
                            // We need a point on the curve to create the Ray entities we will
                            // use to mark the two axes.  If the user gives us an invalid param,
                            // we aren't catching it here, so it will throw an exception.  Under
                            // normal circumstances, we could just put this in the try/catch block
                            // below.  Here, we want to demonstrate each possible error.
                        try {
                            Point3d ptOnCurve = curve.GetPointAtParameter(param);
                            try {
                                Vector3d firstDeriv = curve.GetFirstDerivative(param);
                                m_ed.WriteMessage(string.Format("\nFIRST DERIV:  {0}", Utils.AcadUi.VecToStr(firstDeriv)));
                                Utils.Db.MakeRayEnt(ptOnCurve, firstDeriv, 1, tr, m_db);
                            }
                            catch (Autodesk.AutoCAD.Runtime.Exception e) {
                                m_ed.WriteMessage("\nFirst Derivative ERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                            }
                            
                            try {
                                Vector3d secondDeriv = curve.GetSecondDerivative(param);
                                m_ed.WriteMessage(string.Format("\nSECOND DERIV: {0}", Utils.AcadUi.VecToStr(secondDeriv)));
                                Utils.Db.MakeRayEnt(ptOnCurve, secondDeriv, 2, tr, m_db);
                            }
                            catch (Autodesk.AutoCAD.Runtime.Exception e) {
                                m_ed.WriteMessage("\nSecond Derivative ERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                            }
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception e) {
                            m_ed.WriteMessage("\nParameter is not on curve: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                            return;
                        }
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        DerivativesAtPoint()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                        // Again, we could put these all in a single try/catch block, but we
                        // want to catch all errors individually so we can demonstrate how the
                        // functions behave.
                    try {
                            // the point picked is not necessarily on the curve (unless we used
                            // an OSNAP that forced it on the curve).  We will first get the closest
                            // point on the curve from the picked point and then use that.
                        Point3d ptOnCurve = curve.GetClosestPointTo(Utils.Db.UcsToWcs(pickPt), false);
                        Utils.Db.MakePointEnt(ptOnCurve, 3, tr, m_db);
                        
                        try {
                            Vector3d firstDeriv = curve.GetFirstDerivative(ptOnCurve);
                            m_ed.WriteMessage(string.Format("\nFIRST DERIV:  {0}", Utils.AcadUi.VecToStr(firstDeriv)));
                            Utils.Db.MakeRayEnt(ptOnCurve, firstDeriv, 1, tr, m_db);
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception e) {
                            m_ed.WriteMessage("\nFirst Derivative ERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                        }
                        
                        try {
                            Vector3d secondDeriv = curve.GetSecondDerivative(ptOnCurve);
                            m_ed.WriteMessage(string.Format("\nSECOND DERIV: {0}", Utils.AcadUi.VecToStr(secondDeriv)));
                            Utils.Db.MakeRayEnt(ptOnCurve, secondDeriv, 2, tr, m_db);
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception e) {
                            m_ed.WriteMessage("\nSecond Derivative ERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                        }
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nPoint is not on curve: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                        return;
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        ClosestPointTo()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                    // let user pick an arbitrary point
                PromptPointResult prPtRes = m_ed.GetPoint("\nEnter a point");
                if (prPtRes.Status != PromptStatus.OK)
                    return;
                
                bool allowExtend = false;
                if (Utils.AcadUi.PromptYesNo("\nExtend curve?", allowExtend, out allowExtend) != PromptStatus.OK)
                    return;
                    
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                                        
                    try {
                        Point3d ptOnCurve = curve.GetClosestPointTo(Utils.Db.UcsToWcs(prPtRes.Value), allowExtend);

                        m_ed.WriteMessage(string.Format("\nCLOSEST PT:  {0}", Utils.AcadUi.PtToStr(ptOnCurve)));
                        Utils.Db.MakePointEnt(Utils.Db.UcsToWcs(prPtRes.Value), 4, tr, m_db);
                        Utils.Db.MakePointEnt(ptOnCurve, 3, tr, m_db);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        ClosestPointToDirection()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                    // let user pick an arbitrary point
                PromptPointResult prPtRes = m_ed.GetPoint("\nEnter a point");
                if (prPtRes.Status != PromptStatus.OK)
                    return;
                    
                    // find out which direction they want to go
                PromptPointOptions prDirPtOpts = new PromptPointOptions("\nDirection");
                prDirPtOpts.BasePoint = prPtRes.Value;
                prDirPtOpts.UseBasePoint = true;
                
                PromptPointResult prDirPtRes = m_ed.GetPoint(prDirPtOpts);
                if (prDirPtRes.Status != PromptStatus.OK)
                    return;
                    
                Vector3d dirVec = Utils.Db.UcsToWcs(prPtRes.Value - prDirPtRes.Value);
                if (dirVec.IsZeroLength()) {
                    m_ed.WriteMessage("\nDirection vector is zero-length!");
                    return;
                }
                
                bool allowExtend = false;
                if (Utils.AcadUi.PromptYesNo("\nExtend curve?", allowExtend, out allowExtend) != PromptStatus.OK)
                    return;
                    
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                                        
                    try {
                        Point3d ptOnCurve = curve.GetClosestPointTo(Utils.Db.UcsToWcs(prPtRes.Value), dirVec, allowExtend);

                        m_ed.WriteMessage(string.Format("\nCLOSEST PT:  {0}", Utils.AcadUi.PtToStr(ptOnCurve)));
                        Utils.Db.MakePointEnt(Utils.Db.UcsToWcs(prPtRes.Value), 4, tr, m_db);
                        Utils.Db.MakePointEnt(ptOnCurve, 3, tr, m_db);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        OffsetCurves()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                    // first prompt for offset distance (can be negative to go other direction)
                PromptDoubleResult res = m_ed.GetDistance("\nOffset distance");
                if (res.Status != PromptStatus.OK) {
                    return;
                }
    
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                                        
                    try {
                        DBObjectCollection newCurves = curve.GetOffsetCurves(res.Value);
                            // bump the color index of each curve returned so we can see how
                            // many there are.
                        int colorIndex = 1;
                        foreach (Entity ent in newCurves)
                            ent.ColorIndex = colorIndex++;
                            
                        Utils.SymTbl.AddToCurrentSpace(newCurves, m_db, tr);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        /// <summary>
        /// TBD: this one doesn't seem to work too well (or else I don't understand it)
        /// </summary>
        public void
        OffsetCurvesGivenPlaneNormal()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                    // first prompt for offset distance (can be negative to go other direction)
                PromptDoubleResult res = m_ed.GetDistance("\nOffset distance");
                if (res.Status != PromptStatus.OK) {
                    return;
                }
                
                Vector3d planeVec = new Vector3d(1.0, 1.0, 1.0);    // arbitrary plane normal
    
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                                        
                    try {
                        DBObjectCollection newCurves = curve.GetOffsetCurvesGivenPlaneNormal(planeVec, res.Value);
                            // bump the color index of each curve returned so we can see how
                            // many there are.
                        int colorIndex = 1;
                        foreach (Entity ent in newCurves)
                            ent.ColorIndex = colorIndex++;
                            
                        Utils.SymTbl.AddToCurrentSpace(newCurves, m_db, tr);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        /// <summary>
        /// To see how this one works, do the following:
        ///     1)  Draw curves in WCS
        ///     2)  Create a new UCS with X, Y, and Z axes rotated 45 degrees each
        ///     3)  Go to PLAN view in the new UCS
        ///     4)  Run the test.  You should see red curves overlap the originals
        ///     5)  Do command UCS,World
        ///     6)  Go to PLAN view in the now established WCS
        ///     7)  The red projected curves should now be in a totally different plane
        /// </summary>
        
        public void
        ProjectedCurve()
        {
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            m_db = Utils.Db.GetCurDwg();

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            m_ed.WriteMessage("\nProjecting onto current UCS plane with VIEWDIR as projection direction...");

            Plane planeToProjectOn = Utils.Db.GetUcsPlane(m_db);
            Point3d tmpPt = (Point3d)AcadApp.GetSystemVariable("viewdir");
            Vector3d projDir = Utils.Db.UcsToWcs(tmpPt.GetAsVector());
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {

                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    try {
                        Curve projCurve = curve.GetProjectedCurve(planeToProjectOn, projDir);
                        projCurve.ColorIndex = 1;
                        
                        Utils.SymTbl.AddToCurrentSpace(projCurve, m_db, tr);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        /// <summary>
        /// See notes for ProjecteCurve() for how to demonstrate this.
        /// </summary>
        
        public void
        OrthoProjectedCurve()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            Plane planeToProjectOn = Utils.Db.GetUcsPlane(m_db);
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {

                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    try {
                        Curve projCurve = curve.GetOrthoProjectedCurve(planeToProjectOn);
                        projCurve.ColorIndex = 1;
                        
                        Utils.SymTbl.AddToCurrentSpace(projCurve, m_db, tr);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        SplitCurvesParams()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
                        
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    PrintParamInterval(curve);
                    
                    DoubleCollection paramVals;
                    if (GetCurveParams(out paramVals) == false)
                        return;
                    
                    try {
                        DBObjectCollection splitCurves = curve.GetSplitCurves(paramVals);
                        
                        int colorIndex = 1;
                        foreach (Entity ent in splitCurves)
                            ent.ColorIndex = colorIndex++;
                        
                        Utils.SymTbl.AddToCurrentSpace(splitCurves, m_db, tr);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        SplitCurvesPoints()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
                        
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                                        
                    Point3dCollection pts;
                    if (GetCurvePoints(curve, out pts) == false)
                        return;
                    
                    try {
                        DBObjectCollection splitCurves = curve.GetSplitCurves(pts);
                        
                        int colorIndex = 1;
                        foreach (Entity ent in splitCurves)
                            ent.ColorIndex = colorIndex++;
                        
                        Utils.SymTbl.AddToCurrentSpace(splitCurves, m_db, tr);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nERROR: {0}", ((ErrorStatus)e.ErrorStatus).ToString());
                    }
                    
                    tr.Commit();
                }
            }
        }
        
        public void
        Spline()
        {
            m_db = Utils.Db.GetCurDwg();
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

            ObjectId curveId = ObjectId.Null;
            Point3d pickPt = new Point3d();
            
            while (GetCurveEntity(ref curveId, ref pickPt)) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Curve curve = (Curve)tr.GetObject(curveId, OpenMode.ForRead);
                    
                    try {
                        Spline newSpline = curve.Spline;
                        newSpline.ColorIndex = 1;
                        Utils.SymTbl.AddToCurrentSpace(newSpline, m_db, tr);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.NotApplicable ||
                            e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.NotImplementedYet)
                            m_ed.WriteMessage("\nSPLINE: Not Applicable");
                        else
                            throw;
                    }
                    
                    tr.Commit();
                }
            }
        }

        #endregion

        #region Utils

        private bool
        GetCurveEntity(ref ObjectId curveId, ref Point3d pickPt)
        {           
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptEntityOptions prCurveOpts = new PromptEntityOptions("\nSelect a Curve");
            prCurveOpts.SetRejectMessage("\nSelected entity must be of type Curve.");
            prCurveOpts.AddAllowedClass(typeof(Curve), false);
            
            PromptEntityResult prCurveRes = ed.GetEntity(prCurveOpts);
            if (prCurveRes.Status == PromptStatus.OK) {
                curveId = prCurveRes.ObjectId;
                pickPt = prCurveRes.PickedPoint;
                return true;
            }
            else {
                curveId = ObjectId.Null;
                pickPt = Point3d.Origin;
                return false;
            }

        }
        
        /// <summary>
        /// Print the curve's param interval so that the user knows the range of valid values to input.
        /// </summary>
        /// <param name="curve">The curve in question</param>
        
        public void
        PrintParamInterval(Curve curve)
        {
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            try {
                double startParam = curve.StartParam;
                double endParam   = curve.EndParam;
                
                m_ed.WriteMessage("\nParam interval (as double): ({0} --> {1})",
                        Converter.DistanceToString(startParam),
                        Converter.DistanceToString(endParam));
                 m_ed.WriteMessage("\nParam interval (as angle): ({0} --> {1})",
                        Converter.AngleToString(startParam),
                        Converter.AngleToString(endParam));
            }
            catch (Autodesk.AutoCAD.Runtime.Exception e) {
                if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.NotApplicable) 
                    m_ed.WriteMessage("\nParam Interval: Not Applicable");
                else
                    throw;
            }
        }
        
        private bool
        GetCurveParam(out double param)
        {
            m_ed  = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            param = 0.0;
            
            bool asAngles;
            if (Utils.AcadUi.PromptYesNo("\nInput param as angle", true, out asAngles) != PromptStatus.OK)
                return false;

            if (asAngles) {
                PromptDoubleResult res = m_ed.GetAngle("\nParam angle of curve");
                if (res.Status == PromptStatus.OK) {
                    param = res.Value;
                    return true;
                }
            }
            else {
                PromptDoubleResult res = m_ed.GetDistance("\nParam of curve");
                if (res.Status == PromptStatus.OK) {
                    param = res.Value;
                    return true;
                }
            }
            return false;
        }
        
        private bool
        GetCurveParams(out DoubleCollection paramVals)
        {
            m_ed      = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            paramVals = new DoubleCollection();
            
            bool asAngles;
            if (Utils.AcadUi.PromptYesNo("\nInput params as angle", true, out asAngles) != PromptStatus.OK)
                return false;

            if (asAngles) {
                PromptAngleOptions prAngOpts = new PromptAngleOptions("\nParam angle of curve");
                prAngOpts.AllowNone = true;
                
                while (true) {
                    PromptDoubleResult prRes = m_ed.GetAngle(prAngOpts);
                    if (prRes.Status == PromptStatus.OK)
                        paramVals.Add(prRes.Value);
                    else if (prRes.Status == PromptStatus.None)
                        return true;        // done inputting values
                    else
                        return false;       // canceled
                }
            }
            else {
                PromptDistanceOptions prDistOpts = new PromptDistanceOptions("\nParam of curve");
                prDistOpts.AllowNone = true;
                
                while (true) {
                    PromptDoubleResult prRes = m_ed.GetDistance(prDistOpts);
                    if (prRes.Status == PromptStatus.OK)
                        paramVals.Add(prRes.Value);
                    else if (prRes.Status == PromptStatus.None)
                        return true;        // done inputting values
                    else
                        return false;       // canceled
                }
            }
        }
        
        private bool
        GetCurvePoints(Curve curve, out Point3dCollection pts)
        {
            m_ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            pts  = new Point3dCollection();
            
            PromptPointOptions prPtOpts = new PromptPointOptions("\nPoint on curve");
            prPtOpts.AllowNone = true;
            
            while (true) {
                PromptPointResult prRes = m_ed.GetPoint(prPtOpts);
                if (prRes.Status == PromptStatus.OK) {
                    try {
                        Point3d ptOnCurve = curve.GetClosestPointTo(Utils.Db.UcsToWcs(prRes.Value), false);
                        pts.Add(ptOnCurve);
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception e) {
                        m_ed.WriteMessage("\nClosest point not on curve: {0}", ((ErrorStatus)e.ErrorStatus).ToString());                        
                    }
                }
                else if (prRes.Status == PromptStatus.None)
                    return true;        // done inputting values
                else
                    return false;       // canceled
            }
        }
        #endregion
    }
}
