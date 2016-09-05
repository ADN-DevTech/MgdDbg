
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

namespace MgdDbg.Reactors.Events {

    public class DocumentMgrEvents : EventsBase {

        public
        DocumentMgrEvents()
        {
        }

        protected override void
        EnableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nDocument Manager Events Turned On ...\n");

            AcApp.DocumentCollection docs = AcApp.Application.DocumentManager;

            docs.DocumentActivated += new AcApp.DocumentCollectionEventHandler(event_DocumentActivated);
            docs.DocumentActivationChanged += new AcApp.DocumentActivationChangedEventHandler(event_DocumentActivationChanged);
            docs.DocumentBecameCurrent += new AcApp.DocumentCollectionEventHandler(event_DocumentBecameCurrent);
            docs.DocumentCreated += new AcApp.DocumentCollectionEventHandler(event_DocumentCreated);
            docs.DocumentCreateStarted += new AcApp.DocumentCollectionEventHandler(event_DocumentCreateStarted);
            docs.DocumentCreationCanceled += new AcApp.DocumentCollectionEventHandler(event_DocumentCreationCanceled);
            docs.DocumentDestroyed += new AcApp.DocumentDestroyedEventHandler(event_DocumentDestroyed);
            docs.DocumentLockModeChanged += new AcApp.DocumentLockModeChangedEventHandler(event_DocumentLockModeChanged);
            docs.DocumentLockModeChangeVetoed += new AcApp.DocumentLockModeChangeVetoedEventHandler(event_DocumentLockModeChangeVetoed);
            docs.DocumentLockModeWillChange += new AcApp.DocumentLockModeWillChangeEventHandler(event_DocumentLockModeWillChange);
            docs.DocumentToBeActivated += new AcApp.DocumentCollectionEventHandler(event_DocumentToBeActivated);
            docs.DocumentToBeDeactivated += new AcApp.DocumentCollectionEventHandler(event_DocumentToBeDeactivated);
            docs.DocumentToBeDestroyed += new AcApp.DocumentCollectionEventHandler(event_DocumentToBeDestroyed);
        }

        protected override void
        DisableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nDocument Manager Events Turned Off ...\n");

            AcApp.DocumentCollection docs = AcApp.Application.DocumentManager;

