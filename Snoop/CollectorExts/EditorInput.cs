//
// (C) Copyright 2006 by Autodesk, Inc. 
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

using Autodesk.AutoCAD.EditorInput;

using MgdDbg.Snoop.Collectors;

namespace MgdDbg.Snoop.CollectorExts {
    class EditorInput : CollectorExt {

		public
		EditorInput()
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
            Editor editor = e.ObjToSnoop as Editor;
            if (editor != null) {
                Stream(snoopCollector.Data(), editor);
                return;
            }

            PromptOptions promptOpts = e.ObjToSnoop as PromptOptions;
            if (promptOpts != null) {
                Stream(snoopCollector.Data(), promptOpts);
                return;
            }

            PromptStringOptionsEventArgs promptStringArgs = e.ObjToSnoop as PromptStringOptionsEventArgs;
            if (promptStringArgs != null) {
                Stream(snoopCollector.Data(), promptStringArgs);
                return;
            }

            PromptSelectionOptionsEventArgs promptSelectionArgs = e.ObjToSnoop as PromptSelectionOptionsEventArgs;
            if (promptSelectionArgs != null) {
                Stream(snoopCollector.Data(), promptSelectionArgs);
                return;
            }

            PromptSelectionOptions promptSelectionOpts = e.ObjToSnoop as PromptSelectionOptions;
            if (promptSelectionOpts != null) {
                Stream(snoopCollector.Data(), promptSelectionOpts);
                return;
            }

            PromptPointOptionsEventArgs promptPointArgs = e.ObjToSnoop as PromptPointOptionsEventArgs;
            if (promptPointArgs != null) {
                Stream(snoopCollector.Data(), promptPointArgs);
                return;
            }

            PromptNestedEntityResultEventArgs promptNestResultArgs = e.ObjToSnoop as PromptNestedEntityResultEventArgs;
            if (promptNestResultArgs != null) {
                Stream(snoopCollector.Data(), promptNestResultArgs);
                return;
            }

            PromptResult promptResult = e.ObjToSnoop as PromptResult;
            if (promptResult != null) {
                Stream(snoopCollector.Data(), promptResult);
                return;
            }

            PromptKeywordOptionsEventArgs promptKeywordArgs = e.ObjToSnoop as PromptKeywordOptionsEventArgs;
            if (promptKeywordArgs != null) {
                Stream(snoopCollector.Data(), promptKeywordArgs);
                return;
            }

            PromptIntegerOptionsEventArgs promptIntArgs = e.ObjToSnoop as PromptIntegerOptionsEventArgs;
            if (promptIntArgs != null) {
                Stream(snoopCollector.Data(), promptIntArgs);
                return;
            }

            PromptEntityOptionsEventArgs promptEntArgs = e.ObjToSnoop as PromptEntityOptionsEventArgs;
            if (promptEntArgs != null) {
                Stream(snoopCollector.Data(), promptEntArgs);
                return;
            }

            PromptDoubleOptionsEventArgs promptDoubleArgs = e.ObjToSnoop as PromptDoubleOptionsEventArgs;
            if (promptDoubleArgs != null) {
                Stream(snoopCollector.Data(), promptDoubleArgs);
                return;
            }

            PromptSelectionResult prSelRes = e.ObjToSnoop as PromptSelectionResult;
            if (prSelRes != null) {           
                Stream(snoopCollector.Data(), prSelRes);
                return;
            }

            SelectionSet selSet = e.ObjToSnoop as SelectionSet;
            if (selSet != null) {           
                Stream(snoopCollector.Data(), selSet);
                return;
            }

            SelectedObject selObj = e.ObjToSnoop as SelectedObject;
            if (selObj != null) {           
                Stream(snoopCollector.Data(), selObj);
                return;
            }

            SelectedSubObject selSubObj = e.ObjToSnoop as SelectedSubObject;
            if (selSubObj != null) {           
                Stream(snoopCollector.Data(), selSubObj);
                return;
            }

            SelectionDetails selDetails = e.ObjToSnoop as SelectionDetails;
            if (selDetails != null) {           
                Stream(snoopCollector.Data(), selDetails);
                return;
            }

            SelectionRemovedEventArgs selRemovedArgs = e.ObjToSnoop as SelectionRemovedEventArgs;
            if (selRemovedArgs != null) {
                Stream(snoopCollector.Data(), selRemovedArgs);
                return;
            }

            SelectionAddedEventArgs selAddedArgs = e.ObjToSnoop as SelectionAddedEventArgs;
            if (selAddedArgs != null) {
                Stream(snoopCollector.Data(), selAddedArgs);
                return;
            }

            DraggingEndedEventArgs dragEndArgs = e.ObjToSnoop as DraggingEndedEventArgs;
            if (dragEndArgs != null) {
                Stream(snoopCollector.Data(), dragEndArgs);
                return;
            }

