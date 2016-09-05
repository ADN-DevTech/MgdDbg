
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MgdDbg.Snoop.Forms
{
	/// <summary>
	/// Summary description for ClassesComp.
	/// </summary>
	public class ClassesComp : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button         m_bnOK;
        private System.Windows.Forms.Button         m_bnCancel;
        private System.Windows.Forms.ColumnHeader   m_colName;
        private System.Windows.Forms.ColumnHeader   m_colNamSpc;
        private System.Windows.Forms.ColumnHeader   m_colPrnt;
        private System.Windows.Forms.ListView       m_lvData;

        private MgdDbg.Utils.ListViewColumnSorter   m_colSorter;
        private ArrayList   m_classArray = new ArrayList();
        private System.Type m_selectedClass = null;
        private ColumnHeader m_colAss;
        private ColumnHeader m_colPub;
        private ColumnHeader m_colSeal;
        private ColumnHeader m_colAbs;
        private ColumnHeader m_colGUID;
        
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <summary>
        /// 
        /// </summary>
		public
		ClassesComp()
		{
			InitializeComponent();
            m_colSorter = new MgdDbg.Utils.ListViewColumnSorter();
            m_lvData.ListViewItemSorter = m_colSorter;
			BuildClassList();
			Display();
		}

        /// <summary>
        /// If somebody has already done the hard work, just give
        /// me the arraylist.
        /// </summary>
        /// <param name="tempArray"></param>
        public
        ClassesComp(ArrayList tempArray)
        {
            InitializeComponent();
            m_colSorter = new MgdDbg.Utils.ListViewColumnSorter();
            m_lvData.ListViewItemSorter = m_colSorter;
            m_classArray = tempArray;
            Display();
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void
		Dispose(bool disposing)
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
		private void InitializeComponent()
		{
            this.m_lvData = new System.Windows.Forms.ListView();
            this.m_colName = new System.Windows.Forms.ColumnHeader();
            this.m_colNamSpc = new System.Windows.Forms.ColumnHeader();
            this.m_colPrnt = new System.Windows.Forms.ColumnHeader();
            this.m_colAss = new System.Windows.Forms.ColumnHeader();
            this.m_colPub = new System.Windows.Forms.ColumnHeader();
            this.m_colSeal = new System.Windows.Forms.ColumnHeader();
            this.m_colAbs = new System.Windows.Forms.ColumnHeader();
            this.m_colGUID = new System.Windows.Forms.ColumnHeader();
            this.m_bnOK = new System.Windows.Forms.Button();
            this.m_bnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lvData
            // 
            this.m_lvData.AllowColumnReorder = true;
            this.m_lvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colName,
            this.m_colNamSpc,
            this.m_colPrnt,
            this.m_colAss,
            this.m_colPub,
            this.m_colSeal,
            this.m_colAbs,
            this.m_colGUID});
            this.m_lvData.FullRowSelect = true;
            this.m_lvData.GridLines = true;
            this.m_lvData.HideSelection = false;
            this.m_lvData.Location = new System.Drawing.Point(12, 12);
            this.m_lvData.MultiSelect = false;
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.Size = new System.Drawing.Size(888, 496);
            this.m_lvData.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.m_lvData.TabIndex = 0;
            this.m_lvData.UseCompatibleStateImageBehavior = false;
            this.m_lvData.View = System.Windows.Forms.View.Details;
            this.m_lvData.SelectedIndexChanged += new System.EventHandler(this.ItemSelected);
            this.m_lvData.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lvData_ColumnClick);
            // 
            // m_colName
            // 
            this.m_colName.Text = "Class Name";
            this.m_colName.Width = 168;
            // 
            // m_colNamSpc
            // 
            this.m_colNamSpc.Text = "NameSpace";
            this.m_colNamSpc.Width = 144;
            // 
            // m_colPrnt
            // 
            this.m_colPrnt.Text = "Parent Name";
            this.m_colPrnt.Width = 152;
            // 
            // m_colAss
            // 
            this.m_colAss.Text = "Assembly Qualified Name";
            // 
            // m_colPub
            // 
            this.m_colPub.Text = "IsPublic";
            this.m_colPub.Width = 64;
            // 
            // m_colSeal
            // 
            this.m_colSeal.Text = "IsSealed";
            this.m_colSeal.Width = 65;
            // 
            // m_colAbs
            // 
            this.m_colAbs.Text = "IsAbstract";
            this.m_colAbs.Width = 73;
            // 
            // m_colGUID
            // 
            this.m_colGUID.Text = "GUID";
            this.m_colGUID.Width = 163;
            // 
            // m_bnOK
            // 
            this.m_bnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_bnOK.Location = new System.Drawing.Point(379, 528);
            this.m_bnOK.Name = "m_bnOK";
            this.m_bnOK.Size = new System.Drawing.Size(75, 23);
            this.m_bnOK.TabIndex = 1;
            this.m_bnOK.Text = "OK";
            // 
            // m_bnCancel
            // 
            this.m_bnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bnCancel.Location = new System.Drawing.Point(467, 528);
            this.m_bnCancel.Name = "m_bnCancel";
            this.m_bnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_bnCancel.TabIndex = 2;
            this.m_bnCancel.Text = "Cancel";
            // 
            // ClassesComp
            // 
            this.AcceptButton = this.m_bnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_bnCancel;
            this.ClientSize = new System.Drawing.Size(920, 559);
            this.Controls.Add(this.m_bnCancel);
            this.Controls.Add(this.m_bnOK);
            this.Controls.Add(this.m_lvData);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClassesComp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Classes";
            this.ResumeLayout(false);

        }
		#endregion

        /// <summary>
        /// 
        /// </summary>
        public void
        BuildClassList()
        {
            try {

                /// These are dependencies that need to be preloaded 
                /// in order to load assemblies: "mscorlib", "acmgd" and "acdbmgd".
                /// This is because ReflectionOnlyLoad() does not take care of dependencies
                /// unlike Load() and we donot want to unnecessarily "load" these assemblies.
                /// Instead run with ReflectionOnlyLoad() which is designed for this very purpose.
                /// (TBD: Somehow the assembly names for System, System.Xml etc are not working. 
                ///  So for now I am using hard coded file names. Do away later. This works for now.
                /// jai 02.23.06)
                System.Reflection.Assembly.ReflectionOnlyLoadFrom("C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\System.Drawing.dll");
                System.Reflection.Assembly.ReflectionOnlyLoadFrom("C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\System.Windows.Forms.dll");
                System.Reflection.Assembly.ReflectionOnlyLoadFrom("C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\System.Configuration.dll");
                System.Reflection.Assembly.ReflectionOnlyLoadFrom("C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\System.Xml.dll");
                System.Reflection.Assembly.ReflectionOnlyLoadFrom("C:\\WINDOWS\\Microsoft.NET\\Framework\\v2.0.50727\\System.dll");

                System.Reflection.Assembly assembly1 = System.Reflection.Assembly.ReflectionOnlyLoad("mscorlib");
                System.Reflection.Assembly assembly2 = System.Reflection.Assembly.ReflectionOnlyLoad("acmgd");
                System.Reflection.Assembly assembly3 = System.Reflection.Assembly.ReflectionOnlyLoad("acdbmgd");
                System.Reflection.Assembly assembly4 = System.Reflection.Assembly.ReflectionOnlyLoadFrom("C:\\Kyoto\\bin\\debug\\AecBaseMgd.dll");
                System.Reflection.Assembly assembly5 = System.Reflection.Assembly.ReflectionOnlyLoadFrom("C:\\Kyoto\\bin\\debug\\AecArchMgd.dll");

                m_classArray.AddRange(assembly1.GetTypes());
                m_classArray.AddRange(assembly2.GetTypes());
                m_classArray.AddRange(assembly3.GetTypes());
                m_classArray.AddRange(assembly4.GetTypes());
                m_classArray.AddRange(assembly5.GetTypes());

                /// Currently only classes are being considered and the rest of the types
                /// (ex. Value Types) are being thrown away. However, something like 
                /// Autodesk.AutoCAD.EditorInput.PickPointDescriptor (a struct) is a value type
                /// and thus gets left out of this. Do we want everything instead(comes with a lot
                /// of unwanted stuff)??
                /// jai 02.24.06
                for (int i = 0; i < m_classArray.Count; i++) {
                    System.Type type = (System.Type)m_classArray[i];
                    if (!type.IsClass) {
                        m_classArray.RemoveAt(i);
                        i--;
                    }
                }
            }
            catch (System.Reflection.ReflectionTypeLoadException e) {
                Exception[] ex = e.LoaderExceptions;
                MessageBox.Show(e.Message);
            }
        }
    	
        /// <summary>
        /// Hook up the details in the list view
        /// </summary>
	    public void
	    Display()
	    {
	        m_lvData.BeginUpdate();
            m_lvData.Items.Clear();
            
            int len = m_classArray.Count;
            for (int i=0; i<len; i++) {
                System.Type tmpClass = (System.Type)m_classArray[i];

                if (tmpClass != null) {
                    ListViewItem lvItem = new ListViewItem(tmpClass.Name);
                    lvItem.SubItems.Add(tmpClass.Namespace);
                    if (tmpClass.BaseType != null)
                        lvItem.SubItems.Add(tmpClass.BaseType.Name);
                    else
                        lvItem.SubItems.Add("null");
                    lvItem.SubItems.Add(tmpClass.AssemblyQualifiedName);
                    lvItem.SubItems.Add(tmpClass.IsPublic.ToString());
                    lvItem.SubItems.Add(tmpClass.IsSealed.ToString());
                    lvItem.SubItems.Add(tmpClass.IsAbstract.ToString());
                    lvItem.SubItems.Add(tmpClass.GUID.ToString());
                    lvItem.Tag = tmpClass;

                    m_lvData.Items.Add(lvItem);
                }
	        }
	        
	        m_lvData.EndUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Type
        SelectedClass()
        {
            return m_selectedClass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void
        ItemSelected(object sender, System.EventArgs e)
        {
            Debug.Assert((m_lvData.SelectedItems.Count > 1) == false);
            
            if (m_lvData.SelectedItems.Count != 0)
                m_selectedClass = (System.Type)m_lvData.SelectedItems[0].Tag;
            else
                m_selectedClass = null;
        }

        private void m_lvData_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == m_colSorter.SortColumn) {
                // Reverse the current sort direction for this column.
                if (m_colSorter.Order == SortOrder.Ascending){
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
           m_lvData.Sort();

        }
    }
}