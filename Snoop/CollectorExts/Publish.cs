
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
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text;

using MgdDbg.Snoop.Collectors;

using Autodesk.AutoCAD.Publishing;

namespace MgdDbg.Snoop.CollectorExts {

    class Publish : CollectorExt {

		public
		Publish()
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
            AboutToBeginPublishingEventArgs aboutBeginPub = e.ObjToSnoop as AboutToBeginPublishingEventArgs;
            if (aboutBeginPub != null) {
                Stream(snoopCollector.Data(), aboutBeginPub);
                return;
            }

            AboutToBeginBackgroundPublishingEventArgs aboutBeginBgPub = e.ObjToSnoop as AboutToBeginBackgroundPublishingEventArgs;
            if (aboutBeginBgPub != null) {
                Stream(snoopCollector.Data(), aboutBeginBgPub);
                return;
            }

            PublishEventArgs pubEvent = e.ObjToSnoop as PublishEventArgs;
            if (pubEvent != null) {
                Stream(snoopCollector.Data(), pubEvent);
                return;
            }

            BeginPublishingSheetEventArgs beginSheetArgs = e.ObjToSnoop as BeginPublishingSheetEventArgs;
            if (beginSheetArgs != null) {
                Stream(snoopCollector.Data(), beginSheetArgs);
                return;
            }

            BeginAggregationEventArgs beginAggrArgs = e.ObjToSnoop as BeginAggregationEventArgs;
            if (beginAggrArgs != null) {
                Stream(snoopCollector.Data(), beginAggrArgs);
                return;
            }

            PublishSheetEventArgs pubSheetArgs = e.ObjToSnoop as PublishSheetEventArgs;
            if (pubSheetArgs != null) {
                Stream(snoopCollector.Data(), pubSheetArgs);
                return;
            }
          
            PublishEntityEventArgs pubEntityArgs = e.ObjToSnoop as PublishEntityEventArgs;
            if (pubEntityArgs != null) {
                Stream(snoopCollector.Data(), pubEntityArgs);
                return;
            }

            PublishUIEventArgs pubUiArgs = e.ObjToSnoop as PublishUIEventArgs;
            if (pubUiArgs != null) {
                Stream(snoopCollector.Data(), pubUiArgs);
                return;
            }

            Dwf3dNavigationTreeNode dwf3dNavNode = e.ObjToSnoop as Dwf3dNavigationTreeNode;
            if (dwf3dNavNode != null) {
                Stream(snoopCollector.Data(), dwf3dNavNode);
                return;
            }

            DwfNode dwfNode = e.ObjToSnoop as DwfNode;
            if (dwfNode != null) {
                Stream(snoopCollector.Data(), dwfNode);
                return;
            }

            EPlotAttribute eplotAttr = e.ObjToSnoop as EPlotAttribute;
            if (eplotAttr != null) {
                Stream(snoopCollector.Data(), eplotAttr);
                return;
            }

            EPlotProperty eplotProp = e.ObjToSnoop as EPlotProperty;
            if (eplotProp != null) {
                Stream(snoopCollector.Data(), eplotProp);
                return;
            }

            EPlotPropertyBag eplotPropBag = e.ObjToSnoop as EPlotPropertyBag;
            if (eplotPropBag != null) {
                Stream(snoopCollector.Data(), eplotPropBag);
                return;
            }

            EPlotResource eplotResource = e.ObjToSnoop as EPlotResource;
            if (eplotResource != null) {
                Stream(snoopCollector.Data(), eplotResource);
                return;
            }
        }
        
