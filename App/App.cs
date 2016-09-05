
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
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;

[assembly:ExtensionApplication(typeof(MgdDbg.App))]
[assembly:CommandClass(typeof(MgdDbg.Test.TestCmds))]

namespace MgdDbg
{
	
	public class App : IExtensionApplication
	{
        private ArrayList m_tests = new ArrayList();
        private AppDocReactor m_appDocReactor = null;

		public void
		Initialize()
		{
            Utils.AcadUi.PrintToCmdLine("\nLoading MgdDbg...");
            
                // register any Snoop Collector Extension objects that we have
            Snoop.CollectorExts.Object extObj = new Snoop.CollectorExts.Object();
            Snoop.CollectorExts.RxObject extRxObj = new Snoop.CollectorExts.RxObject();
            Snoop.CollectorExts.DbObject extObjects = new Snoop.CollectorExts.DbObject();
            Snoop.CollectorExts.SymbolTable extSymTbl = new Snoop.CollectorExts.SymbolTable();
            Snoop.CollectorExts.Entity extEnts = new Snoop.CollectorExts.Entity();
            Snoop.CollectorExts.Color extColor = new Snoop.CollectorExts.Color();
            Snoop.CollectorExts.Geometry extGeom = new Snoop.CollectorExts.Geometry();
            Snoop.CollectorExts.GraphNodes extGraphNodes = new Snoop.CollectorExts.GraphNodes();
            Snoop.CollectorExts.DbMisc extDbMisc = new Snoop.CollectorExts.DbMisc();
            Snoop.CollectorExts.GraphicsInterface extGraphicsInterface = new Snoop.CollectorExts.GraphicsInterface();
            Snoop.CollectorExts.LayerManager extLayerMgr = new Snoop.CollectorExts.LayerManager();
            Snoop.CollectorExts.GraphicsSystem extGraphicsSystem = new Snoop.CollectorExts.GraphicsSystem();
            Snoop.CollectorExts.Publish extPublish = new Snoop.CollectorExts.Publish();
            Snoop.CollectorExts.Plotting extPlotting = new Snoop.CollectorExts.Plotting();
            Snoop.CollectorExts.EditorInput extEdInput = new Snoop.CollectorExts.EditorInput();
            
            AppContextMenu.AddContextMenu();    // add our commands to the App right-click menu

            CreateAndAddTestFuncs();            // populate the TestFramework with our functions

            m_appDocReactor = new AppDocReactor();
            m_appDocReactor.EnableEvents();

            AddFilterForSnoopClasses();
		}
		
		public void
		Terminate()
		{
            AppContextMenu.RemoveContextMenu();
            RemoveAndFreeTestFuncs();

            m_appDocReactor.DisableEvents();
		}

        /// <summary>
        /// The TestFramework allows us to plug tests and sample functions into an existing
        /// UI Framework.  For each TestFuncs object we've created to house our individual
        /// tests, we need to add them during App start up, and remove them during App shut down.
        /// </summary>
        
        private void
        CreateAndAddTestFuncs()
        {
            m_tests.Add(new MgdDbg.Test.DbTests());
            m_tests.Add(new MgdDbg.Test.MakeEntTests());
            m_tests.Add(new MgdDbg.Test.MakeSymTblRecTests());
            m_tests.Add(new MgdDbg.Test.ModifyEntTests());
            m_tests.Add(new MgdDbg.Test.QueryCurveTests());
            m_tests.Add(new MgdDbg.Test.QueryEntTests());
            m_tests.Add(new MgdDbg.Test.CategoryTests());

            foreach (MgdDbg.Test.MgdDbgTestFuncs testFunc in m_tests) {
                MgdDbg.Test.MgdDbgTestFuncs.AddTestFuncsToFramework(testFunc);
            }
        }

        /// <summary>
        /// Reverse of above.  Nothing to do for each TestFunc object though
        /// because we already know which ones were registered for this app.
        /// </summary>
        
        private void
        RemoveAndFreeTestFuncs()
        {
            foreach (MgdDbg.Test.MgdDbgTestFuncs testFunc in m_tests) {
                MgdDbg.Test.MgdDbgTestFuncs.RemoveTestFuncsFromFramework(testFunc);
            }
        }

        /// <summary>
        /// This function adds the assemblies we are interested in having the Snoop.Editor
        /// dialog get class information from.  We don't want to display class info for every
        /// assembly in .NET, just the ones we are responsible for.  So, it acts as a filter.
        /// </summary>
        
        private void
        AddFilterForSnoopClasses()
        {
            ArrayList assembliesToLoad = new ArrayList();
            
            assembliesToLoad.Add("mscorlib");       // for the base System.Object
            assembliesToLoad.Add("acmgd");
            assembliesToLoad.Add("acdbmgd");

            System.Reflection.AssemblyName[] assemblyNames = System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (System.Reflection.AssemblyName assemblyName in assemblyNames) {
                if (assembliesToLoad.Contains(assemblyName.Name)) {
                    MgdDbg.Snoop.Forms.Editor.assemblyNamesToLoad.Add(assemblyName.FullName);
                }
            }
        }
	}
}
