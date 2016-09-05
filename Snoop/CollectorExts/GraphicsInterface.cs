
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
using System.Diagnostics;

using Autodesk.AutoCAD.GraphicsInterface;
using MgdDbg.Snoop.Collectors;

namespace MgdDbg.Snoop.CollectorExts
{
	/// <summary>
	/// Summary description for GraphicsInterface.
	/// </summary>
	public class GraphicsInterface : CollectorExt
	{
		public
		GraphicsInterface()
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
                
                // TBD: this one just gets in the way of normal entity display
            //Drawable drawable = e.ObjToSnoop as Drawable;
            //if (drawable != null)            
            //    Stream(snoopCollector.Data(), drawable);

            TextStyle txtStyle = e.ObjToSnoop as TextStyle;
            if (txtStyle != null)
                Stream(snoopCollector.Data(), txtStyle);

            // TBD: "twilight zone" weird matMap throws a nullReferenceException
            // even as it is being checked for null !!!
            //MaterialMap matMap = e.ObjToSnoop as MaterialMap;
            //if (matMap != null) {
            //    Stream(snoopCollector.Data(), matMap);
            //    return;
            //}

            CommonDraw comDraw = e.ObjToSnoop as CommonDraw;
            if (comDraw != null) {
                Stream(snoopCollector.Data(), comDraw);
                return;
            }

            SubEntityTraits subEntTraits = e.ObjToSnoop as SubEntityTraits;
            if (subEntTraits != null) {
                Stream(snoopCollector.Data(), subEntTraits);
                return;
            }

            LightAttenuation lightAtt = e.ObjToSnoop as LightAttenuation;
            if (lightAtt != null) {
                Stream(snoopCollector.Data(), lightAtt);
                return;
            }

            /*DisplayStyle dispStyl = e.ObjToSnoop as DisplayStyle;
            if (dispStyl != null) {
                Stream(snoopCollector.Data(), dispStyl);
                return;
            }*/ // TBD: Fix JMA

            ShadowParameters shadParams = e.ObjToSnoop as ShadowParameters;
            if (shadParams != null) {
                Stream(snoopCollector.Data(), shadParams);
                return;
            }

            //ShadowParameters2 shadParams2 = e.ObjToSnoop as ShadowParameters2;
            //if (shadParams2 != null) {
            //    Stream(snoopCollector.Data(), shadParams2);
            //    return;
            //}

            MaterialColor matColor = e.ObjToSnoop as MaterialColor;
            if (matColor != null) {          
                Stream(snoopCollector.Data(), matColor);
                return;
            }
            
            Mapper mapper = e.ObjToSnoop as Mapper;
            if (mapper != null) {          
                Stream(snoopCollector.Data(), mapper);
                return;
            }

            MaterialTexture matTxtr = e.ObjToSnoop as MaterialTexture;
            if (matTxtr != null) {
                Stream(snoopCollector.Data(), matTxtr);
                return;
            }

            Autodesk.AutoCAD.GraphicsInterface.Geometry geo = e.ObjToSnoop as Autodesk.AutoCAD.GraphicsInterface.Geometry;
            if (geo != null) {
                Stream(snoopCollector.Data(), geo);
                return;
            }

            Viewport vPort = e.ObjToSnoop as Viewport;
            if (vPort != null) {
                Stream(snoopCollector.Data(), vPort);
                return;
            }

            Context ctxt = e.ObjToSnoop as Context;
            if (ctxt != null) {
                Stream(snoopCollector.Data(), ctxt);
                return;
            }

            /*FaceStyle faceStyl = e.ObjToSnoop as FaceStyle;
            if (faceStyl != null) {
                Stream(snoopCollector.Data(), faceStyl);
                return;
            }*/ // TBD: Fix JMA

            /*EdgeStyle edgStyl = e.ObjToSnoop as EdgeStyle;
            if (edgStyl != null) {
                Stream(snoopCollector.Data(), edgStyl);
                return;
            }*/ // TBD: Fix JMA

            VisualStyle visStyl = e.ObjToSnoop as VisualStyle;
            if (visStyl != null) {
                Stream(snoopCollector.Data(), visStyl);
                return;
            }

            ClipBoundary clipBdry = e.ObjToSnoop as ClipBoundary;
            if (clipBdry != null) {
                Stream(snoopCollector.Data(), clipBdry);
                return;
            }

            EdgeData edgData = e.ObjToSnoop as EdgeData;
            if (edgData != null) {
                Stream(snoopCollector.Data(), edgData);
                return;
            }

            FaceData faceData = e.ObjToSnoop as FaceData;
            if (faceData != null) {
                Stream(snoopCollector.Data(), faceData);
                return;
            }

            VertexData vertData = e.ObjToSnoop as VertexData;
            if (vertData != null) {
                Stream(snoopCollector.Data(), vertData);
                return;
            }

            ContextualColors cntxtColors = e.ObjToSnoop as ContextualColors;
            if (cntxtColors != null) {
                Stream(snoopCollector.Data(), cntxtColors);
                return;
            }

            SkyParameters skyParams = e.ObjToSnoop as SkyParameters;
            if (skyParams != null) {
                Stream(snoopCollector.Data(), skyParams);
                return;
            }

            ToneOperatorParameters toneOperParams = e.ObjToSnoop as ToneOperatorParameters;
            if (toneOperParams != null) {
                Stream(snoopCollector.Data(), toneOperParams);
                return;
            }

            //Variant variant = e.ObjToSnoop as Variant;
            //if (variant != null) {
            //    Stream(snoopCollector.Data(), variant);
            //    return;
            //}

                // these are value types so we have to treat them a bit different
            if (e.ObjToSnoop is FontDescriptor) {
                Stream(snoopCollector.Data(), (FontDescriptor)e.ObjToSnoop);
                return; 
            }

           /* if (e.ObjToSnoop is EdgeStyleOverride) {
                Stream(snoopCollector.Data(), (EdgeStyleOverride)e.ObjToSnoop);
                return;
            }*/ // TBD: Fix JMA

            if (e.ObjToSnoop is MaterialDiffuseComponent) {
                Stream(snoopCollector.Data(), (MaterialDiffuseComponent)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MaterialSpecularComponent) {
                Stream(snoopCollector.Data(), (MaterialSpecularComponent)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MaterialOpacityComponent) {
                Stream(snoopCollector.Data(), (MaterialOpacityComponent)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MaterialRefractionComponent) {
                Stream(snoopCollector.Data(), (MaterialRefractionComponent)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MaterialRefractionComponent) {
                Stream(snoopCollector.Data(), (MaterialRefractionComponent)e.ObjToSnoop);
                return;
            }

           /* if (e.ObjToSnoop is MentalRayRenderSettingsTraitsIntegerRangeParameter) {
                Stream(snoopCollector.Data(), (MentalRayRenderSettingsTraitsIntegerRangeParameter)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MentalRayRenderSettingsTraitsTraceParameter) {
                Stream(snoopCollector.Data(), (MentalRayRenderSettingsTraitsTraceParameter)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MentalRayRenderSettingsTraitsFloatParameter) {
                Stream(snoopCollector.Data(), (MentalRayRenderSettingsTraitsFloatParameter)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MentalRayRenderSettingsTraitsDoubleRangeParameter) {
                Stream(snoopCollector.Data(), (MentalRayRenderSettingsTraitsDoubleRangeParameter)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MentalRayRenderSettingsTraitsSamplingParameter) {
                Stream(snoopCollector.Data(), (MentalRayRenderSettingsTraitsSamplingParameter)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MentalRayRenderSettingsTraitsBoolParameter) {
                Stream(snoopCollector.Data(), (MentalRayRenderSettingsTraitsBoolParameter)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MentalRayRenderSettingsTraitsDiagnosticGridModeParameter) {
                Stream(snoopCollector.Data(), (MentalRayRenderSettingsTraitsDiagnosticGridModeParameter)e.ObjToSnoop);
                return;
            }*/

            if (e.ObjToSnoop is FrontAndBackClipping) {
                Stream(snoopCollector.Data(), (FrontAndBackClipping)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is ColorRGB) {
                Stream(snoopCollector.Data(), (ColorRGB)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is ColorRGBA) {
                Stream(snoopCollector.Data(), (ColorRGBA)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is MaterialNormalMapComponent) {
                Stream(snoopCollector.Data(), (MaterialNormalMapComponent)e.ObjToSnoop);
                return;
            }
        }
        

        private void
        Stream(ArrayList data, Drawable drawable)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.GraphicsInterface.Drawable)));
            
            data.Add(new Snoop.Data.ObjectId("ID", drawable.Id));
            data.Add(new Snoop.Data.Bool("Is persistent", drawable.IsPersistent));
            data.Add(new Snoop.Data.String("Drawable Type", drawable.DrawableType.ToString()));

            Glyph glyph = drawable as Glyph;
            if (glyph != null) {
                Stream(data, glyph);
                return;
            }
        }

        private void
        Stream(ArrayList data, TextStyle txtStyle)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(TextStyle)));

