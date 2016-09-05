
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
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Printing;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;

using MgdDbg.Utils;

namespace MgdDbg.Snoop
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>
	public class Utils
	{
		public Utils()
		{
		}
		
		    // static ObjIdSet that Snoop forms use to collect a set.  If the Snoop dialogs
		    // are being used to collect a set, this will temporarily be set to something.
		    // After the form is finished it will get set back to null for the normal snoop
		    // contexts.
		private static ObjIdSet     m_snoopObjSet = null;
		
		public static ObjIdSet
		CurrentSnoopSet
		{
		    get { return m_snoopObjSet;  }
		}
		
        /// <summary>
        /// Given a ListView to display the data in, clear it out and add all the
        /// data that has been collected.  For things that have a DrillDown, change
        /// the font to Bold so the user knows they can select it.  For things that
        /// are a Separator, change the background color.
        /// </summary>
        /// <param name="m_lvData">The ListView UI control to draw into</param>
        /// <param name="snoopCollector">The collected data about an object</param>
        
        public static void
        Display(ListView lvCur, Snoop.Collectors.Collector snoopCollector)
        {
            lvCur.BeginUpdate();
            lvCur.Items.Clear();
            
            System.Drawing.Font oldFont = lvCur.Font;
            System.Drawing.FontStyle newStyle = lvCur.Font.Style ^ System.Drawing.FontStyle.Bold;
            System.Drawing.Font boldFont = new System.Drawing.Font(oldFont, newStyle);
            
            for (int i=0; i < snoopCollector.Data().Count; i++) {
                Snoop.Data.Data tmpSnoopData = (Snoop.Data.Data)snoopCollector.Data()[i];
                                
                    // if it is a class separator, then color the background differently
                    // and don't add a SubItem for the "Field" value
                if (tmpSnoopData.IsSeparator) {
                    ListViewItem lvItem = new ListViewItem(tmpSnoopData.StrValue());
                    
                    if (tmpSnoopData is Snoop.Data.ClassSeparator)
                        lvItem.BackColor = Color.LightBlue;
                    else
                        lvItem.BackColor = Color.Khaki;
                        
                    lvItem.Tag = tmpSnoopData;
                    lvCur.Items.Add(lvItem);
                }
                else {
                    ListViewItem lvItem = new ListViewItem(tmpSnoopData.Label);
                    lvItem.SubItems.Add(tmpSnoopData.StrValue());
                    
                    if (tmpSnoopData.HasDrillDown) {
                        ListViewItem.ListViewSubItem sItem = (ListViewItem.ListViewSubItem)lvItem.SubItems[0];
                        sItem.Font = boldFont;
                    }

                    if (tmpSnoopData.IsError == true)
                    {
                        lvItem.ForeColor = Color.Red;
                    }

                    lvItem.Tag = tmpSnoopData;
                    lvCur.Items.Add(lvItem);
                }    
            }
            
            lvCur.EndUpdate();
        }
        
        /// <summary>
        /// A Snoop.Data item was selected in the ListView. Call its DrillDown() function to 
        /// get more detailed info about it.
        /// </summary>
        /// <param name="m_lvData">The ListView control in question</param>
        
        public static void
        DataItemSelected(ListView lvCur)
        {
            Debug.Assert((lvCur.SelectedItems.Count > 1) == false);
                
            if (lvCur.SelectedItems.Count != 0) {
                Snoop.Data.Data tmpSnoopData = (Snoop.Data.Data)lvCur.SelectedItems[0].Tag;
                tmpSnoopData.DrillDown();
            }
        }
        
        /// <summary>
        /// Show information about the ObjectId itself (not what the ObjectId refers to).
        /// </summary>
        /// <param name="objId">ObjectId to display info for</param>
        
        public static void
        ShowObjIdInfo(ObjectId objId)
        {
            Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(objId);
            dbox.ShowDialog();
        }
        
        /// <summary>
        /// Given an ObjectId, allow the user to browse its properties based on Reflection only.
        /// </summary>
        /// <param name="objId">Object to browse</param>
        
        public static void
        BrowseReflection(ObjectId objId)
        {
            if (objId.IsNull) {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("ObjectId == null");
                return;
            }
            
            if (objId.IsValid == false) {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("ObjectId not valid");
                return;
            }
            
            using (TransactionHelper tr = new TransactionHelper(objId.Database)) {
                tr.Start();

                DBObject tmpObj = tr.Transaction.GetObject(objId, OpenMode.ForRead);
                MgdDbg.Snoop.Forms.GenericPropGrid pgForm = new MgdDbg.Snoop.Forms.GenericPropGrid(tmpObj);
                pgForm.Text = MgdDbg.Utils.AcadUi.ObjToTypeAndHandleStr(tmpObj);
                pgForm.ShowDialog();
                
                tr.Commit();
            }
        }

        /// <summary>
        /// Given an ObjectId, allow the user to browse its properties based on Reflection only.
        /// </summary>
        /// <param name="objId">Object to browse</param>
        
        public static void
        BrowseReflection(Object obj)
        {
            if (obj == null) {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Object == null");
                return;
            }
            
            MgdDbg.Snoop.Forms.GenericPropGrid pgForm = new MgdDbg.Snoop.Forms.GenericPropGrid(obj);
            pgForm.ShowDialog();
        }
        
        /// <summary>
        /// Ask the user for an ObjIdSet.  First, they are allowed to select graphical
        /// objects, SsGet() style.  Then, they are prompted to search for non-graphic
        /// objects via the Snoop.Database form.
        /// </summary>
        /// <returns>The collected ObjIdSet or null if the user canceled</returns>
        /// 
        public static Snoop.ObjIdSet
        GetSnoopSet()
        {
            try {
                Snoop.ObjIdSet objSet = new Snoop.ObjIdSet("*A");   // TBD: how to deal with named sets? ... later...
                
                    // select the graphic objects they want               
                PromptSelectionOptions selOpts = new PromptSelectionOptions();
                selOpts.MessageForAdding = "Select graphical objects for test (or RETURN for none)";
                Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                PromptSelectionResult res = ed.GetSelection(selOpts);
                if (res.Status == PromptStatus.OK) {
                    foreach (ObjectId objId in res.Value.GetObjectIds())     // set these as the original, let dialog add the rest
                        objSet.AddToSet(objId);
                }
                else if (res.Status == PromptStatus.Error) {
                    ;       // empty selection set
                }
                else {
                    return null;
                }

                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Select non-graphical objects by browsing, right-click on item, choose \"Add to Snoop Set\".");

                using (TransactionHelper trHlp = new TransactionHelper(MgdDbg.Utils.Db.GetCurDwg())) {
                    trHlp.Start();
                    
                    Snoop.Forms.Database form = new Snoop.Forms.Database(trHlp.Database, trHlp);
                        // temporarily hook up the snoop set to the snoop browse dialogs.
                    m_snoopObjSet = objSet;
                    
                    if (form.ShowDialog() == DialogResult.OK) {
                        trHlp.Commit();
                        return objSet;
                    }
                    else {
                        trHlp.Abort();
                        return null;
                    }
                }
            }
            finally {
                m_snoopObjSet = null;   // reset to null for next invocation
            }
        }

		/// <summary>
		/// Format a type string to represent a given object.
		/// </summary>
		/// <param name="obj">Object to label</param>
		/// <returns>The formatted type string</returns>
		
		public static string
        ObjToTypeStr(System.Object obj)
        {
			if (obj == null)
				return "(null)";

			return string.Format("< {0} >", obj.GetType().Name);
        }

        public static string
        BytesToHexStr(byte[] bytes)
        {
            string hexStr = "";

            for (int j=0; j<bytes.Length; j++) {
                hexStr += string.Format("{0:X}", bytes[j]);
            }
            return hexStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="lv"></param>
        public static void
        CopyToClipboard (ListView lv)
        {
            if (lv.Items.Count == 0) {
                Clipboard.Clear();
                return;
            }

            //First find the longest piece of text in the Field column
            //
            Int32 maxField = 0;
            Int32 maxValue = 0;

            foreach (ListViewItem item in lv.Items) {
                if (item.Text.Length > maxField) {
                    maxField = item.Text.Length;
                }
                if ((item.SubItems.Count > 1) && (item.SubItems[1].Text.Length > maxValue)) {
                    maxValue = item.SubItems[1].Text.Length;
                }
            }

            String headerFormat = String.Format("{{0,{0}}}----{1}\r\n", maxField, new String('-', maxValue));
            String tabbedFormat = String.Format("{{0,{0}}}    {{1}}\r\n", maxField);

            System.Text.StringBuilder bldr = new System.Text.StringBuilder();

            foreach (ListViewItem item in lv.Items) {
                if (item.SubItems.Count == 1) {
                    String tmp = item.Text;
                    if (item.Text.Length < maxField) {
                        tmp = item.Text.PadLeft(item.Text.Length + (maxField - item.Text.Length), '-');
                    }

                    bldr.AppendFormat(headerFormat, tmp);
                }
                else if (item.SubItems.Count > 1) {
                    bldr.AppendFormat(tabbedFormat, item.Text, item.SubItems[1].Text);
                }
            }

            String txt = bldr.ToString();
            if (String.IsNullOrEmpty(txt) == false) {
                Clipboard.SetDataObject(txt);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lvItem"></param>
        public static void
        CopyToClipboard (ListViewItem lvItem)
        {
            if (lvItem.SubItems.Count > 1) {
                Clipboard.SetDataObject(lvItem.SubItems[1].Text);
            }
            else {
                Clipboard.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="lv"></param>
        /// <param name="e"></param>
        /// <param name="maxFieldWidth"></param>
        /// <param name="maxValueWidth"></param>
        /// <param name="currentItem"></param>
        /// <returns></returns>
        public static Int32
        Print (String title, ListView lv, System.Drawing.Printing.PrintPageEventArgs e, Int32 maxFieldWidth, Int32 maxValueWidth, Int32 currentItem)
        {
            Single linesPerPage = 0;
            Single yPos = 0;
            Single leftMargin = e.MarginBounds.Left + ((e.MarginBounds.Width - (maxFieldWidth + maxValueWidth)) / 2);
            Single topMargin = e.MarginBounds.Top;
            Single fontHeight = lv.Font.GetHeight(e.Graphics);
            Int32 count = 0;
            String line = null;
            ListViewItem item;
            SolidBrush backgroundBrush;
            SolidBrush subbackgroundBrush;
            SolidBrush textBrush;
            RectangleF rect;
            StringFormat centerFormat = new StringFormat();
            StringFormat fieldFormat = new StringFormat();
            StringFormat valueFormat = new StringFormat();

            centerFormat.Alignment = StringAlignment.Center;
            fieldFormat.Alignment = HorizontalAlignmentToStringAligment(lv.Columns[0].TextAlign);
            valueFormat.Alignment = HorizontalAlignmentToStringAligment(lv.Columns[1].TextAlign);

            //Draw the title of the list.
            //
            rect = new RectangleF(leftMargin, topMargin, maxFieldWidth + maxValueWidth, fontHeight);
            e.Graphics.DrawString(title, lv.Font, Brushes.Black, rect, centerFormat);

            //Update the count so that we are giving ourselves a line between the title and the list.
            //
            count = 2;

            //Calculate the number of lines per page
            //
            linesPerPage = e.MarginBounds.Height / fontHeight;

            while ((count < linesPerPage) && (currentItem < lv.Items.Count))
            {
                item = lv.Items[currentItem];
                line = item.Text;
                yPos = topMargin + (count * fontHeight);

                backgroundBrush = new SolidBrush(item.BackColor);
                textBrush = new SolidBrush(item.ForeColor);

                rect = new RectangleF(leftMargin, yPos, maxFieldWidth, fontHeight);

                e.Graphics.FillRectangle(backgroundBrush, rect);
                e.Graphics.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);

                //Draw the field portion of the list view item
                //
                e.Graphics.DrawString(" " + item.Text, item.Font, textBrush, rect, fieldFormat);

                //Draw the value portion of the list view item
                //
                rect = new RectangleF(leftMargin + maxFieldWidth, yPos, maxValueWidth, fontHeight);
                if (item.SubItems.Count > 1)
                {
                    subbackgroundBrush = new SolidBrush(item.SubItems[1].BackColor);

                    e.Graphics.FillRectangle(subbackgroundBrush, rect);
                    e.Graphics.DrawString(" " + item.SubItems[1].Text, item.Font, textBrush, rect, valueFormat);
                }
                else
                {
                    e.Graphics.FillRectangle(backgroundBrush, rect);
                }
                e.Graphics.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);

                count++;
                currentItem++;
            }

            if (currentItem < lv.Items.Count)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                currentItem = 0;
            }

            return currentItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ha"></param>
        /// <returns></returns>
        public static StringAlignment
        HorizontalAlignmentToStringAligment (HorizontalAlignment ha)
        {
            switch (ha)
            {
            case HorizontalAlignment.Center:
                return StringAlignment.Center;
            case HorizontalAlignment.Left:
                return StringAlignment.Near;
            case HorizontalAlignment.Right:
                return StringAlignment.Far;
            default:
                return StringAlignment.Near;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public static Int32[]
        GetMaximumColumnWidths (ListView lv)
        {
            Int32 index = 0;
            Int32[] widthArray = new Int32[lv.Columns.Count];

            foreach (ColumnHeader col in lv.Columns)
            {
                widthArray[index] = col.Width;
                index++;
            }

            System.Drawing.Graphics g = lv.CreateGraphics();
            Int32 offset = Convert.ToInt32(Math.Ceiling(g.MeasureString(" ", lv.Font).Width));
            Int32 width = 0;

            foreach (ListViewItem item in lv.Items)
            {
                index = 0;

                foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                {
                    width = Convert.ToInt32(Math.Ceiling(g.MeasureString(subItem.Text, item.Font).Width)) + offset;
                    if (width > widthArray[index])
                    {
                        widthArray[index] = width;
                    }
                    index++;
                }
            }

            g.Dispose();

            return widthArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TreeNode
        GetRootNode (TreeNode node)
        {
            if (node.Parent == null)
            {
                return node;
            }
            return GetRootNode(node.Parent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static String
        GetPrintDocumentName (TreeNode node)
        {
            TreeNode root = GetRootNode(node);
            String str = root.Tag as String;

            if (str != null)
            {
                return System.IO.Path.GetFileNameWithoutExtension((String)root.Tag);
            }
            return String.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tv"></param>
        /// <param name="lv"></param>
        /// <param name="widthArray"></param>
        public static void
        UpdatePrintSettings (PrintDocument doc, TreeView tv, ListView lv, ref Int32[] widthArray)
        {
            if (tv.SelectedNode == null)
            {
                return;
            }
            doc.DocumentName = Utils.GetPrintDocumentName(tv.SelectedNode);
            widthArray = Utils.GetMaximumColumnWidths(lv);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tv"></param>
        /// <param name="lv"></param>
        /// <param name="widthArray"></param>
        public static void
        UpdatePrintSettings (ListView lv, ref Int32[] widthArray)
        {
            widthArray = Utils.GetMaximumColumnWidths(lv);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dlg"></param>
        /// <param name="doc"></param>
        /// <param name="tv"></param>
        /// <param name="lv"></param>
        public static void
        PrintMenuItemClick (PrintDialog dlg, TreeView tv)
        {
            if (tv.SelectedNode == null)
            {
                MessageBox.Show(tv.Parent, "Please select a node in the tree to print.", "MgdDbg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dlg.ShowDialog(tv.Parent) == DialogResult.OK)
            {
                dlg.Document.Print();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dlg"></param>
        /// <param name="doc"></param>
        /// <param name="tv"></param>
        /// <param name="lv"></param>
        public static void
        PrintMenuItemClick (PrintDialog dlg)
        {
            dlg.Document.Print();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dlg"></param>
        /// <param name="doc"></param>
        /// <param name="tv"></param>
        public static void
        PrintPreviewMenuItemClick (PrintPreviewDialog dlg, TreeView tv)
        {
            if (tv.SelectedNode == null)
            {
                MessageBox.Show(tv.Parent, "Please select a node in the tree to print.", "MgdDbg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (PrinterSettings.InstalledPrinters.Count > 0) {
                dlg.ShowDialog(tv.Parent);
            }
            else {
                MessageBox.Show("No installed printers found", "MgdDbg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dlg"></param>
        /// <param name="doc"></param>
        /// <param name="tv"></param>
        public static void
        PrintPreviewMenuItemClick (PrintPreviewDialog dlg, ListView lv)
        {
            if (PrinterSettings.InstalledPrinters.Count > 0) {
                dlg.ShowDialog(lv.Parent);
            }
            else {
                MessageBox.Show("No installed printers found", "MgdDbg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }      
        }	
    }
}
