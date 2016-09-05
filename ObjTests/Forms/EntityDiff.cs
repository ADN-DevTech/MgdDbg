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

using MgdDbg.Utils;

namespace MgdDbg.Test
{
    /// <summary>
    /// Summary description for ObjectCompare.
    /// </summary>
    public class EntityDiff : System.Windows.Forms.Form
    {
        // member vars
        protected System.Windows.Forms.ListView m_lvData;
        protected System.Windows.Forms.Button m_bnOK;
        protected System.Windows.Forms.ColumnHeader m_lvCol_label;
        protected System.Windows.Forms.ColumnHeader m_lvCol_value1;
        protected System.Windows.Forms.ColumnHeader m_lvCol_value2;

        private ObjectCompare m_compareObjs;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        protected System.ComponentModel.Container components = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objId1"></param>
        /// <param name="objId2"></param>
        /// <param name="tr"></param>
        public
        EntityDiff (AcDb.ObjectId objId1, AcDb.ObjectId objId2, TransactionHelper tr)
        {
            // Required for Windows Form Designer support
            InitializeComponent();

            Object obj1 = tr.Transaction.GetObject(objId1, OpenMode.ForRead);
            Object obj2 = tr.Transaction.GetObject(objId2, OpenMode.ForRead);

            m_compareObjs = new ObjectCompare(obj1, obj2);
            Initialise();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        public
        EntityDiff (Object obj1, Object obj2)
        {
            // Required for Windows Form Designer support
            InitializeComponent();

            m_compareObjs = new ObjectCompare(obj1, obj2);
            Initialise();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void
        Dispose (bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected void
        InitializeComponent ()
        {
            this.m_lvData = new System.Windows.Forms.ListView();
            this.m_lvCol_label = new System.Windows.Forms.ColumnHeader();
            this.m_lvCol_value1 = new System.Windows.Forms.ColumnHeader();
            this.m_lvCol_value2 = new System.Windows.Forms.ColumnHeader();
            this.m_bnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lvData
            // 
            this.m_lvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvData.AutoArrange = false;
            this.m_lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_lvCol_label,
            this.m_lvCol_value1,
            this.m_lvCol_value2});
            this.m_lvData.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_lvData.FullRowSelect = true;
            this.m_lvData.GridLines = true;
            this.m_lvData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvData.HideSelection = false;
            this.m_lvData.Location = new System.Drawing.Point(12, 16);
            this.m_lvData.MultiSelect = false;
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.ShowItemToolTips = true;
            this.m_lvData.Size = new System.Drawing.Size(553, 471);
            this.m_lvData.TabIndex = 1;
            this.m_lvData.UseCompatibleStateImageBehavior = false;
            this.m_lvData.View = System.Windows.Forms.View.Details;
            this.m_lvData.DoubleClick += new System.EventHandler(this.DataItemSelected);
            this.m_lvData.Click += new System.EventHandler(this.DataItemSelected);
            // 
            // m_lvCol_label
            // 
            this.m_lvCol_label.Text = "Field";
            this.m_lvCol_label.Width = 150;
            // 
            // m_lvCol_value1
            // 
            this.m_lvCol_value1.Text = "Value 1";
            this.m_lvCol_value1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_lvCol_value1.Width = 200;
            // 
            // m_lvCol_value2
            // 
            this.m_lvCol_value2.Text = "Value 2";
            this.m_lvCol_value2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_lvCol_value2.Width = 200;
            // 
            // m_bnOK
            // 
            this.m_bnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_bnOK.Location = new System.Drawing.Point(247, 500);
            this.m_bnOK.Name = "m_bnOK";
            this.m_bnOK.Size = new System.Drawing.Size(75, 23);
            this.m_bnOK.TabIndex = 2;
            this.m_bnOK.Text = "OK";
            // 
            // EntityDiff
            // 
            this.AcceptButton = this.m_bnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.m_bnOK;
            this.ClientSize = new System.Drawing.Size(577, 535);
            this.Controls.Add(this.m_bnOK);
            this.Controls.Add(this.m_lvData);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(575, 225);
            this.Name = "EntityDiff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diff Objects";
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Get the common hierarchy of the 2 objects
        /// and then get the properties at each level
        /// </summary>
        private void
        Initialise ()
        {
            ArrayList hierarchy = m_compareObjs.CommonHierarchy;
            
            foreach (Object typeObj in hierarchy) {
             
                Type type = typeObj as Type;
                Hashtable propsTable = m_compareObjs.GetPropsAtLevel(type);
                /// display it
                DisplayDiff(type.Name, propsTable);
            }
        }


        /// <summary>
        /// Drill down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void
        DataItemSelected (object sender, System.EventArgs e)
        {
            Debug.Assert((m_lvData.SelectedItems.Count > 1) == false);

            if (m_lvData.SelectedItems.Count != 0) {

                ListViewItem item = m_lvData.SelectedItems[0];
                if (item.SubItems.Count != 3)
                    return;

                Object[] objs = new Object[2];
                objs[0] = item.SubItems[1].Tag;
                objs[1] = item.SubItems[2].Tag;
                if (objs[0] == null || objs[1] == null)
                    return;

                EntityDiff dbox = new EntityDiff(objs[0], objs[1]);
                AcadApp.Application.ShowModalDialog(dbox);
            }
        }

        /// <summary>
        /// Display the diff between the properties
        /// of the two objects
        /// </summary>
        /// <param name="listViewItem">class separator string</param>
        /// <param name="propTable"> 
        /// key   --> property name
        /// value --> an arraylist of 2 items, each item containing 
        /// the value of the current property for the respective object
        /// </param>
        public void
        DisplayDiff (string listViewItem, Hashtable propTable)
        {
            if (propTable == null)
                return;

            m_lvData.BeginUpdate();

            System.Drawing.Font boldFont = new System.Drawing.Font(m_lvData.Font, System.Drawing.FontStyle.Bold);
            string text = string.Format("--- {0} ---", listViewItem);
            ListViewItem lvItemSeparator = new ListViewItem(text);
            /// show class separator
            lvItemSeparator.BackColor = Color.LightBlue;

            m_lvData.Items.Add(lvItemSeparator);

            IDictionaryEnumerator iDictEnum = propTable.GetEnumerator();

            while (iDictEnum.MoveNext()) {

                DictionaryEntry dictEntry = (DictionaryEntry)iDictEnum.Current;
                string key = dictEntry.Key as string;
                ArrayList arrList = dictEntry.Value as ArrayList;
                object obj1 = arrList[0];
                object obj2 = arrList[1];

                ListViewItem lvItem = new ListViewItem(key);

                ListViewItem.ListViewSubItem lvsItem1 = new ListViewItem.ListViewSubItem();
                lvsItem1.Text = obj1.ToString();

                ListViewItem.ListViewSubItem lvsItem2 = new ListViewItem.ListViewSubItem();
                lvsItem2.Text = obj2.ToString();

                lvItem.SubItems.Add(lvsItem1);
                lvItem.SubItems.Add(lvsItem2);

                Type type = null;
                if (obj1.GetType() == typeof(System.DBNull)) {
                    type = obj2.GetType();
                }
                else
                    type = obj1.GetType();
                
                /// provide a drill down only on worthwhile data
                if (IsDrillDown(type)) {

                    lvsItem1.Tag = obj1;
                    lvsItem2.Tag = obj2;

                    /// visual cue to drill down
                    lvItem.Font = boldFont;
                }

                /// this may happen when there is a collection object comparison
                if (obj1.GetType() != obj2.GetType()) {
                    /// we know this is diff., so highlight it
                    lvItem.BackColor = Color.Red;
                }
                else {
                    /// regular occurence of unequal objects
                    if (!obj1.Equals(obj2)) {
                        /// we know this is diff., so highlight it
                        lvItem.BackColor = Color.Red;
                    }
                }

                m_lvData.Items.Add(lvItem);
            }

            m_lvData.EndUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Boolean
        IsDrillDown (Type type)
        {
            if (type.IsPrimitive == false &&
                type.IsEnum == false &&
                type != typeof(String) &&
                type != typeof(DBNull)) {
                return true;
            }
            return false;
        }
    }
}



