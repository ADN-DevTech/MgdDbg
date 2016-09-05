
// $Header: //Desktop/Source/TestTools/MgdDbg/Snoop/CollectorExts/GraphicsSystem.cs#1 $
// $Author: fergusja $ $DateTime: 2007/03/16 08:08:08 $ $Change: 68854 $
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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using MgdDbg.Snoop.Collectors;

using Autodesk.AutoCAD.GraphicsSystem;

namespace MgdDbg.Snoop.CollectorExts {
    public class GraphicsSystem : CollectorExt {

		public
		GraphicsSystem()
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
            Manager mgr = e.ObjToSnoop as Manager;
            if (mgr != null) {
                Stream(snoopCollector.Data(), mgr);
                return;
            }

            ViewEventArgs args = e.ObjToSnoop as ViewEventArgs;
            if (args != null) {
                Stream(snoopCollector.Data(), args);
                return;
            }

            View view = e.ObjToSnoop as View;
            if (view != null) {
                Stream(snoopCollector.Data(), view);
                return;
            }

            Configuration config = e.ObjToSnoop as Configuration;
            if (config != null) {
                Stream(snoopCollector.Data(), config);
                return;
            }

            Device device = e.ObjToSnoop as Device;
            if (device != null) {
                Stream(snoopCollector.Data(), device);
                return;
            }

            Model model = e.ObjToSnoop as Model;
            if (model != null) {
                Stream(snoopCollector.Data(), model);
                return;
            }

            Node node = e.ObjToSnoop as Node;
            if (node != null) {
                Stream(snoopCollector.Data(), node);
                return;
            }

            // ValueTypes we have to treat a bit different
            if (e.ObjToSnoop is ClientViewInfo){
                Stream(snoopCollector.Data(), (ClientViewInfo)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is DriverInfo) {
                Stream(snoopCollector.Data(), (DriverInfo)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is StereoParameters) {
                Stream(snoopCollector.Data(), (StereoParameters)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is ViewportBorderProperties) {
                Stream(snoopCollector.Data(), (ViewportBorderProperties)e.ObjToSnoop);
                return;
            }
        }

        private void
        Stream (ArrayList data, Manager mgr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Manager)));

#if AC2012
            data.Add(new Snoop.Data.ObjectUnknown("Display Size", mgr.DisplaySize));
#else
            data.Add(new Snoop.Data.ObjectUnknown("Display Size", mgr.DeviceIndependentDisplaySize));
#endif

            KernelDescriptor descriptor = new KernelDescriptor();

            descriptor.addRequirement(Autodesk.AutoCAD.UniqueString.Intern("3D Drawing"));

            GraphicsKernel kernal = Manager.AcquireGraphicsKernel(descriptor);

            data.Add(new Snoop.Data.Object("Get DB Model", mgr.GetDBModel(kernal)));
            data.Add(new Snoop.Data.Object("Get GUI Device", mgr.GetGUIDevice(kernal)));
        }

        private void
        Stream(ArrayList data, ViewEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ViewEventArgs)));
            
            data.Add(new Snoop.Data.Object("View", args.View));
        }

        private void
        Stream(ArrayList data, View view)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(View)));
            
            data.Add(new Snoop.Data.Double("Back clip", view.BackClip));
            data.Add(new Snoop.Data.Double("Front clip", view.FrontClip));
            data.Add(new Snoop.Data.ObjectId("Background ID", view.BackgroundId));
            data.Add(new Snoop.Data.Object("Device", view.Device));
            data.Add(new Snoop.Data.Bool("Enable back clip", view.EnableBackClip));
            data.Add(new Snoop.Data.Bool("Enable front clip", view.EnableFrontClip));
            data.Add(new Snoop.Data.Bool("Enable stereo", view.EnableStereo));
            data.Add(new Snoop.Data.Bool("Exceeded bounds", view.ExceededBounds));
            data.Add(new Snoop.Data.Double("Field width", view.FieldWidth));
            data.Add(new Snoop.Data.Double("Field height", view.FieldHeight));
            data.Add(new Snoop.Data.Bool("Is perspective", view.IsPerspective));
            data.Add(new Snoop.Data.Bool("Is valid", view.IsValid));
            data.Add(new Snoop.Data.Bool("Is visible", view.IsVisible));
            //data.Add(new Snoop.Data.String("Mode", view.Mode.ToString()));
            data.Add(new Snoop.Data.Object("World to device matrix", view.WorldToDeviceMatrix));
            data.Add(new Snoop.Data.Object("Object to device matrix", view.ObjectToDeviceMatrix));
            data.Add(new Snoop.Data.Object("Projection matrix", view.ProjectionMatrix));
            data.Add(new Snoop.Data.Object("Screen matrix", view.ScreenMatrix));
            data.Add(new Snoop.Data.Object("Viewing matrix", view.ViewingMatrix));
            data.Add(new Snoop.Data.Point3d("Position", view.Position));
            data.Add(new Snoop.Data.Point3d("Target", view.Target));
            data.Add(new Snoop.Data.Vector3d("Up vector", view.UpVector));
            data.Add(new Snoop.Data.Object("Stereo parameters", view.StereoParameters));
#if AC2012
            data.Add(new Snoop.Data.Object("Viewport Extents", view.ViewportD));
            //data.Add(new Snoop.Data.Object("Viewport D", view.ViewportD));
