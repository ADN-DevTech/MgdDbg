
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
using System.Diagnostics;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;

using MgdDbg.Utils;

namespace MgdDbg.Reactors.Events {
    
    public class DatabaseObjEvents : EventsBase {

        public
        DatabaseObjEvents()
        {
        }

        protected override void
        EnableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nDatabase Object Events Turned On ...\n");

                // attach event handlers to each database we know about
            DocumentCollection docs = Application.DocumentManager;
            foreach (Document doc in docs) {
                EnableEvents(doc.Database);
            }
        }

        public void
        EnableEvents(Database db)
        {
            db.ObjectAppended += new ObjectEventHandler(event_ObjectAppended);
            db.ObjectErased += new ObjectErasedEventHandler(event_ObjectErased);
            db.ObjectModified += new ObjectEventHandler(event_ObjectModified);
            db.ObjectOpenedForModify += new ObjectEventHandler(event_ObjectOpenedForModify);
            db.ObjectReappended += new ObjectEventHandler(event_ObjectReappended);
            db.ObjectUnappended += new ObjectEventHandler(event_ObjectUnappended);
        }

        protected override void
        DisableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nDatabase Object Events Turned Off ...\n");

                // detach event handlers from each doc
            DocumentCollection docs = Application.DocumentManager;
            foreach (Document doc in docs) {
                DisableEvents(doc.Database);
            }
        }

        public void
        DisableEvents(Database db)
        {
            db.ObjectAppended -= new ObjectEventHandler(event_ObjectAppended);
            db.ObjectErased -= new ObjectErasedEventHandler(event_ObjectErased);
            db.ObjectModified -= new ObjectEventHandler(event_ObjectModified);
            db.ObjectOpenedForModify -= new ObjectEventHandler(event_ObjectOpenedForModify);
            db.ObjectReappended -= new ObjectEventHandler(event_ObjectReappended);
            db.ObjectUnappended -= new ObjectEventHandler(event_ObjectUnappended);
        }

        #region Notifications

        private void
        event_ObjectAppended(object sender, ObjectEventArgs e)
        {
            PrintEventMessage((Database)sender, "Object Appended", e.DBObject);
        }

        private void
        event_ObjectErased(object sender, ObjectErasedEventArgs e)
        {
            if (e.Erased)
                PrintEventMessage((Database)sender, "Object Erased", e.DBObject);
            else
                PrintEventMessage((Database)sender, "Object Un-Erased", e.DBObject);
        }

        private void
        event_ObjectModified(object sender, ObjectEventArgs e)
        {
            PrintEventMessage((Database)sender, "Object Modified", e.DBObject);
        }

        private void
        event_ObjectOpenedForModify(object sender, ObjectEventArgs e)
        {
            PrintEventMessage((Database)sender, "Object Opened For Modify", e.DBObject);
        }

        private void
        event_ObjectReappended(object sender, ObjectEventArgs e)
        {
            PrintEventMessage((Database)sender, "Object Reappended", e.DBObject);
        }

        private void
        event_ObjectUnappended(object sender, ObjectEventArgs e)
        {
            PrintEventMessage((Database)sender, "Object Unappended", e.DBObject);
        }

        #endregion


        # region Print Abstraction

        private void
        PrintEventMessage(Database db, string eventStr, DBObject dbObj)
        {
            string printString = string.Format("\n[DB Event : {0}] : {1,-25} : {2}", DbToStr(db), eventStr, ObjToTypeAndHandleStr(dbObj));
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        private string
        DbToStr(Database db)
        {
            return db.GetHashCode().ToString();
        }

        private string
        ObjToTypeAndHandleStr(DBObject dbObj)
        {
            Debug.Assert(dbObj != null);
            return string.Format("<{0}, {1}>", dbObj.GetType().Name, dbObj.Handle.ToString());
        }


        #endregion
    }
}

