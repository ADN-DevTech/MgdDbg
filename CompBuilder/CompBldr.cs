
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
using Autodesk.AutoCAD.Geometry;

namespace MgdDbg
{
	/// <summary>
	/// This class is intended to ease the creation of multiple entities that
	/// are supposed to represent a "Component".  Basically, anything you would
	/// normally use a BlockDef for.  It will manage a transaction and some transforms
	/// to make it easier to batch produce several related entities.
	/// </summary>
	
	public abstract class CompBldr : TransactionHelper
	{
	        // member variables
        protected BlockTableRecord      m_blkRec = null;
        protected ObjectId              m_blkDefId;
        protected ObjectIdCollection    m_objIds = new ObjectIdCollection();
        protected Stack                 m_xformStack = new Stack();
        protected bool                  m_ignoreXform = false;

		public
		CompBldr(Database db)
		:   base(db)
		{
                // init stack with identity matrix
            m_xformStack.Push(Matrix3d.Identity);
		}

        protected abstract void SetCurrentBlkTblRec();
        
        public ObjectId
        BlockDefId
        {
            get {
                Debug.Assert(m_blkDefId.IsNull == false);   // shouldn't be calling this if things haven't been started!
                return m_blkDefId;
            }
        }
        
        public override void
        Start()
        {
                // call base class to start a transaction
            base.Start();
            
                // call derived class to supply the block that we are drawing into.
            SetCurrentBlkTblRec();
            if (m_blkRec == null)
                throw (new System.Exception("No BlockTableRecord was set up!"));
                
            m_blkDefId = m_blkRec.ObjectId;
        }

        public virtual void
        Reset()
        {
                // reset the stack of xforms
            m_xformStack.Clear();

                // init stack with identity matrix
            m_xformStack.Push(Matrix3d.Identity);

            m_objIds.Clear();
        }


        public virtual void
        AddToDb(Entity ent)
        {
            Debug.Assert((m_db != null) && (m_trans != null) && (m_blkRec != null));

                // transform if not the identity matrix
            if ((m_ignoreXform == false) && (m_xformStack.Count > 1))
                ent.TransformBy(CurrentXform());

                // add to the current block tbl record and add to the transaction
            m_blkRec.AppendEntity(ent);

            m_objIds.Add(ent.ObjectId);   // keep track of everything we added just in case
            m_trans.AddNewlyCreatedDBObject(ent, true);
        }


        public virtual void
        AddToDbNoXform(Entity ent)
        {
            m_ignoreXform = true;
            AddToDb(ent);
            m_ignoreXform = false;
        }
        

        public virtual void
        SetToDefaultProps(Entity ent)
        {
            ent.SetDatabaseDefaults(m_db);
        }

 
        public void
        PushXform(Matrix3d mat)
        {
            Debug.Assert(m_xformStack.Count != 0);
        
            m_xformStack.Push(mat * CurrentXform());
        }

 
        public void
        PopXform()
        {
            Debug.Assert(m_xformStack.Count > 1);  // this means that someone has popped to many times - bad bad bad
            m_xformStack.Pop();
        }


        public Matrix3d
        CurrentXform()
        {
            Debug.Assert(m_xformStack.Count != 0);
            return (Matrix3d)m_xformStack.Peek();
        }
    }
}


