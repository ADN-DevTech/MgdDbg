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
using MgdDbg.Snoop.Collectors;

using AcDb = Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.Snoop.CollectorExts {

    /// <summary>
    /// This is a Snoop Collector Extension object to collect data from Entity objects.
    /// </summary>

    public class Entity : CollectorExt
    {

        public
        Entity ()
        {
        }

        protected override void
        CollectEvent (object sender, CollectorEventArgs e)
        {
            // cast the sender object to the SnoopCollector we are expecting
            Collector snoopCollector = sender as Collector;
            if (snoopCollector == null) {
                Debug.Assert(false);    // why did someone else send us the message?
                return;
            }

            // branch to all types we are concerned with
            AcDb.Entity ent = e.ObjToSnoop as AcDb.Entity;
            if (ent != null) {
                Stream(snoopCollector.Data(), ent);
            }
        }

        #region Entity
        // main branch for anything derived from Entity (AcDbEntity)

        private void 
        TryStreamProperty<T>(ArrayList data, Func<T> getProperty, string propertyName) where T: Snoop.Data.Data
        {
            try
            {
                data.Add(getProperty());
            }
            catch (Autodesk.AutoCAD.Runtime.Exception e)
            {
                if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.NotApplicable)
                    data.Add(new Snoop.Data.Exception(propertyName, e));
                else if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.InvalidExtents)
                    data.Add(new Snoop.Data.Exception(propertyName, e));
                else if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.NullExtents)
                    data.Add(new Snoop.Data.Exception(propertyName, e));
                else if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.InvalidContext)
                    data.Add(new Snoop.Data.Exception(propertyName, e));
                else
                    throw e;
            }
        }

        private void
        Stream (ArrayList data, AcDb.Entity ent)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Entity)));

            data.Add(new Snoop.Data.ObjectId("Block ID", ent.BlockId));
            data.Add(new Snoop.Data.String("Layer", ent.Layer));
            data.Add(new Snoop.Data.ObjectId("Layer ID", ent.LayerId));
            data.Add(new Snoop.Data.ObjectToString("Color", ent.Color));
            data.Add(new Snoop.Data.Object("Entity color", ent.EntityColor));
            data.Add(new Snoop.Data.String("Color index", MgdDbg.Utils.AcadUi.ColorIndexToStr(ent.ColorIndex, true)));
            data.Add(new Snoop.Data.String("Linetype", ent.Linetype));
            data.Add(new Snoop.Data.ObjectId("Linetype ID", ent.LinetypeId));
            data.Add(new Snoop.Data.Distance("Linetype scale", ent.LinetypeScale));
            data.Add(new Snoop.Data.String("Lineweight", ent.LineWeight.ToString()));
            data.Add(new Snoop.Data.String("Plotstyle name", ent.PlotStyleName.ToString()));
            data.Add(new Snoop.Data.String("Plotstyle name ID", ent.PlotStyleNameId.ToString()));
            data.Add(new Snoop.Data.Bool("Visible", ent.Visible));
            data.Add(new Snoop.Data.Bool("Clone me for dragging", ent.CloneMeForDragging));
            data.Add(new Snoop.Data.Bool("Is planar", ent.IsPlanar));
            data.Add(new Snoop.Data.Object("ECS", ent.Ecs));
            data.Add(new Snoop.Data.ObjectCollection("Hyperlinks", (System.Collections.ICollection)ent.Hyperlinks));
            data.Add(new Snoop.Data.Object("Transparency", ent.Transparency));
            data.Add(new Snoop.Data.Bool("Cast shadows", ent.CastShadows));
            data.Add(new Snoop.Data.Bool("Recieve shadows", ent.ReceiveShadows));
            data.Add(new Snoop.Data.Object("Material mapper", ent.MaterialMapper));
            data.Add(new Snoop.Data.String("Material", ent.Material));
            data.Add(new Snoop.Data.ObjectId("Material ID", ent.MaterialId));
            data.Add(new Snoop.Data.String("Collision type", ent.CollisionType.ToString()));
            
                // the following functions only work in some cases
            TryStreamProperty(data, () => new Snoop.Data.Object("Geom extents", ent.GeometricExtents), "Geom extents");

            TryStreamProperty(data, () => new Snoop.Data.Object("Compound object transform", ent.CompoundObjectTransform), "Compound object transform");
  
            // branch to all known major sub-classes

            Autodesk.AutoCAD.EditorInput.Camera cam = ent as Autodesk.AutoCAD.EditorInput.Camera;
            if (cam != null) {
                Stream(data, cam);
                return;
            }

            DBText text = ent as DBText;
            if (text != null) {
                Stream(data, text);
                return;
            }

            Autodesk.AutoCAD.DatabaseServices.Viewport viewPort = ent as Autodesk.AutoCAD.DatabaseServices.Viewport;
            if (viewPort != null) {
                Stream(data, viewPort);
                return;
            }

            Curve crv = ent as Curve;
            if (crv != null) {
                Stream(data, crv);
                return;
            }

            Dimension dim = ent as Dimension;
            if (dim != null) {
                Stream(data, dim);
                return;
            }

            Vertex vert = ent as Vertex;
            if (vert != null) {
                Stream(data, vert);
                return;
            }

            Autodesk.AutoCAD.DatabaseServices.Surface surf = ent as Autodesk.AutoCAD.DatabaseServices.Surface;
            if (surf != null) {
                Stream(data, surf);
                return;
            }

            Solid3d sld3d = ent as Solid3d;
            if (sld3d != null) {
                Stream(data, sld3d);
                return;
            }

            BlockBegin blockBegin = ent as BlockBegin;
            if (blockBegin != null) {
                Stream(data, blockBegin);
                return;
            }

            BlockEnd blockEnd = ent as BlockEnd;
            if (blockEnd != null) {
                Stream(data, blockEnd);
                return;
            }

            BlockReference blkRef = ent as BlockReference;
            if (blkRef != null) {
                Stream(data, blkRef);
                return;
            }

            Body body = ent as Body;
            if (body != null) {
                Stream(data, body);
                return;
            }

            Face face = ent as Face;
            if (face != null) {
                Stream(data, face);
                return;
            }

            FeatureControlFrame fcf = ent as FeatureControlFrame;
            if (fcf != null) {
                Stream(data, fcf);
                return;
            }

            Ole2Frame ole2Frame = ent as Ole2Frame;
            if (ole2Frame != null) {
                Stream(data, ole2Frame);
                return;
            }

            Hatch hatch = ent as Hatch;
            if (hatch != null) {
                Stream(data, hatch);
                return;
            }

            Image img = ent as Image;
            if (img != null) {
                Stream(data, img);
                return;
            }

            DBPoint pt = ent as DBPoint;
            if (pt != null) {
                Stream(data, pt);
                return;
            }

            PolyFaceMesh pfaceMesh = ent as PolyFaceMesh;
            if (pfaceMesh != null) {
                Stream(data, pfaceMesh);
                return;
            }

            PolygonMesh mesh = ent as PolygonMesh;
            if (mesh != null) {
                Stream(data, mesh);
                return;
            }

            MText mtext = ent as MText;
            if (mtext != null) {
                Stream(data, mtext);
                return;
            }

            Mline mline = ent as Mline;
            if (mline != null) {
                Stream(data, mline);
                return;
            }

            MLeader mleader = ent as MLeader;
            if (mleader != null) {
                Stream(data, mleader);
                return;
            }

            ProxyEntity proxyEnt = ent as ProxyEntity;
            if (proxyEnt != null) {
                Stream(data, proxyEnt);
                return;
            }

            Region reg = ent as Region;
            if (reg != null) {
                Stream(data, reg);
                return;
            }

            SequenceEnd seqEnd = ent as SequenceEnd;
            if (seqEnd != null) {
                Stream(data, seqEnd);
                return;
            }

            Shape shp = ent as Shape;
            if (img != null) {
                Stream(data, shp);
                return;
            }

            Solid solid = ent as Solid;
            if (solid != null) {
                Stream(data, solid);
                return;
            }

            Autodesk.AutoCAD.DatabaseServices.Trace trace = ent as Autodesk.AutoCAD.DatabaseServices.Trace;
            if (trace != null) {
                Stream(data, trace);
                return;
            }

            Section sec = ent as Section;
            if (sec != null) {
                Stream(data, sec);
                return;
            }

            UnderlayReference ulayRef = ent as UnderlayReference;
            if (ulayRef != null) {
                Stream(data, ulayRef);
                return;
            }

            Light light = ent as Light;
            if (light != null) {
                Stream(data, light);
                return;
            }

        }

        #region Curves
        // main branch for anything derived from Curve (AcDbCurve)
        private void
        Stream (ArrayList data, Curve crv)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Curve)));

            // MvPart throws an exception for Area
            try {
                data.Add(new Snoop.Data.Double("Area", crv.Area));
            }
            catch (Autodesk.AutoCAD.Runtime.Exception e) {
                data.Add(new Snoop.Data.Exception("Area", e));
            }

            data.Add(new Snoop.Data.Bool("Closed", crv.Closed));
            data.Add(new Snoop.Data.Double("Start param", crv.StartParam));
            data.Add(new Snoop.Data.Double("End param", crv.EndParam));

            // Start point and end point are not applicable for some types of 
            // curves (e.g., Ray, Xline)
            // the following is only valid in some cases
            TryStreamProperty(data, () => new Snoop.Data.Point3d("Start point", crv.StartPoint), "Start point");
            TryStreamProperty(data, () => new Snoop.Data.Point3d("End point", crv.EndPoint), "End point");

            data.Add(new Snoop.Data.Bool("Is periodic", crv.IsPeriodic));

            TryStreamProperty(data, () => new Snoop.Data.Object("Spline", crv.Spline), "Spline");

            // branch to all known major sub-classes
            Line line = crv as Line;
            if (line != null) {
                Stream(data, line);
                return;
            }

            Arc arc = crv as Arc;
            if (arc != null) {
                Stream(data, arc);
                return;
            }

            Circle circ = crv as Circle;
            if (circ != null) {
                Stream(data, circ);
                return;
            }

            Ellipse ellipse = crv as Ellipse;
            if (ellipse != null) {
                Stream(data, ellipse);
                return;
            }

            Polyline pline = crv as Polyline;
            if (pline != null) {
                Stream(data, pline);
                return;
            }

            Polyline2d pline2d = crv as Polyline2d;
            if (pline2d != null) {
                Stream(data, pline2d);
                return;
            }

            Polyline3d pline3d = crv as Polyline3d;
            if (pline3d != null) {
                Stream(data, pline3d);
                return;
            }

            Leader ldr = crv as Leader;
            if (ldr != null) {
                Stream(data, ldr);
                return;
            }

            Ray ray = crv as Ray;
            if (ray != null) {
                Stream(data, ray);
                return;
            }

            Xline xline = crv as Xline;
            if (xline != null) {
                Stream(data, xline);
                return;
            }

            Spline spline = crv as Spline;
            if (spline != null) {
                Stream(data, spline);
                return;
            }
        }

        // data for Line (AcDbLine)
        private void
        Stream (ArrayList data, Line line)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Line)));

            data.Add(new Snoop.Data.Point3d("Start", line.StartPoint));
            data.Add(new Snoop.Data.Point3d("End", line.EndPoint));
            data.Add(new Snoop.Data.Vector3d("Normal", line.Normal));
            data.Add(new Snoop.Data.Distance("Thickness", line.Thickness));
        }

        // data for Arc (AcDbArc)
        private void
        Stream (ArrayList data, Arc arc)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Arc)));

            data.Add(new Snoop.Data.Point3d("Center", arc.Center));
            data.Add(new Snoop.Data.Distance("Radius", arc.Radius));
            data.Add(new Snoop.Data.Angle("Start angle", arc.StartAngle));
            data.Add(new Snoop.Data.Angle("End angle", arc.EndAngle));
            data.Add(new Snoop.Data.Vector3d("Normal", arc.Normal));
            data.Add(new Snoop.Data.Distance("Thickness", arc.Thickness));
        }

        // data for Circle (AcDbCircle)
        private void
        Stream (ArrayList data, Circle circ)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Circle)));

            data.Add(new Snoop.Data.Point3d("Center", circ.Center));
            data.Add(new Snoop.Data.Distance("Radius", circ.Radius));
            data.Add(new Snoop.Data.Vector3d("Normal", circ.Normal));
            data.Add(new Snoop.Data.Distance("Thickness", circ.Thickness));
        }

        private void
        Stream (ArrayList data, Ellipse ellipse)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Ellipse)));

            data.Add(new Snoop.Data.Point3d("Center", ellipse.Center));
            data.Add(new Snoop.Data.Angle("Start angle", ellipse.StartAngle));
            data.Add(new Snoop.Data.Angle("End angle", ellipse.EndAngle));
            data.Add(new Snoop.Data.Vector3d("Major axis", ellipse.MajorAxis));
            data.Add(new Snoop.Data.Vector3d("Minor axis", ellipse.MinorAxis));
            data.Add(new Snoop.Data.Double("Radius ratio", ellipse.RadiusRatio));
            data.Add(new Snoop.Data.Vector3d("Normal", ellipse.Normal));
            data.Add(new Snoop.Data.Bool("Is null", ellipse.IsNull));
        }

        private void
        Stream (ArrayList data, Polyline pline)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Polyline)));

            data.Add(new Snoop.Data.Bool("Closed", pline.Closed));
            data.Add(new Snoop.Data.Distance("Constant width", pline.ConstantWidth));
            data.Add(new Snoop.Data.Distance("Elevation", pline.Elevation));
            data.Add(new Snoop.Data.Bool("Has bulges", pline.HasBulges));
            data.Add(new Snoop.Data.Bool("Has width", pline.HasWidth));
            data.Add(new Snoop.Data.Bool("Is only lines", pline.IsOnlyLines));
            data.Add(new Snoop.Data.Bool("Plinegen", pline.Plinegen));
            data.Add(new Snoop.Data.Vector3d("Normal", pline.Normal));
            data.Add(new Snoop.Data.Distance("Thickness", pline.Thickness));

            data.Add(new Snoop.Data.CategorySeparator("Vertices"));
            data.Add(new Snoop.Data.Int("Number of vertices", pline.NumberOfVertices));

            for (int i = 0; i < pline.NumberOfVertices; i++) {
                data.Add(new Snoop.Data.CategorySeparator(string.Format("Vertex [{0}]", i)));

                SegmentType segType = pline.GetSegmentType(i);
                data.Add(new Snoop.Data.String("Segment type", segType.ToString()));
                data.Add(new Snoop.Data.Point2d("2D point", pline.GetPoint2dAt(i)));
                data.Add(new Snoop.Data.Point3d("3D point", pline.GetPoint3dAt(i)));
                data.Add(new Snoop.Data.Distance("Start width", pline.GetStartWidthAt(i)));
                data.Add(new Snoop.Data.Distance("End width", pline.GetEndWidthAt(i)));
                data.Add(new Snoop.Data.Double("Bulge", pline.GetBulgeAt(i)));

                if (segType == SegmentType.Arc)
                    data.Add(new Snoop.Data.Object("Arc segment", pline.GetArcSegmentAt(i)));
                else if (segType == SegmentType.Line)
                    data.Add(new Snoop.Data.Object("Line segment", pline.GetLineSegmentAt(i)));
            }
        }

        private void
        Stream (ArrayList data, Polyline2d pline)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Polyline2d)));

            data.Add(new Snoop.Data.String("Poly type", pline.PolyType.ToString()));
            data.Add(new Snoop.Data.Bool("Closed", pline.Closed));
            data.Add(new Snoop.Data.Distance("Default start width", pline.DefaultStartWidth));
            data.Add(new Snoop.Data.Distance("Default end width", pline.DefaultEndWidth));
            data.Add(new Snoop.Data.Distance("Elevation", pline.Elevation));
            data.Add(new Snoop.Data.Bool("Linetype generation on", pline.LinetypeGenerationOn));
            data.Add(new Snoop.Data.Vector3d("Normal", pline.Normal));
            data.Add(new Snoop.Data.Distance("Thickness", pline.Thickness));

            ObjectIdCollection vertList = new ObjectIdCollection();
            System.Collections.IEnumerator iter = pline.GetEnumerator();
            while (iter.MoveNext())
                vertList.Add((ObjectId)iter.Current);

            data.Add(new Snoop.Data.ObjectIdCollection("Vertices", vertList));
        }

        private void
        Stream (ArrayList data, Polyline3d pline)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Polyline3d)));

            data.Add(new Snoop.Data.String("Poly type", pline.PolyType.ToString()));
            data.Add(new Snoop.Data.Bool("Closed", pline.Closed));

            ObjectIdCollection vertList = new ObjectIdCollection();

            // In case of MvParts the pline id is always null and so the Get
            if (pline.ObjectId != ObjectId.Null) {
                System.Collections.IEnumerator iter = pline.GetEnumerator();
                while (iter.MoveNext())
                    vertList.Add((ObjectId)iter.Current);
            }
            data.Add(new Snoop.Data.ObjectIdCollection("Vertices", vertList));
        }


        private void
        Stream (ArrayList data, Leader ldr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Leader)));

            data.Add(new Snoop.Data.ObjectId("Dimension style", ldr.DimensionStyle));
            //data.Add(new Snoop.Data.Object("Dimstyle data", ldr.DimstyleData)); // redundant with the one above (just an opened Dimstyle object)
            data.Add(new Snoop.Data.String("Annotation type", ldr.AnnoType.ToString()));
            data.Add(new Snoop.Data.ObjectId("Annotation ID", ldr.Annotation));
            data.Add(new Snoop.Data.Distance("Annotation width", ldr.AnnoWidth));
            data.Add(new Snoop.Data.Distance("Annotation height", ldr.AnnoHeight));
            data.Add(new Snoop.Data.Vector3d("Annotation offset", ldr.AnnotationOffset));
            data.Add(new Snoop.Data.Distance("Dimasz", ldr.Dimasz));
            data.Add(new Snoop.Data.ObjectToString("Dimclrd", ldr.Dimclrd));
            data.Add(new Snoop.Data.ObjectId("Dimension style", ldr.DimensionStyle));
            data.Add(new Snoop.Data.Distance("Dimgap", ldr.Dimgap));
            data.Add(new Snoop.Data.ObjectId("Dimldrblk", ldr.Dimldrblk));
            data.Add(new Snoop.Data.String("Dimlwd", ldr.Dimlwd.ToString()));
            data.Add(new Snoop.Data.Bool("Dimsah", ldr.Dimsah));
            data.Add(new Snoop.Data.Distance("Dimscale", ldr.Dimscale));
            data.Add(new Snoop.Data.Int("Dimtad", ldr.Dimtad));
            //data.Add(new Snoop.Data.ObjectId("Dimtxsty", ldr.Dimtxsty));      // TBD: Fix JMA
            data.Add(new Snoop.Data.Distance("Dimtxt", ldr.Dimtxt));
            data.Add(new Snoop.Data.Bool("Has arrowhead", ldr.HasArrowHead));
            data.Add(new Snoop.Data.Bool("Has hookline", ldr.HasHookLine));
            data.Add(new Snoop.Data.Bool("Is splined", ldr.IsSplined));
            data.Add(new Snoop.Data.Vector3d("Normal", ldr.Normal));

            data.Add(new Snoop.Data.CategorySeparator("Vertices"));
            data.Add(new Snoop.Data.Point3d("First vertex", ldr.FirstVertex));
            data.Add(new Snoop.Data.Point3d("Last vertex", ldr.LastVertex));
            data.Add(new Snoop.Data.Int("Number of vertices", ldr.NumVertices));

            for (int i = 0; i < ldr.NumVertices; i++) {
                data.Add(new Snoop.Data.Point3d(string.Format("Vertex [{0}]", i), ldr.VertexAt(i)));
            }

        }

        private void
        Stream (ArrayList data, Ray ray)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Ray)));

            data.Add(new Snoop.Data.Point3d("Base point", ray.BasePoint));
            data.Add(new Snoop.Data.Vector3d("Unit direction", ray.UnitDir));
        }

        private void
        Stream (ArrayList data, Xline xline)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Xline)));

            data.Add(new Snoop.Data.Point3d("Base point", xline.BasePoint));
            data.Add(new Snoop.Data.Vector3d("Unit direction", xline.UnitDir));
        }

        private void
        Stream (ArrayList data, Spline spline)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Spline)));

            data.Add(new Snoop.Data.Int("Degree", spline.Degree));            

            TryStreamProperty(data, () => new Snoop.Data.Vector3d("Start fit tangent", spline.StartFitTangent), "Start fit tangent");
            TryStreamProperty(data, () => new Snoop.Data.Vector3d("End fit tangent", spline.EndFitTangent), "End fit tangent");
            
            data.Add(new Snoop.Data.Bool("Has fit data", spline.HasFitData));

            TryStreamProperty(data, () => new Snoop.Data.Object("Fit data", spline.FitData), "Fit data");
            TryStreamProperty(data, () => new Snoop.Data.Double("Fit tolerance", spline.FitTolerance), "Fit tolerance");
            
            data.Add(new Snoop.Data.Object("Nurbs data", spline.NurbsData));
            data.Add(new Snoop.Data.Bool("Is null", spline.IsNull));
            data.Add(new Snoop.Data.Bool("Is rational", spline.IsRational));
            
            data.Add(new Snoop.Data.Int("Number of control points", spline.NumControlPoints));
            for (int i=0; i<spline.NumControlPoints; i++) {
                data.Add(new Snoop.Data.Point3d(string.Format("Control point [{0}]", i), spline.GetControlPointAt(i)));
            }

            data.Add(new Snoop.Data.Int("Number of fit points", spline.NumFitPoints));
            for (int i=0; i<spline.NumFitPoints; i++) {
                data.Add(new Snoop.Data.Point3d(string.Format("Fit point [{0}]", i), spline.GetFitPointAt(i)));
            }

            Helix helix = spline as Helix;
            if (helix != null) {
                Stream(data, helix);
                return;
            }
        }

        private void
        Stream (ArrayList data, Helix helix)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Helix)));

            data.Add(new Snoop.Data.String("Constrain", helix.Constrain.ToString()));
            data.Add(new Snoop.Data.Vector3d("Axis vector", helix.AxisVector));
            data.Add(new Snoop.Data.Distance("Base radius", helix.BaseRadius));
            data.Add(new Snoop.Data.Distance("Top radius", helix.TopRadius));
            data.Add(new Snoop.Data.Distance("Height", helix.Height));
            data.Add(new Snoop.Data.Distance("Turn height", helix.TurnHeight));
            data.Add(new Snoop.Data.Double("Turns", helix.Turns));
            data.Add(new Snoop.Data.Double("Turn slope", helix.TurnSlope));
            data.Add(new Snoop.Data.Distance("Total length", helix.TotalLength));
            data.Add(new Snoop.Data.Bool("Twist", helix.Twist));
        }



        #endregion Curves

        private void
        Stream (ArrayList data, BlockReference blkRef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BlockReference)));

            data.Add(new Snoop.Data.ObjectId("Anonymous block table record", blkRef.AnonymousBlockTableRecord));
            data.Add(new Snoop.Data.ObjectId("Block table record", blkRef.BlockTableRecord));
            data.Add(new Snoop.Data.Point3d("Position", blkRef.Position));
            data.Add(new Snoop.Data.Angle("Rotation", blkRef.Rotation));
            data.Add(new Snoop.Data.String("Scale factors", blkRef.ScaleFactors.ToString()));
            data.Add(new Snoop.Data.Vector3d("Normal", blkRef.Normal));
            data.Add(new Snoop.Data.Object("Block transform", blkRef.BlockTransform));
            data.Add(new Snoop.Data.Bool("Treat as block ref for explode", blkRef.TreatAsBlockRefForExplode));

            ObjectIdCollection attCol = new ObjectIdCollection();
            AttributeCollection atts = blkRef.AttributeCollection;
            for (int i = 0; i < atts.Count; i++)
                attCol.Add(atts[i]);
            data.Add(new Snoop.Data.ObjectIdCollection("Attributes", attCol));

            data.Add(new Snoop.Data.Bool("Is dynamic block", blkRef.IsDynamicBlock));
            data.Add(new Snoop.Data.ObjectId("Dynamic block table record", blkRef.DynamicBlockTableRecord));
            data.Add(new Snoop.Data.Enumerable("Dynaimc block reference properties", blkRef.DynamicBlockReferencePropertyCollection));

            MInsertBlock mInsert = blkRef as MInsertBlock;
            if (mInsert != null) {
                Stream(data, mInsert);
                return;
            }

            Table tbl = blkRef as Table;
            if (tbl != null) {
                Stream(data, tbl);
                return;
            }
        }

        private void
        Stream (ArrayList data, Body body)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Body)));

            data.Add(new Snoop.Data.Bool("Is null", body.IsNull));
            data.Add(new Snoop.Data.Int("Number of changes", body.NumChanges));
        }

        private void
        Stream (ArrayList data, MInsertBlock mInsert)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MInsertBlock)));

            data.Add(new Snoop.Data.Int("Columns", mInsert.Columns));
            data.Add(new Snoop.Data.Distance("Column spacing", mInsert.ColumnSpacing));
            data.Add(new Snoop.Data.Int("Rows", mInsert.Rows));
            data.Add(new Snoop.Data.Distance("Row spacing", mInsert.RowSpacing));
        }

        private void
        Stream (ArrayList data, DBPoint pt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DBPoint)));

            data.Add(new Snoop.Data.Point3d("Position", pt.Position));
            data.Add(new Snoop.Data.Angle("ECS rotation", pt.EcsRotation));
            data.Add(new Snoop.Data.Vector3d("Normal", pt.Normal));
            data.Add(new Snoop.Data.Distance("Thickness", pt.Thickness));
        }

        // data for Face (AcDbFace)
        private void
        Stream (ArrayList data, Face face)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Face)));

            for (short i = 0; i < 4; i++) {
                data.Add(new Snoop.Data.Point3d(string.Format("Vertex #{0:g}", i + 1), face.GetVertexAt(i)));
            }
            for (short i = 0; i < 4; i++) {
                data.Add(new Snoop.Data.Bool(string.Format("Edge #{0:g}", i + 1), face.IsEdgeVisibleAt(i)));
            }
        }

        // data for Face (AcDbSolid)
        private void
        Stream (ArrayList data, Solid solid)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Solid)));

            for (short i = 0; i < 4; i++) {
                data.Add(new Snoop.Data.Point3d(string.Format("Point #{0:g}", i + 1), solid.GetPointAt(i)));
            }

            data.Add(new Snoop.Data.Vector3d("Normal", solid.Normal));
            data.Add(new Snoop.Data.Distance("Thickness", solid.Thickness));
        }

        private void
        Stream (ArrayList data, Autodesk.AutoCAD.DatabaseServices.Trace trace)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.DatabaseServices.Trace)));

            data.Add(new Snoop.Data.Vector3d("Normal", trace.Normal));
            data.Add(new Snoop.Data.Distance("Thickness", trace.Thickness));
        }

        private void
        Stream (ArrayList data, FeatureControlFrame fcf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FeatureControlFrame)));

            data.Add(new Snoop.Data.Vector3d("Direction", fcf.Direction));
            data.Add(new Snoop.Data.Point3d("Location", fcf.Location));
            data.Add(new Snoop.Data.Vector3d("Normal", fcf.Normal));
            data.Add(new Snoop.Data.String("Text", fcf.Text));
            data.Add(new Snoop.Data.ObjectToString("Dimclrd", fcf.Dimclrd));
            data.Add(new Snoop.Data.ObjectToString("Dimclrt", fcf.Dimclrt));
            data.Add(new Snoop.Data.ObjectId("Dimension style", fcf.DimensionStyle));
            data.Add(new Snoop.Data.Distance("Dimgap", fcf.Dimgap));
            data.Add(new Snoop.Data.Distance("Dimscale", fcf.Dimscale));
            data.Add(new Snoop.Data.ObjectId("Dimtxsty", fcf.Dimtxsty));
            data.Add(new Snoop.Data.Distance("Dimtxt", fcf.Dimtxt));
            data.Add(new Snoop.Data.Object("Bounding points", fcf.GetBoundingPoints()));
            data.Add(new Snoop.Data.Object("Bounding polyline", fcf.GetBoundingPolyline()));
        }

        private void
        Stream (ArrayList data, Ole2Frame ole2Frame)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Ole2Frame)));

            data.Add(new Snoop.Data.Object("Ole Object", ole2Frame.OleObject));
            data.Add(new Snoop.Data.Int("Output quality", ole2Frame.OutputQuality));
            data.Add(new Snoop.Data.Int("Auto-output quality", ole2Frame.AutoOutputQuality));
            data.Add(new Snoop.Data.Bool("Is linked", ole2Frame.IsLinked));
            if (ole2Frame.IsLinked) {
                data.Add(new Snoop.Data.String("Link name", ole2Frame.LinkName));
                data.Add(new Snoop.Data.String("Link path", ole2Frame.LinkPath));
            }
            data.Add(new Snoop.Data.Point3d("Location", ole2Frame.Location));
            data.Add(new Snoop.Data.Bool("Lock aspect", ole2Frame.LockAspect));
            data.Add(new Snoop.Data.Object("Position 2D", ole2Frame.Position2d));
            data.Add(new Snoop.Data.Object("Position 3D", ole2Frame.Position3d));
            data.Add(new Snoop.Data.Angle("Rotation", ole2Frame.Rotation));
            data.Add(new Snoop.Data.Double("Scale width", ole2Frame.ScaleWidth));
            data.Add(new Snoop.Data.Double("Scale height", ole2Frame.ScaleHeight));
            data.Add(new Snoop.Data.String("Type", ole2Frame.Type.ToString()));
            data.Add(new Snoop.Data.String("User type", ole2Frame.UserType));
            data.Add(new Snoop.Data.Double("WCS width", ole2Frame.WcsWidth));
            data.Add(new Snoop.Data.Double("WCS height", ole2Frame.WcsHeight));
        }

        private void
        Stream (ArrayList data, Mline mline)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Mline)));

            data.Add(new Snoop.Data.ObjectId("Style", mline.Style));
            data.Add(new Snoop.Data.Bool("Is closed", mline.IsClosed));
            data.Add(new Snoop.Data.String("Justification", mline.Justification.ToString()));
            data.Add(new Snoop.Data.Distance("Scale", mline.Scale));
            data.Add(new Snoop.Data.Vector3d("Normal", mline.Normal));
            data.Add(new Snoop.Data.Bool("Suppress start caps", mline.SupressStartCaps));
            data.Add(new Snoop.Data.Bool("Suppress end caps", mline.SupressEndCaps));

            data.Add(new Snoop.Data.Int("Number of vertices", mline.NumberOfVertices));

            for (int i = 0; i < mline.NumberOfVertices; i++) {
                data.Add(new Snoop.Data.Point3d(string.Format("Vertex [{0}]", i), mline.VertexAt(i)));
            }
        }

        private void
        Stream(ArrayList data, MLeader mleader)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MLeader)));

            data.Add(new Snoop.Data.Double("Arrow size", mleader.ArrowSize));
            data.Add(new Snoop.Data.ObjectId("Arrow symbol Id", mleader.ArrowSymbolId));
            data.Add(new Snoop.Data.Object("Block color", mleader.BlockColor));
            data.Add(new Snoop.Data.String("Block connection type", mleader.BlockConnectionType.ToString()));
            data.Add(new Snoop.Data.ObjectId("Block content Id", mleader.BlockContentId));
            TryStreamProperty(data, () => new Snoop.Data.Point3d("Block position", mleader.BlockPosition), "Block position");
            data.Add(new Snoop.Data.Double("Block rotation", mleader.BlockRotation));
            data.Add(new Snoop.Data.Scale3d("Block scale", mleader.BlockScale));
            data.Add(new Snoop.Data.String("Content type", mleader.ContentType.ToString()));
            data.Add(new Snoop.Data.Double("Dogleg length", mleader.DoglegLength));
            data.Add(new Snoop.Data.Bool("Enable annotation scale", mleader.EnableAnnotationScale));
            data.Add(new Snoop.Data.Bool("Enable dogleg", mleader.EnableDogleg));
            data.Add(new Snoop.Data.Bool("Enable frame text", mleader.EnableFrameText));
            data.Add(new Snoop.Data.Bool("Enable landing", mleader.EnableLanding));
            data.Add(new Snoop.Data.Double("Landing gap", mleader.LandingGap));
            data.Add(new Snoop.Data.Int("Leader count", mleader.LeaderCount));
            data.Add(new Snoop.Data.Object("Leader line color", mleader.LeaderLineColor));
            data.Add(new Snoop.Data.Int("Leader line count", mleader.LeaderLineCount));
            data.Add(new Snoop.Data.String("Leader line type", mleader.LeaderLineType.ToString()));
            data.Add(new Snoop.Data.ObjectId("Leader line type Id", mleader.LeaderLineTypeId));
            data.Add(new Snoop.Data.String("Leader line weight", mleader.LeaderLineWeight.ToString()));
            data.Add(new Snoop.Data.ObjectId("MLeader style", mleader.MLeaderStyle));
            data.Add(new Snoop.Data.Object("MText", mleader.MText));
            data.Add(new Snoop.Data.Vector3d("Normal", mleader.Normal));
            data.Add(new Snoop.Data.String("Text alignment type", mleader.TextAlignmentType.ToString()));
            data.Add(new Snoop.Data.String("Text angle type", mleader.TextAngleType.ToString()));
            data.Add(new Snoop.Data.String("Text attachment type", mleader.TextAttachmentType.ToString()));
            data.Add(new Snoop.Data.Object("Text color", mleader.TextColor));
            data.Add(new Snoop.Data.Double("Text height", mleader.TextHeight));
            data.Add(new Snoop.Data.Point3d("Text location", mleader.TextLocation));
            data.Add(new Snoop.Data.ObjectId("Text style Id", mleader.TextStyleId));
            TryStreamProperty(data, () => new Snoop.Data.Point3d("Tolerance location", mleader.ToleranceLocation), "Tolerance location");
        }

        private void
        Stream (ArrayList data, ProxyEntity proxyEnt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ProxyEntity)));

            data.Add(new Snoop.Data.String("Application description", proxyEnt.ApplicationDescription));
            data.Add(new Snoop.Data.String("Graphics metafile type", proxyEnt.GraphicsMetafileType.ToString()));
            data.Add(new Snoop.Data.String("Original class name", proxyEnt.OriginalClassName));
            data.Add(new Snoop.Data.String("Original DXF name", proxyEnt.OriginalDxfName));
            data.Add(new Snoop.Data.Int("Proxy Flags", proxyEnt.ProxyFlags));
            data.Add(new Snoop.Data.ObjectCollection("References", proxyEnt.GetReferences()));
        }

        private void
        Stream (ArrayList data, MText mtext)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MText)));

            data.Add(new Snoop.Data.String("Contents", mtext.Contents));
            data.Add(new Snoop.Data.Point3d("Location", mtext.Location));
            data.Add(new Snoop.Data.Vector3d("Direction", mtext.Direction));
            data.Add(new Snoop.Data.Angle("Rotation", mtext.Rotation));
            data.Add(new Snoop.Data.String("Flow direction", mtext.FlowDirection.ToString()));
            data.Add(new Snoop.Data.Vector3d("Normal", mtext.Normal));
            //data.Add(new Snoop.Data.ObjectId("Text style", mtext.TextStyle));     // TBD: Fix JMA
            data.Add(new Snoop.Data.Distance("Text height", mtext.TextHeight));
            data.Add(new Snoop.Data.Distance("Width", mtext.Width));
            data.Add(new Snoop.Data.Distance("Actual width", mtext.ActualWidth));
            data.Add(new Snoop.Data.Distance("Actual height", mtext.ActualHeight));
            data.Add(new Snoop.Data.Double("Ascent", mtext.Ascent));
            data.Add(new Snoop.Data.Double("Descent", mtext.Descent));
            data.Add(new Snoop.Data.String("Attachment", mtext.Attachment.ToString()));
            data.Add(new Snoop.Data.Bool("Use background color", mtext.UseBackgroundColor));

            data.Add(new Snoop.Data.Bool("Background fill", mtext.BackgroundFill));
            if (mtext.BackgroundFill) {
                data.Add(new Snoop.Data.ObjectToString("Background fill color", mtext.BackgroundFillColor));
            }
            
            TryStreamProperty(data, () => new Snoop.Data.Double("Background scale factor", mtext.BackgroundScaleFactor), "Background scale factor");

            data.Add(new Snoop.Data.Object("Background transparency", mtext.Transparency));

            data.Add(new Snoop.Data.Distance("Line spacing factor", mtext.LineSpacingFactor));
            data.Add(new Snoop.Data.String("Line spacing style", mtext.LineSpacingStyle.ToString()));

            data.Add(new Snoop.Data.CategorySeparator("Static Members"));

            data.Add(new Snoop.Data.String("Align change", MText.AlignChange));
            data.Add(new Snoop.Data.String("Color change", MText.ColorChange));
            data.Add(new Snoop.Data.String("Font change", MText.FontChange));
            data.Add(new Snoop.Data.String("Height change", MText.HeightChange));
            data.Add(new Snoop.Data.String("Oblique change", MText.ObliqueChange));
            data.Add(new Snoop.Data.String("Overline on", MText.OverlineOn));
            data.Add(new Snoop.Data.String("Overline off", MText.OverlineOff));
            data.Add(new Snoop.Data.String("Underline on", MText.UnderlineOn));
            data.Add(new Snoop.Data.String("Underline off", MText.UnderlineOff));
            data.Add(new Snoop.Data.String("Block begin", MText.BlockBegin));
            data.Add(new Snoop.Data.String("Block end", MText.BlockEnd));
            data.Add(new Snoop.Data.String("Stack start", MText.StackStart));
            data.Add(new Snoop.Data.String("Non break space", MText.NonBreakSpace));
            data.Add(new Snoop.Data.String("Line break", MText.LineBreak));
            data.Add(new Snoop.Data.String("Paragraph break", MText.ParagraphBreak));
            data.Add(new Snoop.Data.String("Track change", MText.TrackChange));
            data.Add(new Snoop.Data.String("Width change", MText.WidthChange));
        }        

        private void
        Stream (ArrayList data, Viewport viewPort)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.DatabaseServices.Viewport)));

            data.Add(new Snoop.Data.Object("Ambient light color", viewPort.AmbientLightColor));
            data.Add(new Snoop.Data.Distance("Back clip distance", viewPort.BackClipDistance));
            data.Add(new Snoop.Data.Bool("Back clip on", viewPort.BackClipOn));
            data.Add(new Snoop.Data.ObjectId("Background", viewPort.Background));
            data.Add(new Snoop.Data.Double("Brightness", viewPort.Brightness));
            data.Add(new Snoop.Data.Point3d("Center point", viewPort.CenterPoint));
            data.Add(new Snoop.Data.Int("Circle sides", viewPort.CircleSides));
            data.Add(new Snoop.Data.Double("Contrast", viewPort.Contrast));
            data.Add(new Snoop.Data.Double("Custom scale", viewPort.CustomScale));
            data.Add(new Snoop.Data.Bool("Default lighting on", viewPort.DefaultLightingOn));
            data.Add(new Snoop.Data.String("Default lighting type", viewPort.DefaultLightingType.ToString()));
            data.Add(new Snoop.Data.String("Effective plot style sheet", viewPort.EffectivePlotStyleSheet));
            data.Add(new Snoop.Data.Double("Elevation", viewPort.Elevation));
            data.Add(new Snoop.Data.Bool("Fast zoom on", viewPort.FastZoomOn));
            data.Add(new Snoop.Data.Bool("Front clip at eye on", viewPort.FrontClipAtEyeOn));
            data.Add(new Snoop.Data.Distance("Front clip distance", viewPort.FrontClipDistance));
            data.Add(new Snoop.Data.Bool("Front clip on", viewPort.FrontClipOn));
            data.Add(new Snoop.Data.Bool("Grid adaptive", viewPort.GridAdaptive));
            data.Add(new Snoop.Data.Bool("Grid bound to limits", viewPort.GridBoundToLimits));
            data.Add(new Snoop.Data.Bool("Grid follow", viewPort.GridFollow));
            data.Add(new Snoop.Data.Vector2d("Grid increment", viewPort.GridIncrement));
            data.Add(new Snoop.Data.Int("Grid major", viewPort.GridMajor));
            data.Add(new Snoop.Data.Bool("Grid on", viewPort.GridOn));
            data.Add(new Snoop.Data.Bool("Grid sub division restricted", viewPort.GridSubdivisionRestricted));
            data.Add(new Snoop.Data.Double("Height", viewPort.Height));
            data.Add(new Snoop.Data.Bool("Hidden lines removed", viewPort.HiddenLinesRemoved));
            data.Add(new Snoop.Data.Double("Lens length", viewPort.LensLength));
            data.Add(new Snoop.Data.Bool("Locked", viewPort.Locked));
            data.Add(new Snoop.Data.ObjectId("Non rect clip entity id", viewPort.NonRectClipEntityId));
            data.Add(new Snoop.Data.Bool("Non rect clip on", viewPort.NonRectClipOn));
            data.Add(new Snoop.Data.Int("Number", viewPort.Number));
            data.Add(new Snoop.Data.Bool("On", viewPort.On));
            data.Add(new Snoop.Data.Bool("Perspective on", viewPort.PerspectiveOn));
            data.Add(new Snoop.Data.Bool("Plot as raster", viewPort.PlotAsRaster));
            /// throws eNotImplementedYet
            /// data.Add(new Snoop.Data.String("Plot style sheet", viewPort.PlotStyleSheet));
            data.Add(new Snoop.Data.Bool("Plot wireframe", viewPort.PlotWireframe));
           // data.Add(new Snoop.Data.String("Render mode", viewPort.RenderMode.ToString()));
            data.Add(new Snoop.Data.String("Shade plot", viewPort.ShadePlot.ToString()));
            data.Add(new Snoop.Data.ObjectId("Shade plot id", viewPort.ShadePlotId));
            data.Add(new Snoop.Data.Double("Snap angle", viewPort.SnapAngle));
            data.Add(new Snoop.Data.Point2d("Snap base point", viewPort.SnapBasePoint));
            data.Add(new Snoop.Data.Vector2d("Snap increment", viewPort.SnapIncrement));
            data.Add(new Snoop.Data.Bool("Snap isometric", viewPort.SnapIsometric));
            data.Add(new Snoop.Data.Int("Snap iso pair", viewPort.SnapIsoPair));
            data.Add(new Snoop.Data.Bool("Snap on", viewPort.SnapOn));
            data.Add(new Snoop.Data.String("Standard scale", viewPort.StandardScale.ToString()));
            data.Add(new Snoop.Data.ObjectId("Sun id", viewPort.SunId));
            data.Add(new Snoop.Data.Bool("Transparent", viewPort.Transparent));
            data.Add(new Snoop.Data.Double("Twist angle", viewPort.TwistAngle));
            data.Add(new Snoop.Data.Bool("Ucs follow mode on", viewPort.UcsFollowModeOn));
            data.Add(new Snoop.Data.Bool("Ucs icon at origin", viewPort.UcsIconAtOrigin));
            data.Add(new Snoop.Data.Bool("Ucs icon visible", viewPort.UcsIconVisible));
            data.Add(new Snoop.Data.ObjectId("Ucs name", viewPort.UcsName));
            data.Add(new Snoop.Data.String("Ucs orthographic", viewPort.UcsOrthographic.ToString()));
            data.Add(new Snoop.Data.Bool("Ucs per viewport", viewPort.UcsPerViewport));
            data.Add(new Snoop.Data.Point2d("View center", viewPort.ViewCenter));
            data.Add(new Snoop.Data.Vector3d("View direction", viewPort.ViewDirection));
            data.Add(new Snoop.Data.Double("View height", viewPort.ViewHeight));
            data.Add(new Snoop.Data.String("View orthographic", viewPort.ViewOrthographic.ToString()));
            data.Add(new Snoop.Data.Point3d("View target", viewPort.ViewTarget));
            data.Add(new Snoop.Data.ObjectId("Visual style id", viewPort.VisualStyleId));
            data.Add(new Snoop.Data.Double("Width", viewPort.Width));
            data.Add(new Snoop.Data.ObjectIdCollection("Frozen layers", viewPort.GetFrozenLayers()));

        }

        private void
        Stream (ArrayList data, BlockBegin blkBegin)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BlockBegin)));

            // no data here
        }

        private void
        Stream (ArrayList data, BlockEnd blkEnd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BlockEnd)));

            // no data here
        }

        private void
        Stream (ArrayList data, Solid3d solid)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Solid3d)));

            data.Add(new Snoop.Data.Double("Area", solid.Area));
            data.Add(new Snoop.Data.Bool("Is null", solid.IsNull));
            data.Add(new Snoop.Data.Int("Number of changes", solid.NumChanges));
            data.Add(new Snoop.Data.Bool("Record history", solid.RecordHistory));
            data.Add(new Snoop.Data.Bool("Show history", solid.ShowHistory));
            data.Add(new Snoop.Data.Object("Mass properties", solid.MassProperties));
        }

        #region Dimensions
        // main branching function for Dimension (AcDbDimension)
        private void
        Stream (ArrayList data, Dimension dim)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Dimension)));

            data.Add(new Snoop.Data.ObjectId("Dimension style", dim.DimensionStyle));
            data.Add(new Snoop.Data.String("Dimension text", dim.DimensionText));
            data.Add(new Snoop.Data.Point3d("Dimblk position", dim.DimBlockPosition));
            data.Add(new Snoop.Data.Distance("Elevation", dim.Elevation));
            data.Add(new Snoop.Data.Angle("Horiz. rotation", dim.HorizontalRotation));
            data.Add(new Snoop.Data.Distance("Measurement", dim.Measurement));
            data.Add(new Snoop.Data.Vector3d("Normal", dim.Normal));
            data.Add(new Snoop.Data.String("Text attachement", dim.TextAttachment.ToString()));
            data.Add(new Snoop.Data.Distance("Text line spacing factor", dim.TextLineSpacingFactor));
            data.Add(new Snoop.Data.String("Text line spacing style", dim.TextLineSpacingStyle.ToString()));
            data.Add(new Snoop.Data.Point3d("Normal", dim.TextPosition));
            data.Add(new Snoop.Data.Angle("Text rotation", dim.TextRotation));
            data.Add(new Snoop.Data.Bool("Using default text position", dim.UsingDefaultTextPosition));

            // dimvars
            data.Add(new Snoop.Data.Int("Dimadec", dim.Dimadec));
            data.Add(new Snoop.Data.Bool("Dimalt", dim.Dimalt));
            data.Add(new Snoop.Data.Int("Dimaltd", dim.Dimaltd));
            data.Add(new Snoop.Data.Distance("Dimaltf", dim.Dimaltf));
            data.Add(new Snoop.Data.Distance("Dimaltrnd", dim.Dimaltrnd));
            data.Add(new Snoop.Data.Int("Dimalttd", dim.Dimalttd));
            data.Add(new Snoop.Data.Int("Dimalttz", dim.Dimalttz));
            data.Add(new Snoop.Data.Int("Dimaltu", dim.Dimaltu));
            data.Add(new Snoop.Data.Int("Dimaltz", dim.Dimaltz));
            data.Add(new Snoop.Data.String("Dimapost", dim.Dimapost));
            data.Add(new Snoop.Data.Int("Dimarcsym", dim.Dimarcsym));
            data.Add(new Snoop.Data.Distance("Dimasz", dim.Dimasz));
            data.Add(new Snoop.Data.Int("Dimatfit", dim.Dimatfit));
            data.Add(new Snoop.Data.Int("Dimaunit", dim.Dimaunit));
            data.Add(new Snoop.Data.Int("Dimazin", dim.Dimazin));
            data.Add(new Snoop.Data.ObjectId("Dimblk", dim.Dimblk));
            data.Add(new Snoop.Data.ObjectId("Dimblk1", dim.Dimblk1));
            data.Add(new Snoop.Data.ObjectId("Dimblk2", dim.Dimblk2));
            data.Add(new Snoop.Data.ObjectId("Dimblk ID", dim.DimBlockId));
            data.Add(new Snoop.Data.ObjectId("Dimldrblk", dim.Dimldrblk));
            data.Add(new Snoop.Data.Distance("Dimcen", dim.Dimcen));
            data.Add(new Snoop.Data.ObjectToString("Dimclrd", dim.Dimclrd));
            data.Add(new Snoop.Data.ObjectToString("Dimclre", dim.Dimclre));
            data.Add(new Snoop.Data.ObjectToString("Dimclrt", dim.Dimclrt));
            data.Add(new Snoop.Data.Int("Dimdec", dim.Dimdec));
            data.Add(new Snoop.Data.Distance("Dimdle", dim.Dimdle));
            data.Add(new Snoop.Data.Distance("Dimdli", dim.Dimdli));
            data.Add(new Snoop.Data.String("Dimdsep", dim.Dimdsep.ToString()));
            data.Add(new Snoop.Data.Distance("Dimexe", dim.Dimexe));
            data.Add(new Snoop.Data.Distance("Dimexo", dim.Dimexo));
            data.Add(new Snoop.Data.Int("Dimfrac", dim.Dimfrac));
            data.Add(new Snoop.Data.Distance("Dimxlen", dim.Dimfxlen));
            data.Add(new Snoop.Data.Bool("Dimxlenon", dim.DimfxlenOn));
            data.Add(new Snoop.Data.Distance("Dimgap", dim.Dimgap));
            data.Add(new Snoop.Data.Angle("Dimjogang", dim.Dimjogang));
            data.Add(new Snoop.Data.Int("Dimjust", dim.Dimjust));
            data.Add(new Snoop.Data.Distance("Dimlfac", dim.Dimlfac));
            data.Add(new Snoop.Data.Bool("Dimlim", dim.Dimlim));
            data.Add(new Snoop.Data.ObjectId("Dimltex1", dim.Dimltex1));
            data.Add(new Snoop.Data.ObjectId("Dimltex2", dim.Dimltex2));
            data.Add(new Snoop.Data.ObjectId("Dimltype", dim.Dimltype));
            data.Add(new Snoop.Data.Int("Dimlunit", dim.Dimlunit));
            data.Add(new Snoop.Data.String("Dimlwd", dim.Dimlwd.ToString()));
            data.Add(new Snoop.Data.String("Dimlwe", dim.Dimlwe.ToString()));
            data.Add(new Snoop.Data.String("Dimpost", dim.Dimpost));
            data.Add(new Snoop.Data.Distance("Dimrnd", dim.Dimrnd));
            data.Add(new Snoop.Data.Bool("Dimsah", dim.Dimsah));
            data.Add(new Snoop.Data.Distance("Dimscale", dim.Dimscale));
            data.Add(new Snoop.Data.Bool("Dimsd1", dim.Dimsd1));
            data.Add(new Snoop.Data.Bool("Dimsd2", dim.Dimsd2));
            data.Add(new Snoop.Data.Bool("Dimse1", dim.Dimse1));
            data.Add(new Snoop.Data.Bool("Dimse2", dim.Dimse2));
            data.Add(new Snoop.Data.Bool("Dimsoxd", dim.Dimsoxd));
            data.Add(new Snoop.Data.Int("Dimtad", dim.Dimtad));
            data.Add(new Snoop.Data.Int("Dimtdec", dim.Dimtdec));
            data.Add(new Snoop.Data.Distance("Dimtfac", dim.Dimtfac));
            data.Add(new Snoop.Data.Int("Dimtfill", dim.Dimtfill));
            data.Add(new Snoop.Data.Object("Dimtfillclr", dim.Dimtfillclr));
            data.Add(new Snoop.Data.Bool("Dimtih", dim.Dimtih));
            data.Add(new Snoop.Data.Bool("Dimtix", dim.Dimtix));
            data.Add(new Snoop.Data.Distance("Dimtm", dim.Dimtm));
            data.Add(new Snoop.Data.Int("Dimtmove", dim.Dimtmove));
            data.Add(new Snoop.Data.Bool("Dimtofl", dim.Dimtofl));
            data.Add(new Snoop.Data.Bool("Dimtoh", dim.Dimtoh));
            data.Add(new Snoop.Data.Bool("Dimtol", dim.Dimtol));
            data.Add(new Snoop.Data.Int("Dimtolj", dim.Dimtolj));
            data.Add(new Snoop.Data.Distance("Dimtp", dim.Dimtp));
            data.Add(new Snoop.Data.Distance("Dimtsz", dim.Dimtsz));
            data.Add(new Snoop.Data.Distance("Dimtvp", dim.Dimtvp));
            //data.Add(new Snoop.Data.ObjectId("Dimtxsty", dim.Dimtxsty));      // TBD: Fix JMA
            data.Add(new Snoop.Data.Distance("Dimtxt", dim.Dimtxt));
            data.Add(new Snoop.Data.Int("Dimtzin", dim.Dimtzin));
            data.Add(new Snoop.Data.Bool("Dimupt", dim.Dimupt));
            data.Add(new Snoop.Data.Int("Dimzin", dim.Dimzin));

            AlignedDimension adim = dim as AlignedDimension;
            if (adim != null) {
                Stream(data, adim);
                return;
            }

            ArcDimension arcdim = dim as ArcDimension;
            if (arcdim != null) {
                Stream(data, arcdim);
                return;
            }

            DiametricDimension ddim = dim as DiametricDimension;
            if (ddim != null) {
                Stream(data, ddim);
                return;
            }

            LineAngularDimension2 ldim = dim as LineAngularDimension2;
            if (ldim != null) {
                Stream(data, ldim);
                return;
            }

            OrdinateDimension odim = dim as OrdinateDimension;
            if (odim != null) {
                Stream(data, odim);
                return;
            }

            Point3AngularDimension pdim = dim as Point3AngularDimension;
            if (ldim != null) {
                Stream(data, pdim);
                return;
            }

            RadialDimension rdim = dim as RadialDimension;
            if (rdim != null) {
                Stream(data, rdim);
                return;
            }

            RadialDimensionLarge rdimLg = dim as RadialDimensionLarge;
            if (rdimLg != null) {
                Stream(data, rdimLg);
                return;
            }

            RotatedDimension rtdim = dim as RotatedDimension;
            if (rtdim != null) {
                Stream(data, rtdim);
                return;
            }
        }

        private void
        Stream (ArrayList data, AlignedDimension dim)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AlignedDimension)));

            data.Add(new Snoop.Data.Point3d("Dim line point", dim.DimLinePoint));
            data.Add(new Snoop.Data.Angle("Oblique", dim.Oblique));
            data.Add(new Snoop.Data.Point3d("XLine1 point", dim.XLine1Point));
            data.Add(new Snoop.Data.Point3d("XLine2 point", dim.XLine2Point));
        }

        private void
        Stream (ArrayList data, ArcDimension dim)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ArcDimension)));

            data.Add(new Snoop.Data.Double("Arc start param", dim.ArcStartParam));
            data.Add(new Snoop.Data.Double("Arc end param", dim.ArcEndParam));
            data.Add(new Snoop.Data.Point3d("Arc point", dim.ArcPoint));
            data.Add(new Snoop.Data.Point3d("Center point", dim.CenterPoint));
            data.Add(new Snoop.Data.Int("ArcSymbolType", dim.ArcSymbolType));
            data.Add(new Snoop.Data.Bool("Has leader", dim.HasLeader));
            data.Add(new Snoop.Data.Bool("Is partial", dim.IsPartial));
            data.Add(new Snoop.Data.Point3d("Leader1 point", dim.Leader1Point));
            data.Add(new Snoop.Data.Point3d("Leader2 point", dim.Leader2Point));
            data.Add(new Snoop.Data.Point3d("XLine1 point", dim.XLine1Point));
            data.Add(new Snoop.Data.Point3d("XLine2 point", dim.XLine2Point));
        }

        private void
        Stream (ArrayList data, DiametricDimension dim)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DiametricDimension)));

            data.Add(new Snoop.Data.Point3d("Chord point", dim.ChordPoint));
            data.Add(new Snoop.Data.Point3d("Far chord point", dim.FarChordPoint));
            data.Add(new Snoop.Data.Distance("Leader length", dim.LeaderLength));
        }

        private void
        Stream (ArrayList data, LineAngularDimension2 dim)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LineAngularDimension2)));

            data.Add(new Snoop.Data.Point3d("Arc point", dim.ArcPoint));
            data.Add(new Snoop.Data.Point3d("XLine1 start", dim.XLine1Start));
            data.Add(new Snoop.Data.Point3d("XLine1 end", dim.XLine1End));
            data.Add(new Snoop.Data.Point3d("XLine2 start", dim.XLine2Start));
            data.Add(new Snoop.Data.Point3d("XLine2 end", dim.XLine2End));
        }

        private void
        Stream (ArrayList data, OrdinateDimension dim)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(OrdinateDimension)));

            data.Add(new Snoop.Data.Point3d("Defining point", dim.DefiningPoint));
            data.Add(new Snoop.Data.Point3d("Leader end point", dim.LeaderEndPoint));
            data.Add(new Snoop.Data.Point3d("Origin", dim.Origin));
            data.Add(new Snoop.Data.Bool("Using X axis", dim.UsingXAxis));
            data.Add(new Snoop.Data.Bool("Using Y axis", dim.UsingYAxis));
        }

        private void
        Stream (ArrayList data, Point3AngularDimension dim)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Point3AngularDimension)));

            data.Add(new Snoop.Data.Point3d("Arc point", dim.ArcPoint));
            data.Add(new Snoop.Data.Point3d("Center point", dim.CenterPoint));
            data.Add(new Snoop.Data.Point3d("XLine1 point", dim.XLine1Point));
            data.Add(new Snoop.Data.Point3d("XLine2 point", dim.XLine2Point));
        }

        private void
        Stream (ArrayList data, RadialDimension dim)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RadialDimension)));

            data.Add(new Snoop.Data.Point3d("Center", dim.Center));
            data.Add(new Snoop.Data.Point3d("Chord point", dim.ChordPoint));
            data.Add(new Snoop.Data.Distance("Leader length", dim.LeaderLength));
        }

        private void
        Stream (ArrayList data, RadialDimensionLarge dim)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RadialDimensionLarge)));

            data.Add(new Snoop.Data.Point3d("Center", dim.Center));
            data.Add(new Snoop.Data.Point3d("Chord point", dim.ChordPoint));
            data.Add(new Snoop.Data.Angle("Jog angle", dim.JogAngle));
            data.Add(new Snoop.Data.Point3d("Jog point", dim.JogPoint));
            data.Add(new Snoop.Data.Point3d("Override center", dim.OverrideCenter));
        }

        private void
        Stream (ArrayList data, RotatedDimension dim)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RotatedDimension)));

            data.Add(new Snoop.Data.Point3d("Dim line point", dim.DimLinePoint));
            data.Add(new Snoop.Data.Angle("Oblique", dim.Oblique));
            data.Add(new Snoop.Data.Angle("Rotation", dim.Rotation));
            data.Add(new Snoop.Data.Point3d("XLine1 point", dim.XLine1Point));
            data.Add(new Snoop.Data.Point3d("XLine2 point", dim.XLine2Point));
        }

        #endregion Dimensions

        private void
        Stream (ArrayList data, Hatch hatch)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Hatch)));

            data.Add(new Snoop.Data.String("Hatch object type", hatch.HatchObjectType.ToString()));
            data.Add(new Snoop.Data.String("Hatch style", hatch.HatchStyle.ToString()));
            data.Add(new Snoop.Data.Bool("Associative", hatch.Associative));
            data.Add(new Snoop.Data.Distance("Elevation", hatch.Elevation));
            data.Add(new Snoop.Data.Vector3d("Normal", hatch.Normal));
            data.Add(new Snoop.Data.Bool("Is hatch", hatch.IsHatch));
            data.Add(new Snoop.Data.Bool("Is solid fill", hatch.IsSolidFill));
            data.Add(new Snoop.Data.String("Pattern type", hatch.HatchStyle.ToString()));
            data.Add(new Snoop.Data.String("Pattern name", hatch.PatternName));
            data.Add(new Snoop.Data.Angle("Pattern angle", hatch.PatternAngle));
            data.Add(new Snoop.Data.Distance("Pattern scale", hatch.PatternScale));
            data.Add(new Snoop.Data.Bool("Pattern double", hatch.PatternDouble));
            data.Add(new Snoop.Data.Distance("Pattern space", hatch.PatternSpace));
            data.Add(new Snoop.Data.Bool("Is gradient", hatch.IsGradient));
            data.Add(new Snoop.Data.String("Gradient type", hatch.GradientType.ToString()));
            data.Add(new Snoop.Data.String("Gradient name", hatch.GradientName));
            data.Add(new Snoop.Data.Angle("Gradient angle", hatch.GradientAngle));
            data.Add(new Snoop.Data.Bool("Graident one color mode", hatch.GradientOneColorMode));
            data.Add(new Snoop.Data.Distance("Gradient shift", hatch.GradientShift));
            data.Add(new Snoop.Data.Double("Shade tint value", hatch.ShadeTintValue));

            // only makes sense when not a Gradient.
            if (hatch.HatchObjectType == HatchObjectType.HatchObject) {
                data.Add(new Snoop.Data.Int("Number of hatch lines", hatch.NumberOfHatchLines));
                data.Add(new Snoop.Data.ObjectCollection("Hatch lines data", (System.Collections.ICollection)hatch.GetHatchLinesData()));
            }

            data.Add(new Snoop.Data.Int("Number of loops", hatch.NumberOfLoops));

            // TBD: thows exception even though it reports a loop! Only works if
            // HatchLoopType is Polyline
            /*try {
                ArrayList hatchLoops = new ArrayList();
                for (int i=0; i<hatch.NumberOfLoops; i++)
                    hatchLoops.Add(hatch.GetLoopAt(i));
                data.Add(new Snoop.Data.ObjectCollection("Hatch loops", hatchLoops));
            }
            catch (Autodesk.AutoCAD.Runtime.Exception e) {
                if (e.ErrorStatus == (int)Autodesk.AutoCAD.Runtime.ErrorStatus.NotApplicable)
                    data.Add(new Snoop.Data.String("Hatch loops", Autodesk.AutoCAD.Runtime.ErrorStatus.NotApplicable.ToString()));
                else
                    throw e;
            }*/

            data.Add(new Snoop.Data.Int("Number of pattern defs", hatch.NumberOfPatternDefinitions));
            ArrayList patternDefs = new ArrayList();
            for (int i = 0; i < hatch.NumberOfPatternDefinitions; i++)
                patternDefs.Add(hatch.GetPatternDefinitionAt(i));
            data.Add(new Snoop.Data.ObjectCollection("Pattern defs", patternDefs));

            data.Add(new Snoop.Data.ObjectIdCollection("Associated Objects", hatch.GetAssociatedObjectIds()));
        }

        private void
        Stream(ArrayList data, Autodesk.AutoCAD.EditorInput.Camera camera)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.EditorInput.Camera)));

            data.Add(new Snoop.Data.Double("Back clip distance", camera.BackClipDistance));
            data.Add(new Snoop.Data.Bool("Back clip enabled", camera.BackClipEnabled));
            data.Add(new Snoop.Data.Double("Field of view", camera.FieldOfView));
            data.Add(new Snoop.Data.Double("Front clip distance", camera.FrontClipDistance));
            data.Add(new Snoop.Data.Bool("Front clip enabled", camera.FrontClipEnabled));
            data.Add(new Snoop.Data.Bool("Is camera plottable", camera.IsCameraPlottable));
            data.Add(new Snoop.Data.Double("Lens length", camera.LensLength));
            data.Add(new Snoop.Data.Point3d("Position", camera.Position));
            data.Add(new Snoop.Data.Point3d("Target", camera.Target));
            data.Add(new Snoop.Data.ObjectId("View Id", camera.ViewId));
            data.Add(new Snoop.Data.Double("View twist", camera.ViewTwist));       
        }


        // main branch for anything derived from DBText (AcDbText)
        private void
        Stream (ArrayList data, DBText text)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DBText)));

            data.Add(new Snoop.Data.String("Text string", text.TextString));
            //data.Add(new Snoop.Data.ObjectId("Text style", text.TextStyle));      // TBD: Fix JMA
            data.Add(new Snoop.Data.Bool("Is default alignment", text.IsDefaultAlignment));
            data.Add(new Snoop.Data.Point3d("Alignment point", text.AlignmentPoint));
            data.Add(new Snoop.Data.Point3d("Position", text.Position));
            data.Add(new Snoop.Data.Distance("Height", text.Height));
            data.Add(new Snoop.Data.Distance("Width factor", text.WidthFactor));
            data.Add(new Snoop.Data.Angle("Oblique", text.Oblique));
            data.Add(new Snoop.Data.Angle("Rotation", text.Rotation));
            data.Add(new Snoop.Data.String("Horizontal mode", text.HorizontalMode.ToString()));
            data.Add(new Snoop.Data.String("Vertical mode", text.VerticalMode.ToString()));
            data.Add(new Snoop.Data.Bool("Is mirrored in X", text.IsMirroredInX));
            data.Add(new Snoop.Data.Bool("Is mirrored in Y", text.IsMirroredInY));
            data.Add(new Snoop.Data.Distance("Thickness", text.Thickness));
            data.Add(new Snoop.Data.Vector3d("Normal", text.Normal));

            // branch to all known major sub-classes
            AttributeDefinition attdef = text as AttributeDefinition;
            if (attdef != null) {
                Stream(data, attdef);
                return;
            }

            AttributeReference attref = text as AttributeReference;
            if (attref != null) {
                Stream(data, attref);
                return;
            }
        }

        private void
        Stream (ArrayList data, PolygonMesh mesh)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PolygonMesh)));

            data.Add(new Snoop.Data.String("Poly mesh type", mesh.PolyMeshType.ToString()));
            data.Add(new Snoop.Data.Bool("Is M closed", mesh.IsMClosed));
            data.Add(new Snoop.Data.Int("M size", mesh.MSize));
            data.Add(new Snoop.Data.Int("M surface density", mesh.MSurfaceDensity));
            data.Add(new Snoop.Data.Bool("Is N closed", mesh.IsNClosed));
            data.Add(new Snoop.Data.Int("N size", mesh.NSize));
            data.Add(new Snoop.Data.Int("N surface density", mesh.NSurfaceDensity));

            ObjectIdCollection vertList = new ObjectIdCollection();
            System.Collections.IEnumerator iter = mesh.GetEnumerator();
            while (iter.MoveNext())
                vertList.Add((ObjectId)iter.Current);

            data.Add(new Snoop.Data.ObjectIdCollection("Vertices", vertList));
        }

        private void
        Stream (ArrayList data, PolyFaceMesh mesh)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PolyFaceMesh)));

            data.Add(new Snoop.Data.Int("Number of faces", mesh.NumFaces));
            data.Add(new Snoop.Data.Int("Number of vertices", mesh.NumVertices));

            ObjectIdCollection faceList = new ObjectIdCollection();
            System.Collections.IEnumerator iter = mesh.GetEnumerator();
            while (iter.MoveNext())
                faceList.Add((ObjectId)iter.Current);

            data.Add(new Snoop.Data.ObjectIdCollection("Faces and Vertices", faceList));
        }

        private void
        Stream (ArrayList data, Vertex vert)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Vertex)));

            PolygonMeshVertex pmeshVert = vert as PolygonMeshVertex;
            if (pmeshVert != null) {
                Stream(data, pmeshVert);
                return;
            }

            PolyFaceMeshVertex pfaceVert = vert as PolyFaceMeshVertex;
            if (pfaceVert != null) {
                Stream(data, pfaceVert);
                return;
            }

            FaceRecord face = vert as FaceRecord;
            if (face != null) {
                Stream(data, face);
                return;
            }

            Vertex2d vert2d = vert as Vertex2d;
            if (vert2d != null) {
                Stream(data, vert2d);
                return;
            }

            PolylineVertex3d vert3d = vert as PolylineVertex3d;
            if (vert3d != null) {
                Stream(data, vert3d);
                return;
            }
        }

        private void
        Stream (ArrayList data, PolygonMeshVertex vert)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PolygonMeshVertex)));

            data.Add(new Snoop.Data.String("Vertex type", vert.VertexType.ToString()));
            data.Add(new Snoop.Data.Point3d("Position", vert.Position));
        }

        private void
        Stream (ArrayList data, PolyFaceMeshVertex vert)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PolyFaceMeshVertex)));

            data.Add(new Snoop.Data.Point3d("Position", vert.Position));
        }

        private void
        Stream (ArrayList data, FaceRecord face)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FaceRecord)));

            // Faces are either 3 or 4 points.  Will return 0 if not applicable
            int numPts = (face.GetVertexAt(3) == 0) ? 3 : 4;

            for (short i = 0; i < numPts; i++) {
                data.Add(new Snoop.Data.Int(string.Format("Vertex index #{0:g}", i + 1), face.GetVertexAt(i)));
            }
            for (short i = 0; i < numPts; i++) {
                data.Add(new Snoop.Data.Bool(string.Format("Edge #{0:g}", i + 1), face.IsEdgeVisibleAt(i)));
            }
        }

        private void
        Stream (ArrayList data, Vertex2d vert)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Vertex2d)));

            data.Add(new Snoop.Data.String("Vertex type", vert.VertexType.ToString()));
            data.Add(new Snoop.Data.Point3d("Position", vert.Position));
            data.Add(new Snoop.Data.Distance("Start width", vert.StartWidth));
            data.Add(new Snoop.Data.Distance("End width", vert.EndWidth));
            data.Add(new Snoop.Data.Double("Bulge", vert.Bulge));
            data.Add(new Snoop.Data.Bool("Tangent used", vert.TangentUsed));
            data.Add(new Snoop.Data.Double("Tangent", vert.Tangent));
        }

        private void
        Stream (ArrayList data, PolylineVertex3d vert)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PolylineVertex3d)));

            data.Add(new Snoop.Data.String("Vertex type", vert.VertexType.ToString()));
            data.Add(new Snoop.Data.Point3d("Position", vert.Position));
        }

        private void
        Stream (ArrayList data, Section sec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Section)));

            data.Add(new Snoop.Data.Object("Indicator fill color", sec.IndicatorFillColor));
            data.Add(new Snoop.Data.Int("Indicator transparency", sec.IndicatorTransparency));
            data.Add(new Snoop.Data.Bool("Is live section enabled", sec.IsLiveSectionEnabled));
            data.Add(new Snoop.Data.String("Name", sec.Name));
            data.Add(new Snoop.Data.Vector3d("Normal", sec.Normal));
            data.Add(new Snoop.Data.ObjectId("Settings ID", sec.Settings));
            data.Add(new Snoop.Data.String("State", sec.State.ToString()));
            data.Add(new Snoop.Data.Vector3d("Vertical direction", sec.VerticalDirection));
            data.Add(new Snoop.Data.Vector3d("Viewing direction", sec.ViewingDirection));

            /*data.Add(new Snoop.Data.Int("Number of vertices", sec.NumVertices));
            int len = sec.NumVertices;
            for (int i = 0; i < len; i++) {
                data.Add(new Snoop.Data.Point3d(string.Format("Vertex [{0}]", i), sec.GetVertex(i)));
            }*/  // TBD: Fix JMA
        }

        private void
        Stream (ArrayList data, Region reg)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Region)));

            data.Add(new Snoop.Data.Double("Area", reg.Area));
            data.Add(new Snoop.Data.Bool("Is null", reg.IsNull));
            data.Add(new Snoop.Data.Vector3d("Normal", reg.Normal));
            data.Add(new Snoop.Data.Int("Number of changes", reg.NumChanges));
            data.Add(new Snoop.Data.Double("Perimeter", reg.Perimeter));
        }

        private void
        Stream (ArrayList data, SequenceEnd seqEnd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SequenceEnd)));
        }

        private void
        Stream (ArrayList data, UnderlayReference ulayRef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(UnderlayReference)));

            data.Add(new Snoop.Data.Int("Contrast", ulayRef.Contrast));
            data.Add(new Snoop.Data.ObjectId("Definition id", ulayRef.DefinitionId));
            data.Add(new Snoop.Data.Int("Fade", ulayRef.Fade));
            data.Add(new Snoop.Data.Bool("Is clipped", ulayRef.IsClipped));
            data.Add(new Snoop.Data.Bool("Is on", ulayRef.IsOn));
            data.Add(new Snoop.Data.Bool("Monochrome", ulayRef.Monochrome));
            data.Add(new Snoop.Data.Vector3d("Normal", ulayRef.Normal));
            data.Add(new Snoop.Data.Point3d("Position", ulayRef.Position));
            data.Add(new Snoop.Data.Double("Rotation", ulayRef.Rotation));

            data.Add(new Snoop.Data.CategorySeparator("Static Members"));
            data.Add(new Snoop.Data.Int("Contrast lower limit", UnderlayReference.ContrastLowerLimit));
            data.Add(new Snoop.Data.Int("Contrast upper limit", UnderlayReference.ContrastUpperLimit));
            data.Add(new Snoop.Data.Int("Default contrast", UnderlayReference.DefaultContrast));
            data.Add(new Snoop.Data.Int("Default fade", UnderlayReference.DefaultFade));
            data.Add(new Snoop.Data.Int("Fade lower limit", UnderlayReference.FadeLowerLimit));
            data.Add(new Snoop.Data.Int("Fade upper limit", UnderlayReference.FadeUpperLimit));


            DgnReference dgnRef = ulayRef as DgnReference;
            if (dgnRef != null) {
                Stream(data, dgnRef);
                return;
            }

            DwfReference dwfRef = ulayRef as DwfReference;
            if (dwfRef != null) {
                Stream(data, dwfRef);
                return;
            }                                     
        }

        private void
        Stream (ArrayList data, DgnReference dgnRef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DgnReference)));
        }

        private void
        Stream (ArrayList data, DwfReference dwfRef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DwfReference)));
        }

        private void
        Stream (ArrayList data, Light light)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Light)));

            data.Add(new Snoop.Data.String("Name", light.Name));
            data.Add(new Snoop.Data.Vector3d("Direction", light.Direction));
            data.Add(new Snoop.Data.Angle("Falloff angle", light.FalloffAngle));
            data.Add(new Snoop.Data.Angle("Hotspot angle", light.HotspotAngle));
            data.Add(new Snoop.Data.Double("Intensity", light.Intensity));
            data.Add(new Snoop.Data.Object("Light color", light.LightColor));
            data.Add(new Snoop.Data.Object("Light type", light.LightType));
            data.Add(new Snoop.Data.Point3d("Position", light.Position));
            data.Add(new Snoop.Data.Point3d("Target location", light.TargetLocation));
            data.Add(new Snoop.Data.Object("Attenuation", light.Attenuation));
            data.Add(new Snoop.Data.Bool("Is on", light.IsOn));
            data.Add(new Snoop.Data.Bool("Is plottable", light.IsPlottable));
            data.Add(new Snoop.Data.Object("Shadow", light.Shadow));

        }

        #endregion  ///Entity


        private void
        Stream (ArrayList data, AttributeReference attref)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AttributeReference)));

            data.Add(new Snoop.Data.Bool("Is constant", attref.IsConstant));
            data.Add(new Snoop.Data.Bool("Invisible", attref.Invisible));
            data.Add(new Snoop.Data.Bool("Is preset", attref.IsPreset));
            data.Add(new Snoop.Data.Bool("Is verifiable", attref.IsVerifiable));
            data.Add(new Snoop.Data.String("Tag", attref.Tag));
            data.Add(new Snoop.Data.Int("Field length", attref.FieldLength));
            data.Add(new Snoop.Data.Bool("Lock position in block", attref.LockPositionInBlock));
        }

        private void
        Stream (ArrayList data, AttributeDefinition attDef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AttributeDefinition)));

            data.Add(new Snoop.Data.Bool("Is constant", attDef.Constant));
            data.Add(new Snoop.Data.Bool("Invisible", attDef.Invisible));
            data.Add(new Snoop.Data.Bool("Is preset", attDef.Preset));
            data.Add(new Snoop.Data.Bool("Is verifiable", attDef.Verifiable));
            data.Add(new Snoop.Data.String("Prompt", attDef.Prompt));
            data.Add(new Snoop.Data.String("Tag", attDef.Tag));
            data.Add(new Snoop.Data.Int("Field length", attDef.FieldLength));
            data.Add(new Snoop.Data.Bool("Lock position in block", attDef.LockPositionInBlock));
        }

        private void
        Stream (ArrayList data, Table tbl)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Table)));

            data.Add(new Snoop.Data.ObjectId("Table style", tbl.TableStyle));
            data.Add(new Snoop.Data.Vector3d("Direction", tbl.Direction));
            data.Add(new Snoop.Data.String("Flow direction", tbl.FlowDirection.ToString()));
            data.Add(new Snoop.Data.Distance("Width", tbl.Width));
            data.Add(new Snoop.Data.Distance("Height", tbl.Height));
            data.Add(new Snoop.Data.Double("Minimum table width", tbl.MinimumTableWidth));
            data.Add(new Snoop.Data.Double("Minimum table height", tbl.MinimumTableHeight));
            //data.Add(new Snoop.Data.Distance("Horizontal cell margin", tbl.HorizontalCellMargin));    // TBD: Fix JMA
            //data.Add(new Snoop.Data.Distance("Vertical cell margin", tbl.VerticalCellMargin));        // TBD: Fix JMA
            //data.Add(new Snoop.Data.Bool("Is header suppressed", tbl.IsHeaderSuppressed));            // TBD: Fix JMA
            //data.Add(new Snoop.Data.Bool("Is title suppressed", tbl.IsTitleSuppressed));              // TBD: Fix JMA
            //data.Add(new Snoop.Data.Int("Number of columns", tbl.NumColumns));                        // TBD: Fix JMA
            //data.Add(new Snoop.Data.Int("Number of rows", tbl.NumRows));                              // TBD: Fix JMA
            data.Add(new Snoop.Data.Bool("Has sub selection", tbl.HasSubSelection));
            if (tbl.HasSubSelection)
                data.Add(new Snoop.Data.Object("Sub selection", tbl.SubSelection));

            // TBD: more of the non-property functions to show?
        }


        /// <summary>
        /// needs to be revisited: The managed wrapper for AcDbSurface class is missing an object factory 
        /// (protocol extension that maps rx class to managed class).  
        /// arj 02/03/06
        /// </summary>
        /// <param name="surf"></param>

        private void
        Stream (ArrayList data, Autodesk.AutoCAD.DatabaseServices.Surface surf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.DatabaseServices.Surface)));

            data.Add(new Snoop.Data.Int("U isoline density", surf.UIsoLineDensity));
            data.Add(new Snoop.Data.Int("V isoline density", surf.VIsoLineDensity));


            ExtrudedSurface extrSurf = surf as ExtrudedSurface;
            if (extrSurf != null) {
                Stream(data, extrSurf);
                return;
            }

            LoftedSurface loftSurf = surf as LoftedSurface;
            if (loftSurf != null) {
                Stream(data, loftSurf);
                return;
            }

            PlaneSurface planeSurf = surf as PlaneSurface;
            if (planeSurf != null) {
                Stream(data, planeSurf);
                return;
            }

            RevolvedSurface revSurf = surf as RevolvedSurface;
            if (revSurf != null) {
                Stream(data, revSurf);
                return;
            }

            SweptSurface sweptSurf = surf as SweptSurface;
            if (sweptSurf != null) {
                Stream(data, sweptSurf);
                return;
            }
        }

        private void
        Stream (ArrayList data, ExtrudedSurface extrSurf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ExtrudedSurface)));

            data.Add(new Snoop.Data.Double("Height", extrSurf.Height));
            data.Add(new Snoop.Data.Object("Sweep entity", extrSurf.SweepEntity));
            data.Add(new Snoop.Data.Object("Sweep options", extrSurf.SweepOptions));
            data.Add(new Snoop.Data.Vector3d("Sweep vector", extrSurf.SweepVec));
        }

        private void
        Stream (ArrayList data, LoftedSurface loftSurf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LoftedSurface)));

            data.Add(new Snoop.Data.ObjectCollection("Cross sections", loftSurf.CrossSections));
            data.Add(new Snoop.Data.ObjectCollection("Guide curves", loftSurf.GuideCurves));
            data.Add(new Snoop.Data.Object("Loft options", loftSurf.LoftOptions));
            data.Add(new Snoop.Data.Object("Path entity", loftSurf.PathEntity));
        }

        private void
        Stream (ArrayList data, PlaneSurface planeSurf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlaneSurface)));
        }

        private void
        Stream (ArrayList data, RevolvedSurface revSurf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RevolvedSurface)));

            data.Add(new Snoop.Data.Vector3d("Axis direction", revSurf.AxisDirection));
            data.Add(new Snoop.Data.Point3d("Axis point", revSurf.AxisPoint));
            data.Add(new Snoop.Data.Angle("Revolve angle", revSurf.RevolveAngle));
            data.Add(new Snoop.Data.Object("Revolve entity", revSurf.RevolveEntity));
            data.Add(new Snoop.Data.Object("Revolve options", revSurf.RevolveOptions));
            data.Add(new Snoop.Data.Double("Start angle", revSurf.StartAngle));
        }

        private void
        Stream (ArrayList data, SweptSurface sweptSurf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SweptSurface)));

            data.Add(new Snoop.Data.Distance("Path length", sweptSurf.PathLength));
            data.Add(new Snoop.Data.Object("Path entity", sweptSurf.PathEntity));
            data.Add(new Snoop.Data.Object("Sweep entity", sweptSurf.SweepEntity));
            data.Add(new Snoop.Data.Object("Sweep options", sweptSurf.SweepOptions));
        }

        private void
        Stream (ArrayList data, Image img)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Image)));

            // no data at this level

            RasterImage rasterImg = img as RasterImage;
            if (rasterImg != null) {
                Stream(data, rasterImg);
                return;
            }
        }

        private void
        Stream (ArrayList data, RasterImage rasterImg)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RasterImage)));

            data.Add(new Snoop.Data.ObjectId("Image def ID", rasterImg.ImageDefId));
            data.Add(new Snoop.Data.Double("Brightness", rasterImg.Brightness));
            data.Add(new Snoop.Data.String("Clip boundary type", rasterImg.ClipBoundaryType.ToString()));
            data.Add(new Snoop.Data.Int("Contrast", rasterImg.Contrast));
            data.Add(new Snoop.Data.String("Display options", rasterImg.DisplayOptions.ToString()));
            data.Add(new Snoop.Data.Int("Fade", rasterImg.Fade));
            data.Add(new Snoop.Data.Bool("Is clipped", rasterImg.IsClipped));
            data.Add(new Snoop.Data.Object("Orientation", rasterImg.Orientation));
            data.Add(new Snoop.Data.Object("Pixel to model transform", rasterImg.PixelToModelTransform));
            data.Add(new Snoop.Data.ObjectId("Reactor ID", rasterImg.ReactorId));
            data.Add(new Snoop.Data.Vector2d("Scale", rasterImg.Scale));

            Wipeout wipeout = rasterImg as Wipeout;
            if (wipeout != null) {
                Stream(data, wipeout);
                return;
            }
        }

        private void
        Stream(ArrayList data, Wipeout wipeout)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Wipeout)));

            data.Add(new Snoop.Data.Bool("Has frame", wipeout.HasFrame));            
        }

        private void
        Stream (ArrayList data, Shape shp)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Shape)));

            data.Add(new Snoop.Data.ObjectId("Style ID", shp.StyleId));
            data.Add(new Snoop.Data.String("Name", shp.Name));
            data.Add(new Snoop.Data.Vector3d("Normal", shp.Normal));
            data.Add(new Snoop.Data.Angle("Oblique", shp.Oblique));
            data.Add(new Snoop.Data.Point3d("Position", shp.Position));
            data.Add(new Snoop.Data.Angle("Rotation", shp.Rotation));
            data.Add(new Snoop.Data.ObjectId("Shape index", shp.ShapeIndex));
            data.Add(new Snoop.Data.Int("Shape number", shp.ShapeNumber));
            data.Add(new Snoop.Data.Distance("Size", shp.Size));
            data.Add(new Snoop.Data.Distance("Thickness", shp.Thickness));
            data.Add(new Snoop.Data.Distance("Width factor", shp.WidthFactor));
        }
    }
}

