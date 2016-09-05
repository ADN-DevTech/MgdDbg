
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
using Autodesk.AutoCAD.DatabaseServices;
using MgdDbg.Snoop.Collectors;

namespace MgdDbg.Snoop.CollectorExts
{
	/// <summary>
	/// This is a Snoop Collector Extension object to collect data from Graph objects.
	/// </summary>
	public class GraphNodes : CollectorExt
	{
		public
		GraphNodes()
		{
		}
		
        protected override void
        CollectEvent(object sender, CollectorEventArgs e)
        {
                // cast the sender object to the SnoopCollector we are expecting
            Collector snoopCollector = sender as Collector;
            if (snoopCollector == null) {
                Debug.Assert(false);    // why did someone else send us the message?
                return;
            }
            
                // see if it is a type we are responsible for
            GraphNode node = e.ObjToSnoop as GraphNode;
            if (node != null)            
                Stream(snoopCollector.Data(), node);
                
                // see if it is a type we are responsible for
            Graph graph = e.ObjToSnoop as Graph;
            if (graph != null)            
                Stream(snoopCollector.Data(), graph);
        }
        
        
        private void
        Stream(ArrayList data, GraphNode node)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(GraphNode)));
            
            data.Add(new Snoop.Data.Bool("Is cycle node", node.IsCycleNode));
            data.Add(new Snoop.Data.Int("Num cycle in", node.NumCycleIn));
            data.Add(new Snoop.Data.Int("Num cycle out", node.NumCycleOut));
            data.Add(new Snoop.Data.Int("Num in", node.NumIn));
            data.Add(new Snoop.Data.Int("Num out", node.NumOut));
            
            XrefGraphNode xGraphNode = node as XrefGraphNode;
            if (xGraphNode != null)
                Stream(data, xGraphNode);
        }
        
        private void
        Stream(ArrayList data, XrefGraphNode xGraphNode)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(XrefGraphNode)));

            data.Add(new Snoop.Data.String("Name", xGraphNode.Name));
            data.Add(new Snoop.Data.ObjectId("Block table record ID", xGraphNode.BlockTableRecordId));
            data.Add(new Snoop.Data.Database("Database", xGraphNode.Database));
            data.Add(new Snoop.Data.Bool("Is nested", xGraphNode.IsNested));
            data.Add(new Snoop.Data.String("Xref status", xGraphNode.XrefStatus.ToString()));
            data.Add(new Snoop.Data.String("Xref notification status", xGraphNode.XrefNotificationStatus.ToString()));
        }
        
        private void
        Stream(ArrayList data, Graph graph)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(Graph)));

            data.Add(new Snoop.Data.Bool("Is empty", graph.IsEmpty));
            data.Add(new Snoop.Data.Int("Num nodes", graph.NumNodes));
            
            XrefGraph xGraph = graph as XrefGraph;
            if (xGraph != null)
                Stream(data, xGraph);
        }
        
        private void
        Stream(ArrayList data, XrefGraph xgraph)
        {
            data.Add(new Snoop.Data.ClassSeparator(typeof(XrefGraph)));

                // no data at this level
        }
        
	}
}
