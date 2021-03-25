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
using Autodesk.AutoCAD.ApplicationServices;

using MgdDbg.Snoop.Collectors;

using AcDb = Autodesk.AutoCAD.DatabaseServices;
using AcRx = Autodesk.AutoCAD.Runtime;

namespace MgdDbg.Snoop.CollectorExts {

    /// <summary>
    /// This is a Snoop Collector Extension object to collect data from RxObject objects.
    /// </summary>

    public class RxObject : CollectorExt {

        public
        RxObject()
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

            Autodesk.AutoCAD.Runtime.RXObject rxObj = e.ObjToSnoop as Autodesk.AutoCAD.Runtime.RXObject;
            if (rxObj != null) {
                Stream(snoopCollector.Data(), rxObj);
                return;
            }            
        }

            //  main branch for anything derived from RXObject (AcRxObject)
        private void
        Stream(ArrayList data, Autodesk.AutoCAD.Runtime.RXObject rxObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.Runtime.RXObject)));

            data.Add(new Snoop.Data.Class("Class", rxObj.GetType()));
            data.Add(new Snoop.Data.String("Rx class name", MgdDbg.Utils.AcadUi.ObjToRxClassStr(rxObj)));

                // branch to all types we are concerned with
            AcDb.Database db = rxObj as AcDb.Database;
            if (db != null) {
                Stream(data, db);
                return;
            }

            AcDb.HostApplicationServices hAppSrvs = rxObj as AcDb.HostApplicationServices;
            if (hAppSrvs != null) {
                Stream(data, hAppSrvs);
                return;
            }

            AcRx.DynamicLinker dLinker = rxObj as AcRx.DynamicLinker;
            if (dLinker != null) {
                Stream(data, dLinker);
                return;
            }

            AcRx.Dictionary dict = rxObj as AcRx.Dictionary;
            if (dict != null) {
                Stream(data, dict);
                return;
            }

            AcRx.RXClass rxClass = rxObj as AcRx.RXClass;
            if (rxClass != null) {
                Stream(data, rxClass);
                return;
            }          
        }


        private void
        Stream(ArrayList data, Autodesk.AutoCAD.DatabaseServices.Database db)
        {
            // Symbol Tables
            data.Add(new Snoop.Data.CategorySeparator("Symbol Tables"));
            data.Add(new Snoop.Data.ObjectId("Block table ID", db.BlockTableId));
            data.Add(new Snoop.Data.ObjectId("DimStyleTableId", db.DimStyleTableId));
            data.Add(new Snoop.Data.ObjectId("Layer table ID", db.LayerTableId));
            data.Add(new Snoop.Data.ObjectId("Linetype table ID", db.LinetypeTableId));
            data.Add(new Snoop.Data.ObjectId("RegApp table ID", db.RegAppTableId));
            data.Add(new Snoop.Data.ObjectId("Text style", db.Textstyle));
            data.Add(new Snoop.Data.ObjectId("Text style table ID", db.TextStyleTableId));
            data.Add(new Snoop.Data.ObjectId("UCS table ID", db.UcsTableId));
            data.Add(new Snoop.Data.ObjectId("Viewport table ID", db.ViewportTableId));
            data.Add(new Snoop.Data.ObjectId("View table ID", db.ViewTableId));

            // Dictionaries
            data.Add(new Snoop.Data.CategorySeparator("Dictionaries"));
            data.Add(new Snoop.Data.ObjectId("Color dictionary ID", db.ColorDictionaryId));
            data.Add(new Snoop.Data.ObjectId("Data link dictionary ID", db.DataLinkDictionaryId));
            data.Add(new Snoop.Data.ObjectId("Group dictionary ID", db.GroupDictionaryId));
            data.Add(new Snoop.Data.ObjectId("Layout dictionary ID", db.LayoutDictionaryId));
            data.Add(new Snoop.Data.ObjectId("Material dictionary ID", db.MaterialDictionaryId));
            data.Add(new Snoop.Data.ObjectId("MLine style dictionary ID", db.MLStyleDictionaryId));
            data.Add(new Snoop.Data.ObjectId("Named objects dictionary ID", db.NamedObjectsDictionaryId));
            data.Add(new Snoop.Data.ObjectId("Plot settings dictionary ID", db.PlotSettingsDictionaryId));
            data.Add(new Snoop.Data.ObjectId("Plot style name dictionary ID", db.PlotStyleNameDictionaryId));
            data.Add(new Snoop.Data.ObjectId("Table style dictionary ID", db.TableStyleDictionaryId));

            // Current settings
            data.Add(new Snoop.Data.CategorySeparator("Default SymbolRecs and DictionaryRecs"));
            data.Add(new Snoop.Data.ObjectId("Layer zero", db.LayerZero));
            data.Add(new Snoop.Data.ObjectId("ByBlock Linetype ID", db.ByBlockLinetype));
            data.Add(new Snoop.Data.ObjectId("ByLayer Linetype ID", db.ByLayerLinetype));
            data.Add(new Snoop.Data.ObjectId("Continuous linetype", db.ContinuousLinetype));
            data.Add(new Snoop.Data.ObjectId("Current space ID", db.CurrentSpaceId));
            data.Add(new Snoop.Data.ObjectId("Current viewport table record ID", db.CurrentViewportTableRecordId));
            data.Add(new Snoop.Data.ObjectId("Paperspace vport ID", db.PaperSpaceVportId));
            data.Add(new Snoop.Data.ObjectId("Dimstyle", db.Dimstyle));
            data.Add(new Snoop.Data.ObjectId("Table style", db.Tablestyle));
            data.Add(new Snoop.Data.String("Plot style name ID", db.PlotStyleNameId.ToString()));
            data.Add(new Snoop.Data.ObjectId("Section manager ID", db.SectionManagerId));

            // File information
            data.Add(new Snoop.Data.CategorySeparator("File Information"));
            data.Add(new Snoop.Data.Bitmap("Thumbnail", db.ThumbnailBitmap));
            data.Add(new Snoop.Data.Int("Update thumbnail", db.UpdateThumbnail));
            data.Add(new Snoop.Data.String("Filename", db.Filename));
            data.Add(new Snoop.Data.String("Fingerprint GUID", db.FingerprintGuid));
            data.Add(new Snoop.Data.String("Project name", db.ProjectName));
            data.Add(new Snoop.Data.String("Menu", db.Menu));
            data.Add(new Snoop.Data.Int("Approx. number of objects", db.ApproxNumObjects));
            data.Add(new Snoop.Data.Bool("Is partially opened", db.IsPartiallyOpened));
            data.Add(new Snoop.Data.Int("Number of saves", db.NumberOfSaves));
            data.Add(new Snoop.Data.Bool("Drawing file saved by Autodesk software", db.DwgFileWasSavedByAutodeskSoftware));
            data.Add(new Snoop.Data.String("Version GUID", db.VersionGuid));
            data.Add(new Snoop.Data.Object("Summary info", db.SummaryInfo));
            data.Add(new Snoop.Data.String("Last saved as maintenance version", db.LastSavedAsMaintenanceVersion.ToString()));
            data.Add(new Snoop.Data.String("Last saved as version", db.LastSavedAsVersion.ToString()));
            data.Add(new Snoop.Data.Int("Maintenance release version", db.MaintenanceReleaseVersion));
            data.Add(new Snoop.Data.String("Original file Name", db.OriginalFileName));
            data.Add(new Snoop.Data.String("Original file version", db.OriginalFileVersion.ToString()));
            data.Add(new Snoop.Data.String("Original file maintenance version", db.OriginalFileMaintenanceVersion.ToString()));
            data.Add(new Snoop.Data.String("Original file saved by version", db.OriginalFileSavedByVersion.ToString()));
            data.Add(new Snoop.Data.String("Original file saved by maintenance version", db.OriginalFileSavedByMaintenanceVersion.ToString()));
            data.Add(new Snoop.Data.Bool("Retain original thumbnail bitmap", db.RetainOriginalThumbnailBitmap));
            data.Add(new Snoop.Data.Int("Save proxy graphics", db.Saveproxygraphics));

            data.Add(new Snoop.Data.ObjectIdCollection("Dim recent style list", db.GetDimRecentStyleList()));

            // System variables
            data.Add(new Snoop.Data.CategorySeparator("System Variables"));
            data.Add(new Snoop.Data.Bool("Allow extended names", db.AllowExtendedNames));
            data.Add(new Snoop.Data.Angle("Angbase", db.Angbase));
            data.Add(new Snoop.Data.Bool("Angdir", db.Angdir));
            data.Add(new Snoop.Data.Bool("AnnoAllVisible", db.AnnoAllVisible));
            data.Add(new Snoop.Data.Bool("AnnotativeDwg", db.AnnotativeDwg));
            data.Add(new Snoop.Data.Int("Attmode", db.Attmode));
            data.Add(new Snoop.Data.Int("Aunits", db.Aunits));
            data.Add(new Snoop.Data.Int("Auprec", db.Auprec));
            data.Add(new Snoop.Data.Bool("Camera display", db.CameraDisplay));
            data.Add(new Snoop.Data.Distance("Camera height", db.CameraHeight));
            data.Add(new Snoop.Data.Object("Cannoscale", db.Cannoscale));
            data.Add(new Snoop.Data.ObjectToString("Cecolor", db.Cecolor));
            data.Add(new Snoop.Data.Distance("Celtscale", db.Celtscale));
            data.Add(new Snoop.Data.ObjectId("Celtype", db.Celtype));
            data.Add(new Snoop.Data.String("Celweight", db.Celweight.ToString()));
            data.Add(new Snoop.Data.Object("Cetransparency", db.Cetransparency));
            data.Add(new Snoop.Data.Distance("Chamfera", db.Chamfera));
            data.Add(new Snoop.Data.Distance("Chamferb", db.Chamferb));
            data.Add(new Snoop.Data.Distance("Chamferc", db.Chamferc));
            data.Add(new Snoop.Data.Distance("Chamferd", db.Chamferd));
            data.Add(new Snoop.Data.ObjectId("Celayer", db.Clayer));
            data.Add(new Snoop.Data.ObjectId("Cmaterial", db.Cmaterial));
            data.Add(new Snoop.Data.Int("Cmljust", db.Cmljust));
            data.Add(new Snoop.Data.Distance("Cmlscale", db.Cmlscale));
            data.Add(new Snoop.Data.ObjectId("CmlstyleID", db.CmlstyleID));
            data.Add(new Snoop.Data.Int("Cshadow", db.Cshadow));
            data.Add(new Snoop.Data.Object("Data link manager", db.DataLinkManager));
            data.Add(new Snoop.Data.Int("DgnFrame", db.DgnFrame));
            data.Add(new Snoop.Data.Int("Dimadec", db.Dimadec));
            data.Add(new Snoop.Data.Bool("Dimalt", db.Dimalt));
            data.Add(new Snoop.Data.Int("Dimaltd", db.Dimaltd));
            data.Add(new Snoop.Data.Distance("Dimaltf", db.Dimaltf));
            data.Add(new Snoop.Data.Distance("Dimaltrnd", db.Dimaltrnd));
            data.Add(new Snoop.Data.Int("Dimalttd", db.Dimalttd));
            data.Add(new Snoop.Data.Int("Dimalttz", db.Dimalttz));
            data.Add(new Snoop.Data.Int("Dimaltu", db.Dimaltu));
            data.Add(new Snoop.Data.Int("Dimaltz", db.Dimaltz));
            data.Add(new Snoop.Data.String("Dimapost", db.Dimapost));
            data.Add(new Snoop.Data.Int("Dimarcsym", db.Dimarcsym));
            data.Add(new Snoop.Data.Bool("Dimaso", db.Dimaso));
            data.Add(new Snoop.Data.Int("Dimassoc", db.DimAssoc));
            data.Add(new Snoop.Data.Distance("Dimasz", db.Dimasz));
            data.Add(new Snoop.Data.Int("Dimatfit", db.Dimatfit));
            data.Add(new Snoop.Data.Int("Dimaunit", db.Dimaunit));
            data.Add(new Snoop.Data.Int("Dimazin", db.Dimazin));
            data.Add(new Snoop.Data.ObjectId("Dimblk", db.Dimblk));
            data.Add(new Snoop.Data.ObjectId("Dimblk1", db.Dimblk1));
            data.Add(new Snoop.Data.ObjectId("Dimblk2", db.Dimblk2));
            data.Add(new Snoop.Data.ObjectId("Dimldrblk", db.Dimldrblk));
            data.Add(new Snoop.Data.Distance("Dimcen", db.Dimcen));
            data.Add(new Snoop.Data.Object("Dimclrd", db.Dimclrd));
            data.Add(new Snoop.Data.Object("Dimclre", db.Dimclre));
            data.Add(new Snoop.Data.Object("Dimclrt", db.Dimclrt));
            data.Add(new Snoop.Data.Int("Dimdec", db.Dimdec));
            data.Add(new Snoop.Data.Distance("Dimdle", db.Dimdle));
            data.Add(new Snoop.Data.Distance("Dimdli", db.Dimdli));
            data.Add(new Snoop.Data.String("Dimdsep", db.Dimdsep.ToString()));
            data.Add(new Snoop.Data.Distance("Dimexe", db.Dimexe));
            data.Add(new Snoop.Data.Distance("Dimexo", db.Dimexo));
            data.Add(new Snoop.Data.Int("Dimfrac", db.Dimfrac));
            data.Add(new Snoop.Data.Distance("Dimfxlen", db.Dimfxlen));
            data.Add(new Snoop.Data.Bool("Dimfxlenon", db.DimfxlenOn));
            data.Add(new Snoop.Data.Distance("Dimgap", db.Dimgap));
            data.Add(new Snoop.Data.Angle("Dimjogang", db.Dimjogang));
            data.Add(new Snoop.Data.Int("Dimjust", db.Dimjust));
            data.Add(new Snoop.Data.Distance("Dimlfac", db.Dimlfac));
            data.Add(new Snoop.Data.Bool("Dimlim", db.Dimlim));
            data.Add(new Snoop.Data.ObjectId("Dimltex1", db.Dimltex1));
            data.Add(new Snoop.Data.ObjectId("Dimltex2", db.Dimltex2));
            data.Add(new Snoop.Data.ObjectId("Dimltype", db.Dimltype));
            data.Add(new Snoop.Data.Int("Dimlunit", db.Dimlunit));
            data.Add(new Snoop.Data.String("Dimlwd", db.Dimlwd.ToString()));
            data.Add(new Snoop.Data.String("Dimlwe", db.Dimlwe.ToString()));
            data.Add(new Snoop.Data.String("Dimpost", db.Dimpost));
            data.Add(new Snoop.Data.Distance("Dimrnd", db.Dimrnd));
            data.Add(new Snoop.Data.Bool("Dimsah", db.Dimsah));
            data.Add(new Snoop.Data.Distance("Dimscale", db.Dimscale));
            data.Add(new Snoop.Data.Bool("Dimsd1", db.Dimsd1));
            data.Add(new Snoop.Data.Bool("Dimsd2", db.Dimsd2));
            data.Add(new Snoop.Data.Bool("Dimse1", db.Dimse1));
            data.Add(new Snoop.Data.Bool("Dimse2", db.Dimse2));
            data.Add(new Snoop.Data.Bool("Dimsoxd", db.Dimsoxd));
            data.Add(new Snoop.Data.ObjectId("Dimstyle", db.Dimstyle));
            data.Add(new Snoop.Data.Int("Dimtad", db.Dimtad));
            data.Add(new Snoop.Data.Int("Dimtdec", db.Dimtdec));
            data.Add(new Snoop.Data.Distance("Dimtfac", db.Dimtfac));
            data.Add(new Snoop.Data.Int("Dimtfill", db.Dimtfill));
            data.Add(new Snoop.Data.Object("Dimtfillclr", db.Dimtfillclr));
            data.Add(new Snoop.Data.Bool("Dimtih", db.Dimtih));
            data.Add(new Snoop.Data.Bool("Dimtix", db.Dimtix));
            data.Add(new Snoop.Data.Distance("Dimtm", db.Dimtm));
            data.Add(new Snoop.Data.Int("Dimtmove", db.Dimtmove));
            data.Add(new Snoop.Data.Bool("Dimtofl", db.Dimtofl));
            data.Add(new Snoop.Data.Bool("Dimtoh", db.Dimtoh));
            data.Add(new Snoop.Data.Bool("Dimtol", db.Dimtol));
            data.Add(new Snoop.Data.Int("Dimtolj", db.Dimtolj));
            data.Add(new Snoop.Data.Distance("Dimtp", db.Dimtp));
            data.Add(new Snoop.Data.Distance("Dimtsz", db.Dimtsz));
            data.Add(new Snoop.Data.Distance("Dimtvp", db.Dimtvp));
            data.Add(new Snoop.Data.ObjectId("Dimtxsty", db.Dimtxsty));
            data.Add(new Snoop.Data.Distance("Dimtxt", db.Dimtxt));
            data.Add(new Snoop.Data.Int("Dimtzin", db.Dimtzin));
            data.Add(new Snoop.Data.Bool("Dimupt", db.Dimupt));
            data.Add(new Snoop.Data.Int("Dimzin", db.Dimzin));
            data.Add(new Snoop.Data.Bool("DispSilh", db.DispSilh));
            data.Add(new Snoop.Data.ObjectId("Dragvs", db.dragvs));
            data.Add(new Snoop.Data.String("Draw order control", db.DrawOrderCtl.ToString()));
            data.Add(new Snoop.Data.Int("Dwf frame", db.DwfFrame));
            data.Add(new Snoop.Data.Int("Dx eval", db.DxEval));
            data.Add(new Snoop.Data.Distance("Elevation", db.Elevation));
            data.Add(new Snoop.Data.String("End caps", db.EndCaps.ToString()));
            data.Add(new Snoop.Data.Point3d("Extmax", db.Extmax));
            data.Add(new Snoop.Data.Point3d("Extmin", db.Extmin));
            data.Add(new Snoop.Data.Double("Facetres", db.Facetres));
            data.Add(new Snoop.Data.Distance("Filletrad", db.Filletrad));
            data.Add(new Snoop.Data.Bool("Fillmode", db.Fillmode));

            try {
                data.Add(new Snoop.Data.ObjectId("Geo data object", db.GeoDataObject));     // Fails when null instead of just returning null ObjectID
            }
            catch (Autodesk.AutoCAD.Runtime.Exception e) {
                data.Add(new Snoop.Data.Exception("Geo data object", e));   //  returns eKeyNotFound
            }

            data.Add(new Snoop.Data.Int("Halo gap", db.HaloGap));
            data.Add(new Snoop.Data.String("Handseed", db.Handseed.ToString()));
            data.Add(new Snoop.Data.Int("Hide text", db.HideText));

            try {
                data.Add(new Snoop.Data.Object("Home view", db.HomeView));    // Fails when null instead of just returning null
            }
            catch (Autodesk.AutoCAD.Runtime.Exception e) {
                data.Add(new Snoop.Data.Exception("Home view", e));         // return eKeyNotFound
            }
           
            data.Add(new Snoop.Data.Bool("Hp inherit", db.HpInherit));
            data.Add(new Snoop.Data.Point2d("Hp origin", db.HpOrigin));
            data.Add(new Snoop.Data.String("Hyperlink base", db.HyperlinkBase));
            data.Add(new Snoop.Data.Int("Indexctl", db.Indexctl));
            data.Add(new Snoop.Data.Point3d("Insbase", db.Insbase));
            data.Add(new Snoop.Data.String("Insunits", db.Insunits.ToString()));
            data.Add(new Snoop.Data.Object("Interfere color", db.Interferecolor));
            data.Add(new Snoop.Data.ObjectId("Interfereobjvs", db.Interfereobjvs));
            data.Add(new Snoop.Data.ObjectId("Interferevpvs", db.Interferevpvs));
            data.Add(new Snoop.Data.Int("Intersect color", db.IntersectColor));
            data.Add(new Snoop.Data.Int("Intersect display", db.IntersectDisplay));
          //  data.Add(new Snoop.Data.Bool("IsEmr", db.IsEmr));
            data.Add(new Snoop.Data.Int("Isolines", db.Isolines));
            data.Add(new Snoop.Data.String("Join style", db.JoinStyle.ToString()));
            data.Add(new Snoop.Data.Double("Latitude", db.Latitude));
            data.Add(new Snoop.Data.Int("Layer eval", db.LayerEval));
            data.Add(new Snoop.Data.Object("Layer filters", db.LayerFilters));
            data.Add(new Snoop.Data.Int("Layer notify", db.LayerNotify));
            data.Add(new Snoop.Data.Object("Layer state manager", db.LayerStateManager));
            data.Add(new Snoop.Data.Double("Lens length", db.LensLength));
            data.Add(new Snoop.Data.Int("Light glyph display", db.LightGlyphDisplay));
            data.Add(new Snoop.Data.String("Lighting units", db.LightingUnits.ToString()));
            data.Add(new Snoop.Data.Bool("Lights in blocks", db.LightsInBlocks));
            data.Add(new Snoop.Data.Bool("Limcheck", db.Limcheck));
            data.Add(new Snoop.Data.Point2d("Limmax", db.Limmax));
            data.Add(new Snoop.Data.Point2d("Limmin", db.Limmin));
            data.Add(new Snoop.Data.Bool("Lineweight display", db.LineWeightDisplay));
            data.Add(new Snoop.Data.Angle("Loft ang 1", db.LoftAng1));
            data.Add(new Snoop.Data.Angle("Loft ang 2", db.LoftAng2));
            data.Add(new Snoop.Data.Double("Loft mag 1", db.LoftMag1));
            data.Add(new Snoop.Data.Double("Loft mag 2", db.LoftMag2));
            data.Add(new Snoop.Data.Int("Loft normals", db.LoftNormals));
            data.Add(new Snoop.Data.Int("Loft param", db.LoftParam));
            data.Add(new Snoop.Data.Double("Longitude", db.Longitude));
            data.Add(new Snoop.Data.Distance("Ltscale", db.Ltscale));
            data.Add(new Snoop.Data.Int("Lunits", db.Lunits));
            data.Add(new Snoop.Data.Int("Luprec", db.Luprec));
            data.Add(new Snoop.Data.Int("Maxactvp", db.Maxactvp));
            data.Add(new Snoop.Data.String("Measurement", db.Measurement.ToString()));
            data.Add(new Snoop.Data.Bool("Mirrtext", db.Mirrtext));
            data.Add(new Snoop.Data.Bool("MsLt scale", db.MsLtScale));
            data.Add(new Snoop.Data.Double("MsOleScale", db.MsOleScale));
            data.Add(new Snoop.Data.Double("North direction", db.NorthDirection));
            data.Add(new Snoop.Data.Object("Object context manager", db.ObjectContextManager));
            data.Add(new Snoop.Data.Int("Obscured color", db.ObscuredColor));
            data.Add(new Snoop.Data.Int("Obscured linetype", db.ObscuredLineType));
            data.Add(new Snoop.Data.Bool("OleStartUp", db.OleStartUp));
            data.Add(new Snoop.Data.Bool("Orthomode", db.Orthomode));
            data.Add(new Snoop.Data.Int("Pdf frame", db.PdfFrame));
            data.Add(new Snoop.Data.Int("Pdmode", db.Pdmode));
            data.Add(new Snoop.Data.Distance("Pdsize", db.Pdsize));
            data.Add(new Snoop.Data.Distance("Pelevation", db.Pelevation));
            data.Add(new Snoop.Data.Point3d("Pextmax", db.Pextmax));
            data.Add(new Snoop.Data.Point3d("Pextmin", db.Pextmin));
            data.Add(new Snoop.Data.Point3d("Pinsbase", db.Pinsbase));
            data.Add(new Snoop.Data.Bool("Plimcheck", db.Plimcheck));
            data.Add(new Snoop.Data.Point2d("Plimmax", db.Plimmax));
            data.Add(new Snoop.Data.Point2d("Plimmin", db.Plimmin));
            data.Add(new Snoop.Data.Bool("PlineEllipse", db.PlineEllipse));
            data.Add(new Snoop.Data.Bool("Plinegen", db.Plinegen));
            data.Add(new Snoop.Data.Distance("Plinewid", db.Plinewid));
            data.Add(new Snoop.Data.Bool("Plot style mode", db.PlotStyleMode));
            data.Add(new Snoop.Data.Bool("Psltscale", db.Psltscale));
            data.Add(new Snoop.Data.Distance("Psol height", db.PsolHeight));
            data.Add(new Snoop.Data.Distance("Psol width", db.PsolWidth));
            data.Add(new Snoop.Data.ObjectId("PucsBase", db.PucsBase));
            data.Add(new Snoop.Data.ObjectId("Pucsname", db.Pucsname));
            data.Add(new Snoop.Data.Point3d("Pucsorg", db.Pucsorg));
            data.Add(new Snoop.Data.String("PucsOrthographic", db.PucsOrthographic.ToString()));
            data.Add(new Snoop.Data.Vector3d("Pucsxdir", db.Pucsxdir));
            data.Add(new Snoop.Data.Vector3d("Pucsydir", db.Pucsydir));
            data.Add(new Snoop.Data.Bool("Qtextmode", db.Qtextmode));
            data.Add(new Snoop.Data.Bool("Regenmode", db.Regenmode));
            data.Add(new Snoop.Data.Object("Security parameters", db.SecurityParameters));
            data.Add(new Snoop.Data.Int("Shadedge", db.Shadedge));
            data.Add(new Snoop.Data.Int("Shadedif", db.Shadedif));
            data.Add(new Snoop.Data.Distance("Shadow plane location", db.ShadowPlaneLocation));
            data.Add(new Snoop.Data.Int("Show hist", db.ShowHist));
            data.Add(new Snoop.Data.Distance("Sketchinc", db.Sketchinc));
            data.Add(new Snoop.Data.Bool("Skpoly", db.Skpoly));
            data.Add(new Snoop.Data.Int("Sortents", db.SortEnts));
            data.Add(new Snoop.Data.Int("Solid hist", db.SolidHist));
            data.Add(new Snoop.Data.Bool("Splframe", db.Splframe));
            data.Add(new Snoop.Data.Int("Splinesegs", db.Splinesegs));
            data.Add(new Snoop.Data.Int("Splinetype", db.Splinetype));
            data.Add(new Snoop.Data.Distance("Step size", db.StepSize));
            data.Add(new Snoop.Data.Double("Step per sec", db.StepsPerSec));
            data.Add(new Snoop.Data.String("Style sheet", db.StyleSheet));
            data.Add(new Snoop.Data.Int("Surftab1", db.Surftab1));
            data.Add(new Snoop.Data.Int("Surftab2", db.Surftab2));
            data.Add(new Snoop.Data.Int("Surftype", db.Surftype));
            data.Add(new Snoop.Data.Int("Surfu", db.Surfu));
            data.Add(new Snoop.Data.Int("Surfv", db.Surfv));
            data.Add(new Snoop.Data.String("Tdcreate", db.Tdcreate.ToString()));
            data.Add(new Snoop.Data.String("Tdindwg", db.Tdindwg.ToString()));
            data.Add(new Snoop.Data.String("Tducreate", db.Tducreate.ToString()));
            data.Add(new Snoop.Data.String("Tdupdate", db.Tdupdate.ToString()));
            data.Add(new Snoop.Data.String("Tdusrtimer", db.Tdusrtimer.ToString()));
            data.Add(new Snoop.Data.String("Tduupdate", db.Tduupdate.ToString()));
            data.Add(new Snoop.Data.Distance("Textsize", db.Textsize));
            data.Add(new Snoop.Data.Distance("Thickness", db.Thickness));
            data.Add(new Snoop.Data.Bool("Tilemode", db.TileMode));
            data.Add(new Snoop.Data.Int("Tilemode light synch", db.TileModeLightSynch));
            data.Add(new Snoop.Data.String("Time zone", db.TimeZone.ToString()));
            data.Add(new Snoop.Data.Distance("Tracewid", db.Tracewid));
            data.Add(new Snoop.Data.Int("Treedepth", db.Treedepth));
            data.Add(new Snoop.Data.Int("TStackAlign", db.TStackAlign));
            data.Add(new Snoop.Data.Int("TStackSize", db.TstackSize));
            data.Add(new Snoop.Data.ObjectId("UcsBase", db.UcsBase));
            data.Add(new Snoop.Data.ObjectId("Ucsname", db.Ucsname));
            data.Add(new Snoop.Data.Point3d("Ucsorg", db.Ucsorg));
            data.Add(new Snoop.Data.String("UcsOrthographic", db.UcsOrthographic.ToString()));
            data.Add(new Snoop.Data.Vector3d("Ucsxdir", db.Ucsxdir));
            data.Add(new Snoop.Data.Vector3d("Ucsydir", db.Ucsydir));
            data.Add(new Snoop.Data.Bool("Undo recoding", db.UndoRecording));
            data.Add(new Snoop.Data.Int("Unitmode", db.Unitmode));
            data.Add(new Snoop.Data.Int("Useri1", db.Useri1));
            data.Add(new Snoop.Data.Int("Useri2", db.Useri2));
            data.Add(new Snoop.Data.Int("Useri3", db.Useri3));
            data.Add(new Snoop.Data.Int("Useri4", db.Useri4));
            data.Add(new Snoop.Data.Int("Useri5", db.Useri5));
            data.Add(new Snoop.Data.Double("Userr1", db.Userr1));
            data.Add(new Snoop.Data.Double("Userr2", db.Userr2));
            data.Add(new Snoop.Data.Double("Userr3", db.Userr3));
            data.Add(new Snoop.Data.Double("Userr4", db.Userr4));
            data.Add(new Snoop.Data.Double("Userr5", db.Userr5));
            data.Add(new Snoop.Data.Bool("Usrtimer", db.Usrtimer));
            data.Add(new Snoop.Data.Distance("Viewport scale default", db.ViewportScaleDefault));
            data.Add(new Snoop.Data.Bool("Visretain", db.Visretain));
            data.Add(new Snoop.Data.Bool("Worldview", db.Worldview));
            data.Add(new Snoop.Data.Int("Xclip frame", db.XclipFrame));
            data.Add(new Snoop.Data.ObjectId("Xref block ID", db.XrefBlockId));
            data.Add(new Snoop.Data.Bool("Xref edit enabled", db.XrefEditEnabled));
            // TBD: GetHostDwgXrefGraph()

            data.Add(new Snoop.Data.ObjectIdCollection("Viewports (includePspace = true)", db.GetViewports(true)));
            data.Add(new Snoop.Data.ObjectIdCollection("Viewports (includePspace = false)", db.GetViewports(false)));
            // TBD: GetVisualStyleList();
        }

        /// <summary>
        /// Collect data about host app services
        /// </summary>
        
        private void
        Stream(ArrayList data, AcDb.HostApplicationServices hAppSrvs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcDb.HostApplicationServices)));

            data.Add(new Snoop.Data.String("Alternate font name", hAppSrvs.AlternateFontName));
            data.Add(new Snoop.Data.String("Font map file name", hAppSrvs.FontMapFileName));
            data.Add(new Snoop.Data.String("Roamable root folder", hAppSrvs.RoamableRootFolder));
            data.Add(new Snoop.Data.String("Local root folder", hAppSrvs.LocalRootFolder));
            data.Add(new Snoop.Data.String("Program", hAppSrvs.Program));
            data.Add(new Snoop.Data.String("Product", hAppSrvs.Product));
            data.Add(new Snoop.Data.String("Company name", hAppSrvs.CompanyName));
