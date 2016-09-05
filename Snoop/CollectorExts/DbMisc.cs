
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
using Autodesk.AutoCAD.DatabaseServices;

using MgdDbg.Snoop.Collectors;

namespace MgdDbg.Snoop.CollectorExts
{
	/// <summary>
	/// This is a Snoop Collector Extension object to collect data from Color objects.
	/// </summary>
	public class DbMisc : CollectorExt
	{
		public
		DbMisc()
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
            ResultBuffer resBuf = e.ObjToSnoop as ResultBuffer;
            if (resBuf != null) {
                Stream(snoopCollector.Data(), resBuf);
                return;
            }           
            
            BulgeVertex bulgeVert = e.ObjToSnoop as BulgeVertex;
            if (bulgeVert != null) {          
                Stream(snoopCollector.Data(), bulgeVert);
                return;
            }

            LoftOptions loftOpt = e.ObjToSnoop as LoftOptions;
            if (loftOpt != null) {
                Stream(snoopCollector.Data(), loftOpt);
                return;
            }

            SweepOptions sweepOpt = e.ObjToSnoop as SweepOptions;
            if (sweepOpt != null) {
                Stream(snoopCollector.Data(), sweepOpt);
                return;
            }
            
            RevolveOptions revOpt = e.ObjToSnoop as RevolveOptions;
            if (revOpt != null) {
                Stream(snoopCollector.Data(), revOpt);
                return;
            }

            PlotStyleTableChangedEventArgs plotStyleTableChangedArgs = e.ObjToSnoop as PlotStyleTableChangedEventArgs;
            if (plotStyleTableChangedArgs != null) {
                Stream(snoopCollector.Data(), plotStyleTableChangedArgs);
                return;
            }

            LayoutCopiedEventArgs layoutCopiedArgs = e.ObjToSnoop as LayoutCopiedEventArgs;
            if (layoutCopiedArgs != null) {
                Stream(snoopCollector.Data(), layoutCopiedArgs);
                return;
            }

            LayoutRenamedEventArgs layoutRenamedArgs = e.ObjToSnoop as LayoutRenamedEventArgs;
            if (layoutRenamedArgs != null) {
                Stream(snoopCollector.Data(), layoutRenamedArgs);
                return;
            }

            LayoutEventArgs layoutArgs = e.ObjToSnoop as LayoutEventArgs;
            if (layoutArgs != null) {
                Stream(snoopCollector.Data(), layoutArgs);
                return;
            }

            XrefSubCommandEventArgs xrefSubCmdArgs = e.ObjToSnoop as XrefSubCommandEventArgs;
            if (xrefSubCmdArgs != null) {
                Stream(snoopCollector.Data(), xrefSubCmdArgs);
                return;
            }

            AuditInfo auditInfo = e.ObjToSnoop as AuditInfo;
            if (auditInfo != null) {
                Stream(snoopCollector.Data(), auditInfo);
                return;
            }

            CustomObjectSnapMode custObjSnapMode = e.ObjToSnoop as CustomObjectSnapMode;
            if (custObjSnapMode != null) {
                Stream(snoopCollector.Data(), custObjSnapMode);
                return;
            }

            // TBD: "DbHomeView" throws a nullReferenceException
            // even as it is being checked for null !!!
            //DbHomeView homeView = e.ObjToSnoop as DbHomeView;
            //if (homeView != null) {
            //    Stream(snoopCollector.Data(), homeView);
            //    return;
            //}

            UnderlayItem underlayItem = e.ObjToSnoop as UnderlayItem;
            if (underlayItem != null) {
                Stream(snoopCollector.Data(), underlayItem);
                return;
            }

            DynamicBlockReferenceProperty dynBlkRefProp = e.ObjToSnoop as DynamicBlockReferenceProperty;
            if (dynBlkRefProp != null) {
                Stream(snoopCollector.Data(), dynBlkRefProp);
                return;
            }

            DynamicDimensionChangedEventArgs dimChangeArgs = e.ObjToSnoop as DynamicDimensionChangedEventArgs;
            if (dimChangeArgs != null) {
                Stream(snoopCollector.Data(), dimChangeArgs);
                return;
            }

            DynamicDimensionData dimData = e.ObjToSnoop as DynamicDimensionData;
            if (dimData != null) {
                Stream(snoopCollector.Data(), dimData);
                return;
            }

            EntityAlignmentEventArgs entAlignArgs = e.ObjToSnoop as EntityAlignmentEventArgs;
            if (entAlignArgs != null) {
                Stream(snoopCollector.Data(), entAlignArgs);
                return;
            }

            FileDependencyManager fileDepMgr = e.ObjToSnoop as FileDependencyManager;
            if (fileDepMgr != null) {
                Stream(snoopCollector.Data(), fileDepMgr);
                return;
            }

            IdMappingEventArgs idMapArgs = e.ObjToSnoop as IdMappingEventArgs;
            if (idMapArgs != null) {
                Stream(snoopCollector.Data(), idMapArgs);
                return;
            }

            LayerStateDeletedEventArgs layerStateDelArgs = e.ObjToSnoop as LayerStateDeletedEventArgs;
            if (layerStateDelArgs != null) {
                Stream(snoopCollector.Data(), layerStateDelArgs);
                return;
            }

            LayerStateEventArgs layerStateArgs = e.ObjToSnoop as LayerStateEventArgs;
            if (layerStateArgs != null) {
                Stream(snoopCollector.Data(), layerStateArgs);
                return;
            }

            LayerStateRenameEventArgs layerStateRenArgs = e.ObjToSnoop as LayerStateRenameEventArgs;
            if (layerStateRenArgs != null) {
                Stream(snoopCollector.Data(), layerStateRenArgs);
                return;
            }

            LayerViewportProperties layerViewportProps = e.ObjToSnoop as LayerViewportProperties;
            if (layerViewportProps != null) {
                Stream(snoopCollector.Data(), layerViewportProps);
                return;
            }

            MTextFragment mtextFrag = e.ObjToSnoop as MTextFragment;
            if (mtextFrag != null) {
                Stream(snoopCollector.Data(), mtextFrag);
                return;
            }

            ObjectClosedEventArgs objCloseArgs = e.ObjToSnoop as ObjectClosedEventArgs;
            if (objCloseArgs != null) {
                Stream(snoopCollector.Data(), objCloseArgs);
                return;
            }

            ObjectErasedEventArgs objEraseArgs = e.ObjToSnoop as ObjectErasedEventArgs;
            if (objEraseArgs != null) {
                Stream(snoopCollector.Data(), objEraseArgs);
                return;
            }

            ObjectSnapContext objSnapCtxt = e.ObjToSnoop as ObjectSnapContext;
            if (objSnapCtxt != null) {
                Stream(snoopCollector.Data(), objSnapCtxt);
                return;
            }

            ObjectSnapInfo objSnapInfo = e.ObjToSnoop as ObjectSnapInfo;
            if (objSnapInfo != null) {
                Stream(snoopCollector.Data(), objSnapInfo);
                return;
            }

            RevolveOptionsCheckRevolveCurveOut revOptsChkRevCurOut = e.ObjToSnoop as RevolveOptionsCheckRevolveCurveOut;
            if (revOptsChkRevCurOut != null) {
                Stream(snoopCollector.Data(), revOptsChkRevCurOut);
                return;
            }

            SweepOptionsCheckSweepCurveOut sweepOptsChkSweepCur = e.ObjToSnoop as SweepOptionsCheckSweepCurveOut;
            if (sweepOptsChkSweepCur != null) {
                Stream(snoopCollector.Data(), sweepOptsChkSweepCur);
                return;
            }

