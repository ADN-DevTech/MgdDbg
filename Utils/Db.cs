
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
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace MgdDbg.Utils
{
	/// <summary>
	/// Summary description for Db.
	/// </summary>
	public class Db
	{
		public Db()
		{
		}
		
		/// <summary>
		/// shortcut for getting the current DWG's database
		/// </summary>
		/// <returns>Database for the current drawing</returns>
		
		public static Database
		GetCurDwg()
		{
            Database db = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;
            return db;
		}
		
		/// <summary>
		/// Is Paper Space active in the given database?
		/// </summary>
		/// <param name="db">Specific database to use</param>
		/// <returns></returns>
		
		public static bool
		IsPaperSpace(Database db)
		{
		    Debug.Assert(db != null);
		    
		    if (db.TileMode)
		        return false;
		        
		    Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            if (db.PaperSpaceVportId == ed.CurrentViewportObjectId)
		        return true;
		        
		    return false;
		}
				
		 /// <summary>
        /// Figure out the current UCS matrix for the given database.  If
        /// PaperSpace is active, it will return the UCS for PaperSpace.
        /// Otherwise, it will return the UCS for the current viewport in 
        /// ModelSpace.
        /// </summary>
        /// <param name="db">Specific database to use</param>
        /// <returns>UCS Matrix for the specified database</returns>

        public static Matrix3d
        GetUcsMatrix(Database db)
        {
            Debug.Assert(db != null);
		    
            Point3d origin;
            Vector3d xAxis, yAxis, zAxis;

            if (IsPaperSpace(db)) {
                origin = db.Pucsorg;
                xAxis  = db.Pucsxdir;
                yAxis  = db.Pucsydir;
            }
            else {
                origin = db.Ucsorg;
                xAxis  = db.Ucsxdir;
                yAxis  = db.Ucsydir;
            }

            zAxis = xAxis.CrossProduct(yAxis);
            
            return Matrix3d.AlignCoordinateSystem(Utils.Ge.kOrigin, Utils.Ge.kXAxis, Utils.Ge.kYAxis, Utils.Ge.kZAxis, origin, xAxis, yAxis, zAxis);
        }
                
        /// <summary>
        /// Get the UCS Z Axis for the given database
        /// </summary>
        /// <param name="db">Specific database to use</param>
        /// <returns>UCS Z Axis</returns>
        
        public static Vector3d
        GetUcsZAxis(Database db)
        {
            Matrix3d m = GetUcsMatrix(db);

            return m.CoordinateSystem3d.Zaxis;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Vector3d
        GetUcsXAxis (Database db)
        {
            Matrix3d m = GetUcsMatrix(db);

            return m.CoordinateSystem3d.Xaxis;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Vector3d
        GetUcsYAxis (Database db)
        {
            Matrix3d m = GetUcsMatrix(db);

            return m.CoordinateSystem3d.Yaxis;
        }
        
        /// <summary>
        /// Get the Plane that is defined by the current UCS
        /// </summary>
        /// <param name="db">Database to use</param>
        /// <returns>Plane defined by the current UCS</returns>
        
        public static Plane
        GetUcsPlane(Database db)
        {
            Matrix3d m = GetUcsMatrix(db);
            CoordinateSystem3d coordSys = m.CoordinateSystem3d;
            
            return new Plane(coordSys.Origin, coordSys.Xaxis, coordSys.Yaxis);
        }

        
        /// <summary>
        /// Get the Matrix that is the Xform between UCS and WCS Origin.  This is useful
        /// for operations like creating a block definition.  For those cases you want the
        /// origin of the block to be in a reasonable spot.
        /// </summary>
        /// <param name="wcsBasePt">Base point to use as the origin</param>
        /// <param name="db">Specific database to use</param>
        /// <returns>Xform between UCS and WCS Origin</returns>
        
        public static Matrix3d
        GetUcsToWcsOriginMatrix(Point3d wcsBasePt, Database db)
        {
            Matrix3d m = GetUcsMatrix(db);

		    Point3d origin = m.CoordinateSystem3d.Origin;
		    origin += wcsBasePt.GetAsVector();

	        m = Matrix3d.AlignCoordinateSystem(origin,
	                            m.CoordinateSystem3d.Xaxis,
	                            m.CoordinateSystem3d.Yaxis,
	                            m.CoordinateSystem3d.Zaxis,
	                            Utils.Ge.kOrigin, Utils.Ge.kXAxis, Utils.Ge.kYAxis, Utils.Ge.kZAxis);
	                            
	        return m;
        }
        
        /// <summary>
        /// Get the X-Axis relative to an entities ECS (In other words, what it considers the
        /// X-Axis.  This is crucial for Entities like Dimensions and DBPoints.  The X-Axis is
        /// determined by the Arbitrary Axis algorithm.
        /// </summary>
        /// <param name="ecsZAxis">The normal vector of the entity</param>
        /// <returns>The X-Axis for this ECS</returns>
        
        public static Vector3d
        GetEcsXAxis(Vector3d ecsZAxis)
        {
            Matrix3d arbMat = GetEcsToWcsMatrix(Utils.Ge.kOrigin, ecsZAxis);

            return arbMat.CoordinateSystem3d.Xaxis;
        }

        /// <summary>
        /// This is the AutoCAD Arbitrary Axis Algorithm.  Given a normal vector,
        /// establish the ECS matrix that corresponds.
        /// </summary>
        /// <param name="origin">Origin point</param>
        /// <param name="zAxis">Normal vector of the entity</param>
        /// <returns>ECS Matrix</returns>
        
        public static Matrix3d
        GetEcsToWcsMatrix(Point3d origin, Vector3d zAxis)
        {
            const double kArbBound = 0.015625;         //  1/64th
            
                // short circuit if in WCS already
            if (zAxis == Utils.Ge.kZAxis) {
                return Matrix3d.Identity;
            }

            Vector3d xAxis, yAxis;

            Debug.Assert(zAxis.IsUnitLength(Tolerance.Global));

            if ((Math.Abs(zAxis.X) < kArbBound) && (Math.Abs(zAxis.Y) < kArbBound))
                xAxis = Utils.Ge.kYAxis.CrossProduct(zAxis);
            else
                xAxis = Utils.Ge.kZAxis.CrossProduct(zAxis);

            xAxis = xAxis.GetNormal();
            yAxis = zAxis.CrossProduct(xAxis).GetNormal();

            return Matrix3d.AlignCoordinateSystem(Utils.Ge.kOrigin, Utils.Ge.kXAxis, Utils.Ge.kYAxis, Utils.Ge.kZAxis,
                                        origin, xAxis, yAxis, zAxis);
        }
        
        /// <summary>
        /// Get a transformed copy of a point from UCS to WCS
        /// </summary>
        /// <param name="pt">Point to transform</param>
        /// <returns>Transformed copy of point</returns>
        
        public static Point3d
        UcsToWcs(Point3d pt)
        {
            Matrix3d m = GetUcsMatrix(GetCurDwg());
            
            return pt.TransformBy(m);
        }
                
        /// <summary>
        /// Get a transformed copy of a vector from UCS to WCS
        /// </summary>
        /// <param name="vec">Vector to transform</param>
        /// <returns>Transformed copy of vector</returns>
        
        public static Vector3d
        UcsToWcs(Vector3d vec)
        {
            Matrix3d m = GetUcsMatrix(GetCurDwg());
            
            return vec.TransformBy(m);
        }
                        
        /// <summary>
        /// Get a transformed copy of a point from WCS to UCS
        /// </summary>
        /// <param name="pt">Point to transform</param>
        /// <returns>Transformed copy of point</returns>
        
        public static Point3d
        WcsToUcs(Point3d pt)
        {
            Matrix3d m = GetUcsMatrix(GetCurDwg());
            
            return pt.TransformBy(m.Inverse());
        }
        		
		/// <summary>
		/// Transform an Entity from UCS to WCS
		/// </summary>
		/// <param name="ent">Entity to transform</param>
		/// <param name="db">Database the entity belongs to (or will belong to)</param>
		
		public static void
		TransformToWcs(Entity ent, Database db)
		{
            Debug.Assert(ent != null);
            Debug.Assert(db != null);
            Debug.Assert(ent.IsWriteEnabled);
            
            Matrix3d m = GetUcsMatrix(db);
            ent.TransformBy(m);
		}
		
		/// <summary>
		/// Transform a collection of Entities from UCS to WCS
		/// </summary>
		/// <param name="ents">Entities to transform</param>
		/// <param name="db">Database the entities belong to (or will belong to)</param>
		
		public static void
		TransformToWcs(DBObjectCollection ents, Database db)
		{
            Debug.Assert(ents != null);
            Debug.Assert(db != null);
            
            Matrix3d m = GetUcsMatrix(db);
            
            foreach (Entity tmpEnt in ents) {
                Debug.Assert(tmpEnt.IsWriteEnabled);
                tmpEnt.TransformBy(m);
            }
        }
        
        /// <summary>
        /// Clone and transform a collection of entities.  NOTE: this function
        /// is not operational yet because it can't iterate over the idMap to
        /// accomplish the Xform.
        /// </summary>
        /// <param name="db">database to perform the operation on</param>
        /// <param name="entsToClone">set of objects to clone</param>
        /// <param name="ownerBlockId">owner of the new objects</param>
        /// <param name="xformMat">transformation to apply</param>
        
        public static void
        CloneAndXformObjects(Database db, ObjectIdCollection entsToClone,
                    ObjectId ownerBlockId, Matrix3d xformMat)
        {
            using (TransactionHelper trHlp = new TransactionHelper(db)) {
                trHlp.Start();
                
                IdMapping idMap = new IdMapping();
                db.DeepCloneObjects(entsToClone, ownerBlockId, idMap, false);
                
                    // walk through all the cloned objects and Xform any of the entities
                foreach (IdPair idpair in idMap) {
                    if (idpair.IsCloned) {
                        DBObject clonedObj = trHlp.Transaction.GetObject(idpair.Value, OpenMode.ForWrite);
                        Entity clonedEnt = clonedObj as Entity;
                        if (clonedEnt != null)
                            clonedEnt.TransformBy(xformMat);
                    }
                }
                
                trHlp.Commit();
            }
        }
       
        public static void
        MakePointEnt(Point3d pt, int colorIndex, Transaction tr, Database db)
        {
            short mode = (short)AcadApp.GetSystemVariable("pdmode");
            if (mode == 0)
                AcadApp.SetSystemVariable("pdmode", 99);

            using (DBPoint dbPt = new DBPoint(pt)) {
                dbPt.ColorIndex = colorIndex;
                MgdDbg.Utils.SymTbl.AddToCurrentSpace(dbPt, db, tr);
            }
        }

        public static void
        MakeRayEnt(Point3d basePt, Vector3d unitDir, int colorIndex, Transaction tr, Database db)
        {
            if (unitDir.IsZeroLength()) {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                ed.WriteMessage("\nSkipping zero length vector (colorIndex = {0:d})", colorIndex);
                return;
            }

            using (Ray ray = new Ray()) {
                ray.ColorIndex = colorIndex;
                ray.BasePoint = basePt;
                ray.UnitDir = unitDir;
                MgdDbg.Utils.SymTbl.AddToCurrentSpace(ray, db, tr);
            }
        }
        
        public static void
        MakeExtentsBlock(BoundBlock3d ext, int colorIndex, Database db)
        {
            Point3d minPt = ext.GetMinimumPoint();
            Point3d maxPt = ext.GetMaximumPoint();
            
            double deltaX = Math.Abs(maxPt.X - minPt.X);
            double deltaY = Math.Abs(maxPt.Y - minPt.Y);
            double deltaZ = Math.Abs(maxPt.Z - minPt.Z);

            Point3d[] pts = new Point3d[8];
            
            pts[0] = minPt;
            pts[6] = maxPt;

                // make bottom face
            pts[1] = new Point3d(pts[0].X + deltaX, pts[0].Y, pts[0].Z);
            pts[2] = new Point3d(pts[1].X, pts[1].Y + deltaY, pts[1].Z);
            pts[3] = new Point3d(pts[0].X, pts[0].Y + deltaY, pts[0].Z);

                // project up by Z
            pts[4] = new Point3d(pts[0].X, pts[0].Y, pts[0].Z + deltaZ);
            pts[5] = new Point3d(pts[1].X, pts[1].Y, pts[1].Z + deltaZ);
            pts[7] = new Point3d(pts[3].X, pts[3].Y, pts[3].Z + deltaZ);

            Vector3d offset = minPt.GetAsVector();

                // move points so that they are centered at WCS origin
                // for block creation.  Express everything in WCS since
                // that is what Entity.Extents works in.
            for (int i=0; i<pts.Length; i++) {
                pts[i] -= offset;
            }

            DBObjectCollection faceEnts = new DBObjectCollection();
            faceEnts.Add(new Face(pts[0], pts[1], pts[2], pts[3], true, true, true, true));  // bottom face
            faceEnts.Add(new Face(pts[4], pts[5], pts[6], pts[7], true, true, true, true));  // top face
            faceEnts.Add(new Face(pts[0], pts[1], pts[5], pts[4], true, true, true, true));  // front face
            faceEnts.Add(new Face(pts[1], pts[2], pts[6], pts[5], true, true, true, true));  // right side face
            faceEnts.Add(new Face(pts[2], pts[3], pts[7], pts[6], true, true, true, true));  // back side face
            faceEnts.Add(new Face(pts[3], pts[0], pts[4], pts[7], true, true, true, true));  // left side face
            
            CompBldrAnonBlkDef compBldr = new CompBldrAnonBlkDef(db);
            compBldr.Start();
            
            foreach (Entity ent in faceEnts) {
                compBldr.SetToDefaultProps(ent);
                compBldr.AddToDb(ent);
            }
            
            compBldr.Commit();
            
            BlockReference blkRef = new BlockReference(minPt, compBldr.BlockDefId);
            blkRef.ColorIndex = colorIndex;
            Utils.SymTbl.AddToCurrentSpaceAndClose(blkRef, compBldr.Database);
        }

        /// <summary>
        /// Converts a string value to a Handle object.
        /// </summary>
        /// <param name="strHandle"></param>
        /// <returns></returns>
        public static Handle
        StringToHandle (String strHandle)
        {
            Handle handle = new Handle();

            try
            {
                Int64 nHandle = Convert.ToInt64(strHandle, 16);
                handle = new Handle(nHandle);
            }
            catch (System.FormatException)
            {
            }
            return handle;
        }

        /// <summary>
        /// Gets the ObjectId that corresponds to the handle.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static ObjectId
        HandleToObjectId (Database db, Handle h)
        {
            ObjectId id = ObjectId.Null;
            try
            {
                id = db.GetObjectId(false, h, 0);
            }
            catch (Autodesk.AutoCAD.Runtime.Exception x)
            {
                if (x.ErrorStatus != Autodesk.AutoCAD.Runtime.ErrorStatus.UnknownHandle)
                {
                    throw x;
                }
            }
            return id;
        }
    }

}
