
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

using Autodesk.AutoCAD.PlottingServices;

namespace MgdDbg.Snoop.CollectorExts {

    class Plotting : CollectorExt {

		public
		Plotting()
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
            PlotLogger log = e.ObjToSnoop as PlotLogger;
            if (log != null) {
                Stream(snoopCollector.Data(), log);
                return;
            }

            DsdData dsdData = e.ObjToSnoop as DsdData;
            if (dsdData != null) {
                Stream(snoopCollector.Data(), dsdData);
                return;
            }

            DsdEntry dsdEntry = e.ObjToSnoop as DsdEntry;
            if (dsdEntry != null) {
                Stream(snoopCollector.Data(), dsdEntry);
                return;
            }

            Dwf3dOptions dwf3dOpt = e.ObjToSnoop as Dwf3dOptions;
            if (dwf3dOpt != null) {
                Stream(snoopCollector.Data(), dwf3dOpt);
                return;
            }

            BeginDocumentEventArgs beginDocArgs = e.ObjToSnoop as BeginDocumentEventArgs;
            if (beginDocArgs != null) {
                Stream(snoopCollector.Data(), beginDocArgs);
                return;
            }

            BeginPageEventArgs beginPageArgs = e.ObjToSnoop as BeginPageEventArgs;
            if (beginPageArgs != null) {
                Stream(snoopCollector.Data(), beginPageArgs);
                return;
            }

            BeginPlotEventArgs beginPlotArgs = e.ObjToSnoop as BeginPlotEventArgs;
            if (beginPlotArgs != null) {
                Stream(snoopCollector.Data(), beginPlotArgs);
                return;
            }

            EndDocumentEventArgs endDocArgs = e.ObjToSnoop as EndDocumentEventArgs;
            if (endDocArgs != null) {
                Stream(snoopCollector.Data(), endDocArgs);
                return;
            }

            EndPageEventArgs endPageArgs = e.ObjToSnoop as EndPageEventArgs;
            if (endPageArgs != null) {
                Stream(snoopCollector.Data(), endPageArgs);
                return;
            }

            EndPlotEventArgs endPlotArgs = e.ObjToSnoop as EndPlotEventArgs;
            if (endPlotArgs != null) {
                Stream(snoopCollector.Data(), endPlotArgs);
                return;
            }

            PlotInfo plotInfo = e.ObjToSnoop as PlotInfo;
            if (plotInfo != null) {
                Stream(snoopCollector.Data(), plotInfo);
                return;
            }

            PlotPageInfo plotPageInfo = e.ObjToSnoop as PlotPageInfo;
            if (plotPageInfo != null) {
                Stream(snoopCollector.Data(), plotPageInfo);
                return;
            }

            PlotConfig plotConfig = e.ObjToSnoop as PlotConfig;
            if (plotConfig != null) {
                Stream(snoopCollector.Data(), plotConfig);
                return;
            }

            PlotConfigInfo plotConfigInfo = e.ObjToSnoop as PlotConfigInfo;
            if (plotConfigInfo != null) {
                Stream(snoopCollector.Data(), plotConfigInfo);
                return;
            }

            PlotEngine plotEngine = e.ObjToSnoop as PlotEngine;
            if (plotEngine != null) {
                Stream(snoopCollector.Data(), plotEngine);
                return;
            }

            PlotInfoValidator plotInfoValidator = e.ObjToSnoop as PlotInfoValidator;
            if (plotInfoValidator != null) {
                Stream(snoopCollector.Data(), plotInfoValidator);
                return;
            }

            PlotProgress plotProgress = e.ObjToSnoop as PlotProgress;
            if (plotProgress != null) {
                Stream(snoopCollector.Data(), plotProgress);
                return;
            }

            // ValueTypes we have to treat a bit different
            if (e.ObjToSnoop is MediaBounds) {
                Stream(snoopCollector.Data(), (MediaBounds)e.ObjToSnoop);
                return;
            }
        }
        