            docs.DocumentActivated -= new AcApp.DocumentCollectionEventHandler(event_DocumentActivated);
            docs.DocumentActivationChanged -= new AcApp.DocumentActivationChangedEventHandler(event_DocumentActivationChanged);
            docs.DocumentBecameCurrent -= new AcApp.DocumentCollectionEventHandler(event_DocumentBecameCurrent);
            docs.DocumentCreated -= new AcApp.DocumentCollectionEventHandler(event_DocumentCreated);
            docs.DocumentCreateStarted -= new AcApp.DocumentCollectionEventHandler(event_DocumentCreateStarted);
            docs.DocumentCreationCanceled -= new AcApp.DocumentCollectionEventHandler(event_DocumentCreationCanceled);
            docs.DocumentDestroyed -= new AcApp.DocumentDestroyedEventHandler(event_DocumentDestroyed);
            docs.DocumentLockModeChanged -= new AcApp.DocumentLockModeChangedEventHandler(event_DocumentLockModeChanged);
            docs.DocumentLockModeChangeVetoed -= new AcApp.DocumentLockModeChangeVetoedEventHandler(event_DocumentLockModeChangeVetoed);
            docs.DocumentLockModeWillChange -= new AcApp.DocumentLockModeWillChangeEventHandler(event_DocumentLockModeWillChange);
            docs.DocumentToBeActivated -= new AcApp.DocumentCollectionEventHandler(event_DocumentToBeActivated);
            docs.DocumentToBeDeactivated -= new AcApp.DocumentCollectionEventHandler(event_DocumentToBeDeactivated);
            docs.DocumentToBeDestroyed -= new AcApp.DocumentCollectionEventHandler(event_DocumentToBeDestroyed);
        }

        private void
        event_DocumentToBeDestroyed(object sender, AcApp.DocumentCollectionEventArgs e)
        {
            PrintEventMessage("Document To Be Destroyed", e.Document);
        }

        private void
        event_DocumentToBeDeactivated(object sender, AcApp.DocumentCollectionEventArgs e)
        {
            PrintEventMessage("Document To Be Deactivated", e.Document);
        }

        private void
        event_DocumentToBeActivated(object sender, AcApp.DocumentCollectionEventArgs e)
        {
            PrintEventMessage("Document To Be Activated", e.Document);
        }

        private void
        event_DocumentLockModeWillChange(object sender, AcApp.DocumentLockModeWillChangeEventArgs e)
        {
            PrintEventMessage("Document Lock Mode Will Change", e.Document);
            if (m_showDetails) {
                PrintSubEventMessage("Global Command Name", e.GlobalCommandName);
                PrintSubEventMessage("Current Mode", e.CurrentMode.ToString());
                PrintSubEventMessage("My Current Mode", e.MyCurrentMode.ToString());
                PrintSubEventMessage("My New Mode", e.MyNewMode.ToString());
            }            
        }

        private void
        event_DocumentLockModeChangeVetoed(object sender, AcApp.DocumentLockModeChangeVetoedEventArgs e)
        {
            PrintEventMessage("Document Lock Mode Change Vetoed", e.Document);
            if (m_showDetails) {
                PrintSubEventMessage("Global Command Name", e.GlobalCommandName);
            }            
        }

        private void
        event_DocumentLockModeChanged(object sender, AcApp.DocumentLockModeChangedEventArgs e)
        {
            PrintEventMessage("Document Lock Mode Changed", e.Document);
            if (m_showDetails) {
                PrintSubEventMessage("Global Command Name", e.GlobalCommandName);
                PrintSubEventMessage("Current Mode", e.CurrentMode.ToString());
                PrintSubEventMessage("My Current Mode", e.MyCurrentMode.ToString());
                PrintSubEventMessage("My Previous Mode", e.MyPreviousMode.ToString());
            }            
        }

        private void
        event_DocumentDestroyed(object sender, AcApp.DocumentDestroyedEventArgs e)
        {
            PrintEventMessage("Document Destroyed", e.FileName);
        }

        private void
        event_DocumentCreationCanceled(object sender, AcApp.DocumentCollectionEventArgs e)
        {
            PrintEventMessage("Document Creation Canceled", e.Document);
        }

        private void
        event_DocumentCreateStarted(object sender, AcApp.DocumentCollectionEventArgs e)
        {
            PrintEventMessage("Document Create Started", e.Document);
        }

        private void
        event_DocumentCreated(object sender, AcApp.DocumentCollectionEventArgs e)
        {
            PrintEventMessage("Document Created", e.Document);
        }

        private void
        event_DocumentBecameCurrent(object sender, AcApp.DocumentCollectionEventArgs e)
        {
            PrintEventMessage("Document Became Current", e.Document);
        }

        private void
        event_DocumentActivationChanged(object sender, AcApp.DocumentActivationChangedEventArgs e)
        {
            PrintEventMessage("Document Activation Changed", e.NewValue.ToString());
        }

        private void
        event_DocumentActivated(object sender, AcApp.DocumentCollectionEventArgs e)
        {
            PrintEventMessage("Document Activated", e.Document);
        }

        #region Print Abstraction

        private void
        PrintEventMessage(string eventStr)
        {
            string printString = string.Format("\n[Doc Mgr Event] : {0,-25}", eventStr);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        private void
        PrintEventMessage(string eventStr, AcApp.Document doc)
        {
            string printString = string.Format("\n[Doc Mgr Event] : {0,-25} : {1}", eventStr, doc.Name);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        private void
        PrintEventMessage(string eventStr, string msg)
        {
            string printString = string.Format("\n[Doc Mgr Event] : {0,-25} : {1}", eventStr, msg);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        private void
        PrintSubEventMessage(string eventStr, string msg)
        {
            string printString = string.Format("\n    {0,-20} : {1}", eventStr, msg);
            Utils.AcadUi.PrintToCmdLine(printString);
        }


        #endregion
    }
}
