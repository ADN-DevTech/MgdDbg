
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
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MgdDbg
{
	/// <summary>
	/// Summary description for TestForm.
	/// </summary>
	public class TestForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.TreeView       m_tvEnts;
        private System.Windows.Forms.ListView       m_lvData;
        private System.Windows.Forms.Label          m_txtTreeLabel;
        private System.Windows.Forms.Button         m_bnOK;
        private System.Windows.Forms.ColumnHeader   m_lvCol_label;
        private System.Windows.Forms.ColumnHeader   m_lvCol_value;
        private System.Windows.Forms.TrackBar       m_trkBarOpacity;
        private System.Windows.Forms.Label          label1;
        
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TestForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            TreeNode newNode = new TreeNode("My root node dude");
            m_tvEnts.Nodes.Add(newNode);
            //m_tvEnts.SelectedNode = newNode;
            
            //m_tvEnts.SelectedNode.Nodes.Add(new TreeNode("Dufus"));
            newNode.Nodes.Add(new TreeNode("Dufus"));
            m_tvEnts.Nodes.Add(new TreeNode("Hobo Show"));
            m_tvEnts.ExpandAll();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_tvEnts = new System.Windows.Forms.TreeView();
            this.m_lvData = new System.Windows.Forms.ListView();
            this.m_lvCol_label = new System.Windows.Forms.ColumnHeader();
            this.m_lvCol_value = new System.Windows.Forms.ColumnHeader();
            this.m_txtTreeLabel = new System.Windows.Forms.Label();
            this.m_bnOK = new System.Windows.Forms.Button();
            this.m_trkBarOpacity = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_trkBarOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // m_tvEnts
            // 
            this.m_tvEnts.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.m_tvEnts.ForeColor = System.Drawing.Color.Red;
            this.m_tvEnts.ImageIndex = -1;
            this.m_tvEnts.Location = new System.Drawing.Point(16, 64);
            this.m_tvEnts.Name = "m_tvEnts";
            this.m_tvEnts.SelectedImageIndex = -1;
            this.m_tvEnts.Size = new System.Drawing.Size(240, 328);
            this.m_tvEnts.TabIndex = 0;
            this.m_tvEnts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeNodeSelected);
            // 
            // m_lvData
            // 
            this.m_lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                       this.m_lvCol_label,
                                                                                       this.m_lvCol_value});
            this.m_lvData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvData.Location = new System.Drawing.Point(272, 64);
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.Size = new System.Drawing.Size(456, 328);
            this.m_lvData.TabIndex = 1;
            // 
            // m_lvCol_label
            // 
            this.m_lvCol_label.Text = "Field";
            // 
            // m_lvCol_value
            // 
            this.m_lvCol_value.Text = "Value";
            // 
            // m_txtTreeLabel
            // 
            this.m_txtTreeLabel.Location = new System.Drawing.Point(16, 32);
            this.m_txtTreeLabel.Name = "m_txtTreeLabel";
            this.m_txtTreeLabel.Size = new System.Drawing.Size(240, 23);
            this.m_txtTreeLabel.TabIndex = 3;
            this.m_txtTreeLabel.Text = "Entities";
            this.m_txtTreeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_bnOK
            // 
            this.m_bnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bnOK.Location = new System.Drawing.Point(335, 480);
            this.m_bnOK.Name = "m_bnOK";
            this.m_bnOK.TabIndex = 2;
            this.m_bnOK.Text = "OK";
            // 
            // m_trkBarOpacity
            // 
            this.m_trkBarOpacity.BackColor = System.Drawing.Color.CornflowerBlue;
            this.m_trkBarOpacity.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_trkBarOpacity.Location = new System.Drawing.Point(72, 408);
            this.m_trkBarOpacity.Maximum = 100;
            this.m_trkBarOpacity.Minimum = 10;
            this.m_trkBarOpacity.Name = "m_trkBarOpacity";
            this.m_trkBarOpacity.Size = new System.Drawing.Size(232, 50);
            this.m_trkBarOpacity.TabIndex = 4;
            this.m_trkBarOpacity.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.m_trkBarOpacity.Value = 100;
            this.m_trkBarOpacity.ValueChanged += new System.EventHandler(this.ChangeOpacity);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Opacity";
            // 
            // TestForm
            // 
            this.AcceptButton = this.m_bnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_bnOK;
            this.ClientSize = new System.Drawing.Size(744, 511);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.label1,
                                                                          this.m_trkBarOpacity,
                                                                          this.m_txtTreeLabel,
                                                                          this.m_bnOK,
                                                                          this.m_lvData,
                                                                          this.m_tvEnts});
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TestForm";
            ((System.ComponentModel.ISupportInitialize)(this.m_trkBarOpacity)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        private void
        TreeNodeSelected(object sender, System.Windows.Forms.TreeViewEventArgs e) {
            //MessageBox.Show("Dude, we selected something!");
        }

        private void
        ChangeOpacity(object sender, System.EventArgs e) {
            this.Opacity = m_trkBarOpacity.Value / 100.0;
        }
	}
}