            data.Add(new Snoop.Data.Bool("Backward", txtStyle.Backward));
            data.Add(new Snoop.Data.String("Big font file name", txtStyle.BigFontFileName));
            data.Add(new Snoop.Data.String("File name", txtStyle.FileName));
            data.Add(new Snoop.Data.Object("Font", txtStyle.Font));
            //data.Add(new Snoop.Data.Byte("Font", txtStyle.LoadStyleRec));
            data.Add(new Snoop.Data.Angle("Obliquing angle", txtStyle.ObliquingAngle));
            data.Add(new Snoop.Data.Bool("Overlined", txtStyle.Overlined));
            data.Add(new Snoop.Data.Bool("PreLoaded", txtStyle.PreLoaded));
            data.Add(new Snoop.Data.String("Style name", txtStyle.StyleName));
            data.Add(new Snoop.Data.Distance("Text size", txtStyle.TextSize));
            data.Add(new Snoop.Data.Double("Tracking percent", txtStyle.TrackingPercent));
            data.Add(new Snoop.Data.Bool("Underlined", txtStyle.Underlined));
            data.Add(new Snoop.Data.Bool("Upside down", txtStyle.UpsideDown));
            data.Add(new Snoop.Data.Bool("Vertical", txtStyle.Vertical));
            data.Add(new Snoop.Data.Double("X scale", txtStyle.XScale));
        }

