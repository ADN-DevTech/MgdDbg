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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AcDb = Autodesk.AutoCAD.DatabaseServices;
using MgdDbg.Utils;

namespace MgdDbg.Reactors.Forms {
    public partial class Details : Form {
        private AcDb.IdMapping                      m_idMap;
        private MgdDbg.Utils.ListViewColumnSorter   m_colSorter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleName"></param>
        /// <param name="idMap"></param>
        public Details(string titleName, AcDb.IdMapping idMap)
        {
            InitializeComponent();
            this.Name = titleName;
            this.Text = titleName;
            m_idMap = idMap;
            m_colSorter = new MgdDbg.Utils.ListViewColumnSorter();
            m_listViewMapItem.ListViewItemSorter = m_colSorter;
            DisplayMapProps();
            DisplayMapItems();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayMapProps()
        {
            ListViewItem item = new ListViewItem();
            item.Text = "Destination Database";
            try {
                item.SubItems.Add(AcadUi.DbToStr(m_idMap.DestinationDatabase));
            }
            catch (Autodesk.AutoCAD.Runtime.Exception e) {
                item.SubItems.Add(e.Message);
            }
            m_listViewMap.Items.Add(item);

            ListViewItem item1 = new ListViewItem();
            item1.Text = "Original Database";
            try {
                item.SubItems.Add(AcadUi.DbToStr(m_idMap.OriginalDatabase));
            }
            catch (Autodesk.AutoCAD.Runtime.Exception e) {
                item.SubItems.Add(e.Message);
            }
            m_listViewMap.Items.Add(item1);

            ListViewItem item2 = new ListViewItem();
            item2.Text = "Deep Clone Context";
            item2.SubItems.Add(m_idMap.DeepCloneContext.ToString());
            m_listViewMap.Items.Add(item2);

            ListViewItem item3 = new ListViewItem();
            item3.Text = "Duplicate Record Cloning";
            item3.SubItems.Add(m_idMap.DuplicateRecordCloning.ToString());
            m_listViewMap.Items.Add(item3);
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayMapItems()
        {
            System.Collections.IEnumerator iter = m_idMap.GetEnumerator();
            while (iter.MoveNext()) {
                AcDb.IdPair pair = (AcDb.IdPair)iter.Current;

                try {
                    ListViewItem item = new ListViewItem(AcadUi.ObjToTypeAndHandleStr(pair.Key));
                    item.SubItems.Add(pair.Key.ToString());
                    item.SubItems.Add(pair.Value.ToString());
                    item.SubItems.Add(pair.IsCloned.ToString());
                    item.SubItems.Add(pair.IsPrimary.ToString());
                    item.SubItems.Add(pair.IsOwnerTranslated.ToString());
                    item.Tag = pair;
                    m_listViewMapItem.Items.Add(item);
                }
                catch (Autodesk.AutoCAD.Runtime.Exception e) {
                    MessageBox.Show("Couldn't add a pair from the map: " + e.Message);
                }
            }
        }

        private void m_listViewMapItem_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == m_colSorter.SortColumn) {
                // Reverse the current sort direction for this column.
                if (m_colSorter.Order == SortOrder.Ascending) {
                    m_colSorter.Order = SortOrder.Descending;
                }
                else {
                    m_colSorter.Order = SortOrder.Ascending;
                }
            }
            else {
                // Set the column number that is to be sorted; default to ascending.
                m_colSorter.SortColumn = e.Column;
                m_colSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            m_listViewMapItem.Sort();
        }
    }
}