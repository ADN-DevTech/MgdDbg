
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
using Autodesk.AutoCAD.Colors;
using MgdDbg.Snoop.Collectors;

namespace MgdDbg.Snoop.CollectorExts
{
	/// <summary>
	/// This is a Snoop Collector Extension object to collect data from Color objects.
	/// </summary>
	public class Color : CollectorExt
	{
		public
		Color()
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
            Autodesk.AutoCAD.Colors.Color clr = e.ObjToSnoop as Autodesk.AutoCAD.Colors.Color;
            if (clr != null) {
                Stream(snoopCollector.Data(), clr);
                return;
            }
                
                // these are value types so we have to treat them a bit different
            if (e.ObjToSnoop is Autodesk.AutoCAD.Colors.EntityColor) {
                Stream(snoopCollector.Data(), (Autodesk.AutoCAD.Colors.EntityColor)e.ObjToSnoop);
                return;
            }

            if (e.ObjToSnoop is Autodesk.AutoCAD.Colors.Transparency) {
                Stream(snoopCollector.Data(), (Autodesk.AutoCAD.Colors.Transparency)e.ObjToSnoop);
                return;
            }
        }
        

        private void
        Stream(ArrayList data, Autodesk.AutoCAD.Colors.Color clr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.Colors.Color)));
            
            data.Add(new Snoop.Data.Int("Red", clr.Red));
            data.Add(new Snoop.Data.Int("Green", clr.Green));
            data.Add(new Snoop.Data.Int("Blue", clr.Blue));
            data.Add(new Snoop.Data.Int("Color index", clr.ColorIndex));
            data.Add(new Snoop.Data.String("Color method", clr.ColorMethod.ToString()));
            data.Add(new Snoop.Data.Bool("Has book name", clr.HasBookName));
            data.Add(new Snoop.Data.String("Book name", clr.BookName));
            data.Add(new Snoop.Data.Bool("Has color name", clr.HasColorName));
            data.Add(new Snoop.Data.String("Color name", clr.ColorName));
            data.Add(new Snoop.Data.String("Color name for display", clr.ColorNameForDisplay));
            data.Add(new Snoop.Data.String("Color value", clr.ColorValue.ToString()));
            data.Add(new Snoop.Data.String("Description", clr.Description));
            data.Add(new Snoop.Data.String("Dictionary key", clr.DictionaryKey));
            data.Add(new Snoop.Data.Int("Dictionary key length", clr.DictionaryKeyLength));
            data.Add(new Snoop.Data.Object("Entity color", clr.EntityColor));
            data.Add(new Snoop.Data.String("Explanation", clr.Explanation));
            data.Add(new Snoop.Data.Bool("Is ByAci", clr.IsByAci));
            data.Add(new Snoop.Data.Bool("Is ByBlock", clr.IsByBlock));
            data.Add(new Snoop.Data.Bool("Is ByColor", clr.IsByColor));
            data.Add(new Snoop.Data.Bool("Is ByLayer", clr.IsByLayer));
            data.Add(new Snoop.Data.Bool("Is ByPen", clr.IsByPen));
            data.Add(new Snoop.Data.Bool("Is Foreground", clr.IsForeground));
            data.Add(new Snoop.Data.Bool("Is None", clr.IsNone));
            data.Add(new Snoop.Data.Int("Pen index", clr.PenIndex));
        }
        
        private void
        Stream(ArrayList data, Autodesk.AutoCAD.Colors.EntityColor clr)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.Colors.EntityColor)));

            data.Add(new Snoop.Data.Int("Red", clr.Red));
            data.Add(new Snoop.Data.Int("Green", clr.Green));
            data.Add(new Snoop.Data.Int("Blue", clr.Blue));
            data.Add(new Snoop.Data.Int("Color index", clr.ColorIndex));
            data.Add(new Snoop.Data.String("Color method", clr.ColorMethod.ToString()));
            data.Add(new Snoop.Data.Bool("Is ByAci", clr.IsByAci));
            data.Add(new Snoop.Data.Bool("Is ByBlock", clr.IsByBlock));
            data.Add(new Snoop.Data.Bool("Is ByColor", clr.IsByColor));
            data.Add(new Snoop.Data.Bool("Is ByLayer", clr.IsByLayer));
            data.Add(new Snoop.Data.Bool("Is ByPen", clr.IsByPen));
            data.Add(new Snoop.Data.Bool("Is Foreground", clr.IsForeground));
            data.Add(new Snoop.Data.Bool("Is None", clr.IsNone));
            data.Add(new Snoop.Data.Bool("Is layer frozen", clr.IsLayerFrozen));
            data.Add(new Snoop.Data.Bool("Is layer off", clr.IsLayerOff));
            // sometimes this asserts ... figure out why??
            data.Add(new Snoop.Data.Bool("Is layer frozen or off", clr.IsLayerFrozenOrOff));
            data.Add(new Snoop.Data.Int("Layer index", clr.LayerIndex));
            data.Add(new Snoop.Data.Int("Pen index", clr.PenIndex));
            data.Add(new Snoop.Data.Int("True color", clr.TrueColor));
        }
        
        private void
        Stream(ArrayList data, Autodesk.AutoCAD.Colors.Transparency transp)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.Colors.Transparency)));

            data.Add(new Snoop.Data.Bool("Is ByAlpha", transp.IsByAlpha));
            if (transp.IsByAlpha) {
                data.Add(new Snoop.Data.Int("Alpha", transp.Alpha));
                data.Add(new Snoop.Data.Bool("Is clear", transp.IsClear));
                data.Add(new Snoop.Data.Bool("Is solid", transp.IsSolid));
            }
            data.Add(new Snoop.Data.Bool("Is ByBlock", transp.IsByBlock));
            data.Add(new Snoop.Data.Bool("Is ByLayer", transp.IsByLayer));
        }
    }
}
