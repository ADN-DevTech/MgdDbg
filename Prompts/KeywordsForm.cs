
// $Header: //Desktop/Source/TestTools/MgdDbg/Prompts/KeywordsForm.cs#1 $
// $Author: fergusja $ $DateTime: 2007/03/16 08:08:08 $ $Change: 68854 $
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

using Autodesk.AutoCAD.EditorInput;

namespace MgdDbg.Prompts
{
	/// <summary>
	/// Summary description for KeywordsForm.
	/// </summary>
	public class KeywordsForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button     m_bnOK;
        private System.Windows.Forms.Button     m_bnCancel;
        private System.Windows.Forms.Label      m_label1;
        private System.Windows.Forms.TextBox    m_ebGlobal;
        private System.Windows.Forms.Label      m_label2;
        private System.Windows.Forms.Label      m_label3;
        private System.Windows.Forms.TextBox    m_ebLocal;
        private System.Windows.Forms.TextBox    m_ebDisplay;
        private System.Windows.Forms.CheckBox   m_cbEnabled;
        private System.Windows.Forms.CheckBox   m_cbVisible;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private Keyword     m_kword = null;

		public
		KeywordsForm(Keyword kword)
		{
		    m_kword = kword;
		    
			InitializeComponent();
			
			m_ebGlobal.Text     = kword.GlobalName;
			m_ebLocal.Text      = kword.LocalName;
			m_ebDisplay.Text    = kword.DisplayName;
			m_cbEnabled.Checked = kword.Enabled;
			m_cbVisible.Checked = kword.Visible;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void
		Dispose( bool disposing )
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
            this.m_bnOK = new System.Windows.Forms.Button();
            this.m_bnCancel = new System.Windows.Forms.Button();
            this.m_label1 = new System.Windows.Forms.Label();
            this.m_ebGlobal = new System.Windows.Forms.TextBox();
            this.m_label2 = new System.Windows.Forms.Label();
            this.m_label3 = new System.Windows.Forms.Label();
            this.m_ebLocal = new System.Windows.Forms.TextBox();
            this.m_ebDisplay = new System.Windows.Forms.TextBox();
            this.m_cbEnabled = new System.Windows.Forms.CheckBox();
            this.m_cbVisible = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_bnOK
            // 
            this.m_bnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_bnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_bnOK.Location = new System.Drawing.Point(83, 128);
            this.m_bnOK.Name = "m_bnOK";
            this.m_bnOK.TabIndex = 0;
            this.m_bnOK.Text = "OK";
            this.m_bnOK.Click += new System.EventHandler(this.OnOk);
            // 
            // m_bnCancel
            // 
            this.m_bnCancel.CausesValidation = false;
            this.m_bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_bnCancel.Location = new System.Drawing.Point(171, 128);
            this.m_bnCancel.Name = "m_bnCancel";
            this.m_bnCancel.TabIndex = 1;
            this.m_bnCancel.Text = "Cancel";
            // 
            // m_label1
            // 
            this.m_label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_label1.Location = new System.Drawing.Point(24, 24);
            this.m_label1.Name = "m_label1";
            this.m_label1.Size = new System.Drawing.Size(48, 23);
            this.m_label1.TabIndex = 2;
            this.m_label1.Text = "Global";
            // 
            // m_ebGlobal
            // 
            this.m_ebGlobal.Location = new System.Drawing.Point(88, 24);
            this.m_ebGlobal.Name = "m_ebGlobal";
            this.m_ebGlobal.Size = new System.Drawing.Size(120, 20);
            this.m_ebGlobal.TabIndex = 3;
            this.m_ebGlobal.Text = "";
            // 
            // m_label2
            // 
            this.m_label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_label2.Location = new System.Drawing.Point(24, 48);
            this.m_label2.Name = "m_label2";
            this.m_label2.Size = new System.Drawing.Size(48, 23);
            this.m_label2.TabIndex = 4;
            this.m_label2.Text = "Local";
            // 
            // m_label3
            // 
            this.m_label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_label3.Location = new System.Drawing.Point(24, 72);
            this.m_label3.Name = "m_label3";
            this.m_label3.Size = new System.Drawing.Size(48, 23);
            this.m_label3.TabIndex = 5;
            this.m_label3.Text = "Display";
            // 
            // m_ebLocal
            // 
            this.m_ebLocal.Location = new System.Drawing.Point(88, 48);
            this.m_ebLocal.Name = "m_ebLocal";
            this.m_ebLocal.Size = new System.Drawing.Size(120, 20);
            this.m_ebLocal.TabIndex = 6;
            this.m_ebLocal.Text = "";
            // 
            // m_ebDisplay
            // 
            this.m_ebDisplay.Location = new System.Drawing.Point(88, 72);
            this.m_ebDisplay.Name = "m_ebDisplay";
            this.m_ebDisplay.Size = new System.Drawing.Size(120, 20);
            this.m_ebDisplay.TabIndex = 7;
            this.m_ebDisplay.Text = "";
            // 
            // m_cbEnabled
            // 
            this.m_cbEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbEnabled.Location = new System.Drawing.Point(240, 24);
            this.m_cbEnabled.Name = "m_cbEnabled";
            this.m_cbEnabled.Size = new System.Drawing.Size(72, 24);
            this.m_cbEnabled.TabIndex = 8;
            this.m_cbEnabled.Text = "Enabled";
            // 
            // m_cbVisible
            // 
            this.m_cbVisible.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbVisible.Location = new System.Drawing.Point(240, 48);
            this.m_cbVisible.Name = "m_cbVisible";
            this.m_cbVisible.Size = new System.Drawing.Size(64, 24);
            this.m_cbVisible.TabIndex = 9;
            this.m_cbVisible.Text = "Visible";
            // 
            // KeywordsForm
            // 
            this.AcceptButton = this.m_bnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_bnCancel;
            this.ClientSize = new System.Drawing.Size(328, 166);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.m_cbVisible,
                                                                          this.m_cbEnabled,
                                                                          this.m_ebDisplay,
                                                                          this.m_ebLocal,
                                                                          this.m_label3,
                                                                          this.m_label2,
                                                                          this.m_ebGlobal,
                                                                          this.m_label1,
                                                                          this.m_bnCancel,
                                                                          this.m_bnOK});
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KeywordsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Key Word Settings";
            this.ResumeLayout(false);

        }
		#endregion

        private void
        OnOk(object sender, System.EventArgs e)
        {
            m_kword.GlobalName  = m_ebGlobal.Text;
            m_kword.LocalName   = m_ebLocal.Text;
            m_kword.DisplayName = m_ebDisplay.Text;
            m_kword.Enabled     = m_cbEnabled.Checked;
            m_kword.Visible     = m_cbVisible.Checked;
        }

	}
}
