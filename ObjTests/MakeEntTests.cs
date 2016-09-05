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

namespace MgdDbg.Test
{
	/// <summary>
    /// The entmake functions in this module are designed to show how to create
    /// entities based on user input.  Therefore, things like the current UCS are
    /// of concern because points input by the user are in UCS, but points in the
    /// database must be in WCS.  The general strategy is:
    ///
    ///        1) acquire points from the user in UCS without translating them to WCS.
    ///        2) do not set the Normal for any entity explicitly.  Instead, let it
    ///           default to (0, 0, 1).
    ///        3) transform the entity from the UCS to WCS to get the correct points,
    ///           normals, and angles that the database will expect.
    ///
    /// This method allows you to "pretend" that everything is in WCS and you don't
    /// have to bother with trasforming individual points and reverse engineering
    /// any angles input by the user.
    ///
    /// Also, I try to demonstrate several different "styles" of handling errors and
    /// exceptions.  I'm still experimenting with things myself, so bear with me a bit
    /// until I can come to grips with the best practices. (jma - 04/28/04)
    /// </summary>
    
	public class MakeEntTests : MgdDbgTestFuncs
	{
	    private Database    m_db = null;
	    
		public
		MakeEntTests()
		{	    
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Line", "Hardwired line from (0,0) to (50, 50)", typeof(Line), new MgdDbgTestFuncInfo.TestFunc(LineHardwired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Circle (WCS-only)", "Hardwired circle [Center=(10,10) Radius=(6)].  Will not be relative to currnt UCS.", typeof(Circle), new MgdDbgTestFuncInfo.TestFunc(CircleHardwiredWcsOnly), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Circle", "Hardwired circle [Center=(10,10) Radius=(6)]", typeof(Circle), new MgdDbgTestFuncInfo.TestFunc(CircleHardwired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Solid", "Hardwired solid [Triangle with thickness]", typeof(Solid), new MgdDbgTestFuncInfo.TestFunc(SolidHardwired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Arc", "Hardwired arc [Center=(12,12) Radius=(6)]", typeof(Arc), new MgdDbgTestFuncInfo.TestFunc(ArcHardwired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Polyline (light weight)", "Hardwired Polyline [Rectangle=(15 x 8)]", typeof(Polyline), new MgdDbgTestFuncInfo.TestFunc(PolylineHardwired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Polyline 2d", "Hardwired Polyline 2d [Rectangle=(15 x 8)]", typeof(Polyline2d), new MgdDbgTestFuncInfo.TestFunc(Polyline2dHardwired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Polyline 3d", "Hardwired Polyline 3d [Rectangle=(15 x 8)]", typeof(Polyline3d), new MgdDbgTestFuncInfo.TestFunc(Polyline3dHardwired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Polyline (width & bulge)", "Hardwired Polyline [Rectangle=(15 x 8), Width=1, Filleted]", typeof(Polyline), new MgdDbgTestFuncInfo.TestFunc(PolylineWidthFillet), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Hatch", "Hardwired triangle with fill", typeof(Hatch), new MgdDbgTestFuncInfo.TestFunc(HatchHardwired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Assoc Hatch", "Hardwired triangle with void, and boundaries are associative", typeof(Hatch), new MgdDbgTestFuncInfo.TestFunc(HatchAssoc), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("3D Face", "Hardwired 3D Face", typeof(Face), new MgdDbgTestFuncInfo.TestFunc(FaceHardwired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Point", "User picked point", typeof(DBPoint), new MgdDbgTestFuncInfo.TestFunc(Point), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Aligned Dimension", "Aligned dim based on two points", typeof(Dimension), new MgdDbgTestFuncInfo.TestFunc(DimAligned), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Comp Builder (Current Space)", "Test of the CompBldr object", typeof(Entity), new MgdDbgTestFuncInfo.TestFunc(CompBldrCurSpace), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Lineweight test", "Test of each lineweight", typeof(Line), new MgdDbgTestFuncInfo.TestFunc(Lineweights), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Ray", "Hardwired Ray [From=(0,0) To=(50,50)]", typeof(Ray), new MgdDbgTestFuncInfo.TestFunc(RayHardWired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Leader", "Hardwired Leader [From=(0,0) Through=(50,50) To=(100,50)]", typeof(Leader), new MgdDbgTestFuncInfo.TestFunc(LeaderHardWired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Ellipse", "Hardwired Ellipse [Center=(50,50)]", typeof(Ellipse), new MgdDbgTestFuncInfo.TestFunc(EllipseHardWired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Spline", "Hardwired Spline", typeof(Spline), new MgdDbgTestFuncInfo.TestFunc(SplineHardWired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Helix", "Hardwired Helix", typeof(Helix), new MgdDbgTestFuncInfo.TestFunc(HelixHardWired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Xline", "Hardwired Xline", typeof(Xline), new MgdDbgTestFuncInfo.TestFunc(XlineHardWired), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Simple Table", "Hardwired Table", typeof(Table), new MgdDbgTestFuncInfo.TestFunc(TableHardWired), MgdDbgTestFuncInfo.TestType.Create));
        }

        #region Tests

        public void
        LineHardwired()
        {
                // This shows the style that C++ programmers are used to.  In this
                // simple case, it is probably OK because it is very unlikely that
                // any exceptions will be thrown between the time the Line object
                // is instantiated and the time it gets (safely) added to the database
                // by AddToCurrentSpaceAndClose().  Technically, an exception could be
                // thrown in a function like TransformToWcs().  Would that be bad?
                // When an exception is thrown we want to cleanly return to the command
                // line and leave the database/system in the same state it was before
                // we started.  In this case, we wouldn't leave any messes behind because
                // we only have a single entity that never got added.
                // So, for this simple case, the only drawback is that we left the new
                // Line entity around for garbage collection to deal with.  This isn't
                // so bad in a pure .NET environment, but we need to remember that the Line
                // object is really a managed wrapper that points to an unmanaged resource
                // in C++.  So, having alot of these guys around will trick the garbage
                // collector into thinking there isn't as much pressure to clean up
                // as there really is.  Therefore, for anything but super-trivial cases,
                // it is advised to use one of the techniques demonstrated below
                // (a using statement, transactions, Component builders, etc.)
            m_db = Utils.Db.GetCurDwg();

            Line line = new Line();
            line.StartPoint = new Point3d(0.0, 0.0, 0.0);
            line.EndPoint = new Point3d(50.0, 50.0, 0.0);
            line.ColorIndex = 1;
            
            Utils.Db.TransformToWcs(line, m_db);
            Utils.SymTbl.AddToCurrentSpaceAndClose(line, m_db);
        }
        
        public void
        CircleHardwired()
        {
                // To be exception safe, we need to keep things in a
                // try/catch/finally block.  Any error that happens
                // within the try block will be caught and we will
                // clean up our new Circle.  However, it is painful
                // to write all this housekeeping code each time we
                // want to use an object.  The other methods of creating
                // objects makes the process a bit less tedious.

            m_db = Utils.Db.GetCurDwg();

            Circle circ = null;
            try {
                circ = new Circle();
                circ.ColorIndex = 4;
                circ.Center = new Point3d(10.0, 10.0, 0.0);
                circ.Radius = 6.0;
                
                Utils.Db.TransformToWcs(circ, m_db);
                Utils.SymTbl.AddToCurrentSpaceAndClose(circ, m_db);
            }
            catch (System.Exception e) {
                    // maybe we want to do something here, maybe not?
                throw e;
            }
            finally {
                if (circ != null)
                    circ.Dispose();
            }
        }

        public void
        SolidHardwired()
        {
            // To be exception safe, we need to keep things in a
            // try/catch/finally block.  Any error that happens
            // within the try block will be caught and we will
            // clean up our new Circle.  However, it is painful
            // to write all this housekeeping code each time we
            // want to use an object.  The other methods of creating
            // objects makes the process a bit less tedious.

            m_db = Utils.Db.GetCurDwg();

            Solid solid = null;
            try
            {
                solid = new Solid();
                solid.ColorIndex = 4;
                solid.SetPointAt(0, new Point3d( 0.0,  0.0, 0.0));
                solid.SetPointAt(1, new Point3d( 0.0, 100.0, 0.0));
                solid.SetPointAt(2, new Point3d(100.0,  0.0, 0.0));
                solid.SetPointAt(3, new Point3d( 0.0,  0.0, 0.0));
                solid.Thickness = 20.0;

                Utils.Db.TransformToWcs(solid, m_db);
                Utils.SymTbl.AddToCurrentSpaceAndClose(solid, m_db);
            }
            catch (System.Exception e)
            {
                // maybe we want to do something here, maybe not?
                throw e;
            }
            finally
            {
                if (solid != null)
                    solid.Dispose();
            }
        }

        public void
        CircleHardwiredWcsOnly()
        {
            m_db = Utils.Db.GetCurDwg();

                // The using statement is equivalent to the try/catch/finally
                // block in the above example.  The finally clause will call
                // Dispose() on the Circle and release the underlying unmanaged
                // C++ object (AcDbCircle).
            using (Circle circ = new Circle()) {
                circ.ColorIndex = 3;
                circ.Center = new Point3d(10.0, 10.0, 0.0);
                circ.Radius = 6.0;
                
                // NOTE: we are specifically not transforming from UCS, so everything
                // is expressed directly in WCS space.  This is to demonstrate what
                // happens if you don't do it the way we are doing all the others.
                
                //Utils.Db.TransformToWcs(circ, m_db);   // without this call, it won't be relative to the current UCS

                Utils.SymTbl.AddToCurrentSpaceAndClose(circ, m_db);
            }
        }
                
        public void
        ArcHardwired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (Arc arc = new Arc()) {
                arc.ColorIndex = 4;
                arc.Center = new Point3d(12.0, 12.0, 0.0);
                arc.Radius = 6.0;
                arc.StartAngle = Utils.Ge.DegreesToRadians(30.0);
                arc.EndAngle = Utils.Ge.kRad90;
                
                Utils.Db.TransformToWcs(arc, m_db);
                Utils.SymTbl.AddToCurrentSpaceAndClose(arc, m_db);
            }
        }
        
        public void
        PolylineHardwired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (Polyline pline = new Polyline()) {
                pline.Color = Color.FromRgb(42, 184, 185);
                
                Point2d[] pts = new Point2d[4];
                pts[0] = new Point2d(0.0, 0.0);
                pts[1] = new Point2d(15.0, 0.0);
                pts[2] = new Point2d(15.0, 8.0);
                pts[3] = new Point2d(0.0, 8.0);
                
                for (int i=0; i<pts.Length; i++)
                    pline.AddVertexAt(i, pts[i], 0.0, 0.0, 0.0);
                    
                pline.Closed = true;
                
                Utils.Db.TransformToWcs(pline, m_db);
                Utils.SymTbl.AddToCurrentSpaceAndClose(pline, m_db);
            }
        }

        public void
        PolylineWidthFillet()
        {
            m_db = Utils.Db.GetCurDwg();

                // set up the input parameters for width/depth/filletRad
            double xLen = 15.0;
            double yLen = 8.0;
            double filletDist = 2.0;
            Point2d tmpPt = new Point2d(5.0, 5.0);  // "origin"
           
                // we'll make a filleted Rectangle
            Point2d[] pts = new Point2d[8];
            double[] bulges = new double[8];
            
                // start at bottom left (after fillet)
            tmpPt = new Point2d(tmpPt.X + filletDist, tmpPt.Y);
            pts[0] = tmpPt;
            
                // bottom right (going into fillet)
            tmpPt = new Point2d(tmpPt.X + (xLen - filletDist), tmpPt.Y);
            pts[1] = tmpPt;
            
                // bottom right (after fillet)
            tmpPt = new Point2d(tmpPt.X + filletDist, tmpPt.Y + filletDist);
            pts[2] = tmpPt;
            
                // top right (going into fillet)
            tmpPt = new Point2d(tmpPt.X, tmpPt.Y + (yLen - filletDist));
            pts[3] = tmpPt;
            
                // top right (after fillet)
            tmpPt = new Point2d(tmpPt.X - filletDist, tmpPt.Y + filletDist);
            pts[4] = tmpPt;
            
                // top left (going into fillet)
            tmpPt = new Point2d(tmpPt.X - (xLen - filletDist), tmpPt.Y);
            pts[5] = tmpPt;
            
                // top left (after fillet)
            tmpPt = new Point2d(tmpPt.X - filletDist, tmpPt.Y - filletDist);
            pts[6] = tmpPt;
            
                // bottom left (going into fillet)
            tmpPt = new Point2d(tmpPt.X, tmpPt.Y - (yLen - filletDist));
            pts[7] = tmpPt;
            
            double bulge = Math.Tan(Utils.Ge.kRad90 / 4.0);  // the bulge factor is tangent of a 4th of the included angle, here 90 degrees.
            bulges[0] = bulges[2] = bulges[4] = bulges[6] = 0.0;
            bulges[1] = bulges[3] = bulges[5] = bulges[7] = bulge;
            
                // now make the polyline entity
            using (Polyline pline = new Polyline()) {
                pline.ColorIndex = 4;
                for (int i=0; i<pts.Length; i++)
                    pline.AddVertexAt(i, pts[i], bulges[i], 0.0, 0.0);
                
                pline.ConstantWidth = 1.0;
                pline.Closed = true;
                
                Utils.Db.TransformToWcs(pline, m_db);
                Utils.SymTbl.AddToCurrentSpaceAndClose(pline, m_db);
            }
        }
        
        public void
        HatchHardwired()
        {
            m_db = Utils.Db.GetCurDwg();

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                
                    // make boundary triangle
                Polyline pline = new Polyline();
                pline.ColorIndex = 1;
                pline.AddVertexAt(0, new Point2d(0.0, 0.0), 0.0, 0.0, 0.0);
                pline.AddVertexAt(0, new Point2d(30.0, 0.0), 0.0, 0.0, 0.0);
                pline.AddVertexAt(0, new Point2d(15.0, 30.0), 0.0, 0.0, 0.0);
                pline.Closed = true;
                
                Utils.Db.TransformToWcs(pline, m_db);
                Utils.SymTbl.AddToCurrentSpace(pline, m_db, tr);
                
                ObjectIdCollection boundaryIds = new ObjectIdCollection();
                boundaryIds.Add(pline.ObjectId);
                
                Hatch hatch = new Hatch();
                hatch.ColorIndex = 4;
                
                Utils.Db.TransformToWcs(hatch, m_db);   // NOTE: need to transform to correct plane *BEFORE* setting pattern and boundary info
                
                hatch.PatternAngle = Utils.Ge.kRad45;
                hatch.PatternScale = 4.0;
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "ESCHER");
                
                hatch.Associative = false;      // not in this sample

                hatch.AppendLoop(HatchLoopTypes.Default, boundaryIds);
                hatch.EvaluateHatch(false);
                
                Utils.SymTbl.AddToCurrentSpace(hatch, m_db, tr);
                
                tr.Commit();
            }
        }
        
        public void
        HatchAssoc()
        {
            m_db = Utils.Db.GetCurDwg();

            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;
            using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {

                    // make boundary triangle
                Polyline pline1 = new Polyline();
                pline1.ColorIndex = 211;
                pline1.AddVertexAt(0, new Point2d(0.0, 0.0), 0.0, 0.0, 0.0);
                pline1.AddVertexAt(0, new Point2d(30.0, 0.0), 0.0, 0.0, 0.0);
                pline1.AddVertexAt(0, new Point2d(15.0, 30.0), 0.0, 0.0, 0.0);
                pline1.Closed = true;
                Utils.Db.TransformToWcs(pline1, m_db);
                Utils.SymTbl.AddToCurrentSpace(pline1, m_db, tr);
                
                    // offset the triangle and add those entities to the db
                DBObjectCollection offsetCurves = pline1.GetOffsetCurves(5.0);
                Utils.SymTbl.AddToCurrentSpace(offsetCurves, m_db, tr);
                
                ObjectIdCollection boundaryIds1 = new ObjectIdCollection();
                boundaryIds1.Add(pline1.ObjectId);
                
                ObjectIdCollection boundaryIds2 = new ObjectIdCollection();
                foreach (Entity tmpEnt in offsetCurves) {
                    boundaryIds2.Add(tmpEnt.ObjectId);
                }
                
                Hatch hatch = new Hatch();
                hatch.ColorIndex = 141;
                
                Utils.Db.TransformToWcs(hatch, m_db);   // NOTE: need to transform to correct plane *BEFORE* setting pattern and boundary info

                hatch.PatternAngle = Utils.Ge.kRad0;
                hatch.PatternScale = 4.0;
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "STARS");
                
                hatch.HatchStyle = HatchStyle.Normal;
                Utils.SymTbl.AddToCurrentSpace(hatch, m_db, tr);
                
                    // To make an associative hatch we have to first add it to the database (above).
                    // When we call AppendLoop(), it will add the Persistent Reactor that keeps the
                    // boundary associative.
                hatch.Associative = true;
                hatch.AppendLoop(HatchLoopTypes.Default, boundaryIds1);
                if (boundaryIds2.Count > 0)
                    hatch.AppendLoop(HatchLoopTypes.Default, boundaryIds2);
                
                hatch.EvaluateHatch(false);
                
                tr.Commit();
            }
        }
                