#if(AC2012)
#else
            data.Add(new Snoop.Data.String("Registry product root key", hAppSrvs.MachineRegistryProductRootKey));
#endif
            data.Add(new Snoop.Data.String("Modeler flavor", hAppSrvs.ModelerFlavor.ToString()));
            data.Add(new Snoop.Data.Database("Working database", AcDb.HostApplicationServices.WorkingDatabase));
            data.Add(new Snoop.Data.Object("Current", AcDb.HostApplicationServices.Current));
        }

        private void
        Stream(ArrayList data, AcRx.DynamicLinker dLinker)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcRx.DynamicLinker)));

#if(AC2012)
#else
            //data.Add(new Snoop.Data.String("Product key", dLinker.ProductKey));
#endif
            data.Add(new Snoop.Data.Int("Product license ID", dLinker.ProductLcid));

            // TBD:  GetLoadedModules();
        }

        private void
        Stream(ArrayList data, AcRx.Dictionary dict)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcRx.Dictionary)));

            data.Add(new Snoop.Data.Int("Count", dict.Count));
            data.Add(new Snoop.Data.Bool("Deletes objects", dict.DeletesObjects));
            data.Add(new Snoop.Data.Bool("Is case sensitive", dict.IsCaseSensitive));
            data.Add(new Snoop.Data.Bool("Is sorted", dict.IsSorted));
        }

        private void
        Stream(ArrayList data, AcRx.RXClass rxClass)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcRx.RXClass)));

            data.Add(new Snoop.Data.String("Name", rxClass.Name));
            data.Add(new Snoop.Data.String("Appname", rxClass.AppName));
            data.Add(new Snoop.Data.String("DXF name", rxClass.DxfName));
            data.Add(new Snoop.Data.Object("Class version", rxClass.ClassVersion));
            data.Add(new Snoop.Data.Object("My parent", rxClass.MyParent));
            data.Add(new Snoop.Data.Int("Proxy flags", rxClass.ProxyFlags));
        }      
    }
}
