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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.PlottingServices;

using AcPb = Autodesk.AutoCAD.Publishing;
using AcApp = Autodesk.AutoCAD.ApplicationServices;
using AcDb = Autodesk.AutoCAD.DatabaseServices;

namespace MgdDbg.Reactors.Events {

    /// <summary>
    ///  Handle all the events exposed by the Publish namespace
    /// </summary>
    
    public class PublishEvents : EventsBase {

        private bool m_publishProps;

        public
        PublishEvents()
        {
            m_publishProps = false;
        }

        protected override void
        EnableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nPublish Events Turned On ...\n");

            Autodesk.AutoCAD.Publishing.Publisher pub = AcApp.Application.Publisher;

            pub.AboutToBeginBackgroundPublishing += new Autodesk.AutoCAD.Publishing.AboutToBeginBackgroundPublishingEventHandler(event_AboutToBeginBackgroundPublishing);
            pub.AboutToBeginPublishing += new Autodesk.AutoCAD.Publishing.AboutToBeginPublishingEventHandler(event_AboutToBeginPublishing);
            pub.AboutToEndPublishing += new Autodesk.AutoCAD.Publishing.AboutToEndPublishingEventHandler(event_AboutToEndPublishing);
            pub.AboutToMoveFile += new Autodesk.AutoCAD.Publishing.AboutToMoveFileEventHandler(event_AboutToMoveFile);
            pub.BeginAggregation += new Autodesk.AutoCAD.Publishing.BeginAggregationEventHandler(event_BeginAggregation);
            pub.BeginEntity += new Autodesk.AutoCAD.Publishing.BeginEntityEventHandler(event_BeginEntity);
            pub.BeginPublishingSheet += new Autodesk.AutoCAD.Publishing.BeginPublishingSheetEventHandler(event_BeginPublishingSheet);
            pub.BeginSheet += new Autodesk.AutoCAD.Publishing.BeginSheetEventHandler(event_BeginSheet);
            pub.CancelledOrFailedPublishing += new Autodesk.AutoCAD.Publishing.CancelledOrFailedPublishingEventHandler(event_CancelledOrFailedPublishing);
            pub.EndEntity += new Autodesk.AutoCAD.Publishing.EndEntityEventHandler(event_EndEntity);
            pub.EndPublish += new Autodesk.AutoCAD.Publishing.EndPublishEventHandler(event_EndPublish);
            pub.EndSheet += new Autodesk.AutoCAD.Publishing.EndSheetEventHandler(event_EndSheet);
            pub.InitPublishOptionsDialog += new Autodesk.AutoCAD.Publishing.InitPublishOptionsDialogEventHandler(event_InitPublishOptionsDialog);
        }

        protected override void
        DisableEventsImp()
        {
            Utils.AcadUi.PrintToCmdLine("\nPublish Events Turned Off ...\n");

            Autodesk.AutoCAD.Publishing.Publisher pub = AcApp.Application.Publisher;

            pub.AboutToBeginBackgroundPublishing -= new Autodesk.AutoCAD.Publishing.AboutToBeginBackgroundPublishingEventHandler(event_AboutToBeginBackgroundPublishing);
            pub.AboutToBeginPublishing -= new Autodesk.AutoCAD.Publishing.AboutToBeginPublishingEventHandler(event_AboutToBeginPublishing);
            pub.AboutToEndPublishing -= new Autodesk.AutoCAD.Publishing.AboutToEndPublishingEventHandler(event_AboutToEndPublishing);
            pub.AboutToMoveFile -= new Autodesk.AutoCAD.Publishing.AboutToMoveFileEventHandler(event_AboutToMoveFile);
            pub.BeginAggregation -= new Autodesk.AutoCAD.Publishing.BeginAggregationEventHandler(event_BeginAggregation);
            pub.BeginEntity -= new Autodesk.AutoCAD.Publishing.BeginEntityEventHandler(event_BeginEntity);
            pub.BeginPublishingSheet -= new Autodesk.AutoCAD.Publishing.BeginPublishingSheetEventHandler(event_BeginPublishingSheet);
            pub.BeginSheet -= new Autodesk.AutoCAD.Publishing.BeginSheetEventHandler(event_BeginSheet);
            pub.CancelledOrFailedPublishing -= new Autodesk.AutoCAD.Publishing.CancelledOrFailedPublishingEventHandler(event_CancelledOrFailedPublishing);
            pub.EndEntity -= new Autodesk.AutoCAD.Publishing.EndEntityEventHandler(event_EndEntity);
            pub.EndPublish -= new Autodesk.AutoCAD.Publishing.EndPublishEventHandler(event_EndPublish);
            pub.EndSheet -= new Autodesk.AutoCAD.Publishing.EndSheetEventHandler(event_EndSheet);
            pub.InitPublishOptionsDialog -= new Autodesk.AutoCAD.Publishing.InitPublishOptionsDialogEventHandler(event_InitPublishOptionsDialog);
        }

