// $Header: //Desktop/Source/TestTools/MgdDbg/Prompts/PromptTestForm.cs#1 $
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
using System.Diagnostics;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace MgdDbg.Prompts
{
	/// <summary>
	/// Summary description for PromptTestForm.
	/// </summary>
	public class PromptTestForm : System.Windows.Forms.Form
	{
	        // Tab page for each type of prompt
        private System.Windows.Forms.TabControl     m_tcMain;
        private System.Windows.Forms.TabPage        m_tpInteger;
        private System.Windows.Forms.TabPage        m_tpDouble;
        private System.Windows.Forms.TabPage        m_tpDistance;
        private System.Windows.Forms.TabPage        m_tpAngle;
        private System.Windows.Forms.TabPage        m_tpCorner;
        private System.Windows.Forms.TabPage        m_tpPoint;
        private System.Windows.Forms.TabPage        m_tpString;
        private System.Windows.Forms.TabPage        m_tpKeyword;
        private System.Windows.Forms.TabPage        m_tpEntity;
        private System.Windows.Forms.TabPage        m_tpNested;
        
            // controls for each page
        private System.Windows.Forms.GroupBox       m_gbOptionsInt;
        private System.Windows.Forms.Label          m_txtMessage;
        private System.Windows.Forms.CheckBox       m_cbIntAllowArbitraryInput;
        private System.Windows.Forms.CheckBox       m_cbIntAllowNone;
        private System.Windows.Forms.CheckBox       m_cbIntAllowZero;
        private System.Windows.Forms.CheckBox       m_cbIntAllowNegative;
        private System.Windows.Forms.TextBox        m_ebIntMessage;
        private System.Windows.Forms.CheckBox       m_cbIntUseDefault;
        private System.Windows.Forms.TextBox        m_ebIntDefault;
        private System.Windows.Forms.Label          m_txtIntDefault;
        private System.Windows.Forms.Button         m_bnRunTest;
        private System.Windows.Forms.Button         m_bnCancel;
        private System.Windows.Forms.GroupBox       m_gbKeywords;
        private System.Windows.Forms.ListView       m_lvIntKeywords;
        private System.Windows.Forms.ColumnHeader   m_lvColKwordGlobal;
        private System.Windows.Forms.ColumnHeader   m_lvColKwordLocal;
        private System.Windows.Forms.ColumnHeader   m_lvColKwordDisplay;
        private System.Windows.Forms.ColumnHeader   m_lvColKwordEnabled;
        private System.Windows.Forms.ColumnHeader   m_lvColKwordVisible;
        private System.Windows.Forms.Button         m_bnIntKwordHardwire;
        private System.Windows.Forms.Button         m_bnIntKwordAdd;
        private System.Windows.Forms.Button         m_bnIntKwordEdit;
        private System.Windows.Forms.Button         m_bnIntKwordClear;
        private System.Windows.Forms.Label          m_txtIntKwordDef;
        private System.Windows.Forms.TextBox        m_ebIntKwordDef;
		
        private System.Windows.Forms.GroupBox       m_gbDblOpts;
        private System.Windows.Forms.TextBox        m_ebDblDefault;
        private System.Windows.Forms.Label          m_txtDblDefault;
        private System.Windows.Forms.CheckBox       m_cbDblUseDefault;
        private System.Windows.Forms.CheckBox       m_cbDblAllowNegative;
        private System.Windows.Forms.CheckBox       m_cbDblAllowZero;
        private System.Windows.Forms.CheckBox       m_cbDblAllowNone;
        private System.Windows.Forms.CheckBox       m_cbDblAllowArbitraryInput;
        private System.Windows.Forms.Label          m_txtDblMessage;
        private System.Windows.Forms.TextBox        m_ebDblMessage;
        private System.Windows.Forms.GroupBox       m_gbDblKwords;
        private System.Windows.Forms.Button         m_bnDblKwordClear;
        private System.Windows.Forms.Button         m_bnDblKwordEdit;
        private System.Windows.Forms.Button         m_bnDblKwordAdd;
        private System.Windows.Forms.ListView       m_lvDblKwords;
        private System.Windows.Forms.ColumnHeader   m_lvColDblGlobal;
        private System.Windows.Forms.ColumnHeader   m_lvColDblLocal;
        private System.Windows.Forms.ColumnHeader   m_lvColDblDisplay;
        private System.Windows.Forms.ColumnHeader   m_lvColDblEnabled;
        private System.Windows.Forms.ColumnHeader   m_lvColDblVisible;
        private System.Windows.Forms.TextBox        m_ebDblKwordDef;
        private System.Windows.Forms.Label          m_txtDblKwordDef;
        private System.Windows.Forms.Button         m_bnDblKwordHardwire;
        
        private System.Windows.Forms.GroupBox       m_gbDistKwords;
        private System.Windows.Forms.Button         m_bnDistKwordClear;
        private System.Windows.Forms.Button         m_bnDistKwordEdit;
        private System.Windows.Forms.Button         m_bnDistKwordAdd;
        private System.Windows.Forms.ListView       m_lvDistKwords;
        private System.Windows.Forms.ColumnHeader   m_lvColDistGlobal;
        private System.Windows.Forms.ColumnHeader   m_lvColDistLocal;
        private System.Windows.Forms.ColumnHeader   m_lvColDistDisplay;
        private System.Windows.Forms.ColumnHeader   m_lvColDistEnabled;
        private System.Windows.Forms.ColumnHeader   m_lvColDistVisible;
        private System.Windows.Forms.GroupBox       m_gbDistOptions;
        private System.Windows.Forms.TextBox        m_ebDistDefault;
        private System.Windows.Forms.Label          m_txtDistDefault;
        private System.Windows.Forms.CheckBox       m_cbDistUseDefault;
        private System.Windows.Forms.CheckBox       m_cbDistAllowNegative;
        private System.Windows.Forms.CheckBox       m_cbDistAllowZero;
        private System.Windows.Forms.CheckBox       m_cbDistAllowNone;
        private System.Windows.Forms.CheckBox       m_cbDistAllowArbitraryInput;
        private System.Windows.Forms.CheckBox       m_cbDistUseDashedLine;
        private System.Windows.Forms.CheckBox       m_cbDistUseBasePt;
        private System.Windows.Forms.CheckBox       m_cbDistOnly2d;
        private System.Windows.Forms.Label          m_txtDistMessage;
        private System.Windows.Forms.TextBox        m_ebDistMessage;
        private System.Windows.Forms.Button         m_bnDistKwordHardwire;
        private System.Windows.Forms.TextBox        m_ebDistKwordDef;
        private System.Windows.Forms.Label          m_txtDistKwordDef;

		
        private System.Windows.Forms.Label          m_txtAngMessage;
        private System.Windows.Forms.TextBox        m_ebAngMessage;
        private System.Windows.Forms.GroupBox       m_gbAngOptions;
        private System.Windows.Forms.ColumnHeader   m_lvColAngGlobal;
        private System.Windows.Forms.ColumnHeader   m_lvColAngLocal;
        private System.Windows.Forms.ColumnHeader   m_lvColAngDisplay;
        private System.Windows.Forms.ColumnHeader   m_lvColAngEnabled;
        private System.Windows.Forms.ColumnHeader   m_lvColAngVisible;
        private System.Windows.Forms.GroupBox       m_gbAngKwords;
        private System.Windows.Forms.Button         m_bnAngKwordClear;
        private System.Windows.Forms.Button         m_bnAngKwordEdit;
        private System.Windows.Forms.Button         m_bnAngKwordAdd;
        private System.Windows.Forms.ListView       m_lvAngKwords;
        private System.Windows.Forms.CheckBox       m_cbAngUseBasePt;
        private System.Windows.Forms.CheckBox       m_cbAngAllowZero;
        private System.Windows.Forms.CheckBox       m_cbAngAllowNone;
        private System.Windows.Forms.CheckBox       m_cbAngAllowArbitraryInput;
        private System.Windows.Forms.TextBox        m_ebAngDefault;
        private System.Windows.Forms.Label          m_txtAngDefault;
        private System.Windows.Forms.CheckBox       m_cbAngUseDashedLine;
        private System.Windows.Forms.CheckBox       m_cbAngUseDefault;
        private System.Windows.Forms.Button         m_bnAngKwordHardwire;
        private System.Windows.Forms.TextBox        m_ebAngKwordDef;
        private System.Windows.Forms.Label          m_txtAngKwordDef;
        
        private System.Windows.Forms.GroupBox       m_gbCrnOptions;
        private System.Windows.Forms.CheckBox       m_cbCrnUseDashedLine;
        private System.Windows.Forms.CheckBox       m_cbCrnAllowNone;
        private System.Windows.Forms.CheckBox       m_cbCrnAllowArbitraryInput;
        private System.Windows.Forms.ColumnHeader   m_lvColCrnGlobal;
        private System.Windows.Forms.ColumnHeader   m_lvColCrnLocal;
        private System.Windows.Forms.ColumnHeader   m_lvColCrnDisplay;
        private System.Windows.Forms.ColumnHeader   m_lvColCrnEnabled;
        private System.Windows.Forms.ColumnHeader   m_lvColCrnVisible;
        private System.Windows.Forms.GroupBox       m_gbCrnKwords;
        private System.Windows.Forms.Button         m_bnCrnKwordClear;
        private System.Windows.Forms.Button         m_bnCrnKwordEdit;
        private System.Windows.Forms.Button         m_bnCrnKwordAdd;
        private System.Windows.Forms.ListView       m_lvCrnKwords;
        private System.Windows.Forms.TextBox        m_ebCrnMessage;
        private System.Windows.Forms.Label          m_txtCrnMessage;
        private System.Windows.Forms.CheckBox       m_cbCrnLimitsChecked;
        private System.Windows.Forms.Button         m_bnCrnKwordHardwire;
        private System.Windows.Forms.TextBox        m_ebCrnKwordDef;
        private System.Windows.Forms.Label          m_txtCrnKwordDef;
        
        private System.Windows.Forms.GroupBox       m_gbPtOptions;
        private System.Windows.Forms.CheckBox       m_cbPtUseDashedLine;
        private System.Windows.Forms.CheckBox       m_cbPtLimitsChecked;
        private System.Windows.Forms.CheckBox       m_cbPtAllowNone;
        private System.Windows.Forms.CheckBox       m_cbPtAllowArbitraryInput;
        private System.Windows.Forms.GroupBox       m_gbPtKwords;
        private System.Windows.Forms.Button         m_bnPtKwordClear;
        private System.Windows.Forms.Button         m_bnPtKwordEdit;
        private System.Windows.Forms.Button         m_bnPtKwordAdd;
        private System.Windows.Forms.ListView       m_lvPtKwords;
        private System.Windows.Forms.ColumnHeader   m_lvColPtGlobal;
        private System.Windows.Forms.ColumnHeader   m_lvColPtLocal;
        private System.Windows.Forms.ColumnHeader   m_lvColPtDisplay;
        private System.Windows.Forms.ColumnHeader   m_lvColPtEnabled;
        private System.Windows.Forms.ColumnHeader   m_lvColPtVisible;
        private System.Windows.Forms.TextBox        m_ebPtMessage;
        private System.Windows.Forms.Label          m_txtPtMessage;
        private System.Windows.Forms.CheckBox       m_cbPtUseBasePt;
        private System.Windows.Forms.Button         m_bnPtKwordHardwire;
        private System.Windows.Forms.TextBox        m_ebPtKwordDef;
        private System.Windows.Forms.Label          m_txtPtKwordDef;
        
        private System.Windows.Forms.GroupBox       m_gbStrOptions;
        private System.Windows.Forms.CheckBox       m_cbStrAllowSpaces;
        private System.Windows.Forms.TextBox        m_ebStrMessage;
        private System.Windows.Forms.Label          m_txtStrMessage;
        
        private System.Windows.Forms.GroupBox       m_gbKwdOptions;
        private System.Windows.Forms.CheckBox       m_cbKwdAllowArbitraryInput;
        private System.Windows.Forms.CheckBox       m_cbKwdAllowNone;
        private System.Windows.Forms.GroupBox       m_gbKwdKwords;
        private System.Windows.Forms.Button         m_bnKwdKwordClear;
        private System.Windows.Forms.Button         m_bnKwdKwordEdit;
        private System.Windows.Forms.Button         m_bnKwdKwordAdd;
        private System.Windows.Forms.ListView       m_lvKwdKwords;
        private System.Windows.Forms.ColumnHeader   m_lvColKwdGlobal;
        private System.Windows.Forms.ColumnHeader   m_lvColKwdLocal;
        private System.Windows.Forms.ColumnHeader   m_lvColKwdDisplay;
        private System.Windows.Forms.ColumnHeader   m_lvColKwdEnabled;
        private System.Windows.Forms.ColumnHeader   m_lvColKwdVisible;
        private System.Windows.Forms.TextBox        m_ebKwdMessage;
        private System.Windows.Forms.Label          m_txtKwdMessage;
        private System.Windows.Forms.Button         m_bnKwdKwordHardwire;
        private System.Windows.Forms.TextBox        m_ebKwdKwordDef;
        private System.Windows.Forms.Label          m_txtKwdKwordDef;
        
        private System.Windows.Forms.GroupBox       m_gbEntOptions;
        private System.Windows.Forms.CheckBox       m_cbEntAllowNone;
        private System.Windows.Forms.TextBox        m_ebEntMessage;
        private System.Windows.Forms.Label          m_txtEntMessage;
        private System.Windows.Forms.GroupBox       m_gbEntKwords;
        private System.Windows.Forms.Button         m_bnEntKwordClear;
        private System.Windows.Forms.Button         m_bnEntKwordEdit;
        private System.Windows.Forms.Button         m_bnEntKwordAdd;
        private System.Windows.Forms.ListView       m_lvEntKwords;
        private System.Windows.Forms.ColumnHeader   m_lvColEntKwordGlobal;
        private System.Windows.Forms.ColumnHeader   m_lvColEntKwordLocal;
        private System.Windows.Forms.ColumnHeader   m_lvColEntKwordDisplay;
        private System.Windows.Forms.ColumnHeader   m_lvColEntKwordEnabled;
        private System.Windows.Forms.ColumnHeader   m_lvColEntKwordVisible;
        private System.Windows.Forms.Button         m_bnEntKwordHardwire;
        private System.Windows.Forms.TextBox        m_ebEntKwordDef;
        private System.Windows.Forms.Label          m_txtEntKwordDef;
        private System.Windows.Forms.GroupBox       m_gbEntFilterClasses;
        private System.Windows.Forms.CheckBox       m_ebEntDoIsATest;
        private System.Windows.Forms.CheckBox       m_cbEntAllowCircles;
        private System.Windows.Forms.CheckBox       m_cbEntAllowLines;
        private System.Windows.Forms.TextBox        m_ebEntRejectMessage;
        private System.Windows.Forms.Label          m_txtEntRejectMessage;
        private System.Windows.Forms.Label          m_txtEntSampleFilter;
        private System.Windows.Forms.CheckBox       m_cbEntAllowCurves;
        private System.Windows.Forms.CheckBox       m_cbEntAllowLockedLayer;
        
        private System.Windows.Forms.GroupBox       m_gbNentKwords;
        private System.Windows.Forms.Button         m_bnNentKwordHardwire;
        private System.Windows.Forms.TextBox        m_ebNentKwordDef;
        private System.Windows.Forms.Label          m_txtNentKwordDef;
        private System.Windows.Forms.Button         m_bnNentKwordClear;
        private System.Windows.Forms.Button         m_bnNentKwordEdit;
        private System.Windows.Forms.Button         m_bnNentKwordAdd;
        private System.Windows.Forms.ListView       m_lvNentKwords;
        private System.Windows.Forms.ColumnHeader   m_lvColNentKwordsGlobal;
        private System.Windows.Forms.ColumnHeader   m_lvColNentKwordsLocal;
        private System.Windows.Forms.ColumnHeader   m_lvColNentKwordsDisplay;
        private System.Windows.Forms.ColumnHeader   m_lvColNentKwordsEnabled;
        private System.Windows.Forms.ColumnHeader   m_lvColNentKwordsVisible;
        private System.Windows.Forms.GroupBox       m_gbNentOptions;
        private System.Windows.Forms.CheckBox       m_cbNentAllowNone;
        private System.Windows.Forms.TextBox        m_ebNentMessage;
        private System.Windows.Forms.Label          m_txtNentMessage;
        private System.Windows.Forms.CheckBox       m_cbNentNonInterPickPt;
        
        private System.ComponentModel.Container components = null;
        
            // we're keeping a separate keyword collection for each page.
            // Not sure I had to, but I wanted each prompt to be easy and
            // quick to test.
		private KeywordCollection   m_kwordsInt     = new KeywordCollection();
		private KeywordCollection   m_kwordsDbl     = new KeywordCollection();
		private KeywordCollection   m_kwordsDist    = new KeywordCollection();
		private KeywordCollection   m_kwordsAng     = new KeywordCollection();
		private KeywordCollection   m_kwordsCrn     = new KeywordCollection();
		private KeywordCollection   m_kwordsPt      = new KeywordCollection();
		private KeywordCollection   m_kwordsKwd     = new KeywordCollection();
		private KeywordCollection   m_kwordsEnt     = new KeywordCollection();
		private KeywordCollection   m_kwordsNent    = new KeywordCollection();

		public
		PromptTestForm()
		{
			InitializeComponent();
			
			    // hook up the backing data with controls so that we
			    // can deal with them generically later when we get
			    // event notification for the keywords on each page.
			m_lvIntKeywords.Tag = m_kwordsInt;
			m_bnIntKwordAdd.Tag = m_lvIntKeywords;
			m_bnIntKwordEdit.Tag = m_lvIntKeywords;
			m_bnIntKwordClear.Tag = m_lvIntKeywords;
			m_bnIntKwordHardwire.Tag = m_lvIntKeywords;
			
		    m_lvDblKwords.Tag = m_kwordsDbl;
			m_bnDblKwordAdd.Tag = m_lvDblKwords;
			m_bnDblKwordEdit.Tag = m_lvDblKwords;
			m_bnDblKwordClear.Tag = m_lvDblKwords;
			m_bnDblKwordHardwire.Tag = m_lvDblKwords;
			
		    m_lvDistKwords.Tag = m_kwordsDist;
			m_bnDistKwordAdd.Tag = m_lvDistKwords;
			m_bnDistKwordEdit.Tag = m_lvDistKwords;
			m_bnDistKwordClear.Tag = m_lvDistKwords;
			m_bnDistKwordHardwire.Tag = m_lvDistKwords;
			
		    m_lvAngKwords.Tag = m_kwordsAng;
			m_bnAngKwordAdd.Tag = m_lvAngKwords;
			m_bnAngKwordEdit.Tag = m_lvAngKwords;
			m_bnAngKwordClear.Tag = m_lvAngKwords;
		    m_bnAngKwordHardwire.Tag = m_lvAngKwords;
			
		    m_lvCrnKwords.Tag = m_kwordsCrn;
			m_bnCrnKwordAdd.Tag = m_lvCrnKwords;
			m_bnCrnKwordEdit.Tag = m_lvCrnKwords;
			m_bnCrnKwordClear.Tag = m_lvCrnKwords;
			m_bnCrnKwordHardwire.Tag = m_lvCrnKwords;
			
		    m_lvPtKwords.Tag = m_kwordsPt;
			m_bnPtKwordAdd.Tag = m_lvPtKwords;
			m_bnPtKwordEdit.Tag = m_lvPtKwords;
			m_bnPtKwordClear.Tag = m_lvPtKwords;
		    m_bnPtKwordHardwire.Tag = m_lvPtKwords;
			
		    m_lvKwdKwords.Tag = m_kwordsKwd;
			m_bnKwdKwordAdd.Tag = m_lvKwdKwords;
			m_bnKwdKwordEdit.Tag = m_lvKwdKwords;
			m_bnKwdKwordClear.Tag = m_lvKwdKwords;
			m_bnKwdKwordHardwire.Tag = m_lvKwdKwords;
			
		    m_lvEntKwords.Tag = m_kwordsEnt;
			m_bnEntKwordAdd.Tag = m_lvEntKwords;
			m_bnEntKwordEdit.Tag = m_lvEntKwords;
			m_bnEntKwordClear.Tag = m_lvEntKwords;
			m_bnEntKwordHardwire.Tag = m_lvEntKwords;
			
            m_lvNentKwords.Tag = m_kwordsNent;
			m_bnNentKwordAdd.Tag = m_lvNentKwords;
			m_bnNentKwordEdit.Tag = m_lvNentKwords;
			m_bnNentKwordClear.Tag = m_lvNentKwords;
			m_bnNentKwordHardwire.Tag = m_lvNentKwords;

                // allocate a new PromptOptions and set the initial dialog
                // state to these values.  This way, we can see what the defaults
                // are if you don't change any of the standard settings.
                
                // PromptInteger
            PromptIntegerOptions prOptInt = new PromptIntegerOptions("");
            m_cbIntAllowArbitraryInput.Checked = prOptInt.AllowArbitraryInput;
            m_cbIntAllowNone.Checked = prOptInt.AllowNone;
            m_cbIntAllowZero.Checked = prOptInt.AllowZero;
            m_cbIntAllowNegative.Checked = prOptInt.AllowNegative;
            m_cbIntUseDefault.Checked = prOptInt.UseDefaultValue;
            m_ebIntDefault.Enabled = prOptInt.UseDefaultValue;
            m_ebIntDefault.Text = 0.ToString();
            
                // PromptDouble
            PromptDoubleOptions prOptDbl = new PromptDoubleOptions("");
            m_cbDblAllowArbitraryInput.Checked = prOptDbl.AllowArbitraryInput;
            m_cbDblAllowNone.Checked = prOptDbl.AllowNone;
            m_cbDblAllowZero.Checked = prOptDbl.AllowZero;
            m_cbDblAllowNegative.Checked = prOptDbl.AllowNegative;
            m_cbDblUseDefault.Checked = prOptDbl.UseDefaultValue;
            m_ebDblDefault.Enabled = prOptDbl.UseDefaultValue;
            m_ebDblDefault.Text = 0.0.ToString();
            
                // PromptDistance
            PromptDistanceOptions prOptDist = new PromptDistanceOptions("");
            m_cbDistAllowArbitraryInput.Checked = prOptDist.AllowArbitraryInput;
            m_cbDistAllowNone.Checked = prOptDist.AllowNone;
            m_cbDistAllowZero.Checked = prOptDist.AllowZero;
            m_cbDistAllowNegative.Checked = prOptDist.AllowNegative;
            m_cbDistUseBasePt.Checked = prOptDist.UseBasePoint;
            m_cbDistUseDashedLine.Checked = prOptDist.UseDashedLine;
            m_cbDistOnly2d.Checked = prOptDist.Only2d;
            m_cbDistUseDefault.Checked = prOptDist.UseDefaultValue;
            m_ebDistDefault.Enabled = prOptDist.UseDefaultValue;
            m_ebDistDefault.Text = 0.0.ToString();

                // PromptAngle
            PromptAngleOptions prOptAng = new PromptAngleOptions("");
            m_cbAngAllowArbitraryInput.Checked = prOptAng.AllowArbitraryInput;
            m_cbAngAllowNone.Checked = prOptAng.AllowNone;
            m_cbAngAllowZero.Checked = prOptAng.AllowZero;
            m_cbAngUseBasePt.Checked = prOptAng.UseBasePoint;
            m_cbAngUseDashedLine.Checked = prOptAng.UseDashedLine;
            m_cbAngUseDefault.Checked = prOptAng.UseDefaultValue;
            m_ebAngDefault.Enabled = prOptAng.UseDefaultValue;
            m_ebAngDefault.Text = 0.0.ToString();
            
                // PromptCorner
            PromptCornerOptions prOptCrn = new PromptCornerOptions("", new Point3d(0.0, 0.0, 0.0));
            m_cbCrnAllowArbitraryInput.Checked = prOptCrn.AllowArbitraryInput;
            m_cbCrnAllowNone.Checked = prOptCrn.AllowNone;
            m_cbCrnLimitsChecked.Checked = prOptCrn.LimitsChecked;
            m_cbCrnUseDashedLine.Checked = prOptCrn.UseDashedLine;
            
                // PromptPoint
            PromptPointOptions prOptPt = new PromptPointOptions("");
            m_cbPtAllowArbitraryInput.Checked = prOptPt.AllowArbitraryInput;
            m_cbPtAllowNone.Checked = prOptPt.AllowNone;
            m_cbPtLimitsChecked.Checked = prOptPt.LimitsChecked;
            m_cbPtUseDashedLine.Checked = prOptPt.UseDashedLine;
            m_cbPtUseBasePt.Checked = prOptPt.UseBasePoint;
            
                // PromptString
            PromptStringOptions prOptStr = new PromptStringOptions("");
            m_cbStrAllowSpaces.Checked = prOptStr.AllowSpaces;
            
                // PromptKeyword
            PromptKeywordOptions prOptKwd = new PromptKeywordOptions("");
            m_cbKwdAllowArbitraryInput.Checked = prOptKwd.AllowArbitraryInput;
            m_cbKwdAllowNone.Checked = prOptKwd.AllowNone;
            
                // PromptEntity
            PromptEntityOptions prOptEnt = new PromptEntityOptions("");
            m_cbEntAllowNone.Checked = prOptEnt.AllowNone;
            
                // PromptNestedEntity
            PromptNestedEntityOptions prOptNent = new PromptNestedEntityOptions("");
            m_cbNentAllowNone.Checked = prOptNent.AllowNone;
            m_cbNentNonInterPickPt.Checked = prOptNent.UseNonInteractivePickPoint;
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
            this.m_tcMain = new System.Windows.Forms.TabControl();
            this.m_tpInteger = new System.Windows.Forms.TabPage();
            this.m_gbKeywords = new System.Windows.Forms.GroupBox();
            this.m_ebIntKwordDef = new System.Windows.Forms.TextBox();
            this.m_txtIntKwordDef = new System.Windows.Forms.Label();
            this.m_bnIntKwordHardwire = new System.Windows.Forms.Button();
            this.m_bnIntKwordClear = new System.Windows.Forms.Button();
            this.m_bnIntKwordEdit = new System.Windows.Forms.Button();
            this.m_bnIntKwordAdd = new System.Windows.Forms.Button();
            this.m_lvIntKeywords = new System.Windows.Forms.ListView();
            this.m_lvColKwordGlobal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColKwordLocal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColKwordDisplay = new System.Windows.Forms.ColumnHeader();
            this.m_lvColKwordEnabled = new System.Windows.Forms.ColumnHeader();
            this.m_lvColKwordVisible = new System.Windows.Forms.ColumnHeader();
            this.m_gbOptionsInt = new System.Windows.Forms.GroupBox();
            this.m_ebIntDefault = new System.Windows.Forms.TextBox();
            this.m_txtIntDefault = new System.Windows.Forms.Label();
            this.m_cbIntUseDefault = new System.Windows.Forms.CheckBox();
            this.m_cbIntAllowNegative = new System.Windows.Forms.CheckBox();
            this.m_cbIntAllowZero = new System.Windows.Forms.CheckBox();
            this.m_cbIntAllowNone = new System.Windows.Forms.CheckBox();
            this.m_cbIntAllowArbitraryInput = new System.Windows.Forms.CheckBox();
            this.m_txtMessage = new System.Windows.Forms.Label();
            this.m_ebIntMessage = new System.Windows.Forms.TextBox();
            this.m_tpDouble = new System.Windows.Forms.TabPage();
            this.m_gbDblKwords = new System.Windows.Forms.GroupBox();
            this.m_bnDblKwordHardwire = new System.Windows.Forms.Button();
            this.m_ebDblKwordDef = new System.Windows.Forms.TextBox();
            this.m_txtDblKwordDef = new System.Windows.Forms.Label();
            this.m_bnDblKwordClear = new System.Windows.Forms.Button();
            this.m_bnDblKwordEdit = new System.Windows.Forms.Button();
            this.m_bnDblKwordAdd = new System.Windows.Forms.Button();
            this.m_lvDblKwords = new System.Windows.Forms.ListView();
            this.m_lvColDblGlobal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColDblLocal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColDblDisplay = new System.Windows.Forms.ColumnHeader();
            this.m_lvColDblEnabled = new System.Windows.Forms.ColumnHeader();
            this.m_lvColDblVisible = new System.Windows.Forms.ColumnHeader();
            this.m_gbDblOpts = new System.Windows.Forms.GroupBox();
            this.m_ebDblDefault = new System.Windows.Forms.TextBox();
            this.m_txtDblDefault = new System.Windows.Forms.Label();
            this.m_cbDblUseDefault = new System.Windows.Forms.CheckBox();
            this.m_cbDblAllowNegative = new System.Windows.Forms.CheckBox();
            this.m_cbDblAllowZero = new System.Windows.Forms.CheckBox();
            this.m_cbDblAllowNone = new System.Windows.Forms.CheckBox();
            this.m_cbDblAllowArbitraryInput = new System.Windows.Forms.CheckBox();
            this.m_txtDblMessage = new System.Windows.Forms.Label();
            this.m_ebDblMessage = new System.Windows.Forms.TextBox();
            this.m_tpDistance = new System.Windows.Forms.TabPage();
            this.m_gbDistKwords = new System.Windows.Forms.GroupBox();
            this.m_bnDistKwordHardwire = new System.Windows.Forms.Button();
            this.m_ebDistKwordDef = new System.Windows.Forms.TextBox();
            this.m_txtDistKwordDef = new System.Windows.Forms.Label();
            this.m_bnDistKwordClear = new System.Windows.Forms.Button();
            this.m_bnDistKwordEdit = new System.Windows.Forms.Button();
            this.m_bnDistKwordAdd = new System.Windows.Forms.Button();
            this.m_lvDistKwords = new System.Windows.Forms.ListView();
            this.m_lvColDistGlobal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColDistLocal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColDistDisplay = new System.Windows.Forms.ColumnHeader();
            this.m_lvColDistEnabled = new System.Windows.Forms.ColumnHeader();
            this.m_lvColDistVisible = new System.Windows.Forms.ColumnHeader();
            this.m_gbDistOptions = new System.Windows.Forms.GroupBox();
            this.m_cbDistOnly2d = new System.Windows.Forms.CheckBox();
            this.m_cbDistUseDashedLine = new System.Windows.Forms.CheckBox();
            this.m_cbDistUseBasePt = new System.Windows.Forms.CheckBox();
            this.m_ebDistDefault = new System.Windows.Forms.TextBox();
            this.m_txtDistDefault = new System.Windows.Forms.Label();
            this.m_cbDistUseDefault = new System.Windows.Forms.CheckBox();
            this.m_cbDistAllowNegative = new System.Windows.Forms.CheckBox();
            this.m_cbDistAllowZero = new System.Windows.Forms.CheckBox();
            this.m_cbDistAllowNone = new System.Windows.Forms.CheckBox();
            this.m_cbDistAllowArbitraryInput = new System.Windows.Forms.CheckBox();
            this.m_txtDistMessage = new System.Windows.Forms.Label();
            this.m_ebDistMessage = new System.Windows.Forms.TextBox();
            this.m_tpAngle = new System.Windows.Forms.TabPage();
            this.m_txtAngMessage = new System.Windows.Forms.Label();
            this.m_ebAngMessage = new System.Windows.Forms.TextBox();
            this.m_gbAngOptions = new System.Windows.Forms.GroupBox();
            this.m_cbAngUseDefault = new System.Windows.Forms.CheckBox();
            this.m_cbAngUseDashedLine = new System.Windows.Forms.CheckBox();
            this.m_ebAngDefault = new System.Windows.Forms.TextBox();
            this.m_txtAngDefault = new System.Windows.Forms.Label();
            this.m_cbAngUseBasePt = new System.Windows.Forms.CheckBox();
            this.m_cbAngAllowZero = new System.Windows.Forms.CheckBox();
            this.m_cbAngAllowNone = new System.Windows.Forms.CheckBox();
            this.m_cbAngAllowArbitraryInput = new System.Windows.Forms.CheckBox();
            this.m_gbAngKwords = new System.Windows.Forms.GroupBox();
            this.m_bnAngKwordHardwire = new System.Windows.Forms.Button();
            this.m_ebAngKwordDef = new System.Windows.Forms.TextBox();
            this.m_txtAngKwordDef = new System.Windows.Forms.Label();
            this.m_bnAngKwordClear = new System.Windows.Forms.Button();
            this.m_bnAngKwordEdit = new System.Windows.Forms.Button();
            this.m_bnAngKwordAdd = new System.Windows.Forms.Button();
            this.m_lvAngKwords = new System.Windows.Forms.ListView();
            this.m_lvColAngGlobal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColAngLocal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColAngDisplay = new System.Windows.Forms.ColumnHeader();
            this.m_lvColAngEnabled = new System.Windows.Forms.ColumnHeader();
            this.m_lvColAngVisible = new System.Windows.Forms.ColumnHeader();
            this.m_tpCorner = new System.Windows.Forms.TabPage();
            this.m_gbCrnOptions = new System.Windows.Forms.GroupBox();
            this.m_cbCrnUseDashedLine = new System.Windows.Forms.CheckBox();
            this.m_cbCrnLimitsChecked = new System.Windows.Forms.CheckBox();
            this.m_cbCrnAllowNone = new System.Windows.Forms.CheckBox();
            this.m_cbCrnAllowArbitraryInput = new System.Windows.Forms.CheckBox();
            this.m_gbCrnKwords = new System.Windows.Forms.GroupBox();
            this.m_bnCrnKwordHardwire = new System.Windows.Forms.Button();
            this.m_ebCrnKwordDef = new System.Windows.Forms.TextBox();
            this.m_txtCrnKwordDef = new System.Windows.Forms.Label();
            this.m_bnCrnKwordClear = new System.Windows.Forms.Button();
            this.m_bnCrnKwordEdit = new System.Windows.Forms.Button();
            this.m_bnCrnKwordAdd = new System.Windows.Forms.Button();
            this.m_lvCrnKwords = new System.Windows.Forms.ListView();
            this.m_lvColCrnGlobal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColCrnLocal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColCrnDisplay = new System.Windows.Forms.ColumnHeader();
            this.m_lvColCrnEnabled = new System.Windows.Forms.ColumnHeader();
            this.m_lvColCrnVisible = new System.Windows.Forms.ColumnHeader();
            this.m_ebCrnMessage = new System.Windows.Forms.TextBox();
            this.m_txtCrnMessage = new System.Windows.Forms.Label();
            this.m_tpPoint = new System.Windows.Forms.TabPage();
            this.m_gbPtOptions = new System.Windows.Forms.GroupBox();
            this.m_cbPtUseBasePt = new System.Windows.Forms.CheckBox();
            this.m_cbPtUseDashedLine = new System.Windows.Forms.CheckBox();
            this.m_cbPtLimitsChecked = new System.Windows.Forms.CheckBox();
            this.m_cbPtAllowNone = new System.Windows.Forms.CheckBox();
            this.m_cbPtAllowArbitraryInput = new System.Windows.Forms.CheckBox();
            this.m_gbPtKwords = new System.Windows.Forms.GroupBox();
            this.m_bnPtKwordHardwire = new System.Windows.Forms.Button();
            this.m_ebPtKwordDef = new System.Windows.Forms.TextBox();
            this.m_txtPtKwordDef = new System.Windows.Forms.Label();
            this.m_bnPtKwordClear = new System.Windows.Forms.Button();
            this.m_bnPtKwordEdit = new System.Windows.Forms.Button();
            this.m_bnPtKwordAdd = new System.Windows.Forms.Button();
            this.m_lvPtKwords = new System.Windows.Forms.ListView();
            this.m_lvColPtGlobal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColPtLocal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColPtDisplay = new System.Windows.Forms.ColumnHeader();
            this.m_lvColPtEnabled = new System.Windows.Forms.ColumnHeader();
            this.m_lvColPtVisible = new System.Windows.Forms.ColumnHeader();
            this.m_ebPtMessage = new System.Windows.Forms.TextBox();
            this.m_txtPtMessage = new System.Windows.Forms.Label();
            this.m_tpString = new System.Windows.Forms.TabPage();
            this.m_gbStrOptions = new System.Windows.Forms.GroupBox();
            this.m_cbStrAllowSpaces = new System.Windows.Forms.CheckBox();
            this.m_ebStrMessage = new System.Windows.Forms.TextBox();
            this.m_txtStrMessage = new System.Windows.Forms.Label();
            this.m_tpKeyword = new System.Windows.Forms.TabPage();
            this.m_gbKwdOptions = new System.Windows.Forms.GroupBox();
            this.m_cbKwdAllowArbitraryInput = new System.Windows.Forms.CheckBox();
            this.m_cbKwdAllowNone = new System.Windows.Forms.CheckBox();
            this.m_gbKwdKwords = new System.Windows.Forms.GroupBox();
            this.m_bnKwdKwordHardwire = new System.Windows.Forms.Button();
            this.m_ebKwdKwordDef = new System.Windows.Forms.TextBox();
            this.m_txtKwdKwordDef = new System.Windows.Forms.Label();
            this.m_bnKwdKwordClear = new System.Windows.Forms.Button();
            this.m_bnKwdKwordEdit = new System.Windows.Forms.Button();
            this.m_bnKwdKwordAdd = new System.Windows.Forms.Button();
            this.m_lvKwdKwords = new System.Windows.Forms.ListView();
            this.m_lvColKwdGlobal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColKwdLocal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColKwdDisplay = new System.Windows.Forms.ColumnHeader();
            this.m_lvColKwdEnabled = new System.Windows.Forms.ColumnHeader();
            this.m_lvColKwdVisible = new System.Windows.Forms.ColumnHeader();
            this.m_ebKwdMessage = new System.Windows.Forms.TextBox();
            this.m_txtKwdMessage = new System.Windows.Forms.Label();
            this.m_tpEntity = new System.Windows.Forms.TabPage();
            this.m_gbEntFilterClasses = new System.Windows.Forms.GroupBox();
            this.m_cbEntAllowCurves = new System.Windows.Forms.CheckBox();
            this.m_txtEntSampleFilter = new System.Windows.Forms.Label();
            this.m_ebEntDoIsATest = new System.Windows.Forms.CheckBox();
            this.m_cbEntAllowCircles = new System.Windows.Forms.CheckBox();
            this.m_cbEntAllowLines = new System.Windows.Forms.CheckBox();
            this.m_ebEntRejectMessage = new System.Windows.Forms.TextBox();
            this.m_txtEntRejectMessage = new System.Windows.Forms.Label();
            this.m_gbEntKwords = new System.Windows.Forms.GroupBox();
            this.m_bnEntKwordHardwire = new System.Windows.Forms.Button();
            this.m_ebEntKwordDef = new System.Windows.Forms.TextBox();
            this.m_txtEntKwordDef = new System.Windows.Forms.Label();
            this.m_bnEntKwordClear = new System.Windows.Forms.Button();
            this.m_bnEntKwordEdit = new System.Windows.Forms.Button();
            this.m_bnEntKwordAdd = new System.Windows.Forms.Button();
            this.m_lvEntKwords = new System.Windows.Forms.ListView();
            this.m_lvColEntKwordGlobal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColEntKwordLocal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColEntKwordDisplay = new System.Windows.Forms.ColumnHeader();
            this.m_lvColEntKwordEnabled = new System.Windows.Forms.ColumnHeader();
            this.m_lvColEntKwordVisible = new System.Windows.Forms.ColumnHeader();
            this.m_gbEntOptions = new System.Windows.Forms.GroupBox();
            this.m_cbEntAllowLockedLayer = new System.Windows.Forms.CheckBox();
            this.m_cbEntAllowNone = new System.Windows.Forms.CheckBox();
            this.m_ebEntMessage = new System.Windows.Forms.TextBox();
            this.m_txtEntMessage = new System.Windows.Forms.Label();
            this.m_tpNested = new System.Windows.Forms.TabPage();
            this.m_gbNentKwords = new System.Windows.Forms.GroupBox();
            this.m_bnNentKwordHardwire = new System.Windows.Forms.Button();
            this.m_ebNentKwordDef = new System.Windows.Forms.TextBox();
            this.m_txtNentKwordDef = new System.Windows.Forms.Label();
            this.m_bnNentKwordClear = new System.Windows.Forms.Button();
            this.m_bnNentKwordEdit = new System.Windows.Forms.Button();
            this.m_bnNentKwordAdd = new System.Windows.Forms.Button();
            this.m_lvNentKwords = new System.Windows.Forms.ListView();
            this.m_lvColNentKwordsGlobal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColNentKwordsLocal = new System.Windows.Forms.ColumnHeader();
            this.m_lvColNentKwordsDisplay = new System.Windows.Forms.ColumnHeader();
            this.m_lvColNentKwordsEnabled = new System.Windows.Forms.ColumnHeader();
            this.m_lvColNentKwordsVisible = new System.Windows.Forms.ColumnHeader();
            this.m_gbNentOptions = new System.Windows.Forms.GroupBox();
            this.m_cbNentNonInterPickPt = new System.Windows.Forms.CheckBox();
            this.m_cbNentAllowNone = new System.Windows.Forms.CheckBox();
            this.m_ebNentMessage = new System.Windows.Forms.TextBox();
            this.m_txtNentMessage = new System.Windows.Forms.Label();
            this.m_bnRunTest = new System.Windows.Forms.Button();
            this.m_bnCancel = new System.Windows.Forms.Button();
            this.m_tcMain.SuspendLayout();
            this.m_tpInteger.SuspendLayout();
            this.m_gbKeywords.SuspendLayout();
            this.m_gbOptionsInt.SuspendLayout();
            this.m_tpDouble.SuspendLayout();
            this.m_gbDblKwords.SuspendLayout();
            this.m_gbDblOpts.SuspendLayout();
            this.m_tpDistance.SuspendLayout();
            this.m_gbDistKwords.SuspendLayout();
            this.m_gbDistOptions.SuspendLayout();
            this.m_tpAngle.SuspendLayout();
            this.m_gbAngOptions.SuspendLayout();
            this.m_gbAngKwords.SuspendLayout();
            this.m_tpCorner.SuspendLayout();
            this.m_gbCrnOptions.SuspendLayout();
            this.m_gbCrnKwords.SuspendLayout();
            this.m_tpPoint.SuspendLayout();
            this.m_gbPtOptions.SuspendLayout();
            this.m_gbPtKwords.SuspendLayout();
            this.m_tpString.SuspendLayout();
            this.m_gbStrOptions.SuspendLayout();
            this.m_tpKeyword.SuspendLayout();
            this.m_gbKwdOptions.SuspendLayout();
            this.m_gbKwdKwords.SuspendLayout();
            this.m_tpEntity.SuspendLayout();
            this.m_gbEntFilterClasses.SuspendLayout();
            this.m_gbEntKwords.SuspendLayout();
            this.m_gbEntOptions.SuspendLayout();
            this.m_tpNested.SuspendLayout();
            this.m_gbNentKwords.SuspendLayout();
            this.m_gbNentOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tcMain
            // 
            this.m_tcMain.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                   this.m_tpInteger,
                                                                                   this.m_tpDouble,
                                                                                   this.m_tpDistance,
                                                                                   this.m_tpAngle,
                                                                                   this.m_tpCorner,
                                                                                   this.m_tpPoint,
                                                                                   this.m_tpString,
                                                                                   this.m_tpKeyword,
                                                                                   this.m_tpEntity,
                                                                                   this.m_tpNested});
            this.m_tcMain.Location = new System.Drawing.Point(16, 16);
            this.m_tcMain.Name = "m_tcMain";
            this.m_tcMain.SelectedIndex = 0;
            this.m_tcMain.Size = new System.Drawing.Size(616, 344);
            this.m_tcMain.TabIndex = 0;
            this.m_tcMain.Click += new System.EventHandler(this.OnEntAllowLines);
            // 
            // m_tpInteger
            // 
            this.m_tpInteger.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.m_gbKeywords,
                                                                                      this.m_gbOptionsInt,
                                                                                      this.m_txtMessage,
                                                                                      this.m_ebIntMessage});
            this.m_tpInteger.Location = new System.Drawing.Point(4, 22);
            this.m_tpInteger.Name = "m_tpInteger";
            this.m_tpInteger.Size = new System.Drawing.Size(608, 318);
            this.m_tpInteger.TabIndex = 0;
            this.m_tpInteger.Text = "Integer";
            // 
            // m_gbKeywords
            // 
            this.m_gbKeywords.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.m_ebIntKwordDef,
                                                                                       this.m_txtIntKwordDef,
                                                                                       this.m_bnIntKwordHardwire,
                                                                                       this.m_bnIntKwordClear,
                                                                                       this.m_bnIntKwordEdit,
                                                                                       this.m_bnIntKwordAdd,
                                                                                       this.m_lvIntKeywords});
            this.m_gbKeywords.Location = new System.Drawing.Point(200, 48);
            this.m_gbKeywords.Name = "m_gbKeywords";
            this.m_gbKeywords.Size = new System.Drawing.Size(392, 264);
            this.m_gbKeywords.TabIndex = 2;
            this.m_gbKeywords.TabStop = false;
            this.m_gbKeywords.Text = "Key Words";
            // 
            // m_ebIntKwordDef
            // 
            this.m_ebIntKwordDef.Location = new System.Drawing.Point(272, 232);
            this.m_ebIntKwordDef.Name = "m_ebIntKwordDef";
            this.m_ebIntKwordDef.Size = new System.Drawing.Size(96, 20);
            this.m_ebIntKwordDef.TabIndex = 6;
            this.m_ebIntKwordDef.Text = "";
            // 
            // m_txtIntKwordDef
            // 
            this.m_txtIntKwordDef.Location = new System.Drawing.Point(216, 232);
            this.m_txtIntKwordDef.Name = "m_txtIntKwordDef";
            this.m_txtIntKwordDef.Size = new System.Drawing.Size(48, 23);
            this.m_txtIntKwordDef.TabIndex = 5;
            this.m_txtIntKwordDef.Text = "Default";
            // 
            // m_bnIntKwordHardwire
            // 
            this.m_bnIntKwordHardwire.Location = new System.Drawing.Point(24, 200);
            this.m_bnIntKwordHardwire.Name = "m_bnIntKwordHardwire";
            this.m_bnIntKwordHardwire.Size = new System.Drawing.Size(88, 23);
            this.m_bnIntKwordHardwire.TabIndex = 4;
            this.m_bnIntKwordHardwire.Text = "Hardwire";
            this.m_bnIntKwordHardwire.Click += new System.EventHandler(this.OnKwordHardwire);
            // 
            // m_bnIntKwordClear
            // 
            this.m_bnIntKwordClear.Location = new System.Drawing.Point(296, 200);
            this.m_bnIntKwordClear.Name = "m_bnIntKwordClear";
            this.m_bnIntKwordClear.TabIndex = 3;
            this.m_bnIntKwordClear.Text = "Clear";
            this.m_bnIntKwordClear.Click += new System.EventHandler(this.OnKwordClear);
            // 
            // m_bnIntKwordEdit
            // 
            this.m_bnIntKwordEdit.Location = new System.Drawing.Point(208, 200);
            this.m_bnIntKwordEdit.Name = "m_bnIntKwordEdit";
            this.m_bnIntKwordEdit.TabIndex = 2;
            this.m_bnIntKwordEdit.Text = "Edit...";
            this.m_bnIntKwordEdit.Click += new System.EventHandler(this.OnKwordEdit);
            // 
            // m_bnIntKwordAdd
            // 
            this.m_bnIntKwordAdd.Location = new System.Drawing.Point(120, 200);
            this.m_bnIntKwordAdd.Name = "m_bnIntKwordAdd";
            this.m_bnIntKwordAdd.TabIndex = 1;
            this.m_bnIntKwordAdd.Text = "Add...";
            this.m_bnIntKwordAdd.Click += new System.EventHandler(this.OnKwordAdd);
            // 
            // m_lvIntKeywords
            // 
            this.m_lvIntKeywords.CausesValidation = false;
            this.m_lvIntKeywords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                              this.m_lvColKwordGlobal,
                                                                                              this.m_lvColKwordLocal,
                                                                                              this.m_lvColKwordDisplay,
                                                                                              this.m_lvColKwordEnabled,
                                                                                              this.m_lvColKwordVisible});
            this.m_lvIntKeywords.FullRowSelect = true;
            this.m_lvIntKeywords.GridLines = true;
            this.m_lvIntKeywords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvIntKeywords.HideSelection = false;
            this.m_lvIntKeywords.Location = new System.Drawing.Point(16, 24);
            this.m_lvIntKeywords.MultiSelect = false;
            this.m_lvIntKeywords.Name = "m_lvIntKeywords";
            this.m_lvIntKeywords.Size = new System.Drawing.Size(360, 168);
            this.m_lvIntKeywords.TabIndex = 0;
            this.m_lvIntKeywords.View = System.Windows.Forms.View.Details;
            // 
            // m_lvColKwordGlobal
            // 
            this.m_lvColKwordGlobal.Text = "Global";
            this.m_lvColKwordGlobal.Width = 80;
            // 
            // m_lvColKwordLocal
            // 
            this.m_lvColKwordLocal.Text = "Local";
            this.m_lvColKwordLocal.Width = 80;
            // 
            // m_lvColKwordDisplay
            // 
            this.m_lvColKwordDisplay.Text = "Display";
            this.m_lvColKwordDisplay.Width = 80;
            // 
            // m_lvColKwordEnabled
            // 
            this.m_lvColKwordEnabled.Text = "Enabled";
            // 
            // m_lvColKwordVisible
            // 
            this.m_lvColKwordVisible.Text = "Visible";
            // 
            // m_gbOptionsInt
            // 
            this.m_gbOptionsInt.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.m_ebIntDefault,
                                                                                         this.m_txtIntDefault,
                                                                                         this.m_cbIntUseDefault,
                                                                                         this.m_cbIntAllowNegative,
                                                                                         this.m_cbIntAllowZero,
                                                                                         this.m_cbIntAllowNone,
                                                                                         this.m_cbIntAllowArbitraryInput});
            this.m_gbOptionsInt.Location = new System.Drawing.Point(16, 48);
            this.m_gbOptionsInt.Name = "m_gbOptionsInt";
            this.m_gbOptionsInt.Size = new System.Drawing.Size(168, 264);
            this.m_gbOptionsInt.TabIndex = 0;
            this.m_gbOptionsInt.TabStop = false;
            this.m_gbOptionsInt.Text = "Options";
            // 
            // m_ebIntDefault
            // 
            this.m_ebIntDefault.Location = new System.Drawing.Point(64, 152);
            this.m_ebIntDefault.Name = "m_ebIntDefault";
            this.m_ebIntDefault.Size = new System.Drawing.Size(88, 20);
            this.m_ebIntDefault.TabIndex = 6;
            this.m_ebIntDefault.Text = "";
            // 
            // m_txtIntDefault
            // 
            this.m_txtIntDefault.Location = new System.Drawing.Point(8, 152);
            this.m_txtIntDefault.Name = "m_txtIntDefault";
            this.m_txtIntDefault.Size = new System.Drawing.Size(56, 23);
            this.m_txtIntDefault.TabIndex = 5;
            this.m_txtIntDefault.Text = "Default";
            // 
            // m_cbIntUseDefault
            // 
            this.m_cbIntUseDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbIntUseDefault.Location = new System.Drawing.Point(8, 120);
            this.m_cbIntUseDefault.Name = "m_cbIntUseDefault";
            this.m_cbIntUseDefault.Size = new System.Drawing.Size(144, 24);
            this.m_cbIntUseDefault.TabIndex = 4;
            this.m_cbIntUseDefault.Text = "Use Default Value";
            this.m_cbIntUseDefault.Click += new System.EventHandler(this.OnIntUseDefaultValue);
            // 
            // m_cbIntAllowNegative
            // 
            this.m_cbIntAllowNegative.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbIntAllowNegative.Location = new System.Drawing.Point(8, 96);
            this.m_cbIntAllowNegative.Name = "m_cbIntAllowNegative";
            this.m_cbIntAllowNegative.TabIndex = 3;
            this.m_cbIntAllowNegative.Text = "Allow Negative";
            // 
            // m_cbIntAllowZero
            // 
            this.m_cbIntAllowZero.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbIntAllowZero.Location = new System.Drawing.Point(8, 72);
            this.m_cbIntAllowZero.Name = "m_cbIntAllowZero";
            this.m_cbIntAllowZero.TabIndex = 2;
            this.m_cbIntAllowZero.Text = "Allow Zero";
            // 
            // m_cbIntAllowNone
            // 
            this.m_cbIntAllowNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbIntAllowNone.Location = new System.Drawing.Point(8, 48);
            this.m_cbIntAllowNone.Name = "m_cbIntAllowNone";
            this.m_cbIntAllowNone.Size = new System.Drawing.Size(128, 24);
            this.m_cbIntAllowNone.TabIndex = 1;
            this.m_cbIntAllowNone.Text = "Allow None";
            // 
            // m_cbIntAllowArbitraryInput
            // 
            this.m_cbIntAllowArbitraryInput.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbIntAllowArbitraryInput.Location = new System.Drawing.Point(8, 24);
            this.m_cbIntAllowArbitraryInput.Name = "m_cbIntAllowArbitraryInput";
            this.m_cbIntAllowArbitraryInput.Size = new System.Drawing.Size(136, 24);
            this.m_cbIntAllowArbitraryInput.TabIndex = 0;
            this.m_cbIntAllowArbitraryInput.Text = "Allow Arbitrary Input";
            // 
            // m_txtMessage
            // 
            this.m_txtMessage.Location = new System.Drawing.Point(16, 16);
            this.m_txtMessage.Name = "m_txtMessage";
            this.m_txtMessage.Size = new System.Drawing.Size(56, 23);
            this.m_txtMessage.TabIndex = 1;
            this.m_txtMessage.Text = "Message";
            // 
            // m_ebIntMessage
            // 
            this.m_ebIntMessage.Location = new System.Drawing.Point(88, 16);
            this.m_ebIntMessage.Name = "m_ebIntMessage";
            this.m_ebIntMessage.Size = new System.Drawing.Size(504, 20);
            this.m_ebIntMessage.TabIndex = 0;
            this.m_ebIntMessage.Text = "Enter an integer";
            // 
            // m_tpDouble
            // 
            this.m_tpDouble.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.m_gbDblKwords,
                                                                                     this.m_gbDblOpts,
                                                                                     this.m_txtDblMessage,
                                                                                     this.m_ebDblMessage});
            this.m_tpDouble.Location = new System.Drawing.Point(4, 22);
            this.m_tpDouble.Name = "m_tpDouble";
            this.m_tpDouble.Size = new System.Drawing.Size(608, 318);
            this.m_tpDouble.TabIndex = 1;
            this.m_tpDouble.Text = "Double";
            // 
            // m_gbDblKwords
            // 
            this.m_gbDblKwords.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.m_bnDblKwordHardwire,
                                                                                        this.m_ebDblKwordDef,
                                                                                        this.m_txtDblKwordDef,
                                                                                        this.m_bnDblKwordClear,
                                                                                        this.m_bnDblKwordEdit,
                                                                                        this.m_bnDblKwordAdd,
                                                                                        this.m_lvDblKwords});
            this.m_gbDblKwords.Location = new System.Drawing.Point(200, 48);
            this.m_gbDblKwords.Name = "m_gbDblKwords";
            this.m_gbDblKwords.Size = new System.Drawing.Size(392, 264);
            this.m_gbDblKwords.TabIndex = 5;
            this.m_gbDblKwords.TabStop = false;
            this.m_gbDblKwords.Text = "Key Words";
            // 
            // m_bnDblKwordHardwire
            // 
            this.m_bnDblKwordHardwire.Location = new System.Drawing.Point(24, 200);
            this.m_bnDblKwordHardwire.Name = "m_bnDblKwordHardwire";
            this.m_bnDblKwordHardwire.Size = new System.Drawing.Size(88, 23);
            this.m_bnDblKwordHardwire.TabIndex = 9;
            this.m_bnDblKwordHardwire.Text = "Hardwire";
            this.m_bnDblKwordHardwire.Click += new System.EventHandler(this.OnKwordHardwire);
            // 
            // m_ebDblKwordDef
            // 
            this.m_ebDblKwordDef.Location = new System.Drawing.Point(272, 232);
            this.m_ebDblKwordDef.Name = "m_ebDblKwordDef";
            this.m_ebDblKwordDef.Size = new System.Drawing.Size(96, 20);
            this.m_ebDblKwordDef.TabIndex = 8;
            this.m_ebDblKwordDef.Text = "";
            // 
            // m_txtDblKwordDef
            // 
            this.m_txtDblKwordDef.Location = new System.Drawing.Point(216, 232);
            this.m_txtDblKwordDef.Name = "m_txtDblKwordDef";
            this.m_txtDblKwordDef.Size = new System.Drawing.Size(48, 23);
            this.m_txtDblKwordDef.TabIndex = 7;
            this.m_txtDblKwordDef.Text = "Default";
            // 
            // m_bnDblKwordClear
            // 
            this.m_bnDblKwordClear.Location = new System.Drawing.Point(296, 200);
            this.m_bnDblKwordClear.Name = "m_bnDblKwordClear";
            this.m_bnDblKwordClear.TabIndex = 3;
            this.m_bnDblKwordClear.Text = "Clear";
            this.m_bnDblKwordClear.Click += new System.EventHandler(this.OnKwordClear);
            // 
            // m_bnDblKwordEdit
            // 
            this.m_bnDblKwordEdit.Location = new System.Drawing.Point(208, 200);
            this.m_bnDblKwordEdit.Name = "m_bnDblKwordEdit";
            this.m_bnDblKwordEdit.TabIndex = 2;
            this.m_bnDblKwordEdit.Text = "Edit...";
            this.m_bnDblKwordEdit.Click += new System.EventHandler(this.OnKwordEdit);
            // 
            // m_bnDblKwordAdd
            // 
            this.m_bnDblKwordAdd.Location = new System.Drawing.Point(120, 200);
            this.m_bnDblKwordAdd.Name = "m_bnDblKwordAdd";
            this.m_bnDblKwordAdd.TabIndex = 1;
            this.m_bnDblKwordAdd.Text = "Add...";
            this.m_bnDblKwordAdd.Click += new System.EventHandler(this.OnKwordAdd);
            // 
            // m_lvDblKwords
            // 
            this.m_lvDblKwords.CausesValidation = false;
            this.m_lvDblKwords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                            this.m_lvColDblGlobal,
                                                                                            this.m_lvColDblLocal,
                                                                                            this.m_lvColDblDisplay,
                                                                                            this.m_lvColDblEnabled,
                                                                                            this.m_lvColDblVisible});
            this.m_lvDblKwords.FullRowSelect = true;
            this.m_lvDblKwords.GridLines = true;
            this.m_lvDblKwords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvDblKwords.HideSelection = false;
            this.m_lvDblKwords.Location = new System.Drawing.Point(16, 24);
            this.m_lvDblKwords.MultiSelect = false;
            this.m_lvDblKwords.Name = "m_lvDblKwords";
            this.m_lvDblKwords.Size = new System.Drawing.Size(360, 168);
            this.m_lvDblKwords.TabIndex = 0;
            this.m_lvDblKwords.View = System.Windows.Forms.View.Details;
            // 
            // m_lvColDblGlobal
            // 
            this.m_lvColDblGlobal.Text = "Global";
            this.m_lvColDblGlobal.Width = 80;
            // 
            // m_lvColDblLocal
            // 
            this.m_lvColDblLocal.Text = "Local";
            this.m_lvColDblLocal.Width = 80;
            // 
            // m_lvColDblDisplay
            // 
            this.m_lvColDblDisplay.Text = "Display";
            this.m_lvColDblDisplay.Width = 80;
            // 
            // m_lvColDblEnabled
            // 
            this.m_lvColDblEnabled.Text = "Enabled";
            // 
            // m_lvColDblVisible
            // 
            this.m_lvColDblVisible.Text = "Visible";
            // 
            // m_gbDblOpts
            // 
            this.m_gbDblOpts.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.m_ebDblDefault,
                                                                                      this.m_txtDblDefault,
                                                                                      this.m_cbDblUseDefault,
                                                                                      this.m_cbDblAllowNegative,
                                                                                      this.m_cbDblAllowZero,
                                                                                      this.m_cbDblAllowNone,
                                                                                      this.m_cbDblAllowArbitraryInput});
            this.m_gbDblOpts.Location = new System.Drawing.Point(16, 48);
            this.m_gbDblOpts.Name = "m_gbDblOpts";
            this.m_gbDblOpts.Size = new System.Drawing.Size(168, 264);
            this.m_gbDblOpts.TabIndex = 3;
            this.m_gbDblOpts.TabStop = false;
            this.m_gbDblOpts.Text = "Options";
            // 
            // m_ebDblDefault
            // 
            this.m_ebDblDefault.Location = new System.Drawing.Point(64, 152);
            this.m_ebDblDefault.Name = "m_ebDblDefault";
            this.m_ebDblDefault.Size = new System.Drawing.Size(88, 20);
            this.m_ebDblDefault.TabIndex = 6;
            this.m_ebDblDefault.Text = "";
            // 
            // m_txtDblDefault
            // 
            this.m_txtDblDefault.Location = new System.Drawing.Point(8, 152);
            this.m_txtDblDefault.Name = "m_txtDblDefault";
            this.m_txtDblDefault.Size = new System.Drawing.Size(56, 23);
            this.m_txtDblDefault.TabIndex = 5;
            this.m_txtDblDefault.Text = "Default";
            // 
            // m_cbDblUseDefault
            // 
            this.m_cbDblUseDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDblUseDefault.Location = new System.Drawing.Point(8, 120);
            this.m_cbDblUseDefault.Name = "m_cbDblUseDefault";
            this.m_cbDblUseDefault.Size = new System.Drawing.Size(144, 24);
            this.m_cbDblUseDefault.TabIndex = 4;
            this.m_cbDblUseDefault.Text = "Use Default Value";
            this.m_cbDblUseDefault.Click += new System.EventHandler(this.OnDblUseDefaultValue);
            // 
            // m_cbDblAllowNegative
            // 
            this.m_cbDblAllowNegative.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDblAllowNegative.Location = new System.Drawing.Point(8, 96);
            this.m_cbDblAllowNegative.Name = "m_cbDblAllowNegative";
            this.m_cbDblAllowNegative.TabIndex = 3;
            this.m_cbDblAllowNegative.Text = "Allow Negative";
            // 
            // m_cbDblAllowZero
            // 
            this.m_cbDblAllowZero.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDblAllowZero.Location = new System.Drawing.Point(8, 72);
            this.m_cbDblAllowZero.Name = "m_cbDblAllowZero";
            this.m_cbDblAllowZero.TabIndex = 2;
            this.m_cbDblAllowZero.Text = "Allow Zero";
            // 
            // m_cbDblAllowNone
            // 
            this.m_cbDblAllowNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDblAllowNone.Location = new System.Drawing.Point(8, 48);
            this.m_cbDblAllowNone.Name = "m_cbDblAllowNone";
            this.m_cbDblAllowNone.Size = new System.Drawing.Size(128, 24);
            this.m_cbDblAllowNone.TabIndex = 1;
            this.m_cbDblAllowNone.Text = "Allow None";
            // 
            // m_cbDblAllowArbitraryInput
            // 
            this.m_cbDblAllowArbitraryInput.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDblAllowArbitraryInput.Location = new System.Drawing.Point(8, 24);
            this.m_cbDblAllowArbitraryInput.Name = "m_cbDblAllowArbitraryInput";
            this.m_cbDblAllowArbitraryInput.Size = new System.Drawing.Size(136, 24);
            this.m_cbDblAllowArbitraryInput.TabIndex = 0;
            this.m_cbDblAllowArbitraryInput.Text = "Allow Arbitrary Input";
            // 
            // m_txtDblMessage
            // 
            this.m_txtDblMessage.Location = new System.Drawing.Point(16, 16);
            this.m_txtDblMessage.Name = "m_txtDblMessage";
            this.m_txtDblMessage.Size = new System.Drawing.Size(56, 23);
            this.m_txtDblMessage.TabIndex = 4;
            this.m_txtDblMessage.Text = "Message";
            // 
            // m_ebDblMessage
            // 
            this.m_ebDblMessage.Location = new System.Drawing.Point(88, 16);
            this.m_ebDblMessage.Name = "m_ebDblMessage";
            this.m_ebDblMessage.Size = new System.Drawing.Size(504, 20);
            this.m_ebDblMessage.TabIndex = 2;
            this.m_ebDblMessage.Text = "Enter a double";
            // 
            // m_tpDistance
            // 
            this.m_tpDistance.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.m_gbDistKwords,
                                                                                       this.m_gbDistOptions,
                                                                                       this.m_txtDistMessage,
                                                                                       this.m_ebDistMessage});
            this.m_tpDistance.Location = new System.Drawing.Point(4, 22);
            this.m_tpDistance.Name = "m_tpDistance";
            this.m_tpDistance.Size = new System.Drawing.Size(608, 318);
            this.m_tpDistance.TabIndex = 2;
            this.m_tpDistance.Text = "Distance";
            // 
            // m_gbDistKwords
            // 
            this.m_gbDistKwords.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.m_bnDistKwordHardwire,
                                                                                         this.m_ebDistKwordDef,
                                                                                         this.m_txtDistKwordDef,
                                                                                         this.m_bnDistKwordClear,
                                                                                         this.m_bnDistKwordEdit,
                                                                                         this.m_bnDistKwordAdd,
                                                                                         this.m_lvDistKwords});
            this.m_gbDistKwords.Location = new System.Drawing.Point(200, 48);
            this.m_gbDistKwords.Name = "m_gbDistKwords";
            this.m_gbDistKwords.Size = new System.Drawing.Size(392, 264);
            this.m_gbDistKwords.TabIndex = 6;
            this.m_gbDistKwords.TabStop = false;
            this.m_gbDistKwords.Text = "Key Words";
            // 
            // m_bnDistKwordHardwire
            // 
            this.m_bnDistKwordHardwire.Location = new System.Drawing.Point(24, 200);
            this.m_bnDistKwordHardwire.Name = "m_bnDistKwordHardwire";
            this.m_bnDistKwordHardwire.Size = new System.Drawing.Size(88, 23);
            this.m_bnDistKwordHardwire.TabIndex = 12;
            this.m_bnDistKwordHardwire.Text = "Hardwire";
            this.m_bnDistKwordHardwire.Click += new System.EventHandler(this.OnKwordHardwire);
            // 
            // m_ebDistKwordDef
            // 
            this.m_ebDistKwordDef.Location = new System.Drawing.Point(272, 232);
            this.m_ebDistKwordDef.Name = "m_ebDistKwordDef";
            this.m_ebDistKwordDef.Size = new System.Drawing.Size(96, 20);
            this.m_ebDistKwordDef.TabIndex = 11;
            this.m_ebDistKwordDef.Text = "";
            // 
            // m_txtDistKwordDef
            // 
            this.m_txtDistKwordDef.Location = new System.Drawing.Point(216, 232);
            this.m_txtDistKwordDef.Name = "m_txtDistKwordDef";
            this.m_txtDistKwordDef.Size = new System.Drawing.Size(48, 23);
            this.m_txtDistKwordDef.TabIndex = 10;
            this.m_txtDistKwordDef.Text = "Default";
            // 
            // m_bnDistKwordClear
            // 
            this.m_bnDistKwordClear.Location = new System.Drawing.Point(296, 200);
            this.m_bnDistKwordClear.Name = "m_bnDistKwordClear";
            this.m_bnDistKwordClear.TabIndex = 3;
            this.m_bnDistKwordClear.Text = "Clear";
            this.m_bnDistKwordClear.Click += new System.EventHandler(this.OnKwordClear);
            // 
            // m_bnDistKwordEdit
            // 
            this.m_bnDistKwordEdit.Location = new System.Drawing.Point(208, 200);
            this.m_bnDistKwordEdit.Name = "m_bnDistKwordEdit";
            this.m_bnDistKwordEdit.TabIndex = 2;
            this.m_bnDistKwordEdit.Text = "Edit...";
            this.m_bnDistKwordEdit.Click += new System.EventHandler(this.OnKwordEdit);
            // 
            // m_bnDistKwordAdd
            // 
            this.m_bnDistKwordAdd.Location = new System.Drawing.Point(120, 200);
            this.m_bnDistKwordAdd.Name = "m_bnDistKwordAdd";
            this.m_bnDistKwordAdd.TabIndex = 1;
            this.m_bnDistKwordAdd.Text = "Add...";
            this.m_bnDistKwordAdd.Click += new System.EventHandler(this.OnKwordAdd);
            // 
            // m_lvDistKwords
            // 
            this.m_lvDistKwords.CausesValidation = false;
            this.m_lvDistKwords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                             this.m_lvColDistGlobal,
                                                                                             this.m_lvColDistLocal,
                                                                                             this.m_lvColDistDisplay,
                                                                                             this.m_lvColDistEnabled,
                                                                                             this.m_lvColDistVisible});
            this.m_lvDistKwords.FullRowSelect = true;
            this.m_lvDistKwords.GridLines = true;
            this.m_lvDistKwords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvDistKwords.HideSelection = false;
            this.m_lvDistKwords.Location = new System.Drawing.Point(16, 24);
            this.m_lvDistKwords.MultiSelect = false;
            this.m_lvDistKwords.Name = "m_lvDistKwords";
            this.m_lvDistKwords.Size = new System.Drawing.Size(360, 168);
            this.m_lvDistKwords.TabIndex = 0;
            this.m_lvDistKwords.View = System.Windows.Forms.View.Details;
            // 
            // m_lvColDistGlobal
            // 
            this.m_lvColDistGlobal.Text = "Global";
            this.m_lvColDistGlobal.Width = 80;
            // 
            // m_lvColDistLocal
            // 
            this.m_lvColDistLocal.Text = "Local";
            this.m_lvColDistLocal.Width = 80;
            // 
            // m_lvColDistDisplay
            // 
            this.m_lvColDistDisplay.Text = "Display";
            this.m_lvColDistDisplay.Width = 80;
            // 
            // m_lvColDistEnabled
            // 
            this.m_lvColDistEnabled.Text = "Enabled";
            // 
            // m_lvColDistVisible
            // 
            this.m_lvColDistVisible.Text = "Visible";
            // 
            // m_gbDistOptions
            // 
            this.m_gbDistOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                          this.m_cbDistOnly2d,
                                                                                          this.m_cbDistUseDashedLine,
                                                                                          this.m_cbDistUseBasePt,
                                                                                          this.m_ebDistDefault,
                                                                                          this.m_txtDistDefault,
                                                                                          this.m_cbDistUseDefault,
                                                                                          this.m_cbDistAllowNegative,
                                                                                          this.m_cbDistAllowZero,
                                                                                          this.m_cbDistAllowNone,
                                                                                          this.m_cbDistAllowArbitraryInput});
            this.m_gbDistOptions.Location = new System.Drawing.Point(16, 48);
            this.m_gbDistOptions.Name = "m_gbDistOptions";
            this.m_gbDistOptions.Size = new System.Drawing.Size(168, 264);
            this.m_gbDistOptions.TabIndex = 4;
            this.m_gbDistOptions.TabStop = false;
            this.m_gbDistOptions.Text = "Options";
            // 
            // m_cbDistOnly2d
            // 
            this.m_cbDistOnly2d.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDistOnly2d.Location = new System.Drawing.Point(8, 168);
            this.m_cbDistOnly2d.Name = "m_cbDistOnly2d";
            this.m_cbDistOnly2d.TabIndex = 10;
            this.m_cbDistOnly2d.Text = "Only 2D";
            // 
            // m_cbDistUseDashedLine
            // 
            this.m_cbDistUseDashedLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDistUseDashedLine.Location = new System.Drawing.Point(8, 144);
            this.m_cbDistUseDashedLine.Name = "m_cbDistUseDashedLine";
            this.m_cbDistUseDashedLine.Size = new System.Drawing.Size(144, 24);
            this.m_cbDistUseDashedLine.TabIndex = 9;
            this.m_cbDistUseDashedLine.Text = "Use Dashed Line";
            // 
            // m_cbDistUseBasePt
            // 
            this.m_cbDistUseBasePt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDistUseBasePt.Location = new System.Drawing.Point(8, 120);
            this.m_cbDistUseBasePt.Name = "m_cbDistUseBasePt";
            this.m_cbDistUseBasePt.Size = new System.Drawing.Size(144, 24);
            this.m_cbDistUseBasePt.TabIndex = 8;
            this.m_cbDistUseBasePt.Text = "Use Base Point";
            // 
            // m_ebDistDefault
            // 
            this.m_ebDistDefault.Location = new System.Drawing.Point(64, 224);
            this.m_ebDistDefault.Name = "m_ebDistDefault";
            this.m_ebDistDefault.Size = new System.Drawing.Size(88, 20);
            this.m_ebDistDefault.TabIndex = 6;
            this.m_ebDistDefault.Text = "";
            // 
            // m_txtDistDefault
            // 
            this.m_txtDistDefault.Location = new System.Drawing.Point(8, 224);
            this.m_txtDistDefault.Name = "m_txtDistDefault";
            this.m_txtDistDefault.Size = new System.Drawing.Size(56, 23);
            this.m_txtDistDefault.TabIndex = 5;
            this.m_txtDistDefault.Text = "Default";
            // 
            // m_cbDistUseDefault
            // 
            this.m_cbDistUseDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDistUseDefault.Location = new System.Drawing.Point(8, 192);
            this.m_cbDistUseDefault.Name = "m_cbDistUseDefault";
            this.m_cbDistUseDefault.Size = new System.Drawing.Size(144, 24);
            this.m_cbDistUseDefault.TabIndex = 4;
            this.m_cbDistUseDefault.Text = "Use Default Value";
            this.m_cbDistUseDefault.Click += new System.EventHandler(this.OnDistUseDefaultValue);
            // 
            // m_cbDistAllowNegative
            // 
            this.m_cbDistAllowNegative.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDistAllowNegative.Location = new System.Drawing.Point(8, 96);
            this.m_cbDistAllowNegative.Name = "m_cbDistAllowNegative";
            this.m_cbDistAllowNegative.TabIndex = 3;
            this.m_cbDistAllowNegative.Text = "Allow Negative";
            // 
            // m_cbDistAllowZero
            // 
            this.m_cbDistAllowZero.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDistAllowZero.Location = new System.Drawing.Point(8, 72);
            this.m_cbDistAllowZero.Name = "m_cbDistAllowZero";
            this.m_cbDistAllowZero.TabIndex = 2;
            this.m_cbDistAllowZero.Text = "Allow Zero";
            // 
            // m_cbDistAllowNone
            // 
            this.m_cbDistAllowNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDistAllowNone.Location = new System.Drawing.Point(8, 48);
            this.m_cbDistAllowNone.Name = "m_cbDistAllowNone";
            this.m_cbDistAllowNone.Size = new System.Drawing.Size(128, 24);
            this.m_cbDistAllowNone.TabIndex = 1;
            this.m_cbDistAllowNone.Text = "Allow None";
            // 
            // m_cbDistAllowArbitraryInput
            // 
            this.m_cbDistAllowArbitraryInput.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbDistAllowArbitraryInput.Location = new System.Drawing.Point(8, 24);
            this.m_cbDistAllowArbitraryInput.Name = "m_cbDistAllowArbitraryInput";
            this.m_cbDistAllowArbitraryInput.Size = new System.Drawing.Size(136, 24);
            this.m_cbDistAllowArbitraryInput.TabIndex = 0;
            this.m_cbDistAllowArbitraryInput.Text = "Allow Arbitrary Input";
            // 
            // m_txtDistMessage
            // 
            this.m_txtDistMessage.Location = new System.Drawing.Point(16, 16);
            this.m_txtDistMessage.Name = "m_txtDistMessage";
            this.m_txtDistMessage.Size = new System.Drawing.Size(56, 23);
            this.m_txtDistMessage.TabIndex = 5;
            this.m_txtDistMessage.Text = "Message";
            // 
            // m_ebDistMessage
            // 
            this.m_ebDistMessage.Location = new System.Drawing.Point(88, 16);
            this.m_ebDistMessage.Name = "m_ebDistMessage";
            this.m_ebDistMessage.Size = new System.Drawing.Size(504, 20);
            this.m_ebDistMessage.TabIndex = 3;
            this.m_ebDistMessage.Text = "Enter a distance";
            // 
            // m_tpAngle
            // 
            this.m_tpAngle.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.m_txtAngMessage,
                                                                                    this.m_ebAngMessage,
                                                                                    this.m_gbAngOptions,
                                                                                    this.m_gbAngKwords});
            this.m_tpAngle.Location = new System.Drawing.Point(4, 22);
            this.m_tpAngle.Name = "m_tpAngle";
            this.m_tpAngle.Size = new System.Drawing.Size(608, 318);
            this.m_tpAngle.TabIndex = 7;
            this.m_tpAngle.Text = "Angle";
            // 
            // m_txtAngMessage
            // 
            this.m_txtAngMessage.Location = new System.Drawing.Point(16, 16);
            this.m_txtAngMessage.Name = "m_txtAngMessage";
            this.m_txtAngMessage.Size = new System.Drawing.Size(56, 23);
            this.m_txtAngMessage.TabIndex = 9;
            this.m_txtAngMessage.Text = "Message";
            // 
            // m_ebAngMessage
            // 
            this.m_ebAngMessage.Location = new System.Drawing.Point(88, 16);
            this.m_ebAngMessage.Name = "m_ebAngMessage";
            this.m_ebAngMessage.Size = new System.Drawing.Size(504, 20);
            this.m_ebAngMessage.TabIndex = 7;
            this.m_ebAngMessage.Text = "Enter an angle";
            // 
            // m_gbAngOptions
            // 
            this.m_gbAngOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.m_cbAngUseDefault,
                                                                                         this.m_cbAngUseDashedLine,
                                                                                         this.m_ebAngDefault,
                                                                                         this.m_txtAngDefault,
                                                                                         this.m_cbAngUseBasePt,
                                                                                         this.m_cbAngAllowZero,
                                                                                         this.m_cbAngAllowNone,
                                                                                         this.m_cbAngAllowArbitraryInput});
            this.m_gbAngOptions.Location = new System.Drawing.Point(16, 48);
            this.m_gbAngOptions.Name = "m_gbAngOptions";
            this.m_gbAngOptions.Size = new System.Drawing.Size(168, 264);
            this.m_gbAngOptions.TabIndex = 8;
            this.m_gbAngOptions.TabStop = false;
            this.m_gbAngOptions.Text = "Options";
            // 
            // m_cbAngUseDefault
            // 
            this.m_cbAngUseDefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbAngUseDefault.Location = new System.Drawing.Point(8, 144);
            this.m_cbAngUseDefault.Name = "m_cbAngUseDefault";
            this.m_cbAngUseDefault.Size = new System.Drawing.Size(120, 24);
            this.m_cbAngUseDefault.TabIndex = 8;
            this.m_cbAngUseDefault.Text = "Use Default";
            this.m_cbAngUseDefault.Click += new System.EventHandler(this.OnAngUseDefaultValue);
            // 
            // m_cbAngUseDashedLine
            // 
            this.m_cbAngUseDashedLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbAngUseDashedLine.Location = new System.Drawing.Point(8, 120);
            this.m_cbAngUseDashedLine.Name = "m_cbAngUseDashedLine";
            this.m_cbAngUseDashedLine.Size = new System.Drawing.Size(144, 24);
            this.m_cbAngUseDashedLine.TabIndex = 7;
            this.m_cbAngUseDashedLine.Text = "Use Dashed Line";
            // 
            // m_ebAngDefault
            // 
            this.m_ebAngDefault.Location = new System.Drawing.Point(64, 176);
            this.m_ebAngDefault.Name = "m_ebAngDefault";
            this.m_ebAngDefault.Size = new System.Drawing.Size(88, 20);
            this.m_ebAngDefault.TabIndex = 6;
            this.m_ebAngDefault.Text = "";
            // 
            // m_txtAngDefault
            // 
            this.m_txtAngDefault.Location = new System.Drawing.Point(8, 176);
            this.m_txtAngDefault.Name = "m_txtAngDefault";
            this.m_txtAngDefault.Size = new System.Drawing.Size(56, 23);
            this.m_txtAngDefault.TabIndex = 5;
            this.m_txtAngDefault.Text = "Default";
            // 
            // m_cbAngUseBasePt
            // 
            this.m_cbAngUseBasePt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbAngUseBasePt.Location = new System.Drawing.Point(8, 96);
            this.m_cbAngUseBasePt.Name = "m_cbAngUseBasePt";
            this.m_cbAngUseBasePt.Size = new System.Drawing.Size(144, 24);
            this.m_cbAngUseBasePt.TabIndex = 4;
            this.m_cbAngUseBasePt.Text = "Use Base Point";
            // 
            // m_cbAngAllowZero
            // 
            this.m_cbAngAllowZero.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbAngAllowZero.Location = new System.Drawing.Point(8, 72);
            this.m_cbAngAllowZero.Name = "m_cbAngAllowZero";
            this.m_cbAngAllowZero.TabIndex = 2;
            this.m_cbAngAllowZero.Text = "Allow Zero";
            // 
            // m_cbAngAllowNone
            // 
            this.m_cbAngAllowNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbAngAllowNone.Location = new System.Drawing.Point(8, 48);
            this.m_cbAngAllowNone.Name = "m_cbAngAllowNone";
            this.m_cbAngAllowNone.Size = new System.Drawing.Size(128, 24);
            this.m_cbAngAllowNone.TabIndex = 1;
            this.m_cbAngAllowNone.Text = "Allow None";
            // 
            // m_cbAngAllowArbitraryInput
            // 
            this.m_cbAngAllowArbitraryInput.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbAngAllowArbitraryInput.Location = new System.Drawing.Point(8, 24);
            this.m_cbAngAllowArbitraryInput.Name = "m_cbAngAllowArbitraryInput";
            this.m_cbAngAllowArbitraryInput.Size = new System.Drawing.Size(136, 24);
            this.m_cbAngAllowArbitraryInput.TabIndex = 0;
            this.m_cbAngAllowArbitraryInput.Text = "Allow Arbitrary Input";
            // 
            // m_gbAngKwords
            // 
            this.m_gbAngKwords.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.m_bnAngKwordHardwire,
                                                                                        this.m_ebAngKwordDef,
                                                                                        this.m_txtAngKwordDef,
                                                                                        this.m_bnAngKwordClear,
                                                                                        this.m_bnAngKwordEdit,
                                                                                        this.m_bnAngKwordAdd,
                                                                                        this.m_lvAngKwords});
            this.m_gbAngKwords.Location = new System.Drawing.Point(200, 48);
            this.m_gbAngKwords.Name = "m_gbAngKwords";
            this.m_gbAngKwords.Size = new System.Drawing.Size(392, 264);
            this.m_gbAngKwords.TabIndex = 10;
            this.m_gbAngKwords.TabStop = false;
            this.m_gbAngKwords.Text = "Key Words";
            // 
            // m_bnAngKwordHardwire
            // 
            this.m_bnAngKwordHardwire.Location = new System.Drawing.Point(24, 200);
            this.m_bnAngKwordHardwire.Name = "m_bnAngKwordHardwire";
            this.m_bnAngKwordHardwire.Size = new System.Drawing.Size(88, 23);
            this.m_bnAngKwordHardwire.TabIndex = 15;
            this.m_bnAngKwordHardwire.Text = "Hardwire";
            this.m_bnAngKwordHardwire.Click += new System.EventHandler(this.OnKwordHardwire);
            // 
            // m_ebAngKwordDef
            // 
            this.m_ebAngKwordDef.Location = new System.Drawing.Point(272, 232);
            this.m_ebAngKwordDef.Name = "m_ebAngKwordDef";
            this.m_ebAngKwordDef.Size = new System.Drawing.Size(96, 20);
            this.m_ebAngKwordDef.TabIndex = 14;
            this.m_ebAngKwordDef.Text = "";
            // 
            // m_txtAngKwordDef
            // 
            this.m_txtAngKwordDef.Location = new System.Drawing.Point(216, 232);
            this.m_txtAngKwordDef.Name = "m_txtAngKwordDef";
            this.m_txtAngKwordDef.Size = new System.Drawing.Size(48, 23);
            this.m_txtAngKwordDef.TabIndex = 13;
            this.m_txtAngKwordDef.Text = "Default";
            // 
            // m_bnAngKwordClear
            // 
            this.m_bnAngKwordClear.Location = new System.Drawing.Point(296, 200);
            this.m_bnAngKwordClear.Name = "m_bnAngKwordClear";
            this.m_bnAngKwordClear.TabIndex = 3;
            this.m_bnAngKwordClear.Text = "Clear";
            this.m_bnAngKwordClear.Click += new System.EventHandler(this.OnKwordClear);
            // 
            // m_bnAngKwordEdit
            // 
            this.m_bnAngKwordEdit.Location = new System.Drawing.Point(208, 200);
            this.m_bnAngKwordEdit.Name = "m_bnAngKwordEdit";
            this.m_bnAngKwordEdit.TabIndex = 2;
            this.m_bnAngKwordEdit.Text = "Edit...";
            this.m_bnAngKwordEdit.Click += new System.EventHandler(this.OnKwordEdit);
            // 
            // m_bnAngKwordAdd
            // 
            this.m_bnAngKwordAdd.Location = new System.Drawing.Point(120, 200);
            this.m_bnAngKwordAdd.Name = "m_bnAngKwordAdd";
            this.m_bnAngKwordAdd.TabIndex = 1;
            this.m_bnAngKwordAdd.Text = "Add...";
            this.m_bnAngKwordAdd.Click += new System.EventHandler(this.OnKwordAdd);
            // 
            // m_lvAngKwords
            // 
            this.m_lvAngKwords.CausesValidation = false;
            this.m_lvAngKwords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                            this.m_lvColAngGlobal,
                                                                                            this.m_lvColAngLocal,
                                                                                            this.m_lvColAngDisplay,
                                                                                            this.m_lvColAngEnabled,
                                                                                            this.m_lvColAngVisible});
            this.m_lvAngKwords.FullRowSelect = true;
            this.m_lvAngKwords.GridLines = true;
            this.m_lvAngKwords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvAngKwords.HideSelection = false;
            this.m_lvAngKwords.Location = new System.Drawing.Point(16, 24);
            this.m_lvAngKwords.MultiSelect = false;
            this.m_lvAngKwords.Name = "m_lvAngKwords";
            this.m_lvAngKwords.Size = new System.Drawing.Size(360, 168);
            this.m_lvAngKwords.TabIndex = 0;
            this.m_lvAngKwords.View = System.Windows.Forms.View.Details;
            // 
            // m_lvColAngGlobal
            // 
            this.m_lvColAngGlobal.Text = "Global";
            this.m_lvColAngGlobal.Width = 80;
            // 
            // m_lvColAngLocal
            // 
            this.m_lvColAngLocal.Text = "Local";
            this.m_lvColAngLocal.Width = 80;
            // 
            // m_lvColAngDisplay
            // 
            this.m_lvColAngDisplay.Text = "Display";
            this.m_lvColAngDisplay.Width = 80;
            // 
            // m_lvColAngEnabled
            // 
            this.m_lvColAngEnabled.Text = "Enabled";
            // 
            // m_lvColAngVisible
            // 
            this.m_lvColAngVisible.Text = "Visible";
            // 
            // m_tpCorner
            // 
            this.m_tpCorner.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.m_gbCrnOptions,
                                                                                     this.m_gbCrnKwords,
                                                                                     this.m_ebCrnMessage,
                                                                                     this.m_txtCrnMessage});
            this.m_tpCorner.Location = new System.Drawing.Point(4, 22);
            this.m_tpCorner.Name = "m_tpCorner";
            this.m_tpCorner.Size = new System.Drawing.Size(608, 318);
            this.m_tpCorner.TabIndex = 3;
            this.m_tpCorner.Text = "Corner";
            // 
            // m_gbCrnOptions
            // 
            this.m_gbCrnOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.m_cbCrnUseDashedLine,
                                                                                         this.m_cbCrnLimitsChecked,
                                                                                         this.m_cbCrnAllowNone,
                                                                                         this.m_cbCrnAllowArbitraryInput});
            this.m_gbCrnOptions.Location = new System.Drawing.Point(16, 48);
            this.m_gbCrnOptions.Name = "m_gbCrnOptions";
            this.m_gbCrnOptions.Size = new System.Drawing.Size(168, 264);
            this.m_gbCrnOptions.TabIndex = 12;
            this.m_gbCrnOptions.TabStop = false;
            this.m_gbCrnOptions.Text = "Options";
            // 
            // m_cbCrnUseDashedLine
            // 
            this.m_cbCrnUseDashedLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbCrnUseDashedLine.Location = new System.Drawing.Point(8, 96);
            this.m_cbCrnUseDashedLine.Name = "m_cbCrnUseDashedLine";
            this.m_cbCrnUseDashedLine.Size = new System.Drawing.Size(144, 24);
            this.m_cbCrnUseDashedLine.TabIndex = 7;
            this.m_cbCrnUseDashedLine.Text = "Use Dashed Line";
            // 
            // m_cbCrnLimitsChecked
            // 
            this.m_cbCrnLimitsChecked.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbCrnLimitsChecked.Location = new System.Drawing.Point(8, 72);
            this.m_cbCrnLimitsChecked.Name = "m_cbCrnLimitsChecked";
            this.m_cbCrnLimitsChecked.TabIndex = 2;
            this.m_cbCrnLimitsChecked.Text = "Limits Checked";
            // 
            // m_cbCrnAllowNone
            // 
            this.m_cbCrnAllowNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbCrnAllowNone.Location = new System.Drawing.Point(8, 48);
            this.m_cbCrnAllowNone.Name = "m_cbCrnAllowNone";
            this.m_cbCrnAllowNone.Size = new System.Drawing.Size(128, 24);
            this.m_cbCrnAllowNone.TabIndex = 1;
            this.m_cbCrnAllowNone.Text = "Allow None";
            // 
            // m_cbCrnAllowArbitraryInput
            // 
            this.m_cbCrnAllowArbitraryInput.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbCrnAllowArbitraryInput.Location = new System.Drawing.Point(8, 24);
            this.m_cbCrnAllowArbitraryInput.Name = "m_cbCrnAllowArbitraryInput";
            this.m_cbCrnAllowArbitraryInput.Size = new System.Drawing.Size(136, 24);
            this.m_cbCrnAllowArbitraryInput.TabIndex = 0;
            this.m_cbCrnAllowArbitraryInput.Text = "Allow Arbitrary Input";
            // 
            // m_gbCrnKwords
            // 
            this.m_gbCrnKwords.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.m_bnCrnKwordHardwire,
                                                                                        this.m_ebCrnKwordDef,
                                                                                        this.m_txtCrnKwordDef,
                                                                                        this.m_bnCrnKwordClear,
                                                                                        this.m_bnCrnKwordEdit,
                                                                                        this.m_bnCrnKwordAdd,
                                                                                        this.m_lvCrnKwords});
            this.m_gbCrnKwords.Location = new System.Drawing.Point(200, 48);
            this.m_gbCrnKwords.Name = "m_gbCrnKwords";
            this.m_gbCrnKwords.Size = new System.Drawing.Size(392, 264);
            this.m_gbCrnKwords.TabIndex = 14;
            this.m_gbCrnKwords.TabStop = false;
            this.m_gbCrnKwords.Text = "Key Words";
            // 
            // m_bnCrnKwordHardwire
            // 
            this.m_bnCrnKwordHardwire.Location = new System.Drawing.Point(24, 200);
            this.m_bnCrnKwordHardwire.Name = "m_bnCrnKwordHardwire";
            this.m_bnCrnKwordHardwire.Size = new System.Drawing.Size(88, 23);
            this.m_bnCrnKwordHardwire.TabIndex = 18;
            this.m_bnCrnKwordHardwire.Text = "Hardwire";
            this.m_bnCrnKwordHardwire.Click += new System.EventHandler(this.OnKwordHardwire);
            // 
            // m_ebCrnKwordDef
            // 
            this.m_ebCrnKwordDef.Location = new System.Drawing.Point(272, 232);
            this.m_ebCrnKwordDef.Name = "m_ebCrnKwordDef";
            this.m_ebCrnKwordDef.Size = new System.Drawing.Size(96, 20);
            this.m_ebCrnKwordDef.TabIndex = 17;
            this.m_ebCrnKwordDef.Text = "";
            // 
            // m_txtCrnKwordDef
            // 
            this.m_txtCrnKwordDef.Location = new System.Drawing.Point(216, 232);
            this.m_txtCrnKwordDef.Name = "m_txtCrnKwordDef";
            this.m_txtCrnKwordDef.Size = new System.Drawing.Size(48, 23);
            this.m_txtCrnKwordDef.TabIndex = 16;
            this.m_txtCrnKwordDef.Text = "Default";
            // 
            // m_bnCrnKwordClear
            // 
            this.m_bnCrnKwordClear.Location = new System.Drawing.Point(296, 200);
            this.m_bnCrnKwordClear.Name = "m_bnCrnKwordClear";
            this.m_bnCrnKwordClear.TabIndex = 3;
            this.m_bnCrnKwordClear.Text = "Clear";
            this.m_bnCrnKwordClear.Click += new System.EventHandler(this.OnKwordClear);
            // 
            // m_bnCrnKwordEdit
            // 
            this.m_bnCrnKwordEdit.Location = new System.Drawing.Point(208, 200);
            this.m_bnCrnKwordEdit.Name = "m_bnCrnKwordEdit";
            this.m_bnCrnKwordEdit.TabIndex = 2;
            this.m_bnCrnKwordEdit.Text = "Edit...";
            this.m_bnCrnKwordEdit.Click += new System.EventHandler(this.OnKwordEdit);
            // 
            // m_bnCrnKwordAdd
            // 
            this.m_bnCrnKwordAdd.Location = new System.Drawing.Point(120, 200);
            this.m_bnCrnKwordAdd.Name = "m_bnCrnKwordAdd";
            this.m_bnCrnKwordAdd.TabIndex = 1;
            this.m_bnCrnKwordAdd.Text = "Add...";
            this.m_bnCrnKwordAdd.Click += new System.EventHandler(this.OnKwordAdd);
            // 
            // m_lvCrnKwords
            // 
            this.m_lvCrnKwords.CausesValidation = false;
            this.m_lvCrnKwords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                            this.m_lvColCrnGlobal,
                                                                                            this.m_lvColCrnLocal,
                                                                                            this.m_lvColCrnDisplay,
                                                                                            this.m_lvColCrnEnabled,
                                                                                            this.m_lvColCrnVisible});
            this.m_lvCrnKwords.FullRowSelect = true;
            this.m_lvCrnKwords.GridLines = true;
            this.m_lvCrnKwords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvCrnKwords.HideSelection = false;
            this.m_lvCrnKwords.Location = new System.Drawing.Point(16, 24);
            this.m_lvCrnKwords.MultiSelect = false;
            this.m_lvCrnKwords.Name = "m_lvCrnKwords";
            this.m_lvCrnKwords.Size = new System.Drawing.Size(360, 168);
            this.m_lvCrnKwords.TabIndex = 0;
            this.m_lvCrnKwords.View = System.Windows.Forms.View.Details;
            // 
            // m_lvColCrnGlobal
            // 
            this.m_lvColCrnGlobal.Text = "Global";
            this.m_lvColCrnGlobal.Width = 80;
            // 
            // m_lvColCrnLocal
            // 
            this.m_lvColCrnLocal.Text = "Local";
            this.m_lvColCrnLocal.Width = 80;
            // 
            // m_lvColCrnDisplay
            // 
            this.m_lvColCrnDisplay.Text = "Display";
            this.m_lvColCrnDisplay.Width = 80;
            // 
            // m_lvColCrnEnabled
            // 
            this.m_lvColCrnEnabled.Text = "Enabled";
            // 
            // m_lvColCrnVisible
            // 
            this.m_lvColCrnVisible.Text = "Visible";
            // 
            // m_ebCrnMessage
            // 
            this.m_ebCrnMessage.Location = new System.Drawing.Point(88, 16);
            this.m_ebCrnMessage.Name = "m_ebCrnMessage";
            this.m_ebCrnMessage.Size = new System.Drawing.Size(504, 20);
            this.m_ebCrnMessage.TabIndex = 11;
            this.m_ebCrnMessage.Text = "Enter other corner";
            // 
            // m_txtCrnMessage
            // 
            this.m_txtCrnMessage.Location = new System.Drawing.Point(16, 16);
            this.m_txtCrnMessage.Name = "m_txtCrnMessage";
            this.m_txtCrnMessage.Size = new System.Drawing.Size(56, 23);
            this.m_txtCrnMessage.TabIndex = 13;
            this.m_txtCrnMessage.Text = "Message";
            // 
            // m_tpPoint
            // 
            this.m_tpPoint.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.m_gbPtOptions,
                                                                                    this.m_gbPtKwords,
                                                                                    this.m_ebPtMessage,
                                                                                    this.m_txtPtMessage});
            this.m_tpPoint.Location = new System.Drawing.Point(4, 22);
            this.m_tpPoint.Name = "m_tpPoint";
            this.m_tpPoint.Size = new System.Drawing.Size(608, 318);
            this.m_tpPoint.TabIndex = 4;
            this.m_tpPoint.Text = "Point";
            // 
            // m_gbPtOptions
            // 
            this.m_gbPtOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.m_cbPtUseBasePt,
                                                                                        this.m_cbPtUseDashedLine,
                                                                                        this.m_cbPtLimitsChecked,
                                                                                        this.m_cbPtAllowNone,
                                                                                        this.m_cbPtAllowArbitraryInput});
            this.m_gbPtOptions.Location = new System.Drawing.Point(16, 48);
            this.m_gbPtOptions.Name = "m_gbPtOptions";
            this.m_gbPtOptions.Size = new System.Drawing.Size(168, 264);
            this.m_gbPtOptions.TabIndex = 16;
            this.m_gbPtOptions.TabStop = false;
            this.m_gbPtOptions.Text = "Options";
            // 
            // m_cbPtUseBasePt
            // 
            this.m_cbPtUseBasePt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbPtUseBasePt.Location = new System.Drawing.Point(8, 120);
            this.m_cbPtUseBasePt.Name = "m_cbPtUseBasePt";
            this.m_cbPtUseBasePt.Size = new System.Drawing.Size(128, 24);
            this.m_cbPtUseBasePt.TabIndex = 8;
            this.m_cbPtUseBasePt.Text = "Use Base Point";
            // 
            // m_cbPtUseDashedLine
            // 
            this.m_cbPtUseDashedLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbPtUseDashedLine.Location = new System.Drawing.Point(8, 96);
            this.m_cbPtUseDashedLine.Name = "m_cbPtUseDashedLine";
            this.m_cbPtUseDashedLine.Size = new System.Drawing.Size(144, 24);
            this.m_cbPtUseDashedLine.TabIndex = 7;
            this.m_cbPtUseDashedLine.Text = "Use Dashed Line";
            // 
            // m_cbPtLimitsChecked
            // 
            this.m_cbPtLimitsChecked.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbPtLimitsChecked.Location = new System.Drawing.Point(8, 72);
            this.m_cbPtLimitsChecked.Name = "m_cbPtLimitsChecked";
            this.m_cbPtLimitsChecked.TabIndex = 2;
            this.m_cbPtLimitsChecked.Text = "Limits Checked";
            // 
            // m_cbPtAllowNone
            // 
            this.m_cbPtAllowNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbPtAllowNone.Location = new System.Drawing.Point(8, 48);
            this.m_cbPtAllowNone.Name = "m_cbPtAllowNone";
            this.m_cbPtAllowNone.Size = new System.Drawing.Size(128, 24);
            this.m_cbPtAllowNone.TabIndex = 1;
            this.m_cbPtAllowNone.Text = "Allow None";
            // 
            // m_cbPtAllowArbitraryInput
            // 
            this.m_cbPtAllowArbitraryInput.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbPtAllowArbitraryInput.Location = new System.Drawing.Point(8, 24);
            this.m_cbPtAllowArbitraryInput.Name = "m_cbPtAllowArbitraryInput";
            this.m_cbPtAllowArbitraryInput.Size = new System.Drawing.Size(136, 24);
            this.m_cbPtAllowArbitraryInput.TabIndex = 0;
            this.m_cbPtAllowArbitraryInput.Text = "Allow Arbitrary Input";
            // 
            // m_gbPtKwords
            // 
            this.m_gbPtKwords.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                       this.m_bnPtKwordHardwire,
                                                                                       this.m_ebPtKwordDef,
                                                                                       this.m_txtPtKwordDef,
                                                                                       this.m_bnPtKwordClear,
                                                                                       this.m_bnPtKwordEdit,
                                                                                       this.m_bnPtKwordAdd,
                                                                                       this.m_lvPtKwords});
            this.m_gbPtKwords.Location = new System.Drawing.Point(200, 48);
            this.m_gbPtKwords.Name = "m_gbPtKwords";
            this.m_gbPtKwords.Size = new System.Drawing.Size(392, 264);
            this.m_gbPtKwords.TabIndex = 18;
            this.m_gbPtKwords.TabStop = false;
            this.m_gbPtKwords.Text = "Key Words";
            // 
            // m_bnPtKwordHardwire
            // 
            this.m_bnPtKwordHardwire.Location = new System.Drawing.Point(24, 200);
            this.m_bnPtKwordHardwire.Name = "m_bnPtKwordHardwire";
            this.m_bnPtKwordHardwire.Size = new System.Drawing.Size(88, 23);
            this.m_bnPtKwordHardwire.TabIndex = 21;
            this.m_bnPtKwordHardwire.Text = "Hardwire";
            this.m_bnPtKwordHardwire.Click += new System.EventHandler(this.OnKwordHardwire);
            // 
            // m_ebPtKwordDef
            // 
            this.m_ebPtKwordDef.Location = new System.Drawing.Point(272, 232);
            this.m_ebPtKwordDef.Name = "m_ebPtKwordDef";
            this.m_ebPtKwordDef.Size = new System.Drawing.Size(96, 20);
            this.m_ebPtKwordDef.TabIndex = 20;
            this.m_ebPtKwordDef.Text = "";
            // 
            // m_txtPtKwordDef
            // 
            this.m_txtPtKwordDef.Location = new System.Drawing.Point(216, 232);
            this.m_txtPtKwordDef.Name = "m_txtPtKwordDef";
            this.m_txtPtKwordDef.Size = new System.Drawing.Size(48, 23);
            this.m_txtPtKwordDef.TabIndex = 19;
            this.m_txtPtKwordDef.Text = "Default";
            // 
            // m_bnPtKwordClear
            // 
            this.m_bnPtKwordClear.Location = new System.Drawing.Point(296, 200);
            this.m_bnPtKwordClear.Name = "m_bnPtKwordClear";
            this.m_bnPtKwordClear.TabIndex = 3;
            this.m_bnPtKwordClear.Text = "Clear";
            this.m_bnPtKwordClear.Click += new System.EventHandler(this.OnKwordClear);
            // 
            // m_bnPtKwordEdit
            // 
            this.m_bnPtKwordEdit.Location = new System.Drawing.Point(208, 200);
            this.m_bnPtKwordEdit.Name = "m_bnPtKwordEdit";
            this.m_bnPtKwordEdit.TabIndex = 2;
            this.m_bnPtKwordEdit.Text = "Edit...";
            this.m_bnPtKwordEdit.Click += new System.EventHandler(this.OnKwordEdit);
            // 
            // m_bnPtKwordAdd
            // 
            this.m_bnPtKwordAdd.Location = new System.Drawing.Point(120, 200);
            this.m_bnPtKwordAdd.Name = "m_bnPtKwordAdd";
            this.m_bnPtKwordAdd.TabIndex = 1;
            this.m_bnPtKwordAdd.Text = "Add...";
            this.m_bnPtKwordAdd.Click += new System.EventHandler(this.OnKwordAdd);
            // 
            // m_lvPtKwords
            // 
            this.m_lvPtKwords.CausesValidation = false;
            this.m_lvPtKwords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                           this.m_lvColPtGlobal,
                                                                                           this.m_lvColPtLocal,
                                                                                           this.m_lvColPtDisplay,
                                                                                           this.m_lvColPtEnabled,
                                                                                           this.m_lvColPtVisible});
            this.m_lvPtKwords.FullRowSelect = true;
            this.m_lvPtKwords.GridLines = true;
            this.m_lvPtKwords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvPtKwords.HideSelection = false;
            this.m_lvPtKwords.Location = new System.Drawing.Point(16, 24);
            this.m_lvPtKwords.MultiSelect = false;
            this.m_lvPtKwords.Name = "m_lvPtKwords";
            this.m_lvPtKwords.Size = new System.Drawing.Size(360, 168);
            this.m_lvPtKwords.TabIndex = 0;
            this.m_lvPtKwords.View = System.Windows.Forms.View.Details;
            // 
            // m_lvColPtGlobal
            // 
            this.m_lvColPtGlobal.Text = "Global";
            this.m_lvColPtGlobal.Width = 80;
            // 
            // m_lvColPtLocal
            // 
            this.m_lvColPtLocal.Text = "Local";
            this.m_lvColPtLocal.Width = 80;
            // 
            // m_lvColPtDisplay
            // 
            this.m_lvColPtDisplay.Text = "Display";
            this.m_lvColPtDisplay.Width = 80;
            // 
            // m_lvColPtEnabled
            // 
            this.m_lvColPtEnabled.Text = "Enabled";
            // 
            // m_lvColPtVisible
            // 
            this.m_lvColPtVisible.Text = "Visible";
            // 
            // m_ebPtMessage
            // 
            this.m_ebPtMessage.Location = new System.Drawing.Point(88, 16);
            this.m_ebPtMessage.Name = "m_ebPtMessage";
            this.m_ebPtMessage.Size = new System.Drawing.Size(504, 20);
            this.m_ebPtMessage.TabIndex = 15;
            this.m_ebPtMessage.Text = "Enter a point";
            // 
            // m_txtPtMessage
            // 
            this.m_txtPtMessage.Location = new System.Drawing.Point(16, 16);
            this.m_txtPtMessage.Name = "m_txtPtMessage";
            this.m_txtPtMessage.Size = new System.Drawing.Size(56, 23);
            this.m_txtPtMessage.TabIndex = 17;
            this.m_txtPtMessage.Text = "Message";
            // 
            // m_tpString
            // 
            this.m_tpString.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.m_gbStrOptions,
                                                                                     this.m_ebStrMessage,
                                                                                     this.m_txtStrMessage});
            this.m_tpString.Location = new System.Drawing.Point(4, 22);
            this.m_tpString.Name = "m_tpString";
            this.m_tpString.Size = new System.Drawing.Size(608, 318);
            this.m_tpString.TabIndex = 5;
            this.m_tpString.Text = "String";
            // 
            // m_gbStrOptions
            // 
            this.m_gbStrOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.m_cbStrAllowSpaces});
            this.m_gbStrOptions.Location = new System.Drawing.Point(16, 48);
            this.m_gbStrOptions.Name = "m_gbStrOptions";
            this.m_gbStrOptions.Size = new System.Drawing.Size(168, 232);
            this.m_gbStrOptions.TabIndex = 20;
            this.m_gbStrOptions.TabStop = false;
            this.m_gbStrOptions.Text = "Options";
            // 
            // m_cbStrAllowSpaces
            // 
            this.m_cbStrAllowSpaces.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbStrAllowSpaces.Location = new System.Drawing.Point(8, 24);
            this.m_cbStrAllowSpaces.Name = "m_cbStrAllowSpaces";
            this.m_cbStrAllowSpaces.Size = new System.Drawing.Size(136, 24);
            this.m_cbStrAllowSpaces.TabIndex = 0;
            this.m_cbStrAllowSpaces.Text = "Allow Spaces";
            // 
            // m_ebStrMessage
            // 
            this.m_ebStrMessage.Location = new System.Drawing.Point(88, 16);
            this.m_ebStrMessage.Name = "m_ebStrMessage";
            this.m_ebStrMessage.Size = new System.Drawing.Size(504, 20);
            this.m_ebStrMessage.TabIndex = 19;
            this.m_ebStrMessage.Text = "Enter a string";
            // 
            // m_txtStrMessage
            // 
            this.m_txtStrMessage.Location = new System.Drawing.Point(16, 16);
            this.m_txtStrMessage.Name = "m_txtStrMessage";
            this.m_txtStrMessage.Size = new System.Drawing.Size(56, 23);
            this.m_txtStrMessage.TabIndex = 21;
            this.m_txtStrMessage.Text = "Message";
            // 
            // m_tpKeyword
            // 
            this.m_tpKeyword.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                      this.m_gbKwdOptions,
                                                                                      this.m_gbKwdKwords,
                                                                                      this.m_ebKwdMessage,
                                                                                      this.m_txtKwdMessage});
            this.m_tpKeyword.Location = new System.Drawing.Point(4, 22);
            this.m_tpKeyword.Name = "m_tpKeyword";
            this.m_tpKeyword.Size = new System.Drawing.Size(608, 318);
            this.m_tpKeyword.TabIndex = 8;
            this.m_tpKeyword.Text = "Keyword";
            // 
            // m_gbKwdOptions
            // 
            this.m_gbKwdOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.m_cbKwdAllowArbitraryInput,
                                                                                         this.m_cbKwdAllowNone});
            this.m_gbKwdOptions.Location = new System.Drawing.Point(16, 48);
            this.m_gbKwdOptions.Name = "m_gbKwdOptions";
            this.m_gbKwdOptions.Size = new System.Drawing.Size(168, 264);
            this.m_gbKwdOptions.TabIndex = 20;
            this.m_gbKwdOptions.TabStop = false;
            this.m_gbKwdOptions.Text = "Options";
            // 
            // m_cbKwdAllowArbitraryInput
            // 
            this.m_cbKwdAllowArbitraryInput.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbKwdAllowArbitraryInput.Location = new System.Drawing.Point(8, 24);
            this.m_cbKwdAllowArbitraryInput.Name = "m_cbKwdAllowArbitraryInput";
            this.m_cbKwdAllowArbitraryInput.Size = new System.Drawing.Size(136, 24);
            this.m_cbKwdAllowArbitraryInput.TabIndex = 2;
            this.m_cbKwdAllowArbitraryInput.Text = "Allow Arbitrary Input";
            // 
            // m_cbKwdAllowNone
            // 
            this.m_cbKwdAllowNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbKwdAllowNone.Location = new System.Drawing.Point(8, 48);
            this.m_cbKwdAllowNone.Name = "m_cbKwdAllowNone";
            this.m_cbKwdAllowNone.Size = new System.Drawing.Size(128, 24);
            this.m_cbKwdAllowNone.TabIndex = 1;
            this.m_cbKwdAllowNone.Text = "Allow None";
            // 
            // m_gbKwdKwords
            // 
            this.m_gbKwdKwords.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.m_bnKwdKwordHardwire,
                                                                                        this.m_ebKwdKwordDef,
                                                                                        this.m_txtKwdKwordDef,
                                                                                        this.m_bnKwdKwordClear,
                                                                                        this.m_bnKwdKwordEdit,
                                                                                        this.m_bnKwdKwordAdd,
                                                                                        this.m_lvKwdKwords});
            this.m_gbKwdKwords.Location = new System.Drawing.Point(200, 48);
            this.m_gbKwdKwords.Name = "m_gbKwdKwords";
            this.m_gbKwdKwords.Size = new System.Drawing.Size(392, 264);
            this.m_gbKwdKwords.TabIndex = 22;
            this.m_gbKwdKwords.TabStop = false;
            this.m_gbKwdKwords.Text = "Key Words";
            // 
            // m_bnKwdKwordHardwire
            // 
            this.m_bnKwdKwordHardwire.Location = new System.Drawing.Point(24, 200);
            this.m_bnKwdKwordHardwire.Name = "m_bnKwdKwordHardwire";
            this.m_bnKwdKwordHardwire.Size = new System.Drawing.Size(88, 23);
            this.m_bnKwdKwordHardwire.TabIndex = 24;
            this.m_bnKwdKwordHardwire.Text = "Hardwire";
            this.m_bnKwdKwordHardwire.Click += new System.EventHandler(this.OnKwordHardwire);
            // 
            // m_ebKwdKwordDef
            // 
            this.m_ebKwdKwordDef.Location = new System.Drawing.Point(272, 232);
            this.m_ebKwdKwordDef.Name = "m_ebKwdKwordDef";
            this.m_ebKwdKwordDef.Size = new System.Drawing.Size(96, 20);
            this.m_ebKwdKwordDef.TabIndex = 23;
            this.m_ebKwdKwordDef.Text = "";
            // 
            // m_txtKwdKwordDef
            // 
            this.m_txtKwdKwordDef.Location = new System.Drawing.Point(216, 232);
            this.m_txtKwdKwordDef.Name = "m_txtKwdKwordDef";
            this.m_txtKwdKwordDef.Size = new System.Drawing.Size(48, 23);
            this.m_txtKwdKwordDef.TabIndex = 22;
            this.m_txtKwdKwordDef.Text = "Default";
            // 
            // m_bnKwdKwordClear
            // 
            this.m_bnKwdKwordClear.Location = new System.Drawing.Point(296, 200);
            this.m_bnKwdKwordClear.Name = "m_bnKwdKwordClear";
            this.m_bnKwdKwordClear.TabIndex = 3;
            this.m_bnKwdKwordClear.Text = "Clear";
            this.m_bnKwdKwordClear.Click += new System.EventHandler(this.OnKwordClear);
            // 
            // m_bnKwdKwordEdit
            // 
            this.m_bnKwdKwordEdit.Location = new System.Drawing.Point(208, 200);
            this.m_bnKwdKwordEdit.Name = "m_bnKwdKwordEdit";
            this.m_bnKwdKwordEdit.TabIndex = 2;
            this.m_bnKwdKwordEdit.Text = "Edit...";
            this.m_bnKwdKwordEdit.Click += new System.EventHandler(this.OnKwordEdit);
            // 
            // m_bnKwdKwordAdd
            // 
            this.m_bnKwdKwordAdd.Location = new System.Drawing.Point(120, 200);
            this.m_bnKwdKwordAdd.Name = "m_bnKwdKwordAdd";
            this.m_bnKwdKwordAdd.TabIndex = 1;
            this.m_bnKwdKwordAdd.Text = "Add...";
            this.m_bnKwdKwordAdd.Click += new System.EventHandler(this.OnKwordAdd);
            // 
            // m_lvKwdKwords
            // 
            this.m_lvKwdKwords.CausesValidation = false;
            this.m_lvKwdKwords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                            this.m_lvColKwdGlobal,
                                                                                            this.m_lvColKwdLocal,
                                                                                            this.m_lvColKwdDisplay,
                                                                                            this.m_lvColKwdEnabled,
                                                                                            this.m_lvColKwdVisible});
            this.m_lvKwdKwords.FullRowSelect = true;
            this.m_lvKwdKwords.GridLines = true;
            this.m_lvKwdKwords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvKwdKwords.HideSelection = false;
            this.m_lvKwdKwords.Location = new System.Drawing.Point(16, 24);
            this.m_lvKwdKwords.MultiSelect = false;
            this.m_lvKwdKwords.Name = "m_lvKwdKwords";
            this.m_lvKwdKwords.Size = new System.Drawing.Size(360, 168);
            this.m_lvKwdKwords.TabIndex = 0;
            this.m_lvKwdKwords.View = System.Windows.Forms.View.Details;
            // 
            // m_lvColKwdGlobal
            // 
            this.m_lvColKwdGlobal.Text = "Global";
            this.m_lvColKwdGlobal.Width = 80;
            // 
            // m_lvColKwdLocal
            // 
            this.m_lvColKwdLocal.Text = "Local";
            this.m_lvColKwdLocal.Width = 80;
            // 
            // m_lvColKwdDisplay
            // 
            this.m_lvColKwdDisplay.Text = "Display";
            this.m_lvColKwdDisplay.Width = 80;
            // 
            // m_lvColKwdEnabled
            // 
            this.m_lvColKwdEnabled.Text = "Enabled";
            // 
            // m_lvColKwdVisible
            // 
            this.m_lvColKwdVisible.Text = "Visible";
            // 
            // m_ebKwdMessage
            // 
            this.m_ebKwdMessage.Location = new System.Drawing.Point(88, 16);
            this.m_ebKwdMessage.Name = "m_ebKwdMessage";
            this.m_ebKwdMessage.Size = new System.Drawing.Size(504, 20);
            this.m_ebKwdMessage.TabIndex = 19;
            this.m_ebKwdMessage.Text = "Enter a keyword";
            // 
            // m_txtKwdMessage
            // 
            this.m_txtKwdMessage.Location = new System.Drawing.Point(16, 16);
            this.m_txtKwdMessage.Name = "m_txtKwdMessage";
            this.m_txtKwdMessage.Size = new System.Drawing.Size(56, 23);
            this.m_txtKwdMessage.TabIndex = 21;
            this.m_txtKwdMessage.Text = "Message";
            // 
            // m_tpEntity
            // 
            this.m_tpEntity.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.m_gbEntFilterClasses,
                                                                                     this.m_gbEntKwords,
                                                                                     this.m_gbEntOptions,
                                                                                     this.m_ebEntMessage,
                                                                                     this.m_txtEntMessage});
            this.m_tpEntity.Location = new System.Drawing.Point(4, 22);
            this.m_tpEntity.Name = "m_tpEntity";
            this.m_tpEntity.Size = new System.Drawing.Size(608, 318);
            this.m_tpEntity.TabIndex = 6;
            this.m_tpEntity.Text = "Entity";
            // 
            // m_gbEntFilterClasses
            // 
            this.m_gbEntFilterClasses.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                               this.m_cbEntAllowCurves,
                                                                                               this.m_txtEntSampleFilter,
                                                                                               this.m_ebEntDoIsATest,
                                                                                               this.m_cbEntAllowCircles,
                                                                                               this.m_cbEntAllowLines,
                                                                                               this.m_ebEntRejectMessage,
                                                                                               this.m_txtEntRejectMessage});
            this.m_gbEntFilterClasses.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_gbEntFilterClasses.Location = new System.Drawing.Point(16, 136);
            this.m_gbEntFilterClasses.Name = "m_gbEntFilterClasses";
            this.m_gbEntFilterClasses.Size = new System.Drawing.Size(168, 176);
            this.m_gbEntFilterClasses.TabIndex = 26;
            this.m_gbEntFilterClasses.TabStop = false;
            this.m_gbEntFilterClasses.Text = "Filter Classes";
            // 
            // m_cbEntAllowCurves
            // 
            this.m_cbEntAllowCurves.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbEntAllowCurves.Location = new System.Drawing.Point(8, 72);
            this.m_cbEntAllowCurves.Name = "m_cbEntAllowCurves";
            this.m_cbEntAllowCurves.Size = new System.Drawing.Size(144, 24);
            this.m_cbEntAllowCurves.TabIndex = 13;
            this.m_cbEntAllowCurves.Text = "Allow Curves";
            this.m_cbEntAllowCurves.Click += new System.EventHandler(this.OnEntAllowLines);
            // 
            // m_txtEntSampleFilter
            // 
            this.m_txtEntSampleFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_txtEntSampleFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.m_txtEntSampleFilter.ForeColor = System.Drawing.Color.MediumBlue;
            this.m_txtEntSampleFilter.Location = new System.Drawing.Point(80, 0);
            this.m_txtEntSampleFilter.Name = "m_txtEntSampleFilter";
            this.m_txtEntSampleFilter.TabIndex = 12;
            this.m_txtEntSampleFilter.Text = "(Sample)";
            // 
            // m_ebEntDoIsATest
            // 
            this.m_ebEntDoIsATest.Enabled = false;
            this.m_ebEntDoIsATest.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_ebEntDoIsATest.Location = new System.Drawing.Point(8, 96);
            this.m_ebEntDoIsATest.Name = "m_ebEntDoIsATest";
            this.m_ebEntDoIsATest.Size = new System.Drawing.Size(128, 24);
            this.m_ebEntDoIsATest.TabIndex = 11;
            this.m_ebEntDoIsATest.Text = "Exact Class Match";
            // 
            // m_cbEntAllowCircles
            // 
            this.m_cbEntAllowCircles.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbEntAllowCircles.Location = new System.Drawing.Point(8, 48);
            this.m_cbEntAllowCircles.Name = "m_cbEntAllowCircles";
            this.m_cbEntAllowCircles.Size = new System.Drawing.Size(144, 24);
            this.m_cbEntAllowCircles.TabIndex = 10;
            this.m_cbEntAllowCircles.Text = "Allow Circles";
            this.m_cbEntAllowCircles.Click += new System.EventHandler(this.OnEntAllowLines);
            // 
            // m_cbEntAllowLines
            // 
            this.m_cbEntAllowLines.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbEntAllowLines.Location = new System.Drawing.Point(8, 24);
            this.m_cbEntAllowLines.Name = "m_cbEntAllowLines";
            this.m_cbEntAllowLines.Size = new System.Drawing.Size(144, 24);
            this.m_cbEntAllowLines.TabIndex = 9;
            this.m_cbEntAllowLines.Text = "Allow Lines";
            this.m_cbEntAllowLines.Click += new System.EventHandler(this.OnEntAllowLines);
            // 
            // m_ebEntRejectMessage
            // 
            this.m_ebEntRejectMessage.Enabled = false;
            this.m_ebEntRejectMessage.Location = new System.Drawing.Point(8, 144);
            this.m_ebEntRejectMessage.Name = "m_ebEntRejectMessage";
            this.m_ebEntRejectMessage.Size = new System.Drawing.Size(152, 20);
            this.m_ebEntRejectMessage.TabIndex = 8;
            this.m_ebEntRejectMessage.Text = "Wrong type of entity selected!";
            // 
            // m_txtEntRejectMessage
            // 
            this.m_txtEntRejectMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_txtEntRejectMessage.Location = new System.Drawing.Point(8, 120);
            this.m_txtEntRejectMessage.Name = "m_txtEntRejectMessage";
            this.m_txtEntRejectMessage.Size = new System.Drawing.Size(96, 23);
            this.m_txtEntRejectMessage.TabIndex = 7;
            this.m_txtEntRejectMessage.Text = "Reject Message";
            // 
            // m_gbEntKwords
            // 
            this.m_gbEntKwords.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.m_bnEntKwordHardwire,
                                                                                        this.m_ebEntKwordDef,
                                                                                        this.m_txtEntKwordDef,
                                                                                        this.m_bnEntKwordClear,
                                                                                        this.m_bnEntKwordEdit,
                                                                                        this.m_bnEntKwordAdd,
                                                                                        this.m_lvEntKwords});
            this.m_gbEntKwords.Location = new System.Drawing.Point(200, 48);
            this.m_gbEntKwords.Name = "m_gbEntKwords";
            this.m_gbEntKwords.Size = new System.Drawing.Size(392, 264);
            this.m_gbEntKwords.TabIndex = 25;
            this.m_gbEntKwords.TabStop = false;
            this.m_gbEntKwords.Text = "Key Words";
            // 
            // m_bnEntKwordHardwire
            // 
            this.m_bnEntKwordHardwire.Location = new System.Drawing.Point(24, 200);
            this.m_bnEntKwordHardwire.Name = "m_bnEntKwordHardwire";
            this.m_bnEntKwordHardwire.Size = new System.Drawing.Size(88, 23);
            this.m_bnEntKwordHardwire.TabIndex = 27;
            this.m_bnEntKwordHardwire.Text = "Hardwire";
            this.m_bnEntKwordHardwire.Click += new System.EventHandler(this.OnKwordHardwire);
            // 
            // m_ebEntKwordDef
            // 
            this.m_ebEntKwordDef.Location = new System.Drawing.Point(272, 232);
            this.m_ebEntKwordDef.Name = "m_ebEntKwordDef";
            this.m_ebEntKwordDef.Size = new System.Drawing.Size(96, 20);
            this.m_ebEntKwordDef.TabIndex = 26;
            this.m_ebEntKwordDef.Text = "";
            // 
            // m_txtEntKwordDef
            // 
            this.m_txtEntKwordDef.Location = new System.Drawing.Point(216, 232);
            this.m_txtEntKwordDef.Name = "m_txtEntKwordDef";
            this.m_txtEntKwordDef.Size = new System.Drawing.Size(48, 23);
            this.m_txtEntKwordDef.TabIndex = 25;
            this.m_txtEntKwordDef.Text = "Default";
            // 
            // m_bnEntKwordClear
            // 
            this.m_bnEntKwordClear.Location = new System.Drawing.Point(296, 200);
            this.m_bnEntKwordClear.Name = "m_bnEntKwordClear";
            this.m_bnEntKwordClear.TabIndex = 3;
            this.m_bnEntKwordClear.Text = "Clear";
            this.m_bnEntKwordClear.Click += new System.EventHandler(this.OnKwordClear);
            // 
            // m_bnEntKwordEdit
            // 
            this.m_bnEntKwordEdit.Location = new System.Drawing.Point(208, 200);
            this.m_bnEntKwordEdit.Name = "m_bnEntKwordEdit";
            this.m_bnEntKwordEdit.TabIndex = 2;
            this.m_bnEntKwordEdit.Text = "Edit...";
            this.m_bnEntKwordEdit.Click += new System.EventHandler(this.OnKwordEdit);
            // 
            // m_bnEntKwordAdd
            // 
            this.m_bnEntKwordAdd.Location = new System.Drawing.Point(120, 200);
            this.m_bnEntKwordAdd.Name = "m_bnEntKwordAdd";
            this.m_bnEntKwordAdd.TabIndex = 1;
            this.m_bnEntKwordAdd.Text = "Add...";
            this.m_bnEntKwordAdd.Click += new System.EventHandler(this.OnKwordAdd);
            // 
            // m_lvEntKwords
            // 
            this.m_lvEntKwords.CausesValidation = false;
            this.m_lvEntKwords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                            this.m_lvColEntKwordGlobal,
                                                                                            this.m_lvColEntKwordLocal,
                                                                                            this.m_lvColEntKwordDisplay,
                                                                                            this.m_lvColEntKwordEnabled,
                                                                                            this.m_lvColEntKwordVisible});
            this.m_lvEntKwords.FullRowSelect = true;
            this.m_lvEntKwords.GridLines = true;
            this.m_lvEntKwords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvEntKwords.HideSelection = false;
            this.m_lvEntKwords.Location = new System.Drawing.Point(16, 24);
            this.m_lvEntKwords.MultiSelect = false;
            this.m_lvEntKwords.Name = "m_lvEntKwords";
            this.m_lvEntKwords.Size = new System.Drawing.Size(360, 168);
            this.m_lvEntKwords.TabIndex = 0;
            this.m_lvEntKwords.View = System.Windows.Forms.View.Details;
            // 
            // m_lvColEntKwordGlobal
            // 
            this.m_lvColEntKwordGlobal.Text = "Global";
            this.m_lvColEntKwordGlobal.Width = 80;
            // 
            // m_lvColEntKwordLocal
            // 
            this.m_lvColEntKwordLocal.Text = "Local";
            this.m_lvColEntKwordLocal.Width = 80;
            // 
            // m_lvColEntKwordDisplay
            // 
            this.m_lvColEntKwordDisplay.Text = "Display";
            this.m_lvColEntKwordDisplay.Width = 80;
            // 
            // m_lvColEntKwordEnabled
            // 
            this.m_lvColEntKwordEnabled.Text = "Enabled";
            // 
            // m_lvColEntKwordVisible
            // 
            this.m_lvColEntKwordVisible.Text = "Visible";
            // 
            // m_gbEntOptions
            // 
            this.m_gbEntOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.m_cbEntAllowLockedLayer,
                                                                                         this.m_cbEntAllowNone});
            this.m_gbEntOptions.Location = new System.Drawing.Point(16, 48);
            this.m_gbEntOptions.Name = "m_gbEntOptions";
            this.m_gbEntOptions.Size = new System.Drawing.Size(168, 80);
            this.m_gbEntOptions.TabIndex = 23;
            this.m_gbEntOptions.TabStop = false;
            this.m_gbEntOptions.Text = "Options";
            // 
            // m_cbEntAllowLockedLayer
            // 
            this.m_cbEntAllowLockedLayer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbEntAllowLockedLayer.Location = new System.Drawing.Point(8, 48);
            this.m_cbEntAllowLockedLayer.Name = "m_cbEntAllowLockedLayer";
            this.m_cbEntAllowLockedLayer.Size = new System.Drawing.Size(144, 24);
            this.m_cbEntAllowLockedLayer.TabIndex = 2;
            this.m_cbEntAllowLockedLayer.Text = "Allow Locked Layer";
            // 
            // m_cbEntAllowNone
            // 
            this.m_cbEntAllowNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbEntAllowNone.Location = new System.Drawing.Point(8, 24);
            this.m_cbEntAllowNone.Name = "m_cbEntAllowNone";
            this.m_cbEntAllowNone.Size = new System.Drawing.Size(128, 24);
            this.m_cbEntAllowNone.TabIndex = 1;
            this.m_cbEntAllowNone.Text = "Allow None";
            // 
            // m_ebEntMessage
            // 
            this.m_ebEntMessage.Location = new System.Drawing.Point(88, 16);
            this.m_ebEntMessage.Name = "m_ebEntMessage";
            this.m_ebEntMessage.Size = new System.Drawing.Size(504, 20);
            this.m_ebEntMessage.TabIndex = 22;
            this.m_ebEntMessage.Text = "Select entity";
            // 
            // m_txtEntMessage
            // 
            this.m_txtEntMessage.Location = new System.Drawing.Point(16, 16);
            this.m_txtEntMessage.Name = "m_txtEntMessage";
            this.m_txtEntMessage.Size = new System.Drawing.Size(56, 23);
            this.m_txtEntMessage.TabIndex = 24;
            this.m_txtEntMessage.Text = "Message";
            // 
            // m_tpNested
            // 
            this.m_tpNested.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.m_gbNentKwords,
                                                                                     this.m_gbNentOptions,
                                                                                     this.m_ebNentMessage,
                                                                                     this.m_txtNentMessage});
            this.m_tpNested.Location = new System.Drawing.Point(4, 22);
            this.m_tpNested.Name = "m_tpNested";
            this.m_tpNested.Size = new System.Drawing.Size(608, 318);
            this.m_tpNested.TabIndex = 9;
            this.m_tpNested.Text = "Nested Entity";
            // 
            // m_gbNentKwords
            // 
            this.m_gbNentKwords.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                         this.m_bnNentKwordHardwire,
                                                                                         this.m_ebNentKwordDef,
                                                                                         this.m_txtNentKwordDef,
                                                                                         this.m_bnNentKwordClear,
                                                                                         this.m_bnNentKwordEdit,
                                                                                         this.m_bnNentKwordAdd,
                                                                                         this.m_lvNentKwords});
            this.m_gbNentKwords.Location = new System.Drawing.Point(200, 48);
            this.m_gbNentKwords.Name = "m_gbNentKwords";
            this.m_gbNentKwords.Size = new System.Drawing.Size(392, 264);
            this.m_gbNentKwords.TabIndex = 29;
            this.m_gbNentKwords.TabStop = false;
            this.m_gbNentKwords.Text = "Key Words";
            // 
            // m_bnNentKwordHardwire
            // 
            this.m_bnNentKwordHardwire.Location = new System.Drawing.Point(24, 200);
            this.m_bnNentKwordHardwire.Name = "m_bnNentKwordHardwire";
            this.m_bnNentKwordHardwire.Size = new System.Drawing.Size(88, 23);
            this.m_bnNentKwordHardwire.TabIndex = 27;
            this.m_bnNentKwordHardwire.Text = "Hardwire";
            this.m_bnNentKwordHardwire.Click += new System.EventHandler(this.OnKwordHardwire);
            // 
            // m_ebNentKwordDef
            // 
            this.m_ebNentKwordDef.Location = new System.Drawing.Point(272, 232);
            this.m_ebNentKwordDef.Name = "m_ebNentKwordDef";
            this.m_ebNentKwordDef.Size = new System.Drawing.Size(96, 20);
            this.m_ebNentKwordDef.TabIndex = 26;
            this.m_ebNentKwordDef.Text = "";
            // 
            // m_txtNentKwordDef
            // 
            this.m_txtNentKwordDef.Location = new System.Drawing.Point(216, 232);
            this.m_txtNentKwordDef.Name = "m_txtNentKwordDef";
            this.m_txtNentKwordDef.Size = new System.Drawing.Size(48, 23);
            this.m_txtNentKwordDef.TabIndex = 25;
            this.m_txtNentKwordDef.Text = "Default";
            // 
            // m_bnNentKwordClear
            // 
            this.m_bnNentKwordClear.Location = new System.Drawing.Point(296, 200);
            this.m_bnNentKwordClear.Name = "m_bnNentKwordClear";
            this.m_bnNentKwordClear.TabIndex = 3;
            this.m_bnNentKwordClear.Text = "Clear";
            this.m_bnNentKwordClear.Click += new System.EventHandler(this.OnKwordClear);
            // 
            // m_bnNentKwordEdit
            // 
            this.m_bnNentKwordEdit.Location = new System.Drawing.Point(208, 200);
            this.m_bnNentKwordEdit.Name = "m_bnNentKwordEdit";
            this.m_bnNentKwordEdit.TabIndex = 2;
            this.m_bnNentKwordEdit.Text = "Edit...";
            this.m_bnNentKwordEdit.Click += new System.EventHandler(this.OnKwordEdit);
            // 
            // m_bnNentKwordAdd
            // 
            this.m_bnNentKwordAdd.Location = new System.Drawing.Point(120, 200);
            this.m_bnNentKwordAdd.Name = "m_bnNentKwordAdd";
            this.m_bnNentKwordAdd.TabIndex = 1;
            this.m_bnNentKwordAdd.Text = "Add...";
            this.m_bnNentKwordAdd.Click += new System.EventHandler(this.OnKwordAdd);
            // 
            // m_lvNentKwords
            // 
            this.m_lvNentKwords.CausesValidation = false;
            this.m_lvNentKwords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                             this.m_lvColNentKwordsGlobal,
                                                                                             this.m_lvColNentKwordsLocal,
                                                                                             this.m_lvColNentKwordsDisplay,
                                                                                             this.m_lvColNentKwordsEnabled,
                                                                                             this.m_lvColNentKwordsVisible});
            this.m_lvNentKwords.FullRowSelect = true;
            this.m_lvNentKwords.GridLines = true;
            this.m_lvNentKwords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvNentKwords.HideSelection = false;
            this.m_lvNentKwords.Location = new System.Drawing.Point(16, 24);
            this.m_lvNentKwords.MultiSelect = false;
            this.m_lvNentKwords.Name = "m_lvNentKwords";
            this.m_lvNentKwords.Size = new System.Drawing.Size(360, 168);
            this.m_lvNentKwords.TabIndex = 0;
            this.m_lvNentKwords.View = System.Windows.Forms.View.Details;
            // 
            // m_lvColNentKwordsGlobal
            // 
            this.m_lvColNentKwordsGlobal.Text = "Global";
            this.m_lvColNentKwordsGlobal.Width = 80;
            // 
            // m_lvColNentKwordsLocal
            // 
            this.m_lvColNentKwordsLocal.Text = "Local";
            this.m_lvColNentKwordsLocal.Width = 80;
            // 
            // m_lvColNentKwordsDisplay
            // 
            this.m_lvColNentKwordsDisplay.Text = "Display";
            this.m_lvColNentKwordsDisplay.Width = 80;
            // 
            // m_lvColNentKwordsEnabled
            // 
            this.m_lvColNentKwordsEnabled.Text = "Enabled";
            // 
            // m_lvColNentKwordsVisible
            // 
            this.m_lvColNentKwordsVisible.Text = "Visible";
            // 
            // m_gbNentOptions
            // 
            this.m_gbNentOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                          this.m_cbNentNonInterPickPt,
                                                                                          this.m_cbNentAllowNone});
            this.m_gbNentOptions.Location = new System.Drawing.Point(16, 48);
            this.m_gbNentOptions.Name = "m_gbNentOptions";
            this.m_gbNentOptions.Size = new System.Drawing.Size(168, 264);
            this.m_gbNentOptions.TabIndex = 27;
            this.m_gbNentOptions.TabStop = false;
            this.m_gbNentOptions.Text = "Options";
            // 
            // m_cbNentNonInterPickPt
            // 
            this.m_cbNentNonInterPickPt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbNentNonInterPickPt.Location = new System.Drawing.Point(8, 48);
            this.m_cbNentNonInterPickPt.Name = "m_cbNentNonInterPickPt";
            this.m_cbNentNonInterPickPt.Size = new System.Drawing.Size(136, 24);
            this.m_cbNentNonInterPickPt.TabIndex = 2;
            this.m_cbNentNonInterPickPt.Text = "Use Non-Interactive Pick Point";
            // 
            // m_cbNentAllowNone
            // 
            this.m_cbNentAllowNone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_cbNentAllowNone.Location = new System.Drawing.Point(8, 24);
            this.m_cbNentAllowNone.Name = "m_cbNentAllowNone";
            this.m_cbNentAllowNone.Size = new System.Drawing.Size(128, 24);
            this.m_cbNentAllowNone.TabIndex = 1;
            this.m_cbNentAllowNone.Text = "Allow None";
            // 
            // m_ebNentMessage
            // 
            this.m_ebNentMessage.Location = new System.Drawing.Point(88, 16);
            this.m_ebNentMessage.Name = "m_ebNentMessage";
            this.m_ebNentMessage.Size = new System.Drawing.Size(504, 20);
            this.m_ebNentMessage.TabIndex = 26;
            this.m_ebNentMessage.Text = "Select nested entity";
            // 
            // m_txtNentMessage
            // 
            this.m_txtNentMessage.Location = new System.Drawing.Point(16, 16);
            this.m_txtNentMessage.Name = "m_txtNentMessage";
            this.m_txtNentMessage.Size = new System.Drawing.Size(56, 23);
            this.m_txtNentMessage.TabIndex = 28;
            this.m_txtNentMessage.Text = "Message";
            // 
            // m_bnRunTest
            // 
            this.m_bnRunTest.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_bnRunTest.Location = new System.Drawing.Point(464, 368);
            this.m_bnRunTest.Name = "m_bnRunTest";
            this.m_bnRunTest.TabIndex = 1;
            this.m_bnRunTest.Text = "< Run Test";
            // 
            // m_bnCancel
            // 
            this.m_bnCancel.CausesValidation = false;
            this.m_bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bnCancel.Location = new System.Drawing.Point(552, 368);
            this.m_bnCancel.Name = "m_bnCancel";
            this.m_bnCancel.TabIndex = 2;
            this.m_bnCancel.Text = "Cancel";
            // 
            // PromptTestForm
            // 
            this.AcceptButton = this.m_bnRunTest;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_bnCancel;
            this.ClientSize = new System.Drawing.Size(648, 398);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.m_bnCancel,
                                                                          this.m_bnRunTest,
                                                                          this.m_tcMain});
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromptTestForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Prompts";
            this.m_tcMain.ResumeLayout(false);
            this.m_tpInteger.ResumeLayout(false);
            this.m_gbKeywords.ResumeLayout(false);
            this.m_gbOptionsInt.ResumeLayout(false);
            this.m_tpDouble.ResumeLayout(false);
            this.m_gbDblKwords.ResumeLayout(false);
            this.m_gbDblOpts.ResumeLayout(false);
            this.m_tpDistance.ResumeLayout(false);
            this.m_gbDistKwords.ResumeLayout(false);
            this.m_gbDistOptions.ResumeLayout(false);
            this.m_tpAngle.ResumeLayout(false);
            this.m_gbAngOptions.ResumeLayout(false);
            this.m_gbAngKwords.ResumeLayout(false);
            this.m_tpCorner.ResumeLayout(false);
            this.m_gbCrnOptions.ResumeLayout(false);
            this.m_gbCrnKwords.ResumeLayout(false);
            this.m_tpPoint.ResumeLayout(false);
            this.m_gbPtOptions.ResumeLayout(false);
            this.m_gbPtKwords.ResumeLayout(false);
            this.m_tpString.ResumeLayout(false);
            this.m_gbStrOptions.ResumeLayout(false);
            this.m_tpKeyword.ResumeLayout(false);
            this.m_gbKwdOptions.ResumeLayout(false);
            this.m_gbKwdKwords.ResumeLayout(false);
            this.m_tpEntity.ResumeLayout(false);
            this.m_gbEntFilterClasses.ResumeLayout(false);
            this.m_gbEntKwords.ResumeLayout(false);
            this.m_gbEntOptions.ResumeLayout(false);
            this.m_tpNested.ResumeLayout(false);
            this.m_gbNentKwords.ResumeLayout(false);
            this.m_gbNentOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        /// <summary>
        /// Disable the default value if they aren't going to use it
        /// </summary>
        
        private void
        OnIntUseDefaultValue(object sender, System.EventArgs e)
        {
            m_ebIntDefault.Enabled = m_cbIntUseDefault.Checked;
        }
        
        private void
        OnDblUseDefaultValue(object sender, System.EventArgs e)
        {
            m_ebDblDefault.Enabled = m_cbDblUseDefault.Checked;        
        }
        
        private void
        OnDistUseDefaultValue(object sender, System.EventArgs e)
        {
            m_ebDistDefault.Enabled = m_cbDistUseDefault.Checked;                
        }
        
        private void
        OnAngUseDefaultValue(object sender, System.EventArgs e)
        {
            m_ebAngDefault.Enabled = m_cbAngUseDefault.Checked;                
        }
        
        /// <summary>
        /// After the form has been dismissed, figure out which page
        /// was active and run that specific prompt.
        /// </summary>
        
        public void
        RunPrompt()
        {
            if (m_tcMain.SelectedTab == m_tpInteger) {
                RunIntegerPrompt();
            }
            else if (m_tcMain.SelectedTab == m_tpDouble) {
                RunDoublePrompt();
            }
            else if (m_tcMain.SelectedTab == m_tpDistance) {
                RunDistancePrompt();
            }
            else if (m_tcMain.SelectedTab == m_tpAngle) {
                RunAnglePrompt();
            }
            else if (m_tcMain.SelectedTab == m_tpCorner) {
                RunCornerPrompt();
            }
            else if (m_tcMain.SelectedTab == m_tpPoint) {
                RunPointPrompt();
            }
            else if (m_tcMain.SelectedTab == m_tpString) {
                RunStringPrompt();
            }
            else if (m_tcMain.SelectedTab == m_tpKeyword) {
                RunKeywordPrompt();
            }
            else if (m_tcMain.SelectedTab == m_tpEntity) {
                RunEntityPrompt();
            }
            else if (m_tcMain.SelectedTab == m_tpNested) {
                RunNestedEntityPrompt();
            }
            else {
                Debug.Assert(false, "Not a tab!");
            }
        }
        
        private void
        RunIntegerPrompt()
        {
            PromptIntegerOptions prOpts = new PromptIntegerOptions(string.Format("\n{0}", m_ebIntMessage.Text));
            prOpts.AllowArbitraryInput = m_cbIntAllowArbitraryInput.Checked;
            prOpts.AllowNone = m_cbIntAllowNone.Checked;
            prOpts.AllowZero = m_cbIntAllowZero.Checked;
            prOpts.AllowNegative = m_cbIntAllowNegative.Checked;
            prOpts.UseDefaultValue = m_cbIntUseDefault.Checked;
            if (m_cbIntUseDefault.Checked)
                prOpts.DefaultValue = int.Parse(m_ebIntDefault.Text);   // TBD: need a validating EditBoxInteger
            
            AddKeywordsToPrompt(prOpts, m_kwordsInt, m_ebIntKwordDef.Text);
            
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            PromptIntegerResult prRes = ed.GetInteger(prOpts);
            
            ShowPromptResult(prRes);
        }
        
        private void
        RunDoublePrompt()
        {
            PromptDoubleOptions prOpts = new PromptDoubleOptions(string.Format("\n{0}", m_ebDblMessage.Text));
            prOpts.AllowArbitraryInput = m_cbDblAllowArbitraryInput.Checked;
            prOpts.AllowNone = m_cbDblAllowNone.Checked;
            prOpts.AllowZero = m_cbDblAllowZero.Checked;
            prOpts.AllowNegative = m_cbDblAllowNegative.Checked;
            prOpts.UseDefaultValue = m_cbDblUseDefault.Checked;
            if (m_cbDblUseDefault.Checked)
                prOpts.DefaultValue = double.Parse(m_ebDblDefault.Text);   // TBD: need a validating EditBoxDbleger
            
            AddKeywordsToPrompt(prOpts, m_kwordsDbl, m_ebDblKwordDef.Text);
            
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            PromptDoubleResult prRes = ed.GetDouble(prOpts);
            
            ShowPromptResult(prRes);
        }
        
        private void
        RunDistancePrompt()
        {
            PromptDistanceOptions prOpts = new PromptDistanceOptions(string.Format("\n{0}", m_ebDistMessage.Text));
            prOpts.AllowArbitraryInput = m_cbDistAllowArbitraryInput.Checked;
            prOpts.AllowNone = m_cbDistAllowNone.Checked;
            prOpts.AllowZero = m_cbDistAllowZero.Checked;
            prOpts.AllowNegative = m_cbDistAllowNegative.Checked;
            prOpts.UseBasePoint = m_cbDistUseBasePt.Checked;
            prOpts.UseDashedLine = m_cbDistUseDashedLine.Checked;
            prOpts.Only2d = m_cbDistOnly2d.Checked;
            prOpts.UseDefaultValue = m_cbDistUseDefault.Checked;
            if (m_cbDistUseDefault.Checked)
                prOpts.DefaultValue = double.Parse(m_ebDistDefault.Text);   // TBD: need a validating EditBoxDbleger
            
            AddKeywordsToPrompt(prOpts, m_kwordsDist, m_ebDistKwordDef.Text);
            
            if (m_cbDistUseBasePt.Checked)
                prOpts.BasePoint = GetBasePt();
            
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            PromptDoubleResult prRes = ed.GetDistance(prOpts);
            
            ShowPromptResult(prRes);
        }
        
        private void
        RunAnglePrompt()
        {
            PromptAngleOptions prOpts = new PromptAngleOptions(string.Format("\n{0}", m_ebAngMessage.Text));
            prOpts.AllowArbitraryInput = m_cbAngAllowArbitraryInput.Checked;
            prOpts.AllowNone = m_cbAngAllowNone.Checked;
            prOpts.AllowZero = m_cbAngAllowZero.Checked;
            prOpts.UseBasePoint = m_cbAngUseBasePt.Checked;
            prOpts.UseDashedLine = m_cbAngUseDashedLine.Checked;
            prOpts.UseDefaultValue = m_cbAngUseDefault.Checked;
            if (m_cbAngUseDefault.Checked)
                prOpts.DefaultValue = MgdDbg.Utils.Ge.DegreesToRadians(double.Parse(m_ebAngDefault.Text));   // TBD: need a validating EditBoxDbleger
            
            AddKeywordsToPrompt(prOpts, m_kwordsAng, m_ebAngKwordDef.Text);
            
            if (m_cbAngUseBasePt.Checked)
                prOpts.BasePoint = GetBasePt();
            
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            PromptDoubleResult prRes = ed.GetAngle(prOpts);
            
            ShowPromptResult(prRes);
        }
        
        private void
        RunCornerPrompt()
        {
            Point3d basePt = GetBasePt(); // nees a base point to corner from
            
            PromptCornerOptions prOpts = new PromptCornerOptions(string.Format("\n{0}", m_ebCrnMessage.Text), basePt);
            prOpts.AllowArbitraryInput = m_cbCrnAllowArbitraryInput.Checked;
            prOpts.AllowNone = m_cbCrnAllowNone.Checked;
            prOpts.LimitsChecked = m_cbCrnLimitsChecked.Checked;
            prOpts.UseDashedLine = m_cbCrnUseDashedLine.Checked;
            
            AddKeywordsToPrompt(prOpts, m_kwordsCrn, m_ebCrnKwordDef.Text);
            
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            PromptPointResult prRes = ed.GetCorner(prOpts);
            
            ShowPromptResult(prRes);
        }
        
        private void
        RunPointPrompt()
        { 
            PromptPointOptions prOpts = new PromptPointOptions(string.Format("\n{0}", m_ebPtMessage.Text));
            prOpts.AllowArbitraryInput = m_cbPtAllowArbitraryInput.Checked;
            prOpts.AllowNone = m_cbPtAllowNone.Checked;
            prOpts.LimitsChecked = m_cbPtLimitsChecked.Checked;
            prOpts.UseDashedLine = m_cbPtUseDashedLine.Checked;
            prOpts.UseBasePoint = m_cbPtUseBasePt.Checked;
            if (m_cbPtUseBasePt.Checked)
                prOpts.BasePoint = GetBasePt();
            
            AddKeywordsToPrompt(prOpts, m_kwordsPt, m_ebPtKwordDef.Text);
            
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            PromptPointResult prRes = ed.GetPoint(prOpts);
            
            ShowPromptResult(prRes);
        }
        
        private void
        RunStringPrompt()
        { 
            PromptStringOptions prOpts = new PromptStringOptions(string.Format("\n{0}", m_ebStrMessage.Text));
            prOpts.AllowSpaces = m_cbStrAllowSpaces.Checked;
            
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            PromptResult prRes = ed.GetString(prOpts);
            
            ShowPromptResult(prRes);
        }
        
        private void
        RunKeywordPrompt()
        { 
            PromptKeywordOptions prOpts = new PromptKeywordOptions(string.Format("\n{0}", m_ebKwdMessage.Text));
            prOpts.AllowArbitraryInput = m_cbKwdAllowArbitraryInput.Checked;
            prOpts.AllowNone = m_cbKwdAllowNone.Checked;
            
            AddKeywordsToPrompt(prOpts, m_kwordsKwd, m_ebKwdKwordDef.Text);
                        
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            PromptResult prRes = ed.GetKeywords(prOpts);
            
            ShowPromptResult(prRes);
        }
        
        private void
        RunEntityPrompt()
        { 
            PromptEntityOptions prOpts = new PromptEntityOptions(string.Format("\n{0}", m_ebEntMessage.Text));
            prOpts.AllowNone = m_cbEntAllowNone.Checked;
            
                // You can add whatever classes you want to filter for, but you must
                // first add a RejectMessage
            if (m_cbEntAllowLines.Checked || m_cbEntAllowCircles.Checked || m_cbEntAllowCurves.Checked)
                prOpts.SetRejectMessage(string.Format("\n{0}", m_ebEntRejectMessage.Text));
                
            if (m_cbEntAllowLines.Checked)
                prOpts.AddAllowedClass(typeof(Autodesk.AutoCAD.DatabaseServices.Line), m_ebEntDoIsATest.Checked);
                
            if (m_cbEntAllowCircles.Checked)
                prOpts.AddAllowedClass(typeof(Autodesk.AutoCAD.DatabaseServices.Circle), m_ebEntDoIsATest.Checked);
                
            if (m_cbEntAllowCurves.Checked) {
                if (m_ebEntDoIsATest.Checked == true)
                    MessageBox.Show("There are no entities with an exact match of type Curve.\nYou should uncheck Exact Match to select any type of curve.");
                prOpts.AddAllowedClass(typeof(Autodesk.AutoCAD.DatabaseServices.Curve), m_ebEntDoIsATest.Checked);
            }
            
            if (m_cbEntAllowLockedLayer.Checked)
                prOpts.AllowObjectOnLockedLayer = true;
            
            AddKeywordsToPrompt(prOpts, m_kwordsEnt, m_ebEntKwordDef.Text);
                        
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            PromptEntityResult prRes = ed.GetEntity(prOpts);
            
            ShowPromptResult(prRes);
        }
        
        private void
        RunNestedEntityPrompt()
        { 
            PromptNestedEntityOptions prOpts = new PromptNestedEntityOptions(string.Format("\n{0}", m_ebNentMessage.Text));
            prOpts.AllowNone = m_cbNentAllowNone.Checked;
            prOpts.UseNonInteractivePickPoint = m_cbNentNonInterPickPt.Checked;
            if (m_cbNentNonInterPickPt.Checked)
                prOpts.NonInteractivePickPoint = GetPickPt();
            
            AddKeywordsToPrompt(prOpts, m_kwordsNent, m_ebNentKwordDef.Text);
                        
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            
            PromptEntityResult prRes = ed.GetNestedEntity(prOpts);
            
            ShowPromptResult(prRes);
        }
        
        /// <summary>
        /// Some prompts have an optional base point to use.  This just retrives that dynamically
        /// before the actual prompt is issued (otherwise, we'd be typing in a point value in the
        /// dialog, which would be no fun).
        /// </summary>
        /// <returns>the desired base point</returns>
        
        private Point3d
        GetBasePt()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptPointResult prRes = ed.GetPoint("\nBase point to use for prompt");
            if (prRes.Status == PromptStatus.OK)
                return prRes.Value;
            else
                return new Point3d(0.0, 0.0, 0.0);  // TBD: should probably cancel prompt here
        }
        
        private Point3d
        GetPickPt()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptPointResult prRes = ed.GetPoint("\nPick point to use for prompt");
            if (prRes.Status == PromptStatus.OK)
                return prRes.Value;
            else
                return new Point3d(0.0, 0.0, 0.0);  // TBD: should probably cancel prompt here
        }
        
        /// <summary>
        /// Show all the values available from the PromptResult object
        /// </summary>
        /// <param name="prRes">the result of the prompt</param>
        
        private void
        ShowPromptResult(PromptResult prRes)
        {
            Snoop.Forms.Objects resForm = new Snoop.Forms.Objects(prRes);
            resForm.ShowDialog();
        }
        
        /// <summary>
        /// If any keywords were specified for the prompt, set those up here.
        /// </summary>
        /// <param name="prOpts">The options that own the prompt keywords</param>
        /// <param name="kwords">The temporary collection of keywords the dialog collected</param>
        /// <param name="defKword">The default keyword (if any) to use.  Pass string.Empty for no default</param>
        
        private void
        AddKeywordsToPrompt(PromptOptions prOpts, KeywordCollection kwords, string defKword)
        {
            foreach (Keyword kword in kwords)
                prOpts.Keywords.Add(kword.GlobalName, kword.LocalName, kword.DisplayName, kword.Visible, kword.Enabled);
                
            if (defKword != string.Empty)
                prOpts.Keywords.Default = defKword;
        }

        /// <summary>
        /// Add a new keyord to the collection.  We add one with a default value and then
        /// immediately go into Edit mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void
        OnKwordAdd(object sender, System.EventArgs e)
        {
                // we stashed away pointers to the relevant objects so that
                // we wouldn't have to know which specific page was active.
                // Retrieve the appropriate objects for the current page and
                // do the common work.
            Button bnClear = (Button)sender;
            ListView lvKwords = (ListView)bnClear.Tag;
            KeywordCollection kwords = (KeywordCollection)lvKwords.Tag;
            
                // add a new default kword to the collection and then
                // immediately edit it.
            kwords.Add("Global", "Local", "Display", true, true);
            Keyword kword = kwords[kwords.Count - 1];
            
            KeywordsForm kwordForm = new KeywordsForm(kword);
            if (kwordForm.ShowDialog() == DialogResult.OK) {
                ListViewItem lvItem = new ListViewItem(kword.GlobalName);
                lvItem.SubItems.Add(kword.LocalName);
                lvItem.SubItems.Add(kword.DisplayName);
                lvItem.SubItems.Add(kword.Enabled.ToString());
                lvItem.SubItems.Add(kword.Visible.ToString());
                lvItem.Selected = true;
                                
                lvKwords.Items.Add(lvItem);
            }
        }

        /// <summary>
        /// Edit the selected keyword values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void
        OnKwordEdit(object sender, System.EventArgs e)
        {
                // we stashed away pointers to the relevant objects so that
                // we wouldn't have to know which specific page was active.
                // Retrieve the appropriate objects for the current page and
                // do the common work.
            Button bnClear = (Button)sender;
            ListView lvKwords = (ListView)bnClear.Tag;
            KeywordCollection kwords = (KeywordCollection)lvKwords.Tag;

            if (lvKwords.SelectedItems.Count > 0) {
                ListViewItem lvItem = lvKwords.SelectedItems[0];
                Keyword kword = kwords[lvItem.Index];
                
                KeywordsForm kwordForm = new KeywordsForm(kword);
                    
                if (kwordForm.ShowDialog() == DialogResult.OK) {
                    lvItem.Text = kword.GlobalName;
                    lvItem.SubItems[1].Text = kword.LocalName;
                    lvItem.SubItems[2].Text = kword.DisplayName;
                    lvItem.SubItems[3].Text = kword.Enabled.ToString();
                    lvItem.SubItems[4].Text = kword.Visible.ToString();
                }
            }
        }

        /// <summary>
        /// Remove all the keywords from the collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void
        OnKwordClear(object sender, System.EventArgs e)
        {
                // we stashed away pointers to the relevant objects so that
                // we wouldn't have to know which specific page was active.
                // Retrieve the appropriate objects for the current page and
                // do the common work.
            Button bnClear = (Button)sender;
            ListView lvKwords = (ListView)bnClear.Tag;
            KeywordCollection kwords = (KeywordCollection)lvKwords.Tag;
            
            kwords.Clear();
            lvKwords.Items.Clear();
        }

        /// <summary>
        /// Hardwire a set of test values so we don't have to go through
        /// a bunch of steps when we don't really care what the atual keywords are.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void
        OnKwordHardwire(object sender, System.EventArgs e)
        {
            Button bnHardwire = (Button)sender;
            ListView lvKwords = (ListView)bnHardwire.Tag;
            KeywordCollection kwords = (KeywordCollection)lvKwords.Tag;
            
                // clear whatever is currently there
            kwords.Clear();
            lvKwords.Items.Clear();
            
                // hardwire some keywords so we don't have to do it for
                // *EVERY* test case.
            kwords.Add("Red",   "Rojo",  "Rojo",  true, true);
            kwords.Add("Green", "Verde", "Verde", true, true);
            kwords.Add("Blue",  "Azul",  "Azul",  true, true);

            foreach (Keyword kword in kwords) {
                ListViewItem lvItem = new ListViewItem(kword.GlobalName);
                lvItem.SubItems.Add(kword.LocalName);
                lvItem.SubItems.Add(kword.DisplayName);
                lvItem.SubItems.Add(kword.Enabled.ToString());
                lvItem.SubItems.Add(kword.Visible.ToString());
                
                lvKwords.Items.Add(lvItem);
            }
        }

        private void
        OnEntAllowLines(object sender, System.EventArgs e)
        {
            if (m_cbEntAllowLines.Checked || m_cbEntAllowCircles.Checked || m_cbEntAllowCurves.Checked) {
                m_ebEntDoIsATest.Enabled = true;
                m_ebEntRejectMessage.Enabled = true;
            }
            else {
                m_ebEntDoIsATest.Enabled = false;
                m_ebEntRejectMessage.Enabled = false;
            }
        }
        

	}
}
