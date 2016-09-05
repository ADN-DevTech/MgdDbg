
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

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;

namespace MgdDbg.Reactors.Events {

    public class DocumentEvents : EventsBase {

        public
        DocumentEvents()
        {
        }

        protected override void
        EnableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nDocument Events Turned On ...\n");

            DocumentCollection docs = Application.DocumentManager;
            foreach (Document doc in docs) {
                EnableEvents(doc);
            }
        }

        public void
        EnableEvents(Document doc)
        {
            doc.BeginDocumentClose += new DocumentBeginCloseEventHandler(event_BeginDocumentClose);
            doc.CloseAborted += new EventHandler(event_CloseAborted);
            doc.CloseWillStart += new EventHandler(event_CloseWillStart);
            doc.CommandCancelled += new CommandEventHandler(event_CommandCancelled);
            doc.CommandEnded += new CommandEventHandler(event_CommandEnded);
            doc.CommandFailed += new CommandEventHandler(event_CommandFailed);
            doc.CommandWillStart += new CommandEventHandler(event_CommandWillStart);
            doc.ImpliedSelectionChanged += new EventHandler(event_ImpliedSelectionChanged);
            doc.LispCancelled += new EventHandler(event_LispCancelled);
            doc.LispEnded += new EventHandler(event_LispEnded);
            doc.LispWillStart += new LispWillStartEventHandler(event_LispWillStart);
            doc.UnknownCommand += new UnknownCommandEventHandler(event_UnknownCommand);
        }

        protected override void
        DisableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nDocument Events Turned Off ...\n");

            DocumentCollection docs = Application.DocumentManager;
            foreach (Document doc in docs) {
                DisableEvents(doc);
            }
        }

        public void
        DisableEvents(Document doc)
        {
            doc.BeginDocumentClose -= new DocumentBeginCloseEventHandler(event_BeginDocumentClose);
            doc.CloseAborted -= new EventHandler(event_CloseAborted);
            doc.CloseWillStart -= new EventHandler(event_CloseWillStart);
            doc.CommandCancelled -= new CommandEventHandler(event_CommandCancelled);
            doc.CommandEnded -= new CommandEventHandler(event_CommandEnded);
            doc.CommandFailed -= new CommandEventHandler(event_CommandFailed);
            doc.CommandWillStart -= new CommandEventHandler(event_CommandWillStart);
            doc.ImpliedSelectionChanged -= new EventHandler(event_ImpliedSelectionChanged);
            doc.LispCancelled -= new EventHandler(event_LispCancelled);
            doc.LispEnded -= new EventHandler(event_LispEnded);
            doc.LispWillStart -= new LispWillStartEventHandler(event_LispWillStart);
            doc.UnknownCommand -= new UnknownCommandEventHandler(event_UnknownCommand);
        }

        private void
        event_BeginDocumentClose(object sender, DocumentBeginCloseEventArgs e)
        {
            PrintEventMessage("Begin Document Close");
        }

        private void
        event_CloseAborted(object sender, EventArgs e)
        {
            PrintEventMessage("Close Aborted");
        }

        private void
        event_CloseWillStart(object sender, EventArgs e)
        {
            PrintEventMessage("Close will Start");
        }

        private void
        event_CommandCancelled(object sender, CommandEventArgs e)
        {
            PrintEventMessage("Command Cancelled", e.GlobalCommandName);
        }

        private void
        event_CommandEnded(object sender, CommandEventArgs e)
        {
            PrintEventMessage("Command Ended", e.GlobalCommandName);
        }

        private void
        event_CommandFailed(object sender, CommandEventArgs e)
        {
            PrintEventMessage("Command Failed", e.GlobalCommandName);
        }

        private void
        event_CommandWillStart(object sender, CommandEventArgs e)
        {
            PrintEventMessage("Command will Start", e.GlobalCommandName);
        }

        private void
        event_ImpliedSelectionChanged(object sender, EventArgs e)
        {
            PrintEventMessage("Implied Selection Changed");
        }

        private void
        event_LispCancelled(object sender, EventArgs e)
        {
            PrintEventMessage("Lisp Cancelled");
        }

        private void
        event_LispEnded(object sender, EventArgs e)
        {
            PrintEventMessage("Lisp ended");
        }

        private void
        event_LispWillStart(object sender, LispWillStartEventArgs e)
        {
            PrintEventMessage("Lisp will Start", e.FirstLine);
        }

        private void
        event_UnknownCommand(object sender, UnknownCommandEventArgs e)
        {
            PrintEventMessage("Unknown Command", e.GlobalCommandName);
        }

        #region Print Abstraction

        private void
        PrintEventMessage(string eventStr)
        {
            string printString = string.Format("\n[Doc Event] : {0,-25}", eventStr);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        private void
        PrintEventMessage(string eventStr, string msg)
        {
            string printString = string.Format("\n[Doc Event] : {0,-25} : {1}", eventStr, msg);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        #endregion

    }
}
