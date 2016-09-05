# MgdDbg

Inspection tool for AutoCAD Database (DWG) created with .NET.

# Setup

Open the <b>MgdDbg.sln</b> with Visual Studio (this version was tested with Visual Studio 2015). It should Build as is. If references are missing, go to project properties, Reference Path tab and adjust the AutoCAD install path.

Start AutoCAD, type <b>NETLOAD</b> command, select the DLL under <your project folder>/bin/debug/MgdDbg.dll. Right click on the drawing area, the menu item <b>MgdDbg</b> should appear with the options.