        #region CommonDraw
        private void
        Stream(ArrayList data, CommonDraw comDraw)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CommonDraw)));

            data.Add(new Snoop.Data.Object("Context", comDraw.Context));
            data.Add(new Snoop.Data.Bool("Is dragging", comDraw.IsDragging));
            data.Add(new Snoop.Data.Int("Number of isolines", comDraw.NumberOfIsolines));
            data.Add(new Snoop.Data.Object("Raw geometry", comDraw.RawGeometry));
            data.Add(new Snoop.Data.Bool("Regen abort", comDraw.RegenAbort));
            data.Add(new Snoop.Data.String("Regen type", comDraw.RegenType.ToString()));
            data.Add(new Snoop.Data.Object("SubEntityTraits", comDraw.SubEntityTraits));

            WorldDraw worldDraw = comDraw as WorldDraw;
            if (worldDraw != null) {
                Stream(data, worldDraw);
                return;
            }

            ViewportDraw viewPrtDrw = comDraw as ViewportDraw;
            if (viewPrtDrw != null) {
                Stream(data, viewPrtDrw);
                return;
            }
        }

        private void
        Stream(ArrayList data, WorldDraw worldDraw)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(WorldDraw)));

            data.Add(new Snoop.Data.Object("Geometry", worldDraw.Geometry));
        }

        private void
        Stream(ArrayList data, ViewportDraw viewPrtDrw)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ViewportDraw)));

            data.Add(new Snoop.Data.Object("Geometry", viewPrtDrw.Geometry));
            data.Add(new Snoop.Data.Int("Sequence number", viewPrtDrw.SequenceNumber));
            data.Add(new Snoop.Data.Object("Viewport", viewPrtDrw.Viewport));
        }

        #endregion

        #region SubEntityTraits
        private void
        Stream(ArrayList data, SubEntityTraits subEntTraits)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SubEntityTraits)));

            data.Add(new Snoop.Data.Int("Color", subEntTraits.Color));
            data.Add(new Snoop.Data.Int("Draw flags", subEntTraits.DrawFlags));
            //data.Add(new Snoop.Data.Object("Edge style override", subEntTraits.EdgeStyleOverride));       // TBD: Fix JMA
            data.Add(new Snoop.Data.String("Fill type", subEntTraits.FillType.ToString()));
            data.Add(new Snoop.Data.ObjectId("Layer", subEntTraits.Layer));
            data.Add(new Snoop.Data.ObjectId("Line type", subEntTraits.LineType));
            data.Add(new Snoop.Data.Double("Line type scale", subEntTraits.LineTypeScale));
            data.Add(new Snoop.Data.String("Line weight", subEntTraits.LineWeight.ToString()));
            data.Add(new Snoop.Data.Object("Mapper", subEntTraits.Mapper));
            data.Add(new Snoop.Data.ObjectId("Material", subEntTraits.Material));
            data.Add(new Snoop.Data.Object("Plot style descriptor", subEntTraits.PlotStyleDescriptor));
            data.Add(new Snoop.Data.Bool("Sectionable", subEntTraits.Sectionable));
            data.Add(new Snoop.Data.Bool("Selection only geometry", subEntTraits.SelectionOnlyGeometry));
            data.Add(new Snoop.Data.String("Shadow flags", subEntTraits.ShadowFlags.ToString()));
            data.Add(new Snoop.Data.Double("Thickness", subEntTraits.Thickness));
            data.Add(new Snoop.Data.Object("True color", subEntTraits.TrueColor));
            data.Add(new Snoop.Data.ObjectId("Visual style", subEntTraits.VisualStyle));

            DrawableTraits drwblTrts = subEntTraits as DrawableTraits;
            if (drwblTrts != null) {
                Stream(data, drwblTrts);
                return;
            }
        }

        private void
        Stream(ArrayList data, DrawableTraits drwblTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DrawableTraits)));

            // no extra data here

            NonEntityTraits nonEntTrts = drwblTrts as NonEntityTraits;
            if (nonEntTrts != null) {
                Stream(data, nonEntTrts);
                return;
            }

            ViewportTraits viewPrtTrts = drwblTrts as ViewportTraits;
            if (viewPrtTrts != null) {
                Stream(data, viewPrtTrts);
                return;
            }

            VisualStyleTraits visStylTrts = drwblTrts as VisualStyleTraits;
            if (visStylTrts != null) {
                Stream(data, visStylTrts);
                return;
            }
        }

        #region NonEntityTraits
        private void
        Stream(ArrayList data, NonEntityTraits nonEntTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(NonEntityTraits)));

            data.Add(new Snoop.Data.Int("Color", nonEntTrts.Color));
            data.Add(new Snoop.Data.Int("Draw flags", nonEntTrts.DrawFlags));
            //data.Add(new Snoop.Data.Object("Edge style override", nonEntTrts.EdgeStyleOverride));     // TBD: Fix JMA
            data.Add(new Snoop.Data.String("Fill type", nonEntTrts.FillType.ToString()));
            data.Add(new Snoop.Data.ObjectId("Layer", nonEntTrts.Layer));
            data.Add(new Snoop.Data.ObjectId("Line type", nonEntTrts.LineType));
            data.Add(new Snoop.Data.Double("Line type scale", nonEntTrts.LineTypeScale));
            data.Add(new Snoop.Data.String("Line weight", nonEntTrts.LineWeight.ToString()));
            data.Add(new Snoop.Data.Object("Mapper", nonEntTrts.Mapper));
            data.Add(new Snoop.Data.ObjectId("Material", nonEntTrts.Material));
            data.Add(new Snoop.Data.Object("Plot style descriptor", nonEntTrts.PlotStyleDescriptor));
            data.Add(new Snoop.Data.Bool("Sectionable", nonEntTrts.Sectionable));
            data.Add(new Snoop.Data.Bool("Selection only geometry", nonEntTrts.SelectionOnlyGeometry));
            data.Add(new Snoop.Data.String("Shadow flags", nonEntTrts.ShadowFlags.ToString()));
            data.Add(new Snoop.Data.Distance("Thickness", nonEntTrts.Thickness));
            data.Add(new Snoop.Data.Object("True color", nonEntTrts.TrueColor));
            data.Add(new Snoop.Data.ObjectId("Visual style", nonEntTrts.VisualStyle));

            LightTraits lightTrts = nonEntTrts as LightTraits;
            if (nonEntTrts != null) {
                Stream(data, nonEntTrts);
                return;
            }

            GradientBackgroundTraits grdntBkGrndTrts = nonEntTrts as GradientBackgroundTraits;
            if (grdntBkGrndTrts != null) {
                Stream(data, grdntBkGrndTrts);
                return;
            }

            GroundPlaneBackgroundTraits grndPlBkGrndTrts = nonEntTrts as GroundPlaneBackgroundTraits;
            if (grndPlBkGrndTrts != null) {
                Stream(data, grndPlBkGrndTrts);
                return;
            }

            ImageBackgroundTraits imgBkGrndTrts = nonEntTrts as ImageBackgroundTraits;
            if (imgBkGrndTrts != null) {
                Stream(data, imgBkGrndTrts);
                return;
            }

            MaterialTraits matTrts = nonEntTrts as MaterialTraits;
            if (matTrts != null) {
                Stream(data, matTrts);
                return;
            }

            //MaterialTraits2 matTrts2 = nonEntTrts as MaterialTraits2;
            //if (matTrts2 != null) {
            //    Stream(data, matTrts2);
            //    return;
            //}

            RenderSettingsTraits rndrSetTrts = nonEntTrts as RenderSettingsTraits;
            if (rndrSetTrts != null) {
                Stream(data, rndrSetTrts);
                return;
            }

            RenderEnvironmentTraits rndrEnvTrts = nonEntTrts as RenderEnvironmentTraits;
            if (rndrEnvTrts != null) {
                Stream(data, rndrEnvTrts);
                return;
            }

            SolidBackgroundTraits solBkGrndTrts = nonEntTrts as SolidBackgroundTraits;
            if (solBkGrndTrts != null) {
                Stream(data, solBkGrndTrts);
                return;
            }

            SkyBackgroundTraits skyBkGrndTrts = nonEntTrts as SkyBackgroundTraits;
            if (skyBkGrndTrts != null) {
                Stream(data, skyBkGrndTrts);
                return;
            }
        }

        #region LightTraits
        private void
        Stream(ArrayList data, LightTraits lightTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LightTraits)));

            data.Add(new Snoop.Data.Bool("On", lightTrts.On));

            StandardLightTraits stdLightTrts = lightTrts as StandardLightTraits;
            if (stdLightTrts != null) {
                Stream(data, stdLightTrts);
                return;
            }
        }

        private void
        Stream(ArrayList data, StandardLightTraits stdLightTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(StandardLightTraits)));

            data.Add(new Snoop.Data.Double("Intensity", stdLightTrts.Intensity));
            data.Add(new Snoop.Data.Object("Light color", stdLightTrts.LightColor));
            data.Add(new Snoop.Data.Object("Shadow", stdLightTrts.Shadow));

            DistantLightTraits distLightTrts = stdLightTrts as DistantLightTraits;
            if (distLightTrts != null) {
                Stream(data, distLightTrts);
                return;
            }

            //DistantLightTraits2 distLightTrts2 = stdLightTrts as DistantLightTraits2;
            //if (distLightTrts2 != null) {
            //    Stream(data, distLightTrts2);
            //    return;
            //}

            PointLightTraits ptLightTrts = stdLightTrts as PointLightTraits;
            if (ptLightTrts != null) {
                Stream(data, ptLightTrts);
                return;
            }

            //PointLightTraits2 ptLightTrts2 = stdLightTrts as PointLightTraits2;
            //if (ptLightTrts2 != null) {
            //    Stream(data, ptLightTrts2);
            //    return;
            //}

            SpotLightTraits spotLightTrts = stdLightTrts as SpotLightTraits;
            if (spotLightTrts != null) {
                Stream(data, spotLightTrts);
                return;
            }

            //SpotLightTraits2 spotLightTrts2 = stdLightTrts as SpotLightTraits2;
            //if (spotLightTrts2 != null) {
            //    Stream(data, spotLightTrts2);
            //    return;
            //}
        }

        private void
        Stream(ArrayList data, DistantLightTraits distLightTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DistantLightTraits)));

            data.Add(new Snoop.Data.Bool("Is sunlight", distLightTrts.IsSunlight));
            data.Add(new Snoop.Data.Vector3d("Light direction", distLightTrts.LightDirection));

            //data.Add(new Snoop.Data.ClassSeparator(typeof(DistantLightTraits2)));

            data.Add(new Snoop.Data.Object("Lamp color", distLightTrts.LampColor));
            data.Add(new Snoop.Data.Double("Physical intensity", distLightTrts.PhysicalIntensity));
            data.Add(new Snoop.Data.Object("Shadow parameters", distLightTrts.Shadow));
            data.Add(new Snoop.Data.Object("Sky parameters", distLightTrts.SkyParameters));
        }

        //private void
        //Stream(ArrayList data, DistantLightTraits distLightTrts)
        //{
        //    data.Add(new Snoop.Data.ClassSeparator(typeof(DistantLightTraits2)));

        //    data.Add(new Snoop.Data.Object("Lamp color", distLightTrts2.LampColor));
        //    data.Add(new Snoop.Data.Double("Physical intensity", distLightTrts2.PhysicalIntensity));
        //    data.Add(new Snoop.Data.Object("Shadow parameters", distLightTrts2.ShadowParameters));
        //    data.Add(new Snoop.Data.Object("Sky parameters", distLightTrts2.SkyParameters));
        //}

        private void
        Stream(ArrayList data, PointLightTraits ptLightTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PointLightTraits)));

            data.Add(new Snoop.Data.Object("Attenuation", ptLightTrts.Attenuation));
            data.Add(new Snoop.Data.Point3d("Position", ptLightTrts.Position));

            data.Add(new Snoop.Data.Bool("Has target", ptLightTrts.HasTarget));
            data.Add(new Snoop.Data.Object("Lamp color", ptLightTrts.LampColor));
            data.Add(new Snoop.Data.Double("Physical intensity", ptLightTrts.PhysicalIntensity));
            data.Add(new Snoop.Data.Point3d("Target location", ptLightTrts.TargetLocation));

            WebLightTraits webLightTraits = ptLightTrts as WebLightTraits;
            if (webLightTraits != null)
            {
                Stream(data, webLightTraits);
                return;
            }
        }

        //private void
        //Stream(ArrayList data, PointLightTraits2 ptLightTrts2)
        //{
        //    data.Add(new Snoop.Data.ClassSeparator(typeof(PointLightTraits2)));

        //    data.Add(new Snoop.Data.Bool("Has target", ptLightTrts2.HasTarget));
        //    data.Add(new Snoop.Data.Object("Lamp color", ptLightTrts2.LampColor));
        //    data.Add(new Snoop.Data.Double("Physical intensity", ptLightTrts2.PhysicalIntensity));
        //    data.Add(new Snoop.Data.Point3d("Target location", ptLightTrts2.TargetLocation));

        //    WebLightTraits webLightTraits = ptLightTrts2 as WebLightTraits;
        //    if (webLightTraits != null) {
        //        Stream(data, webLightTraits);
        //        return;
        //    }
        //}

        private void
        Stream(ArrayList data, WebLightTraits webLightTraits)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(WebLightTraits)));

            data.Add(new Snoop.Data.String("Web file", webLightTraits.WebFile));
            data.Add(new Snoop.Data.String("Web file type", webLightTraits.WebFileType.ToString()));
            data.Add(new Snoop.Data.Double("Web flux", webLightTraits.WebFlux));
            data.Add(new Snoop.Data.Bool("Web horz ang 90to270", webLightTraits.WebHorzAng90to270));
            data.Add(new Snoop.Data.Vector3d("Web rotation", webLightTraits.WebRotation));
            data.Add(new Snoop.Data.String("Web symmetry", webLightTraits.WebSymmetry.ToString()));
        }

        private void
        Stream(ArrayList data, SpotLightTraits spotLightTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SpotLightTraits)));

            data.Add(new Snoop.Data.Object("Attenuation", spotLightTrts.Attenuation));
            data.Add(new Snoop.Data.Double("Fall off", spotLightTrts.Falloff));
            data.Add(new Snoop.Data.Double("Hotspot", spotLightTrts.Hotspot));
            data.Add(new Snoop.Data.Point3d("Position", spotLightTrts.Position));
            data.Add(new Snoop.Data.Point3d("Target location", spotLightTrts.TargetLocation));

            data.Add(new Snoop.Data.Object("Attenuation", spotLightTrts.LampColor));
            data.Add(new Snoop.Data.Double("Fall off", spotLightTrts.PhysicalIntensity));
            data.Add(new Snoop.Data.Object("Hotspot", spotLightTrts.Shadow));     
        }

        //private void
        //Stream(ArrayList data, SpotLightTraits2 spotLightTrts2)
        //{
        //    data.Add(new Snoop.Data.ClassSeparator(typeof(SpotLightTraits2)));

        //    data.Add(new Snoop.Data.Object("Attenuation", spotLightTrts2.LampColor));
        //    data.Add(new Snoop.Data.Double("Fall off", spotLightTrts2.PhysicalIntensity));
        //    data.Add(new Snoop.Data.Object("Hotspot", spotLightTrts2.ShadowParameters));           
        //}

        #endregion

        private void
        Stream(ArrayList data, GradientBackgroundTraits grdntBkGrndTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(GradientBackgroundTraits)));

            data.Add(new Snoop.Data.Object("Color bottom", grdntBkGrndTrts.ColorBottom));
            data.Add(new Snoop.Data.Object("Color middle", grdntBkGrndTrts.ColorMiddle));
            data.Add(new Snoop.Data.Object("Color top", grdntBkGrndTrts.ColorTop));
            data.Add(new Snoop.Data.Distance("Height", grdntBkGrndTrts.Height));
            data.Add(new Snoop.Data.Distance("Horizon", grdntBkGrndTrts.Horizon));
            data.Add(new Snoop.Data.Angle("Rotation", grdntBkGrndTrts.Rotation));
        }

        private void
        Stream(ArrayList data, GroundPlaneBackgroundTraits grndPlBkGrndTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(GroundPlaneBackgroundTraits)));

            data.Add(new Snoop.Data.Object("Color ground plane far", grndPlBkGrndTrts.ColorGroundPlaneFar));
            data.Add(new Snoop.Data.Object("Color ground plane near", grndPlBkGrndTrts.ColorGroundPlaneNear));
            data.Add(new Snoop.Data.Object("Color sky horizon", grndPlBkGrndTrts.ColorSkyHorizon));
            data.Add(new Snoop.Data.Object("Color sky zenith", grndPlBkGrndTrts.ColorSkyZenith));
            data.Add(new Snoop.Data.Object("Color underground azimuth", grndPlBkGrndTrts.ColorUndergroundAzimuth));
            data.Add(new Snoop.Data.Object("Color underground horizon", grndPlBkGrndTrts.ColorUndergroundHorizon));
        }

        private void
        Stream(ArrayList data, ImageBackgroundTraits imgBkGrndTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ImageBackgroundTraits)));

            data.Add(new Snoop.Data.Bool("Fit to screen", imgBkGrndTrts.FitToScreen));
            data.Add(new Snoop.Data.String("Image filename", imgBkGrndTrts.ImageFilename));
            data.Add(new Snoop.Data.Bool("Maintain aspect ratio", imgBkGrndTrts.MaintainAspectRatio));
            data.Add(new Snoop.Data.Bool("Use tiling", imgBkGrndTrts.UseTiling));
            data.Add(new Snoop.Data.Distance("X offset", imgBkGrndTrts.XOffset));
            data.Add(new Snoop.Data.Distance("Y offset", imgBkGrndTrts.YOffset));
            data.Add(new Snoop.Data.Double("X scale", imgBkGrndTrts.XScale));
            data.Add(new Snoop.Data.Double("Y scale", imgBkGrndTrts.YScale));
        }

        private void
        Stream(ArrayList data, MaterialTraits matTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MaterialTraits)));

            data.Add(new Snoop.Data.Object("Ambient", matTrts.Ambient));
            data.Add(new Snoop.Data.Object("Bump", matTrts.Bump));
            data.Add(new Snoop.Data.String("Channel flags", matTrts.ChannelFlags.ToString()));
            data.Add(new Snoop.Data.Object("Diffuse", matTrts.Diffuse));
            data.Add(new Snoop.Data.String("Illumination model", matTrts.IlluminationModel.ToString()));
            data.Add(new Snoop.Data.String("Mode", matTrts.Mode.ToString()));
            data.Add(new Snoop.Data.Object("Opacity", matTrts.Opacity));
            data.Add(new Snoop.Data.Object("Reflection", matTrts.Reflection));
            data.Add(new Snoop.Data.Double("Reflectivity", matTrts.Reflectivity));
            data.Add(new Snoop.Data.Object("Refraction", matTrts.Refraction));
            data.Add(new Snoop.Data.Double("Self illumination", matTrts.SelfIllumination));
            data.Add(new Snoop.Data.Object("Specular", matTrts.Specular));
            data.Add(new Snoop.Data.Double("Translucence", matTrts.Translucence));

            data.Add(new Snoop.Data.Double("Color bleed scale", matTrts.ColorBleedScale));
            data.Add(new Snoop.Data.Object("Final gather", matTrts.FinalGather.ToString()));
            data.Add(new Snoop.Data.String("Global illumination", matTrts.GlobalIllumination.ToString()));
            data.Add(new Snoop.Data.Double("Indirect bump scale", matTrts.IndirectBumpScale));
            data.Add(new Snoop.Data.Double("Luminance", matTrts.Luminance));
            data.Add(new Snoop.Data.String("Luminance mode", matTrts.LuminanceMode.ToString()));
            data.Add(new Snoop.Data.Object("Normal map", matTrts.NormalMap));
            data.Add(new Snoop.Data.Double("Reflectance scale", matTrts.ReflectanceScale));
            data.Add(new Snoop.Data.Double("Transmittance scale", matTrts.TransmittanceScale));
            data.Add(new Snoop.Data.Bool("Two sided", matTrts.TwoSided));   
        }

        //private void
        //Stream(ArrayList data, MaterialTraits2 matTrts2)
        //{
        //    data.Add(new Snoop.Data.ClassSeparator(typeof(MaterialTraits2)));

        //    data.Add(new Snoop.Data.Double("Color bleed scale", matTrts2.ColorBleedScale));
        //    data.Add(new Snoop.Data.Object("Final gather", matTrts2.FinalGather.ToString()));
        //    data.Add(new Snoop.Data.String("Global illumination", matTrts2.GlobalIllumination.ToString()));
        //    data.Add(new Snoop.Data.Double("Indirect bump scale", matTrts2.IndirectBumpScale));
        //    data.Add(new Snoop.Data.Double("Luminance", matTrts2.Luminance));
        //    data.Add(new Snoop.Data.String("Luminance mode", matTrts2.LuminanceMode.ToString()));
        //    data.Add(new Snoop.Data.Object("Normal map", matTrts2.NormalMap));
        //    data.Add(new Snoop.Data.Double("Reflectance scale", matTrts2.ReflectanceScale));
        //    data.Add(new Snoop.Data.Double("Transmittance scale", matTrts2.TransmittanceScale));
        //    data.Add(new Snoop.Data.Bool("Two sided", matTrts2.TwoSided));           
        //}

        private void
        Stream(ArrayList data, RenderSettingsTraits rndrSetTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RenderSettingsTraits)));

            data.Add(new Snoop.Data.Bool("Back faces enabled", rndrSetTrts.BackFacesEnabled));
            data.Add(new Snoop.Data.Bool("Diagnostic background enabled", rndrSetTrts.DiagnosticBackgroundEnabled));
            data.Add(new Snoop.Data.Bool("Material enabled", rndrSetTrts.MaterialEnabled));
            data.Add(new Snoop.Data.Double("Model scale factor", rndrSetTrts.ModelScaleFactor));
            data.Add(new Snoop.Data.Bool("Shadows enabled", rndrSetTrts.ShadowsEnabled));
            data.Add(new Snoop.Data.Bool("Texture sampling", rndrSetTrts.TextureSampling));

            //MentalRayRenderSettingsTraits mentalRayRendSetTraits = rndrSetTrts as MentalRayRenderSettingsTraits;
            //if (mentalRayRendSetTraits != null) {
            //    Stream(data, mentalRayRendSetTraits);
            //    return;
            //}

            //MentalRayRenderSettingsTraits2 mentalRayRendSetTraits2 = rndrSetTrts as MentalRayRenderSettingsTraits2;
            //if (mentalRayRendSetTraits2 != null) {
            //    Stream(data, mentalRayRendSetTraits2);
            //    return;
            //}
        }

        /*private void
        Stream(ArrayList data, MentalRayRenderSettingsTraits mentalRayRendSetTraits)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MentalRayRenderSettingsTraits)));

            data.Add(new Snoop.Data.String("Diagnostic BSP mode", mentalRayRendSetTraits.DiagnosticBSPMode.ToString()));
            data.Add(new Snoop.Data.Object("Diagnostic grid mode", mentalRayRendSetTraits.DiagnosticGridMode));
            data.Add(new Snoop.Data.String("Diagnostic mode", mentalRayRendSetTraits.DiagnosticMode.ToString()));
            data.Add(new Snoop.Data.String("Diagnostic photon mode", mentalRayRendSetTraits.DiagnosticPhotonMode.ToString()));
            data.Add(new Snoop.Data.Double("Energy multiplier", mentalRayRendSetTraits.EnergyMultiplier));
            data.Add(new Snoop.Data.Bool("Export MI enabled", mentalRayRendSetTraits.ExportMIEnabled));
            data.Add(new Snoop.Data.String("Export MI file name", mentalRayRendSetTraits.ExportMIFileName));
            data.Add(new Snoop.Data.Int("FG ray count", mentalRayRendSetTraits.FGRayCount));
            data.Add(new Snoop.Data.Object("FG sample radius state", mentalRayRendSetTraits.FGSampleRadiusState));
            data.Add(new Snoop.Data.Object("FG sample radius", mentalRayRendSetTraits.FGSampleRadius));
            data.Add(new Snoop.Data.Bool("Final gathering enabled", mentalRayRendSetTraits.FinalGatheringEnabled));
            data.Add(new Snoop.Data.Int("GI photons per light", mentalRayRendSetTraits.GIPhotonsPerLight));
            data.Add(new Snoop.Data.Int("GI sample count", mentalRayRendSetTraits.GISampleCount));
            data.Add(new Snoop.Data.Double("GI sample radius", mentalRayRendSetTraits.GISampleRadius));
            data.Add(new Snoop.Data.Bool("GI sample radius enabled", mentalRayRendSetTraits.GISampleRadiusEnabled));
            data.Add(new Snoop.Data.Bool("Global illumination enabled", mentalRayRendSetTraits.GlobalIlluminationEnabled));
            data.Add(new Snoop.Data.Double("Light luminance scale", mentalRayRendSetTraits.LightLuminanceScale));
            data.Add(new Snoop.Data.Int("Memory limit", mentalRayRendSetTraits.MemoryLimit));
            data.Add(new Snoop.Data.Double("Model scale factor", mentalRayRendSetTraits.ModelScaleFactor));
            data.Add(new Snoop.Data.Object("Photon trace depth", mentalRayRendSetTraits.PhotonTraceDepth));
            data.Add(new Snoop.Data.Object("Ray trace depth", mentalRayRendSetTraits.RayTraceDepth));
            data.Add(new Snoop.Data.Bool("Ray trace enabled", mentalRayRendSetTraits.RayTraceEnabled));
            data.Add(new Snoop.Data.Object("Sampling", mentalRayRendSetTraits.Sampling));
            data.Add(new Snoop.Data.Object("Sampling contrast color", mentalRayRendSetTraits.SamplingContrastColor));
            data.Add(new Snoop.Data.Object("Sampling filter", mentalRayRendSetTraits.SamplingFilter));
            data.Add(new Snoop.Data.Bool("Shadow map enabled", mentalRayRendSetTraits.ShadowMapEnabled));
            data.Add(new Snoop.Data.String("Shadow mode", mentalRayRendSetTraits.ShadowMode.ToString()));
            data.Add(new Snoop.Data.String("Tile order", mentalRayRendSetTraits.TileOrder.ToString()));
            data.Add(new Snoop.Data.Int("Tile size", mentalRayRendSetTraits.TileSize));

            data.Add(new Snoop.Data.String("Exposure type", mentalRayRendSetTraits.ExposureType.ToString()));
            data.Add(new Snoop.Data.String("Final gathering mode", mentalRayRendSetTraits.FinalGatheringMode.ToString()));
            data.Add(new Snoop.Data.Double("Shadow sampling multiplier", mentalRayRendSetTraits.ShadowSamplingMultiplier));
        }*/

        //private void
        //Stream(ArrayList data, MentalRayRenderSettingsTraits2 mentalRayRendSetTraits2)
        //{
        //    data.Add(new Snoop.Data.ClassSeparator(typeof(MentalRayRenderSettingsTraits2)));

        //    data.Add(new Snoop.Data.String("Exposure type", mentalRayRendSetTraits2.ExposureType.ToString()));
        //    data.Add(new Snoop.Data.String("Final gathering mode", mentalRayRendSetTraits2.FinalGatheringMode.ToString()));
        //    data.Add(new Snoop.Data.Double("Shadow sampling multiplier", mentalRayRendSetTraits2.ShadowSamplingMultiplier));           
        //}

        private void
        Stream(ArrayList data, RenderEnvironmentTraits rndrEnvTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(RenderEnvironmentTraits)));

            data.Add(new Snoop.Data.Bool("Enable", rndrEnvTrts.Enable));
            data.Add(new Snoop.Data.Object("Environment map", rndrEnvTrts.EnvironmentMap));
            data.Add(new Snoop.Data.Distance("Far distance", rndrEnvTrts.FarDistance));
            data.Add(new Snoop.Data.Distance("Near distance", rndrEnvTrts.NearDistance));
            data.Add(new Snoop.Data.Double("Far percentage", rndrEnvTrts.FarPercentage));
            data.Add(new Snoop.Data.Double("Near percentage", rndrEnvTrts.NearPercentage));
            data.Add(new Snoop.Data.Object("Fog color", rndrEnvTrts.FogColor));
            data.Add(new Snoop.Data.Bool("Is background", rndrEnvTrts.IsBackground));
        }

        private void
        Stream(ArrayList data, SolidBackgroundTraits solBkGrndTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SolidBackgroundTraits)));

            data.Add(new Snoop.Data.Object("Color solid", solBkGrndTrts.ColorSolid));
        }

        private void
        Stream(ArrayList data, SkyBackgroundTraits skyBkGrndTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SkyBackgroundTraits)));

            data.Add(new Snoop.Data.Object("Sky parameters", skyBkGrndTrts.SkyParameters));
        }

        #endregion

        private void
        Stream(ArrayList data, ViewportTraits viewPrtTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ViewportTraits)));

            data.Add(new Snoop.Data.Object("Ambient light color", viewPrtTrts.AmbientLightColor));
            data.Add(new Snoop.Data.ObjectId("Background", viewPrtTrts.Background));
            data.Add(new Snoop.Data.Double("Brightness", viewPrtTrts.Brightness));
            data.Add(new Snoop.Data.Double("Contrast", viewPrtTrts.Contrast));
            data.Add(new Snoop.Data.Bool("Default lighting on", viewPrtTrts.DefaultLightingOn));
            data.Add(new Snoop.Data.String("Default lighting type", viewPrtTrts.DefaultLightingType.ToString()));
            data.Add(new Snoop.Data.ObjectId("Render environment", viewPrtTrts.RenderEnvironment));
            data.Add(new Snoop.Data.ObjectId("Render settings", viewPrtTrts.RenderSettings));
            data.Add(new Snoop.Data.Object("Tone operator parameters", viewPrtTrts.ToneOperatorParameters));
            //ViewportTraits2 viewPrtTrts2 = viewPrtTrts as ViewportTraits2;
            //if (viewPrtTrts2 != null) {
            //    Stream(data, viewPrtTrts2);
            //    return;
            //}
        }

        //private void
        //Stream(ArrayList data, ViewportTraits2 viewPrtTrts2)
        //{
        //    data.Add(new Snoop.Data.ClassSeparator(typeof(ViewportTraits2)));

        //    data.Add(new Snoop.Data.Object("Tone operator parameters", viewPrtTrts2.ToneOperatorParameters));
        //}


        private void
        Stream(ArrayList data, VisualStyleTraits visStylTrts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(VisualStyleTraits)));

            data.Add(new Snoop.Data.Object("AcGi visual style", visStylTrts.AcGiVisualStyle));
        }

        #endregion

        private void
        Stream(ArrayList data, LightAttenuation lightAtt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(LightAttenuation)));

            data.Add(new Snoop.Data.String("Attenuation type", lightAtt.AttenuationType.ToString()));
            data.Add(new Snoop.Data.Double("End limit", lightAtt.EndLimit));
            data.Add(new Snoop.Data.Double("Start limit", lightAtt.StartLimit));
            data.Add(new Snoop.Data.Bool("Use limits", lightAtt.UseLimits));
        }

        /*private void
        Stream(ArrayList data, DisplayStyle dispStyl)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DisplayStyle)));

            data.Add(new Snoop.Data.Double("Brightness", dispStyl.Brightness));
            data.Add(new Snoop.Data.String("Display settings", dispStyl.DisplaySettings.ToString()));
            data.Add(new Snoop.Data.String("Shadow type", dispStyl.ShadowType.ToString()));
        }*/ // TBD: Fix JMA

        private void
        Stream(ArrayList data, ShadowParameters shadParams)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ShadowParameters)));

            data.Add(new Snoop.Data.Int("Shadow map size", shadParams.ShadowMapSize));
            //data.Add(new Snoop.Data.Byte("Shadow map softness", shadParams.ShadowMapSoftness));
            data.Add(new Snoop.Data.Bool("Shadows on", shadParams.ShadowsOn));
            data.Add(new Snoop.Data.String("Shadow type", shadParams.ShadowType.ToString()));

            data.Add(new Snoop.Data.Double("Extended light length", shadParams.ExtendedLightLength));
            data.Add(new Snoop.Data.Double("Extended light radius", shadParams.ExtendedLightRadius));
            data.Add(new Snoop.Data.String("Extended light shape", shadParams.ExtendedLightShape.ToString()));
            data.Add(new Snoop.Data.Double("Extended light width", shadParams.ExtendedLightWidth));
            data.Add(new Snoop.Data.Int("Shadows samples", shadParams.ShadowSamples));
            data.Add(new Snoop.Data.Bool("Shape visibility", shadParams.ShapeVisibility));
        }

        //private void
        //Stream(ArrayList data, ShadowParameters2 shadParams2)
        //{
        //    data.Add(new Snoop.Data.ClassSeparator(typeof(ShadowParameters2)));

        //    data.Add(new Snoop.Data.Double("Extended light length", shadParams2.ExtendedLightLength));
        //    data.Add(new Snoop.Data.Double("Extended light radius", shadParams2.ExtendedLightRadius));
        //    data.Add(new Snoop.Data.String("Extended light shape", shadParams2.ExtendedLightShape.ToString()));
        //    data.Add(new Snoop.Data.Double("Extended light width", shadParams2.ExtendedLightWidth));
        //    data.Add(new Snoop.Data.Int("Shadows samples", shadParams2.ShadowSamples));
        //    data.Add(new Snoop.Data.Bool("Shape visibility", shadParams2.ShapeVisibility));
        //}

        private void
        Stream(ArrayList data, Glyph glyph)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Glyph)));

            data.Add(new Snoop.Data.ObjectId("ID", glyph.Id));
            data.Add(new Snoop.Data.Bool("Is persistent", glyph.IsPersistent));
        }
        
        private void
        Stream(ArrayList data, MaterialColor matCol)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MaterialColor)));

            data.Add(new Snoop.Data.Object("Color", matCol.Color));
            data.Add(new Snoop.Data.Double("Factor", matCol.Factor));
            data.Add(new Snoop.Data.String("Method", matCol.Method.ToString()));
        }
        
        private void
        Stream(ArrayList data, MaterialMap matMap)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MaterialMap)));

            data.Add(new Snoop.Data.Double("Blend factor", matMap.BlendFactor));
            data.Add(new Snoop.Data.Object("Mapper", matMap.Mapper));
            data.Add(new Snoop.Data.String("Source", matMap.Source.ToString()));
            //data.Add(new Snoop.Data.String("Source filename", matMap.SourceFileName));    // Obsolete in Spago
            data.Add(new Snoop.Data.Object("Texture", matMap.Texture));
        }
        
        private void
        Stream(ArrayList data, Mapper mapper)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Mapper)));

            data.Add(new Snoop.Data.String("Auto transform", mapper.AutoTransform.ToString()));
            data.Add(new Snoop.Data.String("Projection", mapper.Projection.ToString()));
            data.Add(new Snoop.Data.String("U Tiling", mapper.UTiling.ToString()));
            data.Add(new Snoop.Data.String("V Tiling", mapper.VTiling.ToString()));
            data.Add(new Snoop.Data.Object("Transform", mapper.Transform));
        }

        #region MaterialTexture
        private void
        Stream(ArrayList data, MaterialTexture matTxtr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MaterialTexture)));

            /// no data here
            /// 
            ProceduralTexture procTxtr = matTxtr as ProceduralTexture;
            if (procTxtr != null) {
                Stream(data, procTxtr);
                return;
            }

            ImageTexture imgTxtr = matTxtr as ImageTexture;
            if (imgTxtr != null) {
                Stream(data, procTxtr);
                return;
            }
        }

        private void
        Stream(ArrayList data, ProceduralTexture procTxtr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ProceduralTexture)));
            /// no data
            /// 
            WoodTexture woodTxtr = procTxtr as WoodTexture;
            if (woodTxtr != null) {
                Stream(data, woodTxtr);
                return;
            }

            MarbleTexture marTxtr = procTxtr as MarbleTexture;
            if (marTxtr != null) {
                Stream(data, marTxtr);
                return;
            }

            GenericTexture genTxtr = procTxtr as GenericTexture;
            if (genTxtr != null) {
                Stream(data, genTxtr);
                return;
            }
        }

        private void
        Stream(ArrayList data, WoodTexture woodTxtr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(WoodTexture)));

            data.Add(new Snoop.Data.Double("Axial noise", woodTxtr.AxialNoise));
            data.Add(new Snoop.Data.Object("Color 1", woodTxtr.Color1));
            data.Add(new Snoop.Data.Object("Color 2", woodTxtr.Color2));
            data.Add(new Snoop.Data.Distance("Grain thickness", woodTxtr.GrainThickness));
            data.Add(new Snoop.Data.Double("Radial noise", woodTxtr.RadialNoise));
        }

        private void
        Stream(ArrayList data, MarbleTexture marTxtr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MarbleTexture)));

            data.Add(new Snoop.Data.Object("Stone color", marTxtr.StoneColor));
            data.Add(new Snoop.Data.Object("Vein color", marTxtr.VeinColor));
            data.Add(new Snoop.Data.Distance("Vein spacing", marTxtr.VeinSpacing));
            data.Add(new Snoop.Data.Distance("Vein width", marTxtr.VeinWidth));
        }

        private void
        Stream(ArrayList data, GenericTexture genTxtr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(GenericTexture)));

            data.Add(new Snoop.Data.Object("Definition", genTxtr.Definition));           
        }

        private void
        Stream(ArrayList data, ImageTexture imgTxtr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ImageTexture)));

            /// no data
            /// 
            ImageFileTexture imgFilTxtr = imgTxtr as ImageFileTexture;
            if (imgFilTxtr != null) {
                Stream(data, imgFilTxtr);
                return;
            }
        }

        private void
        Stream(ArrayList data, ImageFileTexture imgFilTxtr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ImageFileTexture)));

            data.Add(new Snoop.Data.String("Source file name", imgFilTxtr.SourceFileName));
        }

        #endregion

        private void
        Stream(ArrayList data, Autodesk.AutoCAD.GraphicsInterface.Geometry geo)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.GraphicsInterface.Geometry)));

            data.Add(new Snoop.Data.Object("Model to world transform", geo.ModelToWorldTransform));
            data.Add(new Snoop.Data.Object("World to model transform", geo.WorldToModelTransform));

            ViewportGeometry vPortGeo = geo as ViewportGeometry;
            if (vPortGeo != null) {
                data.Add(new Snoop.Data.ClassSeparator(typeof(ViewportGeometry)));
                // no more data here
                return;
            }

            WorldGeometry worldGeo = geo as WorldGeometry;
            if (worldGeo != null) {
                data.Add(new Snoop.Data.ClassSeparator(typeof(WorldGeometry)));
                // no more data here
                return;
            }
        }

        private void
        Stream(ArrayList data, Viewport vPort)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Viewport)));

            data.Add(new Snoop.Data.Int("Acad window ID", vPort.AcadWindowId));
            data.Add(new Snoop.Data.Int("Viewport ID", vPort.ViewportId.ToInt32()));
            data.Add(new Snoop.Data.Point3d("Camera location", vPort.CameraLocation));
            data.Add(new Snoop.Data.Point3d("Camera target", vPort.CameraTarget));
            data.Add(new Snoop.Data.Vector3d("Camera up vector", vPort.CameraUpVector));
            data.Add(new Snoop.Data.Object("Device context viewport corners", vPort.DeviceContextViewportCorners));
            data.Add(new Snoop.Data.Object("Front and back clipping", vPort.FrontAndBackClipping));
            data.Add(new Snoop.Data.Bool("Is perspective", vPort.IsPerspective));
            data.Add(new Snoop.Data.Double("Linetype generation criteria", vPort.LinetypeGenerationCriteria));
            data.Add(new Snoop.Data.Double("Linetype scale multiplier", vPort.LinetypeScaleMultiplier));
            data.Add(new Snoop.Data.Object("Eye to model transform", vPort.EyeToModelTransform));
            data.Add(new Snoop.Data.Object("Eye to world transform", vPort.EyeToWorldTransform));
            data.Add(new Snoop.Data.Object("Model to eye transform", vPort.ModelToEyeTransform));
            data.Add(new Snoop.Data.Object("World to eye transform", vPort.WorldToEyeTransform));
            data.Add(new Snoop.Data.Vector3d("View direction", vPort.ViewDirection));
        }

        private void
        Stream(ArrayList data, Context ctxt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Context)));

            data.Add(new Snoop.Data.String("ByBlock line weight", ctxt.ByBlockLineWeight.ToString()));
            data.Add(new Snoop.Data.ObjectId("ByBlock PlotStyleName ID", ctxt.ByBlockPlotStyleNameId));
            data.Add(new Snoop.Data.Database("Database", ctxt.Database));
            data.Add(new Snoop.Data.Object("Effective color", ctxt.EffectiveColor));
            data.Add(new Snoop.Data.Bool("Is boundary clipping", ctxt.IsBoundaryClipping));
            data.Add(new Snoop.Data.Bool("Is nesting", ctxt.IsNesting));
            data.Add(new Snoop.Data.Bool("Is plot generation", ctxt.IsPlotGeneration));
            data.Add(new Snoop.Data.Bool("Is PostScript out", ctxt.IsPostScriptOut));
            data.Add(new Snoop.Data.Bool("Supports TrueType text", ctxt.SupportsTrueTypeText));
        }

       /* private void
        Stream(ArrayList data, FaceStyle faceStyl)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FaceStyle)));

            data.Add(new Snoop.Data.String("Face color mode", faceStyl.FaceColorMode.ToString()));
            data.Add(new Snoop.Data.String("Face modifiers", faceStyl.FaceModifiers.ToString()));
            data.Add(new Snoop.Data.String("Lighting model", faceStyl.LightingModel.ToString()));
            data.Add(new Snoop.Data.String("Lighting quality", faceStyl.LightingQuality.ToString()));
            data.Add(new Snoop.Data.Object("Mono color", faceStyl.MonoColor));
            data.Add(new Snoop.Data.Double("Opacity", faceStyl.Opacity));
            data.Add(new Snoop.Data.Double("Specular highlight", faceStyl.SpecularHighlight));
        } */    // TBD - fix JMA

        /*private void
        Stream(ArrayList data, EdgeStyle edgStyl)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EdgeStyle)));

            data.Add(new Snoop.Data.Angle("Crease angle", edgStyl.CreaseAngle));
            data.Add(new Snoop.Data.Object("Edge color", edgStyl.EdgeColor));
            data.Add(new Snoop.Data.String("Edge model", edgStyl.EdgeModel.ToString()));
            data.Add(new Snoop.Data.String("Edge modifiers", edgStyl.EdgeModifiers.ToString()));
            data.Add(new Snoop.Data.String("Edge style apply", edgStyl.EdgeStyleApply.ToString()));
            data.Add(new Snoop.Data.String("Edge visibility", edgStyl.EdgeVisibility.ToString()));
            data.Add(new Snoop.Data.Int("Edge width", edgStyl.EdgeWidth));
            data.Add(new Snoop.Data.Int("Halo gap", edgStyl.HaloGap));
            data.Add(new Snoop.Data.Bool("Hide precision", edgStyl.HidePrecision));
            data.Add(new Snoop.Data.Object("Intersection color", edgStyl.IntersectionColor));
            data.Add(new Snoop.Data.String("Intersection linetype", edgStyl.IntersectionLinetype.ToString()));
            data.Add(new Snoop.Data.Int("Number of isolines", edgStyl.Isolines));
            data.Add(new Snoop.Data.String("Jitter", edgStyl.Jitter.ToString()));
            data.Add(new Snoop.Data.Object("Obscured color", edgStyl.ObscuredColor));
            data.Add(new Snoop.Data.String("Obscured linetype", edgStyl.ObscuredLinetype.ToString()));
            data.Add(new Snoop.Data.Double("Opacity", edgStyl.Opacity));
            data.Add(new Snoop.Data.Int("Overhang", edgStyl.Overhang));
            data.Add(new Snoop.Data.Object("Silhouette color", edgStyl.SilhouetteColor));
            data.Add(new Snoop.Data.Int("Silhouette width", edgStyl.SilhouetteWidth));
        }*/ // TBD Fix JMA

        private void
        Stream(ArrayList data, VisualStyle visStyl)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(VisualStyle)));
            // TBD: Fix JMA
            //data.Add(new Snoop.Data.Object("Display style", visStyl.DisplayStyle));
            //data.Add(new Snoop.Data.Object("Edge style", visStyl.EdgeStyle));
            //data.Add(new Snoop.Data.Object("Face style", visStyl.FaceStyle));

        }

        private void
        Stream(ArrayList data, ClipBoundary clipBdry)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ClipBoundary)));

            data.Add(new Snoop.Data.Distance("Back clip Z", clipBdry.BackClipZ));
            data.Add(new Snoop.Data.Distance("Front clip Z", clipBdry.FrontClipZ));
            data.Add(new Snoop.Data.Bool("Clipping back", clipBdry.ClippingBack));
            data.Add(new Snoop.Data.Bool("Clipping rront", clipBdry.ClippingFront));
            data.Add(new Snoop.Data.Bool("Draw boundary", clipBdry.DrawBoundary));
            data.Add(new Snoop.Data.Vector3d("Normal vector", clipBdry.NormalVector));
            data.Add(new Snoop.Data.Point3d("Point", clipBdry.Point));
            data.Add(new Snoop.Data.Object("Transform inverse BblockRef xform", clipBdry.TransformInverseBlockRefXForm));
            data.Add(new Snoop.Data.Object("Transform to clip space", clipBdry.TransformToClipSpace));

        }

        private void
       Stream(ArrayList data, EdgeData edgData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EdgeData)));


            data.Add(new Snoop.Data.ObjectIdCollection("Layers", edgData.GetLayers()));
            data.Add(new Snoop.Data.ObjectIdCollection("Linetypes", edgData.GetLineTypes()));
            data.Add(new Snoop.Data.ObjectCollection("True colors", edgData.GetTrueColors()));

            short[] colors = edgData.GetColors();
            data.Add(new Snoop.Data.CategorySeparator("Colors"));
            for (int i = 0; i < colors.Length; i++) {
                data.Add(new Snoop.Data.Int(string.Format("Color [{0}]", i), colors[i]));
            }



