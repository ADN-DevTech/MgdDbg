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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AcApp = Autodesk.AutoCAD.ApplicationServices;
using MgdDbg.Utils;

namespace MgdDbg.Reactors.Forms {

    public partial class EventsForm : Form {

        public static Events.ApplicationEvents    m_appEvents       = new Events.ApplicationEvents();
        public static Events.DatabaseEvents       m_dbEvents        = new Events.DatabaseEvents();
        public static Events.DatabaseObjEvents    m_dbObjEvents     = new Events.DatabaseObjEvents();
        public static Events.DocumentEvents       m_docEvents       = new Events.DocumentEvents();
        public static Events.DocumentMgrEvents    m_docMgrEvents    = new Events.DocumentMgrEvents();
        public static Events.DynamicLinkerEvents  m_dynLnkEvents    = new Events.DynamicLinkerEvents();
        public static Events.EditorEvents         m_edEvents        = new Events.EditorEvents();
        public static Events.GraphicsSystemEvents m_gsEvents        = new Events.GraphicsSystemEvents();
        public static Events.LayoutManagerEvents  m_layoutMgrEvents = new Events.LayoutManagerEvents();
        public static Events.PlotEvents           m_plotEvents      = new Events.PlotEvents();
        public static Events.PublishEvents        m_publishEvents   = new Events.PublishEvents();

        public
        EventsForm()
        {
            InitializeComponent();

            m_cbAppEventsOn.Checked = m_appEvents.AreEventsEnabled;
            m_cbDbEventsOn.Checked  = m_dbEvents.AreEventsEnabled;
            m_cbDbObjEventsOn.Checked  = m_dbObjEvents.AreEventsEnabled;
            m_cbDocEventsOn.Checked = m_docEvents.AreEventsEnabled;
            m_cbDocMgrEventsOn.Checked = m_docMgrEvents.AreEventsEnabled;
            m_cbDynLinkerEventsOn.Checked = m_dynLnkEvents.AreEventsEnabled;
            m_cbEdEventsOn.Checked = m_edEvents.AreEventsEnabled;
            m_cbGsEventsOn.Checked = m_gsEvents.AreEventsEnabled;
            m_cbLayoutMgrEventsOn.Checked = m_layoutMgrEvents.AreEventsEnabled;
            m_cbPlotEventsOn.Checked = m_plotEvents.AreEventsEnabled;
            m_cbPubEventsOn.Checked = m_publishEvents.AreEventsEnabled;

            m_cbShowDetailsDbEvents.Checked  = m_dbEvents.ShowDetails;
            m_cbShowDetailsDocMgrEvents.Checked = m_docMgrEvents.ShowDetails;
            m_cbShowDetailsEdEvents.Checked = m_edEvents.ShowDetails;
            m_cbShowDetailsGsEvents.Checked = m_gsEvents.ShowDetails;
            m_cbShowDetailsLayoutMgrEvents.Checked = m_layoutMgrEvents.ShowDetails;
            m_cbShowDetailsPlotEvents.Checked = m_plotEvents.ShowDetails;
            m_cbShowDetailsPublishEvents.Checked = m_publishEvents.ShowDetails;
        }

        private void
        event_OnBnOkClick(object sender, EventArgs e)
        {
            SetEventsOnOff(m_appEvents, m_cbAppEventsOn.Checked, false);
            SetEventsOnOff(m_dbEvents, m_cbDbEventsOn.Checked, m_cbShowDetailsDbEvents.Checked);
            SetEventsOnOff(m_dbObjEvents, m_cbDbObjEventsOn.Checked, false);
            SetEventsOnOff(m_docEvents, m_cbDocEventsOn.Checked, false);
            SetEventsOnOff(m_docMgrEvents, m_cbDocMgrEventsOn.Checked, m_cbShowDetailsDocMgrEvents.Checked);
            SetEventsOnOff(m_dynLnkEvents, m_cbDynLinkerEventsOn.Checked, false);
            SetEventsOnOff(m_edEvents, m_cbEdEventsOn.Checked, m_cbShowDetailsEdEvents.Checked);
            SetEventsOnOff(m_gsEvents, m_cbGsEventsOn.Checked, m_cbShowDetailsGsEvents.Checked);
            SetEventsOnOff(m_layoutMgrEvents, m_cbLayoutMgrEventsOn.Checked, m_cbShowDetailsLayoutMgrEvents.Checked);
            SetEventsOnOff(m_plotEvents, m_cbPlotEventsOn.Checked, m_cbShowDetailsPlotEvents.Checked);
            SetEventsOnOff(m_publishEvents, m_cbPubEventsOn.Checked, m_cbShowDetailsPublishEvents.Checked);

            this.Close();
        }

        private void
        SetEventsOnOff(Events.EventsBase eventGroup, bool onOff, bool showDetails)
        {
            if (onOff) {    // on
                if (eventGroup.AreEventsEnabled == false) {
                    eventGroup.EnableEvents();
                }
            }
            else {          // off
                if (eventGroup.AreEventsEnabled == true) {
                    eventGroup.DisableEvents();
                }
            }
            eventGroup.ShowDetails = showDetails;
        }

        private void m_tabPageSys_Click(object sender, EventArgs e)
        {

        }
    }
}