        private void
        event_AboutToBeginBackgroundPublishing(object sender, Autodesk.AutoCAD.Publishing.AboutToBeginBackgroundPublishingEventArgs e)
        {
            PrintReactorMessage("About To Begin Background Publishing");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "AboutToBeginBackgroundPublishing";
                dbox.ShowDialog();
            }
        }

        private void
        event_AboutToBeginPublishing(object sender, Autodesk.AutoCAD.Publishing.AboutToBeginPublishingEventArgs e)
        {
            PrintReactorMessage("About To Begin Publishing");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "AboutToBeginPublishing";
                dbox.ShowDialog();
            }
        }

        private void
        event_AboutToEndPublishing(object sender, Autodesk.AutoCAD.Publishing.PublishEventArgs e)
        {
            PrintReactorMessage("About To End Publishing");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "AboutToEndPublishing";
                dbox.ShowDialog();
            }
        }

        private void
        event_EndPublish(object sender, Autodesk.AutoCAD.Publishing.PublishEventArgs e)
        {
            PrintReactorMessage("End Publish");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "EndPublish";
                dbox.ShowDialog();
            }
        }

        private void
        event_CancelledOrFailedPublishing(object sender, Autodesk.AutoCAD.Publishing.PublishEventArgs e)
        {
            PrintReactorMessage("Cancelled Or Failed Publishing");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "CancelledOrFailedPublishing";
                dbox.ShowDialog();
            }
        }

        private void
        event_BeginPublishingSheet(object sender, Autodesk.AutoCAD.Publishing.BeginPublishingSheetEventArgs e)
        {
            PrintReactorMessage("Begin Publishing Sheet");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "BeginPublishingSheet";
                dbox.ShowDialog();
            }
        }

        private void
        event_BeginAggregation(object sender, Autodesk.AutoCAD.Publishing.BeginAggregationEventArgs e)
        {
            PrintReactorMessage("Begin Aggregation");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "BeginAggregation";
                dbox.ShowDialog();
            }
        }

        private void
        event_AboutToMoveFile(object sender, Autodesk.AutoCAD.Publishing.PublishEventArgs e)
        {
            PrintReactorMessage("About To Move File");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "AboutToMoveFile";
                dbox.ShowDialog();
            }
        }

        private void
        event_EndSheet(object sender, Autodesk.AutoCAD.Publishing.PublishSheetEventArgs e)
        {
            PrintReactorMessage("End Sheet");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "EndSheet";
                dbox.ShowDialog();
            }
        }

        private void
        event_EndEntity(object sender, Autodesk.AutoCAD.Publishing.PublishEntityEventArgs e)
        {
            PrintReactorMessage("End Entity");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "EndEntity";
                dbox.ShowDialog();
            }
        }

        private void
        event_BeginSheet(object sender, Autodesk.AutoCAD.Publishing.PublishSheetEventArgs e)
        {
            PrintReactorMessage("Begin Sheet");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "BeginSheet";
                dbox.ShowDialog();
            }

            if (m_publishProps) {
                PublishSheetProperties(e);
            }
        }

 
        private void
        event_BeginEntity(object sender, Autodesk.AutoCAD.Publishing.PublishEntityEventArgs e)
        {
            PrintReactorMessage("Begin Entity");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "BeginEntity";
                dbox.ShowDialog();
            }

            if (m_publishProps) {
                PublishEntityProperties(e);
            }
        }

        private void
        event_InitPublishOptionsDialog(object sender, Autodesk.AutoCAD.Publishing.PublishUIEventArgs e)
        {
            PrintReactorMessage("Init Publish Options Dialog");
            if (m_showDetails) {
                Snoop.Forms.Objects dbox = new Snoop.Forms.Objects(e);
                dbox.Text = "InitPublishOptionsDialog";
                dbox.ShowDialog();
            }
        }

        private void
        PublishEntityProperties (Autodesk.AutoCAD.Publishing.PublishEntityEventArgs e)
        {
            Int32 curNodeId = -1;
            AcDb.ObjectIdCollection refPathIds;

            refPathIds = e.getEntityBlockRefPath();
            curNodeId = e.GetEntityNode(e.Entity.ObjectId, refPathIds);

            AcPb.EPlotPropertyBag propBag = new AcPb.EPlotPropertyBag();
            AcPb.EPlotProperty prop;
            AcPb.EPlotAttribute attrib;

            //Add the property
            //
            prop = new AcPb.EPlotProperty();
            prop.Name = "Cast Shadows";
            prop.Category = "Entity Properties";
            prop.Value = e.Entity.CastShadows.ToString();

            //Add the hidden attribute data to the property
            //
            attrib = new AcPb.EPlotAttribute();
            attrib.Ns = "MgdDbg";
            attrib.NsUrl = "http://adsk/MgdDbg.xsd";
            attrib.Name = "value";
            attrib.Value = System.Convert.ToInt32(e.Entity.CastShadows).ToString();

            prop.Attributes.Add(attrib);
            propBag.Properties.Add(prop);

            //Add the property
            //
            prop = new AcPb.EPlotProperty();
            prop.Name = "Clone Me For Dragging";
            prop.Category = "Entity Properties";
            prop.Value = e.Entity.CloneMeForDragging.ToString();

            //Add the hidden attribute data to the property
            //
            attrib = new AcPb.EPlotAttribute();
            attrib.Ns = "MgdDbg";
            attrib.NsUrl = "http://adsk/MgdDbg.xsd";
            attrib.Name = "value";
            attrib.Value = System.Convert.ToInt32(e.Entity.CloneMeForDragging).ToString();

            prop.Attributes.Add(attrib);
            propBag.Properties.Add(prop);

            //Add the property
            //
            prop = new AcPb.EPlotProperty();
            prop.Name = "Collision Type";
            prop.Category = "Entity Properties";
            prop.Value = e.Entity.CollisionType.ToString();

            //Add the hidden attribute data to the property
            //
            attrib = new AcPb.EPlotAttribute();
            attrib.Ns = "MgdDbg";
            attrib.NsUrl = "http://adsk/MgdDbg.xsd";
            attrib.Name = "value";
            attrib.Value = System.Convert.ToInt32(e.Entity.CollisionType).ToString();

            prop.Attributes.Add(attrib);
            propBag.Properties.Add(prop);

            //Add the property
            //
            prop = new AcPb.EPlotProperty();
            prop.Name = "Color";
            prop.Category = "Entity Properties";
            prop.Value = e.Entity.Color.ColorNameForDisplay;

            //Add the hidden attribute data to the property
            //
            attrib = new AcPb.EPlotAttribute();
            attrib.Ns = "MgdDbg";
            attrib.NsUrl = "http://adsk/MgdDbg.xsd";
            attrib.Name = "value";
            attrib.Value = e.Entity.ColorIndex.ToString();

            prop.Attributes.Add(attrib);
            propBag.Properties.Add(prop);

            //Now that all the properties have been setup, associate them with the node
            //
            AcPb.DwfNode dwfNode = null;

            if (curNodeId == -1)
            {
                curNodeId = e.GetNextAvailableNode();
                dwfNode = new AcPb.DwfNode(curNodeId, "MgdDbg");

                e.AddNodeToMap(e.Entity.ObjectId, refPathIds, curNodeId);
            }
            else
            {
                dwfNode = e.GetNode(curNodeId);
            }

            propBag.Id = "MGDDBG-" + e.UniqueEntityId;
            propBag.References.Add(propBag.Id);

            e.AddPropertyBag(propBag);
            e.AddPropertiesIds(propBag, dwfNode);
        }
        
        private void
        PublishSheetProperties (Autodesk.AutoCAD.Publishing.PublishSheetEventArgs e)
        {
            AcPb.EPlotProperty prop;
            AcPb.EPlotProperty[] propArray = new AcPb.EPlotProperty[4];

            //Get the linear units
            //
            prop = new AcPb.EPlotProperty();
            prop.Name = "_UnitLinear";
            prop.Value = "inch";
            propArray[0] = prop;

            //Get the angular units
            //
            prop = new AcPb.EPlotProperty();
            prop.Name = "_UnitAngular";
            prop.Value = "radian";
            propArray[1] = prop;

            //Get the area units.
            //
            prop = new AcPb.EPlotProperty();
            prop.Name = "_UnitArea";
            prop.Value = "square_foot";
            propArray[2] = prop;

            //Get the volume units
            //
            prop = new AcPb.EPlotProperty();
            prop.Name = "_UnitVolume";
            prop.Value = "cubic_foot";
            propArray[3] = prop;

            e.AddPagePropertyRange(propArray);
        }

        # region Print Abstraction

        private void
        PrintReactorMessage(string eventStr)
        {
            string printString = string.Format("\n[Publish Event] : {0,-20} ", eventStr);
            Utils.AcadUi.PrintToCmdLine(printString);
        }

        #endregion
    }
}