            InputPointContext inputPtCntxt = e.ObjToSnoop as InputPointContext;
            if (inputPtCntxt != null) {
                Stream(snoopCollector.Data(), inputPtCntxt);
                return;
            }

            Keyword keyword = e.ObjToSnoop as Keyword;
            if (keyword != null) {
                Stream(snoopCollector.Data(), keyword);
                return;
            }

            PointFilterEventArgs ptFilterEventArgs = e.ObjToSnoop as PointFilterEventArgs;
            if (ptFilterEventArgs != null) {
                Stream(snoopCollector.Data(), ptFilterEventArgs);
                return;
            }

            PointFilterResult ptFilterRes = e.ObjToSnoop as PointFilterResult;
            if (ptFilterRes != null) {
                Stream(snoopCollector.Data(), ptFilterRes);
                return;
            }

            PointMonitorEventArgs ptMonitorArgs = e.ObjToSnoop as PointMonitorEventArgs;
            if (ptMonitorArgs != null) {
                Stream(snoopCollector.Data(), ptMonitorArgs);
                return;
            }

            PromptAngleOptionsEventArgs promptAngleOptsArgs = e.ObjToSnoop as PromptAngleOptionsEventArgs;
            if (promptAngleOptsArgs != null) {
                Stream(snoopCollector.Data(), promptAngleOptsArgs);
                return;
            }

            PromptDistanceOptionsEventArgs promptDistanceOptsArgs = e.ObjToSnoop as PromptDistanceOptionsEventArgs;
            if (promptDistanceOptsArgs != null) {
                Stream(snoopCollector.Data(), promptDistanceOptsArgs);
                return;
            }

            PromptDoubleResultEventArgs promptDoubleResArgs = e.ObjToSnoop as PromptDoubleResultEventArgs;
            if (promptDoubleResArgs != null) {
                Stream(snoopCollector.Data(), promptDoubleResArgs);
                return;
            }

            PromptFileOptions promptFileOpts = e.ObjToSnoop as PromptFileOptions;
            if (promptFileOpts != null) {
                Stream(snoopCollector.Data(), promptFileOpts);
                return;
            }

            PromptForEntityEndingEventArgs promptForEntEndArgs = e.ObjToSnoop as PromptForEntityEndingEventArgs;
            if (promptForEntEndArgs != null) {
                Stream(snoopCollector.Data(), promptForEntEndArgs);
                return;
            }

            PromptForSelectionEndingEventArgs promptForSelEndArgs = e.ObjToSnoop as PromptForSelectionEndingEventArgs;
            if (promptForSelEndArgs != null) {
                Stream(snoopCollector.Data(), promptForSelEndArgs);
                return;
            }

            PromptNestedEntityOptions promptNestEntOpts = e.ObjToSnoop as PromptNestedEntityOptions;
            if (promptNestEntOpts != null) {
                Stream(snoopCollector.Data(), promptNestEntOpts);
                return;
            }

            PromptNestedEntityOptionsEventArgs promptNestEntOptsArgs = e.ObjToSnoop as PromptNestedEntityOptionsEventArgs;
            if (promptNestEntOptsArgs != null) {
                Stream(snoopCollector.Data(), promptNestEntOptsArgs);
                return;
            }

            PromptSelectionResultEventArgs promptSelResArgs = e.ObjToSnoop as PromptSelectionResultEventArgs;
            if (promptSelResArgs != null) {
                Stream(snoopCollector.Data(), promptSelResArgs);
                return;
            }

                // ValueTypes we have to treat a bit different
            if (e.ObjToSnoop is PickPointDescriptor) {
                Stream(snoopCollector.Data(), (PickPointDescriptor)e.ObjToSnoop);
                return;
            }
        }

