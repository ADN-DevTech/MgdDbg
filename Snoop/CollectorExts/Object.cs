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
using MgdDbg.Snoop.Collectors;

using AcDb = Autodesk.AutoCAD.DatabaseServices;
using AcAp = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Windows;

namespace MgdDbg.Snoop.CollectorExts {

    /// <summary>
    /// This is a Snoop Collector Extension object to collect data from RxObject objects.
    /// </summary>

    public class Object : CollectorExt {

        public
        Object()
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
            System.Type type = e.ObjToSnoop as System.Type;
            if (type != null) {
                Stream(snoopCollector.Data(), type);
                return;
            }

            System.Version ver = e.ObjToSnoop as System.Version;
            if (ver != null) {
                Stream(snoopCollector.Data(), ver);
                return;
            }

            System.Collections.Specialized.StringCollection strCol = e.ObjToSnoop as System.Collections.Specialized.StringCollection;
            if (strCol != null) {
                Stream(snoopCollector.Data(), strCol);
                return;
            }

            AcAp.Document doc = e.ObjToSnoop as AcAp.Document;
            if (doc != null) {
                Stream(snoopCollector.Data(), doc);
                return;
            }

            AcAp.DocumentCollectionEventArgs docCollectEventArgs = e.ObjToSnoop as AcAp.DocumentCollectionEventArgs;
            if (docCollectEventArgs != null) {
                Stream(snoopCollector.Data(), docCollectEventArgs);
                return;
            }

            AcAp.DocumentLockModeWillChangeEventArgs docLockModeEventArgs = e.ObjToSnoop as AcAp.DocumentLockModeWillChangeEventArgs;
            if (docLockModeEventArgs != null) {
                Stream(snoopCollector.Data(), docLockModeEventArgs);
                return;
            }

            AcAp.DocumentLockModeChangedEventArgs docLockModeChangedEventArgs = e.ObjToSnoop as AcAp.DocumentLockModeChangedEventArgs;
            if (docLockModeChangedEventArgs != null) {
                Stream(snoopCollector.Data(), docLockModeChangedEventArgs);
                return;
            }

            AcAp.DocumentLockModeChangeVetoedEventArgs docLockModeVetoedEventArgs = e.ObjToSnoop as AcAp.DocumentLockModeChangeVetoedEventArgs;
            if (docLockModeVetoedEventArgs != null) {
                Stream(snoopCollector.Data(), docLockModeVetoedEventArgs);
                return;
            }

            AcAp.DocumentDestroyedEventArgs docDestroyedEventArgs = e.ObjToSnoop as AcAp.DocumentDestroyedEventArgs;
            if (docDestroyedEventArgs != null) {
                Stream(snoopCollector.Data(), docDestroyedEventArgs);
                return;
            }

            AcAp.DocumentCollection docCollection = e.ObjToSnoop as AcAp.DocumentCollection;
            if (docCollection != null) {
                Stream(snoopCollector.Data(), docCollection);
                return;
            }

            AcAp.XrefFileLock xrefFileLock = e.ObjToSnoop as AcAp.XrefFileLock;
            if (xrefFileLock != null) {
                Stream(snoopCollector.Data(), xrefFileLock);
                return;
            }

            Autodesk.AutoCAD.Windows.Window win = e.ObjToSnoop as Autodesk.AutoCAD.Windows.Window;
            if (win != null) {
                Stream(snoopCollector.Data(), win);
                return;
            }

            Autodesk.AutoCAD.Windows.StatusBar statusBar = e.ObjToSnoop as Autodesk.AutoCAD.Windows.StatusBar;
            if (statusBar != null) {
                Stream(snoopCollector.Data(), statusBar);
                return;
            }

            Autodesk.AutoCAD.Windows.StatusBarItem statusBarItem = e.ObjToSnoop as Autodesk.AutoCAD.Windows.StatusBarItem;
            if (statusBarItem != null) {
                Stream(snoopCollector.Data(), statusBarItem);
                return;
            }
        }


        /// <summary>
        /// Collect data about a class
        /// </summary>
        
