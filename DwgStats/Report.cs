
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
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.DwgStats
{
	/// <summary>
	/// Summary description for Report.
	/// </summary>
	public class Report
	{
	        // data members
        private ArrayList           m_entities = new ArrayList();
        private ArrayList           m_objects = new ArrayList();
        private Database            m_db = null;
        private TransactionHelper   m_trHlp = null;
        private ObjectIdCollection  m_extDictIds = new ObjectIdCollection();

		public
		Report()
		{
		}
	    
	    /// <summary>
	    /// Return the Count object for this block (or make a new one if
	    /// we haven't already seen it).
	    /// </summary>
	    /// <param name="blkRecId">The BlockTableRecord we are looking for</param>
	    /// <param name="addIfNotThere">Add a new one if not there?</param>
	    /// <returns>The count object for this BlockTableRecord</returns>
	    
		private BlockCount
        GetEntityNode(ObjectId blkRecId, bool addIfNotThere)
        {
            foreach (BlockCount tmpNode in m_entities) {
                if (tmpNode.m_blockDefId == blkRecId)
                    return tmpNode;
            }

            if (addIfNotThere) {
                BlockCount tmpNode = new BlockCount();
                tmpNode.m_blockDefId = blkRecId;
                tmpNode.m_blockName = m_trHlp.SymbolIdToName(blkRecId);
                m_entities.Add(tmpNode);
                
                return tmpNode;
            }

            return null;    // didn't find it
        }
        
        /// <summary>
	    /// Return the Count object for this class of object (or make a new one if
	    /// we haven't already seen it).
        /// </summary>
        /// <param name="className">Name of the class we're looking for</param>
        /// <param name="addIfNotThere">Add a new one if not there?</param>
        /// <returns>The count object for this class</returns>
        
        private SymbolCount
        GetObjectNode(string className, string displayName, bool addIfNotThere)
        {
            foreach (SymbolCount tmpNode in m_objects) {
                if (tmpNode.m_className == className)
                    return tmpNode;
            }

            if (addIfNotThere) {
                SymbolCount tmpNode = new SymbolCount();
                tmpNode.m_className = className;
                tmpNode.m_displayName = displayName;
                m_objects.Add(tmpNode);
                
                return tmpNode;
            }

            return null;    // didn't find it
        }
        
        /// <summary>
        /// Given an object, get its class name, even if it is currently
        /// a proxy.
        /// </summary>
        /// <param name="obj">The object in question</param>
        /// <returns>The name of the class that this object is an instance of</returns>
        private string
        GetObjClassName(DBObject obj)
        {
                // if this is a proxy we can't use the proxy class
                // name because we would get duplicates on the list
                // for each object.
            if (obj.IsAProxy) {
                ProxyObject proxyObj = obj as ProxyObject;
                if (proxyObj != null)
                    return proxyObj.OriginalClassName;
                else {
                    ProxyEntity proxyEnt = obj as ProxyEntity;
                    if (proxyEnt != null)
                        return proxyEnt.OriginalClassName;
                    else {
                        Debug.Assert(false);
                        return obj.GetRXClass().Name;
                    }
                }
            }
            else
                return obj.GetRXClass().Name;
        }
        
        /// <summary>
        /// Walk through all the blocks in the drawing and process all the entities
        /// </summary>
        
        private void
        ProcessEntities()
        {
                // get all blocks that aren't from Xrefs
            ObjectIdCollection blkRecIds = m_trHlp.CollectBlockIds(false, false, false, true);

            DwgStats.BlockCount tmpBlkCount = null;
            DwgStats.ObjCount tmpObjCount = null;
            
                // walk through all the block defs and count entities within them
            foreach (ObjectId blkRecId in blkRecIds) {
                tmpBlkCount = GetEntityNode(blkRecId, true);
                if (tmpBlkCount != null) {
                    BlockTableRecord blkRec = (BlockTableRecord)m_trHlp.Transaction.GetObject(blkRecId, OpenMode.ForRead);
                    foreach (ObjectId tmpEntId in blkRec) {
                        Entity ent = (Entity)m_trHlp.Transaction.GetObject(tmpEntId, OpenMode.ForRead);
                        tmpObjCount = tmpBlkCount.GetCount(GetObjClassName(ent), ent.GetType().Name, true);
                        
                            // if it has an extension dictionary, record its id so we can process it later
                        ObjectId extDictId = ent.ExtensionDictionary;
                        if ((extDictId.IsNull == false) && (extDictId.IsErased == false))
                            m_extDictIds.Add(extDictId);
                            
                        if (tmpObjCount != null)
                            tmpObjCount.m_count++;
                    }
                }
            }
        }
        
        /// <summary>
        /// Walk through all the Dictionaries and process them
        /// </summary>
        
        private void
        ProcessObjects()
        {
            ObjectIdCollection objIds = new ObjectIdCollection();
            ArrayList names = new ArrayList();
            
                // get all the symbol table objects
            GetSymbolTableEntries(m_db.BlockTableId, ref objIds, ref names);
            GetSymbolTableEntries(m_db.DimStyleTableId, ref objIds, ref names);
            GetSymbolTableEntries(m_db.LayerTableId, ref objIds, ref names);
            GetSymbolTableEntries(m_db.LinetypeTableId, ref objIds, ref names);
            GetSymbolTableEntries(m_db.RegAppTableId, ref objIds, ref names);
            GetSymbolTableEntries(m_db.TextStyleTableId, ref objIds, ref names);
            GetSymbolTableEntries(m_db.ViewTableId, ref objIds, ref names);
            GetSymbolTableEntries(m_db.ViewportTableId, ref objIds, ref names);
            GetSymbolTableEntries(m_db.UcsTableId, ref objIds, ref names);

                // get all the NamedObjectDictionary objects
            GetRecursiveDictionaryEntries(m_db.NamedObjectsDictionaryId, ref objIds, ref names);
            
                // get all the Extension dictionary objects
            foreach (ObjectId extDictId in m_extDictIds)
                GetRecursiveDictionaryEntries(extDictId, ref objIds, ref names);

            int len = objIds.Count;

                // get reference count to these objects
            int[] countArray = new int[len];
            for (int i=0; i<len; i++)
                countArray[i] = 0;  // init count values;

            m_db.CountHardReferences(objIds, countArray);

            SymbolCount tmpCount = null;
            for (int i=0; i<len; i++) {
                DBObject tmpObj = m_trHlp.Transaction.GetObject(objIds[i], OpenMode.ForRead);
                tmpCount = GetObjectNode(GetObjClassName(tmpObj), tmpObj.GetType().Name, true);
                if (tmpCount != null) {
                    tmpCount.m_count++;
                    tmpCount.m_symbolNames.Add(names[i]);
                    tmpCount.m_references.Add(countArray[i]);
                }
            }
        }
        
        /// <summary>
        /// Get all the entries in a given SymbolTable
        /// </summary>
        /// <param name="tblId">The ObjectId of the SymbolTable</param>
        /// <param name="objIds">The ObjectIds of all the entries</param>
        /// <param name="names">The names of all the entries</param>
        
        private void
        GetSymbolTableEntries(ObjectId tblId, ref ObjectIdCollection objIds, ref ArrayList names)
        {
            DBObject tmpObj = m_trHlp.Transaction.GetObject(tblId, OpenMode.ForRead);
            SymbolTable tbl = tmpObj as SymbolTable;
            if (tbl != null) {
                    // iterate over each TblRec in the SymbolTable
                foreach (ObjectId tblRecId in tbl) {
                    names.Add(m_trHlp.SymbolIdToName(tblRecId));
                    objIds.Add(tblRecId);
                }
            }
        }
        
        /// <summary>
        /// Recursively walk through all Dictionaries under the one passed in
        /// </summary>
        /// <param name="dictId"></param>
        /// <param name="tr"></param>
        /// <param name="objIds"></param>
        /// <param name="names"></param>
        
        private void
        GetRecursiveDictionaryEntries(ObjectId dictId, ref ObjectIdCollection objIds, ref ArrayList names)
        {
                // NOTE: when recursively processing items in a dictionary
                // we may encounter things that are not derived from DBDictionary.
                // In that case, the cast to type DBDictionary will fail and
                // we'll just return without adding any nested items.
            DBObject tmpObj = m_trHlp.Transaction.GetObject(dictId, OpenMode.ForRead);
            DBDictionary dbDict = tmpObj as DBDictionary;
            if (dbDict != null) {
                foreach (DictionaryEntry curEntry in dbDict) {
                    objIds.Add((ObjectId)curEntry.Value);
                    names.Add((string)curEntry.Key);
                        
                        // if this is a dictionary, it will recursively add
                        // all of its children to the tree
                    GetRecursiveDictionaryEntries((ObjectId)curEntry.Value, ref objIds, ref names);   
                }
            }
        }
        
        /// <summary>
        /// Create the XML Report for this database
        /// </summary>
        /// <param name="reportPath"></param>
        /// <param name="db"></param>
        
        public void
        XmlReport(string reportPath, Database db)
        {
                // make sure everything is cleaned out in case they run this twice
		    m_entities.Clear();
		    m_objects.Clear();
		    m_extDictIds.Clear();
            m_db = db;
            
            using (m_trHlp = new TransactionHelper(db)) {
                m_trHlp.Start();

                ProcessEntities();
                ProcessObjects();

                XmlTextWriter stream = new XmlTextWriter(reportPath, System.Text.Encoding.UTF8);
                stream.Formatting = Formatting.Indented;
                stream.IndentChar = '\t';
                stream.Indentation = 1;
                
                stream.WriteStartDocument();
                stream.WriteStartElement("Drawings");
                stream.WriteStartElement("Drawing");

                stream.WriteAttributeString("path", db.Filename);

                stream.WriteStartElement("Entities");
                XmlReportRawEntityCount(stream);
                XmlReportBlockDefs(stream);
                stream.WriteEndElement();    // "Entities"

                stream.WriteStartElement("Symbols");
                XmlReportRawSymbolCount(stream);
                XmlReportSymbolReferences(stream);
                stream.WriteEndElement();    // "Symbols"

                stream.WriteEndElement();    // "Drawing"
                stream.WriteEndElement();    // "Drawings"
                stream.WriteEndDocument();
                
                stream.Close();
                
                m_trHlp.Commit();
            }
        }
        
        /// <summary>
        /// Create the XML report for a batch of drawings.
        /// </summary>
        /// <param name="reportPath">The XML filename to use</param>
        /// <param name="fnames">Array of paths to drawing files</param>
        public void
        XmlReport(string reportPath, string[] fnames)
        {
            XmlTextWriter stream = new XmlTextWriter(reportPath, System.Text.Encoding.UTF8);
            stream.Formatting = Formatting.Indented;
            stream.IndentChar = '\t';
            stream.Indentation = 1;
            
            stream.WriteStartDocument();
            stream.WriteStartElement("Drawings");
            
            foreach (string dwgPath in fnames) {
                    // make sure everything is cleaned out in case they run this twice
		        m_entities.Clear();
		        m_objects.Clear();
		        m_extDictIds.Clear();

                Utils.AcadUi.PrintToCmdLine(string.Format("\nProcessing drawing file: {0} ...", dwgPath));
                using (m_db = new Database(false, true)) {
                    m_db.ReadDwgFile(dwgPath, System.IO.FileShare.Read, true, null);
            
                    using (m_trHlp = new TransactionHelper(m_db)) {
                        m_trHlp.Start();

                        ProcessEntities();
                        ProcessObjects();

                        stream.WriteStartElement("Drawing");

                        stream.WriteAttributeString("path", dwgPath);

                        stream.WriteStartElement("Entities");
                        XmlReportRawEntityCount(stream);
                        XmlReportBlockDefs(stream);
                        stream.WriteEndElement();    // "Entities"

                        stream.WriteStartElement("Symbols");
                        XmlReportRawSymbolCount(stream);
                        XmlReportSymbolReferences(stream);
                        stream.WriteEndElement();    // "Symbols"

                        stream.WriteEndElement();    // "Drawing"
                        
                        m_trHlp.Commit();
                    }
                    
                    m_db.Dispose();
                }

                m_db = null;
            }

            stream.WriteEndElement();    // "Drawings"
            stream.WriteEndDocument();
            stream.Close();
        }
        
        private void
        XmlReportBlockDefs(XmlTextWriter stream)
        {
            DwgStats.BlockCount tmpBlockCount;
            DwgStats.ObjCount tmpObjCount;

            stream.WriteStartElement("BlockDefs");

                // walk through all the entries and file out the stats
            int len = m_entities.Count;
            for (int i=0;i<len;i++) {
                tmpBlockCount = (DwgStats.BlockCount)m_entities[i];

                int len2 = tmpBlockCount.m_objCounts.Count;

                    // only output this if there are entities within the block
                if (len2 > 0) {
                    stream.WriteStartElement("BlockDef");
                    stream.WriteAttributeString("name", tmpBlockCount.m_blockName);

                    BlockTableRecord blkRec = (BlockTableRecord)m_trHlp.Transaction.GetObject(tmpBlockCount.m_blockDefId, OpenMode.ForRead);
                    stream.WriteAttributeString("isAnonymous", blkRec.IsAnonymous ? "1" : "0");
                    stream.WriteAttributeString("isFromXref", (blkRec.IsFromExternalReference || blkRec.IsFromOverlayReference) ? "1" : "0");
                    stream.WriteAttributeString("isLayout", blkRec.IsLayout ? "1" : "0");

                    for (int j=0;j<len2;j++) {
                        tmpObjCount = (DwgStats.ObjCount)tmpBlockCount.m_objCounts[j];
                    
                        stream.WriteStartElement("ObjectType");
                        stream.WriteAttributeString("class", tmpObjCount.m_className);
                        stream.WriteAttributeString("displayName", tmpObjCount.m_displayName);
                        stream.WriteAttributeString("count", tmpObjCount.m_count.ToString());
                        stream.WriteEndElement();
                    }
                
                    stream.WriteEndElement();
                }
            }

            stream.WriteEndElement();
        }

        private void
        XmlReportRawEntityCount(XmlTextWriter stream)
        {
            BlockCount tmpBlockCount = null;
            ObjCount tmpObjCount = null;

            stream.WriteStartElement("RawCount");

            ArrayList classNames = new ArrayList();
            ArrayList displayNames = new ArrayList();
            ArrayList objCounts = new ArrayList();;

                // walk through all the entries and file out the stats
            int len = m_entities.Count;
            for (int i=0;i<len;i++) {
                tmpBlockCount = (BlockCount)m_entities[i];

                int len2 = tmpBlockCount.m_objCounts.Count;
                for (int j=0;j<len2;j++) {
                    tmpObjCount = (ObjCount)tmpBlockCount.m_objCounts[j];

                    int index;
                    if (FindClassName(classNames, tmpObjCount.m_className, out index)) {
                        objCounts[index] = ((long)objCounts[index]) + tmpObjCount.m_count;
                    }
                    else {
                        classNames.Add(tmpObjCount.m_className);
                        displayNames.Add(tmpObjCount.m_displayName);
                        objCounts.Add(tmpObjCount.m_count);
                    }
                }
            }

                // now push out to XML
            len = classNames.Count;
            for (int i=0;i<len;i++) {
                stream.WriteStartElement("ObjectType");
                stream.WriteAttributeString("class", (string)classNames[i]);
                stream.WriteAttributeString("displayName", (string)displayNames[i]);
                stream.WriteAttributeString("count", objCounts[i].ToString());
                stream.WriteEndElement();
            }

            stream.WriteEndElement();
        }
        
        private void
        XmlReportRawSymbolCount(XmlTextWriter stream)
        {
            SymbolCount tmpObjCount;

            stream.WriteStartElement("RawCount");

                // walk through all the entries and file out the stats
            int len = m_objects.Count;
            for (int i=0;i<len;i++) {
                tmpObjCount = (SymbolCount)m_objects[i];

                stream.WriteStartElement("ObjectType");
                stream.WriteAttributeString("class", tmpObjCount.m_className);
                stream.WriteAttributeString("displayName", tmpObjCount.m_displayName);
                stream.WriteAttributeString("count", tmpObjCount.m_count.ToString());
                stream.WriteEndElement();
            }

            stream.WriteEndElement();
        }
        
        private void
        XmlReportSymbolReferences(XmlTextWriter stream)
        {
            SymbolCount tmpSymCount;

            stream.WriteStartElement("References");

                // walk through all the entries and file out the stats
            int len = m_objects.Count;
            for (int i=0;i<len;i++) {
                tmpSymCount = (SymbolCount)m_objects[i];

                stream.WriteStartElement("ObjectType");
                stream.WriteAttributeString("class", tmpSymCount.m_className);
                stream.WriteAttributeString("displayName", tmpSymCount.m_displayName);
                stream.WriteAttributeString("count", tmpSymCount.m_count.ToString());

                int len2 = tmpSymCount.m_symbolNames.Count;
                for (int j=0; j<len2; j++) {
                    stream.WriteStartElement("Symbol");
                    stream.WriteAttributeString("name", (string)tmpSymCount.m_symbolNames[j]);
                    stream.WriteAttributeString("references", tmpSymCount.m_references[j].ToString());
                    stream.WriteEndElement();
                }
                stream.WriteEndElement();    // "ObjectType"
            }

            stream.WriteEndElement();    // "References"
        }
        
        private bool
        FindClassName(ArrayList strs, string matchStr, out int index)
        {
            index = 0;
            int len = strs.Count;

            for (int i=0;i<len;i++) {
                if (matchStr == (string)strs[i]) {
                    index = i;
                    return true;
                }
            }

            return false;
        }
	}
}
