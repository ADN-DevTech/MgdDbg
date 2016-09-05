
//
// (C) Copyright 2006 by Autodesk, Inc. 
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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using AcadApp = Autodesk.AutoCAD.ApplicationServices;

namespace MgdDbg.ObjTests.Forms {

    public partial class SelectionSet : Form {

        enum SelectionMethod {
            kUser = 0,
            kAll,
            kImplied,
            kCrossing,
            kCrossingPolygon,
            kFence,
            kLast,
            kPrevious,
            kWindow,
            kWindowPolygon,
        };

        private Editor m_editor;
        private ArrayList m_entityClasses = new ArrayList();

        public
        SelectionSet()
        {
            m_editor = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;

            InitializeComponent();

            m_puMainSelectionMethod.SelectedIndex = 0;  // Select user mode by default

            GetClassesDerivedFrom((RXClass)SystemObjects.ClassDictionary["AcDbEntity"], m_entityClasses, true);
            foreach (RXClass tmpClass in m_entityClasses) {
                m_puClassType.Items.Add(tmpClass.Name);
            }

            m_puClassType.SelectedItem = "AcDbEntity";  // have this be the default class
        }

        private void
        OnRunTest(object sender, EventArgs e)
        {
                /// start user interaction object so that this form
                /// does not interfere with user selection in editor
            EditorUserInteraction userInteraction = m_editor.StartUserInteraction(this);

            SelectionMethod selMethod = (SelectionMethod)m_puMainSelectionMethod.SelectedIndex;

            if (selMethod == SelectionMethod.kUser)
                SelectionUser();
            else if (selMethod == SelectionMethod.kAll)
                SelectionAll();
            else if (selMethod == SelectionMethod.kImplied)
                SelectionImplied();
            else if (selMethod == SelectionMethod.kCrossing)
                SelectionCrossing();
            else if (selMethod == SelectionMethod.kCrossingPolygon)
                SelectionCrossingPolygon();
            else if (selMethod == SelectionMethod.kFence)
                SelectionFence();
            else if (selMethod == SelectionMethod.kLast)
                SelectionLast();
            else if (selMethod == SelectionMethod.kPrevious)
                SelectionPrevious();
            else if (selMethod == SelectionMethod.kWindow)
                SelectionWindow();
            else if (selMethod == SelectionMethod.kWindowPolygon)
                SelectionWindowPolygon();
            else {
                Debug.Assert(false);
            }
            
            userInteraction.End();
        }

        #region Selection Methods

        private void
        SelectionUser()
        {
            PromptSelectionOptions selOpts = new PromptSelectionOptions();

            selOpts.AllowSubSelections = m_cbAllowSubSelections.Checked;
            selOpts.ForceSubSelections = m_cbForceSubSelections.Checked;
            if (m_cbAllowDuplicates.Checked) {  // only if not in combination with the others
                if ((selOpts.AllowSubSelections == false) && (selOpts.ForceSubSelections == false))
                    selOpts.AllowDuplicates = true;
            }
            selOpts.RejectObjectsFromNonCurrentSpace = m_cbRejectNonCurrentSpace.Checked;
            selOpts.RejectObjectsOnLockedLayers = m_cbFilterLockedLayers.Checked;
            selOpts.RejectPaperspaceViewport = m_cbRejectPaperSpaceViewport.Checked;
            selOpts.SelectEverythingInAperture = m_cbSelectEverythingInAperature.Checked;
            selOpts.SingleOnly = m_cbSingleOnly.Checked;
            selOpts.SinglePickInSpace = m_cbSinglePickInSpace.Checked;

            if (m_cbMsgAdd.Checked)
                selOpts.MessageForAdding = m_ebMsgAdd.Text;

            if (m_cbMsgRemove.Checked)
                selOpts.MessageForRemoval = m_ebMsgRemove.Text;

            if (m_cbKeywordEvent.Checked) {
                selOpts.Keywords.Add("Red");
                selOpts.Keywords.Add("Green");
                selOpts.Keywords.Add("Blue");

                selOpts.KeywordInput += new SelectionTextInputEventHandler(event_KeywordInput);
            }

            if (m_cbUnknownEvent.Checked) {
                selOpts.UnknownInput += new SelectionTextInputEventHandler(event_UnknownInput);
            }

            selOpts.PrepareOptionalDetails = true;      // always turn this on so we get all info available for the test

            PromptSelectionResult result;
            if (m_cbApplyFilter.Checked)
                result = m_editor.GetSelection(selOpts, GetSelectionFilter());
            else
                result = m_editor.GetSelection(selOpts);

            ShowPromptResult("Select: User", result);
        }

        /// <summary>
        /// Callback function for our keywords
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        void
        event_KeywordInput(object sender, SelectionTextInputEventArgs e)
        {
            MessageBox.Show(string.Format("Keyword event: {0}", e.Input), "KeywordInput Event");
        }

