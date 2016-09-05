
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

using AcApp = Autodesk.AutoCAD.ApplicationServices;
using AcRx = Autodesk.AutoCAD.Runtime;

namespace MgdDbg {
    
    class AppDocReactor {

        public
        AppDocReactor()
        {
        }
     
        public void
        EnableEvents()
        {
            AcApp.DocumentCollection docs = AcApp.Application.DocumentManager;

            docs.DocumentCreated += new AcApp.DocumentCollectionEventHandler(event_DocumentCreated);
            docs.DocumentToBeDestroyed += new AcApp.DocumentCollectionEventHandler(event_DocumentToBeDestroyed);
        }

        public void
        DisableEvents()
        {
            AcApp.DocumentCollection docs = AcApp.Application.DocumentManager;
            
            // Throws an assert when the SnoopEd command is used. Need to investigate this further.
            //docs.DocumentCreated -= new AcApp.DocumentCollectionEventHandler(event_DocumentCreated);            

            docs.DocumentToBeDestroyed -= new AcApp.DocumentCollectionEventHandler(event_DocumentToBeDestroyed);
        }
      
        private void
        event_DocumentCreated(object sender, AcApp.DocumentCollectionEventArgs e)
        {
                // if the reactor instance exists and if the relevant checkbox is ticked in the Reactors UI
            if (MgdDbg.Reactors.Forms.EventsForm.m_dbEvents.AreEventsEnabled) {
                MgdDbg.Reactors.Forms.EventsForm.m_dbEvents.EnableEvents(e.Document.Database);  // will turn on just for this new document
            }

            if (MgdDbg.Reactors.Forms.EventsForm.m_dbObjEvents.AreEventsEnabled) {
                MgdDbg.Reactors.Forms.EventsForm.m_dbObjEvents.EnableEvents(e.Document.Database);  // will turn on just for this new document
            }

            if (MgdDbg.Reactors.Forms.EventsForm.m_docEvents.AreEventsEnabled) {
                MgdDbg.Reactors.Forms.EventsForm.m_docEvents.EnableEvents(e.Document);          // will turn on just for this new document
            }

            if (MgdDbg.Reactors.Forms.EventsForm.m_edEvents.AreEventsEnabled) {
                MgdDbg.Reactors.Forms.EventsForm.m_edEvents.EnableEvents(e.Document.Editor);    // will turn on just for this new document
            }

            if (MgdDbg.Reactors.Forms.EventsForm.m_gsEvents.AreEventsEnabled) {
                MgdDbg.Reactors.Forms.EventsForm.m_gsEvents.EnableEvents(e.Document.GraphicsManager);    // will turn on just for this new document
            }
        }

        private void
        event_DocumentToBeDestroyed(object sender, AcApp.DocumentCollectionEventArgs e)
        {
                // if the reactor instance exists and if the relevant checkbox is ticked in the Reactors UI
            if (MgdDbg.Reactors.Forms.EventsForm.m_dbEvents.AreEventsEnabled) {
                MgdDbg.Reactors.Forms.EventsForm.m_dbEvents.DisableEvents(e.Document.Database); // will turn off just for this new document
            }

            if (MgdDbg.Reactors.Forms.EventsForm.m_dbObjEvents.AreEventsEnabled) {
                MgdDbg.Reactors.Forms.EventsForm.m_dbObjEvents.DisableEvents(e.Document.Database); // will turn off just for this new document
            }

            if (MgdDbg.Reactors.Forms.EventsForm.m_docEvents.AreEventsEnabled) {
                MgdDbg.Reactors.Forms.EventsForm.m_docEvents.DisableEvents(e.Document);         // will turn off just for this new document
            }

            if (MgdDbg.Reactors.Forms.EventsForm.m_edEvents.AreEventsEnabled) {
                MgdDbg.Reactors.Forms.EventsForm.m_edEvents.DisableEvents(e.Document.Editor);   // will turn off just for this new document
            }

            if (MgdDbg.Reactors.Forms.EventsForm.m_gsEvents.AreEventsEnabled) {
                MgdDbg.Reactors.Forms.EventsForm.m_gsEvents.DisableEvents(e.Document.GraphicsManager);   // will turn off just for this new document
            }

        }

    }
}
