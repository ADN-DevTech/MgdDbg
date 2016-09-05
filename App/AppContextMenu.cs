
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
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Windows;

namespace MgdDbg
{
    /// <summary>
    /// Simple derived MenuItem class to keep a command name so that we can have
    /// a single generic callback function for the event.
    /// </summary>
    
    public class AppContextMenuItem : Autodesk.AutoCAD.Windows.MenuItem
    {
        private string m_cmdName;
        
        public
        AppContextMenuItem(string title, string cmdName)
        :   base(title)
        {
            m_cmdName = cmdName;
        }
        
        public string
        CommandName
        {
            get { return m_cmdName; }
            set { m_cmdName = value; }
        }  
    }
    
	/// <summary>
	/// The AppContextMenu for our app.
	/// </summary>
	
	public class AppContextMenu
	{
        static ContextMenuExtension m_appMenu = null;

		public static void
		AddContextMenu()
		{
			m_appMenu = new ContextMenuExtension();
			m_appMenu.Title = "MgdDbg";

            m_appMenu.MenuItems.Add(new AppContextMenuItem("Snoop Entities...", "MgdDbgSnoopEnts"));
            m_appMenu.MenuItems.Add(new AppContextMenuItem("Snoop Entities (nested)...", "MgdDbgSnoopNEnts"));
            m_appMenu.MenuItems.Add(new AppContextMenuItem("Snoop (by Handle)...", "MgdDbgSnoopByHandle"));
            m_appMenu.MenuItems.Add(new AppContextMenuItem("Snoop Database...", "MgdDbgSnoopDb"));
            m_appMenu.MenuItems.Add(new AppContextMenuItem("Snoop Editor...", "MgdDbgSnoopEd"));
            m_appMenu.MenuItems.Add(new MenuItem(""));    // separator
            m_appMenu.MenuItems.Add(new AppContextMenuItem("Events...", "MgdDbgEvents"));
            m_appMenu.MenuItems.Add(new MenuItem(""));    // separator
            m_appMenu.MenuItems.Add(new AppContextMenuItem("Test Framework...", "MgdDbgTests"));                               
		    
		    foreach (MenuItem mnuItem in m_appMenu.MenuItems) {
		        AppContextMenuItem appContextMnuItem = mnuItem as AppContextMenuItem;
		        if (appContextMnuItem != null)
			        appContextMnuItem.Click += new EventHandler(ExecuteCommand);
			}
			
		    Application.AddDefaultContextMenuExtension(m_appMenu);
		}
		
	    private static void
		ExecuteCommand(Object o, EventArgs e)
		{
            AppContextMenuItem mnuItem = (AppContextMenuItem)o;

		    string fullCmdLine = string.Format("_{0}\n", mnuItem.CommandName);
		    Application.DocumentManager.MdiActiveDocument.SendStringToExecute(fullCmdLine, false, false, true);
		}

        public static void
        RemoveContextMenu()
        {
            Application.RemoveDefaultContextMenuExtension(m_appMenu);
        }

    }
}
