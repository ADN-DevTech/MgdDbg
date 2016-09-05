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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcDb = Autodesk.AutoCAD.DatabaseServices;
using AcadApp = Autodesk.AutoCAD.ApplicationServices;

namespace MgdDbg.Utils
{
    /// <summary>
    /// This class takes two objects and compares their 
    /// properties using Reflection and stores them in a map.
    /// </summary>
    public class ObjectCompare
    {
        // member vars
        private Object m_obj1;
        private Object m_obj2;

        private PropertyInfo[] m_propInfos1 = null;
        private PropertyInfo[] m_propInfos2 = null;

        private ArrayList m_commonHierarchy = new ArrayList();
        /// <summary>
        /// only 2 objects to compare , so restrict the capacity
        /// </summary>
        private ArrayList m_objData = new ArrayList(2); 
        private Hashtable m_propMapItem = new Hashtable();
        private Hashtable m_propMap = new Hashtable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        public
        ObjectCompare (Object obj1, Object obj2)
        {
            if (obj1 == null)
                m_obj1 = System.DBNull.Value;
            if (obj2 == null)
                m_obj2 = System.DBNull.Value;

            m_obj1 = obj1;
            m_obj2 = obj2;

            Initialise();
        }

        /// <summary>
        /// 
        /// </summary>
        public ArrayList
        CommonHierarchy
        {
            get { return m_commonHierarchy; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void
        Initialise ()
        {
            m_propInfos1 = m_obj1.GetType().GetProperties();
            m_propInfos2 = m_obj2.GetType().GetProperties();

            Type intersectionType = GetIntersectionLevel(m_obj1, m_obj2);
            Boolean isIntersecting = (intersectionType == typeof(System.Object)) ? false : true;

            m_propInfos1 = GetPropsTillLevel(m_propInfos1, intersectionType);
            m_propInfos2 = GetPropsTillLevel(m_propInfos2, intersectionType);

            BuildHierarchy(m_propInfos1);

            if (m_obj1.GetType() == typeof(System.DBNull) && m_obj2.GetType() != typeof(System.DBNull)) {
                m_propInfos1 = new PropertyInfo[m_propInfos2.Length];
                m_propInfos1.Initialize();
                BuildHierarchy(m_propInfos2);
            }

            if (m_obj2.GetType() == typeof(System.DBNull) && m_obj1.GetType() != typeof(System.DBNull)) {
                m_propInfos2 = new PropertyInfo[m_propInfos1.Length];
                m_propInfos2.Initialize();
                BuildHierarchy(m_propInfos1);
            }

            
            if (isIntersecting) {
                for (int i = m_propInfos1.Length - 1; i >= 0; i--) {

                    for (int j = m_propInfos2.Length - 1; j >= 0; j--) {

                        /// Case where a base property is hidden by a derived property
                        /// ex: Curve's Closed property is hidden by the Polyline's 
                        /// Closed property and thus for a pline object the Closed property
                        /// doesnt show up at the Curve level, so check to see if the
                        /// property name *and* the declaring type are the same
                        if (m_propInfos1[i].Name == m_propInfos2[j].Name
                            && m_propInfos1[i].DeclaringType == m_propInfos2[j].DeclaringType) {

                            PopulateMap(m_propInfos1[i], m_propInfos2[j]);
                            break;
                        }
                    }
                }
            }
            else {
                /// Case where a valid object is being compared to a 
                /// null object. Happens in case of collection objects
                for (int i = m_propInfos1.Length - 1; i >= 0; i--) {
                    PopulateMap(m_propInfos1[i], m_propInfos2[i]);
                }
            }

            /// address collection objects 
            if (IsCollectionObject(m_obj1) || IsCollectionObject(m_obj2)) {

                ArrayList items1 = GetItemsFromCollection(m_obj1);
                ArrayList items2 = GetItemsFromCollection(m_obj2);

                int maxCount = (items1.Count >= items2.Count) ? items1.Count : items2.Count;

                for (int i = 0; i < maxCount; i++) {

                    m_objData = new ArrayList(2);
                    try {
                        m_objData.Add(items1[i]);
                    }
                    catch {
                        m_objData.Add(System.DBNull.Value);
                    }
                    try {
                        m_objData.Add(items2[i]);
                    }
                    catch {
                        m_objData.Add(System.DBNull.Value);
                    }
                    string keyStr = String.Format("Item {0}", i);
                    m_propMapItem[keyStr] = m_objData;
                }
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propInfo1"></param>
        /// <param name="propInfo2"></param>
        private void
        PopulateMap (PropertyInfo propInfo1, PropertyInfo propInfo2)
        {
            Type declaringType = null;
            String propName = string.Empty;
            
            if (propInfo1 != null && propInfo2 != null) {
                declaringType = propInfo1.DeclaringType;
                propName = propInfo1.Name;
            }

            if (propInfo1 == null) {
                declaringType = propInfo2.DeclaringType;
                propName = propInfo2.Name;
            }
            if (propInfo2 == null) {
                declaringType = propInfo1.DeclaringType;
                propName = propInfo1.Name;
            }
            
            if (!m_propMap.ContainsKey(declaringType)) {

                m_propMapItem = new Hashtable();
                m_propMap.Add(declaringType, m_propMapItem);
            }

            m_objData = new ArrayList(2);

            /// some properties throw an exception 
            /// handle them
            try {
                object value = propInfo1.GetValue(m_obj1, null);
                if (value == null)
                    m_objData.Add(System.DBNull.Value);
                else
                    m_objData.Add(value);
            }
            catch {
                m_objData.Add(System.DBNull.Value);
            }
            /// some properties throw an exception 
            /// handle them
            try {
                object value = propInfo2.GetValue(m_obj2, null);
                if (value == null)
                    m_objData.Add(System.DBNull.Value);
                else
                    m_objData.Add(value);
            }
            catch {
                m_objData.Add(System.DBNull.Value);
            }
            m_propMapItem[propName] = m_objData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Hashtable
        GetPropsAtLevel (Type type)
        {
            return m_propMap[type] as Hashtable;
        }

        /// <summary>
        /// Build and store the object hierarchy
        /// </summary>
        /// <param name="propInfos"></param>
        private void
        BuildHierarchy (PropertyInfo[] propInfos)
        {
            Type type = null;
            Stack types = new Stack();
            foreach (PropertyInfo propInfo in propInfos) {
                if (propInfo.DeclaringType != type) {
                    type = propInfo.DeclaringType;
                    types.Push(type);
                }
            }

            m_commonHierarchy.Clear();

            while (types.Count != 0) {
                m_commonHierarchy.Add(types.Pop() as Type);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Boolean
        IsCollectionObject (Object obj)
        {
            /// a collection object has to implement IList
            if (obj.GetType().GetInterface(typeof(IList).FullName) != null)
                return true;
            return false;
        }

        /// <summary>
        /// Gets the properties till the level type
        /// </summary>
        /// <param name="propInfo"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private PropertyInfo[]
        GetPropsTillLevel (PropertyInfo[] propInfo, Type level)
        {
            Boolean done = false;
            for (int i = propInfo.Length - 1; i >= 0; i--) {

                if (propInfo[i].DeclaringType == level) {
                    done = true;
                }
                else {
                    if (done) {
                        PropertyInfo[] tempPropInfo = propInfo;
                        propInfo = new PropertyInfo[(propInfo.Length) - (i + 1)];
                        Array.Copy(tempPropInfo, i + 1, propInfo, 0, propInfo.Length);
                        break;
                    }
                }
            }
            return propInfo;
        }

        /// <summary>
        /// At what level of the hierarchy do 
        /// the 2 objects intersect
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        private Type
        GetIntersectionLevel (Object obj1, Object obj2)
        {
            Type type1 = obj1.GetType();
            Type type2 = obj2.GetType();
            Type origType2 = obj2.GetType();

            while (type1 != type2) {
                if (type2 != null)
                    type2 = type2.BaseType;
                else {
                    type2 = origType2;
                    type1 = type1.BaseType;
                }

            }

            return type1;
        }

        /// <summary>
        /// mine into collection objects and get their items
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private ArrayList
        GetItemsFromCollection (Object obj)
        {
            PropertyInfo[] propInfos = obj.GetType().GetProperties();
            PropertyInfo propInfoItem = null;
            Int32 count = 0;

            foreach (PropertyInfo propInfo in propInfos) {

                ParameterInfo[] paramArr = propInfo.GetIndexParameters();
                if (paramArr.Length != 0) {
                    propInfoItem = propInfo;
                    continue;
                }

                /// TBD: an IList implementer has to implement 
                /// the "Count" property, so this should be safe
                /// albeit there should be a better way
                if (propInfo.Name == "Count") {
                    count = (Int32)propInfo.GetValue(obj, null);
                }
            }

            ArrayList itemData = new ArrayList();
            /// some properties throw an exception 
            /// handle them

            for (int i = 0; i < count; i++) {

                object[] index = new object[1];
                index[0] = i;

                try {
                    object value = propInfoItem.GetValue(obj, index);
                    if (value == null)
                        itemData.Add(System.DBNull.Value);
                    else
                        itemData.Add(value);
                }
                catch {
                    itemData.Add(System.DBNull.Value);
                }
            }
            return itemData;
        }
    }
}

