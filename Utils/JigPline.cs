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

using AcDb = Autodesk.AutoCAD.DatabaseServices;
using AcEd = Autodesk.AutoCAD.EditorInput;
using AcGe = Autodesk.AutoCAD.Geometry;
using AcAp = Autodesk.AutoCAD.ApplicationServices;

namespace MgdDbg.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class JigPline : AcEd.DrawJig
    {
        private AcGe.Point3d m_startPt;
        private AcGe.Point3d m_endPt;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPt"></param>
        public
        JigPline (AcGe.Point3d startPt)
        {
            m_startPt = startPt;
        }

        /// <summary>
        /// 
        /// </summary>
        public AcGe.Point3d
        EndPoint
        {
            get { return m_endPt; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="draw"></param>
        /// <returns></returns>
        protected override bool
        WorldDraw (Autodesk.AutoCAD.GraphicsInterface.WorldDraw draw)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="draw"></param>
        protected override void
        ViewportDraw (Autodesk.AutoCAD.GraphicsInterface.ViewportDraw draw)
        {
            draw.Geometry.WorldLine(m_startPt, m_endPt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompts"></param>
        /// <returns></returns>
        protected override Autodesk.AutoCAD.EditorInput.SamplerStatus
        Sampler (Autodesk.AutoCAD.EditorInput.JigPrompts prompts)
        {
            AcEd.SamplerStatus samplerStatus = AcEd.SamplerStatus.NoChange;
            AcEd.JigPromptPointOptions opts = new AcEd.JigPromptPointOptions();

            opts.Message = "\nPoint";
            opts.BasePoint = m_startPt;
            opts.UseBasePoint = true;
            opts.UserInputControls |= AcEd.UserInputControls.AnyBlankTerminatesInput | AcEd.UserInputControls.NullResponseAccepted;

            AcEd.PromptPointResult result = prompts.AcquirePoint(opts);
            if (result.Status == AcEd.PromptStatus.OK) {
                if (m_endPt != result.Value) {
                    m_endPt = result.Value;
                    samplerStatus = AcEd.SamplerStatus.OK;
                }
            }

            return samplerStatus;
        }
    }
}
