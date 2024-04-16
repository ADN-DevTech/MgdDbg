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
    /// This is a Snoop Collector Extension object to collect data from SymbolTable objects.
    /// </summary>
    
    public class SymbolTable : CollectorExt {
        
        public
        SymbolTable()
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
            AcDb.SymbolTableRecord tblRec = e.ObjToSnoop as AcDb.SymbolTableRecord;
            if (tblRec != null) {
                Stream(snoopCollector.Data(), tblRec);
                return;
            }

            AcDb.SymbolTable symTbl = e.ObjToSnoop as AcDb.SymbolTable; // need to use namespace or it thinks we are referring to this class "SymbolTable" instead!
            if (symTbl != null) {
                Stream(snoopCollector.Data(), symTbl);
                return;
            }
        }

        #region SymbolTableRecords

        private void
        Stream(ArrayList data, AcDb.SymbolTableRecord tblRec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcDb.SymbolTableRecord)));

            data.Add(new Snoop.Data.String("Name", tblRec.Name));
            data.Add(new Snoop.Data.Bool("Is dependent", tblRec.IsDependent));
            data.Add(new Snoop.Data.Bool("Is resolved", tblRec.IsResolved));
            
                // branch to all known major sub-classes
            AbstractViewTableRecord viewRec = tblRec as AbstractViewTableRecord;
            if (viewRec != null) {
                Stream(data, viewRec);
                return;
            }

            BlockTableRecord blkRec = tblRec as BlockTableRecord;
            if (blkRec != null) {
                Stream(data, blkRec);
                return;
            }
            
            DimStyleTableRecord dimRec = tblRec as DimStyleTableRecord;
            if (dimRec != null) {
                Stream(data, dimRec);
                return;
            }

            LayerTableRecord layRec = tblRec as LayerTableRecord;
            if (layRec != null) {
                Stream(data, layRec);
                return;
            }
            
            LinetypeTableRecord ltypeRec = tblRec as LinetypeTableRecord;
            if (ltypeRec != null) {
                Stream(data, ltypeRec);
                return;
            }
            
            TextStyleTableRecord textRec = tblRec as TextStyleTableRecord;
            if (textRec != null) {
                Stream(data, textRec);
                return;
            }
            
            UcsTableRecord ucsRec = tblRec as UcsTableRecord;
            if (ucsRec != null) {
                Stream(data, ucsRec);
                return;
            }

        }
        
        private void
        Stream(ArrayList data, AbstractViewTableRecord rec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AbstractViewTableRecord)));

            data.Add(new Snoop.Data.Point2d("Center point", rec.CenterPoint));
            data.Add(new Snoop.Data.Distance("Width", rec.Width));
            data.Add(new Snoop.Data.Distance("Height", rec.Height));
            data.Add(new Snoop.Data.Point3d("Target", rec.Target));
            data.Add(new Snoop.Data.Vector3d("View direction", rec.ViewDirection));
            data.Add(new Snoop.Data.Distance("View twist", rec.ViewTwist));
            data.Add(new Snoop.Data.Double("Lens length", rec.LensLength));
            data.Add(new Snoop.Data.Bool("Front clip at eye", rec.FrontClipAtEye));
            data.Add(new Snoop.Data.Distance("Front clip distance", rec.FrontClipDistance));
            data.Add(new Snoop.Data.Distance("Back clip distance", rec.BackClipDistance));
            data.Add(new Snoop.Data.Bool("Front clip enabled", rec.FrontClipEnabled));
            data.Add(new Snoop.Data.Bool("Back clip enabled", rec.BackClipEnabled));
            data.Add(new Snoop.Data.Distance("Elevation", rec.Elevation));
            data.Add(new Snoop.Data.Bool("Perspective enabled", rec.PerspectiveEnabled));
           // data.Add(new Snoop.Data.String("Render mode", rec.RenderMode.ToString()));
            data.Add(new Snoop.Data.Object("UCS", rec.Ucs));
            data.Add(new Snoop.Data.ObjectId("UCS name", rec.UcsName));
            data.Add(new Snoop.Data.String("UCS orthographic", rec.UcsOrthographic.ToString()));
            data.Add(new Snoop.Data.String("View orthographic", rec.ViewOrthographic.ToString()));

            data.Add(new Snoop.Data.Object("Ambient light color", rec.AmbientLightColor));
            data.Add(new Snoop.Data.ObjectId("Background ID", rec.Background));
            data.Add(new Snoop.Data.Double("Brightness", rec.Brightness));
            data.Add(new Snoop.Data.Double("Contrast", rec.Contrast));
            data.Add(new Snoop.Data.Bool("Default lighting on", rec.DefaultLightingOn));
            data.Add(new Snoop.Data.String("Default lighting type", rec.DefaultLightingType.ToString()));
            data.Add(new Snoop.Data.ObjectId("Sun ID", rec.SunId));
            data.Add(new Snoop.Data.ObjectId("Visual style ID", rec.VisualStyleId));

            ViewportTableRecord viewportRec = rec as ViewportTableRecord;
            if (viewportRec != null) {
                Stream(data, viewportRec);
                return;
            }
            
            ViewTableRecord viewRec = rec as ViewTableRecord;
            if (viewRec != null) {
                Stream(data, viewRec);
                return;
            }
        }
        
        private void
        Stream(ArrayList data, ViewportTableRecord rec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ViewportTableRecord)));

            data.Add(new Snoop.Data.Point2d("Lower left corner", rec.LowerLeftCorner));
            data.Add(new Snoop.Data.Point2d("Upper right corner", rec.UpperRightCorner));
            data.Add(new Snoop.Data.Int("Circle sides", rec.CircleSides));
            data.Add(new Snoop.Data.Bool("Fast zooms enabled", rec.FastZoomsEnabled));
            data.Add(new Snoop.Data.Bool("Grid enabled", rec.GridEnabled));
            data.Add(new Snoop.Data.Bool("Grid adaptive", rec.GridAdaptive));
            data.Add(new Snoop.Data.Bool("Grid bound to limits", rec.GridBoundToLimits));
            data.Add(new Snoop.Data.Bool("Grid follow", rec.GridFollow));
            data.Add(new Snoop.Data.Int("Grid major", rec.GridMajor));
            data.Add(new Snoop.Data.Bool("Grid sub-division restricted", rec.GridSubdivisionRestricted));
            data.Add(new Snoop.Data.Point2d("Grid increments", rec.GridIncrements));
            data.Add(new Snoop.Data.Bool("Icon enabled", rec.IconEnabled));
            data.Add(new Snoop.Data.Bool("Icon at origin", rec.IconAtOrigin));
            data.Add(new Snoop.Data.Bool("Isometric snap enabled", rec.IsometricSnapEnabled));
            data.Add(new Snoop.Data.Bool("Snap enabled", rec.SnapEnabled));
            data.Add(new Snoop.Data.Point2d("Snap increments", rec.SnapIncrements));
            data.Add(new Snoop.Data.Int("Snap pair", rec.SnapPair));
            data.Add(new Snoop.Data.Bool("UCS follow mode", rec.UcsFollowMode));
            data.Add(new Snoop.Data.Bool("UCS saved with viewport", rec.UcsSavedWithViewport));
        }
        
        private void
        Stream(ArrayList data, ViewTableRecord rec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ViewTableRecord)));

            data.Add(new Snoop.Data.String("Cateogry name", rec.CategoryName));
            data.Add(new Snoop.Data.Bool("Is paper space view", rec.IsPaperspaceView));
            data.Add(new Snoop.Data.Bool("Is UCS assoc. to view", rec.IsUcsAssociatedToView));
            data.Add(new Snoop.Data.Bool("View assoc. to viewport", rec.ViewAssociatedToViewport));
            data.Add(new Snoop.Data.String("Layer state", rec.LayerState));
            data.Add(new Snoop.Data.ObjectId("Layout", rec.Layout));
            data.Add(new Snoop.Data.ObjectId("Live section ID", rec.LiveSection));
            data.Add(new Snoop.Data.Bitmap("Thumbnail", rec.Thumbnail));
        }

        private void
        Stream(ArrayList data, BlockTableRecord rec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BlockTableRecord)));

            data.Add(new Snoop.Data.String("Path name", rec.PathName));
            data.Add(new Snoop.Data.String("Comments", rec.Comments));
            data.Add(new Snoop.Data.Point3d("Origin", rec.Origin));
            data.Add(new Snoop.Data.ObjectId("Layout ID", rec.LayoutId));
            data.Add(new Snoop.Data.Bool("Has attribute definitions", rec.HasAttributeDefinitions));
            data.Add(new Snoop.Data.Bool("Has preview icon", rec.HasPreviewIcon));
            //if (rec.HasPreviewIcon)
            //    data.Add(new Snoop.Data.Bitmap("Preview icon", rec.PreviewIcon));   // TBD: throws exception, Not Implemented!
            data.Add(new Snoop.Data.String("Block scaling", rec.BlockScaling.ToString()));
            data.Add(new Snoop.Data.Bool("Explodable", rec.Explodable));
            data.Add(new Snoop.Data.String("Units", rec.Units.ToString()));
            data.Add(new Snoop.Data.Bool("Is anonymous", rec.IsAnonymous));
            data.Add(new Snoop.Data.Bool("Is dynamic block", rec.IsDynamicBlock));
            data.Add(new Snoop.Data.Bool("Is from external reference", rec.IsFromExternalReference));
            data.Add(new Snoop.Data.Bool("Is from overlay reference", rec.IsFromOverlayReference));
            data.Add(new Snoop.Data.Bool("Is layout", rec.IsLayout));
            data.Add(new Snoop.Data.Bool("Is unloaded", rec.IsUnloaded));
            data.Add(new Snoop.Data.ObjectId("Draw order table ID", rec.DrawOrderTableId));
            data.Add(new Snoop.Data.ObjectId("BlockBegin ID", rec.BlockBeginId));
            data.Add(new Snoop.Data.ObjectId("BlockEnd ID", rec.BlockEndId));
            data.Add(new Snoop.Data.ObjectIdCollection("Entities within block", MgdDbg.Utils.SymTbl.CollectBlockEnts(rec)));
            data.Add(new Snoop.Data.ObjectIdCollection("Block reference IDs (directOnly = true)", rec.GetBlockReferenceIds(true, false)));
            data.Add(new Snoop.Data.ObjectIdCollection("Block reference IDs (directOnly = false)", rec.GetBlockReferenceIds(false, false)));
            data.Add(new Snoop.Data.ObjectIdCollection("Block reference IDs (Erased)", rec.GetErasedBlockReferenceIds()));
            data.Add(new Snoop.Data.String("Xref status", rec.XrefStatus.ToString()));
            data.Add(new Snoop.Data.Database("Xref database", rec.GetXrefDatabase(true)));
        }
        
        private void
        Stream(ArrayList data, DimStyleTableRecord rec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DimStyleTableRecord)));
          
            data.Add(new Snoop.Data.ObjectId("First arrow", rec.GetArrowId(DimArrowFlag.FirstArrow)));
            data.Add(new Snoop.Data.ObjectId("Second arrow", rec.GetArrowId(DimArrowFlag.SecondArrow)));
            data.Add(new Snoop.Data.Bool("Is modified for recompute", rec.IsModifiedForRecompute));

            data.Add(new Snoop.Data.CategorySeparator("DIMVARS"));

                // dimvars
            data.Add(new Snoop.Data.Int("Dimadec", rec.Dimadec));
            data.Add(new Snoop.Data.Bool("Dimalt", rec.Dimalt));
            data.Add(new Snoop.Data.Int("Dimaltd", rec.Dimaltd));
            data.Add(new Snoop.Data.Distance("Dimaltf", rec.Dimaltf));
            data.Add(new Snoop.Data.Distance("Dimaltrnd", rec.Dimaltrnd));
            data.Add(new Snoop.Data.Int("Dimalttd", rec.Dimalttd));
            data.Add(new Snoop.Data.Int("Dimalttz", rec.Dimalttz));
            data.Add(new Snoop.Data.Int("Dimaltu", rec.Dimaltu));
            data.Add(new Snoop.Data.Int("Dimaltz", rec.Dimaltz));
            data.Add(new Snoop.Data.String("Dimapost", rec.Dimapost));
            data.Add(new Snoop.Data.Int("Dimarcsym", rec.Dimarcsym));
            data.Add(new Snoop.Data.Distance("Dimasz", rec.Dimasz));
            data.Add(new Snoop.Data.Int("Dimatfit", rec.Dimatfit));
            data.Add(new Snoop.Data.Int("Dimaunit", rec.Dimaunit));
            data.Add(new Snoop.Data.Int("Dimazin", rec.Dimazin));
            data.Add(new Snoop.Data.ObjectId("Dimblk", rec.Dimblk));
            data.Add(new Snoop.Data.ObjectId("Dimblk1", rec.Dimblk1));
            data.Add(new Snoop.Data.ObjectId("Dimblk2", rec.Dimblk2));
            data.Add(new Snoop.Data.Distance("Dimcen", rec.Dimcen));
            data.Add(new Snoop.Data.ObjectToString("Dimclrd", rec.Dimclrd));
            data.Add(new Snoop.Data.ObjectToString("Dimclre", rec.Dimclre));
            data.Add(new Snoop.Data.ObjectToString("Dimclrt", rec.Dimclrt));
            data.Add(new Snoop.Data.Int("Dimdec", rec.Dimdec));
            data.Add(new Snoop.Data.Distance("Dimdle", rec.Dimdle));
            data.Add(new Snoop.Data.Distance("Dimdli", rec.Dimdli));
            data.Add(new Snoop.Data.String("Dimdsep", rec.Dimdsep.ToString()));
            data.Add(new Snoop.Data.Distance("Dimexe", rec.Dimexe));
            data.Add(new Snoop.Data.Distance("Dimexo", rec.Dimexo));
            data.Add(new Snoop.Data.Int("Dimfrac", rec.Dimfrac));
            data.Add(new Snoop.Data.Distance("Dimfxlen", rec.Dimfxlen));
            data.Add(new Snoop.Data.Bool("DimfxlenOn", rec.DimfxlenOn));
            data.Add(new Snoop.Data.Distance("Dimgap", rec.Dimgap));
            data.Add(new Snoop.Data.Angle("Dimjogang", rec.Dimjogang));
            data.Add(new Snoop.Data.Int("Dimjust", rec.Dimjust));
            data.Add(new Snoop.Data.ObjectId("Dimldrblk", rec.Dimldrblk));
            data.Add(new Snoop.Data.Distance("Dimlfac", rec.Dimlfac));
            data.Add(new Snoop.Data.Bool("Dimlim", rec.Dimlim));
            data.Add(new Snoop.Data.ObjectId("Dimltex1", rec.Dimltex1));
            data.Add(new Snoop.Data.ObjectId("Dimltex2", rec.Dimltex2));
            data.Add(new Snoop.Data.ObjectId("Dimltype", rec.Dimltype));
            data.Add(new Snoop.Data.Int("Dimlunit", rec.Dimlunit));
            data.Add(new Snoop.Data.String("Dimlwd", rec.Dimlwd.ToString()));
            data.Add(new Snoop.Data.String("Dimlwe", rec.Dimlwe.ToString()));
            data.Add(new Snoop.Data.String("Dimpost", rec.Dimpost));
            data.Add(new Snoop.Data.Distance("Dimrnd", rec.Dimrnd));
            data.Add(new Snoop.Data.Bool("Dimsah", rec.Dimsah));
            data.Add(new Snoop.Data.Distance("Dimscale", rec.Dimscale));
            data.Add(new Snoop.Data.Bool("Dimsd1", rec.Dimsd1));
            data.Add(new Snoop.Data.Bool("Dimsd2", rec.Dimsd2));
            data.Add(new Snoop.Data.Bool("Dimse1", rec.Dimse1));
            data.Add(new Snoop.Data.Bool("Dimse2", rec.Dimse2));
            data.Add(new Snoop.Data.Bool("Dimsoxd", rec.Dimsoxd));
            data.Add(new Snoop.Data.Int("Dimtad", rec.Dimtad));
            data.Add(new Snoop.Data.Int("Dimtdec", rec.Dimtdec));
            data.Add(new Snoop.Data.Distance("Dimtfac", rec.Dimtfac));
            data.Add(new Snoop.Data.Int("Dimtfill", rec.Dimtfill));
            data.Add(new Snoop.Data.Object("Dimtfillclr", rec.Dimtfillclr));
            data.Add(new Snoop.Data.Bool("Dimtih", rec.Dimtih));
            data.Add(new Snoop.Data.Bool("Dimtix", rec.Dimtix));
            data.Add(new Snoop.Data.Distance("Dimtm", rec.Dimtm));
            data.Add(new Snoop.Data.Int("Dimtmove", rec.Dimtmove));
            data.Add(new Snoop.Data.Bool("Dimtofl", rec.Dimtofl));
            data.Add(new Snoop.Data.Bool("Dimtoh", rec.Dimtoh));
            data.Add(new Snoop.Data.Bool("Dimtol", rec.Dimtol));
            data.Add(new Snoop.Data.Int("Dimtolj", rec.Dimtolj));
            data.Add(new Snoop.Data.Distance("Dimtp", rec.Dimtp));
            data.Add(new Snoop.Data.Distance("Dimtsz", rec.Dimtsz));
            data.Add(new Snoop.Data.Distance("Dimtvp", rec.Dimtvp));
            data.Add(new Snoop.Data.ObjectId("Dimtxsty", rec.Dimtxsty));
            data.Add(new Snoop.Data.Distance("Dimtxt", rec.Dimtxt));
            data.Add(new Snoop.Data.Int("Dimtzin", rec.Dimtzin));
            data.Add(new Snoop.Data.Bool("Dimupt", rec.Dimupt));
            data.Add(new Snoop.Data.Int("Dimzin", rec.Dimzin));
        }
        
        private void
        Stream(ArrayList data, LayerTableRecord rec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerTableRecord)));

            data.Add(new Snoop.Data.String("Description", rec.Description));
            data.Add(new Snoop.Data.ObjectToString("Color", rec.Color));
            data.Add(new Snoop.Data.Object("Entity color", rec.EntityColor));
            data.Add(new Snoop.Data.ObjectId("Linetype ID", rec.LinetypeObjectId));
            data.Add(new Snoop.Data.String("Lineweight", rec.LineWeight.ToString()));
            data.Add(new Snoop.Data.String("PlotStyle name", rec.PlotStyleName));
            data.Add(new Snoop.Data.ObjectId("PlotStyle name ID", rec.PlotStyleNameId));
            data.Add(new Snoop.Data.Object("Transparency", rec.Transparency));
            data.Add(new Snoop.Data.Bool("Viewport visibility default", rec.ViewportVisibilityDefault));
            data.Add(new Snoop.Data.Bool("Is frozen", rec.IsFrozen));
            data.Add(new Snoop.Data.Bool("Is locked", rec.IsLocked));
            data.Add(new Snoop.Data.Bool("Is off", rec.IsOff));
            data.Add(new Snoop.Data.Bool("Is plottable", rec.IsPlottable));
            data.Add(new Snoop.Data.Bool("Is used", rec.IsUsed));
        }
        
        private void
        Stream(ArrayList data, LinetypeTableRecord rec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LinetypeTableRecord)));

            data.Add(new Snoop.Data.String("Ascii Description", rec.AsciiDescription));
            data.Add(new Snoop.Data.String("Comments", rec.Comments));
            data.Add(new Snoop.Data.Bool("Is scaled to fit", rec.IsScaledToFit));
            data.Add(new Snoop.Data.Distance("Pattern length", rec.PatternLength));
            data.Add(new Snoop.Data.Int("Number of dashes", rec.NumDashes));

            int i, len;
            len = rec.NumDashes;
            for (i=0; i<len; i++) {
                data.Add(new Snoop.Data.CategorySeparator(string.Format("--- DASH [{0}] ---", i)));
                data.Add(new Snoop.Data.Distance("Length", rec.DashLengthAt(i)));
                data.Add(new Snoop.Data.ObjectId("Shape style", rec.ShapeStyleAt(i)));
                data.Add(new Snoop.Data.Int("Shape number at", rec.ShapeNumberAt(i)));
                data.Add(new Snoop.Data.Vector2d("Shape offset at", rec.ShapeOffsetAt(i)));
                data.Add(new Snoop.Data.Double("Shape scale at", rec.ShapeScaleAt(i)));
                data.Add(new Snoop.Data.Bool("Shape is UCS oriented at", rec.ShapeIsUcsOrientedAt(i)));
                data.Add(new Snoop.Data.Angle("Shape rotation at", rec.ShapeRotationAt(i)));

                try {
                    data.Add(new Snoop.Data.String("Text at", rec.TextAt(i)));
                }
                catch (Autodesk.AutoCAD.Runtime.Exception e) {
                    if (e.ErrorStatus == Autodesk.AutoCAD.Runtime.ErrorStatus.NotApplicable)
                        data.Add(new Snoop.Data.Exception("Text at", e));
                    else
                        throw;
                }
            }
        }
        
        private void
        Stream(ArrayList data, RegAppTableRecord rec) {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RegAppTableRecord)));
       }
       
        private void
        Stream(ArrayList data, TextStyleTableRecord rec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(TextStyleTableRecord)));

            data.Add(new Snoop.Data.String("Big font file name", rec.BigFontFileName));
            data.Add(new Snoop.Data.String("Filename", rec.FileName));
            data.Add(new Snoop.Data.String("Flag bits", rec.FlagBits.ToString()));
            data.Add(new Snoop.Data.Object("Font descriptor", rec.Font));
            data.Add(new Snoop.Data.Bool("Is shape file", rec.IsShapeFile));
            data.Add(new Snoop.Data.Bool("Is vertical", rec.IsVertical));
            data.Add(new Snoop.Data.Angle("Obliquing angle", rec.ObliquingAngle));
            data.Add(new Snoop.Data.Distance("Prior size", rec.PriorSize));
            data.Add(new Snoop.Data.Distance("Text size", rec.TextSize));
            data.Add(new Snoop.Data.Distance("X scale", rec.XScale));
        }
        
        private void
        Stream(ArrayList data, UcsTableRecord rec)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(UcsTableRecord)));

            data.Add(new Snoop.Data.Point3d("Origin", rec.Origin));
            data.Add(new Snoop.Data.Vector3d("X axis", rec.XAxis));
            data.Add(new Snoop.Data.Vector3d("Y axis", rec.YAxis));
        }
        #endregion

        #region SymbolTable

        private void
        Stream(ArrayList data, AcDb.SymbolTable symTbl)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcDb.SymbolTable)));

                // There is no data unique to different types of SymbolTables, so we will just take
                // a short-cut here and print out the class name.  The virtuals it defines are already
                // taken care of by the UI (that is how we got the tree structure).
                // branch to all known major sub-classes
            data.Add(new Snoop.Data.ClassSeparator(symTbl.GetType()));
        }

        #endregion

    }
}