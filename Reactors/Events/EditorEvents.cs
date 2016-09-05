
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
using Autodesk.AutoCAD.EditorInput;

namespace MgdDbg.Reactors.Events {

    public class EditorEvents : EventsBase {

        public bool m_suppressDuringDrag = true;

        public
        EditorEvents()
        {
        }

        protected override void
        EnableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nEditor Events Turned On ...\n");

            DocumentCollection docs = Application.DocumentManager;
            foreach (Document doc in docs) {
                EnableEvents(doc.Editor);
            }
        }

        public void
        EnableEvents(Editor ed)
        {
            ed.EnteringQuiescentState += new EventHandler(event_EnteringQuiescentState);
            ed.LeavingQuiescentState += new EventHandler(event_LeavingQuiescentState);

            ed.PointFilter += new PointFilterEventHandler(event_PointFilter);
            ed.PointMonitor += new PointMonitorEventHandler(event_PointMonitor);

            ed.PromptingForAngle += new PromptAngleOptionsEventHandler(event_PromptingForAngle);
            ed.PromptedForAngle += new PromptDoubleResultEventHandler(event_PromptedForAngle);

            ed.PromptingForCorner += new PromptPointOptionsEventHandler(event_PromptingForCorner);
            ed.PromptedForCorner += new PromptPointResultEventHandler(event_PromptedForCorner);
            
            ed.PromptingForDistance += new PromptDistanceOptionsEventHandler(event_PromptingForDistance);
            ed.PromptedForDistance += new PromptDoubleResultEventHandler(event_PromptedForDistance);

            ed.PromptingForDouble += new PromptDoubleOptionsEventHandler(event_PromptingForDouble);
            ed.PromptedForDouble += new PromptDoubleResultEventHandler(event_PromptedForDouble);

            ed.PromptingForEntity += new PromptEntityOptionsEventHandler(event_PromptingForEntity);
            ed.PromptedForEntity += new PromptEntityResultEventHandler(event_PromptedForEntity);
            ed.PromptForEntityEnding += new PromptForEntityEndingEventHandler(event_PromptForEntityEnding);

            ed.PromptingForInteger += new PromptIntegerOptionsEventHandler(event_PromptingForInteger);
            ed.PromptedForInteger += new PromptIntegerResultEventHandler(event_PromptedForInteger);

            ed.PromptingForKeyword += new PromptKeywordOptionsEventHandler(event_PromptingForKeyword);
            ed.PromptedForKeyword += new PromptStringResultEventHandler(event_PromptedForKeyword);

            ed.PromptingForNestedEntity += new PromptNestedEntityOptionsEventHandler(event_PromptingForNestedEntity);
            ed.PromptedForNestedEntity += new PromptNestedEntityResultEventHandler(event_PromptedForNestedEntity);

            ed.PromptingForPoint += new PromptPointOptionsEventHandler(event_PromptingForPoint);
            ed.PromptedForPoint += new PromptPointResultEventHandler(event_PromptedForPoint);

            ed.PromptingForSelection += new PromptSelectionOptionsEventHandler(event_PromptingForSelection);
            ed.PromptedForSelection += new PromptSelectionResultEventHandler(event_PromptedForSelection);
            ed.PromptForSelectionEnding += new PromptForSelectionEndingEventHandler(event_PromptForSelectionEnding);
            ed.SelectionAdded += new SelectionAddedEventHandler(event_SelectionAdded);
            ed.SelectionRemoved += new SelectionRemovedEventHandler(event_SelectionRemoved);

            ed.PromptingForString += new PromptStringOptionsEventHandler(event_PromptingForString);
            ed.PromptedForString += new PromptStringResultEventHandler(event_PromptedForString);
        }

        protected override void
        DisableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nEditor Events Turned Off ...\n");

