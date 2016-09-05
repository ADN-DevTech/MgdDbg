
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
using System.Windows.Forms;

using AcDb = Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.Snoop.Data {

    class ReferencedBy : Data {

	    public AcDb.ObjectIdCollection    m_hardPointerIds   = new AcDb.ObjectIdCollection();
	    public AcDb.ObjectIdCollection    m_softPointerIds   = new AcDb.ObjectIdCollection();
	    public AcDb.ObjectIdCollection    m_hardOwnershipIds = new AcDb.ObjectIdCollection();
	    public AcDb.ObjectIdCollection    m_softOwnershipIds = new AcDb.ObjectIdCollection();

        protected AcDb.ObjectId m_val;
        protected int m_count = 0;
        protected int m_skipped = 0;
	    
		public
		ReferencedBy(string label, AcDb.ObjectId val)
		:   base(label)
		{
		    m_val = val;
		}

        public override string
        StrValue()
        {
            return "< ObjectIdCollection >";
        }

        public override bool
        HasDrillDown
        {
            get {
                if (m_val.IsNull)
                    return false;

                return true;    // someone should always reference it (besides, we don't want to go thru
                                // the brute force algorithm just to find out)
            }
        }
        
        public override void
        DrillDown()
        {
            using (TransactionHelper trHlp = new TransactionHelper(m_val.Database)) {
                trHlp.Start();

                BruteForceFindReferences(trHlp);
                
                Snoop.Forms.References form = new Snoop.Forms.References(m_hardPointerIds, m_softPointerIds, m_hardOwnershipIds, m_softOwnershipIds, trHlp);
                form.Text = "Object Referenced By";
                form.ShowDialog();

                trHlp.Commit();
            }
        }

        private void
        BruteForceFindReferences(TransactionHelper trHelp)
        {
            m_count = 0;
            m_skipped = 0;

                // since we aren't calculating this in the destructor, we have to re-init every time they
                // do the drill-down.
            m_hardPointerIds.Clear();
            m_softPointerIds.Clear();
            m_hardOwnershipIds.Clear();
            m_softOwnershipIds.Clear();

            AcDb.Database db = m_val.Database;

                // pass in all the root objects
            ProcessObject(trHelp, m_val, db.NamedObjectsDictionaryId);
            ProcessObject(trHelp, m_val, db.BlockTableId);
            ProcessObject(trHelp, m_val, db.DimStyleTableId);
            ProcessObject(trHelp, m_val, db.LayerTableId);
            ProcessObject(trHelp, m_val, db.LinetypeTableId);
            ProcessObject(trHelp, m_val, db.RegAppTableId);
            ProcessObject(trHelp, m_val, db.TextStyleTableId);
            ProcessObject(trHelp, m_val, db.UcsTableId);
            ProcessObject(trHelp, m_val, db.ViewportTableId);
            ProcessObject(trHelp, m_val, db.ViewTableId);

            //string str = string.Format("Visited: {0:d}, Skipped: {1:d}, DB Approx: {2:d}", m_count, m_skipped, db.ApproxNumObjects);
            //MessageBox.Show(str);
        }

        private void
        ProcessObject(TransactionHelper trHelp, AcDb.ObjectId lookForObjId, AcDb.ObjectId curObjId)
        {
            AcDb.DBObject tmpObj = trHelp.Transaction.GetObject(curObjId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead);
            if (tmpObj != null) {
                m_count++;
                MgdDbg.Utils.ReferenceFiler filer = new MgdDbg.Utils.ReferenceFiler();
                tmpObj.DwgOut(filer);     // find out who this object owns

                RecordReferences(lookForObjId, tmpObj, filer); // record references for this object

                    // now recursively visit all the objects this one owns
                for (int i=0; i<filer.m_hardOwnershipIds.Count; i++)
                    ProcessObject(trHelp, lookForObjId, filer.m_hardOwnershipIds[i]);

                for (int i=0; i<filer.m_softOwnershipIds.Count; i++)
                    ProcessObject(trHelp, lookForObjId, filer.m_softOwnershipIds[i]);
            }
            else
                m_skipped++;
        }

        private void
        RecordReferences(AcDb.ObjectId lookForObjId, AcDb.DBObject objToCheck, MgdDbg.Utils.ReferenceFiler filer)
        {
                // now see if we showed up in any of the lists
            if (filer.m_hardPointerIds.Contains(lookForObjId))
                m_hardPointerIds.Add(objToCheck.ObjectId);

            if (filer.m_softPointerIds.Contains(lookForObjId))
                m_softPointerIds.Add(objToCheck.ObjectId);

            if (filer.m_hardOwnershipIds.Contains(lookForObjId))
                m_hardOwnershipIds.Add(objToCheck.ObjectId);

            if (filer.m_softOwnershipIds.Contains(lookForObjId))
                m_softOwnershipIds.Add(objToCheck.ObjectId);
        }
    }
}