        private void
        Stream(ArrayList data, PlotLogger log)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotLogger)));

            data.Add(new Snoop.Data.Bool("Error has happened in job", log.ErrorHasHappenedInJob));
            data.Add(new Snoop.Data.Bool("Error has happened in sheet", log.ErrorHasHappenedInSheet));
            data.Add(new Snoop.Data.Bool("Warning has happened in job", log.WarningHasHappenedInJob));
            data.Add(new Snoop.Data.Bool("Warning has happened in sheet", log.WarningHasHappenedInSheet));
        }

        private void
        Stream(ArrayList data, DsdData dsd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DsdData)));

            data.Add(new Snoop.Data.String("Category name", dsd.CategoryName));
            data.Add(new Snoop.Data.String("Destination name", dsd.DestinationName));
            data.Add(new Snoop.Data.Object("DWF 3D options", dsd.Dwf3dOptions));
            data.Add(new Snoop.Data.Bool("Is homogenous", dsd.IsHomogeneous));
            data.Add(new Snoop.Data.Bool("Is sheet set", dsd.IsSheetSet));
            data.Add(new Snoop.Data.String("Log file path", dsd.LogFilePath));
            data.Add(new Snoop.Data.Int("Major version", dsd.MajorVersion));
            data.Add(new Snoop.Data.Int("Minor version", dsd.MinorVersion));
            data.Add(new Snoop.Data.Int("Number of copies", dsd.NoOfCopies));
            data.Add(new Snoop.Data.String("Password", dsd.Password));
            data.Add(new Snoop.Data.String("Project path", dsd.ProjectPath));
            data.Add(new Snoop.Data.Bool("Plot stamp on", dsd.PlotStampOn));
            data.Add(new Snoop.Data.String("Selection set name", dsd.SelectionSetName));
            data.Add(new Snoop.Data.String("Sheet set name", dsd.SheetSetName));
            data.Add(new Snoop.Data.String("Sheet type", dsd.SheetType.ToString()));
            data.Add(new Snoop.Data.ObjectCollection("Unrecognized data section names", dsd.UnrecognizedDataSectionNames));
            data.Add(new Snoop.Data.ObjectCollection("Unrecognized data sections", dsd.UnrecognizedDataSections));
        }

        private void
        Stream(ArrayList data, DsdEntry dsd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DsdEntry)));

            data.Add(new Snoop.Data.String("Title", dsd.Title));
            data.Add(new Snoop.Data.String("DWG name", dsd.DwgName));
            data.Add(new Snoop.Data.String("Layout", dsd.Layout));
            data.Add(new Snoop.Data.String("Nps", dsd.Nps));
            data.Add(new Snoop.Data.String("Nps source DWG", dsd.NpsSourceDwg));
        }

        private void
        Stream(ArrayList data, Dwf3dOptions opt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Dwf3dOptions)));

            data.Add(new Snoop.Data.Bool("Group by Xref hierarchy", opt.GroupByXrefHierarchy));
            data.Add(new Snoop.Data.Bool("Publish with materials", opt.PublishWithMaterials));
        }

        private void
        Stream(ArrayList data, BeginDocumentEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BeginDocumentEventArgs)));

            data.Add(new Snoop.Data.Int("Copies", args.Copies));
            data.Add(new Snoop.Data.String("Document name", args.DocumentName));
            data.Add(new Snoop.Data.String("File name", args.FileName));
            data.Add(new Snoop.Data.Object("Plot info", args.PlotInfo));
            data.Add(new Snoop.Data.Bool("Plot to file", args.PlotToFile));
        }

        private void
        Stream(ArrayList data, BeginPageEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BeginPageEventArgs)));

            data.Add(new Snoop.Data.Object("Plot info", args.PlotInfo));
            data.Add(new Snoop.Data.Object("Plot page info", args.PlotPageInfo));
            data.Add(new Snoop.Data.Bool("Last page", args.LastPage));
        }

        private void
        Stream(ArrayList data, BeginPlotEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(BeginPlotEventArgs)));

            data.Add(new Snoop.Data.Object("Plot progress", args.PlotProgress));
            data.Add(new Snoop.Data.String("Plot type", args.PlotType.ToString()));
        }

        private void
        Stream(ArrayList data, EndDocumentEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EndDocumentEventArgs)));

            data.Add(new Snoop.Data.String("Status", args.Status.ToString()));
        }

        private void
        Stream(ArrayList data, EndPageEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EndPageEventArgs)));

            data.Add(new Snoop.Data.String("Status", args.Status.ToString()));
        }

        private void
        Stream(ArrayList data, EndPlotEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EndPlotEventArgs)));

            data.Add(new Snoop.Data.String("Status", args.Status.ToString()));
        }

        private void
        Stream(ArrayList data, PlotInfo info)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotInfo)));

            data.Add(new Snoop.Data.Object("Device override", info.DeviceOverride));
            data.Add(new Snoop.Data.Bool("Is validated", info.IsValidated));
            data.Add(new Snoop.Data.ObjectId("Layout", info.Layout));
            data.Add(new Snoop.Data.Int("Merge status", info.MergeStatus));
            /// this always says that the operation is not valid 'cos of current state
            /// jai 11.30.06
            //data.Add(new Snoop.Data.Object("Override settings", info.OverrideSettings));
            data.Add(new Snoop.Data.Object("Validated config", info.ValidatedConfig));
            data.Add(new Snoop.Data.Object("Validated settings", info.ValidatedSettings));
        }

        private void
        Stream(ArrayList data, PlotPageInfo info)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotPageInfo)));

            data.Add(new Snoop.Data.Int("Entity count", info.EntityCount));
            data.Add(new Snoop.Data.Int("Gradient count", info.GradientCount));
            data.Add(new Snoop.Data.Int("OLE object count", info.OleObjectCount));
            data.Add(new Snoop.Data.Int("Raster count", info.RasterCount));
            data.Add(new Snoop.Data.Int("Shaded viewport type", info.ShadedViewportType));
        }

        private void
        Stream(ArrayList data, PlotConfig info)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotConfig)));

            data.Add(new Snoop.Data.ObjectCollection("Canonical media names", info.CanonicalMediaNames));
            data.Add(new Snoop.Data.String("Comment", info.Comment));
            data.Add(new Snoop.Data.String("Default file extension", info.DefaultFileExtension));
            data.Add(new Snoop.Data.String("Device name", info.DeviceName));
            data.Add(new Snoop.Data.Int("Device type", info.DeviceType));
            data.Add(new Snoop.Data.String("Driver name", info.DriverName));
            data.Add(new Snoop.Data.String("Full path", info.FullPath));
            data.Add(new Snoop.Data.String("Location name", info.LocationName));
            data.Add(new Snoop.Data.Bool("Is plot to file", info.IsPlotToFile));
            data.Add(new Snoop.Data.Int("Max device dots per inch", info.MaximumDeviceDotsPerInch));
            data.Add(new Snoop.Data.String("Plot to file capability", info.PlotToFileCapability.ToString()));
            data.Add(new Snoop.Data.String("Port name", info.PortName));
            data.Add(new Snoop.Data.String("Server name", info.ServerName));
            data.Add(new Snoop.Data.String("Tag line", info.TagLine));
        }

        private void
        Stream(ArrayList data, PlotConfigInfo plotConfigInfo)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotConfigInfo)));

            data.Add(new Snoop.Data.String("Device name", plotConfigInfo.DeviceName));
            data.Add(new Snoop.Data.String("Device type", plotConfigInfo.DeviceType.ToString()));
            data.Add(new Snoop.Data.String("Full path", plotConfigInfo.FullPath));
        }

        private void
        Stream(ArrayList data, PlotEngine plotEngine)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotEngine)));

            data.Add(new Snoop.Data.Bool("Is background packaging", plotEngine.IsBackgroundPackaging));            
        } 

        private void
        Stream(ArrayList data, PlotInfoValidator plotInfoValidator)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotInfoValidator)));

            data.Add(new Snoop.Data.Int("Dimensional weight", plotInfoValidator.DimensionalWeight));
            data.Add(new Snoop.Data.Int("Media bounds weight", plotInfoValidator.MediaBoundsWeight));
            data.Add(new Snoop.Data.Int("Media group weight", plotInfoValidator.MediaGroupWeight));
            data.Add(new Snoop.Data.String("Media matching policy", plotInfoValidator.MediaMatchingPolicy.ToString()));
            data.Add(new Snoop.Data.Int("Media matching threshold", plotInfoValidator.MediaMatchingThreshold));
            data.Add(new Snoop.Data.Int("Printable bounds weight", plotInfoValidator.PrintableBoundsWeight));
            data.Add(new Snoop.Data.Int("Sheet dimensional weight", plotInfoValidator.SheetDimensionalWeight));
            data.Add(new Snoop.Data.Int("Sheet media group weight", plotInfoValidator.SheetMediaGroupWeight));    
        }

        private void
        Stream(ArrayList data, PlotProgress plotProgress)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotProgress)));

            data.Add(new Snoop.Data.Bool("Is plot cancelled", plotProgress.IsPlotCancelled));
            data.Add(new Snoop.Data.Bool("Is sheet cancelled", plotProgress.IsSheetCancelled));
            data.Add(new Snoop.Data.Bool("Is visible", plotProgress.IsVisible));
            data.Add(new Snoop.Data.Int("Lower plot progress range", plotProgress.LowerPlotProgressRange));
            data.Add(new Snoop.Data.Int("Lower sheet progress range", plotProgress.LowerSheetProgressRange));
            data.Add(new Snoop.Data.String("Plot cancel status", plotProgress.PlotCancelStatus.ToString()));
            data.Add(new Snoop.Data.Int("Plot progress pos", plotProgress.PlotProgressPos));
            data.Add(new Snoop.Data.String("Sheet cancel status", plotProgress.SheetCancelStatus.ToString()));
            data.Add(new Snoop.Data.Int("Sheet progress pos", plotProgress.SheetProgressPos));
            data.Add(new Snoop.Data.String("Status msg string", plotProgress.StatusMsgString));
            data.Add(new Snoop.Data.Int("Upper plot progress range", plotProgress.UpperPlotProgressRange));
            data.Add(new Snoop.Data.Int("Upper sheet progress range", plotProgress.UpperSheetProgressRange));

            PlotProgressDialog plotProgDlg = plotProgress as PlotProgressDialog;
            if (plotProgDlg != null) {
                Stream(data, plotProgDlg);
                return;
            }
        }

        private void
        Stream(ArrayList data, PlotProgressDialog plotProgDlg)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PlotProgressDialog)));

            data.Add(new Snoop.Data.Bool("Is single sheet plot", plotProgDlg.IsSingleSheetPlot));           
        }

        private void
        Stream(ArrayList data, MediaBounds mediaBounds)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MediaBounds)));

            data.Add(new Snoop.Data.Point2d("Lower left printable area", mediaBounds.LowerLeftPrintableArea));
            data.Add(new Snoop.Data.Point2d("Page size", mediaBounds.PageSize));
            data.Add(new Snoop.Data.Point2d("Upper right printable area", mediaBounds.UpperRightPrintableArea));           
        }
    }
}