            DocumentCollection docs = Application.DocumentManager;
            foreach (Document doc in docs) {
                DisableEvents(doc.Editor);
            }
        }

        public void
        DisableEvents(Editor ed)
        {
            ed.EnteringQuiescentState -= new EventHandler(event_EnteringQuiescentState);
            ed.LeavingQuiescentState -= new EventHandler(event_LeavingQuiescentState);

            ed.PointFilter -= new PointFilterEventHandler(event_PointFilter);
            ed.PointMonitor -= new PointMonitorEventHandler(event_PointMonitor);

            ed.PromptingForAngle -= new PromptAngleOptionsEventHandler(event_PromptingForAngle);
            ed.PromptedForAngle -= new PromptDoubleResultEventHandler(event_PromptedForAngle);

            ed.PromptingForCorner -= new PromptPointOptionsEventHandler(event_PromptingForCorner);
            ed.PromptedForCorner -= new PromptPointResultEventHandler(event_PromptedForCorner);

            ed.PromptingForDistance -= new PromptDistanceOptionsEventHandler(event_PromptingForDistance);
            ed.PromptedForDistance -= new PromptDoubleResultEventHandler(event_PromptedForDistance);

            ed.PromptingForDouble -= new PromptDoubleOptionsEventHandler(event_PromptingForDouble);
            ed.PromptedForDouble -= new PromptDoubleResultEventHandler(event_PromptedForDouble);

            ed.PromptingForEntity -= new PromptEntityOptionsEventHandler(event_PromptingForEntity);
            ed.PromptedForEntity -= new PromptEntityResultEventHandler(event_PromptedForEntity);
            ed.PromptForEntityEnding -= new PromptForEntityEndingEventHandler(event_PromptForEntityEnding);

            ed.PromptingForInteger -= new PromptIntegerOptionsEventHandler(event_PromptingForInteger);
            ed.PromptedForInteger -= new PromptIntegerResultEventHandler(event_PromptedForInteger);

            ed.PromptingForKeyword -= new PromptKeywordOptionsEventHandler(event_PromptingForKeyword);
            ed.PromptedForKeyword -= new PromptStringResultEventHandler(event_PromptedForKeyword);

            ed.PromptingForNestedEntity -= new PromptNestedEntityOptionsEventHandler(event_PromptingForNestedEntity);
            ed.PromptedForNestedEntity -= new PromptNestedEntityResultEventHandler(event_PromptedForNestedEntity);

            ed.PromptingForPoint -= new PromptPointOptionsEventHandler(event_PromptingForPoint);
            ed.PromptedForPoint -= new PromptPointResultEventHandler(event_PromptedForPoint);

            ed.PromptingForSelection -= new PromptSelectionOptionsEventHandler(event_PromptingForSelection);
            ed.PromptedForSelection -= new PromptSelectionResultEventHandler(event_PromptedForSelection);
            ed.PromptForSelectionEnding -= new PromptForSelectionEndingEventHandler(event_PromptForSelectionEnding);
            ed.SelectionAdded -= new SelectionAddedEventHandler(event_SelectionAdded);
            ed.SelectionRemoved -= new SelectionRemovedEventHandler(event_SelectionRemoved);

            ed.PromptingForString -= new PromptStringOptionsEventHandler(event_PromptingForString);
            ed.PromptedForString -= new PromptStringResultEventHandler(event_PromptedForString);
        }

        private void
        event_SelectionRemoved(object sender, SelectionRemovedEventArgs e)
        {
            PrintEventMessage(sender, "Selection Removed");
            /*if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "SelectionRemoved";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_SelectionAdded(object sender, SelectionAddedEventArgs e)
        {
            PrintEventMessage(sender, "Selection Added");
            /*if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "SelectionAdded";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForString(object sender, PromptStringOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For String");
            /*if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForString";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForSelection(object sender, PromptSelectionOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For Selection");
            /*if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForSelection";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForPoint(object sender, PromptPointOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For Point");
            /*if (PrintDetails(sender)) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForPoint";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForNestedEntity(object sender, PromptNestedEntityOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For Nested Entity");
            /*if (PrintDetails(sender)) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForNestedEntity";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForKeyword(object sender, PromptKeywordOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For Keyword");
            /*if (PrintDetails(sender)) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForKeyword";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForInteger(object sender, PromptIntegerOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For Integer");
            /*if (PrintDetails(sender)) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForInteger";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForEntity(object sender, PromptEntityOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For Entity");
            /*if (PrintDetails(sender)) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForEntity";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForDouble(object sender, PromptDoubleOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For Double");
            /*if (PrintDetails(sender)) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForDouble";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForDistance(object sender, PromptDistanceOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For Distance");
            /*if (PrintDetails(sender)) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForDistance";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForCorner(object sender, PromptPointOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For Corner");
            /*if (PrintDetails(sender)) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForCorner";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptingForAngle(object sender, PromptAngleOptionsEventArgs e)
        {
            PrintEventMessage(sender, "Prompting For Angle");
            /*if (PrintDetails(sender)) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "PromptingForAngle";
                dbox.ShowDialog();
            }*/
        }

        private void
        event_PromptForSelectionEnding(object sender, PromptForSelectionEndingEventArgs e)
        {
            PrintEventMessage(sender, "Prompt For Selection Ending");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptForEntityEnding(object sender, PromptForEntityEndingEventArgs e)
        {
            PrintEventMessage(sender, "Prompt For Entity Ending");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForString(object sender, PromptStringResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For String");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForSelection(object sender, PromptSelectionResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For Selection");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForPoint(object sender, PromptPointResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For Point");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForNestedEntity(object sender, PromptNestedEntityResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For Nested Entity");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForKeyword(object sender, PromptStringResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For Keyword");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForInteger(object sender, PromptIntegerResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For Integer");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForEntity(object sender, PromptEntityResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For Entity");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForDouble(object sender, PromptDoubleResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For Double");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForDistance(object sender, PromptDoubleResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For Distance");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForCorner(object sender, PromptPointResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For Corner");
            if (m_showDetails) {
            }
        }

        private void
        event_PromptedForAngle(object sender, PromptDoubleResultEventArgs e)
        {
            PrintEventMessage(sender, "Prompted For Angle");
            if (m_showDetails) {
            }
        }

        private void
        event_PointMonitor(object sender, PointMonitorEventArgs e)
        {
            PrintEventMessage(sender, "Point Monitor");
            if (m_showDetails) {
            }
        }

        private void
        event_PointFilter(object sender, PointFilterEventArgs e)
        {
            PrintEventMessage(sender, "Point Filter");
            if (m_showDetails) {
            }
        }

        private void
        event_LeavingQuiescentState(object sender, EventArgs e)
        {
            PrintEventMessage(sender, "Leaving Quiescent State");
       }

        private void
        event_EnteringQuiescentState(object sender, EventArgs e)
        {
            PrintEventMessage(sender, "Entering Quiescent State");
        }

        #region Print Abstraction

        private void
        PrintEventMessage(object obj, string eventStr)
        {
            //Editor ed = (Editor)obj;
            //if (!(ed.IsDragging && m_suppressDuringDrag)) {
                string printString = string.Format("\n[Editor Event] : {0,-20} ", eventStr);
                Utils.AcadUi.PrintToCmdLine(printString);
            //}
        }

        private bool
        PrintDetails(object obj)
        {
                // TBD: getting so many messages during drag that they become worthless.  On C++ side
                // there was a beginDragSequence() event that allowed us to filter the extras out.  Can't
                // find a way to do that on .NET side (jma - 09/08/06)
            Editor ed = (Editor)obj;
            if (ed.IsDragging) {
                    // print the first drag message, but suppress the rest until the
                    // end of the drag sequence
                //if (m_dragJustStarted) {
                //    m_dragJustStarted = false;
                //    return true;
                return false;
            }
            else {
                return m_showDetails;
            }
        }

        #endregion
    }
}
