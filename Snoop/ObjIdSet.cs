
//
// (C) Copyright 2004 by Autodesk, Inc. 
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
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;

using Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.Snoop
{
	/// <summary>
	/// A class to keep track of a unique set of objects collected by the user.
	/// It is basically a specialized form of ObjectIdCollection that ensures that
	/// each object is unique and encourages them all to be from the same database.  The
	/// first object added to the set establishes the primary database and it
	/// is suggested to the user that the rest should be from the same database.
	/// If you want to enforce it, call the constructor with the Database arg.
	/// </summary>
	
	public class ObjIdSet
	{
	    private string              m_name;
	    private Database            m_db = null;
	    private ObjectIdCollection  m_objIdSet = new ObjectIdCollection();
	    private bool                m_enforceSameDb = false;
	    
	    /// <summary>
	    /// Constructor to use when first object selected establishes the primary database
	    /// and non-same dbs are only encouraged, not enforced.
	    /// </summary>
	    /// <param name="name">name of the set</param>
	    
		public
		ObjIdSet(string name)
		{
		    m_name = name;
		}
		
		/// <summary>
		/// Constructor to use when all objects must come from the same database
		/// </summary>
		/// <param name="name">name of the set</param>
		/// <param name="allowThisDbOnly">database that all objects must come from</param>
		
        public
        ObjIdSet(string name, Database allowThisDbOnly)
        {
            m_name = name;
            m_db = allowThisDbOnly;
            m_enforceSameDb = true;
        }
		
		public string
		Name {
            get { return m_name;  }
            set { m_name = value; }
        }
        
        public Database
        Db {
            get { return m_db; }
        }
        
        public ObjectIdCollection
        Set {
            get { return m_objIdSet; }
        }
        
        public bool
        AddToSet(ObjectId objId)
        {
            if (objId.IsNull) {
                Debug.Assert(false);
                return false;
            }
            
                // if this the first thing recorded, stamp the selection set
                // as being from a particular database so we can warn non-same
                // db additions.
            if ((m_enforceSameDb == false) && (m_db == null)) {
                m_db = objId.Database;
                m_objIdSet.Add(objId);
                return true;    
            }
            else {
                if (m_db != objId.Database) {
                    if (m_enforceSameDb) {
                        MessageBox.Show("This object is not from the same database dictated by the set.  It will not be added.");
                        return false;
                    }
                    else {
                        DialogResult res = MessageBox.Show("This object is from a different database.  Are you sure you want to add it to the set?", "ObjectId Set", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (res == DialogResult.No)
                            return false;
                    }
                }
                
                if (Contains(objId)) {
                    MessageBox.Show("This object is already in the set.", "ObjectId Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                
                m_objIdSet.Add(objId);
                return true;
            }
        }
        
        public void
        RemoveFromSet(ObjectId objId)
        {
            m_objIdSet.Remove(objId);
            if (m_objIdSet.Count == 0) {
                if (m_enforceSameDb == false)
                    m_db = null;
            }
        }
        
        public void
        ClearSet()
        {
            m_objIdSet.Clear();
            if (m_enforceSameDb == false)
                m_db = null;
        }
        
        /// <summary>
        /// The Contains() function of ObjectIdCollection does not see ObjectId's as the same
        /// if there are two separate references to it.  Compare the OldId integer values to 
        /// see if they both point to the same thing.
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        
        public bool
        Contains(ObjectId objId)
        {
            foreach (ObjectId tmpObjId in m_objIdSet) {
                if (tmpObjId.OldIdPtr.ToInt32() == objId.OldIdPtr.ToInt32())
                    return true;
            }
            
            return false;
        }
    }
}
