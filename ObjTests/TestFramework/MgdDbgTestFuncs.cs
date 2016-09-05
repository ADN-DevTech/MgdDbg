
//
// (C) Copyright 2005 by Autodesk, Inc. 
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
using System.Collections;


namespace MgdDbg.Test
{
	/// <summary>
	/// Summary description for MgdDbgTestFuncs.
	/// </summary>
	public abstract class MgdDbgTestFuncs
	{
        // TBD: public for now
        public    ArrayList          m_testFrameworkFuncs = new ArrayList();

        private   static ArrayList   m_testFuncs          = new ArrayList();    		
        
        // other modules can extend the tests available to the Test Framework UI
        // by adding groups of TestFuncs here.  Allocate them in your app's constructor
        // and register them with this class.  In your app's destructor, remove them
        // from here and delete them.
        public static void
        AddTestFuncsToFramework (MgdDbgTestFuncs testFuncs)
        {
            m_testFuncs.Add(testFuncs);
        }

        public static void
        RemoveTestFuncsFromFramework (MgdDbgTestFuncs testFuncs)
        {
            m_testFuncs.Remove(testFuncs);
        }

		protected
        MgdDbgTestFuncs ()
		{
		}
		
		public static ArrayList
        RegisteredTestFuncs ()
		{
            return m_testFuncs;
		}
	}
}