        private void
        Stream(ArrayList data, System.Type classes)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(System.Type)));
            data.Add(new Snoop.Data.Class("Name", classes));
            data.Add(new Snoop.Data.String("NameSpace", classes.Namespace));
            if (classes.BaseType == null)
                data.Add(new Snoop.Data.Class("Parent Class", null));
            else
                data.Add(new Snoop.Data.Class("Parent Class", classes.BaseType));
            data.Add(new Snoop.Data.String("Assembly Qualified Name", classes.AssemblyQualifiedName));
            data.Add(new Snoop.Data.String("Attributes", classes.Attributes.ToString()));
            data.Add(new Snoop.Data.String("GUID", classes.GUID.ToString()));
            data.Add(new Snoop.Data.Bool("Is Abstract", classes.IsAbstract));
            data.Add(new Snoop.Data.Bool("Is Array", classes.IsArray));
            data.Add(new Snoop.Data.Bool("Is Primitive", classes.IsPrimitive));
            data.Add(new Snoop.Data.Bool("Is Public", classes.IsPublic));
            data.Add(new Snoop.Data.Bool("Is Sealed", classes.IsSealed));
        }

        private void
        Stream(ArrayList data, System.Version ver)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(System.Version)));

            data.Add(new Snoop.Data.Int("Build", ver.Build));
            data.Add(new Snoop.Data.Int("Major", ver.Major));
            data.Add(new Snoop.Data.Int("Minor", ver.Minor));
            data.Add(new Snoop.Data.Int("Revision", ver.Revision));
            data.Add(new Snoop.Data.Int("Major revision", ver.MajorRevision));
            data.Add(new Snoop.Data.Int("Minor revision", ver.MinorRevision));
            data.Add(new Snoop.Data.String("To string", ver.ToString()));
        }

        /// <summary>
        /// Collect data about a document
        /// </summary>

        private void
        Stream(ArrayList data, AcAp.Document doc)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcAp.Document)));

#if(AC2012)
#else
            data.Add(new Snoop.Data.Object("Acad document", doc.GetAcadDocument()));
#endif
            data.Add(new Snoop.Data.String("Name", doc.Name));
            data.Add(new Snoop.Data.Database("Database", doc.Database));
            data.Add(new Snoop.Data.String("Command in progress", doc.CommandInProgress));
            data.Add(new Snoop.Data.Bool("Is active", doc.IsActive));
            data.Add(new Snoop.Data.Bool("Is read only", doc.IsReadOnly));
            data.Add(new Snoop.Data.Object("Editor", doc.Editor));
            data.Add(new Snoop.Data.Object("Graphics manager", doc.GraphicsManager));
#if(AC2012)
#else
            data.Add(new Snoop.Data.Object("Status bar", doc.GetStatusBar()));
#endif
            data.Add(new Snoop.Data.Object("Transaction manager", doc.TransactionManager));
            data.Add(new Snoop.Data.ObjectCollection("User data", doc.UserData));
            data.Add(new Snoop.Data.Object("Window", doc.Window));
        }

        private void
        Stream(ArrayList data, AcAp.DocumentCollectionEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcAp.DocumentCollectionEventArgs)));

            data.Add(new Snoop.Data.Object("Document", args.Document));
        }

        private void
        Stream(ArrayList data, AcAp.DocumentLockModeWillChangeEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcAp.DocumentLockModeWillChangeEventArgs)));

            data.Add(new Snoop.Data.Object("Document", args.Document));
            data.Add(new Snoop.Data.String("Global command name", args.GlobalCommandName));
            data.Add(new Snoop.Data.String("Current mode", args.CurrentMode.ToString()));
            data.Add(new Snoop.Data.String("My current mode", args.MyCurrentMode.ToString()));
            data.Add(new Snoop.Data.String("My new mode", args.MyNewMode.ToString()));
        }

        private void
        Stream(ArrayList data, AcAp.DocumentLockModeChangedEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcAp.DocumentLockModeChangedEventArgs)));

            data.Add(new Snoop.Data.Object("Document", args.Document));
            data.Add(new Snoop.Data.String("Global command name", args.GlobalCommandName));
            data.Add(new Snoop.Data.String("Current mode", args.CurrentMode.ToString()));
            data.Add(new Snoop.Data.String("My current mode", args.MyCurrentMode.ToString()));
            data.Add(new Snoop.Data.String("My previous mode", args.MyPreviousMode.ToString()));
        }

        private void
        Stream(ArrayList data, AcAp.DocumentLockModeChangeVetoedEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcAp.DocumentLockModeChangeVetoedEventArgs)));

            data.Add(new Snoop.Data.Object("Document", args.Document));
            data.Add(new Snoop.Data.String("Global command name", args.GlobalCommandName));
        }

        private void
        Stream(ArrayList data, AcAp.DocumentDestroyedEventArgs args)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcAp.DocumentDestroyedEventArgs)));

            data.Add(new Snoop.Data.String("File name", args.FileName));
        }

        private void
        Stream(ArrayList data, AcAp.DocumentCollection docCol)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcAp.DocumentCollection)));

            data.Add(new Snoop.Data.Bool("Document activation enabled", docCol.DocumentActivationEnabled));
            data.Add(new Snoop.Data.Bool("Is application context", docCol.IsApplicationContext));
            data.Add(new Snoop.Data.Int("Count", docCol.Count));
            data.Add(new Snoop.Data.Object("MDI active document", docCol.MdiActiveDocument));
            data.Add(new Snoop.Data.Enumerable("Documents", docCol));
        }

        private void
        Stream(ArrayList data, AcAp.XrefFileLock xrefFileLock)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(AcAp.XrefFileLock)));

            data.Add(new Snoop.Data.ObjectIdCollection("Out of sync block Ids", xrefFileLock.OutOfSyncBlockIds));
            data.Add(new Snoop.Data.Int("Xload CtlType", xrefFileLock.XloadCtlType));          
        }

        private void
        Stream(ArrayList data, Autodesk.AutoCAD.Windows.Window win)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.Windows.Window)));

            data.Add(new Snoop.Data.Object("Handle", win.Handle));
