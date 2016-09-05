
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
using Autodesk.AutoCAD.Geometry;
using MgdDbg.Snoop.Collectors;

namespace MgdDbg.Snoop.CollectorExts
{
	/// <summary>
	/// This is a Snoop Collector Extension object to collect data from AcGe objects.
	/// </summary>
	public class Geometry : CollectorExt
	{
		public
		Geometry()
		{
		}
		
        protected override void
        CollectEvent(object sender, CollectorEventArgs e)
        {
                // cast the sender object to the SnoopCollector we are expecting
            Collector snoopCollector = sender as Collector;
            if (snoopCollector == null) {
                Debug.Assert(false);    // why did someone else send us the message?
                return;
            }
            
                // see if it is a type we are responsible for
            Entity3d ent3d = e.ObjToSnoop as Entity3d;
            if (ent3d != null) {          
                Stream(snoopCollector.Data(), ent3d);
                return;
            }
            
            Entity2d ent2d = e.ObjToSnoop as Entity2d;
            if (ent2d != null) {          
                Stream(snoopCollector.Data(), ent2d);
                return;
            }

            CurveBoundary curveBound = e.ObjToSnoop as CurveBoundary;
            if (curveBound != null) {
                Stream(snoopCollector.Data(), curveBound);
                return;
            }

            Interval interval = e.ObjToSnoop as Interval;
            if (interval != null) {
                Stream(snoopCollector.Data(), interval);
                return;
            }            
                
            if (e.ObjToSnoop is Matrix2d) {
                Stream(snoopCollector.Data(), (Matrix2d)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is Matrix3d) {
                Stream(snoopCollector.Data(), (Matrix3d)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is CoordinateSystem3d) {
                Stream(snoopCollector.Data(), (CoordinateSystem3d)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is Point2dCollection) {
                Stream(snoopCollector.Data(), (Point2dCollection)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is Point3dCollection) {
                Stream(snoopCollector.Data(), (Point3dCollection)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is DoubleCollection) {
                Stream(snoopCollector.Data(), (DoubleCollection)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is IntegerCollection) {
                Stream(snoopCollector.Data(), (IntegerCollection)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is ClipBoundary2dData) {
                Stream(snoopCollector.Data(), (ClipBoundary2dData)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is CompositeParameter) {
                Stream(snoopCollector.Data(), (CompositeParameter)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is CurveBoundaryData) {
                Stream(snoopCollector.Data(), (CurveBoundaryData)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is GeoDataLonLatAltInfo) {
                Stream(snoopCollector.Data(), (GeoDataLonLatAltInfo)e.ObjToSnoop);
                return;
            }            

            if (e.ObjToSnoop is NurbCurve2dData) {
                Stream(snoopCollector.Data(), (NurbCurve2dData)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is NurbCurve2dFitData) {
                Stream(snoopCollector.Data(), (NurbCurve2dFitData)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is NurbCurve3dData) {
                Stream(snoopCollector.Data(), (NurbCurve2dData)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is NurbCurve3dFitData) {
                Stream(snoopCollector.Data(), (NurbCurve2dFitData)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is NurbSurfaceDefinition) {
                Stream(snoopCollector.Data(), (NurbCurve2dFitData)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is PlanarEquationCoefficients) {
                Stream(snoopCollector.Data(), (PlanarEquationCoefficients)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is SurfaceSurfaceIntersectorConfigurations) {
                Stream(snoopCollector.Data(), (SurfaceSurfaceIntersectorConfigurations)e.ObjToSnoop);
                return;
            }
        }
     
        private void
        Stream(ArrayList data, Entity3d ent3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Entity3d)));
            
                // branch to all known major sub-classes
            Curve3d crv = ent3d as Curve3d;
            if (crv != null) {
                Stream(data, crv);
            }
            
            BoundBlock3d bndBlk = ent3d as BoundBlock3d;
            if (bndBlk != null) {
                Stream(data, bndBlk);
            }

            Surface surf = ent3d as Surface;
            if (surf != null) {
                Stream(data, surf);
            }

            PointEntity3d ptEnt3d = ent3d as PointEntity3d;
            if (ptEnt3d != null) {
                Stream(data, ptEnt3d);
            }

            CurveCurveIntersector3d curCurInt3d = ent3d as CurveCurveIntersector3d;
            if (curCurInt3d != null) {
                Stream(data, curCurInt3d);
            }

            SurfaceSurfaceIntersector surSurInt = ent3d as SurfaceSurfaceIntersector;
            if (surSurInt != null) {
                Stream(data, surSurInt);
            }
        }

        #region Entity3d Derived classes

        private void
        Stream(ArrayList data, Curve3d crv)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Curve3d)));

            data.Add(new Snoop.Data.Object("Bound block", crv.BoundBlock));
            data.Add(new Snoop.Data.Object("Ortho bound block", crv.OrthoBoundBlock));

            // branch to all known major sub-classes
            LinearEntity3d linearEnt = crv as LinearEntity3d;
            if (linearEnt != null) {
                Stream(data, linearEnt);
                return;
            }

            CircularArc3d circArc = crv as CircularArc3d;
            if (circArc != null) {
                Stream(data, circArc);
                return;
            }

            SplineEntity3d splineEnt3d = crv as SplineEntity3d;
            if (splineEnt3d != null) {
                Stream(data, splineEnt3d);
                return;
            }

            CompositeCurve3d compCur3d = crv as CompositeCurve3d;
            if (compCur3d != null) {
                Stream(data, compCur3d);
                return;
            }

            OffsetCurve3d offCur3d = crv as OffsetCurve3d;
            if (offCur3d != null) {
                Stream(data, offCur3d);
                return;
            }

            EllipticalArc3d ellArc3d = crv as EllipticalArc3d;
            if (ellArc3d != null) {
                Stream(data, ellArc3d);
                return;
            }

            ExternalCurve3d extCur3d = crv as ExternalCurve3d;
            if (extCur3d != null) {
                Stream(data, extCur3d);
                return;
            }

        }

        #region Curve3d Derived classes
        private void
        Stream(ArrayList data, SplineEntity3d splineEnt3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SplineEntity3d)));

            data.Add(new Snoop.Data.Int("Degree", splineEnt3d.Degree));
            data.Add(new Snoop.Data.Int("Number Of Control Points", splineEnt3d.NumberOfControlPoints));
            data.Add(new Snoop.Data.Int("Number Of Knots", splineEnt3d.NumberOfKnots));
            data.Add(new Snoop.Data.Int("Order", splineEnt3d.Order));
            data.Add(new Snoop.Data.Double("Start Parameter", splineEnt3d.StartParameter));
            data.Add(new Snoop.Data.Double("End Parameter", splineEnt3d.EndParameter));
            data.Add(new Snoop.Data.Point3d("Start Point", splineEnt3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("End Point", splineEnt3d.EndPoint));

            // branch to all known major sub-classes
            PolylineCurve3d plineCur3d = splineEnt3d as PolylineCurve3d;
            if (plineCur3d != null) {
                Stream(data, plineCur3d);
                return;
            }

            CubicSplineCurve3d cubSplineCur3d = splineEnt3d as CubicSplineCurve3d;
            if (cubSplineCur3d != null) {
                Stream(data, cubSplineCur3d);
                return;
            }

            NurbCurve3d nurbCur = splineEnt3d as NurbCurve3d;
            if (nurbCur != null) {
                Stream(data, nurbCur);
                return;
            }
        }

        #region SplineEnt3d Derived classes

        private void
        Stream(ArrayList data, PolylineCurve3d plineCur3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PolylineCurve3d)));

            data.Add(new Snoop.Data.Int("Degree", plineCur3d.Degree));
            data.Add(new Snoop.Data.Int("Number Of Control Points", plineCur3d.NumberOfControlPoints));
            data.Add(new Snoop.Data.Int("Number Of Fit Points", plineCur3d.NumberOfFitPoints));
            data.Add(new Snoop.Data.Int("Number Of Knots", plineCur3d.NumberOfKnots));
            data.Add(new Snoop.Data.Int("Order", plineCur3d.Order));
            data.Add(new Snoop.Data.Double("Start Parameter", plineCur3d.StartParameter));
            data.Add(new Snoop.Data.Double("End Parameter", plineCur3d.EndParameter));
            data.Add(new Snoop.Data.Point3d("Start Point", plineCur3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("End Point", plineCur3d.EndPoint));

            // branch to all known major sub-classes
            AugmentedPolylineCurve3d augPlineCur3d = plineCur3d as AugmentedPolylineCurve3d;
            if (augPlineCur3d != null) {
                Stream(data, augPlineCur3d);
                return;
            }
        }

        private void
       Stream(ArrayList data, AugmentedPolylineCurve3d augPlineCur3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AugmentedPolylineCurve3d)));

            data.Add(new Snoop.Data.Int("Degree", augPlineCur3d.Degree));
            data.Add(new Snoop.Data.Int("Number Of Control Points", augPlineCur3d.NumberOfControlPoints));
            data.Add(new Snoop.Data.Int("Number Of Fit Points", augPlineCur3d.NumberOfFitPoints));
            data.Add(new Snoop.Data.Int("Number Of Knots", augPlineCur3d.NumberOfKnots));
            data.Add(new Snoop.Data.Int("Order", augPlineCur3d.Order));
            data.Add(new Snoop.Data.Double("Start Parameter", augPlineCur3d.StartParameter));
            data.Add(new Snoop.Data.Double("End Parameter", augPlineCur3d.EndParameter));
            data.Add(new Snoop.Data.Point3d("Start Point", augPlineCur3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("End Point", augPlineCur3d.EndPoint));
            data.Add(new Snoop.Data.Double("Approximate Tolerance", augPlineCur3d.ApproximateTolerance));
        }

        private void
        Stream(ArrayList data, CubicSplineCurve3d cubSplineCur3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CubicSplineCurve3d)));

            data.Add(new Snoop.Data.Int("Degree", cubSplineCur3d.Degree));
            data.Add(new Snoop.Data.Int("Number Of Control Points", cubSplineCur3d.NumberOfControlPoints));
            data.Add(new Snoop.Data.Int("Number Of Fit Points", cubSplineCur3d.NumberOfFitPoints));
            data.Add(new Snoop.Data.Int("Number Of Knots", cubSplineCur3d.NumberOfKnots));
            data.Add(new Snoop.Data.Int("Order", cubSplineCur3d.Order));
            data.Add(new Snoop.Data.Double("Start Parameter", cubSplineCur3d.StartParameter));
            data.Add(new Snoop.Data.Double("End Parameter", cubSplineCur3d.EndParameter));
            data.Add(new Snoop.Data.Point3d("Start Point", cubSplineCur3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("End Point", cubSplineCur3d.EndPoint));
        }

        private void
        Stream(ArrayList data, NurbCurve3d nurbCur3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(NurbCurve3d)));

