
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

using AcDb = Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.Snoop.Data {

    class References : Data {

	    public AcDb.ObjectIdCollection    m_hardPointerIds   = new AcDb.ObjectIdCollection();
	    public AcDb.ObjectIdCollection    m_softPointerIds   = new AcDb.ObjectIdCollection();
	    public AcDb.ObjectIdCollection    m_hardOwnershipIds = new AcDb.ObjectIdCollection();
	    public AcDb.ObjectIdCollection    m_softOwnershipIds = new AcDb.ObjectIdCollection();

        protected AcDb.ObjectId m_val;
        protected bool          m_hasRefs = false;
	    
		public
		References(string label, AcDb.ObjectId val)
		:   base(label)
		{
		    m_val = val;

            if (m_val.IsNull == false) {
                using (TransactionHelper trHlp = new TransactionHelper(m_val.Database)) {
                    trHlp.Start();

                    AcDb.DBObject dbObj = trHlp.Transaction.GetObject(m_val, AcDb.OpenMode.ForRead);

                    MgdDbg.Utils.ReferenceFiler filer = new MgdDbg.Utils.ReferenceFiler();
                    dbObj.DwgOut(filer);

                    trHlp.Commit();

                    m_hardPointerIds = filer.m_hardPointerIds;
                    m_softPointerIds = filer.m_softPointerIds;
                    m_hardOwnershipIds = filer.m_hardOwnershipIds;
                    m_softOwnershipIds = filer.m_softOwnershipIds;
                }

                if ((m_hardPointerIds.Count == 0) && (m_softPointerIds.Count == 0) && (m_hardOwnershipIds.Count == 0) && (m_softOwnershipIds.Count == 0))
                    m_hasRefs = false;
                else
                    m_hasRefs = true;
            }
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
                return m_hasRefs;
            }
        }
        
        public override void
        DrillDown()
        {
            if (m_hasRefs) {
                using (TransactionHelper trHlp = new TransactionHelper(m_val.Database)) {
                    trHlp.Start();
                    
                    Snoop.Forms.References form = new Snoop.Forms.References(m_hardPointerIds, m_softPointerIds, m_hardOwnershipIds, m_softOwnershipIds, trHlp);
                    form.Text = "Object References";
                    form.ShowDialog();

                    trHlp.Commit();
                }
            }
        }
    }
}