#if(AC2012)
#else
            data.Add(new Snoop.Data.Icon("Icon", win.GetIcon()));
            data.Add(new Snoop.Data.Object("Location", win.GetLocation()));
            data.Add(new Snoop.Data.Object("Size", win.GetSize()));
#endif
            data.Add(new Snoop.Data.String("Text", win.Text));
            data.Add(new Snoop.Data.Bool("Visible", win.Visible));
            data.Add(new Snoop.Data.String("Window state", win.WindowState.ToString()));
        }

        private void
        Stream(ArrayList data, Autodesk.AutoCAD.Windows.StatusBar statusBar)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.Windows.StatusBar)));

            data.Add(new Snoop.Data.Enumerable("Panes", statusBar.Panes));
            data.Add(new Snoop.Data.Enumerable("Tray items", statusBar.TrayItems));
        }

        private void
        Stream(ArrayList data, Autodesk.AutoCAD.Windows.StatusBarItem statusBarItem)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.Windows.StatusBarItem)));

            data.Add(new Snoop.Data.Bool("Enabled", statusBarItem.Enabled));
            data.Add(new Snoop.Data.Icon("Icon", statusBarItem.Icon));
            data.Add(new Snoop.Data.String("Tool tip text", statusBarItem.ToolTipText));
            data.Add(new Snoop.Data.Bool("Visible", statusBarItem.Visible));

            Autodesk.AutoCAD.Windows.Pane pane = statusBarItem as Autodesk.AutoCAD.Windows.Pane;
            if (pane != null) {
                Stream(data, pane);
                return;
            }

            Autodesk.AutoCAD.Windows.TrayItem trayItem = statusBarItem as Autodesk.AutoCAD.Windows.TrayItem;
            if (trayItem != null) {
                Stream(data, trayItem);
                return;
            }
        }

        private void
        Stream(ArrayList data, Autodesk.AutoCAD.Windows.Pane pane)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.Windows.Pane)));

            data.Add(new Snoop.Data.String("Text", pane.Text));
            data.Add(new Snoop.Data.String("Style", pane.Style.ToString()));
            data.Add(new Snoop.Data.Int("Maximum width", pane.MaximumWidth));
            data.Add(new Snoop.Data.Int("Minimum width", pane.MinimumWidth));
        }

        private void
        Stream(ArrayList data, Autodesk.AutoCAD.Windows.TrayItem pane)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Autodesk.AutoCAD.Windows.TrayItem)));

            // no data at this level
        }

        private void
        Stream(ArrayList data, System.Collections.Specialized.StringCollection strCol)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(System.Collections.Specialized.StringCollection)));

            data.Add(new Snoop.Data.Bool("Is read-only", strCol.IsReadOnly));

            data.Add(new Snoop.Data.CategorySeparator("Strings"));
            for (int i=0; i<strCol.Count; i++) {
                data.Add(new Snoop.Data.String(string.Format("String [{0:d}]", i), strCol[i]));
            }

        }

    }

}
