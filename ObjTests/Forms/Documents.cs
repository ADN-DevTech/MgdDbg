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
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using AcRx = Autodesk.AutoCAD.Runtime;

namespace MgdDbg.ObjTests.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Documents : Form
    {
        private DocumentCollection m_docs = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
        private Document m_doc;

        /// <summary>
        /// 
        /// </summary>
        public Document
        Document
        {
            get
            {
                return m_doc;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Documents ()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        #region ComboBox Item

        /// <summary>
        /// 
        /// </summary>
        public class ComboBoxItem
        {
            private Document m_doc;

            public ComboBoxItem (Document doc)
            {
                m_doc = doc;
            }

            public Document
            Value
            {
                get
                {
                    return m_doc;
                }
            }

            public override string ToString ()
            {
                return Path.GetFileName(m_doc.Name);
            }

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void
        InitializeComboBox ()
        {
            IEnumerator iter = m_docs.GetEnumerator();
            
            while (iter.MoveNext()) {
                Document doc = iter.Current as Document;
                ComboBoxItem item = new ComboBoxItem(doc);
                m_cmbBox.Items.Add(item);
            }

            if (m_cmbBox.Items.Count == 0)
                throw new AcRx.Exception(AcRx.ErrorStatus.FileNotFound);
            m_cmbBox.SelectedIndex = 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cmbBox_SelectedIndexChanged (object sender, EventArgs e)
        {
            ComboBoxItem item = m_cmbBox.SelectedItem as ComboBoxItem;
            m_doc = item.Value;
        }
    }
}