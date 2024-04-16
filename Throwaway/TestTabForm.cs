
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
	/// Summary description for TestTabForm.
	/// </summary>
	public class TestTabForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.TabControl m_tabCtrl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip contextMenu1;
        private System.Windows.Forms.ToolStripMenuItem menuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItem2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TestTabForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.m_tabCtrl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip();
            this.menuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tabCtrl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabCtrl
            // 
            this.m_tabCtrl.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.m_tabCtrl.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.tabPage1,
                                                                                    this.tabPage2});
            this.m_tabCtrl.Location = new System.Drawing.Point(16, 16);
            this.m_tabCtrl.Name = "m_tabCtrl";
            this.m_tabCtrl.SelectedIndex = 0;
            this.m_tabCtrl.Size = new System.Drawing.Size(680, 392);
            this.m_tabCtrl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.ContextMenuStrip = this.contextMenu1;
            this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.button1});
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(672, 366);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.button2});
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(672, 366);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(32, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "button2";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // contextMenu1
            // 

            var ms = new MenuStrip();

            ms.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
                                                                                         this.menuItem1,
                                                                                         this.menuItem2});
            this.ContextMenuStrip = contextMenu1;
            this.contextMenu1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu1_Popup);
            // 
            // menuItem1
            // 
           // this.menuItem1.Index = 0;
            this.menuItem1.Text = "Add to Browse Set";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
           // this.menuItem2.Index = 1;
            this.menuItem2.Text = "Show Object Id Info...";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // TestTabForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(712, 471);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.m_tabCtrl});
            this.Name = "TestTabForm";
            this.Text = "TestTabForm";
            this.m_tabCtrl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        private void button1_Click(object sender, System.EventArgs e) {
            MessageBox.Show("I came from Tab 1");
        }

        private void button2_Click(object sender, System.EventArgs e) {
            MessageBox.Show("I came from Tab 2");
        }

        private void contextMenu1_Popup(object sender, System.ComponentModel.CancelEventArgs e) {
            MessageBox.Show(sender.ToString());
        }

        private void menuItem1_Click(object sender, System.EventArgs e) {
            MessageBox.Show("Context menu event 1");
        }

        private void menuItem2_Click(object sender, System.EventArgs e) {
            MessageBox.Show("Context menu event 2");
        }
	}
}