        private void
        Stream (ArrayList data, Editor editor)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Editor)));

            data.Add(new Snoop.Data.Object("Current User Coordinate System", editor.CurrentUserCoordinateSystem));
            data.Add(new Snoop.Data.ObjectId("Current Viewport ObjectId", editor.CurrentViewportObjectId));
            data.Add(new Snoop.Data.Object("Document", editor.Document));
            data.Add(new Snoop.Data.Bool("Is Dragging", editor.IsDragging));
            data.Add(new Snoop.Data.Bool("Is Quiescent", editor.IsQuiescent));
            data.Add(new Snoop.Data.Bool("Mouse Has Moved", editor.MouseHasMoved));
            // throws an assertion failure
            //data.Add(new Snoop.Data.Object("Current View", editor.GetCurrentView()));
        }


        private void
        Stream(ArrayList data, SelectionRemovedEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SelectionRemovedEventArgs)));

            data.Add(new Snoop.Data.String("Flags", args.Flags.ToString()));
            data.Add(new Snoop.Data.SelectionSet("Selection", args.Selection));
            data.Add(new Snoop.Data.SelectionSet("Removed objects", args.RemovedObjects));
        }

        private void
        Stream(ArrayList data, SelectionAddedEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SelectionAddedEventArgs)));

            data.Add(new Snoop.Data.String("Flags", args.Flags.ToString()));
            data.Add(new Snoop.Data.SelectionSet("Selection", args.Selection));
            data.Add(new Snoop.Data.SelectionSet("Added objects", args.AddedObjects));
        }

        private void
        Stream(ArrayList data, PromptOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptOptions)));

            data.Add(new Snoop.Data.String("Message", opts.Message));
            data.Add(new Snoop.Data.Bool("Append keywords to message", opts.AppendKeywordsToMessage));
            data.Add(new Snoop.Data.Bool("Is read-only", opts.IsReadOnly));
            data.Add(new Snoop.Data.Object("Keywords", opts.Keywords));

            PromptEditorOptions promptEdOpts = opts as PromptEditorOptions;
            if (promptEdOpts != null) {
                Stream(data, promptEdOpts);
                return;
            }

            JigPromptOptions jigPromptOpts = opts as JigPromptOptions;
            if (jigPromptOpts != null) {
                Stream(data, jigPromptOpts);
                return;
            }
        }

        private void
        Stream(ArrayList data, PromptEditorOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptEditorOptions)));

            PromptCornerOptions promptCornerOpts = opts as PromptCornerOptions;
            if (promptCornerOpts != null) {
                Stream(data, promptCornerOpts);
                return;
            }

            PromptStringOptions promptStrOpts = opts as PromptStringOptions;
            if (promptStrOpts != null) {
                Stream(data, promptStrOpts);
                return;
            }

            PromptKeywordOptions promptKwordOpts = opts as PromptKeywordOptions;
            if (promptKwordOpts != null) {
                Stream(data, promptKwordOpts);
                return;
            }

            PromptNumericalOptions promptNumpericalOpts = opts as PromptNumericalOptions;
            if (promptNumpericalOpts != null) {
                Stream(data, promptNumpericalOpts);
                return;
            }

            PromptEntityOptions promptEntOpts = opts as PromptEntityOptions;
            if (promptEntOpts != null) {
                Stream(data, promptEntOpts);
                return;
            }

            PromptAngleOptions promptAngleOpts = opts as PromptAngleOptions;
            if (promptAngleOpts != null) {
                Stream(data, promptAngleOpts);
                return;
            }

            PromptDragOptions promptDragOpts = opts as PromptDragOptions;
            if (promptDragOpts != null) {
                Stream(data, promptDragOpts);
                return;
            }
        }

        private void
        Stream(ArrayList data, PromptCornerOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptCornerOptions)));

            data.Add(new Snoop.Data.Bool("Allow arbitrary input", opts.AllowArbitraryInput));
            data.Add(new Snoop.Data.Bool("Allow none", opts.AllowNone));
            data.Add(new Snoop.Data.Point3d("Base point", opts.BasePoint));
            data.Add(new Snoop.Data.Bool("Limits checked", opts.LimitsChecked));
            data.Add(new Snoop.Data.Bool("Use dashed line", opts.UseDashedLine));

            PromptPointOptions promptPtOpts = opts as PromptPointOptions;
            if (promptPtOpts != null) {
                Stream(data, promptPtOpts);
                return;
            }
        }

        private void
        Stream(ArrayList data, PromptPointOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptPointOptions)));

            data.Add(new Snoop.Data.Bool("Use base point", opts.UseBasePoint));
        }

        private void
        Stream(ArrayList data, PromptStringOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptStringOptions)));

            data.Add(new Snoop.Data.Bool("Allow spaces", opts.AllowSpaces));
            data.Add(new Snoop.Data.String("Default value", opts.DefaultValue));
            data.Add(new Snoop.Data.Bool("Use default value", opts.UseDefaultValue));
        }

        private void
        Stream(ArrayList data, PromptKeywordOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptKeywordOptions)));

            data.Add(new Snoop.Data.Bool("Allow arbitrary input", opts.AllowArbitraryInput));
            data.Add(new Snoop.Data.Bool("Allow none", opts.AllowNone));
        }

        private void
        Stream(ArrayList data, PromptNumericalOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptNumericalOptions)));

            data.Add(new Snoop.Data.Bool("Allow arbitrary input", opts.AllowArbitraryInput));
            data.Add(new Snoop.Data.Bool("Allow none", opts.AllowNone));
            data.Add(new Snoop.Data.Bool("Allow negative", opts.AllowNegative));
            data.Add(new Snoop.Data.Bool("Allow zero", opts.AllowZero));
            data.Add(new Snoop.Data.Bool("Use default value", opts.UseDefaultValue));

            PromptIntegerOptions promptIntOpts = opts as PromptIntegerOptions;
            if (promptIntOpts != null) {
                Stream(data, promptIntOpts);
                return;
            }

            PromptDoubleOptions promptDoubleOpts = opts as PromptDoubleOptions;
            if (promptDoubleOpts != null) {
                Stream(data, promptDoubleOpts);
                return;
            }

            PromptDistanceOptions promptDistanceOpts = opts as PromptDistanceOptions;
            if (promptDistanceOpts != null) {
                Stream(data, promptDistanceOpts);
                return;
            }
        }

        private void
        Stream(ArrayList data, PromptIntegerOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptIntegerOptions)));

            data.Add(new Snoop.Data.Int("Default value", opts.DefaultValue));
            data.Add(new Snoop.Data.Int("Lower limit", opts.LowerLimit));
            data.Add(new Snoop.Data.Int("Upper limit", opts.UpperLimit));
        }

        private void
        Stream(ArrayList data, PromptDoubleOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptDoubleOptions)));

            data.Add(new Snoop.Data.Double("Default value", opts.DefaultValue));
        }

        private void
        Stream(ArrayList data, PromptDistanceOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptDistanceOptions)));

            data.Add(new Snoop.Data.Point3d("Base point", opts.BasePoint));
            data.Add(new Snoop.Data.Double("Default value", opts.DefaultValue));
            data.Add(new Snoop.Data.Bool("Only2d", opts.Only2d));
            data.Add(new Snoop.Data.Bool("Use base point", opts.UseBasePoint));
            data.Add(new Snoop.Data.Bool("Use dashed line", opts.UseDashedLine));          
        }

        private void
        Stream(ArrayList data, PromptEntityOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptEntityOptions)));

            data.Add(new Snoop.Data.Bool("Allow none", opts.AllowNone));
            data.Add(new Snoop.Data.Bool("Allow object on locked layer", opts.AllowObjectOnLockedLayer));
        }

        private void
        Stream(ArrayList data, PromptAngleOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptAngleOptions)));

            data.Add(new Snoop.Data.Bool("Allow arbitrary input", opts.AllowArbitraryInput));
            data.Add(new Snoop.Data.Bool("Allow none", opts.AllowNone));
            data.Add(new Snoop.Data.Bool("Allow zero", opts.AllowZero));
            data.Add(new Snoop.Data.Point3d("Base point", opts.BasePoint));
            data.Add(new Snoop.Data.Double("Default value", opts.DefaultValue));
            data.Add(new Snoop.Data.Bool("Use angle base", opts.UseAngleBase));
            data.Add(new Snoop.Data.Bool("Use base point", opts.UseBasePoint));
            data.Add(new Snoop.Data.Bool("Use dashed line", opts.UseDashedLine));
            data.Add(new Snoop.Data.Bool("Use default value", opts.UseDefaultValue));           
        }

        private void
        Stream(ArrayList data, PromptDragOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptDragOptions)));

            data.Add(new Snoop.Data.Bool("Allow arbitrary input", opts.AllowArbitraryInput));
            data.Add(new Snoop.Data.Bool("Allow none", opts.AllowNone));
            data.Add(new Snoop.Data.Object("Callback", opts.Callback));
            data.Add(new Snoop.Data.String("Cursor", opts.Cursor.ToString()));
            data.Add(new Snoop.Data.Object("Selection", opts.Selection));          
        }

        private void
        Stream(ArrayList data, PromptStringOptionsEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptStringOptionsEventArgs)));

            data.Add(new Snoop.Data.Object("Options", args.Options));
        }

        private void
        Stream(ArrayList data, PromptSelectionOptionsEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptSelectionOptionsEventArgs)));

            data.Add(new Snoop.Data.Object("Options", args.Options));
            data.Add(new Snoop.Data.Object("Filter", args.Filter));
        }

        private void
        Stream(ArrayList data, PromptSelectionOptions opts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptSelectionOptions)));

            data.Add(new Snoop.Data.Bool("Allow duplicates", opts.AllowDuplicates));
            data.Add(new Snoop.Data.Bool("Allow sub selections", opts.AllowSubSelections));
            data.Add(new Snoop.Data.Bool("Force sub selections", opts.ForceSubSelections));
            data.Add(new Snoop.Data.Object("Keywords", opts.Keywords));
            data.Add(new Snoop.Data.String("Message for adding", opts.MessageForAdding));
            data.Add(new Snoop.Data.String("Message for removal", opts.MessageForRemoval));
            data.Add(new Snoop.Data.Bool("Prepare optional details", opts.PrepareOptionalDetails));
            data.Add(new Snoop.Data.Bool("Reject objects from non-current space", opts.RejectObjectsFromNonCurrentSpace));
            data.Add(new Snoop.Data.Bool("Reject objects on locked layers", opts.RejectObjectsOnLockedLayers));
            data.Add(new Snoop.Data.Bool("Reject paper space viewport", opts.RejectPaperspaceViewport));
            data.Add(new Snoop.Data.Bool("Select everything in aperture", opts.SelectEverythingInAperture));
            data.Add(new Snoop.Data.Bool("Single only", opts.SingleOnly));
            data.Add(new Snoop.Data.Bool("Single pick in space", opts.SinglePickInSpace));
        }

        private void
        Stream(ArrayList data, PromptPointOptionsEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptPointOptionsEventArgs)));

            data.Add(new Snoop.Data.Object("Options", args.Options));
        }

        private void
        Stream(ArrayList data, PromptNestedEntityResultEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptNestedEntityResultEventArgs)));

            data.Add(new Snoop.Data.Object("Result", args.Result));
        }

        private void
        Stream(ArrayList data, PromptResult res)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptResult)));

            data.Add(new Snoop.Data.String("Status", res.Status.ToString()));
            data.Add(new Snoop.Data.String("String result", res.StringResult));
            data.Add(new Snoop.Data.String("ToString", res.ToString()));
            
                // just add the values directly
            PromptIntegerResult prResInt = res as PromptIntegerResult;
            if (prResInt != null) {
                data.Add(new Snoop.Data.ClassSeparator(typeof(PromptIntegerResult)));
                data.Add(new Snoop.Data.Int("Value", prResInt.Value));
                return;
            }
                // just add the values directly
            PromptDoubleResult prResDbl = res as PromptDoubleResult;
            if (prResDbl != null) {
                data.Add(new Snoop.Data.ClassSeparator(typeof(PromptDoubleResult)));
                data.Add(new Snoop.Data.Double("Value", prResDbl.Value));
                return;
            }
                // just add the values directly
            PromptPointResult prResPt = res as PromptPointResult;
            if (prResPt != null) {
                data.Add(new Snoop.Data.ClassSeparator(typeof(PromptPointResult)));
                data.Add(new Snoop.Data.Point3d("Value", prResPt.Value));
                return;
            }
                // branch to sub-classes
            PromptEntityResult promptEntRes = res as PromptEntityResult;
            if (promptEntRes != null) {
                Stream(data, promptEntRes);
                return;
            }
        }

        private void
        Stream(ArrayList data, PromptEntityResult res)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptEntityResult)));

            data.Add(new Snoop.Data.ObjectId("Object ID", res.ObjectId));
            data.Add(new Snoop.Data.Point3d("Picked point", res.PickedPoint));

            PromptNestedEntityResult promptNestedEntRes = res as PromptNestedEntityResult;
            if (promptNestedEntRes != null) {
                Stream(data, promptNestedEntRes);
                return;
            }
        }

        private void
        Stream(ArrayList data, PromptNestedEntityResult res)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptNestedEntityResult)));

            data.Add(new Snoop.Data.Object("Transform", res.Transform));
            data.Add(new Snoop.Data.ObjectIdCollection("Containers", res.GetContainers()));
        }

        private void
        Stream(ArrayList data, PromptKeywordOptionsEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptKeywordOptionsEventArgs)));

            data.Add(new Snoop.Data.Object("Options", args.Options));
        }

        private void
        Stream(ArrayList data, PromptIntegerOptionsEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptIntegerOptionsEventArgs)));

            data.Add(new Snoop.Data.Object("Options", args.Options));
        }

        private void
        Stream(ArrayList data, PromptEntityOptionsEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptEntityOptionsEventArgs)));

            data.Add(new Snoop.Data.Object("Options", args.Options));
        }

        private void
        Stream(ArrayList data, PromptDoubleOptionsEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptDoubleOptionsEventArgs)));

            data.Add(new Snoop.Data.Object("Options", args.Options));
        }

        private void
        Stream(ArrayList data, PromptSelectionResult prRes)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptSelectionResult)));
            
            data.Add(new Snoop.Data.String("Status", prRes.Status.ToString()));
            data.Add(new Snoop.Data.Object("Value", prRes.Value));
            
        }

        private void
        Stream(ArrayList data, SelectionSet selSet)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SelectionSet)));
            
            data.Add(new Snoop.Data.ObjectIdCollection("Object IDs", selSet.GetObjectIds()));
            data.Add(new Snoop.Data.Enumerable("Selected objects", selSet));
        }

        private void
        Stream(ArrayList data, SelectedObject selObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SelectedObject)));
            
            data.Add(new Snoop.Data.String("Selection method", selObj.SelectionMethod.ToString()));
            data.Add(new Snoop.Data.ObjectId("Object ID", selObj.ObjectId));
            //data.Add(new Snoop.Data.Int("Graphics system marker", selObj.GraphicsSystemMarker));  // TBD: Fix JMA
            data.Add(new Snoop.Data.Object("Optional details", selObj.OptionalDetails));
            data.Add(new Snoop.Data.Enumerable("Sub-entities", selObj.GetSubentities()));

            CrossingOrWindowSelectedObject crossOrWindowSelObj = selObj as CrossingOrWindowSelectedObject;
            if (crossOrWindowSelObj != null) {
                Stream(data, crossOrWindowSelObj);
                return;
            }

            FenceSelectedObject fenceSelObj = selObj as FenceSelectedObject;
            if (fenceSelObj != null) {
                Stream(data, fenceSelObj);
                return;
            }

            PickPointSelectedObject pickPtSelObj = selObj as PickPointSelectedObject;
            if (pickPtSelObj != null) {
                Stream(data, pickPtSelObj);
                return;
            }

        }

        private void
        Stream(ArrayList data, CrossingOrWindowSelectedObject selObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CrossingOrWindowSelectedObject)));
            
            data.Add(new Snoop.Data.Enumerable("Pick points", selObj.GetPickPoints()));
        }

        private void
        Stream(ArrayList data, FenceSelectedObject selObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FenceSelectedObject)));
            
            data.Add(new Snoop.Data.Enumerable("Intersection points", selObj.GetIntersectionPoints()));
        }

        private void
        Stream(ArrayList data, PickPointSelectedObject selObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PickPointSelectedObject)));
            
            data.Add(new Snoop.Data.Object("Pick point", selObj.PickPoint));
        }

        private void
       Stream(ArrayList data, SelectedSubObject selSubObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SelectedSubObject)));

            data.Add(new Snoop.Data.String("Selection method", selSubObj.SelectionMethod.ToString()));
            data.Add(new Snoop.Data.Object("Full sub-entity path", selSubObj.FullSubentityPath));
            //data.Add(new Snoop.Data.Int("Graphics system marker", selSubObj.GraphicsSystemMarker));   // TBD: Fix JMA
            data.Add(new Snoop.Data.Object("Optional details", selSubObj.OptionalDetails));

            CrossingOrWindowSelectedSubObject crossOrWindowSelSubObj = selSubObj as CrossingOrWindowSelectedSubObject;
            if (crossOrWindowSelSubObj != null) {
                Stream(data, crossOrWindowSelSubObj);
                return;
            }

            FenceSelectedSubObject fenceSelSubObj = selSubObj as FenceSelectedSubObject;
            if (fenceSelSubObj != null) {
                Stream(data, fenceSelSubObj);
                return;
            }

            PickPointSelectedSubObject pickPtSelSubObj = selSubObj as PickPointSelectedSubObject;
            if (pickPtSelSubObj != null) {
                Stream(data, pickPtSelSubObj);
                return;
            }


        }

        private void
        Stream(ArrayList data, CrossingOrWindowSelectedSubObject selObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(CrossingOrWindowSelectedSubObject)));

            data.Add(new Snoop.Data.Enumerable("Pick points", selObj.GetPickPoints()));
        }

        private void
        Stream(ArrayList data, FenceSelectedSubObject selObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(FenceSelectedSubObject)));

            data.Add(new Snoop.Data.Enumerable("Intersection points", selObj.GetIntersectionPoints()));
        }

        private void
        Stream(ArrayList data, PickPointSelectedSubObject selObj)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PickPointSelectedSubObject)));

            data.Add(new Snoop.Data.Object("Pick point", selObj.PickPoint));
        }

        private void
        Stream(ArrayList data, DraggingEndedEventArgs dragEndArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(DraggingEndedEventArgs)));

            data.Add(new Snoop.Data.Vector3d("Offset", dragEndArgs.Offset));
            data.Add(new Snoop.Data.Point3d("Pick point", dragEndArgs.PickPoint));
            data.Add(new Snoop.Data.String("Status", dragEndArgs.Status.ToString()));
        }

        private void
        Stream(ArrayList data, SelectionDetails selDetails)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(SelectionDetails)));

            data.Add(new Snoop.Data.Object("Transform", selDetails.Transform));
            data.Add(new Snoop.Data.ObjectIdCollection("Containers", selDetails.GetContainers()));
        }

        private void
        Stream(ArrayList data, InputPointContext inputPtCntxt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(InputPointContext)));

            data.Add(new Snoop.Data.Point3d("Cartesian snapped point", inputPtCntxt.CartesianSnappedPoint));
            data.Add(new Snoop.Data.Point3d("Computed point", inputPtCntxt.ComputedPoint));
            data.Add(new Snoop.Data.Object("Document", inputPtCntxt.Document));
            data.Add(new Snoop.Data.Object("Draw context", inputPtCntxt.DrawContext));
            data.Add(new Snoop.Data.Point3d("Gripped point", inputPtCntxt.GrippedPoint));
            data.Add(new Snoop.Data.String("History", inputPtCntxt.History.ToString()));
            data.Add(new Snoop.Data.Point3d("Last point", inputPtCntxt.LastPoint));
            data.Add(new Snoop.Data.Object("Object snap overrides", inputPtCntxt.ObjectSnapOverrides.ToString()));
            data.Add(new Snoop.Data.Point3d("Object snapped point", inputPtCntxt.ObjectSnappedPoint));
            data.Add(new Snoop.Data.Bool("Point computed", inputPtCntxt.PointComputed));
            data.Add(new Snoop.Data.Point3d("Raw point", inputPtCntxt.RawPoint));
            data.Add(new Snoop.Data.String("Tooltip text", inputPtCntxt.ToolTipText));            
        }

        private void
        Stream(ArrayList data, JigPromptOptions jigPromptOpts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(JigPromptOptions)));

            data.Add(new Snoop.Data.String("Cursor", jigPromptOpts.Cursor.ToString()));
            data.Add(new Snoop.Data.String("User input controls", jigPromptOpts.UserInputControls.ToString()));

            JigPromptGeometryOptions jigPromptGeoOpts = jigPromptOpts as JigPromptGeometryOptions;
            if (jigPromptGeoOpts != null) {
                Stream(data, jigPromptGeoOpts);
                return;
            }

            JigPromptStringOptions jigPromptStringOpts = jigPromptOpts as JigPromptStringOptions;
            if (jigPromptStringOpts != null) {
                Stream(data, jigPromptStringOpts);
                return;
            }
        }

        private void
        Stream(ArrayList data, JigPromptGeometryOptions jigPromptGeoOpts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(JigPromptGeometryOptions)));

            data.Add(new Snoop.Data.Point3d("Base point", jigPromptGeoOpts.BasePoint));
            data.Add(new Snoop.Data.Bool("Use base point", jigPromptGeoOpts.UseBasePoint));

            JigPromptAngleOptions jigPromptAngleOpts = jigPromptGeoOpts as JigPromptAngleOptions;
            if (jigPromptAngleOpts != null) {
                Stream(data, jigPromptAngleOpts);
                return;
            }

            JigPromptDistanceOptions jigPromptDistanceOpts = jigPromptGeoOpts as JigPromptDistanceOptions;
            if (jigPromptDistanceOpts != null) {
                Stream(data, jigPromptDistanceOpts);
                return;
            }

            JigPromptPointOptions jigPromptPointOpts = jigPromptGeoOpts as JigPromptPointOptions;
            if (jigPromptPointOpts != null) {
                Stream(data, jigPromptPointOpts);
                return;
            }
        }

        private void
        Stream(ArrayList data, JigPromptAngleOptions jigPromptAngleOpts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(JigPromptAngleOptions)));

            data.Add(new Snoop.Data.Double("Default value", jigPromptAngleOpts.DefaultValue));          
        }

        private void
        Stream(ArrayList data, JigPromptDistanceOptions jigPromptDistanceOpts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(JigPromptDistanceOptions)));

            data.Add(new Snoop.Data.Double("Default value", jigPromptDistanceOpts.DefaultValue));
        }

        private void
        Stream(ArrayList data, JigPromptPointOptions jigPromptPointOpts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(JigPromptPointOptions)));

            data.Add(new Snoop.Data.Point3d("Default value", jigPromptPointOpts.DefaultValue));
        }


        private void
        Stream(ArrayList data, JigPromptStringOptions jigPromptStringOpts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(JigPromptStringOptions)));

            data.Add(new Snoop.Data.String("Default value", jigPromptStringOpts.DefaultValue));           
        }

        private void
        Stream(ArrayList data, Keyword keyword)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Keyword)));

            data.Add(new Snoop.Data.String("Display name", keyword.DisplayName));
            data.Add(new Snoop.Data.Bool("Enabled", keyword.Enabled));
            data.Add(new Snoop.Data.String("Global name", keyword.GlobalName));
            data.Add(new Snoop.Data.Bool("Is read only", keyword.IsReadOnly));
            data.Add(new Snoop.Data.String("Local name", keyword.LocalName));
            data.Add(new Snoop.Data.Bool("Visible", keyword.Visible));
        }

        private void
        Stream(ArrayList data, PointFilterEventArgs pointFilterArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PointFilterEventArgs)));

            data.Add(new Snoop.Data.Bool("Call next", pointFilterArgs.CallNext));
            data.Add(new Snoop.Data.Object("Context", pointFilterArgs.Context));
            data.Add(new Snoop.Data.Object("Result", pointFilterArgs.Result));
        }

        private void
        Stream(ArrayList data, PointFilterResult pointFilterRes)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PointFilterResult)));

            data.Add(new Snoop.Data.Bool("Display object snap glyph", pointFilterRes.DisplayObjectSnapGlyph));
            data.Add(new Snoop.Data.Point3d("New point", pointFilterRes.NewPoint));
            data.Add(new Snoop.Data.Bool("Retry", pointFilterRes.Retry));
            data.Add(new Snoop.Data.String("Toolt tip text", pointFilterRes.ToolTipText));
        }

        private void
        Stream(ArrayList data, PointMonitorEventArgs pointMonitorArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PointMonitorEventArgs)));

            data.Add(new Snoop.Data.Object("Context", pointMonitorArgs.Context));            
        }

        private void
        Stream(ArrayList data, PromptAngleOptionsEventArgs promptAngleOptsArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptAngleOptionsEventArgs)));

            data.Add(new Snoop.Data.Object("Options", promptAngleOptsArgs.Options));
        }

        private void
        Stream(ArrayList data, PromptDistanceOptionsEventArgs promptDistanceOptsArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptDistanceOptionsEventArgs)));

            data.Add(new Snoop.Data.Object("Options", promptDistanceOptsArgs.Options));
        }

        private void
        Stream(ArrayList data, PromptDoubleResultEventArgs promptDoubleResArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptDoubleResultEventArgs)));

            data.Add(new Snoop.Data.Object("Result", promptDoubleResArgs.Result));
        }

        private void
        Stream(ArrayList data, PromptFileOptions promptFileOpts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptFileOptions)));

            data.Add(new Snoop.Data.Bool("AllowUrls", promptFileOpts.AllowUrls));
            data.Add(new Snoop.Data.String("Dialog caption", promptFileOpts.DialogCaption));
            data.Add(new Snoop.Data.String("Dialog name", promptFileOpts.DialogName));
            data.Add(new Snoop.Data.String("Filter", promptFileOpts.Filter));
            data.Add(new Snoop.Data.Int("Filter index", promptFileOpts.FilterIndex));
            data.Add(new Snoop.Data.String("Initial directory", promptFileOpts.InitialDirectory));
            data.Add(new Snoop.Data.String("Initial file name", promptFileOpts.InitialFileName));
            data.Add(new Snoop.Data.String("Message", promptFileOpts.Message));
            data.Add(new Snoop.Data.Bool("Prefer command line", promptFileOpts.PreferCommandLine));

            PromptOpenFileOptions promptOpenFileOpts = promptFileOpts as PromptOpenFileOptions;
            if (promptOpenFileOpts != null) {
                Stream(data, promptOpenFileOpts);
                return;
            }

            PromptSaveFileOptions promptSaveFileOpts = promptFileOpts as PromptSaveFileOptions;
            if (promptSaveFileOpts != null) {
                Stream(data, promptSaveFileOpts);
                return;
            }
        }

        private void
        Stream(ArrayList data, PromptOpenFileOptions promptOpenFileOpts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptOpenFileOptions)));

            data.Add(new Snoop.Data.Bool("Search path", promptOpenFileOpts.SearchPath));
            data.Add(new Snoop.Data.Bool("Transfer remote files", promptOpenFileOpts.TransferRemoteFiles));
        }

        private void
        Stream(ArrayList data, PromptSaveFileOptions promptSaveFileOpts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptSaveFileOptions)));

            data.Add(new Snoop.Data.Bool("Derive initial filename from drawing name", promptSaveFileOpts.DeriveInitialFilenameFromDrawingName));
            data.Add(new Snoop.Data.Bool("Display save options menu item", promptSaveFileOpts.DisplaySaveOptionsMenuItem));
            data.Add(new Snoop.Data.Bool("Force overwrite warning for scripts and lisp", promptSaveFileOpts.ForceOverwriteWarningForScriptsAndLisp));
        }

        private void
        Stream(ArrayList data, PromptForEntityEndingEventArgs promptForEntEndArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptForEntityEndingEventArgs)));

            data.Add(new Snoop.Data.Object("Result", promptForEntEndArgs.Result));            
        }

        private void
        Stream(ArrayList data, PromptForSelectionEndingEventArgs promptForSelEndArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptForSelectionEndingEventArgs)));

            data.Add(new Snoop.Data.String("Flags", promptForSelEndArgs.Flags.ToString()));
            data.Add(new Snoop.Data.Object("Selection", promptForSelEndArgs.Selection));
        }

        private void
        Stream(ArrayList data, PromptNestedEntityOptions promptNestEntOpts)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptNestedEntityOptions)));

            data.Add(new Snoop.Data.Bool("AllowNone", promptNestEntOpts.AllowNone));
            data.Add(new Snoop.Data.Point3d("Non interactive pick point", promptNestEntOpts.NonInteractivePickPoint));
            data.Add(new Snoop.Data.Bool("Use non interactive pick point", promptNestEntOpts.UseNonInteractivePickPoint));
        }

        private void
        Stream(ArrayList data, PromptNestedEntityOptionsEventArgs promptNestEntOptsArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptNestedEntityOptionsEventArgs)));

            data.Add(new Snoop.Data.Object("Options", promptNestEntOptsArgs.Options));          
        }

        private void
        Stream(ArrayList data, PromptSelectionResultEventArgs promptSelResArgs)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PromptSelectionResultEventArgs)));

            data.Add(new Snoop.Data.Object("Result", promptSelResArgs.Result));
        }  

        private void
        Stream(ArrayList data, PickPointDescriptor pickPt)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(PickPointDescriptor)));
            
            data.Add(new Snoop.Data.String("Kind", pickPt.Kind.ToString()));
            data.Add(new Snoop.Data.Vector3d("Direction", pickPt.Direction));
            data.Add(new Snoop.Data.Point3d("Point on line", pickPt.PointOnLine));
        }       
    }
}
