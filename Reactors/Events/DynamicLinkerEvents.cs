
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

using Autodesk.AutoCAD.Runtime;

namespace MgdDbg.Reactors.Events {

    public class DynamicLinkerEvents : EventsBase {

        public
        DynamicLinkerEvents()
        {
        }

        protected override void
        EnableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nDynamic Linker Events Turned On ...\n");

            DynamicLinker dLinker = SystemObjects.DynamicLinker;

            dLinker.ModuleLoadAborted += new ModuleLoadAbortedEventHandler(event_ModuleLoadAborted);
            dLinker.ModuleLoaded += new ModuleLoadedEventHandler(event_ModuleLoaded);
            dLinker.ModuleLoading += new ModuleLoadingEventHandler(event_ModuleLoading);
            dLinker.ModuleUnloadAborted += new ModuleUnloadAbortedEventHandler(event_ModuleUnloadAborted);
            dLinker.ModuleUnloaded += new ModuleUnloadedEventHandler(event_ModuleUnloaded);
            dLinker.ModuleUnloading += new ModuleUnloadingEventHandler(event_ModuleUnloading);
        }

        protected override void
        DisableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nDynamic Linker Events Turned Off ...\n");

            DynamicLinker dLinker = SystemObjects.DynamicLinker;

            dLinker.ModuleLoadAborted -= new ModuleLoadAbortedEventHandler(event_ModuleLoadAborted);
            dLinker.ModuleLoaded -= new ModuleLoadedEventHandler(event_ModuleLoaded);
            dLinker.ModuleLoading -= new ModuleLoadingEventHandler(event_ModuleLoading);
            dLinker.ModuleUnloadAborted -= new ModuleUnloadAbortedEventHandler(event_ModuleUnloadAborted);
            dLinker.ModuleUnloaded -= new ModuleUnloadedEventHandler(event_ModuleUnloaded);
            dLinker.ModuleUnloading -= new ModuleUnloadingEventHandler(event_ModuleUnloading);
        }

        private void
        event_ModuleUnloading(object sender, DynamicLinkerEventArgs e)
        {
            PrintEventMessage("Module Unloading", e.FileName);
        }

        private void
        event_ModuleUnloaded(object sender, DynamicLinkerEventArgs e)
        {
            PrintEventMessage("Module Unloaded", e.FileName);
        }

        private void
        event_ModuleUnloadAborted(object sender, DynamicLinkerEventArgs e)
        {
            PrintEventMessage("Module Unload Aborted", e.FileName);
        }

        private void
        event_ModuleLoading(object sender, DynamicLinkerEventArgs e)
        {
            PrintEventMessage("Module Loading", e.FileName);
        }

        private void
        event_ModuleLoaded(object sender, DynamicLinkerEventArgs e)
        {
            PrintEventMessage("Module Loaded", e.FileName);
        }

        private void
        event_ModuleLoadAborted(object sender, DynamicLinkerEventArgs e)
        {
            PrintEventMessage("Module Load Aborted", e.FileName);
        }

        #region Print Abstraction

        private void
        PrintEventMessage(string eventStr, string fileName)
        {
            string printString = string.Format("\n[Dynamic Linker Event] : {0,-25} : {1}", eventStr, fileName);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        #endregion
    }
}
