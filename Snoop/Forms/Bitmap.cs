
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

namespace MgdDbg.Snoop.Forms
{
	/// <summary>
	/// Summary description for Icon.
	/// </summary>
	public class Bitmap : System.Windows.Forms.Form
	{
        private System.Windows.Forms.PictureBox m_picBox;
        private System.Windows.Forms.Button     m_bnOk;
        
		/// <summary>
		/// Required designer variable.
		/// </summary>
		
		private System.ComponentModel.Container components = null;

		public
		Bitmap(System.Drawing.Bitmap bitmap)
		{
			InitializeComponent();

            m_picBox.SizeMode = PictureBoxSizeMode.AutoSize;
            m_picBox.Image = bitmap;
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
            this.m_picBox = new System.Windows.Forms.PictureBox();
            this.m_bnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_picBox
            // 
            this.m_picBox.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.m_picBox.Location = new System.Drawing.Point(16, 24);
            this.m_picBox.Name = "m_picBox";
            this.m_picBox.Size = new System.Drawing.Size(416, 280);
            this.m_picBox.TabIndex = 0;
            this.m_picBox.TabStop = false;
            // 
            // m_bnOk
            // 
            this.m_bnOk.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.m_bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_bnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_bnOk.Location = new System.Drawing.Point(352, 320);
            this.m_bnOk.Name = "m_bnOk";
            this.m_bnOk.TabIndex = 1;
            this.m_bnOk.Text = "OK";
            // 
            // Bitmap
            // 
            this.AcceptButton = this.m_bnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_bnOk;
            this.ClientSize = new System.Drawing.Size(450, 360);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.m_bnOk,
                                                                          this.m_picBox});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Bitmap";
            this.Text = "Bitmap";
            this.ResumeLayout(false);

        }
		#endregion
	}
}