            UnderlayFile underlayFile = e.ObjToSnoop as UnderlayFile;
            if (underlayFile != null) {
                Stream(snoopCollector.Data(), underlayFile);
                return;
            }

            UnderlayLayer underlayLayer = e.ObjToSnoop as UnderlayLayer;
            if (underlayLayer != null) {
                Stream(snoopCollector.Data(), underlayLayer);
                return;
            }

            DataAdapter dataAdapt = e.ObjToSnoop as DataAdapter;
            if (dataAdapt != null) {
                Stream(snoopCollector.Data(), dataAdapt);
                return;
            }

            DataCell dataCell = e.ObjToSnoop as DataCell;
            if (dataCell != null) {
                Stream(snoopCollector.Data(), dataCell);
                return;
            }

            DataColumn dataColl = e.ObjToSnoop as DataColumn;
            if (dataColl != null) {
                Stream(snoopCollector.Data(), dataColl);
                return;
            }

            DataLinkManager dataLinkMan = e.ObjToSnoop as DataLinkManager;
            if (dataLinkMan != null) {
                Stream(snoopCollector.Data(), dataLinkMan);
                return;
            }

            DbDictionaryEnumerator dbDictEnum = e.ObjToSnoop as DbDictionaryEnumerator;
            if (dbDictEnum != null) {
                Stream(snoopCollector.Data(), dbDictEnum);
                return;
            }

            DwgFiler dwgFiler = e.ObjToSnoop as DwgFiler;
            if (dwgFiler != null) {
                Stream(snoopCollector.Data(), dwgFiler);
                return;
            }

            DxfFiler dxfFiler = e.ObjToSnoop as DxfFiler;
            if (dxfFiler != null) {
                Stream(snoopCollector.Data(), dxfFiler);
                return;
            }

            IdMapping idMap = e.ObjToSnoop as IdMapping;
            if (idMap != null) {
                Stream(snoopCollector.Data(), idMap);
                return;
            }

            LayerStateManager stateMgr = e.ObjToSnoop as LayerStateManager;
            if (stateMgr != null) {
                Stream(snoopCollector.Data(), stateMgr);
                return;
            }

            LayoutManager layoutMgr = e.ObjToSnoop as LayoutManager;
            if (layoutMgr != null) {
                Stream(snoopCollector.Data(), layoutMgr);
                return;
            }

            ObjectContext objContext = e.ObjToSnoop as ObjectContext;
            if (objContext != null) {
                Stream(snoopCollector.Data(), objContext);
                return;
            }
           
