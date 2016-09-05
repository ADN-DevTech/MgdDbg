
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
using System.Collections.Generic;
using System.Text;

using AcDb = Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.Reactors.Events {

    public class LayoutManagerEvents : EventsBase {

        private AcDb.LayoutManager m_lMgr;

        public
        LayoutManagerEvents()
        {
            m_lMgr = null;
        }

        protected override void
        EnableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nLayout Manager Events Turned On ...\n");

            m_lMgr = AcDb.LayoutManager.Current;

            m_lMgr.AbortLayoutCopied += new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_AbortLayoutCopied);
            m_lMgr.AbortLayoutRemoved += new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_AbortLayoutRemoved);
            m_lMgr.AbortLayoutRename += new Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventHandler(event_AbortLayoutRename);
            m_lMgr.LayoutCopied += new Autodesk.AutoCAD.DatabaseServices.LayoutCopiedEventHandler(event_LayoutCopied);
            m_lMgr.LayoutCreated += new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_LayoutCreated);
            m_lMgr.LayoutRemoved += new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_LayoutRemoved);
            m_lMgr.LayoutRenamed += new Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventHandler(event_LayoutRenamed);
            m_lMgr.LayoutsReordered += new EventHandler(event_LayoutsReordered);
            m_lMgr.LayoutSwitched += new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_LayoutSwitched);
            m_lMgr.LayoutToBeCopied += new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_LayoutToBeCopied);
            m_lMgr.LayoutToBeRemoved += new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_LayoutToBeRemoved);
            m_lMgr.LayoutToBeRenamed += new Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventHandler(event_LayoutToBeRenamed);
            m_lMgr.PlotStyleTableChanged += new Autodesk.AutoCAD.DatabaseServices.PlotStyleTableChangedEventHandler(event_PlotStyleTableChanged);
        }

        protected override void
        DisableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nLayout Manager Events Turned Off ...\n");

            m_lMgr.AbortLayoutCopied -= new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_AbortLayoutCopied);
            m_lMgr.AbortLayoutRemoved -= new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_AbortLayoutRemoved);
            m_lMgr.AbortLayoutRename -= new Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventHandler(event_AbortLayoutRename);
            m_lMgr.LayoutCopied -= new Autodesk.AutoCAD.DatabaseServices.LayoutCopiedEventHandler(event_LayoutCopied);
            m_lMgr.LayoutCreated -= new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_LayoutCreated);
            m_lMgr.LayoutRemoved -= new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_LayoutRemoved);
            m_lMgr.LayoutRenamed -= new Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventHandler(event_LayoutRenamed);
            m_lMgr.LayoutsReordered -= new EventHandler(event_LayoutsReordered);
            m_lMgr.LayoutSwitched -= new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_LayoutSwitched);
            m_lMgr.LayoutToBeCopied -= new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_LayoutToBeCopied);
            m_lMgr.LayoutToBeRemoved -= new Autodesk.AutoCAD.DatabaseServices.LayoutEventHandler(event_LayoutToBeRemoved);
            m_lMgr.LayoutToBeRenamed -= new Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventHandler(event_LayoutToBeRenamed);
            m_lMgr.PlotStyleTableChanged -= new Autodesk.AutoCAD.DatabaseServices.PlotStyleTableChangedEventHandler(event_PlotStyleTableChanged);
        }

        private void
        event_PlotStyleTableChanged(object sender, Autodesk.AutoCAD.DatabaseServices.PlotStyleTableChangedEventArgs e)
        {
            PrintReactorMessage("Plot Style Table Changed");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PlotStyleTableChanged";
                dbox.ShowDialog();
            }
        }

        private void
        event_LayoutToBeRenamed(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventArgs e)
        {
            PrintReactorMessage("Layout To Be Renamed");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "LayoutToBeRenamed";
                dbox.ShowDialog();
            }
        }

        private void
        event_LayoutToBeRemoved(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutEventArgs e)
        {
            PrintReactorMessage("Layout To Be Removed");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "LayoutToBeRemoved";
                dbox.ShowDialog();
            }
        }

        private void
        event_LayoutToBeCopied(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutEventArgs e)
        {
            PrintReactorMessage("Layout To Be Copied");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "LayoutToBeCopied";
                dbox.ShowDialog();
            }
        }

        private void
        event_LayoutSwitched(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutEventArgs e)
        {
            PrintReactorMessage("Layout Switched");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "LayoutToBeSwitched";
                dbox.ShowDialog();
            }
        }

        private void
        event_LayoutsReordered(object sender, EventArgs e)
        {
            PrintReactorMessage("Layouts Reordered");
        }

        private void
        event_LayoutRenamed(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventArgs e)
        {
            PrintReactorMessage("Layout Renamed");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "LayoutRenamed";
                dbox.ShowDialog();
            }
        }

        private void
        event_LayoutRemoved(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutEventArgs e)
        {
            PrintReactorMessage("Layout Removed");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "LayoutRemoved";
                dbox.ShowDialog();
            }
        }

        private void
        event_LayoutCreated(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutEventArgs e)
        {
            PrintReactorMessage("Layout Created");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "LayoutCreated";
                dbox.ShowDialog();
            }
        }

        private void
        event_LayoutCopied(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutCopiedEventArgs e)
        {
            PrintReactorMessage("Layout Copied");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "LayoutCopied";
                dbox.ShowDialog();
            }
        }

        private void
        event_AbortLayoutRename(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutRenamedEventArgs e)
        {
            PrintReactorMessage("Abort Layout Rename");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "AbortLayoutRename";
                dbox.ShowDialog();
            }
        }

        private void
        event_AbortLayoutRemoved(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutEventArgs e)
        {
            PrintReactorMessage("Abort Layout Removed");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "AbortLayoutRemoved";
                dbox.ShowDialog();
            }
        }

        private void
        event_AbortLayoutCopied(object sender, Autodesk.AutoCAD.DatabaseServices.LayoutEventArgs e)
        {
            PrintReactorMessage("Abort Layout Copied");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "AbortLayoutCopied";
                dbox.ShowDialog();
            }
        }

        #region Print Abstraction

        private void
        PrintReactorMessage(string eventStr)
        {
            string printString = string.Format("\n[Layout Manager Event] : {0,-20} ", eventStr);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        #endregion
    }
}