        /// <summary>
        /// Callback function for when we get input that isn't recognized as anything else
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        void
        event_UnknownInput(object sender, SelectionTextInputEventArgs e)
        {
            MessageBox.Show(string.Format("Unknown event: {0}", e.Input), "UnknownInput Event");
        }

        private void
        SelectionImplied()
        {
            PromptSelectionResult result = m_editor.SelectImplied();

            ShowPromptResult("Select: Implied", result);
        }

        private void
        SelectionCrossing()
        {
            Point3d pt1, pt2;
            if (Utils.AcadUi.GetCorners(out pt1, out pt2)) {
                PromptSelectionResult result;
                if (m_cbApplyFilter.Checked)
                    result = m_editor.SelectCrossingWindow(pt1, pt2, GetSelectionFilter());
                else
                    result = m_editor.SelectCrossingWindow(pt1, pt2);

                ShowPromptResult("Select: Crossing Window", result);
            }
        }

        private void
        SelectionCrossingPolygon()
        {
            Point3dCollection pts = new Point3dCollection();
            if (Utils.AcadUi.GetPline3dCollection(pts)) {
                PromptSelectionResult result;
                if (m_cbApplyFilter.Checked)
                    result = m_editor.SelectCrossingPolygon(pts, GetSelectionFilter());
                else
                    result = m_editor.SelectCrossingPolygon(pts);

                ShowPromptResult("Select: Crossing Polygon", result);
            }
        }

        private void
        SelectionFence()
        {
            Point3dCollection pts = new Point3dCollection();
            if (Utils.AcadUi.GetPline3dCollection(pts)) {
                PromptSelectionResult result;
                if (m_cbApplyFilter.Checked)
                    result = m_editor.SelectFence(pts, GetSelectionFilter());
                else
                    result = m_editor.SelectFence(pts);

                ShowPromptResult("Select: Fence", result);
            }
        }

        private void
        SelectionLast()
        {
            PromptSelectionResult result = m_editor.SelectLast();

            ShowPromptResult("Select: Last", result);
        }

        private void
        SelectionPrevious()
        {
            PromptSelectionResult result = m_editor.SelectPrevious();

            ShowPromptResult("Select: Previous", result);
        }

        private void
        SelectionWindow()
        {
            Point3d pt1, pt2;
            if (Utils.AcadUi.GetCorners(out pt1, out pt2)) {
                PromptSelectionResult result;
                if (m_cbApplyFilter.Checked)
                    result = m_editor.SelectWindow(pt1, pt2, GetSelectionFilter());
                else
                    result = m_editor.SelectWindow(pt1, pt2);

                ShowPromptResult("Select: Window", result);
            }
        }

        private void
        SelectionWindowPolygon()
        {
            Point3dCollection pts = new Point3dCollection();
            if (Utils.AcadUi.GetPline3dCollection(pts)) {
                PromptSelectionResult result;
                if (m_cbApplyFilter.Checked)
                    result = m_editor.SelectWindowPolygon(pts, GetSelectionFilter());
                else
                    result = m_editor.SelectWindowPolygon(pts);

                ShowPromptResult("Select: Window Polygon", result);
            }
        }

        private void
        SelectionAll()
        {
            PromptSelectionResult result;
            if (m_cbApplyFilter.Checked)
                result = m_editor.SelectAll(GetSelectionFilter());
            else
                result = m_editor.SelectAll();

            ShowPromptResult("Select: All", result);
        }

        #endregion

        private void
        ShowPromptResult(string dboxTitle, PromptSelectionResult result)
        {
            if (result.Status == PromptStatus.OK) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(result.Value);
                dbox.Text = dboxTitle;
                AcadApp.Application.ShowModalDialog(dbox);
            }
            else {
                MessageBox.Show(string.Format("Prompt status: {0}", result.Status.ToString()), "PromptSelectionResult");
            }
        }

        private void
        OnSelectionMethodChanged(object sender, EventArgs e)
        {
            SelectionMethod selMethod = (SelectionMethod)m_puMainSelectionMethod.SelectedIndex;

            if (selMethod == SelectionMethod.kUser) {
                SetFlagModes(true);
                SetKeywordModes(true);
                SetPromptModes(true);
                SetFilterModes(true);
            }
            else if ((selMethod == SelectionMethod.kLast) ||
                     (selMethod == SelectionMethod.kImplied) ||
                     (selMethod == SelectionMethod.kPrevious)) {
                SetFlagModes(false);
                SetKeywordModes(false);
                SetPromptModes(false);
                SetFilterModes(false);
            }
            else {
                SetFlagModes(false);
                SetKeywordModes(false);
                SetPromptModes(false);
                SetFilterModes(true);
            }
        }

