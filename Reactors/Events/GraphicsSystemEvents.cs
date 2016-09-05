
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
using System.Collections;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;


namespace MgdDbg.Reactors.Events {

    public class GraphicsSystemEvents : EventsBase {

        public
        GraphicsSystemEvents()
        {
        }

        protected override void
        EnableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nGraphics System Events Turned On ...\n");

            DocumentCollection docs = Application.DocumentManager;

            foreach (Document doc in docs) {
                EnableEvents(doc.GraphicsManager);
            }
        }

        public void
        EnableEvents(Autodesk.AutoCAD.GraphicsSystem.Manager mgr)
        {
            mgr.ConfigWasModified += new Autodesk.AutoCAD.GraphicsSystem.ConfigWasModifiedEventHandler(event_ConfigWasModified);
            mgr.GsToBeUnloaded += new Autodesk.AutoCAD.GraphicsSystem.GsToBeUnloadedEventHandler(event_GsToBeUnloaded);
            mgr.ViewToBeDestroyed += new Autodesk.AutoCAD.GraphicsSystem.ViewToBeDestroyedEventHandler(event_ViewToBeDestroyed);
            mgr.ViewWasCreated += new Autodesk.AutoCAD.GraphicsSystem.ViewWasCreatedEventHandler(event_ViewWasCreated);
        }

        protected override void
        DisableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nGraphics System Events Turned Off ...\n");

            DocumentCollection docs = Application.DocumentManager;

            foreach (Document doc in docs) {
                DisableEvents(doc.GraphicsManager);
            }
        }

        public void
        DisableEvents(Autodesk.AutoCAD.GraphicsSystem.Manager mgr)
        {
            mgr.ConfigWasModified -= new Autodesk.AutoCAD.GraphicsSystem.ConfigWasModifiedEventHandler(event_ConfigWasModified);
            mgr.GsToBeUnloaded -= new Autodesk.AutoCAD.GraphicsSystem.GsToBeUnloadedEventHandler(event_GsToBeUnloaded);
            mgr.ViewToBeDestroyed -= new Autodesk.AutoCAD.GraphicsSystem.ViewToBeDestroyedEventHandler(event_ViewToBeDestroyed);
            mgr.ViewWasCreated -= new Autodesk.AutoCAD.GraphicsSystem.ViewWasCreatedEventHandler(event_ViewWasCreated);
        }


        private void
        event_ViewWasCreated(object sender, Autodesk.AutoCAD.GraphicsSystem.ViewEventArgs e)
        {
#if(AC2012)
#else
            PrintReactorMessage("View Was Created", e.View.ViewportExtents.ToString());
#endif 
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "ViewWasCreated";
                dbox.ShowDialog();
            }
        }

        private void
        event_ViewToBeDestroyed(object sender, Autodesk.AutoCAD.GraphicsSystem.ViewEventArgs e)
        {
#if(AC2012)
#else
            PrintReactorMessage("View To Be Destroyed", e.View.ViewportExtents.ToString());
#endif
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "ViewToBeDestroyed";
                dbox.ShowDialog();
            }
        }

        private void
        event_GsToBeUnloaded(object sender, EventArgs e)
        {
            PrintReactorMessage("Gs To Be Unloaded");
        }

        private void
        event_ConfigWasModified(object sender, EventArgs e)
        {
            PrintReactorMessage("Config Was Modified");
        }

        #region Print Abstraction

        private void
        PrintReactorMessage(string eventStr)
        {
            string printString = string.Format("\n[Graphics System Event] : {0,-20} ", eventStr);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        private void
        PrintReactorMessage(string eventStr, string viewName)
        {
            string printString = string.Format("\n[Graphics System Event] : {0,-20} : {1}", eventStr, viewName);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        #endregion

    }
}
