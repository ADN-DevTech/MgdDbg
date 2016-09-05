
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

using AcPl = Autodesk.AutoCAD.PlottingServices;
using AcApp = Autodesk.AutoCAD.ApplicationServices;

namespace MgdDbg.Reactors.Events {

    public class PlotEvents : EventsBase {

        public
        PlotEvents()
        {
        }

        protected override void
        EnableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nPlot Events Turned On ...\n");

            AcPl.PlotReactorManager plot = new AcPl.PlotReactorManager();

            plot.BeginDocument += new Autodesk.AutoCAD.PlottingServices.BeginDocumentEventHandler(event_BeginDocument);
            plot.BeginPage += new Autodesk.AutoCAD.PlottingServices.BeginPageEventHandler(event_BeginPage);
            plot.BeginPlot += new Autodesk.AutoCAD.PlottingServices.BeginPlotEventHandler(event_BeginPlot);
            plot.EndDocument += new Autodesk.AutoCAD.PlottingServices.EndDocumentEventHandler(event_EndDocument);
            plot.EndPage += new Autodesk.AutoCAD.PlottingServices.EndPageEventHandler(event_EndPage);
            plot.EndPlot += new Autodesk.AutoCAD.PlottingServices.EndPlotEventHandler(event_EndPlot);
            plot.PageCancelled += new Autodesk.AutoCAD.PlottingServices.PageCancelledEventHandler(event_PageCancelled);
            plot.PlotCancelled += new Autodesk.AutoCAD.PlottingServices.PlotCancelledEventHandler(event_PlotCancelled);
        }

        protected override void
        DisableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nPlot Events Turned Off ...\n");

            AcPl.PlotReactorManager plot = new AcPl.PlotReactorManager();

            plot.BeginDocument -= new Autodesk.AutoCAD.PlottingServices.BeginDocumentEventHandler(event_BeginDocument);
            plot.BeginPage -= new Autodesk.AutoCAD.PlottingServices.BeginPageEventHandler(event_BeginPage);
            plot.BeginPlot -= new Autodesk.AutoCAD.PlottingServices.BeginPlotEventHandler(event_BeginPlot);
            plot.EndDocument -= new Autodesk.AutoCAD.PlottingServices.EndDocumentEventHandler(event_EndDocument);
            plot.EndPage -= new Autodesk.AutoCAD.PlottingServices.EndPageEventHandler(event_EndPage);
            plot.EndPlot -= new Autodesk.AutoCAD.PlottingServices.EndPlotEventHandler(event_EndPlot);
            plot.PageCancelled -= new Autodesk.AutoCAD.PlottingServices.PageCancelledEventHandler(event_PageCancelled);
            plot.PlotCancelled -= new Autodesk.AutoCAD.PlottingServices.PlotCancelledEventHandler(event_PlotCancelled);
        }

        private void
        event_PlotCancelled(object sender, EventArgs e)
        {
            PrintReactorMessage("Plot Cancelled");
        }

        private void
        event_PageCancelled(object sender, EventArgs e)
        {
            PrintReactorMessage("Page Cancelled");
        }

        private void
        event_EndPlot(object sender, Autodesk.AutoCAD.PlottingServices.EndPlotEventArgs e)
        {
            PrintReactorMessage("End Plot");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "EndPlot";
                dbox.ShowDialog();
            }
        }

        private void
        event_EndPage(object sender, Autodesk.AutoCAD.PlottingServices.EndPageEventArgs e)
        {
            PrintReactorMessage("Plot End Page");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "EndPage";
                dbox.ShowDialog();
            }
        }

        private void
        event_EndDocument(object sender, Autodesk.AutoCAD.PlottingServices.EndDocumentEventArgs e)
        {
            PrintReactorMessage("End Document");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "EndDocument";
                dbox.ShowDialog();
            }
        }

        private void
        event_BeginPlot(object sender, Autodesk.AutoCAD.PlottingServices.BeginPlotEventArgs e)
        {
            PrintReactorMessage("Begin Plot");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "BeginPlot";
                dbox.ShowDialog();
            }
        }

        private void
        event_BeginPage(object sender, Autodesk.AutoCAD.PlottingServices.BeginPageEventArgs e)
        {
            PrintReactorMessage("Begin Page");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "BeginPage";
                dbox.ShowDialog();
            }
        }

        private void
        event_BeginDocument(object sender, Autodesk.AutoCAD.PlottingServices.BeginDocumentEventArgs e)
        {
            PrintReactorMessage("Begin Document");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "BeginDocument";
                dbox.ShowDialog();
            }
        }

        #region Print Abstraction

        private void
        PrintReactorMessage(string eventStr)
        {
            string printString = string.Format("\n[Plot Event] : {0,-20} ", eventStr);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        #endregion
    }
}