        public void
        FaceHardwired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (Face face = new Face()) {
                face.ColorIndex = 1;
                
                double sqSize = 10.0;
                
                    // make a 10 x 10 square face
                Point3d tmpPt = new Point3d(0.0, 0.0, 0.0);
                face.SetVertexAt(0, tmpPt);
                
                tmpPt = new Point3d(tmpPt.X + sqSize, tmpPt.Y, tmpPt.Z);
                face.SetVertexAt(1, tmpPt);
                
                tmpPt = new Point3d(tmpPt.X, tmpPt.Y + sqSize, tmpPt.Z);
                face.SetVertexAt(2, tmpPt);
                
                tmpPt = new Point3d(tmpPt.X - sqSize, tmpPt.Y, tmpPt.Z);
                face.SetVertexAt(3, tmpPt);
                
                Utils.Db.TransformToWcs(face, m_db);
                Utils.SymTbl.AddToCurrentSpaceAndClose(face, m_db);
            }
        }
        
        public void
        DimAligned()
        {
            m_db = Utils.Db.GetCurDwg();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            Point3d pt1, pt2;

                // get end points and angle for extension lines
            if (GetLineOrTwoPoints(out pt1, out pt2) != PromptStatus.OK)
                return;

            PromptPointOptions prTextPtOpts = new PromptPointOptions("\nText point");
            prTextPtOpts.UseBasePoint = true;
            prTextPtOpts.BasePoint = Utils.Ge.Midpoint(pt1, pt2);
            PromptPointResult prTextPtRes = ed.GetPoint(prTextPtOpts);
            if (prTextPtRes.Status != PromptStatus.OK)
                return;

            using (AlignedDimension dim = new AlignedDimension()) {
                dim.XLine1Point = pt1;
                dim.XLine2Point = pt2;

                    // TBD: dimLinePt is supposed automatically set from where
                    // text was placed, unless you deliberately set the dimLinePt (and DIMFIT != 4)
                    // However, it doesn't seem to work unless you set to the same as TextPosition
                    // anyway.
                dim.HorizontalRotation = GetDimHorizRotation();
                dim.UsingDefaultTextPosition = true;    // make text go where user picked
                dim.TextPosition = prTextPtRes.Value;
                dim.DimLinePoint = prTextPtRes.Value;
                
                Utils.Db.TransformToWcs(dim, m_db);
                Utils.SymTbl.AddToCurrentSpaceAndClose(dim, m_db);
            }
        }
        
