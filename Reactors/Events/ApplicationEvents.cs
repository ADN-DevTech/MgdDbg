
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
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;

namespace MgdDbg.Reactors.Events
{

	public class ApplicationEvents : EventsBase
	{

		public
		ApplicationEvents()
		{
		}

		protected override void
		EnableEventsImp()
		{
			Utils.AcadUi.PrintToCmdLine("\nApplication Events Turned On ...\n");

			Application.BeginQuit += Application_BeginQuit;
			Application.DisplayingCustomizeDialog += new TabbedDialogEventHandler(event_DisplayingCustomizeDialog);
			Application.DisplayingDraftingSettingsDialog += new TabbedDialogEventHandler(event_DisplayingDraftingSettingsDialog);
			Application.DisplayingOptionDialog += new TabbedDialogEventHandler(event_DisplayingOptionDialog);
			Application.QuitAborted += new EventHandler(event_QuitAborted);
			Application.QuitWillStart += new EventHandler(event_QuitWillStart);
			Application.SystemVariableChanged += new SystemVariableChangedEventHandler(event_SystemVariableChanged);
			Application.SystemVariableChanging += new SystemVariableChangingEventHandler(event_SystemVariableChanging);
		}

		private void Application_BeginQuit(object sender, BeginQuitEventArgs e)
		{
			PrintEventMessage("Begin Quit");
		}

		protected override void
		  DisableEventsImp()
		{
			Utils.AcadUi.PrintToCmdLine("\nApplication Events Turned Off ...\n");

			Application.BeginQuit -= Application_BeginQuit;
			Application.DisplayingCustomizeDialog -= new TabbedDialogEventHandler(event_DisplayingCustomizeDialog);
			Application.DisplayingDraftingSettingsDialog -= new TabbedDialogEventHandler(event_DisplayingDraftingSettingsDialog);
			Application.DisplayingOptionDialog -= new TabbedDialogEventHandler(event_DisplayingOptionDialog);
			Application.QuitAborted -= new EventHandler(event_QuitAborted);
			Application.QuitWillStart -= new EventHandler(event_QuitWillStart);
			Application.SystemVariableChanged -= new SystemVariableChangedEventHandler(event_SystemVariableChanged);
			Application.SystemVariableChanging -= new SystemVariableChangingEventHandler(event_SystemVariableChanging);
		}


		private void
		event_DisplayingCustomizeDialog(object sender, TabbedDialogEventArgs e)
		{
			PrintEventMessage("Displaying Customize Dialog");
		}

		private void
		event_DisplayingDraftingSettingsDialog(object sender, TabbedDialogEventArgs e)
		{
			PrintEventMessage("Displaying Drafting Settings Dialog");
		}

		private void
		event_DisplayingOptionDialog(object sender, TabbedDialogEventArgs e)
		{
			PrintEventMessage("Displaying Option Dialog");
		}

		private void
		event_QuitAborted(object sender, EventArgs e)
		{
			PrintEventMessage("Quit Aborted");
		}

		private void
		event_QuitWillStart(object sender, EventArgs e)
		{
			PrintEventMessage("Quit Will Start");
		}

		private void
		event_SystemVariableChanged(object sender, SystemVariableChangedEventArgs e)
		{
			if (e.Changed)
				PrintEventMessage("System Var Changed", e.Name);
			else
				PrintEventMessage("System Var Not Changed Successfully", e.Name);
		}

		private void
		event_SystemVariableChanging(object sender, SystemVariableChangingEventArgs e)
		{
			PrintEventMessage("System Var Changing", e.Name);
		}



		#region Print Abstraction

		private void
		PrintEventMessage(string eventStr)
		{
			string printString = string.Format("\n[App Event] : {0,-25}", eventStr);
			Utils.AcadUi.PrintToCmdLine(printString);
		}

		private void
		PrintEventMessage(string eventStr, string msg)
		{
			string printString = string.Format("\n[App Event] : {0,-25} : {1}", eventStr, msg);
			Utils.AcadUi.PrintToCmdLine(printString);
		}

		#endregion

	}
}