        private void
        SetFlagModes(bool allowed) 
        {
            m_grpFlags.Enabled = allowed;

            if (allowed) {
                if ((m_cbAllowSubSelections.Checked) || (m_cbForceSubSelections.Checked)) {
                    m_cbAllowDuplicates.Enabled = false;    // can't combine Duplicates and sub-entity
                }
            }
        }

        private void
        SetKeywordModes(bool allowed) 
        {
            m_grpEvents.Enabled = allowed;
        }

        private void
        SetPromptModes(bool allowed) 
        {
            if (allowed == false) {
                m_grpMessages.Enabled = false;
            }
            else {
                m_grpMessages.Enabled = true;
                m_cbMsgAdd.Enabled = true;
                m_cbMsgRemove.Enabled = true;

                m_ebMsgAdd.Enabled = m_cbMsgAdd.Checked;
                m_ebMsgRemove.Enabled = m_cbMsgRemove.Checked;
            }
        }

        private void
        SetFilterModes(bool allowed) 
        {
            if (allowed == false) {
                m_grpFilter.Enabled = false;
            }
            else {
                m_grpFilter.Enabled = true;
                m_cbApplyFilter.Enabled = true;

                m_puClassType.Enabled = m_cbApplyFilter.Checked;
                m_txtClassType.Enabled = m_cbApplyFilter.Checked;
                m_cbDoIsKindOfTest.Enabled = m_cbApplyFilter.Checked;
            }
        }

        private void
        OnApplyFilterChecked(object sender, EventArgs e)
        {
            m_puClassType.Enabled = m_cbApplyFilter.Checked;
            m_txtClassType.Enabled = m_cbApplyFilter.Checked;
            m_cbDoIsKindOfTest.Enabled = m_cbApplyFilter.Checked;
        }

        private void
        OnMessageAddChecked(object sender, EventArgs e)
        {
            m_ebMsgAdd.Enabled = m_cbMsgAdd.Checked;
        }

        private void
        OnMessageRemoveChecked(object sender, EventArgs e)
        {
            m_ebMsgRemove.Enabled = m_cbMsgRemove.Checked;
        }

        private void
        OnAllowSubSelectionChecked(object sender, EventArgs e)
        {
            if ((m_cbAllowSubSelections.Checked) || (m_cbForceSubSelections.Checked))
                m_cbAllowDuplicates.Enabled = false;    // can't combine Duplicates and sub-entity
            else
                m_cbAllowDuplicates.Enabled = true;
        }

        private void
        GetClassesDerivedFrom(RXClass searchClassType, ArrayList derivedClasses, bool allowClassesWithoutDxfName)
        {
            Debug.Assert(derivedClasses != null);

            Dictionary dict = SystemObjects.ClassDictionary;
            if (dict != null) {
                IEnumerable enumerable = dict as IEnumerable;
                if (enumerable != null) {
                    IEnumerator enumerator = enumerable.GetEnumerator();
                    while (enumerator.MoveNext()) {
                        RXClass rxClass = (RXClass)((DictionaryEntry)enumerator.Current).Value;
                        if (rxClass.IsDerivedFrom(searchClassType)) {
                            if (!allowClassesWithoutDxfName && ((rxClass.DxfName == null) || (rxClass.DxfName == string.Empty))) {
                                // skip it
                            }
                            else {
                                if (derivedClasses.Contains(rxClass) == false)
                                    derivedClasses.Add(rxClass);
                            }
                        }
                    }
                }
            }
        }

        private SelectionFilter
        GetSelectionFilter()
        {
            RXClass selectedClass = (RXClass)SystemObjects.ClassDictionary[(string)m_puClassType.SelectedItem];
            if (selectedClass != null) {
                    // see if they want this class plus all derived classes
                if (m_cbDoIsKindOfTest.Checked == false) {
                        // just this class, use a simple filter
                    TypedValue[] fitlerPairs = {
                        new TypedValue((int)DxfCode.Start, selectedClass.DxfName),
                    };
                    return new SelectionFilter(fitlerPairs);
                }
                else {
                        // look up all derived classes and use a big "OR" statement
                    ArrayList filterClasses = new ArrayList();
                    GetClassesDerivedFrom(selectedClass, filterClasses, false /* don't allow classes without a DXF name*/);

                    int i=0;
                    TypedValue[] values = new TypedValue[filterClasses.Count+2];

                    values[i++] = new TypedValue((int)DxfCode.Operator, "<or");

                    foreach (RXClass tmpClass in filterClasses) {
                        values[i++] = new TypedValue((int)DxfCode.Start, tmpClass.DxfName);
                    }

                    values[i] = new TypedValue((int)DxfCode.Operator, "or>");

                        // in case you want to see what the filter looks like
                    //foreach (TypedValue tv in values) {
                    //    m_editor.WriteMessage(string.Format("\nFilter Pair: {0}", tv.ToString()));
                    //}

                    return new SelectionFilter(values);
                }
            }

            return null;
        }


    }
}