        private void
        Stream(ArrayList data, AboutToBeginPublishingEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AboutToBeginPublishingEventArgs)));

            data.Add(new Snoop.Data.Bool("Job will publish in background", args.JobWillPublishInBackground));
            data.Add(new Snoop.Data.Object("Dsd data", args.DsdData));
            data.Add(new Snoop.Data.Object("Plot logger", args.PlotLogger));
        }

        private void
        Stream(ArrayList data, AboutToBeginBackgroundPublishingEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AboutToBeginBackgroundPublishingEventArgs)));

            data.Add(new Snoop.Data.Bool("Job will publish in background", args.JobWillPublishInBackground));
            data.Add(new Snoop.Data.Object("Dsd data", args.DsdData));
        }

        private void
        Stream(ArrayList data, PublishEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PublishEventArgs)));

            data.Add(new Snoop.Data.String("DWF filename", args.DwfFileName));
            data.Add(new Snoop.Data.String("DWF password", args.DwfPassword));
            data.Add(new Snoop.Data.Bool("Is multi-sheet DWF", args.IsMultiSheetDwf));
            data.Add(new Snoop.Data.String("Temporary DWF filename", args.TemporaryDwfFileName));
        }

        private void
        Stream(ArrayList data, BeginPublishingSheetEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BeginPublishingSheetEventArgs)));

            data.Add(new Snoop.Data.Object("Dsd entry", args.DsdEntry));
            data.Add(new Snoop.Data.Object("Plot logger", args.PlotLogger));
            data.Add(new Snoop.Data.String("Unique ID", args.UniqueId));
        }

        private void
        Stream(ArrayList data, BeginAggregationEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BeginAggregationEventArgs)));

            data.Add(new Snoop.Data.String("DWF filename", args.DwfFileName));
            data.Add(new Snoop.Data.String("DWF password", args.DwfPassword));
            data.Add(new Snoop.Data.Object("Plot logger", args.PlotLogger));
        }

        private void
        Stream(ArrayList data, PublishSheetEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PublishSheetEventArgs)));

            data.Add(new Snoop.Data.Bool("Are lines hidden", args.AreLinesHidden));
            data.Add(new Snoop.Data.Bool("Are plotting lineweights", args.ArePlottingLineWeights));
            data.Add(new Snoop.Data.Bool("Are scaling lineweights", args.AreScalingLineWeights));
            data.Add(new Snoop.Data.String("Canonical media name", args.CanonicalMediaName));
            data.Add(new Snoop.Data.String("Configuration", args.Configuration));
            data.Add(new Snoop.Data.Database("Database", args.Database));
            data.Add(new Snoop.Data.Double("Display max X", args.DisplayMaxX));
            data.Add(new Snoop.Data.Double("Display max Y", args.DisplayMaxY));
            data.Add(new Snoop.Data.Double("Display min X", args.DisplayMinX));
            data.Add(new Snoop.Data.Double("Display min Y", args.DisplayMinY));
            data.Add(new Snoop.Data.Double("Drawing scale", args.DrawingScale));
            data.Add(new Snoop.Data.Object("DWF 3D navigation tree node", args.Dwf3dNavigationTreeNode));
            data.Add(new Snoop.Data.Double("Effective plot offset X", args.EffectivePlotOffsetX));
            data.Add(new Snoop.Data.Double("Effective plot offset Y", args.EffectivePlotOffsetY));
            data.Add(new Snoop.Data.Double("Effective plot offset X device", args.EffectivePlotOffsetXDevice));
            data.Add(new Snoop.Data.Double("Effective plot offset Y device", args.EffectivePlotOffsetYDevice));
            data.Add(new Snoop.Data.Bool("Is model layout", args.IsModelLayout));
            data.Add(new Snoop.Data.Bool("Is plot job cancelled", args.IsPlotJobCancelled));
            data.Add(new Snoop.Data.Bool("Is scale specified", args.IsScaleSpecified));
            data.Add(new Snoop.Data.Double("Layout bounds max X", args.LayoutBoundsMaxX));
            data.Add(new Snoop.Data.Double("Layout bounds max Y", args.LayoutBoundsMaxY));
            data.Add(new Snoop.Data.Double("Layout bounds min X", args.LayoutBoundsMinX));
            data.Add(new Snoop.Data.Double("Layout bounds min Y", args.LayoutBoundsMinY));
            data.Add(new Snoop.Data.Double("Layout margin max X", args.LayoutMarginMaxX));
            data.Add(new Snoop.Data.Double("Layout margin max Y", args.LayoutMarginMaxY));
            data.Add(new Snoop.Data.Double("Layout margin min X", args.LayoutMarginMinX));
            data.Add(new Snoop.Data.Double("Layout margin min Y", args.LayoutMarginMinY));
            data.Add(new Snoop.Data.Double("Max bounds X", args.MaxBoundsX));
            data.Add(new Snoop.Data.Double("Max bounds Y", args.MaxBoundsY));
            data.Add(new Snoop.Data.Double("Origin X", args.OriginX));
            data.Add(new Snoop.Data.Double("Origin Y", args.OriginY));
            data.Add(new Snoop.Data.Double("Paper scale", args.PaperScale));
            data.Add(new Snoop.Data.Double("Plot bounds max X", args.PlotBoundsMaxX));
            data.Add(new Snoop.Data.Double("Plot bounds max Y", args.PlotBoundsMaxY));
            data.Add(new Snoop.Data.Double("Plot bounds min X", args.PlotBoundsMinX));
            data.Add(new Snoop.Data.Double("Plot bounds min Y", args.PlotBoundsMinY));
            data.Add(new Snoop.Data.ObjectId("Plot layout ID", args.PlotLayoutId));
            data.Add(new Snoop.Data.Object("Plot logger", args.PlotLogger));
            data.Add(new Snoop.Data.String("Plot paper unit", args.PlotPaperUnit.ToString()));
            data.Add(new Snoop.Data.String("Plot rotation", args.PlotRotation.ToString()));
            data.Add(new Snoop.Data.String("Plot to file name", args.PlotToFileName));
            data.Add(new Snoop.Data.String("Plot to file path", args.PlotToFilePath));
            data.Add(new Snoop.Data.String("Plot type", args.PlotType.ToString()));
            data.Add(new Snoop.Data.Double("Plot window max X", args.PlotWindowMaxX));
            data.Add(new Snoop.Data.Double("Plot window max Y", args.PlotWindowMaxY));
            data.Add(new Snoop.Data.Double("Plot window min X", args.PlotWindowMinX));
            data.Add(new Snoop.Data.Double("Plot window min Y", args.PlotWindowMinY));
            data.Add(new Snoop.Data.Double("Printable bounds X", args.PrintableBoundsX));
            data.Add(new Snoop.Data.Double("Printable bounds Y", args.PrintableBoundsY));
            data.Add(new Snoop.Data.Bool("Publishing to 3D DWF", args.PublishingTo3DDwf));
            data.Add(new Snoop.Data.Double("Steps per inch", args.StepsPerInch));
            data.Add(new Snoop.Data.String("Unique layout ID", args.UniqueLayoutId));
            data.Add(new Snoop.Data.String("View plotted", args.ViewPlotted));
        }

        private void
        Stream(ArrayList data, PublishEntityEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PublishEntityEventArgs)));

            data.Add(new Snoop.Data.ObjectId("Effective block layer ID", args.EffectiveBlockLayerId));
            data.Add(new Snoop.Data.Object("Entity", args.Entity));
            data.Add(new Snoop.Data.Bool("Is cancelled", args.IsCancelled));
            data.Add(new Snoop.Data.Object("Plot logger", args.PlotLogger));
            data.Add(new Snoop.Data.String("Unique entity ID", args.UniqueEntityId));
        }

        private void
        Stream(ArrayList data, PublishUIEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PublishUIEventArgs)));

            data.Add(new Snoop.Data.Object("Dsd data", args.DsdData));
            //data.Add(new Snoop.Data.ObjectUnknown("IUnknown", args.IUnknown));
            data.Add(new Snoop.Data.Bool("Job will publish in background", args.JobWillPublishInBackground));
        }

        private void
        Stream(ArrayList data, Dwf3dNavigationTreeNode dwf3dNavNode)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Dwf3dNavigationTreeNode)));
            
            data.Add(new Snoop.Data.String("Display name", dwf3dNavNode.DisplayName));
            data.Add(new Snoop.Data.Bool("Is block", dwf3dNavNode.IsBlock));
            data.Add(new Snoop.Data.Bool("Is group", dwf3dNavNode.IsGroup));

            ArrayList nodes = new ArrayList();
            for (int i=0; i<dwf3dNavNode.Children.Count; i++) {
                nodes.Add(dwf3dNavNode.Children[i]);
            }
            data.Add(new MgdDbg.Snoop.Data.ObjectCollection("Dwf 3d navigation tree nodes", nodes));         
        }

        private void
        Stream(ArrayList data, DwfNode dwfNode)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DwfNode)));

            data.Add(new Snoop.Data.Int("Node Id", dwfNode.NodeId));
            data.Add(new Snoop.Data.String("Node name", dwfNode.NodeName));           
        }

        private void
        Stream(ArrayList data, EPlotAttribute eplotAttr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EPlotAttribute)));

            data.Add(new Snoop.Data.String("Name", eplotAttr.Name));
            data.Add(new Snoop.Data.String("Ns", eplotAttr.Ns));
            data.Add(new Snoop.Data.String("Ns url", eplotAttr.NsUrl));
            data.Add(new Snoop.Data.String("Value", eplotAttr.Value));
        }

        private void
        Stream(ArrayList data, EPlotProperty eplotProperty)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EPlotProperty)));

            data.Add(new Snoop.Data.Enumerable("Attributes", eplotProperty.Attributes));
            data.Add(new Snoop.Data.String("Category", eplotProperty.Category));
            data.Add(new Snoop.Data.String("Name", eplotProperty.Name));
            data.Add(new Snoop.Data.String("Type", eplotProperty.Type));
            data.Add(new Snoop.Data.String("Units", eplotProperty.Units));
            data.Add(new Snoop.Data.String("Value", eplotProperty.Value));            
        }

        private void
        Stream(ArrayList data, EPlotPropertyBag eplotPropertyBag)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EPlotPropertyBag)));

            data.Add(new Snoop.Data.String("Id", eplotPropertyBag.Id));
            data.Add(new Snoop.Data.String("Namespace location", eplotPropertyBag.NamespaceLocation));
            data.Add(new Snoop.Data.String("Namespace url", eplotPropertyBag.NamespaceUrl));
            data.Add(new Snoop.Data.Enumerable("Properties", eplotPropertyBag.Properties));
            data.Add(new Snoop.Data.Enumerable("References", eplotPropertyBag.References));
        }

        private void
        Stream(ArrayList data, EPlotResource eplotRes)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EPlotResource)));

            data.Add(new Snoop.Data.String("Mime", eplotRes.Mime));
            data.Add(new Snoop.Data.String("Path", eplotRes.Path));
            data.Add(new Snoop.Data.String("Role", eplotRes.Role));          
        }        
    }
}
