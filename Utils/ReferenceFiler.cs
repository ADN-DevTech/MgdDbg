
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

using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace MgdDbg.Utils {

    /// <summary>
    /// Simple class to record Object References.  All other data is ignored.
    /// </summary>
        
    class ReferenceFiler : DwgFiler {

            // member data
        public ObjectIdCollection  m_softPointerIds = new ObjectIdCollection();
        public ObjectIdCollection  m_hardPointerIds = new ObjectIdCollection();
        public ObjectIdCollection  m_softOwnershipIds = new ObjectIdCollection();
        public ObjectIdCollection  m_hardOwnershipIds = new ObjectIdCollection();


        public override void ResetFilerStatus()         {}

        public override Autodesk.AutoCAD.Runtime.ErrorStatus FilerStatus
        {
            get { return Autodesk.AutoCAD.Runtime.ErrorStatus.OK; }
            set {}
        }

        public override FilerType   FilerType                       { get { return FilerType.IdFiler; } }
        public override long        Position                        { get { return 0; } }
        public override void        Seek(long offset, int method)   {}

        public override IntPtr   ReadAddress()                      { return new System.IntPtr(); }
        public override byte[]   ReadBinaryChunk()                  { return new byte[0]; }
        public override bool     ReadBoolean()                      { return false; }
        public override byte     ReadByte()                         { return 0; }
        public override void     ReadBytes(byte[] value)            {}
        public override double   ReadDouble()                       { return 0.0; }
        public override Handle   ReadHandle()                       { return new Handle(); }
        public override short    ReadInt16()                        { return 0; }
        public override int      ReadInt32()                        { return 0; }
        public override long     ReadInt64()                        { return 0; }
        public override Point2d  ReadPoint2d()                      { return new Point2d(); }
        public override Point3d  ReadPoint3d()                      { return new Point3d(); }
        public override Scale3d  ReadScale3d()                      { return new Scale3d(); }
        public override string   ReadString()                       { return ""; }
        public override ushort   ReadUInt16()                       { return 0; }
        public override uint     ReadUInt32()                       { return 0; }
        public override ulong    ReadUInt64()                       { return 0; }
        public override Vector2d ReadVector2d()                     { return new Vector2d(); }
        public override Vector3d ReadVector3d()                     { return new Vector3d(); }

        public override ObjectId ReadHardOwnershipId()              { return new ObjectId(); }
        public override ObjectId ReadHardPointerId()                { return new ObjectId(); }
        public override ObjectId ReadSoftOwnershipId()              { return new ObjectId(); }
        public override ObjectId ReadSoftPointerId()                { return new ObjectId(); }

        public override void WriteAddress(IntPtr value)             {}
        public override void WriteBinaryChunk(byte[] chunk)         {}
        public override void WriteBoolean(bool value)               {}
        public override void WriteByte(byte value)                  {}
        public override void WriteBytes(byte[] value)               {}
        public override void WriteDouble(double value)              {}
        public override void WriteHandle(Handle handle)             {}
        public override void WriteInt16(short value)                {}
        public override void WriteInt32(int value)                  {}
        public override void WriteInt64(long value)                 {}
        public override void WritePoint2d(Point2d value)            {}
        public override void WritePoint3d(Point3d value)            {}
        public override void WriteScale3d(Scale3d value)            {}
        public override void WriteString(string value)              {}
        public override void WriteUInt16(ushort value)              {}
        public override void WriteUInt32(uint value)                {}
        public override void WriteUInt64(ulong value)               {}
        public override void WriteVector2d(Vector2d value)          {}
        public override void WriteVector3d(Vector3d value)          {}


        public override void
        WriteHardOwnershipId(ObjectId value)
        {
            if (value.IsNull == false)
                m_hardOwnershipIds.Add(value);
        }

        public override void
        WriteHardPointerId(ObjectId value)
        {
            if (value.IsNull == false)
                m_hardPointerIds.Add(value);
        }

        public override void
        WriteSoftOwnershipId(ObjectId value)
        {
            if (value.IsNull == false)
                m_softOwnershipIds.Add(value);
        }

        public override void
        WriteSoftPointerId(ObjectId value)
        {
            if (value.IsNull == false)
                m_hardPointerIds.Add(value);
        }

        public void
        Reset()
        {
            m_softPointerIds.Clear();
            m_hardPointerIds.Clear();
            m_softOwnershipIds.Clear();
            m_hardOwnershipIds.Clear();
        }
    }
}
