
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
    
    public class DatabaseEvents : EventsBase {

        public
        DatabaseEvents()
        {
        }

        protected override void
        EnableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nDatabase Events Turned On ...\n");

                // attach event handlers to each database we know about
            DocumentCollection docs = Application.DocumentManager;
            foreach (Document doc in docs) {
                EnableEvents(doc.Database);
            }
        }

        public void
        EnableEvents(Database db)
        {
            db.AbortDxfIn += new EventHandler(event_AbortDxfIn);
            db.AbortDxfOut += new EventHandler(event_AbortDxfOut);
            db.AbortSave += new EventHandler(event_AbortSave);
            db.BeginDeepClone += new IdMappingEventHandler(event_BeginDeepClone);
            db.BeginDeepCloneTranslation += new IdMappingEventHandler(event_BeginDeepCloneTranslation);
            db.BeginDxfIn += new EventHandler(event_BeginDxfIn);
            db.BeginDxfOut += new EventHandler(event_BeginDxfOut);
            db.BeginInsert += new BeginInsertEventHandler(event_BeginInsert);
            db.BeginSave += new DatabaseIOEventHandler(event_BeginSave);
            db.BeginWblockBlock += new BeginWblockBlockEventHandler(event_BeginWblockBlock);
            db.BeginWblockEntireDatabase += new BeginWblockEntireDatabaseEventHandler(event_BeginWblockEntireDatabase);
            db.BeginWblockObjects += new BeginWblockObjectsEventHandler(event_BeginWblockObjects);
            db.BeginWblockSelectedObjects += new BeginWblockSelectedObjectsEventHandler(event_BeginWblockSelectedObjects);
            # if !ACAD2007
            Database.DatabaseConstructed += new EventHandler(event_DatabaseConstructed);
            # endif
            db.DatabaseToBeDestroyed += new EventHandler(event_DatabaseToBeDestroyed);
            db.DeepCloneAborted += new EventHandler(event_DeepCloneAborted);
            db.DeepCloneEnded += new EventHandler(event_DeepCloneEnded);
            db.DwgFileOpened += new DatabaseIOEventHandler(event_DwgFileOpened);
            db.DxfInComplete += new EventHandler(event_DxfInComplete);
            db.DxfOutComplete += new EventHandler(event_DxfOutComplete);
            db.InitialDwgFileOpenComplete += new EventHandler(event_InitialDwgFileOpenComplete);
            db.InsertAborted += new EventHandler(event_InsertAborted);
            db.InsertEnded += new EventHandler(event_InsertEnded);
            db.InsertMappingAvailable += new IdMappingEventHandler(event_InsertMappingAvailable);
            db.PartialOpenNotice += new EventHandler(event_PartialOpenNotice);
            db.ProxyResurrectionCompleted += new ProxyResurrectionCompletedEventHandler(event_ProxyResurrectionCompleted);
            db.SaveComplete += new DatabaseIOEventHandler(event_SaveComplete);
            db.SystemVariableChanged += new Autodesk.AutoCAD.DatabaseServices.SystemVariableChangedEventHandler(event_SystemVariableChanged);
            db.SystemVariableWillChange += new Autodesk.AutoCAD.DatabaseServices.SystemVariableChangingEventHandler(event_SystemVariableWillChange);
            db.WblockAborted += new EventHandler(event_WblockAborted);
            db.WblockEnded += new EventHandler(event_WblockEnded);
            db.WblockMappingAvailable += new IdMappingEventHandler(event_WblockMappingAvailable);
            db.WblockNotice += new WblockNoticeEventHandler(event_WblockNotice);
            # if !ACAD2007
            Database.XrefAttachAborted += new EventHandler(event_XrefAttachAborted);
            #endif
            db.XrefAttachEnded += new EventHandler(event_XrefAttachEnded);
            db.XrefBeginAttached += new XrefBeginOperationEventHandler(event_XrefBeginAttached);
            db.XrefBeginOtherAttached +=new XrefBeginOperationEventHandler(event_XrefBeginOtherAttached);
            db.XrefBeginRestore += new XrefBeginOperationEventHandler(event_XrefBeginRestore);
            db.XrefComandeered += new XrefComandeeredEventHandler(event_XrefComandeered);
            db.XrefPreXrefLockFile += new XrefPreXrefLockFileEventHandler(event_XrefPreXrefLockFile);
            db.XrefRedirected += new XrefRedirectedEventHandler(event_XrefRedirected);
            db.XrefRestoreAborted += new EventHandler(event_XrefRestoreAborted);
            db.XrefRestoreEnded += new EventHandler(event_XrefRestoreEnded);
            db.XrefSubCommandAborted += new XrefSubCommandAbortedEventHandler(event_XrefSubCommandAborted);
            //see why this is commented out below in the DisableNotifications()
            //db.XrefSubCommandStart += new XrefSubCommandStartEventHandler(event_XrefSubCommandStart);
        }

        protected override void
        DisableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nDatabase Events Turned Off ...\n");

                // detach event handlers from each doc
            DocumentCollection docs = Application.DocumentManager;
            foreach (Document doc in docs) {
                DisableEvents(doc.Database);
            }
        }

        public void
        DisableEvents(Database db)
        {
            db.AbortDxfIn -= new EventHandler(event_AbortDxfIn);
            db.AbortDxfOut -= new EventHandler(event_AbortDxfOut);
            db.AbortSave -= new EventHandler(event_AbortSave);
            db.BeginDeepClone -= new IdMappingEventHandler(event_BeginDeepClone);
            db.BeginDeepCloneTranslation -= new IdMappingEventHandler(event_BeginDeepCloneTranslation);
            db.BeginDxfIn -= new EventHandler(event_BeginDxfIn);
            db.BeginDxfOut -= new EventHandler(event_BeginDxfOut);
            db.BeginInsert -= new BeginInsertEventHandler(event_BeginInsert);
            db.BeginSave -= new DatabaseIOEventHandler(event_BeginSave);
            db.BeginWblockBlock -= new BeginWblockBlockEventHandler(event_BeginWblockBlock);
            db.BeginWblockEntireDatabase -= new BeginWblockEntireDatabaseEventHandler(event_BeginWblockEntireDatabase);
            db.BeginWblockObjects -= new BeginWblockObjectsEventHandler(event_BeginWblockObjects);
            db.BeginWblockSelectedObjects -= new BeginWblockSelectedObjectsEventHandler(event_BeginWblockSelectedObjects);
            # if !ACAD2007
            Database.DatabaseConstructed -= new EventHandler(event_DatabaseConstructed);
            #endif
            db.DatabaseToBeDestroyed -= new EventHandler(event_DatabaseToBeDestroyed);
            db.DeepCloneAborted -= new EventHandler(event_DeepCloneAborted);
            db.DeepCloneEnded -= new EventHandler(event_DeepCloneEnded);
            db.DwgFileOpened -= new DatabaseIOEventHandler(event_DwgFileOpened);
            db.DxfInComplete -= new EventHandler(event_DxfInComplete);
            db.DxfOutComplete -= new EventHandler(event_DxfOutComplete);
            db.InitialDwgFileOpenComplete -= new EventHandler(event_InitialDwgFileOpenComplete);
            db.InsertAborted -= new EventHandler(event_InsertAborted);
            db.InsertEnded -= new EventHandler(event_InsertEnded);
            db.InsertMappingAvailable -= new IdMappingEventHandler(event_InsertMappingAvailable);
            db.PartialOpenNotice -= new EventHandler(event_PartialOpenNotice);
            db.ProxyResurrectionCompleted -= new ProxyResurrectionCompletedEventHandler(event_ProxyResurrectionCompleted);
            db.SaveComplete -= new DatabaseIOEventHandler(event_SaveComplete);
            db.SystemVariableChanged -= new Autodesk.AutoCAD.DatabaseServices.SystemVariableChangedEventHandler(event_SystemVariableChanged);
            db.SystemVariableWillChange -= new Autodesk.AutoCAD.DatabaseServices.SystemVariableChangingEventHandler(event_SystemVariableWillChange);
            db.WblockAborted -= new EventHandler(event_WblockAborted);
            db.WblockEnded -= new EventHandler(event_WblockEnded);
            db.WblockMappingAvailable -= new IdMappingEventHandler(event_WblockMappingAvailable);
            db.WblockNotice -= new WblockNoticeEventHandler(event_WblockNotice);
            # if !ACAD2007
            Database.XrefAttachAborted -= new EventHandler(event_XrefAttachAborted);
            #endif
            db.XrefAttachEnded -= new EventHandler(event_XrefAttachEnded);
            db.XrefBeginAttached -= new XrefBeginOperationEventHandler(event_XrefBeginAttached);
            db.XrefBeginOtherAttached -=new XrefBeginOperationEventHandler(event_XrefBeginOtherAttached);
            db.XrefBeginRestore -= new XrefBeginOperationEventHandler(event_XrefBeginRestore);
            db.XrefComandeered -= new XrefComandeeredEventHandler(event_XrefComandeered);
            db.XrefPreXrefLockFile -= new XrefPreXrefLockFileEventHandler(event_XrefPreXrefLockFile);
            db.XrefRedirected -= new XrefRedirectedEventHandler(event_XrefRedirected);
            db.XrefRestoreAborted -= new EventHandler(event_XrefRestoreAborted);
            db.XrefRestoreEnded -= new EventHandler(event_XrefRestoreEnded);
            db.XrefSubCommandAborted -= new XrefSubCommandAbortedEventHandler(event_XrefSubCommandAborted);
            //see why this is commented out below in the DisableNotifications()
            //db.XrefSubCommandStart -= new XrefSubCommandStartEventHandler(event_XrefSubCommandStart);
        }

        #region Notifications

        private void
        event_AbortDxfIn(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Abort Dxf In");
        }

        private void
        event_AbortDxfOut(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Abort Dxf Out");
        }

        private void
        event_AbortSave(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Abort Save");
        }

        private void
        event_BeginDeepClone(object sender, IdMappingEventArgs e)
        {
            string str2 = string.Format("Context = {0}", e.IdMapping.DeepCloneContext.ToString());
            PrintEventMessage((Database)sender, "Begin Deep Clone", str2);
            if (m_showDetails) {
                MgdDbg.Reactors.Forms.Details details = new MgdDbg.Reactors.Forms.Details("Begin Deep Clone", e.IdMapping);
                details.ShowDialog();
            }
        }

        private void
        event_BeginDeepCloneTranslation(object sender, IdMappingEventArgs e)
        {
            PrintEventMessage((Database)sender, "Begin Deep Clone Translation");
            if (m_showDetails) {
                MgdDbg.Reactors.Forms.Details details = new MgdDbg.Reactors.Forms.Details("Begin Deep Clone Translation", e.IdMapping);
                details.ShowDialog();
            }
        }

        private void
        event_BeginDxfIn(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Begin Dxf In");
        }

        private void
        event_BeginDxfOut(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Begin Dxf Out");
        }

        private void
        event_BeginInsert(object sender, BeginInsertEventArgs e)
        {
            PrintEventMessage((Database)sender, "Begin Insert", AcadUi.DbToStr(e.From));
            if (m_showDetails) {
                using (TransactionHelper trHlp = new TransactionHelper(e.From)) {
                    trHlp.Start();

                    Snoop.Forms.Database dbox = new Snoop.Forms.Database(e.From, trHlp);
                    dbox.Text = "From Database";

                    trHlp.Commit();
                }
            }
        }

        private void
        event_BeginSave(object sender, DatabaseIOEventArgs e)
        {
            string str2 = string.Format("Intended Name = \"{0}\"", e.FileName);
            PrintEventMessage((Database)sender, "Begin Save", str2);
        }

        private void
        event_BeginWblockBlock(object sender, BeginWblockBlockEventArgs e)
        {
            string str2 = string.Format("Block Name = {0}, DB = {1}", AcadUi.ObjToTypeAndHandleStr(e.BlockId), AcadUi.DbToStr(e.From));
            PrintEventMessage((Database)sender, "Begin Wblock", str2);
            if (m_showDetails) {
                using (TransactionHelper trHlp = new TransactionHelper(e.From)) {
                    trHlp.Start();

                    Snoop.Forms.Database dbox = new Snoop.Forms.Database(e.From, trHlp);
                    dbox.Text = "From Database";

                    trHlp.Commit();
                }
            }
        }

        private void
        event_BeginWblockEntireDatabase(object sender, BeginWblockEntireDatabaseEventArgs e)
        {
            string str2 = string.Format("DB = {0}", AcadUi.DbToStr(e.From));
            PrintEventMessage((Database)sender, "Begin Wblock Entire Database", str2);
            if (m_showDetails) {
                using (TransactionHelper trHlp = new TransactionHelper(e.From)) {
                    trHlp.Start();

                    Snoop.Forms.Database dbox = new Snoop.Forms.Database(e.From, trHlp);
                    dbox.Text = "From Database";

                    trHlp.Commit();
                }
            }
        }

        private void
        event_BeginWblockObjects(object sender, BeginWblockObjectsEventArgs e)
        {
            string str2 = string.Format("DB = {0}", AcadUi.DbToStr(e.From));
            PrintEventMessage((Database)sender, "Begin Wblock Objects", str2);
            if (m_showDetails) {
                MgdDbg.Reactors.Forms.Details details = new MgdDbg.Reactors.Forms.Details("Begin Wblock Objects", e.IdMapping);
                details.ShowDialog();
            }
        }

        private void
        event_BeginWblockSelectedObjects(object sender, BeginWblockSelectedObjectsEventArgs e)
        {
            string str2 = string.Format("Insert Pt. = {0}, {1}", AcadUi.PtToStr(e.InsertionPoint), AcadUi.DbToStr(e.From));
            PrintEventMessage((Database)sender, "Begin Wblock Selected Objects", str2);
        }

        private void
        event_DatabaseConstructed(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Database Constructed");
        }

        private void
        event_DatabaseToBeDestroyed(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Database To Be Destroyed");
        }

        private void
        event_DeepCloneAborted(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Deep Clone Aborted");
        }

        private void
        event_DeepCloneEnded(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Deep Clone Ended");
        }

        private void
        event_DwgFileOpened(object sender, DatabaseIOEventArgs e)
        {
            PrintEventMessage((Database)sender, "Dwg File Opened", e.FileName);
        }

        private void
        event_DxfInComplete(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Dxf In Complete");
        }

        private void
        event_DxfOutComplete(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Dxf Out Complete");
        }

        private void
        event_InitialDwgFileOpenComplete(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Initial Dwg File Open Complete");
        }

        private void
        event_InsertAborted(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Insert Aborted");            
        }

        private void
        event_InsertEnded(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Insert Ended");            
        }

        private void
        event_InsertMappingAvailable(object sender, IdMappingEventArgs e)
        {
            PrintEventMessage((Database)sender, "Insert Mapping Available");            
            if (m_showDetails) {
                MgdDbg.Reactors.Forms.Details details = new MgdDbg.Reactors.Forms.Details("Insert Mapping Available", e.IdMapping);
                details.ShowDialog();
            }
        }

        private void
        event_PartialOpenNotice(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Partial Open Notice");
        }

        private void
        event_ProxyResurrectionCompleted(object sender, ProxyResurrectionCompletedEventArgs e)
        {
            PrintEventMessage((Database)sender, "Proxy Resurrection Completed", e.ApplicationName);
             if (m_showDetails) {
                 using (TransactionHelper trHlp = new TransactionHelper(e.Ids[0].Database)) {
                     trHlp.Start();

                     Snoop.Forms.DBObjects form = new Snoop.Forms.DBObjects(e.Ids, trHlp);
                     form.ShowDialog();

                     trHlp.Commit();
                 }
             }
        }

        private void
        event_SaveComplete(object sender, DatabaseIOEventArgs e)
        {
            string str2 = string.Format("Actual Name = \"{0}\"", e.FileName);
            PrintEventMessage((Database)sender, "Save Complete", str2);
        }

        private void
        event_SystemVariableChanged(object sender, Autodesk.AutoCAD.DatabaseServices.SystemVariableChangedEventArgs e)
        {
            if (e.Changed)
                PrintEventMessage((Database)sender, "System Var Changed", e.Name);
            else
                PrintEventMessage((Database)sender, "System Var Not Changed Successfully", e.Name);
        }

        private void
        event_SystemVariableWillChange(object sender, Autodesk.AutoCAD.DatabaseServices.SystemVariableChangingEventArgs e)
        {
            PrintEventMessage((Database)sender, "System Var Will Change", e.Name);
        }

        private void
        event_WblockAborted(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Wblock Aborted");
        }

        private void
        event_WblockEnded(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Wblock Ended");
        }

        private void
        event_WblockMappingAvailable(object sender, IdMappingEventArgs e)
        {
            PrintEventMessage((Database)sender, "Wblock Mapping Available");            

            if (m_showDetails) {
                MgdDbg.Reactors.Forms.Details details = new MgdDbg.Reactors.Forms.Details("Wblock Mapping Available", e.IdMapping);
                details.ShowDialog();
            }
        }

        private void
        event_WblockNotice(object sender, WblockNoticeEventArgs e)
        {
            string str2 = string.Format("To = {0}", AcadUi.DbToStr(e.To));
            PrintEventMessage((Database)sender, "Wblock Notice", str2);
            if (m_showDetails) {
                using (TransactionHelper trHlp = new TransactionHelper(e.To)) {
                    trHlp.Start();

                    Snoop.Forms.Database dbox = new Snoop.Forms.Database(e.To, trHlp);
                    dbox.Text = "To Database";

                    trHlp.Commit();
                }
            }
        }

        private void
        event_XrefAttachAborted(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Attach Aborted");
        }

        private void
        event_XrefAttachEnded(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Attach Ended");
        }

        private void
        event_XrefBeginAttached(object sender, XrefBeginOperationEventArgs e)
        {
            PrintEventMessage((Database)sender, "Begin Attached", AcadUi.DbToStr(e.From));
            if (m_showDetails) {
                using (TransactionHelper trHlp = new TransactionHelper(e.From)) {
                    trHlp.Start();

                    Snoop.Forms.Database dbox = new Snoop.Forms.Database(e.From, trHlp);
                    dbox.Text = "From Database";

                    trHlp.Commit();
                }
            }
        }

        private void
        event_XrefBeginOtherAttached(object sender, XrefBeginOperationEventArgs e)
        {
            PrintEventMessage((Database)sender, "Begin Other Attached", AcadUi.DbToStr(e.From));
            if (m_showDetails) {
                using (TransactionHelper trHlp = new TransactionHelper(e.From)) {
                    trHlp.Start();

                    Snoop.Forms.Database dbox = new Snoop.Forms.Database(e.From, trHlp);
                    dbox.Text = "From Database";

                    trHlp.Commit();
                }
            }
        }

        private void
        event_XrefBeginRestore(object sender, XrefBeginOperationEventArgs e)
        {
            PrintEventMessage((Database)sender, "BeginRestore", AcadUi.DbToStr(e.From));
            if (m_showDetails) {
                using (TransactionHelper trHlp = new TransactionHelper(e.From)) {
                    trHlp.Start();

                    Snoop.Forms.Database dbox = new Snoop.Forms.Database(e.From, trHlp);
                    dbox.Text = "From Database";

                    trHlp.Commit();
                }
            }
        }

        private void
        event_XrefComandeered(object sender, XrefComandeeredEventArgs e)
        {
            string str2 = string.Format("{0} {1}", AcadUi.ObjToTypeAndHandleStr(e.Id), AcadUi.DbToStr(e.From));
            PrintEventMessage((Database)sender, "Comandeered", str2);
        }

        private void
        event_XrefPreXrefLockFile(object sender, XrefPreXrefLockFileEventArgs e)
        {
            PrintEventMessage((Database)sender, "Xref Pre Xref Lock File", AcadUi.ObjToTypeAndHandleStr(e.btrId));
        }

        private void
        event_XrefRedirected(object sender, XrefRedirectedEventArgs e)
        {
            string str2 = string.Format("From = {0} , To = {1}", AcadUi.ObjToTypeAndHandleStr(e.OldId), AcadUi.ObjToTypeAndHandleStr(e.NewId));
            PrintEventMessage((Database)sender, "Redirected", str2);
        }

        private void
        event_XrefRestoreAborted(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Restore Aborted");
        }

        private void
        event_XrefRestoreEnded(object sender, EventArgs e)
        {
            PrintEventMessage((Database)sender, "Restore Ended");
        }

        private void
        event_XrefSubCommandAborted(object sender, XrefSubCommandEventArgs e)
        {
            PrintEventMessage((Database)sender, "Xref Sub-Command Aborted");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "XrefSubCommandAborted";
                dbox.ShowDialog();
            }
        }

        private void
        event_XrefSubCommandStart(object sender, XrefVetoableSubCommandEventArgs e)
        {
            PrintEventMessage((Database)sender, "Xref Sub-Command Start");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "XrefSubCommandStart";
                dbox.ShowDialog();
            }
        }

        #endregion


        # region Print Abstraction

        private void
        PrintEventMessage(Database db, string eventStr)
        {
            string printString = string.Format("\n[DB Event : {0}] : {1}", DbToStr(db), eventStr);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        private void
        PrintEventMessage(Database db, string eventStr, string str2)
        {
            string printString = string.Format("\n[DB Event : {0}] : {1,-25} : {2}", DbToStr(db), eventStr, str2);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        private string
        DbToStr(Database db)
        {
            return db.GetHashCode().ToString();
        }

        #endregion
    }
}