                // ValueTypes we have to treat a bit different
            if (e.ObjToSnoop is ObjectId) {
                Stream(snoopCollector.Data(), (ObjectId)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is Extents2d) {
                Stream(snoopCollector.Data(), (Extents2d)e.ObjToSnoop);
                return;
            }
                
            if (e.ObjToSnoop is Extents3d) {
                Stream(snoopCollector.Data(), (Extents3d)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is HyperLink) {
                Stream(snoopCollector.Data(), (HyperLink)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is Solid3dMassProperties) {
                Stream(snoopCollector.Data(), (Solid3dMassProperties)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is FitData) {
                Stream(snoopCollector.Data(), (FitData)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is NurbsData) {
                Stream(snoopCollector.Data(), (NurbsData)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is PatternDefinition) {
                Stream(snoopCollector.Data(), (PatternDefinition)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is MlineStyleElement) {
                Stream(snoopCollector.Data(), (MlineStyleElement)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is FieldEvaluationStatusResult) {
                Stream(snoopCollector.Data(), (FieldEvaluationStatusResult)e.ObjToSnoop);
                return;
            }
            
            if (e.ObjToSnoop is Rectangle3d) {
                Stream(snoopCollector.Data(), (Rectangle3d)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is DatabaseSummaryInfo) {
                Stream(snoopCollector.Data(), (DatabaseSummaryInfo)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is FullSubentityPath) {
                Stream(snoopCollector.Data(), (FullSubentityPath)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is SubentityId) {
                Stream(snoopCollector.Data(), (SubentityId)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is DataTypeParameter) {
                Stream(snoopCollector.Data(), (DataTypeParameter)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is DBDictionaryEntry) {
                Stream(snoopCollector.Data(), (DBDictionaryEntry)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is FileDependencyInfo) {
                Stream(snoopCollector.Data(), (FileDependencyInfo)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is FullDwgVersion) {
                Stream(snoopCollector.Data(), (FullDwgVersion)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is GridPropertyParameter) {
                Stream(snoopCollector.Data(), (GridPropertyParameter)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MeshPointMap) {
                Stream(snoopCollector.Data(), (MeshPointMap)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MeshPointMaps) {
                Stream(snoopCollector.Data(), (MeshPointMaps)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilterDefinition) {
                Stream(snoopCollector.Data(), (Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilterDefinition)e.ObjToSnoop);
                return;
            }

            //if (e.ObjToSnoop is Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilterVolume) {
            //    Stream(snoopCollector.Data(), (Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilterVolume)e.ObjToSnoop);
            //    return;
            //}
        }

        private void
        Stream(ArrayList data, ResultBuffer resbuf)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ResultBuffer)));

            short typeCode;
            TypedValue tmpVal;

            ResultBufferEnumerator iter = resbuf.GetEnumerator();
            while (iter.MoveNext()) {
                tmpVal = (TypedValue)iter.Current;
                typeCode = tmpVal.TypeCode;
                string dxfCodeStr;

                    // DXF codes
                if (typeCode == 0) {
                    dxfCodeStr = string.Format("{0:d}    (dxf classname/string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if (((typeCode >= 1) && (typeCode <= 4)) ||
                         ((typeCode >= 6) && (typeCode <= 9))) {
                    dxfCodeStr = string.Format("{0:d}    (string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if ((typeCode == 5) || (typeCode == 105)) {
                    dxfCodeStr = string.Format("{0:d}    (handle/string", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if ((typeCode >= 10) && (typeCode <= 17)) {
                    dxfCodeStr = string.Format("{0:d}    (3 reals)", typeCode);
                    data.Add(new Snoop.Data.Point3d(dxfCodeStr, (Point3d)tmpVal.Value));
                }
                else if ((typeCode >= 38) && (typeCode <= 59)) {
                    dxfCodeStr = string.Format("{0:d}    (double)", typeCode);
                    data.Add(new Snoop.Data.Double(dxfCodeStr, (double)tmpVal.Value));
                }
                else if ((typeCode >= 60) && (typeCode <= 79)) {
                    dxfCodeStr = string.Format("{0:d}    (short)", typeCode);
                    data.Add(new Snoop.Data.Int(dxfCodeStr, (short)tmpVal.Value));
                }
                else if ((typeCode >= 90) && (typeCode <= 99)) {
                    dxfCodeStr = string.Format("{0:d}    (long)", typeCode);
                    data.Add(new Snoop.Data.Int(dxfCodeStr, (int)(long)tmpVal.Value));
                }
                else if (typeCode == 100) {
                    dxfCodeStr = string.Format("{0:d}    (sub-class marker/string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if (typeCode == 101) {
                    dxfCodeStr = string.Format("{0:d}    (embedded object start/string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if (typeCode == 102) {
                    dxfCodeStr = string.Format("{0:d}    (control/string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if ((typeCode >= 110) && (typeCode <= 119)) {
                    dxfCodeStr = string.Format("{0:d}    (3 reals)", typeCode);
                    data.Add(new Snoop.Data.Point3d(dxfCodeStr, (Point3d)tmpVal.Value));
                }
                else if ((typeCode >= 140) && (typeCode <= 149)) {
                    dxfCodeStr = string.Format("{0:d}    (double)", typeCode);
                    data.Add(new Snoop.Data.Double(dxfCodeStr, (double)tmpVal.Value));
                }
                else if ((typeCode >= 170) && (typeCode <= 179)) {
                    dxfCodeStr = string.Format("{0:d}    (short)", typeCode);
                    data.Add(new Snoop.Data.Int(dxfCodeStr, (short)tmpVal.Value));
                }
                else if ((typeCode >= 210) && (typeCode <= 219)) {
                    dxfCodeStr = string.Format("{0:d}    (3 reals)", typeCode);
                    data.Add(new Snoop.Data.Point3d(dxfCodeStr, (Point3d)tmpVal.Value));
                }
                else if ((typeCode >= 270) && (typeCode <= 299)) {
                    dxfCodeStr = string.Format("{0:d}    (short)", typeCode);
                    data.Add(new Snoop.Data.Int(dxfCodeStr, (short)tmpVal.Value));
                }
                else if ((typeCode >= 300) && (typeCode <= 309)) {
                    dxfCodeStr = string.Format("{0:d}    (string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if ((typeCode >= 310) && (typeCode <= 319)) {
                    dxfCodeStr = string.Format("{0:d}    (binary chunk)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, Snoop.Utils.BytesToHexStr((byte[])tmpVal.Value)));
                }
                else if ((typeCode >= 320) && (typeCode <= 329)) {
                    dxfCodeStr = string.Format("{0:d}    (handle/string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if ((typeCode >= 330) && (typeCode <= 339)) {
                    dxfCodeStr = string.Format("{0:d}    (soft pointer)", typeCode);
                    data.Add(new Snoop.Data.ObjectId(dxfCodeStr, (ObjectId)tmpVal.Value));
                }
                else if ((typeCode >= 340) && (typeCode <= 349)) {
                    dxfCodeStr = string.Format("{0:d}    (hard pointer)", typeCode);
                    data.Add(new Snoop.Data.ObjectId(dxfCodeStr, (ObjectId)tmpVal.Value));
                }
                else if ((typeCode >= 350) && (typeCode <= 359)) {
                    dxfCodeStr = string.Format("{0:d}    (soft owner)", typeCode);
                    data.Add(new Snoop.Data.ObjectId(dxfCodeStr, (ObjectId)tmpVal.Value));
                }
                else if ((typeCode >= 360) && (typeCode <= 369)) {
                    dxfCodeStr = string.Format("{0:d}    (hard owner)", typeCode);
                    data.Add(new Snoop.Data.ObjectId(dxfCodeStr, (ObjectId)tmpVal.Value));
                }
                else if ((typeCode >= 370) && (typeCode <= 379)) {
                    dxfCodeStr = string.Format("{0:d}    (short)", typeCode);
                    data.Add(new Snoop.Data.Int(dxfCodeStr, (short)tmpVal.Value));
                }
                else if ((typeCode >= 380) && (typeCode <= 389)) {
                    dxfCodeStr = string.Format("{0:d}    (8-bit int)", typeCode);
                    data.Add(new Snoop.Data.Int(dxfCodeStr, (char)tmpVal.Value));
                }
                else if ((typeCode >= 390) && (typeCode <= 399)) {
                    dxfCodeStr = string.Format("{0:d}    (entity name)", typeCode);
                    data.Add(new Snoop.Data.ObjectId(dxfCodeStr, (ObjectId)tmpVal.Value));
                }
                else if ((typeCode >= 400) && (typeCode <= 409)) {
                    dxfCodeStr = string.Format("{0:d}    (short)", typeCode);
                    data.Add(new Snoop.Data.Int(dxfCodeStr, (short)tmpVal.Value));
                }
                else if ((typeCode >= 410) && (typeCode <= 419)) {
                    dxfCodeStr = string.Format("{0:d}    (string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if (typeCode == 999) {
                    dxfCodeStr = string.Format("{0:d}    (comment/string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }

                    // Xdata codes
                else if (typeCode == 1000) {
                    dxfCodeStr = string.Format("{0:d}    (string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if (typeCode == 1001) {
                    dxfCodeStr = string.Format("{0:d}    (appname/string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if (typeCode == 1002) {
                    dxfCodeStr = string.Format("{0:d}    (control/string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if (typeCode == 1003) {
                    dxfCodeStr = string.Format("{0:d}    (layername/string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if (typeCode == 1004) {
                    dxfCodeStr = string.Format("{0:d}    (binary chunk)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, Snoop.Utils.BytesToHexStr((byte[])tmpVal.Value)));
                }
                else if (typeCode == 1005) {
                    dxfCodeStr = string.Format("{0:d}    (handle/string)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if (typeCode == 1010) {
                    dxfCodeStr = string.Format("{0:d}    (3 reals)", typeCode);
                    data.Add(new Snoop.Data.Point3d(dxfCodeStr, (Point3d)tmpVal.Value));
                }
                else if (typeCode == 1011) {
                    dxfCodeStr = string.Format("{0:d}    (World space position/3 reals)", typeCode);
                    data.Add(new Snoop.Data.Point3d(dxfCodeStr, (Point3d)tmpVal.Value));
                }
                else if (typeCode == 1012) {
                    dxfCodeStr = string.Format("{0:d}    (World space displacement/3 reals)", typeCode);
                    data.Add(new Snoop.Data.Point3d(dxfCodeStr, (Point3d)tmpVal.Value));
                }
                else if (typeCode == 1013) {
                    dxfCodeStr = string.Format("{0:d}    (World direction/3 reals)", typeCode);
                    data.Add(new Snoop.Data.Point3d(dxfCodeStr, (Point3d)tmpVal.Value));
                }
                else if (typeCode == 1040) {
                    dxfCodeStr = string.Format("{0:d}    (double)", typeCode);
                    data.Add(new Snoop.Data.Double(dxfCodeStr, (double)tmpVal.Value));
                }
                else if (typeCode == 1041) {
                    dxfCodeStr = string.Format("{0:d}    (distance/double)", typeCode);
                    data.Add(new Snoop.Data.Double(dxfCodeStr, (double)tmpVal.Value));
                }
                else if (typeCode == 1042) {
                    dxfCodeStr = string.Format("{0:d}    (scale factor/double)", typeCode);
                    data.Add(new Snoop.Data.Double(dxfCodeStr, (double)tmpVal.Value));
                }
                else if (typeCode == 1070) {
                    dxfCodeStr = string.Format("{0:d}    (short)", typeCode);
                    data.Add(new Snoop.Data.Int(dxfCodeStr, (short)tmpVal.Value));
                }
                else if (typeCode == 1071) {
                    dxfCodeStr = string.Format("{0:d}    (long)", typeCode);
                    data.Add(new Snoop.Data.Int(dxfCodeStr, (int)tmpVal.Value));
                }
                    // other control marker codes
                else if (typeCode == -6) {
                    dxfCodeStr = string.Format("{0:d}    (extension dictionary)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, ""));
                }
                else if (typeCode == -5) {
                    dxfCodeStr = string.Format("{0:d}    (persistent reactors)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, ""));
                }
                else if (typeCode == -4) {
                    dxfCodeStr = string.Format("{0:d}    (conditional operator)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, (string)tmpVal.Value));
                }
                else if (typeCode == -3) {
                    dxfCodeStr = string.Format("{0:d}    (start of xdata)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, ""));
                }
                else if (typeCode == -2) {
                    dxfCodeStr = string.Format("{0:d}    (entity name reference)", typeCode);
                    data.Add(new Snoop.Data.ObjectId(dxfCodeStr, (ObjectId)tmpVal.Value));
                }
                else if (typeCode == -1) {
                    dxfCodeStr = string.Format("{0:d}    (entity name)", typeCode);
                    data.Add(new Snoop.Data.ObjectId(dxfCodeStr, (ObjectId)tmpVal.Value));
                }
                else if (typeCode == (int)Autodesk.AutoCAD.Runtime.LispDataType.SelectionSet) {
                    dxfCodeStr = string.Format("{0:d}    (picks)", typeCode);
                    //valueStr.Format("<Selection Set: %8lx>", rb->resval.rlname[0]);
                    //data.Add(new Snoop.Data.String(dxfCodeStr, valueStr));
                    data.Add(new Snoop.Data.String(dxfCodeStr, "???")); // TBD: how to get SelectionSet displayed?
                }
                else if (typeCode == (int)Autodesk.AutoCAD.Runtime.LispDataType.ListBegin) {
                    dxfCodeStr = string.Format("{0:d}    (list begin)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, ""));
                }
                else if (typeCode == (int)Autodesk.AutoCAD.Runtime.LispDataType.ListEnd) {
                    dxfCodeStr = string.Format("{0:d}    (list end)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, ""));
                }
                else if (typeCode == (int)Autodesk.AutoCAD.Runtime.LispDataType.Nil) {
                    dxfCodeStr = string.Format("{0:d}    (NIL)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, ""));
                }
                else if (typeCode == (int)Autodesk.AutoCAD.Runtime.LispDataType.T_atom) {
                    dxfCodeStr = string.Format("{0:d}    (T)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, ""));
                }
                else {
                    Debug.Assert(false);
                    dxfCodeStr = string.Format("{0:d}    (*Unknown*)", typeCode);
                    data.Add(new Snoop.Data.String(dxfCodeStr, ""));
                }
            }
        }

        private void
        Stream(ArrayList data, ObjectId objId)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ObjectId)));
            
            data.Add(new Snoop.Data.Database("Database", objId.Database));
            data.Add(new Snoop.Data.Database("Original database", objId.OriginalDatabase));
            data.Add(new Snoop.Data.String("Handle", objId.Handle.ToString()));
            data.Add(new Snoop.Data.String("Non-forwarded handle", objId.NonForwardedHandle.ToString()));
            data.Add(new Snoop.Data.Bool("Is erased", objId.IsErased));
            data.Add(new Snoop.Data.Bool("Is effectively erased", objId.IsEffectivelyErased));
            data.Add(new Snoop.Data.Bool("Is null", objId.IsNull));
            data.Add(new Snoop.Data.Bool("Is valid", objId.IsValid));
            data.Add(new Snoop.Data.Bool("Object left on disk", objId.ObjectLeftOnDisk));
            data.Add(new Snoop.Data.Int("Old ID", objId.OldIdPtr.ToInt32())); 
        }
        
        private void
        Stream(ArrayList data, Extents3d ext)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Extents3d)));
            
            data.Add(new Snoop.Data.Point3d("Min point", ext.MinPoint));
            data.Add(new Snoop.Data.Point3d("Max point", ext.MaxPoint));
        }

        private void
        Stream(ArrayList data, Extents2d ext)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Extents2d)));

            data.Add(new Snoop.Data.Point2d("Min point", ext.MinPoint));
            data.Add(new Snoop.Data.Point2d("Max point", ext.MaxPoint));
        }
        
        private void
        Stream(ArrayList data, HyperLink hlink)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(HyperLink)));
            
            data.Add(new Snoop.Data.String("Name", hlink.Name));
            data.Add(new Snoop.Data.String("Description", hlink.Description));
            data.Add(new Snoop.Data.String("Display string", hlink.DisplayString));
            data.Add(new Snoop.Data.Bool("Is outermost container", hlink.IsOutermostContainer));
            data.Add(new Snoop.Data.Int("Nested level", hlink.NestedLevel));
            data.Add(new Snoop.Data.String("Sub location", hlink.SubLocation));
        }
        
        private void
        Stream(ArrayList data, Solid3dMassProperties massProps)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Solid3dMassProperties)));
            
            data.Add(new Snoop.Data.Point3d("Centroid", massProps.Centroid));
            data.Add(new Snoop.Data.Vector3d("Moment of inertia", massProps.MomentsOfIntertia));
            data.Add(new Snoop.Data.Vector3d("Principal moments", massProps.PrincipalMoments));
            data.Add(new Snoop.Data.Vector3d("Products of inertia", massProps.ProductsOfIntertia));
            data.Add(new Snoop.Data.Vector3d("Radii of gyration", massProps.RadiiOfGyration));
            data.Add(new Snoop.Data.Double("Volume", massProps.Volume));
            data.Add(new Snoop.Data.Vector3d("Principle axis X", massProps[0]));
            data.Add(new Snoop.Data.Vector3d("Principle axis Y", massProps[1]));
            data.Add(new Snoop.Data.Vector3d("Principle axis Z", massProps[2]));
            data.Add(new Snoop.Data.Object("Extents", massProps.Extents));
        }        
        
        private void
        Stream(ArrayList data, FitData fitData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FitData)));
            
            data.Add(new Snoop.Data.Int("Degree", fitData.Degree));
            data.Add(new Snoop.Data.Bool("Tangents exist", fitData.TangentsExist));
            data.Add(new Snoop.Data.Vector3d("Start tangent", fitData.StartTangent));
            data.Add(new Snoop.Data.Vector3d("End tangent", fitData.EndTangent));
            data.Add(new Snoop.Data.Double("Fit tolerance", fitData.FitTolerance));
            data.Add(new Snoop.Data.Object("Fit points", fitData.GetFitPoints()));
        }

        private void
        Stream(ArrayList data, NurbsData nurbsData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(NurbsData)));
            
            data.Add(new Snoop.Data.Int("Degree", nurbsData.Degree));
            data.Add(new Snoop.Data.Double("Control point tolerance", nurbsData.ControlPointTolerance));
            data.Add(new Snoop.Data.Double("Knot tolerance", nurbsData.KnotTolerance));
            data.Add(new Snoop.Data.Bool("Closed", nurbsData.Closed));
            data.Add(new Snoop.Data.Bool("Periodic", nurbsData.Periodic));
            data.Add(new Snoop.Data.Bool("Rational", nurbsData.Rational));
            data.Add(new Snoop.Data.Object("Control points", nurbsData.GetControlPoints()));
            data.Add(new Snoop.Data.Object("Knots", nurbsData.GetKnots()));
            data.Add(new Snoop.Data.Object("Weights", nurbsData.GetWeights()));
        }

        private void
        Stream(ArrayList data, BulgeVertex bulgeVert)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BulgeVertex)));

            data.Add(new Snoop.Data.Point2d("Vertex", bulgeVert.Vertex));
            data.Add(new Snoop.Data.Double("Bulge", bulgeVert.Bulge));           
            

            //HatchLoop loop = bulgeVerts as HatchLoop;
            //if (bulgeVerts != null) {
            //    Stream(data, bulgeVerts);
            //    return;
            //}
        }
        
        private void
        Stream(ArrayList data, HatchLoop loop)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(HatchLoop)));
            
            data.Add(new Snoop.Data.String("Loop type", loop.LoopType.ToString()));
        }

        private void
        Stream(ArrayList data, PatternDefinition patternDef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PatternDefinition)));
            
            data.Add(new Snoop.Data.Double("Angle", patternDef.Angle));
            data.Add(new Snoop.Data.Double("Base X", patternDef.BaseX));
            data.Add(new Snoop.Data.Double("Base Y", patternDef.BaseY));
            data.Add(new Snoop.Data.Double("Offset X", patternDef.OffsetX));
            data.Add(new Snoop.Data.Double("Offset Y", patternDef.OffsetY));
            data.Add(new Snoop.Data.Object("Dashes", patternDef.GetDashes()));
        }

        private void
        Stream(ArrayList data, MlineStyleElement mlStyleElem)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MlineStyleElement)));
            
            data.Add(new Snoop.Data.ObjectToString("Color", mlStyleElem.Color));
            data.Add(new Snoop.Data.ObjectId("Linetype ID", mlStyleElem.LinetypeId));
            data.Add(new Snoop.Data.Double("Offset", mlStyleElem.Offset));
        }

        private void
        Stream(ArrayList data, LoftOptions opt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LoftOptions)));

            data.Add(new Snoop.Data.Bool("Align direction", opt.AlignDirection));
            data.Add(new Snoop.Data.Bool("Arc length param", opt.ArcLengthParam));
            data.Add(new Snoop.Data.Bool("Closed", opt.Closed));
            data.Add(new Snoop.Data.Double("Draft start", opt.DraftStart));
            data.Add(new Snoop.Data.Double("Draft start mag", opt.DraftStartMag));
            data.Add(new Snoop.Data.Double("Draft end", opt.DraftEnd));
            data.Add(new Snoop.Data.Double("Draft end mag", opt.DraftEndMag));
            data.Add(new Snoop.Data.String("Normal option", opt.NormalOption.ToString()));
            data.Add(new Snoop.Data.Bool("No twist", opt.NoTwist));
            data.Add(new Snoop.Data.Bool("Ruled", opt.Ruled));
            data.Add(new Snoop.Data.Bool("Simplify", opt.Simplify));
            data.Add(new Snoop.Data.Bool("Virtual guide", opt.VirtualGuide));
        }