# if !ACAD2007
            // TBD: not sure what to do with IntPtrs?
            //System.IntPtr[] selMrkrs = edgData.GetSelectionMarkers();
            //data.Add(new Snoop.Data.CategorySeparator("Selection Markers"));
            //for (int i = 0; i < selMrkrs.Length; i++) {
            //    data.Add(new Snoop.Data.String(string.Format("Selection marker [{0}]", i), selMrkrs[i].ToString()));
            //}
#endif
        }

        private void
        Stream(ArrayList data, FaceData faceData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FaceData)));

            data.Add(new Snoop.Data.ObjectIdCollection("Layers", faceData.GetLayers()));
            data.Add(new Snoop.Data.ObjectCollection("Mappers", faceData.GetMappers()));
            data.Add(new Snoop.Data.ObjectIdCollection("Materials", faceData.GetMaterials()));
            data.Add(new Snoop.Data.ObjectCollection("True colors", faceData.GetTrueColors()));

            Autodesk.AutoCAD.Geometry.Vector3d[] vec3d = faceData.GetNormalVectors();
            data.Add(new Snoop.Data.CategorySeparator("Normal Vectors"));
            for (int i = 0; i < vec3d.Length; i++) {
                data.Add(new Snoop.Data.Vector3d(string.Format("Normal vector[{0}]", i), vec3d[i]));
            }

            short[] colors = faceData.GetColors();
            data.Add(new Snoop.Data.CategorySeparator("Colors"));
            for (int i = 0; i < colors.Length; i++) {
                data.Add(new Snoop.Data.Int(string.Format("Color [{0}]", i), colors[i]));
            }

            // TBD: not sure what to do with IntPtrs?