            data.Add(new Snoop.Data.Int("Degree", nurbCur3d.Degree));
            data.Add(new Snoop.Data.Int("Number Of Control Points", nurbCur3d.NumberOfControlPoints));
            data.Add(new Snoop.Data.Int("Number Of Fit Points", nurbCur3d.NumFitPoints));
            data.Add(new Snoop.Data.Int("Number Of Knots", nurbCur3d.NumberOfKnots));
            data.Add(new Snoop.Data.Int("Number Of Weights", nurbCur3d.NumWeights));
            data.Add(new Snoop.Data.Int("Order", nurbCur3d.Order));
            data.Add(new Snoop.Data.Double("Start Parameter", nurbCur3d.StartParameter));
            data.Add(new Snoop.Data.Double("End Parameter", nurbCur3d.EndParameter));
            data.Add(new Snoop.Data.Point3d("Start Point", nurbCur3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("End Point", nurbCur3d.EndPoint));
        }

        #endregion

        private void
        Stream(ArrayList data, LinearEntity3d linearEnt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LinearEntity3d)));

            data.Add(new Snoop.Data.Vector3d("Direction", linearEnt.Direction));
            data.Add(new Snoop.Data.Point3d("Point on line", linearEnt.PointOnLine));
  
                // branch to all known major sub-classes
            Line3d line = linearEnt as Line3d;
            if (line != null) {
                Stream(data, line);
                return;
            }

            LineSegment3d line3d = linearEnt as LineSegment3d;
            if (line3d != null) 
            {
                Stream(data, line3d);
                return;
            }

            Ray3d ray3d = linearEnt as Ray3d;
            if (ray3d != null) {
                Stream(data, ray3d);
                return;
            }
        }

        #region LinearEnt3d Derived Classes
        private void
        Stream(ArrayList data, Line3d line)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Line3d)));
            data.Add(new Snoop.Data.Point3d("Start point", line.StartPoint));
            data.Add(new Snoop.Data.Point3d("End point", line.EndPoint));
        }

        private void
        Stream(ArrayList data, LineSegment3d line3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LineSegment3d)));

            data.Add(new Snoop.Data.Point3d("Start point", line3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("Mid point", line3d.MidPoint));
            data.Add(new Snoop.Data.Point3d("End point", line3d.EndPoint));
            data.Add(new Snoop.Data.Double("Length", line3d.Length));
        }

        private void
       Stream(ArrayList data, Ray3d ray3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Ray3d)));

            data.Add(new Snoop.Data.Point3d("Start point", ray3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("End point", ray3d.EndPoint));
        }
        #endregion

        private void
        Stream(ArrayList data, CircularArc3d circArc)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CircularArc3d)));

            data.Add(new Snoop.Data.Point3d("Center", circArc.Center));
            data.Add(new Snoop.Data.Vector3d("Normal", circArc.Normal));
            data.Add(new Snoop.Data.Distance("Radius", circArc.Radius));
            data.Add(new Snoop.Data.Point3d("Start point", circArc.StartPoint));
            data.Add(new Snoop.Data.Point3d("End point", circArc.EndPoint));
            data.Add(new Snoop.Data.Angle("Start angle", circArc.StartAngle));
            data.Add(new Snoop.Data.Angle("End angle", circArc.EndAngle));
            data.Add(new Snoop.Data.Vector3d("Reference vector", circArc.ReferenceVector));
        }
        
        private void
        Stream(ArrayList data, CompositeCurve3d compCur3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CompositeCurve3d)));

            data.Add(new Snoop.Data.Point3d("Start Point", compCur3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("End Point", compCur3d.EndPoint));
        }

        private void
        Stream(ArrayList data, OffsetCurve3d offCur3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(OffsetCurve3d)));

            data.Add(new Snoop.Data.Point3d("Start Point", offCur3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("End Point", offCur3d.EndPoint));
            data.Add(new Snoop.Data.Vector3d("Normal", offCur3d.Normal));
            data.Add(new Snoop.Data.Double("Offset Distance", offCur3d.OffsetDistance));
        }

        private void
        Stream(ArrayList data, EllipticalArc3d ellArc3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EllipticalArc3d)));

            data.Add(new Snoop.Data.Point3d("Start Point", ellArc3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("End Point", ellArc3d.EndPoint));
            data.Add(new Snoop.Data.Vector3d("Normal", ellArc3d.Normal));
            data.Add(new Snoop.Data.Point3d("Center", ellArc3d.Center));
            data.Add(new Snoop.Data.Double("Start Angle", ellArc3d.StartAngle));
            data.Add(new Snoop.Data.Double("End Angle", ellArc3d.EndAngle));
            data.Add(new Snoop.Data.Vector3d("Major Axis", ellArc3d.MajorAxis));
            data.Add(new Snoop.Data.Vector3d("Minor Axis", ellArc3d.MinorAxis));
            data.Add(new Snoop.Data.Double("Major Radius", ellArc3d.MajorRadius));
            data.Add(new Snoop.Data.Double("Minor Radius", ellArc3d.MinorRadius));
        }

        private void
       Stream(ArrayList data, ExternalCurve3d exCur3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ExternalCurve3d)));

            data.Add(new Snoop.Data.Point3d("Start Point", exCur3d.StartPoint));
            data.Add(new Snoop.Data.Point3d("End Point", exCur3d.EndPoint));
            data.Add(new Snoop.Data.Bool("Is Circular Arc", exCur3d.IsCircularArc));
            data.Add(new Snoop.Data.Bool("Is Defined", exCur3d.IsDefined));
            data.Add(new Snoop.Data.Bool("Is EllipticalArc", exCur3d.IsEllipticalArc));
            data.Add(new Snoop.Data.Bool("Is Line", exCur3d.IsLine));
            data.Add(new Snoop.Data.Bool("Is LineSegment", exCur3d.IsLineSegment));
            data.Add(new Snoop.Data.Bool("Is Native Curve", exCur3d.IsNativeCurve));
            data.Add(new Snoop.Data.Bool("Is Nurb Curve", exCur3d.IsNurbCurve));
            data.Add(new Snoop.Data.Bool("Is Ray", exCur3d.IsRay));
        }
        #endregion
        
        private void
        Stream(ArrayList data, BoundBlock3d bndBlk)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BoundBlock3d)));

            data.Add(new Snoop.Data.Point3d("Base point", bndBlk.BasePoint));
            data.Add(new Snoop.Data.Vector3d("Direction 1", bndBlk.Direction1));
            data.Add(new Snoop.Data.Vector3d("Direction 2", bndBlk.Direction2));
            data.Add(new Snoop.Data.Vector3d("Direction 3", bndBlk.Direction3));
            data.Add(new Snoop.Data.Bool("Is box", bndBlk.IsBox));
        }

        private void
        Stream(ArrayList data, Surface surf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Surface)));

            data.Add(new Snoop.Data.Bool("Is normal reversed", surf.IsNormalReversed));
            data.Add(new Snoop.Data.Object("Reverse normal", surf.ReverseNormal));


            // branch to all known major sub-classes
            PlanarEntity planEnt = surf as PlanarEntity;
            if (planEnt != null) {
                Stream(data, planEnt);
            }

            Cone cone = surf as Cone;
            if (cone != null) {
                Stream(data, cone);
            }

            Cylinder cyl = surf as Cylinder;
            if (cyl != null) {
                Stream(data, cyl);
            }

            ExternalSurface extSurf = surf as ExternalSurface;
            if (extSurf != null) {
                Stream(data, extSurf);
            }

            ExternalBoundedSurface extBoundSurf = surf as ExternalBoundedSurface;
            if (extBoundSurf != null) {
                Stream(data, extBoundSurf);
            }

            NurbSurface nurbSurf = surf as NurbSurface;
            if (nurbSurf != null) {
                Stream(data, nurbSurf);
            }

            OffsetSurface offSurf = surf as OffsetSurface;
            if (offSurf != null) {
                Stream(data, offSurf);
            }

            Sphere sphere = surf as Sphere;
            if (sphere != null) {
                Stream(data, sphere);
            }

            Torus torus = surf as Torus;
            if (torus != null) {
                Stream(data, torus);
            }
        }

        #region Surface Derived Classes
        private void
        Stream(ArrayList data, PlanarEntity planEnt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlanarEntity)));

            data.Add(new Snoop.Data.Point3d("Point On Plane", planEnt.PointOnPlane));
            data.Add(new Snoop.Data.Vector3d("Normal", planEnt.Normal));
            data.Add(new Snoop.Data.Bool("Is Normal Reversed", planEnt.IsNormalReversed));

            BoundedPlane boundPlane = planEnt as BoundedPlane;
            if (boundPlane != null) {
                Stream(data, boundPlane);
            }
        }

        private void
         Stream(ArrayList data, BoundedPlane boundPlane)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BoundedPlane)));

            data.Add(new Snoop.Data.Point3d("Point On Plane", boundPlane.PointOnPlane));
            data.Add(new Snoop.Data.Vector3d("Normal", boundPlane.Normal));
            data.Add(new Snoop.Data.Bool("Is Normal Reversed", boundPlane.IsNormalReversed));
        }

        private void
        Stream(ArrayList data, Cone cone)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Cone)));

            data.Add(new Snoop.Data.Point3d("Apex", cone.Apex));
            data.Add(new Snoop.Data.Vector3d("Axis Of Symmetry", cone.AxisOfSymmetry));
            data.Add(new Snoop.Data.Bool("Is Normal Reversed", cone.IsNormalReversed));
            data.Add(new Snoop.Data.Point3d("Base Center", cone.BaseCenter));
            data.Add(new Snoop.Data.Double("Base Radius", cone.BaseRadius));
            data.Add(new Snoop.Data.Double("Half Angle", cone.HalfAngle));
            data.Add(new Snoop.Data.Vector3d("Reference Axis", cone.ReferenceAxis));
        }

        private void
        Stream(ArrayList data, Cylinder cyl)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Cylinder)));

            data.Add(new Snoop.Data.Vector3d("Axis Of Symmetry", cyl.AxisOfSymmetry));
            data.Add(new Snoop.Data.Bool("Is Normal Reversed", cyl.IsNormalReversed));
            data.Add(new Snoop.Data.Vector3d("Reference Axis", cyl.ReferenceAxis));
            data.Add(new Snoop.Data.Point3d("Origin", cyl.Origin));
            data.Add(new Snoop.Data.Double("Radius", cyl.Radius));
        }

        private void
       Stream(ArrayList data, ExternalSurface extSurf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ExternalSurface)));

            data.Add(new Snoop.Data.Bool("Is Cone", extSurf.IsCone));
            data.Add(new Snoop.Data.Bool("Is Cylinder", extSurf.IsCylinder));
            data.Add(new Snoop.Data.Bool("Is Native Surface", extSurf.IsNativeSurface));
            data.Add(new Snoop.Data.Bool("Is Normal Reversed", extSurf.IsNormalReversed));
            data.Add(new Snoop.Data.Bool("Is Nurb Surface", extSurf.IsNurbSurface));
            data.Add(new Snoop.Data.Bool("Is Owner Of Surface", extSurf.IsOwnerOfSurface));
            data.Add(new Snoop.Data.Bool("Is Plane", extSurf.IsPlane));
            data.Add(new Snoop.Data.Bool("Is Sphere", extSurf.IsSphere));
            data.Add(new Snoop.Data.Bool("Is Torus", extSurf.IsTorus));
        }

        private void
        Stream(ArrayList data, ExternalBoundedSurface extBoundSurf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ExternalBoundedSurface)));

            data.Add(new Snoop.Data.Bool("Is Cone", extBoundSurf.IsCone));
            data.Add(new Snoop.Data.Bool("Is Cylinder", extBoundSurf.IsCylinder));
            data.Add(new Snoop.Data.Bool("Is External Surface", extBoundSurf.IsExternalSurface));
            data.Add(new Snoop.Data.Bool("Is Normal Reversed", extBoundSurf.IsNormalReversed));
            data.Add(new Snoop.Data.Bool("Is Nurbs", extBoundSurf.IsNurbs));
            data.Add(new Snoop.Data.Bool("Is Owner Of Surface", extBoundSurf.IsOwnerOfSurface));
            data.Add(new Snoop.Data.Bool("Is Plane", extBoundSurf.IsPlane));
            data.Add(new Snoop.Data.Bool("Is Sphere", extBoundSurf.IsSphere));
            data.Add(new Snoop.Data.Bool("Is Torus", extBoundSurf.IsTorus));
            data.Add(new Snoop.Data.Int("Number of Contours", extBoundSurf.NumContours));
        }

        private void
       Stream(ArrayList data, NurbSurface nurbSurf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(NurbSurface)));

            data.Add(new Snoop.Data.Int("Degree In U", nurbSurf.DegreeInU));
            data.Add(new Snoop.Data.Int("Degree In V", nurbSurf.DegreeInV));
            data.Add(new Snoop.Data.Bool("Is Normal Reversed", nurbSurf.IsNormalReversed));
            data.Add(new Snoop.Data.Bool("Is Periodic In U", nurbSurf.IsPeriodicInU));
            data.Add(new Snoop.Data.Bool("Is Periodic In V", nurbSurf.IsPeriodicInV));
            data.Add(new Snoop.Data.Bool("Is Rational In U", nurbSurf.IsRationalInU));
            data.Add(new Snoop.Data.Bool("Is Rational In V", nurbSurf.IsRationalInV));
            data.Add(new Snoop.Data.Int("Number of Control Points In U", nurbSurf.NumControlPointsInU));
            data.Add(new Snoop.Data.Int("Number of Control Points In V", nurbSurf.NumControlPointsInV));
            data.Add(new Snoop.Data.Int("Number of Knots In U", nurbSurf.NumKnotsInU));
            data.Add(new Snoop.Data.Int("Number of Knots In V", nurbSurf.NumKnotsInV));
            data.Add(new Snoop.Data.Double("Periodic In U", nurbSurf.PeriodicInU));
            data.Add(new Snoop.Data.Double("Periodic In V", nurbSurf.PeriodicInV));
            data.Add(new Snoop.Data.Int("Singularity In U", nurbSurf.SingularityInU));
            data.Add(new Snoop.Data.Int("Singularity In V", nurbSurf.SingularityInV));
        }

        private void
        Stream(ArrayList data, OffsetSurface offSurf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(OffsetSurface)));

            data.Add(new Snoop.Data.Bool("Is Bounded Plane", offSurf.IsBoundedPlane));
            data.Add(new Snoop.Data.Bool("Is Cone", offSurf.IsCone));
            data.Add(new Snoop.Data.Bool("Is Cylinder", offSurf.IsCylinder));
            data.Add(new Snoop.Data.Bool("Is Normal Reversed", offSurf.IsNormalReversed));
            data.Add(new Snoop.Data.Bool("Is Plane", offSurf.IsPlane));
            data.Add(new Snoop.Data.Bool("Is Sphere", offSurf.IsSphere));
            data.Add(new Snoop.Data.Bool("Is Torus", offSurf.IsTorus));
            data.Add(new Snoop.Data.Double("Offset Dist", offSurf.OffsetDist));
        }

        private void
        Stream(ArrayList data, Sphere sphere)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Sphere)));

            data.Add(new Snoop.Data.Point3d("Center", sphere.Center));
            data.Add(new Snoop.Data.Bool("Is Outer Normal", sphere.IsOuterNormal));
            data.Add(new Snoop.Data.Vector3d("Reference Axis", sphere.ReferenceAxis));
            data.Add(new Snoop.Data.Vector3d("North Axis", sphere.NorthAxis));
            data.Add(new Snoop.Data.Point3d("North Pole", sphere.NorthPole));
            data.Add(new Snoop.Data.Point3d("South Pole", sphere.SouthPole));
            data.Add(new Snoop.Data.Double("Radius", sphere.Radius));
        }

        private void
       Stream(ArrayList data, Torus torus)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Torus)));

            data.Add(new Snoop.Data.Vector3d("Axis Of Symmetry", torus.AxisOfSymmetry));
            data.Add(new Snoop.Data.Point3d("Center", torus.Center));
            data.Add(new Snoop.Data.Bool("Is Apple", torus.IsApple));
            data.Add(new Snoop.Data.Bool("Is Degenerate", torus.IsDegenerate));
            data.Add(new Snoop.Data.Bool("Is Doughnut", torus.IsDoughnut));
            data.Add(new Snoop.Data.Bool("Is Hollow", torus.IsHollow));
            data.Add(new Snoop.Data.Bool("Is Lemon", torus.IsLemon));
            data.Add(new Snoop.Data.Bool("Is Normal Reversed", torus.IsNormalReversed));
            data.Add(new Snoop.Data.Bool("Is Outer Normal", torus.IsOuterNormal));
            data.Add(new Snoop.Data.Bool("Is Vortex", torus.IsVortex));
            data.Add(new Snoop.Data.Double("Major Radius", torus.MajorRadius));
            data.Add(new Snoop.Data.Double("Minor Radius", torus.MinorRadius));
            data.Add(new Snoop.Data.Vector3d("Minor Radius", torus.ReferenceAxis));
        }

        #endregion

        private void
        Stream(ArrayList data, PointEntity3d ptEnt3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PointEntity3d)));

            // branch to all known major sub-classes
            PointOnCurve3d ptOnCur3d = ptEnt3d as PointOnCurve3d;
            if (ptOnCur3d != null) {
                Stream(data, ptOnCur3d);
            }

            Position3d postn3d = ptEnt3d as Position3d;
            if (postn3d != null) {
                Stream(data, postn3d);
            }

            PointOnSurface ptOnSurf = ptEnt3d as PointOnSurface;
            if (ptOnSurf != null) {
                Stream(data, ptOnSurf);
            }
        }

        #region PointEntity3d Derived Classes

        private void
        Stream(ArrayList data, PointOnCurve3d ptOnCur3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PointOnCurve3d)));

            data.Add(new Snoop.Data.Double("Parameter", ptOnCur3d.Parameter));
            data.Add(new Snoop.Data.Point3d("Point", ptOnCur3d.Point));
        }

        private void
        Stream(ArrayList data, Position3d postn3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Position3d)));

            // no data here!! 02.28.06
        }

        private void
        Stream(ArrayList data, PointOnSurface ptOnSurf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PointOnSurface)));

            data.Add(new Snoop.Data.Point2d("Parameter", ptOnSurf.Parameter));
        }

        #endregion

        private void
         Stream(ArrayList data, CurveCurveIntersector3d curCurInt3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CurveCurveIntersector3d)));

            data.Add(new Snoop.Data.Object("Curve1", curCurInt3d.Curve1));
            data.Add(new Snoop.Data.Object("Curve2", curCurInt3d.Curve2));
            data.Add(new Snoop.Data.Int("Number Of Intersection Points", curCurInt3d.NumberOfIntersectionPoints));
            data.Add(new Snoop.Data.Vector3d("Plane Normal", curCurInt3d.PlaneNormal));
            data.Add(new Snoop.Data.Object("Tolerance", curCurInt3d.Tolerance));
        }

        private void
         Stream(ArrayList data, SurfaceSurfaceIntersector surfSurfInt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SurfaceSurfaceIntersector)));

            data.Add(new Snoop.Data.Int("NumResults", surfSurfInt.NumResults));
            data.Add(new Snoop.Data.Object("Surface1", surfSurfInt.Surface1));
            data.Add(new Snoop.Data.Object("Surface2", surfSurfInt.Surface2));
            data.Add(new Snoop.Data.Object("Tolerance", surfSurfInt.Tolerance));
        }

        #endregion

        #region Entity2d Derived classes

        private void
        Stream(ArrayList data, Entity2d ent2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Entity2d)));
            
                // branch to all known major sub-classes
            Curve2d crv = ent2d as Curve2d;
            if (crv != null) {
                Stream(data, crv);
            }
            
            BoundBlock2d bndBlk = ent2d as BoundBlock2d;
            if (bndBlk != null) {
                Stream(data, bndBlk);
            }

            ClipBoundary2d clipBound2d = ent2d as ClipBoundary2d;
            if (clipBound2d != null) {
                Stream(data, clipBound2d);
            }

            CurveCurveIntersector2d curcurInt2d = ent2d as CurveCurveIntersector2d;
            if (curcurInt2d != null) {
                Stream(data, curcurInt2d);
            }

            PointEntity2d ptEnt2d = ent2d as PointEntity2d;
            if (ptEnt2d != null) {
                Stream(data, ptEnt2d);
            }
        }
        
        private void
        Stream(ArrayList data, Curve2d crv)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Curve2d)));

            data.Add(new Snoop.Data.Object("Bound block", crv.BoundBlock));
            data.Add(new Snoop.Data.Object("Ortho bound block", crv.OrthoBoundBlock));
            data.Add(new Snoop.Data.Bool("Has start point", crv.HasStartPoint));
            if (crv.HasStartPoint)
                data.Add(new Snoop.Data.Point2d("Start point", crv.StartPoint));
            data.Add(new Snoop.Data.Bool("Has end point", crv.HasEndPoint));
            if (crv.HasEndPoint)
                data.Add(new Snoop.Data.Point2d("End point", crv.EndPoint));
  
                // branch to all known major sub-classes
            LinearEntity2d linearEnt = crv as LinearEntity2d;
            if (linearEnt != null) {
                Stream(data, linearEnt);
                return;
            }
            
            CircularArc2d circArc = crv as CircularArc2d;
            if (circArc != null) {
                Stream(data, circArc);
                return;
            }

            CompositeCurve2d compCurve2d = crv as CompositeCurve2d;
            if (compCurve2d != null) {
                Stream(data, compCurve2d);
                return;
            }

            SplineEntity2d splineEnt2d = crv as SplineEntity2d;
            if (splineEnt2d != null) {
                Stream(data, splineEnt2d);
                return;
            }

            EllipticalArc2d ellipticalArc2d = crv as EllipticalArc2d;
            if (ellipticalArc2d != null) {
                Stream(data, ellipticalArc2d);
                return;
            }

            ExternalCurve2d extCur2d = crv as ExternalCurve2d;
            if (extCur2d != null) {
                Stream(data, extCur2d);
                return;
            }

            OffsetCurve2d offsetCurve = crv as OffsetCurve2d;
            if (offsetCurve != null) {
                Stream(data, offsetCurve);
                return;
            }
        }
        
        private void
        Stream(ArrayList data, LinearEntity2d linearEnt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LinearEntity2d)));

            data.Add(new Snoop.Data.Vector2d("Direction", linearEnt.Direction));
            data.Add(new Snoop.Data.Point2d("Point on line", linearEnt.PointOnLine));
  
                // branch to all known major sub-classes
            Line2d line = linearEnt as Line2d;
            if (line != null) {
                Stream(data, line);
                return;
            }

            LineSegment2d lineSeg = linearEnt as LineSegment2d;
            if (lineSeg != null) {
                Stream(data, lineSeg);
                return;
            }

            Ray2d ray2d = linearEnt as Ray2d;
            if (ray2d != null) {
                Stream(data, ray2d);
                return;
            }
        }
        
        private void
        Stream(ArrayList data, Line2d line)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Line2d)));

                // no data at this level
        }

        private void
        Stream(ArrayList data, LineSegment2d lineSeg)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LineSegment2d)));

            data.Add(new Snoop.Data.Point2d("End point", lineSeg.EndPoint));
            data.Add(new Snoop.Data.Double("Length", lineSeg.Length));
            data.Add(new Snoop.Data.Point2d("Mid point", lineSeg.MidPoint));
            data.Add(new Snoop.Data.Point2d("Start point", lineSeg.StartPoint));            
        }

        private void
        Stream(ArrayList data, Ray2d ray2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Ray2d)));

            data.Add(new Snoop.Data.Point2d("Start point", ray2d.StartPoint));
            data.Add(new Snoop.Data.Point2d("End point", ray2d.EndPoint));       
        }
        
        private void
        Stream(ArrayList data, CircularArc2d circArc)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CircularArc2d)));

            data.Add(new Snoop.Data.Point2d("Center", circArc.Center));
            data.Add(new Snoop.Data.Distance("Radius", circArc.Radius));
            data.Add(new Snoop.Data.Point2d("Start point", circArc.StartPoint));
            data.Add(new Snoop.Data.Point2d("End point", circArc.EndPoint));
            data.Add(new Snoop.Data.Angle("Start angle", circArc.StartAngle));
            data.Add(new Snoop.Data.Angle("End angle", circArc.EndAngle));
            data.Add(new Snoop.Data.Vector2d("Reference vector", circArc.ReferenceVector));
            data.Add(new Snoop.Data.Bool("Is clockwise", circArc.IsClockWise));
        }

        private void
        Stream(ArrayList data, CompositeCurve2d compCurve2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CompositeCurve2d)));

            data.Add(new Snoop.Data.Point2d("Start point", compCurve2d.StartPoint));
            data.Add(new Snoop.Data.Point2d("End point", compCurve2d.EndPoint));            
        }

        private void
        Stream(ArrayList data, SplineEntity2d splineEnt2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SplineEntity2d)));

            data.Add(new Snoop.Data.Int("Degree", splineEnt2d.Degree));
            data.Add(new Snoop.Data.Double("End parameter", splineEnt2d.EndParameter));
            data.Add(new Snoop.Data.Point2d("End point", splineEnt2d.EndPoint));
            data.Add(new Snoop.Data.Bool("Has fit data", splineEnt2d.HasFitData));
            data.Add(new Snoop.Data.Bool("Is rational", splineEnt2d.IsRational));
            data.Add(new Snoop.Data.Enumerable("Knots", splineEnt2d.Knots));
            data.Add(new Snoop.Data.Int("Num control points", splineEnt2d.NumControlPoints));
            data.Add(new Snoop.Data.Int("Num knots", splineEnt2d.NumKnots));
            data.Add(new Snoop.Data.Int("Order", splineEnt2d.Order));
            data.Add(new Snoop.Data.Double("Start parameter", splineEnt2d.StartParameter));
            data.Add(new Snoop.Data.Point2d("Start point", splineEnt2d.StartPoint));


            // branch to all known major sub-classes
            CubicSplineCurve2d cubicSpline = splineEnt2d as CubicSplineCurve2d;
            if (cubicSpline != null) {
                Stream(data, cubicSpline);
                return;
            }

            NurbCurve2d nurbCur2d = splineEnt2d as NurbCurve2d;
            if (nurbCur2d != null) {
                Stream(data, nurbCur2d);
                return;
            }

            PolylineCurve2d polyCur2d = splineEnt2d as PolylineCurve2d;
            if (polyCur2d != null) {
                Stream(data, polyCur2d);
                return;
            }
        }

        private void
        Stream(ArrayList data, CubicSplineCurve2d cubicSpline)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CubicSplineCurve2d)));

            data.Add(new Snoop.Data.Int("Num fit points", cubicSpline.NumFitPoints));            
        }

        private void
        Stream(ArrayList data, NurbCurve2d nurbCur2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(NurbCurve2d)));

            data.Add(new Snoop.Data.Object("Definition data", nurbCur2d.DefinitionData));
            data.Add(new Snoop.Data.Bool("Eval mode", nurbCur2d.EvalMode));
            data.Add(new Snoop.Data.Object("Fit data", nurbCur2d.FitData));
            data.Add(new Snoop.Data.Object("Tolerance", nurbCur2d.FitTolerance));
            data.Add(new Snoop.Data.Int("Num of fit points", nurbCur2d.NumFitPoints));
            data.Add(new Snoop.Data.Int("Num of weights", nurbCur2d.NumWeights));
        }

        private void
        Stream(ArrayList data, PolylineCurve2d polyCur2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PolylineCurve2d)));

            data.Add(new Snoop.Data.Int("Number of fit points", polyCur2d.NumberOfFitPoints));          
        }
        
        private void
        Stream(ArrayList data, EllipticalArc2d ellipticalArc2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EllipticalArc2d)));

            data.Add(new Snoop.Data.Point2d("Center", ellipticalArc2d.Center));
            data.Add(new Snoop.Data.Double("End angle", ellipticalArc2d.EndAngle));
            data.Add(new Snoop.Data.Point2d("End point", ellipticalArc2d.EndPoint));
            data.Add(new Snoop.Data.Bool("Is clockwise", ellipticalArc2d.IsClockWise));
            data.Add(new Snoop.Data.Vector2d("Major axis", ellipticalArc2d.MajorAxis));
            data.Add(new Snoop.Data.Double("Major radius", ellipticalArc2d.MajorRadius));
            data.Add(new Snoop.Data.Vector2d("Minor axis", ellipticalArc2d.MinorAxis));
            data.Add(new Snoop.Data.Double("Minor radius", ellipticalArc2d.MinorRadius));
            data.Add(new Snoop.Data.Double("Start angle", ellipticalArc2d.StartAngle));
            data.Add(new Snoop.Data.Point2d("Start point", ellipticalArc2d.StartPoint));
        }
        
        private void
        Stream(ArrayList data, ExternalCurve2d extCur2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ExternalCurve2d)));

            data.Add(new Snoop.Data.String("External curve kind", extCur2d.ExternalCurveKind.ToString()));
            data.Add(new Snoop.Data.Bool("Is defined", extCur2d.IsDefined));
            data.Add(new Snoop.Data.Bool("Is nurb curve", extCur2d.IsNurbCurve));
            data.Add(new Snoop.Data.Bool("Is owner of curve", extCur2d.IsOwnerOfCurve));
            data.Add(new Snoop.Data.Object("Nurb curve", extCur2d.NurbCurve));
        }

        private void
        Stream(ArrayList data, OffsetCurve2d offsetCur2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(OffsetCurve2d)));

            data.Add(new Snoop.Data.Object("Curve", offsetCur2d.Curve));
            data.Add(new Snoop.Data.Double("Offset distance", offsetCur2d.OffsetDistance));
            data.Add(new Snoop.Data.Bool("Parameter direction", offsetCur2d.ParameterDirection));
            data.Add(new Snoop.Data.Object("Transformation", offsetCur2d.Transformation));          
        }
        
        private void
        Stream(ArrayList data, BoundBlock2d bndBlk)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BoundBlock2d)));

            data.Add(new Snoop.Data.Point2d("Base point", bndBlk.BasePoint));
            data.Add(new Snoop.Data.Vector2d("Direction 1", bndBlk.Direction1));
            data.Add(new Snoop.Data.Vector2d("Direction 2", bndBlk.Direction2));
            data.Add(new Snoop.Data.Bool("Is box", bndBlk.IsBox));
        }

        private void
        Stream(ArrayList data, ClipBoundary2d clipBound2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ClipBoundary2d)));

            // no data at this level          
        }

        private void
        Stream(ArrayList data, CurveCurveIntersector2d curcurInt2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CurveCurveIntersector2d)));

            data.Add(new Snoop.Data.Object("Curve1", curcurInt2d.Curve1));
            data.Add(new Snoop.Data.Object("Curve2", curcurInt2d.Curve2));
            data.Add(new Snoop.Data.Int("Number of intersection points", curcurInt2d.NumberOfIntersectionPoints));
            data.Add(new Snoop.Data.Int("Overlap count", curcurInt2d.OverlapCount));
            data.Add(new Snoop.Data.Bool("Overlap direction", curcurInt2d.OverlapDirection));
            data.Add(new Snoop.Data.Object("Tolerance", curcurInt2d.Tolerance));            
        }

        private void
        Stream(ArrayList data, PointEntity2d ptEnt2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PointEntity2d)));

            data.Add(new Snoop.Data.Point2d("Point", ptEnt2d.Point));

            PointOnCurve2d ptOnCur2d = ptEnt2d as PointOnCurve2d;
            if (ptOnCur2d != null) {
                Stream(data, ptOnCur2d);
                return;
            }

            Position2d pos2d = ptEnt2d as Position2d;
            if (pos2d != null) {
                Stream(data, pos2d);
                return;
            }
        }

        private void
        Stream(ArrayList data, PointOnCurve2d ptOnCur2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PointOnCurve2d)));

            data.Add(new Snoop.Data.Object("Curve", ptOnCur2d.Curve));
            data.Add(new Snoop.Data.Double("Parameter", ptOnCur2d.Parameter));
            data.Add(new Snoop.Data.Point2d("Point", ptOnCur2d.Point));           
        }

        private void
        Stream(ArrayList data, Position2d pos2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Position2d)));

            // no data here!!
        }        
        #endregion

        private void
        Stream(ArrayList data, CurveBoundary curveBound)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CurveBoundary)));

            data.Add(new Snoop.Data.Object("Contour", curveBound.Contour));
            data.Add(new Snoop.Data.Object("Degenerate curve", curveBound.DegenerateCurve));
            data.Add(new Snoop.Data.Object("Degenerate position", curveBound.DegeneratePosition));
            data.Add(new Snoop.Data.Bool("Is degenerate", curveBound.IsDegenerate));
            data.Add(new Snoop.Data.Bool("Is owner of curves", curveBound.IsOwnerOfCurves));
            data.Add(new Snoop.Data.Int("Num of elements", curveBound.NumElements));
        }

        private void
        Stream(ArrayList data, Interval interval)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Interval)));

            data.Add(new Snoop.Data.Double("Element", interval.Element));
            data.Add(new Snoop.Data.Bool("Is bounded", interval.IsBounded));
            data.Add(new Snoop.Data.Bool("Is bounded above", interval.IsBoundedAbove));
            data.Add(new Snoop.Data.Bool("Is bounded below", interval.IsBoundedBelow));
            data.Add(new Snoop.Data.Bool("Is singleton", interval.IsSingleton));
            data.Add(new Snoop.Data.Bool("Is unbounded", interval.IsUnbounded));
            data.Add(new Snoop.Data.Double("Lenght", interval.Length));
            data.Add(new Snoop.Data.Double("Lower bound", interval.LowerBound));
            data.Add(new Snoop.Data.Double("Tolerance", interval.Tolerance));
            data.Add(new Snoop.Data.Double("Upper bound", interval.UpperBound));            
        }
        
        private void
        Stream(ArrayList data, Matrix2d mat2d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Matrix2d)));
                        
            data.Add(new Snoop.Data.Object("Coordinate system", mat2d.CoordinateSystem2d));
            data.Add(new Snoop.Data.Vector2d("Translation", mat2d.Translation));
            data.Add(new Snoop.Data.Bool("Is scaled ortho", mat2d.IsScaledOrtho()));
            data.Add(new Snoop.Data.Bool("Is uni-scaled ortho", mat2d.IsUniscaledOrtho()));
            data.Add(new Snoop.Data.Bool("Is singular", mat2d.IsSingular()));
        }

        
        private void
        Stream(ArrayList data, Matrix3d mat3d)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Matrix3d)));
                        
            data.Add(new Snoop.Data.Object("Coordinate system", mat3d.CoordinateSystem3d));
            data.Add(new Snoop.Data.Object("Inverse", mat3d.Inverse()));
            data.Add(new Snoop.Data.Object("Transpose", mat3d.Transpose()));
            data.Add(new Snoop.Data.Double("Scale", mat3d.GetScale()));
            data.Add(new Snoop.Data.Double("Determinant", mat3d.GetDeterminant()));
            data.Add(new Snoop.Data.Double("Normal", mat3d.Normal));
            data.Add(new Snoop.Data.Vector3d("Translation", mat3d.Translation));
            data.Add(new Snoop.Data.Bool("Is scaled ortho", mat3d.IsScaledOrtho()));
            data.Add(new Snoop.Data.Bool("Is uni-scaled ortho", mat3d.IsUniscaledOrtho()));
            data.Add(new Snoop.Data.Bool("Is singular", mat3d.IsSingular()));
        }

        private void
        Stream(ArrayList data, CoordinateSystem3d coordSys)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CoordinateSystem3d)));
                        
            data.Add(new Snoop.Data.Point3d("Origin", coordSys.Origin));
            data.Add(new Snoop.Data.Vector3d("X-Axis", coordSys.Xaxis));
            data.Add(new Snoop.Data.Vector3d("Y-Axis", coordSys.Yaxis));
            data.Add(new Snoop.Data.Vector3d("Z-Axis", coordSys.Zaxis));      
        }

        private void
        Stream(ArrayList data, Point2dCollection pts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Point2dCollection)));
            
            for (int i=0; i<pts.Count; i++)           
                data.Add(new Snoop.Data.Point2d(string.Format("Point {0}", i), pts[i]));
        }

        private void
        Stream(ArrayList data, Point3dCollection pts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Point3dCollection)));
            
            for (int i=0; i<pts.Count; i++)           
                data.Add(new Snoop.Data.Point3d(string.Format("Point {0}", i), pts[i]));
        }

        private void
        Stream(ArrayList data, DoubleCollection vals)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DoubleCollection)));
            
            for (int i=0; i<vals.Count; i++)           
                data.Add(new Snoop.Data.Double(string.Format("Value {0}", i), vals[i]));
        }

        private void
        Stream(ArrayList data, IntegerCollection vals)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(IntegerCollection)));
            
            for (int i=0; i<vals.Count; i++)           
                data.Add(new Snoop.Data.Int(string.Format("Value {0}", i), vals[i]));
        }

        private void
        Stream(ArrayList data, ClipBoundary2dData clipBound2dData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ClipBoundary2dData)));

            data.Add(new Snoop.Data.String("Clip condition", clipBound2dData.ClipCondition.ToString()));
            data.Add(new Snoop.Data.Enumerable("Clipped segment source label", clipBound2dData.ClippedSegmentSourceLabel));
            data.Add(new Snoop.Data.Enumerable("Clipped vertices", clipBound2dData.ClippedVertices));  
        }

        private void
        Stream(ArrayList data, CompositeParameter compParam)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CompositeParameter)));

            data.Add(new Snoop.Data.Double("Local parameter", compParam.LocalParameter));
            data.Add(new Snoop.Data.Int("Segment index", compParam.SegmentIndex));
        }

        private void
        Stream(ArrayList data, CurveBoundaryData curveBoundData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CurveBoundaryData)));

            data.Add(new Snoop.Data.Enumerable("Curve2d's", curveBoundData.GetCurve2ds()));
            data.Add(new Snoop.Data.Enumerable("Curve3d's", curveBoundData.GetCurve3ds()));
            data.Add(new Snoop.Data.Bool("Orientation2d", curveBoundData.Orientation2d));
            data.Add(new Snoop.Data.Bool("Orientation3d", curveBoundData.Orientation3d));
        }

        private void
        Stream(ArrayList data, GeoDataLonLatAltInfo geoDataLonLat)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(GeoDataLonLatAltInfo)));

            data.Add(new Snoop.Data.Double("Altitude", geoDataLonLat.Altitude));
            data.Add(new Snoop.Data.Double("Latitude", geoDataLonLat.Latitude));
            data.Add(new Snoop.Data.Double("Longitude", geoDataLonLat.Longitude));            
        }

        private void
        Stream(ArrayList data, NurbCurve2dData nurbCur2dData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(NurbCurve2dData)));

            data.Add(new Snoop.Data.Int("Degree", nurbCur2dData.Degree));
            data.Add(new Snoop.Data.Bool("Rational", nurbCur2dData.Rational));
            data.Add(new Snoop.Data.Bool("Periodic", nurbCur2dData.Periodic));
            data.Add(new Snoop.Data.Enumerable("Knots", nurbCur2dData.Knots));
            data.Add(new Snoop.Data.Enumerable("Weights", nurbCur2dData.Weights));
        }

        private void
        Stream(ArrayList data, NurbCurve2dFitData nurbCur2dFitData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(NurbCurve2dFitData)));

            data.Add(new Snoop.Data.Int("Degree", nurbCur2dFitData.Degree));
            data.Add(new Snoop.Data.Vector2d("End tangent", nurbCur2dFitData.EndTangent));
            data.Add(new Snoop.Data.Enumerable("Fit points", nurbCur2dFitData.FitPoints));
            data.Add(new Snoop.Data.Object("Fit tolerance", nurbCur2dFitData.FitTolerance));
            data.Add(new Snoop.Data.Vector2d("Start tangent", nurbCur2dFitData.StartTangent));
            data.Add(new Snoop.Data.Bool("Tangents exist", nurbCur2dFitData.TangentsExist));           
        }

        private void
        Stream(ArrayList data, NurbCurve3dData nurbCur3dData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(NurbCurve3dData)));

            data.Add(new Snoop.Data.Int("Degree", nurbCur3dData.Degree));
            data.Add(new Snoop.Data.Bool("Rational", nurbCur3dData.Rational));
            data.Add(new Snoop.Data.Bool("Periodic", nurbCur3dData.Periodic));
            data.Add(new Snoop.Data.Enumerable("Knots", nurbCur3dData.Knots));
            data.Add(new Snoop.Data.Enumerable("Weights", nurbCur3dData.Weights));
        }

        private void
        Stream(ArrayList data, NurbCurve3dFitData nurbCur3dFitData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(NurbCurve3dFitData)));

            data.Add(new Snoop.Data.Int("Degree", nurbCur3dFitData.Degree));
            data.Add(new Snoop.Data.Vector3d("End tangent", nurbCur3dFitData.EndTangent));
            data.Add(new Snoop.Data.Enumerable("Fit points", nurbCur3dFitData.FitPoints));
            data.Add(new Snoop.Data.Object("Fit tolerance", nurbCur3dFitData.FitTolerance));
            data.Add(new Snoop.Data.Vector3d("Start tangent", nurbCur3dFitData.StartTangent));
            data.Add(new Snoop.Data.Bool("Tangents exist", nurbCur3dFitData.TangentsExist));
        }

        private void
        Stream(ArrayList data, NurbSurfaceDefinition nurbSurfDef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(NurbSurfaceDefinition)));

            data.Add(new Snoop.Data.Enumerable("Control points", nurbSurfDef.ControlPoints));
            data.Add(new Snoop.Data.Enumerable("Weights", nurbSurfDef.Weights));
            data.Add(new Snoop.Data.Enumerable("UKnots", nurbSurfDef.UKnots));
            data.Add(new Snoop.Data.Enumerable("VKnots", nurbSurfDef.VKnots));
            data.Add(new Snoop.Data.Int("Degree in U", nurbSurfDef.DegreeInU));
            data.Add(new Snoop.Data.Int("Degree in V", nurbSurfDef.DegreeInV));
            data.Add(new Snoop.Data.Int("Properties in U", nurbSurfDef.PropertiesInU));
            data.Add(new Snoop.Data.Int("Properties in V", nurbSurfDef.PropertiesInV));
            data.Add(new Snoop.Data.Int("Number of control points in U", nurbSurfDef.NumberOfControlPointsInU));
            data.Add(new Snoop.Data.Int("Number of control points in V", nurbSurfDef.NumberOfControlPointsInV));
        }

        private void
        Stream(ArrayList data, PlanarEquationCoefficients planEqCoeff)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlanarEquationCoefficients)));

            data.Add(new Snoop.Data.Double("A", planEqCoeff.A));
            data.Add(new Snoop.Data.Double("B", planEqCoeff.B));
            data.Add(new Snoop.Data.Double("C", planEqCoeff.C));
            data.Add(new Snoop.Data.Double("D", planEqCoeff.D));          
        }

        private void
        Stream(ArrayList data, SurfaceSurfaceIntersectorConfigurations surfSurfIntConfigs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SurfaceSurfaceIntersectorConfigurations)));

            data.Add(new Snoop.Data.String("Surface1Left", surfSurfIntConfigs.Surface1Left.ToString()));
            data.Add(new Snoop.Data.String("Surface1Right", surfSurfIntConfigs.Surface1Right.ToString()));
            data.Add(new Snoop.Data.String("Surface2Left", surfSurfIntConfigs.Surface2Left.ToString()));
            data.Add(new Snoop.Data.String("Surface2Right", surfSurfIntConfigs.Surface2Right.ToString()));
            data.Add(new Snoop.Data.String("IntersectionType", surfSurfIntConfigs.IntersectionType.ToString()));
            data.Add(new Snoop.Data.Int("Dimension", surfSurfIntConfigs.Dimension));
        }  
    }
}