        private void
        Stream(ArrayList data, SweepOptions opt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SweepOptions)));

            data.Add(new Snoop.Data.String("Align", opt.Align.ToString()));
            data.Add(new Snoop.Data.Angle("Align angle", opt.AlignAngle));
            data.Add(new Snoop.Data.Bool("Align start", opt.AlignStart));
            data.Add(new Snoop.Data.Bool("Bank", opt.Bank));
            data.Add(new Snoop.Data.Point3d("Base point", opt.BasePoint));
            data.Add(new Snoop.Data.Bool("Check intersections", opt.CheckIntersections));
            data.Add(new Snoop.Data.Angle("Draft angle", opt.DraftAngle));
            data.Add(new Snoop.Data.Angle("Twist angle", opt.TwistAngle));
            data.Add(new Snoop.Data.Distance("Start draft distance", opt.StartDraftDist));
            data.Add(new Snoop.Data.Distance("End draft distance", opt.EndDraftDist));
            data.Add(new Snoop.Data.Object("Path entity transform", opt.PathEntityTransform));
            data.Add(new Snoop.Data.Object("Sweep entity transform", opt.SweepEntityTransform));
            data.Add(new Snoop.Data.Double("Scale factor", opt.ScaleFactor));
        }
        
        private void
        Stream(ArrayList data, RevolveOptions opt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RevolveOptions)));

            data.Add(new Snoop.Data.Bool("Close to axis", opt.CloseToAxis));
            data.Add(new Snoop.Data.Angle("Draft angle", opt.DraftAngle));
            data.Add(new Snoop.Data.Angle("Twist angle", opt.TwistAngle));
        }


        private void
        Stream(ArrayList data, FieldEvaluationStatusResult stat)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FieldEvaluationStatusResult)));

            data.Add(new Snoop.Data.Int("Error code", stat.ErrorCode));
            data.Add(new Snoop.Data.String("Error message", stat.ErrorMessage));
            data.Add(new Snoop.Data.String("Status", stat.Status.ToString()));
        }

        private void
        Stream(ArrayList data, Rectangle3d rect)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Rectangle3d)));

            data.Add(new Snoop.Data.Point3d("Lower left", rect.LowerLeft));
            data.Add(new Snoop.Data.Point3d("Lower right", rect.LowerRight));
            data.Add(new Snoop.Data.Point3d("Upper left", rect.UpperLeft));
            data.Add(new Snoop.Data.Point3d("Upper right", rect.UpperRight));
        }

        private void
        Stream(ArrayList data, PlotStyleTableChangedEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotStyleTableChangedEventArgs)));

            data.Add(new Snoop.Data.ObjectId("ID", args.Id));
            data.Add(new Snoop.Data.String("New name", args.NewName));
        }

        private void
        Stream(ArrayList data, LayoutCopiedEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayoutCopiedEventArgs)));

            data.Add(new Snoop.Data.ObjectId("ID", args.Id));
            data.Add(new Snoop.Data.String("Name", args.Name));
            data.Add(new Snoop.Data.ObjectId("New ID", args.NewId));
            data.Add(new Snoop.Data.String("New name", args.NewName));
        }

        private void
        Stream(ArrayList data, LayoutRenamedEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayoutRenamedEventArgs)));

            data.Add(new Snoop.Data.ObjectId("ID", args.Id));
            data.Add(new Snoop.Data.String("Name", args.Name));
            data.Add(new Snoop.Data.String("New name", args.NewName));
        }

        private void
        Stream(ArrayList data, LayoutEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayoutEventArgs)));

            data.Add(new Snoop.Data.ObjectId("ID", args.Id));
            data.Add(new Snoop.Data.String("Name", args.Name));
        }

        private void
        Stream(ArrayList data, XrefSubCommandEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(XrefSubCommandEventArgs)));

            data.Add(new Snoop.Data.ObjectIdCollection("Block table record ID", args.btrIds));
            //data.Add(new Snoop.Data.String("Block table record names", args.btrNames));       // TBD: make a string array snoop
            //data.Add(new Snoop.Data.String("Paths", args.paths));
            data.Add(new Snoop.Data.String("Xref op", args.xrefOp.ToString()));

            XrefVetoableSubCommandEventArgs xrefVetoCmdArgs = args as XrefVetoableSubCommandEventArgs;
            if (xrefVetoCmdArgs != null) {
                Stream(data, xrefVetoCmdArgs);
                return;
            }

        }

        private void
        Stream(ArrayList data, XrefVetoableSubCommandEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(XrefVetoableSubCommandEventArgs)));

            data.Add(new Snoop.Data.Bool("Abort op", args.abortOp));
        }

        private void
        Stream(ArrayList data, AuditInfo auditInfo)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AuditInfo)));

            data.Add(new Snoop.Data.String("Audit pass", auditInfo.AuditPass.ToString()));
            data.Add(new Snoop.Data.Bool("Fix errors", auditInfo.FixErrors));
            data.Add(new Snoop.Data.Int("Number of entities", auditInfo.NumEntities));
            data.Add(new Snoop.Data.Int("Number of errors", auditInfo.NumErrors));
            data.Add(new Snoop.Data.Int("Number of fixes", auditInfo.NumFixes));
        }

        private void
        Stream(ArrayList data, CustomObjectSnapMode custObjSnapMode)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CustomObjectSnapMode)));

            data.Add(new Snoop.Data.String("Global mode string", custObjSnapMode.GlobalModeString));
            data.Add(new Snoop.Data.Object("Glyph", custObjSnapMode.Glyph));
            data.Add(new Snoop.Data.String("Local mode string", custObjSnapMode.LocalModeString));
            data.Add(new Snoop.Data.String("Tool tip text", custObjSnapMode.ToolTipText));           
        }


        //TBD: This throws a null reference exception. Need to investigate.

        //private void
        //Stream(ArrayList data, DbHomeView homeView)
        //{
        //    data.Add(new Snoop.Data.ClassSeparator(typeof(DbHomeView)));

        //    data.Add(new Snoop.Data.Point3d("Center", homeView.Center));
        //    data.Add(new Snoop.Data.Point3d("Eye", homeView.Eye));
        //    data.Add(new Snoop.Data.Double("Height", homeView.Height));
        //    data.Add(new Snoop.Data.Bool("Perspective", homeView.Perspective));
        //    data.Add(new Snoop.Data.Vector3d("Up", homeView.Up));
        //    data.Add(new Snoop.Data.Double("Width", homeView.Width));
        //}

        private void
        Stream(ArrayList data, UnderlayItem underlayItem)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(UnderlayItem)));

            data.Add(new Snoop.Data.Object("Extents", underlayItem.Extents));
            data.Add(new Snoop.Data.String("Name", underlayItem.Name));            
            data.Add(new Snoop.Data.String("Units", underlayItem.Units.ToString()));
            data.Add(new Snoop.Data.Bool("Using partial content", underlayItem.UsingPartialContent));

            DgnUnderlayItem dgnItem = underlayItem as DgnUnderlayItem;
            if (dgnItem != null) {
                Stream(data, dgnItem);
                return;
            }
        }

        private void
        Stream(ArrayList data, DgnUnderlayItem dgnItem)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DgnUnderlayItem)));
            
            data.Add(new Snoop.Data.Bool("Show raster ref", dgnItem.ShowRasterRef));
            data.Add(new Snoop.Data.Bool("Use master units", dgnItem.UseMasterUnits));           
        }

        private void
        Stream(ArrayList data, DynamicBlockReferenceProperty dynBlkRefProp)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DynamicBlockReferenceProperty)));

            data.Add(new Snoop.Data.ObjectId("Block Id", dynBlkRefProp.BlockId));
            data.Add(new Snoop.Data.String("Description", dynBlkRefProp.Description));
            data.Add(new Snoop.Data.String("Property name", dynBlkRefProp.PropertyName));
            data.Add(new Snoop.Data.Int("Property type code", dynBlkRefProp.PropertyTypeCode));
            data.Add(new Snoop.Data.String("Units type", dynBlkRefProp.UnitsType.ToString()));           
        }

        private void
        Stream(ArrayList data, DynamicDimensionChangedEventArgs dimChangeArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DynamicDimensionChangedEventArgs)));

            data.Add(new Snoop.Data.Int("Index", dimChangeArgs.Index));
            data.Add(new Snoop.Data.Double("Value", dimChangeArgs.Value));           
        }

        private void
        Stream(ArrayList data, DynamicDimensionData dimData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DynamicDimensionData)));

            data.Add(new Snoop.Data.Object("Dimension", dimData.Dimension));
            data.Add(new Snoop.Data.Bool("Editable", dimData.Editable));
            data.Add(new Snoop.Data.Bool("Focal", dimData.Focal));
            data.Add(new Snoop.Data.Bool("Hide if value is zero", dimData.HideIfValueIsZero));
            data.Add(new Snoop.Data.Bool("Visible", dimData.Visible));
        }

        private void
        Stream(ArrayList data, EntityAlignmentEventArgs entAlignEventArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EntityAlignmentEventArgs)));

            data.Add(new Snoop.Data.Point3d("Closest point", entAlignEventArgs.ClosestPoint));
            data.Add(new Snoop.Data.Vector3d("Direction", entAlignEventArgs.Direction));
            data.Add(new Snoop.Data.Object("Entity", entAlignEventArgs.Entity));
            data.Add(new Snoop.Data.Vector3d("Normal", entAlignEventArgs.Normal));
            data.Add(new Snoop.Data.Point3d("Pick point", entAlignEventArgs.PickPoint));
        }

        private void
        Stream(ArrayList data, FileDependencyManager fileDepMgr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FileDependencyManager)));

            data.Add(new Snoop.Data.Int("Count entries", fileDepMgr.CountEntries));
            data.Add(new Snoop.Data.Int("Iterator next", fileDepMgr.IteratorNext));            
        }

        private void
        Stream(ArrayList data, IdMappingEventArgs idMapArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(IdMappingEventArgs)));

            data.Add(new Snoop.Data.Object("Id mapping", idMapArgs.IdMapping));            
        }

        private void
        Stream(ArrayList data, LayerStateDeletedEventArgs layerStateDelArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerStateDeletedEventArgs)));

            data.Add(new Snoop.Data.Object("Name", layerStateDelArgs.Name));          
        }

        private void
        Stream(ArrayList data, LayerStateEventArgs layerStateArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerStateEventArgs)));

            data.Add(new Snoop.Data.ObjectId("Id", layerStateArgs.Id));
            data.Add(new Snoop.Data.String("Name", layerStateArgs.Name));
        }

        private void
        Stream(ArrayList data, LayerStateRenameEventArgs layerStateRenArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerStateRenameEventArgs)));

            data.Add(new Snoop.Data.String("Name", layerStateRenArgs.Name));
            data.Add(new Snoop.Data.String("New name", layerStateRenArgs.NewName));
        }

        private void
        Stream(ArrayList data, LayerViewportProperties layerViewportProps)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerViewportProperties)));

            data.Add(new Snoop.Data.Object("Color", layerViewportProps.Color));
            data.Add(new Snoop.Data.Bool("Is color overridden", layerViewportProps.IsColorOverridden));
            data.Add(new Snoop.Data.Bool("Is linetype overridden", layerViewportProps.IsLinetypeOverridden));
            data.Add(new Snoop.Data.Bool("Is line weight overridden", layerViewportProps.IsLineWeightOverridden));
            data.Add(new Snoop.Data.Bool("Is plot style overridden", layerViewportProps.IsPlotStyleOverridden));
            data.Add(new Snoop.Data.ObjectId("Linetype object Id", layerViewportProps.LinetypeObjectId));
            data.Add(new Snoop.Data.String("Line weight", layerViewportProps.LineWeight.ToString()));
            data.Add(new Snoop.Data.String("Plot style name", layerViewportProps.PlotStyleName));
            data.Add(new Snoop.Data.ObjectId("Plot style name Id", layerViewportProps.PlotStyleNameId));          
        }

        private void
        Stream(ArrayList data, MTextFragment mtextFrag)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MTextFragment)));

            data.Add(new Snoop.Data.String("Big font", mtextFrag.BigFont));
            data.Add(new Snoop.Data.Bool("Bold", mtextFrag.Bold));
            data.Add(new Snoop.Data.Double("Caps height", mtextFrag.CapsHeight));
            data.Add(new Snoop.Data.Object("Color", mtextFrag.Color));
            data.Add(new Snoop.Data.Vector3d("Direction", mtextFrag.Direction));
            data.Add(new Snoop.Data.Point2d("Extents", mtextFrag.Extents));
            data.Add(new Snoop.Data.Point3d("Location", mtextFrag.Location));
            data.Add(new Snoop.Data.Bool("Italic", mtextFrag.Italic));
            data.Add(new Snoop.Data.Vector3d("Normal", mtextFrag.Normal));
            data.Add(new Snoop.Data.Double("Oblique angle", mtextFrag.ObliqueAngle));
            data.Add(new Snoop.Data.Bool("Overlined", mtextFrag.Overlined));
            data.Add(new Snoop.Data.String("Shx font", mtextFrag.ShxFont));
            data.Add(new Snoop.Data.Bool("Stack bottom", mtextFrag.StackBottom));
            data.Add(new Snoop.Data.Bool("Stack top", mtextFrag.StackTop));
            data.Add(new Snoop.Data.String("Text", mtextFrag.Text));
            data.Add(new Snoop.Data.Double("Tracking factor", mtextFrag.TrackingFactor));
            data.Add(new Snoop.Data.String("True type font", mtextFrag.TrueTypeFont));
            data.Add(new Snoop.Data.Bool("Underlined", mtextFrag.Underlined));
            data.Add(new Snoop.Data.Double("Width factor", mtextFrag.WidthFactor));
        }

        private void
        Stream(ArrayList data, ObjectClosedEventArgs objClosedArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ObjectClosedEventArgs)));

            data.Add(new Snoop.Data.ObjectId("Id", objClosedArgs.Id));            
        }

        private void
        Stream(ArrayList data, ObjectErasedEventArgs objErasedArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ObjectErasedEventArgs)));

            data.Add(new Snoop.Data.Object("DBObject", objErasedArgs.DBObject));
            data.Add(new Snoop.Data.Bool("Erased", objErasedArgs.Erased));
        }

        private void
        Stream(ArrayList data, ObjectSnapContext objSnapCtxt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ObjectSnapContext)));

            data.Add(new Snoop.Data.Int("Graphics system selection mark", objSnapCtxt.GraphicsSystemSelectionMark));
            data.Add(new Snoop.Data.Point3d("Last point", objSnapCtxt.LastPoint));
            data.Add(new Snoop.Data.Object("Picked object", objSnapCtxt.PickedObject));
            data.Add(new Snoop.Data.Point3d("Pick point", objSnapCtxt.PickPoint));
            data.Add(new Snoop.Data.Object("View transform", objSnapCtxt.ViewTransform));       
        }

        private void
        Stream(ArrayList data, ObjectSnapInfo objSnapInfo)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ObjectSnapInfo)));

            data.Add(new Snoop.Data.Enumerable("Snap curves", objSnapInfo.SnapCurves));
            data.Add(new Snoop.Data.Enumerable("Snap points", objSnapInfo.SnapPoints));            
        }

        private void
        Stream(ArrayList data, RevolveOptionsCheckRevolveCurveOut revOptsChkRevCurOut)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RevolveOptionsCheckRevolveCurveOut)));

            data.Add(new Snoop.Data.Bool("Closed", revOptsChkRevCurOut.Closed));
            data.Add(new Snoop.Data.Bool("End points on axis", revOptsChkRevCurOut.EndPointsOnAxis));
            data.Add(new Snoop.Data.Bool("Planar", revOptsChkRevCurOut.Planar));
        }

        private void
        Stream(ArrayList data, SweepOptionsCheckSweepCurveOut sweepOptsChkSweepCur)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SweepOptionsCheckSweepCurveOut)));

            data.Add(new Snoop.Data.Double("Approximate arc length", sweepOptsChkSweepCur.ApproximateArcLength));
            data.Add(new Snoop.Data.Bool("Closed", sweepOptsChkSweepCur.Closed));
            data.Add(new Snoop.Data.Object("Planarity", sweepOptsChkSweepCur.Planarity));
            data.Add(new Snoop.Data.Point3d("Point", sweepOptsChkSweepCur.Point));
            data.Add(new Snoop.Data.Vector3d("Vector", sweepOptsChkSweepCur.Vector));
        }

        private void
        Stream(ArrayList data, UnderlayFile underlayFile)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(UnderlayFile)));

            data.Add(new Snoop.Data.Enumerable("Items", underlayFile.Items));           
        }

        private void
        Stream(ArrayList data, UnderlayLayer underlayLayer)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(UnderlayLayer)));

            data.Add(new Snoop.Data.String("Name", underlayLayer.Name));
            data.Add(new Snoop.Data.String("State", underlayLayer.State.ToString()));
        }     
  
        private void
        Stream(ArrayList data, DataAdapter dataAdapt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DataAdapter)));

            data.Add(new Snoop.Data.String("Data adapter Id", dataAdapt.DataAdapterId));
            data.Add(new Snoop.Data.String("Description", dataAdapt.Description));
            data.Add(new Snoop.Data.String("Name", dataAdapt.Name));      
        }

        private void
        Stream(ArrayList data, DataCell dataCell)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DataCell)));

            data.Add(new Snoop.Data.String("Cell type", dataCell.CellType.ToString()));                                    
        }

        private void
        Stream(ArrayList data, DataColumn dataCol)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DataColumn)));

            data.Add(new Snoop.Data.String("Column name", dataCol.ColumnName));
            data.Add(new Snoop.Data.String("Column type", dataCol.ColumnType.ToString()));
            data.Add(new Snoop.Data.Int("Number of cells", dataCol.NumCells));
            data.Add(new Snoop.Data.Int("Grow length", dataCol.GrowLength));
            data.Add(new Snoop.Data.Int("Physical length", dataCol.PhysicalLength));
        }

        private void
        Stream(ArrayList data, DataLinkManager dataLinkMan)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DataLinkManager)));

            data.Add(new Snoop.Data.Int("Data link count", dataLinkMan.DataLinkCount));          
        }

        private void
        Stream(ArrayList data, DbDictionaryEnumerator dbDictEnum)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DbDictionaryEnumerator)));

            data.Add(new Snoop.Data.Object("Current", dbDictEnum.Current));
            data.Add(new Snoop.Data.Object("Entry", dbDictEnum.Entry));
            data.Add(new Snoop.Data.String("Key", dbDictEnum.Key));
            data.Add(new Snoop.Data.ObjectId("Value", dbDictEnum.Value));           
        }

        private void
        Stream(ArrayList data, DwgFiler dwgFiler)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DwgFiler)));

            data.Add(new Snoop.Data.Object("Dwg version", dwgFiler.DwgVersion));
            data.Add(new Snoop.Data.String("Filer status", dwgFiler.FilerStatus.ToString()));
            data.Add(new Snoop.Data.String("Filer type", dwgFiler.FilerType.ToString()));
            data.Add(new Snoop.Data.Long("Position", dwgFiler.Position));            
        }

        private void
        Stream(ArrayList data, DxfFiler dxfFiler)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DxfFiler)));

            data.Add(new Snoop.Data.Bool("At embedded object start", dxfFiler.AtEmbeddedObjectStart));
            data.Add(new Snoop.Data.Bool("At end of file", dxfFiler.AtEndOfFile));
            data.Add(new Snoop.Data.Bool("At end of object", dxfFiler.AtEndOfObject));
            data.Add(new Snoop.Data.Bool("At extended data", dxfFiler.AtExtendedData));
            data.Add(new Snoop.Data.Database("Database", dxfFiler.Database));
            data.Add(new Snoop.Data.Object("Dwg version", dxfFiler.DwgVersion));
            data.Add(new Snoop.Data.Double("Elevation", dxfFiler.Elevation));
            data.Add(new Snoop.Data.String("Error message", dxfFiler.ErrorMessage));
            data.Add(new Snoop.Data.String("Filer type", dxfFiler.FilerType.ToString()));
            data.Add(new Snoop.Data.Bool("Includes default values", dxfFiler.IncludesDefaultValues));
            data.Add(new Snoop.Data.Bool("Is modifying existing object", dxfFiler.IsModifyingExistingObject));
            data.Add(new Snoop.Data.Int("Precision", dxfFiler.Precision));
            data.Add(new Snoop.Data.Double("Thickness", dxfFiler.Thickness));            
        }

        private void
        Stream(ArrayList data, IdMapping idMap)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(IdMapping)));

            data.Add(new Snoop.Data.String("Deep clone context", idMap.DeepCloneContext.ToString()));
            data.Add(new Snoop.Data.Database("Destination database", idMap.DestinationDatabase));
            data.Add(new Snoop.Data.String("Duplicate record cloning", idMap.DuplicateRecordCloning.ToString()));
            data.Add(new Snoop.Data.Database("Original database", idMap.OriginalDatabase));                               
        }

        private void
        Stream(ArrayList data, LayerStateManager stateMgr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayerStateManager)));

            data.Add(new Snoop.Data.String("Last restored layer state", stateMgr.LastRestoredLayerState));          
        }

        private void
        Stream(ArrayList data, LayoutManager layoutMgr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LayoutManager)));

            data.Add(new Snoop.Data.String("Current layout", layoutMgr.CurrentLayout));
            data.Add(new Snoop.Data.Int("Layout count", layoutMgr.LayoutCount));            
        }

        private void
        Stream(ArrayList data, ObjectContext objContext)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ObjectContext)));

            data.Add(new Snoop.Data.String("Collection name", objContext.CollectionName));
            data.Add(new Snoop.Data.String("Name", objContext.Name));
        }

        private void
        Stream(ArrayList data, DatabaseSummaryInfo summaryInfo)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DatabaseSummaryInfo)));

            data.Add(new Snoop.Data.String("Title", summaryInfo.Title));
            data.Add(new Snoop.Data.String("Subject", summaryInfo.Subject));
            data.Add(new Snoop.Data.String("Author", summaryInfo.Author));
            data.Add(new Snoop.Data.String("Comments", summaryInfo.Comments));
            data.Add(new Snoop.Data.String("Keywords", summaryInfo.Keywords));
            data.Add(new Snoop.Data.String("Hyperlink base", summaryInfo.HyperlinkBase));
            data.Add(new Snoop.Data.String("Last saved by", summaryInfo.LastSavedBy));
            data.Add(new Snoop.Data.String("Revision number", summaryInfo.RevisionNumber));

			System.Collections.IDictionaryEnumerator iter = summaryInfo.CustomProperties;
            data.Add(new Snoop.Data.CategorySeparator("Custom Properties"));
			while (iter.MoveNext()) {
                data.Add(new Snoop.Data.String((string)iter.Key, (string)iter.Value));
            }
        }

        private void
        Stream(ArrayList data, FullSubentityPath subEntPath)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FullSubentityPath)));

            data.Add(new Snoop.Data.Object("Subent ID", subEntPath.SubentId));
            data.Add(new Snoop.Data.ObjectIdCollection("Object IDs", subEntPath.GetObjectIds()));
        }

        private void
        Stream(ArrayList data, SubentityId subEntId)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SubentityId)));

            //data.Add(new Snoop.Data.Int("Index", subEntId.Index));    // TBD: fix JMA
            data.Add(new Snoop.Data.String("Type", subEntId.Type.ToString()));
            data.Add(new Snoop.Data.Object("Type class", subEntId.TypeClass));
        }

        private void
        Stream(ArrayList data, DataTypeParameter dataTypeParam)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DataTypeParameter)));

            data.Add(new Snoop.Data.String("Data type", dataTypeParam.DataType.ToString()));
            data.Add(new Snoop.Data.String("Unit type", dataTypeParam.UnitType.ToString()));            
        }

        private void
        Stream(ArrayList data, DBDictionaryEntry dbDictEntry)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DBDictionaryEntry)));

            data.Add(new Snoop.Data.String("Key", dbDictEntry.Key));
            data.Add(new Snoop.Data.ObjectId("Value", dbDictEntry.Value));
        }

        private void
        Stream(ArrayList data, FileDependencyInfo depInfo)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FileDependencyInfo)));

            data.Add(new Snoop.Data.String("Feature", depInfo.Feature));
            data.Add(new Snoop.Data.String("File name", depInfo.FileName));
            data.Add(new Snoop.Data.Int("File size", depInfo.FileSize));
            data.Add(new Snoop.Data.String("Fingerprint guid", depInfo.FingerprintGuid));
            data.Add(new Snoop.Data.String("Found path", depInfo.FoundPath));
            data.Add(new Snoop.Data.String("Full filename", depInfo.FullFileName));
            data.Add(new Snoop.Data.Int("Index", depInfo.Index));
            data.Add(new Snoop.Data.Bool("Is affects graphics", depInfo.IsAffectsGraphics));
            data.Add(new Snoop.Data.Bool("Is modified", depInfo.IsModified));
            data.Add(new Snoop.Data.Int("Reference count", depInfo.ReferenceCount));
            data.Add(new Snoop.Data.Int("Time  stamp", depInfo.TimeStamp));
            data.Add(new Snoop.Data.String("Version guid", depInfo.VersionGuid));
        }

        private void
        Stream(ArrayList data, FullDwgVersion dwgVer)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FullDwgVersion)));
             
            data.Add(new Snoop.Data.Object("Major version", dwgVer.MajorVersion));
            data.Add(new Snoop.Data.Object("Minor version", dwgVer.MinorVersion));         
        }

        private void
        Stream(ArrayList data, GridPropertyParameter gridPropParam)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(GridPropertyParameter)));

            data.Add(new Snoop.Data.String("PropertyMask", gridPropParam.PropertyMask.ToString()));
            data.Add(new Snoop.Data.String("Line style", gridPropParam.LineStyle.ToString()));
            data.Add(new Snoop.Data.ObjectId("Line type", gridPropParam.Linetype));
            data.Add(new Snoop.Data.String("Line weight", gridPropParam.LineWeight.ToString()));
            data.Add(new Snoop.Data.Object("Color", gridPropParam.Color));
            data.Add(new Snoop.Data.Object("Visibility", gridPropParam.Visibility));
            data.Add(new Snoop.Data.Double("Double line spacing", gridPropParam.DoubleLineSpacing));           
        }

        private void
        Stream(ArrayList data, MeshPointMap meshPtMap)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MeshPointMap)));

            data.Add(new Snoop.Data.Point2d("Destination point", meshPtMap.DestPoint));
            data.Add(new Snoop.Data.Point2d("Source point", meshPtMap.SourcePoint));
        }

        private void
        Stream(ArrayList data, MeshPointMaps meshPtMaps)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MeshPointMaps)));

            data.Add(new Snoop.Data.Enumerable("Destination points", meshPtMaps.DestPonints));
            data.Add(new Snoop.Data.Enumerable("Source points", meshPtMaps.SourcePonints)); // TBD: Property spelt incorrectly
        }

        private void
        Stream(ArrayList data, Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilterDefinition spaceFiltDef)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilterDefinition)));

            data.Add(new Snoop.Data.Double("Back clip", spaceFiltDef.BackClip));
            data.Add(new Snoop.Data.Double("Elevation", spaceFiltDef.Elevation));
            data.Add(new Snoop.Data.Bool("Enabled", spaceFiltDef.Enabled));
            data.Add(new Snoop.Data.Double("Front clip", spaceFiltDef.FrontClip));
            data.Add(new Snoop.Data.Vector3d("Normal", spaceFiltDef.Normal));

            Point2dCollection points = new Point2dCollection();            
            points = spaceFiltDef.GetPoints();

            data.Add(new Snoop.Data.CategorySeparator("Points"));
            for (int i = 0; i < points.Count; i++) {
                data.Add(new Snoop.Data.Point2d(string.Format("Color [{0}]", i), points[i]));
            }                             
        }

        private void
        Stream(ArrayList data, Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilterVolume spaceFiltVol)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.DatabaseServices.Filters.SpatialFilterVolume)));

            data.Add(new Snoop.Data.Point3d("From point", spaceFiltVol.FromPoint));
            data.Add(new Snoop.Data.Point3d("To point", spaceFiltVol.ToPoint));
            data.Add(new Snoop.Data.Vector3d("Up direction", spaceFiltVol.UpDirection));
            data.Add(new Snoop.Data.Vector2d("View field", spaceFiltVol.ViewField));            
        }
    }
}