# if !ACAD2007
            System.IntPtr[] selMrkrs = faceData.GetSelectionMarkers();
            data.Add(new Snoop.Data.CategorySeparator("Selection Markers"));
            for (int i = 0; i < selMrkrs.Length; i++) {
                data.Add(new Snoop.Data.String(string.Format("Selection marker [{0}]", i), selMrkrs[i].ToString()));
            }
#endif

        }

        private void
        Stream(ArrayList data, VertexData vertData)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(VertexData)));


            data.Add(new Snoop.Data.String("Orientation flag", vertData.OrientationFlag.ToString()));
            data.Add(new Snoop.Data.ObjectCollection("True colors", vertData.GetTrueColors()));

            Autodesk.AutoCAD.Geometry.Vector3d[] vec3d = vertData.GetNormalVectors();
            data.Add(new Snoop.Data.CategorySeparator("Normal Vectors"));
            for (int i = 0; i < vec3d.Length; i++) {
                data.Add(new Snoop.Data.Vector3d(String.Format("Normal vector [{0}]", i), vec3d[i]));
            }
        }

        private void
        Stream(ArrayList data, ContextualColors cntxtColors)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ContextualColors)));


            data.Add(new Snoop.Data.Int("Camera clipping", cntxtColors.CameraClipping));
            data.Add(new Snoop.Data.Int("Camera frustrum", cntxtColors.CameraFrustrum));
            data.Add(new Snoop.Data.Int("Camera glyphs", cntxtColors.CameraGlyphs));
            data.Add(new Snoop.Data.Int("Grid axis lines", cntxtColors.GridAxisLines));
            data.Add(new Snoop.Data.Int("Grid axis line tint XYZ", cntxtColors.GridAxisLineTintXYZ));
            data.Add(new Snoop.Data.Int("Grid major lines", cntxtColors.GridMajorLines));
            data.Add(new Snoop.Data.Int("Grid major line tint XYZ", cntxtColors.GridMajorLineTintXYZ));
            data.Add(new Snoop.Data.Int("Grid minor lines", cntxtColors.GridMinorLines));
            data.Add(new Snoop.Data.Int("Grid minor line tint XYZ", cntxtColors.GridMinorLineTintXYZ));
            data.Add(new Snoop.Data.Object("Light distance color", cntxtColors.LightDistanceColor));
            data.Add(new Snoop.Data.Int("Light end limit", cntxtColors.LightEndLimit));
            data.Add(new Snoop.Data.Int("Light fall off", cntxtColors.LightFalloff));
            data.Add(new Snoop.Data.Int("Light glyphs", cntxtColors.LightGlyphs));
            data.Add(new Snoop.Data.Int("Light hot spot", cntxtColors.LightHotspot));
            data.Add(new Snoop.Data.Object("Light shape color", cntxtColors.LightShapeColor));
            data.Add(new Snoop.Data.Int("Light start limit", cntxtColors.LightStartLimit));
            data.Add(new Snoop.Data.Object("Web mesh color", cntxtColors.WebMeshColor));
            data.Add(new Snoop.Data.Object("Web mesh missing color", cntxtColors.WebMeshMissingColor));
        }

        private void
        Stream(ArrayList data, SkyParameters skyParams)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SkyParameters)));

            data.Add(new Snoop.Data.Bool("Aerial perspective", skyParams.AerialPerspective));
            data.Add(new Snoop.Data.Double("Disk intensity", skyParams.DiskIntensity));
            data.Add(new Snoop.Data.Double("Disk scale", skyParams.DiskScale));
            data.Add(new Snoop.Data.Double("Glow intensity", skyParams.GlowIntensity));
            data.Add(new Snoop.Data.Object("Ground color", skyParams.GroundColor));
            data.Add(new Snoop.Data.Double("Haze", skyParams.Haze));
            data.Add(new Snoop.Data.Double("Horizon blur", skyParams.HorizonBlur));
            data.Add(new Snoop.Data.Double("Horizon height", skyParams.HorizonHeight));
            data.Add(new Snoop.Data.Bool("Illumination", skyParams.Illumination));
            data.Add(new Snoop.Data.Double("Intensity factor", skyParams.IntensityFactor));
            data.Add(new Snoop.Data.Object("Night color", skyParams.NightColor));
            data.Add(new Snoop.Data.Double("Red blue shift", skyParams.RedBlueShift));
            data.Add(new Snoop.Data.Double("Saturationt", skyParams.Saturation));
            data.Add(new Snoop.Data.Int("Solar disk samples", skyParams.SolarDiskSamples));
            data.Add(new Snoop.Data.Vector3d("Sun direction", skyParams.SunDirection));
            data.Add(new Snoop.Data.Double("Visibility distance", skyParams.VisibilityDistance));                           
        }

        private void
        Stream(ArrayList data, ToneOperatorParameters toneOperParams)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ToneOperatorParameters)));

            data.Add(new Snoop.Data.Double("Brightness", toneOperParams.Brightness));
            data.Add(new Snoop.Data.Bool("Chromatic adaptation", toneOperParams.ChromaticAdaptation));
            data.Add(new Snoop.Data.Bool("Color differentiation", toneOperParams.ColorDifferentiation));
            data.Add(new Snoop.Data.Double("Contrast", toneOperParams.Contrast));
            data.Add(new Snoop.Data.String("Exterior day light", toneOperParams.ExteriorDaylight.ToString()));
            data.Add(new Snoop.Data.Bool("Is active", toneOperParams.IsActive));
            data.Add(new Snoop.Data.Double("Mid tones", toneOperParams.MidTones));
            data.Add(new Snoop.Data.Bool("Process background", toneOperParams.ProcessBackground));
            data.Add(new Snoop.Data.Object("White color", toneOperParams.WhiteColor));                    
        }

        //private void
        //Stream(ArrayList data, Variant variant)
        //{
        //    data.Add(new Snoop.Data.ClassSeparator(typeof(Variant)));

        //    data.Add(new Snoop.Data.Bool("Boolean", variant.Boolean));
        //    data.Add(new Snoop.Data.Object("Color", variant.Color));
        //    data.Add(new Snoop.Data.Double("Double", variant.Double));
        //    data.Add(new Snoop.Data.Int("Int", variant.Int));
        //    data.Add(new Snoop.Data.String("String", variant.String));
        //    data.Add(new Snoop.Data.String("Type", variant.Type.ToString()));
        //}

        private void
        Stream(ArrayList data, FontDescriptor font)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.GraphicsInterface.FontDescriptor)));

            data.Add(new Snoop.Data.String("Font typeface", font.TypeFace));
            data.Add(new Snoop.Data.Bool("Font is bold", font.Bold));
            data.Add(new Snoop.Data.Bool("Font is italic", font.Italic));
            data.Add(new Snoop.Data.Int("Font charset", font.CharacterSet));
            data.Add(new Snoop.Data.Int("Font pitch/family", font.PitchAndFamily));
        }

       /* private void
        Stream(ArrayList data, EdgeStyleOverride edgStyOvRd)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(EdgeStyleOverride)));

            data.Add(new Snoop.Data.Object("Edge style", edgStyOvRd.EdgeStyle));
            data.Add(new Snoop.Data.Double("Mask", edgStyOvRd.Mask));
        }*/  // TBD: fix JMA

        private void
        Stream(ArrayList data, MaterialDiffuseComponent matDiffComp)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MaterialDiffuseComponent)));

            data.Add(new Snoop.Data.Object("Color", matDiffComp.Color));
            data.Add(new Snoop.Data.Object("Material map", matDiffComp.Map));
        }

        private void
        Stream(ArrayList data, MaterialSpecularComponent matSpecComp)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MaterialSpecularComponent)));

            data.Add(new Snoop.Data.Object("Color", matSpecComp.Color));
            data.Add(new Snoop.Data.Double("Gloss", matSpecComp.Gloss));
            data.Add(new Snoop.Data.Object("Material pap", matSpecComp.Map));
        }

        private void
        Stream(ArrayList data, MaterialOpacityComponent matOpacComp)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MaterialOpacityComponent)));

            data.Add(new Snoop.Data.Object("Material map", matOpacComp.Map));
            data.Add(new Snoop.Data.Double("Percentage", matOpacComp.Percentage));
        }

        private void
        Stream(ArrayList data, MaterialRefractionComponent matRefComp)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MaterialRefractionComponent)));

            data.Add(new Snoop.Data.Object("Material map", matRefComp.Map));
            data.Add(new Snoop.Data.Double("Index", matRefComp.Index));
        }

        /*private void
        Stream(ArrayList data, MentalRayRenderSettingsTraitsIntegerRangeParameter mRayRSetngsTraitsIntRngParam)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MentalRayRenderSettingsTraitsIntegerRangeParameter)));

            data.Add(new Snoop.Data.Object("Max", mRayRSetngsTraitsIntRngParam.Max));
            data.Add(new Snoop.Data.Double("Min", mRayRSetngsTraitsIntRngParam.Min));
        }

        private void
        Stream(ArrayList data, MentalRayRenderSettingsTraitsTraceParameter mRayRSetngsTraitsTraceParam)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MentalRayRenderSettingsTraitsTraceParameter)));

            data.Add(new Snoop.Data.Int("Reflection", mRayRSetngsTraitsTraceParam.Reflection));
            data.Add(new Snoop.Data.Int("Refraction", mRayRSetngsTraitsTraceParam.Refraction));
            data.Add(new Snoop.Data.Int("Sum", mRayRSetngsTraitsTraceParam.Sum));
        }

        private void
        Stream(ArrayList data, MentalRayRenderSettingsTraitsFloatParameter mRayRSetngsTraitsFltParam)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MentalRayRenderSettingsTraitsFloatParameter)));

            data.Add(new Snoop.Data.Double("A", mRayRSetngsTraitsFltParam.A));
            data.Add(new Snoop.Data.Double("B", mRayRSetngsTraitsFltParam.B));
            data.Add(new Snoop.Data.Double("G", mRayRSetngsTraitsFltParam.G));
            data.Add(new Snoop.Data.Double("R", mRayRSetngsTraitsFltParam.R));
        }

        private void
        Stream(ArrayList data, MentalRayRenderSettingsTraitsDoubleRangeParameter mRayRSetngsTraitsDblRngParam)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MentalRayRenderSettingsTraitsDoubleRangeParameter)));

            data.Add(new Snoop.Data.Double("Max", mRayRSetngsTraitsDblRngParam.Max));
            data.Add(new Snoop.Data.Double("Min", mRayRSetngsTraitsDblRngParam.Min));
        }

        private void
        Stream(ArrayList data, MentalRayRenderSettingsTraitsSamplingParameter mRayRSetngsTraitsSmplParam)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MentalRayRenderSettingsTraitsSamplingParameter)));

            data.Add(new Snoop.Data.String("Filter", mRayRSetngsTraitsSmplParam.Filter.ToString()));
            data.Add(new Snoop.Data.Distance("Height", mRayRSetngsTraitsSmplParam.Height));
            data.Add(new Snoop.Data.Distance("Width", mRayRSetngsTraitsSmplParam.Width));
        }

        private void
        Stream(ArrayList data, MentalRayRenderSettingsTraitsBoolParameter mRayRSetngsTraitsBoolParam)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MentalRayRenderSettingsTraitsBoolParameter)));

            data.Add(new Snoop.Data.Bool("Max", mRayRSetngsTraitsBoolParam.Max));
            data.Add(new Snoop.Data.Bool("Min", mRayRSetngsTraitsBoolParam.Min));
            data.Add(new Snoop.Data.Bool("Pixels", mRayRSetngsTraitsBoolParam.Pixels));
        }

        private void
        Stream(ArrayList data, MentalRayRenderSettingsTraitsDiagnosticGridModeParameter mRayRSetngsTraitsDiaGridModeParam)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MentalRayRenderSettingsTraitsDiagnosticGridModeParameter)));

            data.Add(new Snoop.Data.String("Mode", mRayRSetngsTraitsDiaGridModeParam.Mode.ToString()));
            data.Add(new Snoop.Data.Double("Size", mRayRSetngsTraitsDiaGridModeParam.Size));
        }*/

        private void
        Stream(ArrayList data, FrontAndBackClipping frntBackClip)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FrontAndBackClipping)));

            data.Add(new Snoop.Data.Distance("Front", frntBackClip.Front));
            data.Add(new Snoop.Data.Distance("Back", frntBackClip.Back));
            data.Add(new Snoop.Data.Bool("Clip front", frntBackClip.ClipFront));
            data.Add(new Snoop.Data.Bool("Clip back", frntBackClip.ClipBack));
        }

        private void
        Stream(ArrayList data, ColorRGB colorRGB)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ColorRGB)));

            data.Add(new Snoop.Data.Double("Blue", colorRGB.Blue));
            data.Add(new Snoop.Data.Double("Green", colorRGB.Green));
            data.Add(new Snoop.Data.Double("Red", colorRGB.Red));          
        }

        private void
        Stream(ArrayList data, ColorRGBA colorRGBA)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(ColorRGBA)));

            data.Add(new Snoop.Data.Double("Blue", colorRGBA.Alpha));
            data.Add(new Snoop.Data.Double("Green", colorRGBA.Green));
            data.Add(new Snoop.Data.Double("Red", colorRGBA.Red));
            data.Add(new Snoop.Data.Double("Blue", colorRGBA.Blue));
        }

        private void
        Stream(ArrayList data, MaterialNormalMapComponent matNormMapComp)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(MaterialNormalMapComponent)));

            data.Add(new Snoop.Data.Object("Map", matNormMapComp.Map));
            data.Add(new Snoop.Data.String("Method", matNormMapComp.Method.ToString()));
            data.Add(new Snoop.Data.Double("Strength", matNormMapComp.Strength));            
        }    
    }
}
