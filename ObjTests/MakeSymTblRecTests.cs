
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

namespace MgdDbg.Test
{
	/// <summary>
	/// Summary description for MakeSymTblRecTests.
	/// </summary>
	public class MakeSymTblRecTests : MgdDbgTestFuncs
	{
	    private Database    m_db = null;
	    
		public
		MakeSymTblRecTests()
		{	   
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Block", "Hardwired block", typeof(BlockTableRecord), new MgdDbgTestFuncInfo.TestFunc(Block), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Block (CompBldr)", "Hardwired block (Using CompBldr)", typeof(BlockTableRecord), new MgdDbgTestFuncInfo.TestFunc(BlockCompBldr), MgdDbgTestFuncInfo.TestType.Create));
            m_testFrameworkFuncs.Add(new MgdDbgTestFuncInfo("Layer", "Hardwired layer", typeof(LayerTableRecord), new MgdDbgTestFuncInfo.TestFunc(Layer), MgdDbgTestFuncInfo.TestType.Create));
        }

        #region Tests

        public void
        Block()
        {
            m_db           = Utils.Db.GetCurDwg();
            string symName = "Jimbo";
            
            if (Utils.SymTbl.SymbolTableRecExists(typeof(BlockTableRecord), symName, m_db)) {
                bool answer;
                PromptStatus stat = Utils.AcadUi.PromptYesNo(string.Format("\nBlock \"{0}\"already exists. Replace?", symName), false, out answer);
                if ((stat != PromptStatus.OK) || (answer == false))
                    return;
            }
            
            using (TransactionHelper tr = new TransactionHelper(m_db)) {
                tr.Start();
               
                BlockTableRecord blkRec = tr.DefineNewBlockRec(symName);
                
                Line line = new Line(new Point3d(0.0, 0.0, 0.0), new Point3d(10.0, 10.0, 0.0));
                blkRec.AppendEntity(line);
                tr.Transaction.AddNewlyCreatedDBObject(line, true);
                
                Circle circ = new Circle();
                circ.Center = new Point3d(0.0, 0.0, 0.0);
                circ.Radius = 5.0;
                blkRec.AppendEntity(circ);
                tr.Transaction.AddNewlyCreatedDBObject(circ, true);
                
                tr.Commit();
                Utils.AcadUi.PrintToCmdLine(string.Format("\nCreated block \"{0}\".", symName));
            }
        }

	    public void
        BlockCompBldr()
        {
            m_db           = Utils.Db.GetCurDwg();
            string symName = "Sophie";
            
            if (Utils.SymTbl.SymbolTableRecExists(typeof(BlockTableRecord), symName, m_db)) {
                bool answer;
                PromptStatus stat = Utils.AcadUi.PromptYesNo(string.Format("\nBlock \"{0}\"already exists. Replace?", symName), false, out answer);
                if ((stat != PromptStatus.OK) || (answer == false))
                    return;
            }
            
            using (CompBldrNamedBlkDef compBldr = new CompBldrNamedBlkDef(m_db, symName)) {
                compBldr.Start();
                                
                Line line = new Line(new Point3d(0.0, 0.0, 0.0), new Point3d(-10.0, -10.0, 0.0));
                compBldr.SetToDefaultProps(line);
                compBldr.AddToDb(line);
                
                Circle circ = new Circle();
                circ.Center = new Point3d(0.0, 0.0, 0.0);
                circ.Radius = 5.0;
                compBldr.SetToDefaultProps(circ);
                compBldr.AddToDb(circ);
                
                compBldr.Commit();
                Utils.AcadUi.PrintToCmdLine(string.Format("\nCreated block \"{0}\".", symName));
            }
        }

        public void
        Layer()
        {
            m_db           = Utils.Db.GetCurDwg();
            string symName = "Jimbo";
            
            using (TransactionHelper tr = new TransactionHelper(m_db)) {
                tr.Start();
                
                if (tr.SymbolTableRecExists(typeof(LayerTableRecord), symName)) {
                    Utils.AcadUi.PrintToCmdLine(string.Format("\nLayer \"{0}\" already exists.", symName));
                    return;
                }
                
                LayerTableRecord lyr = new LayerTableRecord();
                lyr.Name = symName;
                lyr.LinetypeObjectId = Utils.SymTbl.GetOrLoadLinetypeId("ZIGZAG", m_db);
                //lyr.Description = "Layer created programmatically by MgdDbg"; // Can't set until after its added to the database!
                
                lyr.Color = Color.FromRgb(0, 130, 160);
                
                tr.AddNewSymbolRec(lyr);
                
                tr.Commit();
                Utils.AcadUi.PrintToCmdLine(string.Format("\nCreated layer \"{0}\".", symName));
            }
        }

        #endregion
    }
}
