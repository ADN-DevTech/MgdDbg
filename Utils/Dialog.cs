
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MgdDbg.Utils
{
    public class Dialog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="defExtension"> .ext </param>
        /// <param name="title">title of the dialog</param>
        /// <param name="filterString"> Ext files (*.ext)|*.ext </param>
        /// <returns></returns>
        public static string OpenFileDialog (string defExtension, string title, string filterString)
        {
            string fileName = string.Empty;
            System.Windows.Forms.OpenFileDialog dbox = new System.Windows.Forms.OpenFileDialog();
            dbox.AddExtension = true;
            dbox.DefaultExt = defExtension;
            dbox.Filter = filterString;
            dbox.Title = title;
            dbox.CheckFileExists = true;
            dbox.Multiselect = false;
            if (dbox.ShowDialog() == DialogResult.OK) {
                fileName = dbox.FileName;
            }
            return fileName;
        }

    }
}