/*
Acad::ErrorStatus
AecRcpCompBldr::addPolyline(AcDbPolyline* newPline, const AcGePoint2dArray& pts, bool isClosed)
{
        // has to be a new polyine!
    if ((newPline == NULL) || (newPline->objectId().isNull() == false)) {
        AEC_ASSERT(0);
        return Acad::eInvalidInput;
    }
    
    Acad::ErrorStatus es;

    int len = pts.length();
    for (int i=0; i<len; i++) {
        es = newPline->addVertexAt(i, pts[i]);
        AEC_ASSERT(es == Acad::eOk);
    }
    newPline->setClosed(isClosed);

    return addToDb(newPline);
}



Acad::ErrorStatus
AecRcpCompBldr::addPolyline(AcDbPolyline* newPline, const AcGePoint2dArray& pts, const AcGeDoubleArray& bulges, bool isClosed)
{
        // has to be a new polyine!
    if ((newPline == NULL) || (newPline->objectId().isNull() == false)) {
        AEC_ASSERT(0);
        return Acad::eInvalidInput;
    }

    AEC_ASSERT(pts.length() == bulges.length());
    
    Acad::ErrorStatus es;

    int len = pts.length();
    for (int i=0; i<len; i++) {
        es = newPline->addVertexAt(i, pts[i], bulges[i]);
        AEC_ASSERT(es == Acad::eOk);
    }
    newPline->setClosed(isClosed);

    return addToDb(newPline);
}



Acad::ErrorStatus
AecRcpCompBldr::addHatchToPrevBoundary(AcDbHatch* newHatch, bool makeAssoc)
{
        // got to have a boundary to set
    if ((newHatch == NULL) || (m_objIds.isEmpty() == true) || (newHatch->objectId().isNull() == false)) {
        AEC_ASSERT(0);
        return Acad::eInvalidInput;
    }

        // need to xform the hatch to the right plane before it tries to evaluate
        // the pattern.
    if (m_xformStack.length() > 1)
        newHatch->transformBy(*curXform());

        // pline is added and has an objectId, now let's add the hatch
    AcDbObjectIdArray loopIds;
    loopIds.append(m_objIds.last());

    Acad::ErrorStatus es;
    es = newHatch->setAssociative(makeAssoc);
    AEC_ASSERT(es == Acad::eOk);

    es = newHatch->appendLoop(AcDbHatch::kExternal, loopIds);
    AEC_ASSERT(es == Acad::eOk);

    if (es == Acad::eOk) {
        es = newHatch->evaluateHatch();

        if (es == Acad::eOk) {
            es = addToDbNoXform(newHatch);
            if (es == Acad::eOk) {
                if (makeAssoc)
                    establishAssociativity(newHatch, loopIds);

                sendToBack(newHatch->objectId(), loopIds);
            }
        }
    }

    if (es != Acad::eOk) {
        AecRmCString str, tmpStr;
        str.LoadString(GetAecRcpBaseAppName(), AECRB_STR_ERROR_CREATE_HATCH);
        acutPrintf(str, newHatch->patternName(), Aec::doubleToStr(newHatch->patternScale(), tmpStr, Aec::kDecimal));
        delete newHatch;
    }

    return es;
}


Acad::ErrorStatus
AecRcpCompBldr::addHatchToBoundaries(AcDbHatch* newHatch, const AcDbObjectIdArray& boundaryIds,
                                   bool makeAssoc, AcDbHatch::HatchStyle hstyle) {
        // got to have a boundary to set
    if ((newHatch == NULL) || (boundaryIds.isEmpty() == true) || (newHatch->objectId().isNull() == false)) {
        AEC_ASSERT(0);
        return Acad::eInvalidInput;
    }

        // need to xform the hatch to the right plane before it tries to evaluate
        // the pattern.
    if (m_xformStack.length() > 1)
        newHatch->transformBy(*curXform());

    Acad::ErrorStatus es;
    es = newHatch->setAssociative(makeAssoc);
    AEC_ASSERT(es == Acad::eOk);

    es = newHatch->setHatchStyle(hstyle);
    AEC_ASSERT(es == Acad::eOk);

        // add each boundary as its own loop
    AcDbObjectIdArray loopIds;
    int len = boundaryIds.length();
    for (int i=0; i<len; i++) {
        loopIds.append(boundaryIds[i]);
        es = newHatch->appendLoop(AcDbHatch::kDefault, loopIds);
        AEC_ASSERT(es == Acad::eOk);

        loopIds.setLogicalLength(0); //clear array
    }

    if (es == Acad::eOk) {
        es = newHatch->evaluateHatch();

        if (es == Acad::eOk) {
            es = addToDbNoXform(newHatch);
            if (es == Acad::eOk) {
                if (makeAssoc)
                    establishAssociativity(newHatch, boundaryIds);

                sendToBack(newHatch->objectId(), boundaryIds);
            }
        }
    }

    if (es != Acad::eOk) {
        AecRmCString str, tmpStr;
        str.LoadString(GetAecRcpBaseAppName(), AECRB_STR_ERROR_CREATE_HATCH);
        acutPrintf(str, newHatch->patternName(), Aec::doubleToStr(newHatch->patternScale(), tmpStr, Aec::kDecimal));
        delete newHatch;
    }

    return es;
}


Acad::ErrorStatus
AecRcpCompBldr::addHatchWithBoundary(AcDbHatch* newHatch, const AcGePoint2dArray& pts, const AcGeDoubleArray& bulges)
{
        // got to have a boundary to set
    if ((newHatch == NULL) || (newHatch->objectId().isNull() == false)) {
        AEC_ASSERT(0);
        return Acad::eInvalidInput;
    }

    Acad::ErrorStatus es;

    es = newHatch->setAssociative(false);
    AEC_ASSERT(es == Acad::eOk);

    es = newHatch->appendLoop(AcDbHatch::kDefault, pts, bulges);
    AEC_ASSERT(es == Acad::eOk);

    if (es == Acad::eOk) {
        es = newHatch->evaluateHatch();

        if (es == Acad::eOk) {
            es = addToDb(newHatch);

            AcDbObjectIdArray bogusBoundaryIds;
            sendToBack(newHatch->objectId(), bogusBoundaryIds);
        }
    }

    if (es != Acad::eOk) {
        AecRmCString str, tmpStr;
        str.LoadString(GetAecRcpBaseAppName(), AECRB_STR_ERROR_CREATE_HATCH);
        acutPrintf(str, newHatch->patternName(), Aec::doubleToStr(newHatch->patternScale(), tmpStr, Aec::kDecimal));
        delete newHatch;
    }

    return es;
}


Acad::ErrorStatus
AecRcpCompBldr::establishAssociativity(AcDbHatch* hatch, const AcDbObjectIdArray& boundaryIds)
{
    AEC_ASSERT(hatch->objectId().isNull() == false);    // have to add to database first!

    AcDbObject* obj;
    Acad::ErrorStatus es;

    int len = boundaryIds.length();
    for (int i=0; i<len; i++) {
        es = Aec::openInTrans(obj, boundaryIds[i], AcDb::kForWrite);
        if (es == Acad::eOk) {
            obj->addPersistentReactor(hatch->objectId());
        }
        else {
            AecRmCString  strMsg;
            strMsg.LoadString(GetAecRcpBaseAppName(),AECRB_STR_ERROR_CREATE_HATCH_ASSOC);
            acutPrintf(strMsg);
            return es;
        }
    }

    return Acad::eOk;
}


Acad::ErrorStatus
AecRcpCompBldr::sendToBack(const AcDbObjectId& backEntId, const AcDbObjectIdArray& topEntIds)
{
    if (AecRcpPreferences::useDrawOrder() == false)
        return Acad::eOk;

    Acad::ErrorStatus es;

    AcDbSortentsTable* sortTbl;
    es = m_blkRec->getSortentsTable(sortTbl, AcDb::kForWrite, true);
    AEC_ASSERT(es == Acad::eOk);

    if (es == Acad::eOk) {
        if (topEntIds.isEmpty()) {
            AcDbObjectIdArray tmpIds;
            tmpIds.append(backEntId);
            es = sortTbl->moveToBottom(tmpIds);
            AEC_ASSERT(es == Acad::eOk);
        }
        else {
            es = sortTbl->moveAbove(topEntIds, backEntId);
            AEC_ASSERT(es == Acad::eOk);
        }
        
        sortTbl->close();

            // if we aren't already putting this sortents table on the block record
            // that this hatch belongs to, then we need to "wake up" the layout this
            // block will eventually go into. Until there is a SORTENTS dictionary on the
            // ModelSpace or PaperSpace block def, no Draworder events happen.
        // TBD: doesn't appear to help the problem!
        /*if (m_blkRec->isLayout() == false) {
            AcDbBlockTableRecord* curSpace = Aec::openCurrentSpaceBlock(AcDb::kForWrite, backEntId.database());
            if (curSpace) {
                es = curSpace->getSortentsTable(sortTbl, AcDb::kForWrite, true);
                if (es == Acad::eOk)
                    sortTbl->close();

                curSpace->close();
            }
        }*/
/*    }

    return es;
}
	}
}*/
