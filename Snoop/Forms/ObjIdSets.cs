
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
	/// Summary description for ObjIdSets.
	/// </summary>
	public class ObjIdSets : System.Windows.Forms.Form
	{
        private System.Windows.Forms.ListView       m_lvObjIdSets;
        private System.Windows.Forms.ColumnHeader   m_lvColName;
        private System.Windows.Forms.Button         m_bnOk;
        private System.Windows.Forms.Button         m_bnNew;
        private System.Windows.Forms.Button         m_bnShow;
        private System.Windows.Forms.Button         m_bnSnoop;
        private System.Windows.Forms.Button         m_bnDelete;
        
        static private ArrayList                    m_objIdSets = new ArrayList();
        
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ObjIdSets()
		{
			InitializeComponent();
			Display();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
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
            this.m_lvObjIdSets = new System.Windows.Forms.ListView();
            this.m_lvColName = new System.Windows.Forms.ColumnHeader();
            this.m_bnOk = new System.Windows.Forms.Button();
            this.m_bnNew = new System.Windows.Forms.Button();
            this.m_bnShow = new System.Windows.Forms.Button();
            this.m_bnSnoop = new System.Windows.Forms.Button();
            this.m_bnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lvObjIdSets
            // 
            this.m_lvObjIdSets.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.m_lvObjIdSets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                            this.m_lvColName});
            this.m_lvObjIdSets.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvObjIdSets.HideSelection = false;
            this.m_lvObjIdSets.LabelEdit = true;
            this.m_lvObjIdSets.Location = new System.Drawing.Point(16, 24);
            this.m_lvObjIdSets.MultiSelect = false;
            this.m_lvObjIdSets.Name = "m_lvObjIdSets";
            this.m_lvObjIdSets.Size = new System.Drawing.Size(336, 376);
            this.m_lvObjIdSets.TabIndex = 1;
            this.m_lvObjIdSets.View = System.Windows.Forms.View.List;
            this.m_lvObjIdSets.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.OnAfterLabelEdit);
            // 
            // m_lvColName
            // 
            this.m_lvColName.Text = "Name";
            this.m_lvColName.Width = 400;
            // 
            // m_bnOk
            // 
            this.m_bnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_bnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_bnOk.Location = new System.Drawing.Point(195, 424);
            this.m_bnOk.Name = "m_bnOk";
            this.m_bnOk.TabIndex = 2;
            this.m_bnOk.Text = "OK";
            // 
            // m_bnNew
            // 
            this.m_bnNew.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.m_bnNew.Location = new System.Drawing.Point(368, 104);
            this.m_bnNew.Name = "m_bnNew";
            this.m_bnNew.TabIndex = 3;
            this.m_bnNew.Text = "New";
            this.m_bnNew.Click += new System.EventHandler(this.OnBnNew);
            // 
            // m_bnShow
            // 
            this.m_bnShow.Location = new System.Drawing.Point(368, 240);
            this.m_bnShow.Name = "m_bnShow";
            this.m_bnShow.TabIndex = 4;
            this.m_bnShow.Text = "Show...";
            this.m_bnShow.Click += new System.EventHandler(this.OnBnShow);
            // 
            // m_bnSnoop
            // 
            this.m_bnSnoop.Location = new System.Drawing.Point(368, 272);
            this.m_bnSnoop.Name = "m_bnSnoop";
            this.m_bnSnoop.TabIndex = 5;
            this.m_bnSnoop.Text = "Snoop...";
            this.m_bnSnoop.Click += new System.EventHandler(this.OnBnSnoop);
            // 
            // m_bnDelete
            // 
            this.m_bnDelete.Location = new System.Drawing.Point(368, 136);
            this.m_bnDelete.Name = "m_bnDelete";
            this.m_bnDelete.TabIndex = 6;
            this.m_bnDelete.Text = "Delete";
            this.m_bnDelete.Click += new System.EventHandler(this.OnBnDelete);
            // 
            // ObjIdSets
            // 
            this.AcceptButton = this.m_bnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_bnOk;
            this.ClientSize = new System.Drawing.Size(464, 463);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.m_bnDelete,
                                                                          this.m_bnSnoop,
                                                                          this.m_bnShow,
                                                                          this.m_bnNew,
                                                                          this.m_bnOk,
                                                                          this.m_lvObjIdSets});
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObjIdSets";
            this.ShowInTaskbar = false;
            this.Text = "ObjIdSets";
            this.ResumeLayout(false);

        }
		#endregion

        public void
        Display()
        {
            m_lvObjIdSets.BeginUpdate();
            m_lvObjIdSets.Items.Clear();
            
            foreach (Snoop.ObjIdSet tmpSet in m_objIdSets) {
                ListViewItem lvItem = new ListViewItem(tmpSet.Name);
                lvItem.Tag = tmpSet;
                m_lvObjIdSets.Items.Add(lvItem);
            }
            
            m_lvObjIdSets.EndUpdate();
        }
        
        private void
        OnAfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
        {
        
        }

        private void
        OnBnNew(object sender, System.EventArgs e)
        {
            string setName;
            
            while (true) {
                int index = 1;
                setName = string.Format("Set {0}", index);
                bool found = false;
                
                foreach (ObjIdSet tmpSet in m_objIdSets) {
                    if (tmpSet.Name == setName) {
                        found = true;
                        break;
                    }
                }
                
                if (!found) {
                    Snoop.ObjIdSet newSet = new Snoop.ObjIdSet(setName);
                    m_objIdSets.Add(newSet);
                    break;
                }
            }
            
            Display();
        }

        private void
        OnBnShow(object sender, System.EventArgs e)
        {
            Snoop.ObjIdSet curSet = GetCurrentSet();
            if (curSet != null) {
                using (TransactionHelper trHlp = new TransactionHelper(curSet.Db)) {
                    trHlp.Start();
                    
                    Snoop.Forms.DBObjects form = new Snoop.Forms.DBObjects(curSet.Set, trHlp);
                    form.ShowDialog();
                    
                    trHlp.Commit();
                }
            }
        }

        private void
        OnBnSnoop(object sender, System.EventArgs e)
        {
            Snoop.ObjIdSet curSet = GetCurrentSet();
            if (curSet != null) {
                using (TransactionHelper trHlp = new TransactionHelper(curSet.Db)) {
                    trHlp.Start();
                    
                    Snoop.Forms.Database form = new Snoop.Forms.Database(curSet.Db, trHlp);
                    form.ShowDialog();
                    
                    trHlp.Commit();
                }
            }
        }
        
        public Snoop.ObjIdSet
        GetCurrentSet()
        {
            Debug.Assert((m_lvObjIdSets.SelectedItems.Count > 1) == false);
                
            if (m_lvObjIdSets.SelectedItems.Count != 0) {
                Snoop.ObjIdSet tmpSet = (Snoop.ObjIdSet)m_lvObjIdSets.SelectedItems[0].Tag;
                return tmpSet;
            }
            
            return null;
        }

        private void
        OnBnDelete(object sender, System.EventArgs e)
        {
            Snoop.ObjIdSet curSet = GetCurrentSet();
            if (curSet != null)
                m_objIdSets.Remove(curSet);
        }
   
	}
}