#else
            data.Add(new Snoop.Data.Object("Viewport Extents", view.ViewportExtents));
#endif
            data.Add(new Snoop.Data.Object("Viewport border properties", view.ViewportBorderProperties));
            data.Add(new Snoop.Data.Bool("Viewport border visibility", view.ViewportBorderVisibility));
            data.Add(new Snoop.Data.Object("Viewport style", view.VisualStyle));
            data.Add(new Snoop.Data.ObjectId("Viewport style ID", view.VisualStyleId));
        }

        private void
        Stream (ArrayList data, Configuration config)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Configuration)));

            data.Add(new Snoop.Data.Bool("Adaptive Degradation", config.AdaptiveDegradation));
            //data.Add(new Snoop.Data.Bool("Cache Viewport Draw Geometry", config.CacheViewportDrawGeometry));  // TBD: Fix JMA
            data.Add(new Snoop.Data.Object("Current Display Driver", config.CurrentDisplayDriver));
            data.Add(new Snoop.Data.Int("Curve Tessellation Tolerance", config.CurveTessellationTolerance));
            data.Add(new Snoop.Data.Object("Default Hardware Accelerated Driver", config.DefaultHardwareAcceleratedDriver));
            data.Add(new Snoop.Data.Bool("Discard Back Faces", config.DiscardBackFaces));
            data.Add(new Snoop.Data.String("Driver Name", config.DriverName));
            data.Add(new Snoop.Data.Int("Driver Revision", config.DriverRevision));
            data.Add(new Snoop.Data.String("Driver Search Path", config.DriverSearchPath));
            data.Add(new Snoop.Data.Int("Driver Version", config.DriverVersion));
            data.Add(new Snoop.Data.Bool("Dynamic Tessellation", config.DynamicTessellation));
            data.Add(new Snoop.Data.Int("Frame Rate", config.FrameRate));
            data.Add(new Snoop.Data.Object("Handedness", config.Handedness));
            data.Add(new Snoop.Data.Object("Hardware Accelerated Driver", config.HardwareAcceleratedDriver));
            data.Add(new Snoop.Data.Int("Max LODs", config.MaxLODs));
            data.Add(new Snoop.Data.Bool("Redraw On Window Expose", config.RedrawOnWindowExpose));
            data.Add(new Snoop.Data.Int("Surface Tessellation Tolerance", config.SurfaceTessellationTolerance));
            data.Add(new Snoop.Data.Object("Transparency", config.Transparency));
           // data.Add(new Snoop.Data.Bool("Use Display Lists", config.UseDisplayLists));
            //data.Add(new Snoop.Data.Bool("Use Half Pixel Deviation", config.UseHalfPixelDeviation));  // TBD: Fix JMA

        }

        private void
        Stream (ArrayList data, Device device)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Device)));

            data.Add(new Snoop.Data.ObjectUnknown("Background Color", device.BackgroundColor));
            data.Add(new Snoop.Data.String("Device Render Type", device.DeviceRenderType.ToString()));
            data.Add(new Snoop.Data.Bool("Is Valid", device.IsValid));
        }

        private void
        Stream (ArrayList data, Model model)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Model)));

            data.Add(new Snoop.Data.ObjectId("Background Id", model.BackgroundId));
            data.Add(new Snoop.Data.Bool("Enable Sectioning", model.EnableSectioning));
            data.Add(new Snoop.Data.Bool("Linetypes Enabled", model.LinetypesEnabled));
            data.Add(new Snoop.Data.String("Render Type", model.RenderType.ToString()));
            data.Add(new Snoop.Data.Double("Shadow Plane Location", model.ShadowPlaneLocation));
            data.Add(new Snoop.Data.Object("Transform", model.Transform));
            data.Add(new Snoop.Data.Object("Visual Style", model.VisualStyle));
            data.Add(new Snoop.Data.ObjectId("Visual Style Id", model.VisualStyleId));
        }

        private void
        Stream (ArrayList data, Node node)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Node)));
            // no data at this level
        }

        private void
        Stream (ArrayList data, ClientViewInfo clientViewInfo)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ClientViewInfo)));

            data.Add(new Snoop.Data.Int("Acad Window Id", clientViewInfo.AcadWindowId));
            data.Add(new Snoop.Data.Int("Viewport Id", clientViewInfo.ViewportId));
            data.Add(new Snoop.Data.Int("Viewport Object Id", clientViewInfo.ViewportObjectId));
        }

        private void
        Stream (ArrayList data, DriverInfo driverInfo)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DriverInfo)));

            data.Add(new Snoop.Data.String("Driver", driverInfo.Driver));
            data.Add(new Snoop.Data.Bool("Hardware Accelerated", driverInfo.HardwareAccelerated));
            data.Add(new Snoop.Data.String("Path", driverInfo.Path));
        }

        private void
        Stream (ArrayList data, StereoParameters stereoParams)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(StereoParameters)));

            data.Add(new Snoop.Data.Double("Magnitude", stereoParams.Magnitude));
            data.Add(new Snoop.Data.Double("Parallax", stereoParams.Parallax));
        }

        private void
        Stream (ArrayList data, ViewportBorderProperties vPortBorderProps)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ViewportBorderProperties)));

            data.Add(new Snoop.Data.Object("Color", vPortBorderProps.Color));
            data.Add(new Snoop.Data.Int("Weight", vPortBorderProps.Weight));
        }
    }
}