        public void
        Point()
        {
            m_db = Utils.Db.GetCurDwg();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptPointOptions prPtOpt = new PromptPointOptions("\nPoint");
            PromptPointResult prPtRes = ed.GetPoint(prPtOpt);
            if (prPtRes.Status != PromptStatus.OK)
                return;
                
            using (DBPoint point = new DBPoint()) {
                point.Position = prPtRes.Value;
                Utils.Db.TransformToWcs(point, m_db);
                
                    // TBD: not sure why, but the call to set the EcsRotation must come
                    // after we do the TransformToWcs().  Need to investigate whether that
                    // is a bug or intended behavior.
                point.EcsRotation = GetDimHorizRotation();
                Utils.SymTbl.AddToCurrentSpaceAndClose(point, m_db);
            }
        }
        
        /// <summary>
        /// This will demonstrate how to use the CompBldr to manage alot of the low
        /// level details of creating new entities.  It's not quite ready yet, so this
        /// example will come at a later date (but will probably be the way you want
        /// to create entities in the future).
        ///
        /// TBD: finish this off and provide several examples.  (jma - 04/28/04)
        /// </summary>
        public void
        CompBldrCurSpace()
        {
            m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                
                double tmpRadius = 1.0;
                
                Circle tmpCirc = null;
                for (int i=0; i<10; i++) {
                    tmpCirc = new Circle();
                    tmpCirc.Radius = tmpRadius;
                    tmpCirc.ColorIndex = i;
                    
                    Utils.Db.TransformToWcs(tmpCirc, m_db);
                    compBldr.AddToDb(tmpCirc);
                    
                    tmpRadius += 1.0;
                }
                
                Matrix3d mat = Matrix3d.Displacement(new Vector3d(10.0, 10.0, 0.0));
                compBldr.PushXform(mat);
                
                tmpCirc = new Circle();
                tmpCirc.Radius = 2.0;
                Utils.Db.TransformToWcs(tmpCirc, m_db);
                compBldr.AddToDb(tmpCirc);
                
                compBldr.PopXform();
                
                compBldr.Commit();
            }
        }
        
        public void
        Lineweights()
        {
            m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                compBldr.PushXform(Utils.Db.GetUcsMatrix(m_db));
                
                Point3d startPt = new Point3d(0.0, 0.0, 0.0);
                Point3d endPt   = new Point3d(100.0, 0.0, 0.0);
                
                Vector3d offset = new Vector3d(0.0, 10.0, 0.0);
                
                foreach (Autodesk.AutoCAD.DatabaseServices.LineWeight lw in System.Enum.GetValues(typeof(LineWeight))) {
                    Line line = new Line(startPt, endPt);
                    line.LineWeight = lw;
                
                    compBldr.AddToDb(line);
                    
                    DBText text = new DBText();
                    text.Height = 5.0;
                    text.Position = endPt;
                    text.TextString = lw.ToString();
                    
                    compBldr.AddToDb(text);
                    
                    startPt += offset;
                    endPt += offset;
                }
                   
                compBldr.Commit();
            }
        }

        public void
        RayHardWired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                compBldr.PushXform(Utils.Db.GetUcsMatrix(m_db));

                Point3d basePoint = new Point3d(0.0, 0.0, 0.0);
                Vector3d unitDir = new Vector3d(10.0, 10.0, 0.0);

                Ray ray = new Ray();
                ray.BasePoint = basePoint;
                ray.UnitDir = unitDir;

                compBldr.AddToDb(ray);
                compBldr.Commit();
            }
        }

        public void
        LeaderHardWired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                compBldr.PushXform(Utils.Db.GetUcsMatrix(m_db));

                Point3d startPoint = new Point3d(0.0, 0.0, 0.0);
                Point3d midPoint = new Point3d(50.0, 50.0, 0.0);
                Point3d endPoint = new Point3d(100.0, 50.0, 0.0);

                Point3dCollection pts = new Point3dCollection();
                pts.Add(startPoint);
                pts.Add(midPoint);
                pts.Add(endPoint);
                
                Leader leader = new Leader();
                foreach (Point3d pt in pts) {
                    leader.AppendVertex(pt);
                }
                compBldr.AddToDb(leader);
                compBldr.Commit();
            }
        }

        public void
        EllipseHardWired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                compBldr.PushXform(Utils.Db.GetUcsMatrix(m_db));

                Point3d center = new Point3d(50.0, 50.0, 0.0);
                // laid out on the XY plane, with normal in Z 
                Vector3d uNormal = new Vector3d(0.0, 0.0, 10.0);
                Vector3d majAxis = new Vector3d(10.0, 0.0, 0.0);
                // default values
                double rRatio = .5;
                double startAngle = 0.0;
                double endAngle = 6.28318530717958647692;
                Ellipse ellipse = new Ellipse(center, uNormal, majAxis, rRatio, startAngle, endAngle);
                compBldr.AddToDb(ellipse);
                compBldr.Commit();
            }
        }

        public void
        SplineHardWired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                compBldr.PushXform(Utils.Db.GetUcsMatrix(m_db));

                Point3dCollection pts = new Point3dCollection();
                pts.Add(new Point3d(0.0, 0.0, 0.0));
                pts.Add(new Point3d(50.0, 50.0, 50.0));
                pts.Add(new Point3d(100.0, 50.0, 100.0));
                pts.Add(new Point3d(150.0, 100.0, 100.0));

                // order of the curve can be between 2 and 26
                int order = 6;
                // determines extent of interpolation through all the points 
                double fitTolerance = .5;

                Spline spline = new Spline(pts, order, fitTolerance);
                compBldr.AddToDb(spline);
                compBldr.Commit();
            }
        }
        
        public void
        HelixHardWired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                compBldr.PushXform(Utils.Db.GetUcsMatrix(m_db));

                Helix helix = new Helix();
                helix.Turns = 10.0;
                helix.Twist = true;
                helix.BaseRadius = 20.0;
                //creates the helix geometry based on the values set in prior function calls
                helix.CreateHelix();
                compBldr.AddToDb(helix);
                compBldr.Commit();
            }
        }

        public void
        XlineHardWired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                compBldr.PushXform(Utils.Db.GetUcsMatrix(m_db));

                Xline xline = new Xline();
                xline.UnitDir = new Vector3d(1.0, 1.0, 0.0);
                compBldr.AddToDb(xline);
                compBldr.Commit();
            }
        }

        public void
        Polyline2dHardwired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                compBldr.PushXform(Utils.Db.GetUcsMatrix(m_db));

                Polyline2d pline2d = new Polyline2d();
                pline2d.Color = Color.FromRgb(42, 184, 185);
                // polyline2d needs to be in database before the vertices
                // can be added to it. The polyline2d owns the vertices.
                compBldr.AddToDb(pline2d);

                Point3d[] pts = new Point3d[4];
                pts[0] = new Point3d(0.0, 0.0, 0.0);
                pts[1] = new Point3d(15.0, 0.0, 0.0);
                pts[2] = new Point3d(15.0, 8.0, 0.0);
                pts[3] = new Point3d(0.0, 8.0, 0.0);

                Vertex2d vertX2d;

                for (int i = 0; i < pts.Length; i++) {
                    vertX2d = new Vertex2d(pts[i], 0.0, 0.0, 0.0, 0.0);
                    pline2d.AppendVertex(vertX2d);
                    compBldr.Transaction.AddNewlyCreatedDBObject(vertX2d, true);
                }

                pline2d.Closed = true;
                compBldr.Commit();

            }
        }

        public void
        Polyline3dHardwired()
        {
            m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                compBldr.PushXform(Utils.Db.GetUcsMatrix(m_db));

                Point3d[] pts = new Point3d[4];
                pts[0] = new Point3d(0.0, 0.0, 0.0);
                pts[1] = new Point3d(15.0, 0.0, 15.0);
                pts[2] = new Point3d(15.0, 8.0, 15.0);
                pts[3] = new Point3d(0.0, 8.0, 0.0);
                Point3dCollection ptsColl = new Point3dCollection(pts);

                Polyline3d pline3d = new Polyline3d(Poly3dType.SimplePoly, ptsColl, true);
                pline3d.Color = Color.FromRgb(42, 184, 185);

                compBldr.AddToDb(pline3d);
                compBldr.Commit();
            }
        }

       public void
       TableHardWired ()
        {
           /* m_db = Utils.Db.GetCurDwg();

            using (CompBldrCurSpace compBldr = new CompBldrCurSpace(m_db)) {
                compBldr.Start();
                compBldr.PushXform(Utils.Db.GetUcsMatrix(m_db));

                Table table = new Table();
                table.NumColumns = 5;
                table.NumRows = 5;
                table.Height = 50;
                table.Width = 15;

                for(int row=0; row<table.NumRows; row++) {
                    for (int col = 0; col < table.NumColumns; col++) {
                        string value = string.Format("Row={0},Col={1}", row, col);
                        //table.SetTextString(row, col, value);     // TBD: Fix JMA
                        // centers the text in the middle
                        //table.SetAlignment(row, col, CellAlignment.MiddleCenter); // TBD: Fix JMA
                    }
                }

                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
                PromptPointOptions prOpts = new PromptPointOptions("\nSpecify point to insert");
                prOpts.AllowNone = true;

                PromptPointResult prPt1Res = ed.GetPoint(prOpts);

                if (prPt1Res.Status == PromptStatus.OK) {
                    table.Position = prPt1Res.Value;
                }

                compBldr.AddToDb(table);
                compBldr.Commit();
            }*/     // TBD: Fix JMA
        }

        #endregion

        #region User Input Utils
        /// <summary>
        /// Find out the rotational difference between the current UCS XAxis
        /// and the ECS XAxis.  Dimensions need to know this to know which way
        /// was horizontal when the dimension text was created.
        /// </summary>
        /// <returns>angle of ECS rotation</returns>
        
        public double
        GetDimHorizRotation()
        {
            m_db = Utils.Db.GetCurDwg();

            Matrix3d m = Utils.Db.GetUcsMatrix(m_db);
            
            Vector3d entXAxis = Utils.Db.GetEcsXAxis(m.CoordinateSystem3d.Zaxis);   // get AutoCAD's arbitrary X-Axis

            return m.CoordinateSystem3d.Xaxis.GetAngleTo(entXAxis, m.CoordinateSystem3d.Zaxis);    // get rotation from one to the other
        }
        
        /// <summary>
        /// Helper function for acquiring Dimension parameters from the user.  It will return
        /// two points, either from the user directly picking two points, or from them selecting
        /// a line entity.
        /// </summary>
        /// <param name="pt1">First point</param>
        /// <param name="pt2">Second point</param>
        /// <returns></returns>
        
        public PromptStatus
        GetLineOrTwoPoints(out Point3d pt1, out Point3d pt2)
        {
            m_db = Utils.Db.GetCurDwg();

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            pt1 = pt2 = Utils.Ge.kOrigin;    // initialize to make compiler happy
            
            PromptPointOptions prOpts = new PromptPointOptions("\nFirst extension line origin or RETURN to select");
            prOpts.AllowNone = true;

            PromptPointResult prPt1Res = ed.GetPoint(prOpts);

                // user wants to select a line to specify both points
            if (prPt1Res.Status == PromptStatus.None) {
                PromptEntityOptions prEntOpts = new PromptEntityOptions("\nSelect a LINE");
                prEntOpts.SetRejectMessage("\nSelected entity must be of type Line");
                prEntOpts.AddAllowedClass(typeof(Line), false);

                PromptEntityResult prEntRes = ed.GetEntity(prEntOpts);
                if (prEntRes.Status != PromptStatus.OK)
                    return prEntRes.Status;

                Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = m_db.TransactionManager;

                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    Line line = (Line)tr.GetObject(prEntRes.ObjectId, OpenMode.ForRead);

                    pt1 = Utils.Db.WcsToUcs(line.StartPoint);
                    pt2 = Utils.Db.WcsToUcs(line.EndPoint);
                    tr.Commit();
                    
                    return PromptStatus.OK;
                }
            }
            else if (prPt1Res.Status == PromptStatus.OK) {
                PromptPointOptions prPt2Opts = new PromptPointOptions("\nSecond extension line origin");
                prPt2Opts.UseBasePoint = true;
                prPt2Opts.BasePoint = prPt1Res.Value;
                PromptPointResult prPt2Res = ed.GetPoint(prPt2Opts);
                if (prPt2Res.Status != PromptStatus.OK)
                    return PromptStatus.Cancel;

                pt1 = prPt1Res.Value;
                pt2 = prPt2Res.Value;
                return prPt1Res.Status;
            }
            
            return prPt1Res.Status;
        }
        
        #endregion  // User Input Utils

	}
}
