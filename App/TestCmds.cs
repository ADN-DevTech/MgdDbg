
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
using System.Windows.Forms;
using System.Collections;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

using MgdDbg.Utils;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace MgdDbg.Test
{
	/// <summary>
	/// Summary description for TestCmds.
	/// </summary>
	public class TestCmds
	{

        [CommandMethod("MgdDbgTestTrans1", CommandFlags.Modal)]

        public void
        TestTrans1()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionResult res = ed.GetSelection();
            if (res.Status != PromptStatus.OK)
                return;
            
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = Utils.Db.GetCurDwg().TransactionManager;
            
            ObjectId[] objIds = res.Value.GetObjectIds();
            foreach (ObjectId objId in objIds) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
		            DBObject tmpObj = tr.GetObject(objId, OpenMode.ForRead);
                    tr.Commit();
                }
            }
        }

        [CommandMethod("MgdDbgTestTrans2", CommandFlags.Modal)]

        public void
        TestTrans2()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionResult res = ed.GetSelection();
            if (res.Status != PromptStatus.OK)
                return;
            
            Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = Utils.Db.GetCurDwg().TransactionManager;
            
            ObjectId[] objIds = res.Value.GetObjectIds();
            foreach (ObjectId objId in objIds) {
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
		            DBObject tmpObj = tr.GetObject(objId, OpenMode.ForRead);
                    tr.Abort();
                }
            }
        }
	       
        //[CommandMethod("MgdDbg", "MgdDbgSnoopEnts", "SnoopEnts", CommandFlags.Modal)]       
        [CommandMethod("MgdDbgSnoopEnts", CommandFlags.Modal)]

        public void
        SnoopEntity()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptSelectionResult res = ed.GetSelection();
            if (res.Status != PromptStatus.OK)
                return;
            
            ObjectIdCollection selSet = new ObjectIdCollection(res.Value.GetObjectIds());
            
            using (TransactionHelper trHlp = new TransactionHelper()) {
                trHlp.Start();
                
                Snoop.Forms.DBObjects dbox = new Snoop.Forms.DBObjects(selSet, trHlp);
                dbox.Text = "Selected Entities";
                AcadApp.ShowModalDialog(dbox);
                
                trHlp.Commit();
            }
        }

        //[CommandMethod("MgdDbg", "MgdDbgSnoopNEnts", "SnoopNEnts", CommandFlags.Modal)]       
        [CommandMethod("MgdDbgSnoopNEnts", CommandFlags.Modal)]

        public void
        SnoopNestedEntity ()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ObjectIdCollection selSet = new ObjectIdCollection();

            while (true) {
                PromptNestedEntityOptions prOptNent = new PromptNestedEntityOptions("\nSelect nested entities or: ");
                prOptNent.AppendKeywordsToMessage = true;
                prOptNent.Keywords.Add("Done");

                PromptNestedEntityResult res = ed.GetNestedEntity(prOptNent);
                if (res.Status == PromptStatus.OK)
                    selSet.Add(res.ObjectId);
                else if (res.Status == PromptStatus.Keyword)
                    break;
                else
                    return;
            }

            using (TransactionHelper trHlp = new TransactionHelper()) {
                trHlp.Start();

                Snoop.Forms.DBObjects dbox = new Snoop.Forms.DBObjects(selSet, trHlp);
                dbox.Text = "Selected Entities";
                AcadApp.ShowModalDialog(dbox);

                trHlp.Commit();
            }
        }

        //[CommandMethod("MgdDbg", "MgdDbgSnoopByHandle", "SnoopEnts", CommandFlags.Modal)]       
        [CommandMethod("MgdDbgSnoopByHandle", CommandFlags.Modal)]

        public void
        SnoopEntityByHandle ()
        {
            ObjectId id = ObjectId.Null;           
            ObjectIdCollection selSet = new ObjectIdCollection();

            Database db = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;            

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            PromptStringOptions options = new PromptStringOptions("Handle of database object");

            String str = ed.GetString(options).StringResult;

            if (str != String.Empty) {

                Handle h = Utils.Db.StringToHandle(str);

                try {
                    id = Utils.Db.HandleToObjectId(db, h);

                    selSet.Add(id);

                    using (TransactionHelper trHlp = new TransactionHelper()) {
                        trHlp.Start();

                        Snoop.Forms.DBObjects dbox = new Snoop.Forms.DBObjects(selSet, trHlp);
                        dbox.Text = "Selected Entities";
                        AcadApp.ShowModalDialog(dbox);

                        trHlp.Commit();
                    }
                }
                catch (Autodesk.AutoCAD.Runtime.Exception x) {
                    AcadUi.PrintToCmdLine(string.Format("\nERROR: {0}", ((ErrorStatus)x.ErrorStatus).ToString()));
                }
            }
        }
        
        
        //[CommandMethod("MgdDbg", "MgdDbgSnoopDb", "SnoopDb", CommandFlags.Modal)]
        [CommandMethod("MgdDbgSnoopDb", CommandFlags.Modal)]
        
        public void
        SnoopDatabase()
        {
            Database db = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;

            using (TransactionHelper trHlp = new TransactionHelper(db)) {
                trHlp.Start();
                
                Snoop.Forms.Database dbox = new Snoop.Forms.Database(db, trHlp);
                dbox.Text = db.Filename;
                AcadApp.ShowModalDialog(dbox);
                
                trHlp.Commit();
            }
        }
        
        //[CommandMethod("MgdDbg", "MgdDbgSnoopEd", "SnoopEd", CommandFlags.Modal)]
        [CommandMethod("MgdDbgSnoopEd", CommandFlags.Modal)]

        public void
        SnoopEd()
        {
            Snoop.Forms.Editor dbox = new Snoop.Forms.Editor();
            AcadApp.ShowModalDialog(dbox);
        }

        //[CommandMethod("MgdDbg", "MgdDbgEvents", "Reactors", CommandFlags.Modal)]
        [CommandMethod("MgdDbgEvents", CommandFlags.Modal)]

        public void
        Events()
        {
            MgdDbg.Reactors.Forms.EventsForm dbox = new MgdDbg.Reactors.Forms.EventsForm();
            AcadApp.ShowModalDialog(dbox);
        }                               
        
        //[CommandMethod("MgdDbg", "MgdDbgTests", "Tests", CommandFlags.Modal)]
        [CommandMethod("MgdDbgTests", CommandFlags.Modal)]
        
        public void
        TestDb()
        {
            MgdDbg.Test.TestForm dbox = new MgdDbg.Test.TestForm(MgdDbgTestFuncs.RegisteredTestFuncs());
            if (AcadApp.ShowModalDialog(dbox) == DialogResult.OK)
                dbox.DoTest();
        }       
        
        //[CommandMethod("MgdDbg", "MgdDbgTestTabForm", "TestTabForm", CommandFlags.Modal)]
        [CommandMethod("MgdDbgTestTabForm", CommandFlags.Modal)]
        
        public void
        TestTabForm()
        {
            TestTabForm dbox = new TestTabForm();
            AcadApp.ShowModalDialog(dbox);
        }                     
        
        
        //[CommandMethod("MgdDbg", "MgdDbgTestSpeed1", "TestSpeed1", CommandFlags.Modal)]
        [CommandMethod("MgdDbgTestSpeed1", CommandFlags.Modal)]
        
        public void
        TestSpeed1()
        {
            Database db = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;
            //db.Usrtimer = true;
            System.DateTime time1 = System.DateTime.Now;
            
            using (Transaction tr = db.TransactionManager.StartTransaction()) {
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                
                double x = 0.0;
                double y = 0.0;
                int colorIndex = 0;
                
                for (int i=0; i<10; i++) {
                    Line tmpLine = new Line(new Point3d(x, y, 0.0), new Point3d(x, y+10.0, 0.0));
                    tmpLine.ColorIndex = colorIndex++;
                    btr.AppendEntity(tmpLine);
                    tr.AddNewlyCreatedDBObject(tmpLine, true);
                    
                    x += 5.0;
                    if (x > 500.0) {
                        x = 0.0;
                        y += 15.0;
                    }
                    
                        // wrap colorIndex when it reaches the limit
                    if (colorIndex > 255)
                        colorIndex = 0;
                }
                
                tr.Commit();
            }
            
            //System.DateTime time = db.Tdusrtimer;
            //Utils.AcadUi.PrintToCmdLine(string.Format("\nTIME: {0}", time.ToString()));
            //db.Usrtimer = false;
            
            System.DateTime time2 = System.DateTime.Now;
            System.TimeSpan elapsedTime = time2 - time1;
            Utils.AcadUi.PrintToCmdLine(string.Format("\nTIME: {0}", elapsedTime.ToString()));
        }

        [CommandMethod("MgdDbgTestAngle", CommandFlags.Modal)]

        public void
        TestAngle()
        {
            Point3d pt1 = new Point3d(0.0, 0.0, 0.0);
            Point3d pt2 = new Point3d(2.0, 2.0, 0.0);

                // METHOD 1:  Make a vector by subtracting pt1 from pt2 and then figure
                // out the angle to the XAxis
            double angle1 = Vector3d.XAxis.GetAngleTo(pt2 - pt1);

                // METHOD 2:  Same thing, but specify the Plane you want to measure on.
            Plane xyPlane = new Plane(Point3d.Origin, Vector3d.ZAxis);
            double angle2 = (pt2 - pt1).AngleOnPlane(xyPlane);

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage(string.Format("\nAngle 1: {0}", Autodesk.AutoCAD.Runtime.Converter.AngleToString(angle1, AngularUnitFormat.Current, -1)));
            ed.WriteMessage(string.Format("\nAngle 2: {0}", Autodesk.AutoCAD.Runtime.Converter.AngleToString(angle2, AngularUnitFormat.Current, -1)));

        }

        [CommandMethod("MgdDbgGetImageAngle", CommandFlags.Modal)]

        public void
        TestImageAngle() 
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

            PromptEntityOptions prOpts = new PromptEntityOptions("\nSelect an Image");
            prOpts.SetRejectMessage("\nSelected entity must by of type RasterImage");
            prOpts.AddAllowedClass(typeof(RasterImage), false);

            PromptEntityResult prRes = ed.GetEntity(prOpts);
            if (prRes.Status == PromptStatus.OK) {
                Autodesk.AutoCAD.DatabaseServices.TransactionManager tm = prRes.ObjectId.Database.TransactionManager;
                using (Autodesk.AutoCAD.DatabaseServices.Transaction tr = tm.StartTransaction()) {
                    RasterImage imgObj = (RasterImage)tr.GetObject(prRes.ObjectId, OpenMode.ForRead);

                    CoordinateSystem3d entEcs = imgObj.Orientation;

                    Vector3d arbXAxis = Utils.Db.GetEcsXAxis(entEcs.Zaxis);   // get AutoCAD's arbitrary X-Axis
                    double rotAngle1 = arbXAxis.GetAngleTo(entEcs.Xaxis, entEcs.Zaxis);
                    ed.WriteMessage(string.Format("\nECS rotation angle: {0}", Autodesk.AutoCAD.Runtime.Converter.AngleToString(rotAngle1, AngularUnitFormat.Current, -1)));

                    Plane ucsPlane = Utils.Db.GetUcsPlane(prRes.ObjectId.Database);
                    double rotAngle2 = entEcs.Xaxis.AngleOnPlane(ucsPlane);
                    ed.WriteMessage(string.Format("\nRotation angle relative to UCS: {0}", Autodesk.AutoCAD.Runtime.Converter.AngleToString(rotAngle2, AngularUnitFormat.Current, -1)));

                    tr.Commit();
                }
            }
        }               
    }
}
