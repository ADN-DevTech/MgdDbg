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
using Autodesk.AutoCAD.DatabaseServices.Filters;

using MgdDbg.Snoop.Collectors;

using AcDb = Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.Snoop.CollectorExts {

    /// <summary>
    /// This is a Snoop Collector Extension object to collect data from DBObjects objects.
    /// </summary>

    public class DbObject : CollectorExt {

        public
        DbObject()
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

            // branch to all types we are concerned with
            AcDb.DBObject obj = e.ObjToSnoop as AcDb.DBObject;
            if (obj != null) {
                Stream(snoopCollector.Data(), obj);
                return;
            }
        }

        //  main branch for anything derived from DBObject
        private void
        Stream(ArrayList data, Autodesk.AutoCAD.DatabaseServices.DBObject dbObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.DatabaseServices.DBObject)));

            //data.Add(new Snoop.Data.String("ID", dbObj.Id.ToString()));           // TBD: Why ID and ObjectId ?
            data.Add(new Snoop.Data.String("Object ID", dbObj.ObjectId.ToString()));
            data.Add(new Snoop.Data.String("Handle", dbObj.Handle.ToString()));
            data.Add(new Snoop.Data.String("Class ID", dbObj.ClassID.ToString()));
            //data.Add(new Snoop.Data.ObjectUnknown("ActiveX object", dbObj.AcadObject));
            data.Add(new Snoop.Data.Database("Database", dbObj.Database));
            data.Add(new Snoop.Data.ObjectId("Owner ID", dbObj.OwnerId));
            data.Add(new Snoop.Data.Bool("Has save version override", dbObj.HasSaveVersionOverride));
            data.Add(new Snoop.Data.String("Object birth version", dbObj.ObjectBirthVersion.ToString()));
            data.Add(new Snoop.Data.Bool("Is a proxy", dbObj.IsAProxy));
            data.Add(new Snoop.Data.String("Merge style", dbObj.MergeStyle.ToString()));
            data.Add(new Snoop.Data.ExtDictId("Extension dictionary", dbObj.ExtensionDictionary));
            data.Add(new Snoop.Data.Object("Xdata", dbObj.XData));
            data.Add(new Snoop.Data.References("References to", dbObj.ObjectId));
            data.Add(new Snoop.Data.ReferencedBy("Referenced by", dbObj.ObjectId));
            //data.Add(new Snoop.Data.Object("Drawable", dbObj.Drawable));    // works but not interesting (just casts back to self)
            //data.Add(new Snoop.Data.Object("Undo filer", dbObj.UndoFiler)); // works but always null for our cases

            data.Add(new Snoop.Data.Bool("Has fields", dbObj.HasFields));

            //data.Add(new Snoop.Data.ObjectCollection("Transient reactors", dbObj.GetReactors()));
            data.Add(new Snoop.Data.ObjectIdCollection("Persistent reactor IDs", dbObj.GetPersistentReactorIds()));

            // short-circuit if this is an Entity derived class
            AcDb.Entity ent = dbObj as AcDb.Entity;
            if (ent != null)
                return;


            DataTable dTable = dbObj as DataTable;
            if (dTable != null) {
                Stream(data, dTable);
                return;
            }

            DBDictionary dbDict = dbObj as DBDictionary;
            if (dbDict != null) {
                Stream(data, dbDict);
                return;
            }

            Autodesk.AutoCAD.DatabaseServices.Filters.Filter filter = dbObj as Autodesk.AutoCAD.DatabaseServices.Filters.Filter;
            if (filter != null) {
                Stream(data, filter);
                return;
            }

            Group group = dbObj as Group;
            if (group != null) {
                Stream(data, group);
                return;
            }

            Autodesk.AutoCAD.DatabaseServices.Filters.Index index = dbObj as Autodesk.AutoCAD.DatabaseServices.Filters.Index;
            if (index != null) {
                Stream(data, index);
                return;
            }

            PlotSettings ps = dbObj as PlotSettings;
            if (ps != null) {
                Stream(data, ps);
                return;
            }

            MlineStyle mlineStyle = dbObj as MlineStyle;
            if (mlineStyle != null) {
                Stream(data, mlineStyle);
                return;
            }

            PlaceHolder placeHolder = dbObj as PlaceHolder;
            if (placeHolder != null) {
                Stream(data, placeHolder);
                return;
            }

            ProxyObject proxyObj = dbObj as ProxyObject;
            if (proxyObj != null) {
                Stream(data, proxyObj);
                return;
            }

            DrawOrderTable drawOrderTbl = dbObj as DrawOrderTable;
            if (drawOrderTbl != null) {
                Stream(data, drawOrderTbl);
                return;
            }

            Xrecord xRec = dbObj as Xrecord;
            if (xRec != null) {
                Stream(data, xRec);
                return;
            }

            RasterImageDef rasterImgDef = dbObj as RasterImageDef;
            if (rasterImgDef != null) {
                Stream(data, rasterImgDef);
                return;
            }

            RasterVariables rasterVars = dbObj as RasterVariables;
            if (rasterVars != null) {
                Stream(data, rasterVars);
                return;
            }

            TableStyle tabStyle = dbObj as TableStyle;
            if (tabStyle != null) {
                Stream(data, tabStyle);
                return;
            }

            SectionSettings secSettings = dbObj as SectionSettings;
            if (secSettings != null) {
                Stream(data, secSettings);
                return;
            }

            SectionManager secMgr = dbObj as SectionManager;
            if (secMgr != null) {
                Stream(data, secMgr);
                return;
            }

            UnderlayDefinition ulayDef = dbObj as UnderlayDefinition;
            if (ulayDef != null) {
                Stream(data, ulayDef);
                return;
            }

            Background backGrnd = dbObj as Background;
            if (backGrnd != null) {
                Stream(data, backGrnd);
                return;
            }

            DBVisualStyle dbVisStyle = dbObj as DBVisualStyle;
            if (dbVisStyle != null) {
                Stream(data, dbVisStyle);
                return;
            }

            Sun sun = dbObj as Sun;
            if (sun != null) {
                Stream(data, sun);
                return;
            }

            RenderSettings rndrSetngs = dbObj as RenderSettings;
            if (rndrSetngs != null) {
                Stream(data, rndrSetngs);
                return;
            }

            RenderEnvironment rndrenv = dbObj as RenderEnvironment;
            if (rndrenv != null) {
                Stream(data, rndrenv);
                return;
            }

            RenderGlobal rndrGlobal = dbObj as RenderGlobal;
            if (rndrGlobal != null) {
                Stream(data, rndrGlobal);
                return;
            }

            Field field = dbObj as Field;
            if (field != null) {
                Stream(data, field);
                return;
            }

            Material mat = dbObj as Material;
            if (mat != null) {
                Stream(data, mat);
                return;
            }

            DataLink dataLink = dbObj as DataLink;
            if (dataLink != null) {
                Stream(data, dataLink);
                return;
            }

            GeoLocationData geoLocData = dbObj as GeoLocationData;
            if (geoLocData != null) {
                Stream(data, geoLocData);
                return;
            }

            MLeaderStyle mleaderStyle = dbObj as MLeaderStyle;
            if (mleaderStyle != null) {
                Stream(data, mleaderStyle);
                return;
            }

            LinkedData linkData = dbObj as LinkedData;
            if (linkData != null) {
                Stream(data, linkData);
                return;
            }
        }

        private void
        Stream(ArrayList data, DataTable dTable)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DataTable)));

            data.Add(new Snoop.Data.String("Table name", dTable.TableName));
            data.Add(new Snoop.Data.Int("Number of rows", dTable.NumRows));
            data.Add(new Snoop.Data.Int("Number of rows grow size", dTable.NumRowsGrowSize));
            data.Add(new Snoop.Data.Int("Number of rows physical size", dTable.NumRowsPhysicalSize));
            data.Add(new Snoop.Data.Int("Number of columns", dTable.NumColumns));
            data.Add(new Snoop.Data.Int("Number of columns grow size", dTable.NumColsGrowSize));
            data.Add(new Snoop.Data.Int("Number of columns physical size", dTable.NumColsPhysicalSize));

            // add all the column objects
            ArrayList cols = new ArrayList();
            for (int i = 0; i < dTable.NumColumns; i++)
                cols.Add(dTable.GetColumnAt(i));
            data.Add(new Snoop.Data.ObjectCollection("Columns", cols));

            // add all the row objects
            ArrayList rows = new ArrayList();
            for (int i = 0; i < dTable.NumRows; i++)
                rows.Add(dTable.GetRowAt(i));
            data.Add(new Snoop.Data.ObjectCollection("Rows", rows));
        }

        #region DBDictionary
        private void
        Stream(ArrayList data, DBDictionary dbDict)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DBDictionary)));

            data.Add(new Snoop.Data.Int("Count", dbDict.Count));

            data.Add(new Snoop.Data.String("Merge style", dbDict.MergeStyle.ToString()));
            data.Add(new Snoop.Data.Bool("Treat elements as hard", dbDict.TreatElementsAsHard));

            DictionaryWithDefaultDictionary dict = dbDict as DictionaryWithDefaultDictionary;
            if (dict != null) {
                Stream(data, dict);
                return;
            }
        }

        private void
        Stream(ArrayList data, DictionaryWithDefaultDictionary dict)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DictionaryWithDefaultDictionary)));

            data.Add(new Snoop.Data.ObjectId("Default ID", dict.DefaultId));
        }
        #endregion

        #region Filter
        private void
        Stream(ArrayList data, Autodesk.AutoCAD.DatabaseServices.Filters.Filter filter)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.DatabaseServices.Filters.Filter)));

            data.Add(new Snoop.Data.Object("Index Class", filter.IndexClass));

            // branch to all known major sub-classes
            SpatialFilter spatialFltr = filter as SpatialFilter;
            if (spatialFltr != null) {
                Stream(data, spatialFltr);
                return;
            }

            LayerFilter layerFltr = filter as LayerFilter;
            if (layerFltr != null) {
                Stream(data, layerFltr);
                return;
            }
        }

        private void
        Stream(ArrayList data, SpatialFilter spatialFltr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SpatialFilter)));

            data.Add(new Snoop.Data.Object("Definition", spatialFltr.Definition));
            data.Add(new Snoop.Data.Object("Index Class", spatialFltr.IndexClass));
            data.Add(new Snoop.Data.Bool("Has Perspective Camera", spatialFltr.HasPerspectiveCamera));
            data.Add(new Snoop.Data.Object("Extents", spatialFltr.GetQueryBounds()));
            data.Add(new Snoop.Data.Object("Volume", spatialFltr.GetVolume()));
        }

        private void
        Stream(ArrayList data, LayerFilter layerFltr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerFilter)));

            data.Add(new Snoop.Data.Bool("Is Valid", layerFltr.IsValid));
            data.Add(new Snoop.Data.Object("Index Class", layerFltr.IndexClass));
            data.Add(new Snoop.Data.Int("Layer Count", layerFltr.LayerCount));
            string tmpName = "";
            for (int i = 0; i < layerFltr.LayerCount; i++) {
                layerFltr.GetAt(i, tmpName);
                data.Add(new Snoop.Data.String(String.Format("Layer [{0}]", i), tmpName));
            }

        }

        #endregion

        private void
        Stream(ArrayList data, DrawOrderTable drawOrderTbl)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DrawOrderTable)));

            data.Add(new Snoop.Data.ObjectId("Block ID", drawOrderTbl.BlockId));
        }

        private void
        Stream(ArrayList data, Xrecord xRec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Xrecord)));

            data.Add(new Snoop.Data.Object("Data", xRec.Data));
            data.Add(new Snoop.Data.Bool("Xlate references", xRec.XlateReferences));
        }

        private void
        Stream(ArrayList data, Group group)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Group)));

            data.Add(new Snoop.Data.String("Name", group.Name));
            data.Add(new Snoop.Data.String("Description", group.Description));
            data.Add(new Snoop.Data.Bool("Is anonymous", group.IsAnonymous));
            data.Add(new Snoop.Data.Bool("Is not accessible", group.IsNotAccessible));
            data.Add(new Snoop.Data.Bool("Selectable", group.Selectable));
            data.Add(new Snoop.Data.Int("Number of entities", group.NumEntities));
            data.Add(new Snoop.Data.ObjectIdCollection("All entities", group.GetAllEntityIds()));
        }

        #region Filters.Index
        private void
        Stream(ArrayList data, Autodesk.AutoCAD.DatabaseServices.Filters.Index index)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.DatabaseServices.Filters.Index)));

            data.Add(new Snoop.Data.Bool("Is up to date", index.IsUptoDate));
            data.Add(new Snoop.Data.Object("Last updated at", index.LastUpdatedAt));
            data.Add(new Snoop.Data.Object("Last updated at (Universal)", index.LastUpdatedAtU));
            data.Add(new Snoop.Data.ObjectId("Object being indexed", index.ObjectBeingIndexedId));

            // branch to all known major sub-classes
            SpatialIndex spatialIndex = index as SpatialIndex;
            if (spatialIndex != null) {
                Stream(data, spatialIndex);
                return;
            }

            LayerIndex layerIndex = index as LayerIndex;
            if (layerIndex != null) {
                Stream(data, layerIndex);
                return;
            }
        }

        private void
        Stream(ArrayList data, SpatialIndex spatialIndex)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SpatialIndex)));

            // no data at this level
        }

        private void
        Stream(ArrayList data, LayerIndex layerIndex)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerIndex)));

            // no data at this level
        }
        #endregion

        #region PlotSettings
        private void
        Stream(ArrayList data, PlotSettings ps)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotSettings)));

            data.Add(new Snoop.Data.String("Canonical media name", ps.CanonicalMediaName));
            data.Add(new Snoop.Data.String("Current style sheet", ps.CurrentStyleSheet));
            data.Add(new Snoop.Data.String("Custom print scale", ps.CustomPrintScale.ToString()));
            data.Add(new Snoop.Data.Bool("Draw viewports first", ps.DrawViewportsFirst));
            data.Add(new Snoop.Data.Bool("Model type", ps.ModelType));
            data.Add(new Snoop.Data.Bool("Plot centered", ps.PlotCentered));
            data.Add(new Snoop.Data.String("Plot configuration name", ps.PlotConfigurationName));
            data.Add(new Snoop.Data.Bool("Plot hidden", ps.PlotHidden));
            data.Add(new Snoop.Data.Point2d("Plot origin", ps.PlotOrigin));
            data.Add(new Snoop.Data.String("Plot paper margins", ps.PlotPaperMargins.ToString()));
            data.Add(new Snoop.Data.Point2d("Plot paper size", ps.PlotPaperSize));
            data.Add(new Snoop.Data.String("Plot paper units", ps.PlotPaperUnits.ToString()));
            data.Add(new Snoop.Data.Bool("Plot plot styles", ps.PlotPlotStyles));
            data.Add(new Snoop.Data.String("Plot rotation", ps.PlotRotation.ToString()));
            data.Add(new Snoop.Data.String("Plot settings name", ps.PlotSettingsName));
            data.Add(new Snoop.Data.String("Plot type", ps.PlotType.ToString()));
            data.Add(new Snoop.Data.String("Plot view name", ps.PlotViewName));
            data.Add(new Snoop.Data.Bool("Plot viewport borders", ps.PlotViewportBorders));
            data.Add(new Snoop.Data.String("Plot window area", ps.PlotWindowArea.ToString()));
            data.Add(new Snoop.Data.Bool("Print lineweights", ps.PrintLineweights));
            data.Add(new Snoop.Data.Bool("Scale lineweights", ps.ScaleLineweights));
            data.Add(new Snoop.Data.String("Shade plot", ps.ShadePlot.ToString()));
            data.Add(new Snoop.Data.Int("Shade plot custom dpi", ps.ShadePlotCustomDpi));
            data.Add(new Snoop.Data.String("Shade plot res level", ps.ShadePlotResLevel.ToString()));
            data.Add(new Snoop.Data.Bool("Show plot styles", ps.ShowPlotStyles));
            data.Add(new Snoop.Data.Double("Standard scale", ps.StdScale));
            data.Add(new Snoop.Data.String("Standard scale type", ps.StdScaleType.ToString()));
            data.Add(new Snoop.Data.Bool("Use standard scale", ps.UseStandardScale));

            // branch to all known major sub-classes
            Layout layout = ps as Layout;
            if (layout != null) {
                Stream(data, layout);
                return;
            }
        }

        private void
        Stream(ArrayList data, Layout layout)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Layout)));

            data.Add(new Snoop.Data.ObjectId("Block table record ID", layout.BlockTableRecordId));
            data.Add(new Snoop.Data.String("Layout name", layout.LayoutName));
            data.Add(new Snoop.Data.Int("Tab order", layout.TabOrder));
            data.Add(new Snoop.Data.Bool("Tab selected", layout.TabSelected));
            data.Add(new Snoop.Data.ObjectIdCollection("Viewports", layout.GetViewports()));
        }
        #endregion

        private void
        Stream(ArrayList data, MlineStyle mlStyle)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MlineStyle)));

            data.Add(new Snoop.Data.String("Name", mlStyle.Name));
            data.Add(new Snoop.Data.String("Description", mlStyle.Description));
            data.Add(new Snoop.Data.Angle("Start angle", mlStyle.StartAngle));
            data.Add(new Snoop.Data.Bool("Start inner arcs", mlStyle.StartInnerArcs));
            data.Add(new Snoop.Data.Bool("Start round cap", mlStyle.StartRoundCap));
            data.Add(new Snoop.Data.Bool("Start square cap", mlStyle.StartSquareCap));
            data.Add(new Snoop.Data.Angle("End angle", mlStyle.EndAngle));
            data.Add(new Snoop.Data.Bool("End inner arcs", mlStyle.EndInnerArcs));
            data.Add(new Snoop.Data.Bool("End round cap", mlStyle.EndRoundCap));
            data.Add(new Snoop.Data.Bool("End square cap", mlStyle.EndSquareCap));
            data.Add(new Snoop.Data.Bool("Filled", mlStyle.Filled));
            data.Add(new Snoop.Data.ObjectToString("Fill color", mlStyle.FillColor));
            data.Add(new Snoop.Data.Bool("Show miters", mlStyle.ShowMiters));
            data.Add(new Snoop.Data.ObjectCollection("Elements", (System.Collections.ICollection)mlStyle.Elements));
        }

        private void
        Stream(ArrayList data, PlaceHolder placeHolder)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlaceHolder)));

            // no data here
        }

        private void
        Stream(ArrayList data, ProxyObject proxyObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ProxyObject)));

            data.Add(new Snoop.Data.String("Application description", proxyObj.ApplicationDescription));
            data.Add(new Snoop.Data.String("Original class name", proxyObj.OriginalClassName));
            data.Add(new Snoop.Data.String("Original DXF name", proxyObj.OriginalDxfName));
            data.Add(new Snoop.Data.Int("Proxy flags", proxyObj.ProxyFlags));
            data.Add(new Snoop.Data.ObjectCollection("References", proxyObj.GetReferences()));
        }

        private void
        Stream(ArrayList data, Material mat)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Material)));

            data.Add(new Snoop.Data.String("Name", mat.Name));
            data.Add(new Snoop.Data.String("Description", mat.Description));
            data.Add(new Snoop.Data.Object("Ambient", mat.Ambient));
            data.Add(new Snoop.Data.Object("Bump", mat.Bump));
            data.Add(new Snoop.Data.Object("Diffuse", mat.Diffuse));
            data.Add(new Snoop.Data.Object("Opacity", mat.Opacity));
            data.Add(new Snoop.Data.Object("Reflection", mat.Reflection));
            data.Add(new Snoop.Data.Double("Reflectivity", mat.Reflectivity));
            data.Add(new Snoop.Data.Object("Refraction", mat.Refraction));
            data.Add(new Snoop.Data.Object("Specular", mat.Specular));
            data.Add(new Snoop.Data.String("Channel flags", mat.ChannelFlags.ToString()));
            data.Add(new Snoop.Data.String("Illumination model", mat.IlluminationModel.ToString()));
            data.Add(new Snoop.Data.String("Mode", mat.Mode.ToString()));
            data.Add(new Snoop.Data.Double("Self illumination", mat.SelfIllumination));
            data.Add(new Snoop.Data.Double("Translucence", mat.Translucence));
        }

        private void
        Stream(ArrayList data, DataLink dataLink)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DataLink)));

            data.Add(new Snoop.Data.String("Connection string", dataLink.ConnectionString));
            data.Add(new Snoop.Data.String("Data adapter Id", dataLink.DataAdapterId));
            data.Add(new Snoop.Data.String("Data link option", dataLink.DataLinkOption.ToString()));
            data.Add(new Snoop.Data.String("Description", dataLink.Description));
            data.Add(new Snoop.Data.Bool("Is valid", dataLink.IsValid));
            data.Add(new Snoop.Data.String("Name", dataLink.Name));
            data.Add(new Snoop.Data.String("Tool tip", dataLink.ToolTip));
            data.Add(new Snoop.Data.Int("Update option", dataLink.UpdateOption));
        }

        private void
        Stream(ArrayList data, GeoLocationData geoLocData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(GeoLocationData)));

            data.Add(new Snoop.Data.ObjectId("Block table record Id", geoLocData.BlockTableRecordId));
            data.Add(new Snoop.Data.Double("Coordinate projection radius", geoLocData.CoordinateProjectionRadius));
            data.Add(new Snoop.Data.String("Coordinate system", geoLocData.CoordinateSystem));
            data.Add(new Snoop.Data.Point3d("Design point", geoLocData.DesignPoint));
            data.Add(new Snoop.Data.Bool("Do sea level correction", geoLocData.DoSeaLevelCorrection));
            data.Add(new Snoop.Data.String("Geo RSS tag", geoLocData.GeoRSSTag));
            data.Add(new Snoop.Data.String("Horizontal units", geoLocData.HorizontalUnits.ToString()));
            data.Add(new Snoop.Data.Double("North direction", geoLocData.NorthDirection));
            data.Add(new Snoop.Data.Vector2d("North direction vector", geoLocData.NorthDirectionVector));
            data.Add(new Snoop.Data.Int("Num mesh points", geoLocData.NumMeshPoints));
            data.Add(new Snoop.Data.Point3d("Reference point", geoLocData.ReferencePoint));
            data.Add(new Snoop.Data.String("Scale estimation method", geoLocData.ScaleEstimationMethod.ToString()));
            data.Add(new Snoop.Data.Double("Scale factor", geoLocData.ScaleFactor));
            data.Add(new Snoop.Data.Distance("Sea level elevation", geoLocData.SeaLevelElevation));
            data.Add(new Snoop.Data.String("Type of coordinates", geoLocData.TypeOfCoordinates.ToString()));
            data.Add(new Snoop.Data.String("Vertical units", geoLocData.VerticalUnits.ToString()));
            data.Add(new Snoop.Data.Double("Vertical units scale", geoLocData.VerticalUnitsScale));
        }


        private void
        Stream(ArrayList data, DBVisualStyle viz)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DBVisualStyle)));

            data.Add(new Snoop.Data.String("Description", viz.Description));
            /*data.Add(new Snoop.Data.Object("Display style", viz.DisplayStyle));
            data.Add(new Snoop.Data.Object("Edge style", viz.EdgeStyle));
            data.Add(new Snoop.Data.Object("Face style", viz.FaceStyle));*/ // TBD: Fix JMA
            data.Add(new Snoop.Data.String("Type", viz.Type.ToString()));
            data.Add(new Snoop.Data.Bool("Internal use only", viz.InternalUseOnly));
        }

        private void
        Stream(ArrayList data, UnderlayDefinition underlayDef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(UnderlayDefinition)));

            data.Add(new Snoop.Data.String("Source file name", underlayDef.SourceFileName));
            data.Add(new Snoop.Data.String("Active file name", underlayDef.ActiveFileName));
            data.Add(new Snoop.Data.Bool("Loaded", underlayDef.Loaded));
            data.Add(new Snoop.Data.String("Item name", underlayDef.ItemName));
            data.Add(new Snoop.Data.Object("Underlay item", underlayDef.UnderlayItem));

            DgnDefinition dgnDef = underlayDef as DgnDefinition;
            if (dgnDef != null) {
                Stream(data, dgnDef);
                return;
            }

            DwfDefinition dwfDef = underlayDef as DwfDefinition;
            if (dwfDef != null) {
                Stream(data, dwfDef);
                return;
            }
        }

        private void
        Stream(ArrayList data, DgnDefinition underlayDef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DgnDefinition)));

            // no data at this level
        }

        private void
        Stream(ArrayList data, DwfDefinition underlayDef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DwfDefinition)));

            // no data at this level
        }


        #region Background
        private void
        Stream(ArrayList data, Background backGrnd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Background)));

            /// No data here!!

            SolidBackground solBackGrnd = backGrnd as SolidBackground;
            if (solBackGrnd != null) {
                Stream(data, solBackGrnd);
                return;
            }

            GradientBackground gradBackGrnd = backGrnd as GradientBackground;
            if (gradBackGrnd != null) {
                Stream(data, gradBackGrnd);
                return;
            }

            ImageBackground imgBackGrnd = backGrnd as ImageBackground;
            if (imgBackGrnd != null) {
                Stream(data, imgBackGrnd);
                return;
            }

            GroundPlaneBackground grndPlaneBackGrnd = backGrnd as GroundPlaneBackground;
            if (grndPlaneBackGrnd != null) {
                Stream(data, grndPlaneBackGrnd);
                return;
            }

            SkyBackground skyBackGrnd = backGrnd as SkyBackground;
            if (skyBackGrnd != null) {
                Stream(data, skyBackGrnd);
                return;
            }
        }

        private void
        Stream(ArrayList data, SolidBackground solBackGrnd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SolidBackground)));

            data.Add(new Snoop.Data.Object("Color", solBackGrnd.Color));
        }

        private void
        Stream(ArrayList data, GradientBackground gradBackGrnd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(GradientBackground)));

            data.Add(new Snoop.Data.Object("Color bottom", gradBackGrnd.ColorBottom));
            data.Add(new Snoop.Data.Object("Color middle", gradBackGrnd.ColorMiddle));
            data.Add(new Snoop.Data.Object("Color top", gradBackGrnd.ColorTop));
            data.Add(new Snoop.Data.Distance("Height", gradBackGrnd.Height));
            data.Add(new Snoop.Data.Distance("Horizon", gradBackGrnd.Horizon));
            data.Add(new Snoop.Data.Angle("Rotation", gradBackGrnd.Rotation));
        }

        private void
        Stream(ArrayList data, ImageBackground imgBackGrnd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ImageBackground)));

            data.Add(new Snoop.Data.Bool("Fit to screen", imgBackGrnd.FitToScreen));
            data.Add(new Snoop.Data.String("Image file name", imgBackGrnd.ImageFileName));
            data.Add(new Snoop.Data.Bool("Maintain aspect ratio", imgBackGrnd.MaintainAspectRatio));
            data.Add(new Snoop.Data.Vector2d("Offset", imgBackGrnd.Offset));
            data.Add(new Snoop.Data.Scale2d("Scale", imgBackGrnd.Scale));
            data.Add(new Snoop.Data.Bool("Use tiling", imgBackGrnd.UseTiling));
        }

        private void
        Stream(ArrayList data, GroundPlaneBackground grndPlaneBackGrnd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(GroundPlaneBackground)));

            data.Add(new Snoop.Data.Object("Color ground plane far", grndPlaneBackGrnd.ColorGroundPlaneFar));
            data.Add(new Snoop.Data.Object("Color ground plane near", grndPlaneBackGrnd.ColorGroundPlaneNear));
            data.Add(new Snoop.Data.Object("Color sky horizon", grndPlaneBackGrnd.ColorSkyHorizon));
            data.Add(new Snoop.Data.Object("Color underground azimuth", grndPlaneBackGrnd.ColorUndergroundAzimuth));
            data.Add(new Snoop.Data.Object("Color underground horizon", grndPlaneBackGrnd.ColorUndergroundHorizon));
        }

        private void
        Stream(ArrayList data, SkyBackground skyBackGrnd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(GroundPlaneBackground)));

            data.Add(new Snoop.Data.ObjectId("Sun Id", skyBackGrnd.SunId));  
        }
        #endregion


        private void
        Stream(ArrayList data, Sun sun)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Sun)));

            data.Add(new Snoop.Data.Bool("Is on", sun.IsOn));
            data.Add(new Snoop.Data.Object("Date Time", sun.DateTime));
            data.Add(new Snoop.Data.Bool("Is daylight savings on", sun.IsDaylightSavingsOn));
            data.Add(new Snoop.Data.Double("Altitude", sun.Altitude));
            data.Add(new Snoop.Data.Double("Azimuth", sun.Azimuth));
            data.Add(new Snoop.Data.Double("Intensity", sun.Intensity));
            data.Add(new Snoop.Data.Object("Shadow parameters", sun.ShadowParameters));
            data.Add(new Snoop.Data.Object("Sun color", sun.SunColor));
            data.Add(new Snoop.Data.Vector3d("Sun direction", sun.SunDirection));
        }

        #region RenderSettings
        private void
        Stream(ArrayList data, RenderSettings rndrSetngs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RenderSettings)));

            data.Add(new Snoop.Data.String("Name", rndrSetngs.Name));
            data.Add(new Snoop.Data.String("Description", rndrSetngs.Description));
            data.Add(new Snoop.Data.String("Preview image filename", rndrSetngs.PreviewImageFileName));
            data.Add(new Snoop.Data.Int("Display index", rndrSetngs.DisplayIndex));
            data.Add(new Snoop.Data.Bool("Back faces enabled", rndrSetngs.BackFacesEnabled));
            data.Add(new Snoop.Data.Bool("Diagnostic background enabled", rndrSetngs.DiagnosticBackgroundEnabled));
            data.Add(new Snoop.Data.Bool("Materials enabled", rndrSetngs.MaterialsEnabled));
            data.Add(new Snoop.Data.Bool("Shadows enabled", rndrSetngs.ShadowsEnabled));
            data.Add(new Snoop.Data.Bool("Texture sampling", rndrSetngs.TextureSampling));

            //MentalRayRenderSettings mRayRndrStngs = rndrSetngs as MentalRayRenderSettings;
            //if (mRayRndrStngs != null) {
            //    Stream(data, mRayRndrStngs);
            //    return;
            //}
        }

       /* private void
        Stream(ArrayList data, MentalRayRenderSettings mRayRndrStngs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MentalRayRenderSettings)));

            data.Add(new Snoop.Data.String("Diagnostic BSP mode", mRayRndrStngs.DiagnosticBSPMode.ToString()));
            data.Add(new Snoop.Data.String("Diagnostic grid mode", mRayRndrStngs.DiagnosticGridMode.Mode.ToString()));
            data.Add(new Snoop.Data.Double("Diagnostic grid mode size", mRayRndrStngs.DiagnosticGridMode.Size));
            data.Add(new Snoop.Data.String("Diagnostic mode", mRayRndrStngs.DiagnosticMode.ToString()));
            data.Add(new Snoop.Data.String("Diagnostic photon mode", mRayRndrStngs.DiagnosticPhotonMode.ToString()));
            data.Add(new Snoop.Data.Double("Energy multiplier", mRayRndrStngs.EnergyMultiplier));
            data.Add(new Snoop.Data.Bool("Export MI enabled", mRayRndrStngs.ExportMIEnabled));
            data.Add(new Snoop.Data.String("Export MI filename", mRayRndrStngs.ExportMIFileName));
            data.Add(new Snoop.Data.Int("FG ray count", mRayRndrStngs.FGRayCount));
            data.Add(new Snoop.Data.Double("FG sample radius (max)", mRayRndrStngs.FGSampleRadius.Max));
            data.Add(new Snoop.Data.Double("FG sample radius (min)", mRayRndrStngs.FGSampleRadius.Min));
            data.Add(new Snoop.Data.Bool("FG sample radius state (max)", mRayRndrStngs.FGSampleRadiusState.Max));
            data.Add(new Snoop.Data.Bool("FG sample radius state (min)", mRayRndrStngs.FGSampleRadiusState.Min));
            data.Add(new Snoop.Data.Bool("FG sample radius state (pixels)", mRayRndrStngs.FGSampleRadiusState.Pixels));
            data.Add(new Snoop.Data.Bool("Final gathering enabled", mRayRndrStngs.FinalGatheringEnabled));
            data.Add(new Snoop.Data.Int("GI photons per light", mRayRndrStngs.GIPhotonsPerLight));
            data.Add(new Snoop.Data.Int("GI sample count", mRayRndrStngs.GISampleCount));
            data.Add(new Snoop.Data.Double("GI sample radius", mRayRndrStngs.GISampleRadius));
            data.Add(new Snoop.Data.Bool("GI sample radius enabled", mRayRndrStngs.GISampleRadiusEnabled));
            data.Add(new Snoop.Data.Bool("Global illumination enabled", mRayRndrStngs.GlobalIlluminationEnabled));
            data.Add(new Snoop.Data.Int("Memory limit", mRayRndrStngs.MemoryLimit));
            data.Add(new Snoop.Data.Int("Photon trace depth (reflection)", mRayRndrStngs.PhotonTraceDepth.Reflection));
            data.Add(new Snoop.Data.Int("Photon trace depth (refraction)", mRayRndrStngs.PhotonTraceDepth.Refraction));
            data.Add(new Snoop.Data.Int("Photon trace depth (sum)", mRayRndrStngs.PhotonTraceDepth.Sum));
            data.Add(new Snoop.Data.Int("Ray trace depth (reflection)", mRayRndrStngs.RayTraceDepth.Reflection));
            data.Add(new Snoop.Data.Int("Ray trace depth (refraction)", mRayRndrStngs.RayTraceDepth.Refraction));
            data.Add(new Snoop.Data.Int("Ray trace depth (sum)", mRayRndrStngs.RayTraceDepth.Sum));
            data.Add(new Snoop.Data.Bool("Ray tracing Enabled", mRayRndrStngs.RayTracingEnabled));
            data.Add(new Snoop.Data.Int("Sampling (max)", mRayRndrStngs.Sampling.Max));
            data.Add(new Snoop.Data.Int("Sampling (min)", mRayRndrStngs.Sampling.Min));
            data.Add(new Snoop.Data.Double("Sampling contrast color (A)", mRayRndrStngs.SamplingContrastColor.A));
            data.Add(new Snoop.Data.Double("Sampling contrast color (B)", mRayRndrStngs.SamplingContrastColor.B));
            data.Add(new Snoop.Data.Double("Sampling contrast color (G)", mRayRndrStngs.SamplingContrastColor.G));
            data.Add(new Snoop.Data.Double("Sampling contrast color (R)", mRayRndrStngs.SamplingContrastColor.R));
            data.Add(new Snoop.Data.String("Sampling filter", mRayRndrStngs.SamplingFilter.Filter.ToString()));
            data.Add(new Snoop.Data.Double("Sampling filter height", mRayRndrStngs.SamplingFilter.Height));
            data.Add(new Snoop.Data.Double("Sampling filter width", mRayRndrStngs.SamplingFilter.Width));
            data.Add(new Snoop.Data.Bool("Shadow maps enabled", mRayRndrStngs.ShadowMapsEnabled));
            data.Add(new Snoop.Data.String("Shadow mode", mRayRndrStngs.ShadowMode.ToString()));
            data.Add(new Snoop.Data.String("Tile order", mRayRndrStngs.TileOrder.ToString()));
            data.Add(new Snoop.Data.Int("Tile size", mRayRndrStngs.TileSize));
        }*/
        #endregion

        private void
        Stream(ArrayList data, RenderEnvironment rndrEnv)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RenderEnvironment)));

            data.Add(new Snoop.Data.Double("Distances (near)", rndrEnv.Distances.Near));
            data.Add(new Snoop.Data.Double("Distances (far)", rndrEnv.Distances.Far));
            data.Add(new Snoop.Data.Bool("Environment image enabled", rndrEnv.EnvironmentImageEnabled));
            data.Add(new Snoop.Data.String("Environment image filename", rndrEnv.EnvironmentImageFileName));
            data.Add(new Snoop.Data.Bool("Fog background enabled", rndrEnv.FogBackgroundEnabled));
            data.Add(new Snoop.Data.Object("Fog color", rndrEnv.FogColor));
            data.Add(new Snoop.Data.Double("Fog density (near)", rndrEnv.FogDensity.Near));
            data.Add(new Snoop.Data.Double("Fog density (rar)", rndrEnv.FogDensity.Far));
            data.Add(new Snoop.Data.Bool("Fog enabled", rndrEnv.FogEnabled));
        }

        private void
        Stream(ArrayList data, RenderGlobal rndrGlobal)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RenderGlobal)));

            data.Add(new Snoop.Data.Int("Dimensions (height)", rndrGlobal.Dimensions.Height));
            data.Add(new Snoop.Data.Int("Dimensions (width)", rndrGlobal.Dimensions.Width));
            data.Add(new Snoop.Data.Bool("High info level", rndrGlobal.HighInfoLevel));
            data.Add(new Snoop.Data.Bool("Predefined presets first", rndrGlobal.PredefinedPresetsFirst));
            data.Add(new Snoop.Data.String("Destination", rndrGlobal.ProcedureAndDestination.Destination.ToString()));
            data.Add(new Snoop.Data.String("Procedure", rndrGlobal.ProcedureAndDestination.Procedure.ToString()));
            data.Add(new Snoop.Data.Bool("Save enabled", rndrGlobal.SaveEnabled));
            data.Add(new Snoop.Data.String("Save filename", rndrGlobal.SaveFileName));
        }

        private void
        Stream(ArrayList data, Field field)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Field)));

            data.Add(new Snoop.Data.String("Data type", field.DataType.ToString()));
            data.Add(new Snoop.Data.String("Evaluation option", field.EvaluationOption.ToString()));
            data.Add(new Snoop.Data.Object("Evaluation status", field.EvaluationStatus));
            data.Add(new Snoop.Data.String("Evaluator ID", field.EvaluatorId));
            data.Add(new Snoop.Data.String("Filing option", field.FilingOption.ToString()));
            data.Add(new Snoop.Data.String("Format", field.Format));
            data.Add(new Snoop.Data.Object("Hyper Link", field.HyperLink));
            data.Add(new Snoop.Data.Bool("Is Text Field", field.IsTextField));
            data.Add(new Snoop.Data.String("State", field.State.ToString()));
            data.Add(new Snoop.Data.Object("Value", field.Value));
            data.Add(new Snoop.Data.String("Field code", field.GetFieldCode()));
            data.Add(new Snoop.Data.String("String value", field.GetStringValue()));
            data.Add(new Snoop.Data.ObjectCollection("Children", field.GetChildren()));
        }

        private void
        Stream(ArrayList data, RasterImageDef rasterImgDef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RasterImageDef)));

            data.Add(new Snoop.Data.String("Active file name", rasterImgDef.ActiveFileName));
            data.Add(new Snoop.Data.String("Source file name", rasterImgDef.SourceFileName));
            // Throws an Internal Error: eNotOpenForWrite and crashes
            //data.Add(new Snoop.Data.String("Search for active path", rasterImgDef.SearchForActivePath));
            data.Add(new Snoop.Data.Int("Color depth", rasterImgDef.ColorDepth));
            data.Add(new Snoop.Data.String("File type", rasterImgDef.FileType));
            data.Add(new Snoop.Data.Bool("Image modified", rasterImgDef.ImageModified));
            data.Add(new Snoop.Data.Bool("Is embedded", rasterImgDef.IsEmbedded));
            data.Add(new Snoop.Data.Bool("Is loaded", rasterImgDef.IsLoaded));
            data.Add(new Snoop.Data.String("Organization", rasterImgDef.Organization.ToString()));
            data.Add(new Snoop.Data.Vector2d("Resolution mm per pixel", rasterImgDef.ResolutionMMPerPixel));
            data.Add(new Snoop.Data.String("Resolution units", rasterImgDef.ResolutionUnits.ToString()));
            data.Add(new Snoop.Data.Vector2d("Size", rasterImgDef.Size));
            data.Add(new Snoop.Data.Int("Undo store size", rasterImgDef.UndoStoreSize));
        }

        private void
        Stream(ArrayList data, RasterVariables rasterVars)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RasterVariables)));

            data.Add(new Snoop.Data.String("Image frame", rasterVars.ImageFrame.ToString()));
            data.Add(new Snoop.Data.String("Image quality", rasterVars.ImageQuality.ToString()));
            data.Add(new Snoop.Data.String("User scale", rasterVars.UserScale.ToString()));
        }

        private void
        Stream(ArrayList data, TableStyle tabStyle)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(TableStyle)));

            data.Add(new Snoop.Data.Int("Bit flags", tabStyle.BitFlags));
            data.Add(new Snoop.Data.String("Description", tabStyle.Description));
            data.Add(new Snoop.Data.String("Flow eirection", tabStyle.FlowDirection.ToString()));
            data.Add(new Snoop.Data.Double("Horizontal cell margin", tabStyle.HorizontalCellMargin));
            data.Add(new Snoop.Data.Double("Vertical cell margin", tabStyle.VerticalCellMargin));
            data.Add(new Snoop.Data.Bool("Is header suppressed", tabStyle.IsHeaderSuppressed));
            data.Add(new Snoop.Data.Bool("Is title suppressed", tabStyle.IsTitleSuppressed));

            data.Add(new Snoop.Data.CategorySeparator("Data row"));
            data.Add(new Snoop.Data.ObjectId("Text style", tabStyle.TextStyle(RowType.DataRow)));
            data.Add(new Snoop.Data.Double("Text height", tabStyle.TextHeight(RowType.DataRow)));
            data.Add(new Snoop.Data.String("Alignment", tabStyle.Alignment(RowType.DataRow).ToString()));
            data.Add(new Snoop.Data.Object("Color", tabStyle.Color(RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Background color", tabStyle.BackgroundColor(RowType.DataRow)));
            data.Add(new Snoop.Data.Bool("Is Background color None", tabStyle.IsBackgroundColorNone(RowType.DataRow)));

            data.Add(new Snoop.Data.CategorySeparator("Header row"));
            data.Add(new Snoop.Data.ObjectId("Text style", tabStyle.TextStyle(RowType.HeaderRow)));
            data.Add(new Snoop.Data.Double("Text height", tabStyle.TextHeight(RowType.HeaderRow)));
            data.Add(new Snoop.Data.String("Alignment", tabStyle.Alignment(RowType.HeaderRow).ToString()));
            data.Add(new Snoop.Data.Object("Color", tabStyle.Color(RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Background color", tabStyle.BackgroundColor(RowType.HeaderRow)));
            data.Add(new Snoop.Data.Bool("Is Background color none", tabStyle.IsBackgroundColorNone(RowType.HeaderRow)));

            data.Add(new Snoop.Data.CategorySeparator("Title row"));
            data.Add(new Snoop.Data.ObjectId("Text style", tabStyle.TextStyle(RowType.TitleRow)));
            data.Add(new Snoop.Data.Double("Text height", tabStyle.TextHeight(RowType.TitleRow)));
            data.Add(new Snoop.Data.String("Alignment", tabStyle.Alignment(RowType.TitleRow).ToString()));
            data.Add(new Snoop.Data.Object("Color", tabStyle.Color(RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("Background color", tabStyle.BackgroundColor(RowType.TitleRow)));
            data.Add(new Snoop.Data.Bool("Is background color none", tabStyle.IsBackgroundColorNone(RowType.TitleRow)));

            data.Add(new Snoop.Data.CategorySeparator("Gridline Colors"));
            data.Add(new Snoop.Data.Object("Data Row - Horizontal Bottom", tabStyle.GridColor(GridLineType.HorizontalBottom, RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Data Row - Horizontal Inside", tabStyle.GridColor(GridLineType.HorizontalInside, RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Data Row - Horizontal Top", tabStyle.GridColor(GridLineType.HorizontalTop, RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Data Row - Vertical Inside", tabStyle.GridColor(GridLineType.VerticalInside, RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Data Row - Vertical Left", tabStyle.GridColor(GridLineType.VerticalLeft, RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Data Row - Vertical Right", tabStyle.GridColor(GridLineType.VerticalRight, RowType.DataRow)));

            data.Add(new Snoop.Data.Object("Header Row - Horizontal Bottom", tabStyle.GridColor(GridLineType.HorizontalBottom, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Header Row - Horizontal Inside", tabStyle.GridColor(GridLineType.HorizontalInside, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Header Row - Horizontal Top", tabStyle.GridColor(GridLineType.HorizontalTop, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Header Row - Vertical Inside", tabStyle.GridColor(GridLineType.VerticalInside, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Header Row - Vertical Left", tabStyle.GridColor(GridLineType.VerticalLeft, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Header Row - Vertical Right", tabStyle.GridColor(GridLineType.VerticalRight, RowType.HeaderRow)));

            data.Add(new Snoop.Data.Object("TitleRow - Horizontal Bottom", tabStyle.GridColor(GridLineType.HorizontalBottom, RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("TitleRow - Horizontal Inside", tabStyle.GridColor(GridLineType.HorizontalInside, RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("TitleRow - Horizontal Top", tabStyle.GridColor(GridLineType.HorizontalTop, RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("TitleRow - Vertical Inside", tabStyle.GridColor(GridLineType.VerticalInside, RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("TitleRow - Vertical Left", tabStyle.GridColor(GridLineType.VerticalLeft, RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("TitleRow - Vertical Right", tabStyle.GridColor(GridLineType.VerticalRight, RowType.TitleRow)));

            data.Add(new Snoop.Data.CategorySeparator("Gridline Weight"));
            data.Add(new Snoop.Data.Object("Data Row - Horizontal Bottom", tabStyle.GridLineWeight(GridLineType.HorizontalBottom, RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Data Row - Horizontal Inside", tabStyle.GridLineWeight(GridLineType.HorizontalInside, RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Data Row - Horizontal Top", tabStyle.GridLineWeight(GridLineType.HorizontalTop, RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Data Row - Vertical Inside", tabStyle.GridLineWeight(GridLineType.VerticalInside, RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Data Row - Vertical Left", tabStyle.GridLineWeight(GridLineType.VerticalLeft, RowType.DataRow)));
            data.Add(new Snoop.Data.Object("Data Row - Vertical Right", tabStyle.GridLineWeight(GridLineType.VerticalRight, RowType.DataRow)));

            data.Add(new Snoop.Data.Object("Header Row - Horizontal Bottom", tabStyle.GridLineWeight(GridLineType.HorizontalBottom, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Header Row - Horizontal Inside", tabStyle.GridLineWeight(GridLineType.HorizontalInside, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Header Row - Horizontal Top", tabStyle.GridLineWeight(GridLineType.HorizontalTop, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Header Row - Vertical Inside", tabStyle.GridLineWeight(GridLineType.VerticalInside, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Header Row - Vertical Left", tabStyle.GridLineWeight(GridLineType.VerticalLeft, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Object("Header Row - Vertical Right", tabStyle.GridLineWeight(GridLineType.VerticalRight, RowType.HeaderRow)));

            data.Add(new Snoop.Data.Object("TitleRow - Horizontal Bottom", tabStyle.GridLineWeight(GridLineType.HorizontalBottom, RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("TitleRow - Horizontal Inside", tabStyle.GridLineWeight(GridLineType.HorizontalInside, RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("TitleRow - Horizontal Top", tabStyle.GridLineWeight(GridLineType.HorizontalTop, RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("TitleRow - Vertical Inside", tabStyle.GridLineWeight(GridLineType.VerticalInside, RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("TitleRow - Vertical Left", tabStyle.GridLineWeight(GridLineType.VerticalLeft, RowType.TitleRow)));
            data.Add(new Snoop.Data.Object("TitleRow - Vertical Right", tabStyle.GridLineWeight(GridLineType.VerticalRight, RowType.TitleRow)));

            data.Add(new Snoop.Data.CategorySeparator("Gridline Visibility"));
            data.Add(new Snoop.Data.Bool("Data Row - Horizontal Bottom", tabStyle.GridVisibility(GridLineType.HorizontalBottom, RowType.DataRow)));
            data.Add(new Snoop.Data.Bool("Data Row - Horizontal Inside", tabStyle.GridVisibility(GridLineType.HorizontalInside, RowType.DataRow)));
            data.Add(new Snoop.Data.Bool("Data Row - Horizontal Top", tabStyle.GridVisibility(GridLineType.HorizontalTop, RowType.DataRow)));
            data.Add(new Snoop.Data.Bool("Data Row - Vertical Inside", tabStyle.GridVisibility(GridLineType.VerticalInside, RowType.DataRow)));
            data.Add(new Snoop.Data.Bool("Data Row - Vertical Left", tabStyle.GridVisibility(GridLineType.VerticalLeft, RowType.DataRow)));
            data.Add(new Snoop.Data.Bool("Data Row - Vertical Right", tabStyle.GridVisibility(GridLineType.VerticalRight, RowType.DataRow)));

            data.Add(new Snoop.Data.Bool("Header Row - Horizontal Bottom", tabStyle.GridVisibility(GridLineType.HorizontalBottom, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Bool("Header Row - Horizontal Inside", tabStyle.GridVisibility(GridLineType.HorizontalInside, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Bool("Header Row - Horizontal Top", tabStyle.GridVisibility(GridLineType.HorizontalTop, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Bool("Header Row - Vertical Inside", tabStyle.GridVisibility(GridLineType.VerticalInside, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Bool("Header Row - Vertical Left", tabStyle.GridVisibility(GridLineType.VerticalLeft, RowType.HeaderRow)));
            data.Add(new Snoop.Data.Bool("Header Row - Vertical Right", tabStyle.GridVisibility(GridLineType.VerticalRight, RowType.HeaderRow)));

            data.Add(new Snoop.Data.Bool("TitleRow - Horizontal Bottom", tabStyle.GridVisibility(GridLineType.HorizontalBottom, RowType.TitleRow)));
            data.Add(new Snoop.Data.Bool("TitleRow - Horizontal Inside", tabStyle.GridVisibility(GridLineType.HorizontalInside, RowType.TitleRow)));
            data.Add(new Snoop.Data.Bool("TitleRow - Horizontal Top", tabStyle.GridVisibility(GridLineType.HorizontalTop, RowType.TitleRow)));
            data.Add(new Snoop.Data.Bool("TitleRow - Vertical Inside", tabStyle.GridVisibility(GridLineType.VerticalInside, RowType.TitleRow)));
            data.Add(new Snoop.Data.Bool("TitleRow - Vertical Left", tabStyle.GridVisibility(GridLineType.VerticalLeft, RowType.TitleRow)));
            data.Add(new Snoop.Data.Bool("TitleRow - Vertical Right", tabStyle.GridVisibility(GridLineType.VerticalRight, RowType.TitleRow)));
        }

        private void
        Stream(ArrayList data, SectionSettings secSettings)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SectionSettings)));

            data.Add(new Snoop.Data.String("Current section type", secSettings.CurrentSectionType.ToString()));

            data.Add(new Snoop.Data.CategorySeparator("Section Type"));
            SectionType secType = secSettings.CurrentSectionType;
            data.Add(new Snoop.Data.ObjectId("Destination block", secSettings.DestinationBlock(secType)));
            data.Add(new Snoop.Data.String("Destination file", secSettings.DestinationFile(secType)));
            data.Add(new Snoop.Data.String("Generation options", secSettings.GenerationOptions(secType).ToString()));

            // TBD: More to do here... need to figure out how this works
        }

        private void
        Stream(ArrayList data, SectionManager secMgr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SectionManager)));

            data.Add(new Snoop.Data.ObjectId("Live section", secMgr.LiveSection));
            data.Add(new Snoop.Data.Int("Number of sections", secMgr.NumSections));

            // TBD: need to enumerate section objects
        }

        private void
        Stream(ArrayList data, AttributeDefinition attdef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AttributeDefinition)));

            data.Add(new Snoop.Data.Bool("Constant", attdef.Constant));
            data.Add(new Snoop.Data.Bool("Invisible", attdef.Invisible));
            data.Add(new Snoop.Data.Bool("Preset", attdef.Preset));
            data.Add(new Snoop.Data.Bool("Verifiable", attdef.Verifiable));
            data.Add(new Snoop.Data.String("Tag", attdef.Tag));
            data.Add(new Snoop.Data.String("Prompt", attdef.Prompt));
            data.Add(new Snoop.Data.Int("Field length", attdef.FieldLength));
            data.Add(new Snoop.Data.Bool("Lock position in block", attdef.LockPositionInBlock));
        }

        private void
        Stream(ArrayList data, AttributeReference attref)
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
        Stream(ArrayList data, Table tbl)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Table)));

            data.Add(new Snoop.Data.ObjectId("Table style", tbl.TableStyle));
            data.Add(new Snoop.Data.Vector3d("Direction", tbl.Direction));
            data.Add(new Snoop.Data.String("Flow direction", tbl.FlowDirection.ToString()));
            data.Add(new Snoop.Data.Double("Width", tbl.Width));
            data.Add(new Snoop.Data.Double("Height", tbl.Height));
            /*data.Add(new Snoop.Data.Double("Horizontal cell margin", tbl.HorizontalCellMargin));
            data.Add(new Snoop.Data.Double("Vertical cell margin", tbl.VerticalCellMargin));
            data.Add(new Snoop.Data.Bool("Is header suppressed", tbl.IsHeaderSuppressed));
            data.Add(new Snoop.Data.Bool("Is title suppressed", tbl.IsTitleSuppressed));
            data.Add(new Snoop.Data.Double("Minimum table width", tbl.MinimumTableWidth));
            data.Add(new Snoop.Data.Double("Minimum table height", tbl.MinimumTableHeight));
            data.Add(new Snoop.Data.Int("Number of columns", tbl.NumColumns));
            data.Add(new Snoop.Data.Int("Number of rows", tbl.NumRows));*/  // TBD: Fix JMA
            data.Add(new Snoop.Data.Bool("Has sub selection", tbl.HasSubSelection));
            if (tbl.HasSubSelection)
                data.Add(new Snoop.Data.Object("Sub selection", tbl.SubSelection));

            // TBD: more of the non-property functions to show?
        }

        private void
        Stream(ArrayList data, MLeaderStyle mleaderStyle)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MLeaderStyle)));

            data.Add(new Snoop.Data.Double("Arrow size", mleaderStyle.ArrowSize));
            data.Add(new Snoop.Data.ObjectId("Arrow symbol Id", mleaderStyle.ArrowSymbolId));
            data.Add(new Snoop.Data.Object("Block color", mleaderStyle.BlockColor));
            data.Add(new Snoop.Data.String("Block connection type", mleaderStyle.BlockConnectionType.ToString()));
            data.Add(new Snoop.Data.ObjectId("Block content Id", mleaderStyle.BlockId));
            data.Add(new Snoop.Data.Double("Block rotation", mleaderStyle.BlockRotation));
            data.Add(new Snoop.Data.Scale3d("Block scale", mleaderStyle.BlockScale));
            data.Add(new Snoop.Data.Double("Break size", mleaderStyle.BreakSize));
            data.Add(new Snoop.Data.String("Content type", mleaderStyle.ContentType.ToString()));
            data.Add(new Snoop.Data.Object("Default mtext", mleaderStyle.DefaultMText));
            data.Add(new Snoop.Data.Double("Dogleg length", mleaderStyle.DoglegLength));
            data.Add(new Snoop.Data.String("Draw leader order type", mleaderStyle.DrawLeaderOrderType.ToString()));
            data.Add(new Snoop.Data.String("Draw mleader order type", mleaderStyle.DrawMLeaderOrderType.ToString()));
            data.Add(new Snoop.Data.Bool("Enable block rotation", mleaderStyle.EnableBlockRotation));
            data.Add(new Snoop.Data.Bool("Enable dogleg", mleaderStyle.EnableDogleg));
            data.Add(new Snoop.Data.Bool("Enable block scale", mleaderStyle.EnableBlockScale));
            data.Add(new Snoop.Data.Bool("Enable frame text", mleaderStyle.EnableFrameText));
            data.Add(new Snoop.Data.Bool("Enable landing", mleaderStyle.EnableLanding));
            data.Add(new Snoop.Data.Object("First segment angle constraint", mleaderStyle.FirstSegmentAngleConstraint));
            data.Add(new Snoop.Data.Double("Landing gap", mleaderStyle.LandingGap));
            data.Add(new Snoop.Data.Object("Leader line color", mleaderStyle.LeaderLineColor));
            data.Add(new Snoop.Data.String("Leader line type", mleaderStyle.LeaderLineType.ToString()));
            data.Add(new Snoop.Data.ObjectId("Leader line type Id", mleaderStyle.LeaderLineTypeId));
            data.Add(new Snoop.Data.String("Leader line weight", mleaderStyle.LeaderLineWeight.ToString()));
            data.Add(new Snoop.Data.Int("Max leader segments points", mleaderStyle.MaxLeaderSegmentsPoints));
            data.Add(new Snoop.Data.String("Name", mleaderStyle.Name));
            data.Add(new Snoop.Data.Double("Scale", mleaderStyle.Scale));
            data.Add(new Snoop.Data.Object("Second segment angle constraint", mleaderStyle.SecondSegmentAngleConstraint));
            data.Add(new Snoop.Data.Bool("Text align always left", mleaderStyle.TextAlignAlwaysLeft));
            data.Add(new Snoop.Data.String("Text alignment type", mleaderStyle.TextAlignmentType.ToString()));
            data.Add(new Snoop.Data.String("Text angle type", mleaderStyle.TextAngleType.ToString()));
            data.Add(new Snoop.Data.String("Text attachment type", mleaderStyle.TextAttachmentType.ToString()));
            data.Add(new Snoop.Data.Object("Text color", mleaderStyle.TextColor));
            data.Add(new Snoop.Data.Double("Text height", mleaderStyle.TextHeight));
            data.Add(new Snoop.Data.ObjectId("Text style Id", mleaderStyle.TextStyleId));
        }

        private void
        Stream(ArrayList data, LinkedData linkData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LinkedData)));

            data.Add(new Snoop.Data.Bool("Is empty", linkData.IsEmpty));
            data.Add(new Snoop.Data.String("Name", linkData.Name));

            LinkedTableData linkTblData = linkData as LinkedTableData;
            if (linkTblData != null) {
                Stream(data, linkTblData);
                return;
            }
        }

        private void
        Stream(ArrayList data, LinkedTableData linkTblData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LinkedTableData)));

            data.Add(new Snoop.Data.Int("Number of columns", linkTblData.NumberOfColumns));
            data.Add(new Snoop.Data.Int("Number of rows", linkTblData.NumberOfRows));


            FormattedTableData formatTblData = linkTblData as FormattedTableData;
            if (formatTblData != null) {
                Stream(data, formatTblData);
                return;
            }
        }

        private void
        Stream(ArrayList data, FormattedTableData formatTblData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FormattedTableData)));

            // no data at this level     

            TableContent tblContent = formatTblData as TableContent;
            if (tblContent != null) {
                Stream(data, tblContent);
                return;
            }
        }

        private void
        Stream(ArrayList data, TableContent tblContent)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(TableContent)));

            data.Add(new Snoop.Data.ObjectId("Table style Id", tblContent.TableStyleId));           
        }
    }    
}
