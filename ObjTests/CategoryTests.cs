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
using System.Collections;
using System.Windows.Forms;

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.EditorInput;
using AcadApp = Autodesk.AutoCAD.ApplicationServices;

namespace MgdDbg.Test
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryTests : MgdDbgTestFuncs
    {
        Autodesk.AutoCAD.DatabaseServices.Database db = null;

        public
        CategoryTests ()
        {
            db = Utils.Db.GetCurDwg();

            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Selection Set Options", 
                "Various combinations of getting selection sets", "Selection Set",
                new MgdDbgTestFuncInfo.TestFunc(SelectionSet), MgdDbgTestFuncInfo.TestType.Other));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Select Red Circles Only",
                "Use a filter to only allow selection of Red Circles", "Selection Set",
                new MgdDbgTestFuncInfo.TestFunc(SelectRedCircles), MgdDbgTestFuncInfo.TestType.Other));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Select Circles or Arcs",
                "Use a filter to only allow selection of Circles and Arcs", "Selection Set",
                new MgdDbgTestFuncInfo.TestFunc(SelectCirclesAndArcs), MgdDbgTestFuncInfo.TestType.Other));			 
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Diff Entities", 
                "Diff between two entities", "Reflection", 
                new MgdDbgTestFuncInfo.TestFunc(EntityDiff), MgdDbgTestFuncInfo.TestType.Other));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Diff Objects",
                "Diff between two objects", "Reflection",
                new MgdDbgTestFuncInfo.TestFunc(ObjectDiff), MgdDbgTestFuncInfo.TestType.Other));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Snoop XML Dom Document",
                "Exercise XML Dom API", "Import/Export",
                new MgdDbgTestFuncInfo.TestFunc(SnoopXml), MgdDbgTestFuncInfo.TestType.Other));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Test Prompts",
                "Exercise prompt classes", "Prompts",
                new MgdDbgTestFuncInfo.TestFunc(TestPrompts), MgdDbgTestFuncInfo.TestType.Other));           
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Drawing Stats",
               "Export database stats to XML file", "Import/Export",
               new MgdDbgTestFuncInfo.TestFunc(DwgStats), MgdDbgTestFuncInfo.TestType.Other));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Drawing Stats Batch",
              "Export batch stats to XML file", "Import/Export",
              new MgdDbgTestFuncInfo.TestFunc(DwgStatsBatch), MgdDbgTestFuncInfo.TestType.Other));
        }


        #region Tests

        /// <summary>
        /// A test for every basic combination of SelectionSet options
        /// </summary>
        
        public void
        SelectionSet()
        {
            ObjTests.Forms.SelectionSet dbox = new ObjTests.Forms.SelectionSet();
            AcadApp.Application.ShowModalDialog(dbox);
        }

        /// <summary>
        ///  Simple test to show how to do a filtered selection
        /// </summary>
        
        public void
        SelectRedCircles()
        {
            TypedValue[] filterPairs = {
                new TypedValue((int)DxfCode.Start, "CIRCLE"),
                new TypedValue((int)DxfCode.Color, 1),
            };

            SelectionFilter filter = new SelectionFilter(filterPairs);

            Editor editor = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions selOpts = new PromptSelectionOptions();
            selOpts.MessageForAdding = "Select red circles";

            PromptSelectionResult prRes = editor.GetSelection(filter);
            ShowPromptResult("Select Red Circles", prRes);
        }

        /// <summary>
        /// Simple test to show how to do a filtered selection with a boolean operator
        /// </summary>
        
        public void
        SelectCirclesAndArcs()
        {
            TypedValue[] filterPairs = {
                new TypedValue((int)DxfCode.Operator, "<OR"),
                new TypedValue((int)DxfCode.Start, "CIRCLE"),
                new TypedValue((int)DxfCode.Start, "ARC"),
                new TypedValue((int)DxfCode.Operator, "OR>"),
            };

            SelectionFilter filter = new SelectionFilter(filterPairs);

            Editor editor = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionOptions selOpts = new PromptSelectionOptions();
            selOpts.MessageForAdding = "Select circles and arcs";

            PromptSelectionResult prRes = editor.GetSelection(filter);
            ShowPromptResult("Select Circles and Arcs", prRes);
        }

        /// <summary>
        /// Display the result of the Selection Prompt to the user for inspection
        /// </summary>
        /// <param name="dboxTitle"></param>
        /// <param name="result"></param>
        
        private void
        ShowPromptResult(string dboxTitle, PromptSelectionResult result)
        {
            if (result.Status == PromptStatus.OK) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(result.Value);
                dbox.Text = dboxTitle;
                AcadApp.Application.ShowModalDialog(dbox);
            }
            else {
                MessageBox.Show(string.Format("Prompt status: {0}", result.Status.ToString()), "PromptSelectionResult", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }        

        /// <summary>
        /// Compare two entities values using relection, and show the differences.
        /// </summary>
        
        public void
   	    EntityDiff ()
        {
            Editor ed = AcadApp.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptEntityResult res = ed.GetEntity("\nSelect first entity");
            if (res.Status != PromptStatus.OK)
                return;
            ObjectId objId1 = res.ObjectId;

            res = ed.GetEntity("\nSelect second entity");
            if (res.Status != PromptStatus.OK)
                return;
            ObjectId objId2 = res.ObjectId;

            using (TransactionHelper trHlp = new TransactionHelper()) {
                trHlp.Start();

                EntityDiff dbox = new EntityDiff(objId1, objId2, trHlp);
                dbox.Text = "Diff Objects";
                AcadApp.Application.ShowModalDialog(dbox);

                trHlp.Commit();
            }
        }

        /// <summary>
        /// Compare two object's values using relection, and show the differences.
        /// </summary>

        public void
        ObjectDiff ()
        {
            using (TransactionHelper trHlpr = new TransactionHelper()) {
                trHlpr.Start();

                ObjectDiff diff = new ObjectDiff(trHlpr);
                AcadApp.Application.ShowModalDialog(diff);

                trHlpr.Commit();
            }
        }

        /// <summary>
        /// Allow the user to snoop through an XML file and test/learn all the various functions
        /// available from the XML DOM.
        /// </summary>
        
        public void
        SnoopXml ()
        {
            System.Windows.Forms.OpenFileDialog dbox = new System.Windows.Forms.OpenFileDialog();
            dbox.CheckFileExists = true;
            dbox.AddExtension = true;
            dbox.DefaultExt = "xml";
            dbox.Filter = "XML Files (*.xml)|*.xml";
            dbox.Multiselect = false;
            dbox.Title = "Select an XML file";

            if (dbox.ShowDialog() == DialogResult.OK) {
                try {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.Load(dbox.FileName);

                    Xml.Forms.Dom form = new Xml.Forms.Dom(xmlDoc);
                    AcadApp.Application.ShowModalDialog(form);
                }
                catch (System.Xml.XmlException e) {
                    MessageBox.Show(e.Message, "XML Exception");
                }
            }
        }

        /// <summary>
        /// Test/Demonstrate every option for Prompting
        /// </summary>
        
        public void
        TestPrompts()
        {
            Prompts.PromptTestForm form = new Prompts.PromptTestForm();

            while (AcadApp.Application.ShowModalDialog(form) == DialogResult.OK) {
                form.RunPrompt();
            }
        }        

        /// <summary>
        /// Publish out DWG stats to an XML file (number of blocks, how many times a layer is
        /// referenced, etc.).  The XML file can then be displayed as HTML Browser reports.
        /// After producing the XML file, double-click on the file:  ../MgdDbg/ReportBrowser/ObjCountReport.html
        /// and then load the XML file.
        /// </summary>
        
        public void
        DwgStats ()
        {
            // get output file to save XML report to
            System.Windows.Forms.SaveFileDialog dbox = new System.Windows.Forms.SaveFileDialog();
            dbox.CreatePrompt = false;
            dbox.OverwritePrompt = true;
            dbox.AddExtension = true;
            dbox.DefaultExt = "xml";
            dbox.Filter = "XML Files (*.xml)|*.xml";
            dbox.Title = "XML file to save report as";

            if (dbox.ShowDialog() == DialogResult.OK) {
                DwgStats.Report statReport = new DwgStats.Report();
                statReport.XmlReport(dbox.FileName, Utils.Db.GetCurDwg());
            }
        }
       
        /// <summary>
        /// Same thing as above except you can batch process several files at once
        /// </summary>
        
        public void
        DwgStatsBatch()
        {
                // get multiple files to batch process
            Autodesk.AutoCAD.Windows.OpenFileDialog dwgsForm = new Autodesk.AutoCAD.Windows.OpenFileDialog("Drawing Files To Process",
                                    null, "dwg", "DwgStatsBatch", Autodesk.AutoCAD.Windows.OpenFileDialog.OpenFileDialogFlags.AllowMultiple);
            if (dwgsForm.ShowDialog() != DialogResult.OK)
                return;

                // get output file to save XML report to
            System.Windows.Forms.SaveFileDialog dbox = new System.Windows.Forms.SaveFileDialog();
            dbox.CreatePrompt = false;
            dbox.OverwritePrompt = true;
            dbox.AddExtension = true;
            dbox.DefaultExt = "xml";
            dbox.Filter = "XML Files (*.xml)|*.xml";
            dbox.Title = "XML file to save report as";

            if (dbox.ShowDialog() == DialogResult.OK) {
                DwgStats.Report statReport = new DwgStats.Report();
                statReport.XmlReport(dbox.FileName, dwgsForm.GetFilenames());
            }
        }

        #endregion

    }
}
