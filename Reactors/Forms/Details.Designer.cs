namespace MgdDbg.Reactors.Forms {
    partial class Details {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_listViewMap = new System.Windows.Forms.ListView();
            this.m_idMapkeyCol = new System.Windows.Forms.ColumnHeader();
            this.m_idMapValCol = new System.Windows.Forms.ColumnHeader();
            this.m_listViewMapItem = new System.Windows.Forms.ListView();
            this.m_objCol = new System.Windows.Forms.ColumnHeader();
            this.m_keyCol = new System.Windows.Forms.ColumnHeader();
            this.m_valCol = new System.Windows.Forms.ColumnHeader();
            this.m_clonedCol = new System.Windows.Forms.ColumnHeader();
            this.m_primCol = new System.Windows.Forms.ColumnHeader();
            this.m_ownerCol = new System.Windows.Forms.ColumnHeader();
            this.m_bnOk = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 23);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(818, 353);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_listViewMap);
            this.tabPage1.Controls.Add(this.m_listViewMapItem);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(810, 327);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Id Mapping";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_listViewMap
            // 
            this.m_listViewMap.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.m_listViewMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listViewMap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_idMapkeyCol,
            this.m_idMapValCol});
            this.m_listViewMap.FullRowSelect = true;
            this.m_listViewMap.GridLines = true;
            this.m_listViewMap.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listViewMap.HideSelection = false;
            this.m_listViewMap.LabelWrap = false;
            this.m_listViewMap.Location = new System.Drawing.Point(6, 6);
            this.m_listViewMap.Name = "m_listViewMap";
            this.m_listViewMap.Size = new System.Drawing.Size(242, 315);
            this.m_listViewMap.TabIndex = 2;
            this.m_listViewMap.UseCompatibleStateImageBehavior = false;
            this.m_listViewMap.View = System.Windows.Forms.View.Details;
            // 
            // m_idMapkeyCol
            // 
            this.m_idMapkeyCol.Text = "Key";
            this.m_idMapkeyCol.Width = 150;
            // 
            // m_idMapValCol
            // 
            this.m_idMapValCol.Text = "Value";
            this.m_idMapValCol.Width = 300;
            // 
            // m_listViewMapItem
            // 
            this.m_listViewMapItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listViewMapItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_objCol,
            this.m_keyCol,
            this.m_valCol,
            this.m_clonedCol,
            this.m_primCol,
            this.m_ownerCol});
            this.m_listViewMapItem.FullRowSelect = true;
            this.m_listViewMapItem.GridLines = true;
            this.m_listViewMapItem.Location = new System.Drawing.Point(266, 6);
            this.m_listViewMapItem.Name = "m_listViewMapItem";
            this.m_listViewMapItem.Size = new System.Drawing.Size(538, 315);
            this.m_listViewMapItem.TabIndex = 1;
            this.m_listViewMapItem.UseCompatibleStateImageBehavior = false;
            this.m_listViewMapItem.View = System.Windows.Forms.View.Details;
            this.m_listViewMapItem.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_listViewMapItem_ColumnClick);
            // 
            // m_objCol
            // 
            this.m_objCol.Text = "<Object     Handle>";
            this.m_objCol.Width = 136;
            // 
            // m_keyCol
            // 
            this.m_keyCol.Text = "Key";
            this.m_keyCol.Width = 79;
            // 
            // m_valCol
            // 
            this.m_valCol.Text = "Value";
            this.m_valCol.Width = 116;
            // 
            // m_clonedCol
            // 
            this.m_clonedCol.Text = "Is Cloned";
            this.m_clonedCol.Width = 62;
            // 
            // m_primCol
            // 
            this.m_primCol.Text = "Is Primary";
            this.m_primCol.Width = 63;
            // 
            // m_ownerCol
            // 
            this.m_ownerCol.Text = "Is OwnerXlated";
            this.m_ownerCol.Width = 84;
            // 
            // m_bnOk
            // 
            this.m_bnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_bnOk.Location = new System.Drawing.Point(371, 400);
            this.m_bnOk.Name = "m_bnOk";
            this.m_bnOk.Size = new System.Drawing.Size(75, 23);
            this.m_bnOk.TabIndex = 1;
            this.m_bnOk.Text = "OK";
            this.m_bnOk.UseVisualStyleBackColor = true;
            // 
            // Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 444);
            this.Controls.Add(this.m_bnOk);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Details";
            this.Text = "Details";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button m_bnOk;
        private System.Windows.Forms.ListView m_listViewMapItem;
        private System.Windows.Forms.ColumnHeader m_keyCol;
        private System.Windows.Forms.ColumnHeader m_valCol;
        private System.Windows.Forms.ColumnHeader m_clonedCol;
        private System.Windows.Forms.ColumnHeader m_primCol;
        private System.Windows.Forms.ColumnHeader m_ownerCol;
        private System.Windows.Forms.ListView m_listViewMap;
        private System.Windows.Forms.ColumnHeader m_idMapkeyCol;
        private System.Windows.Forms.ColumnHeader m_idMapValCol;
        private System.Windows.Forms.ColumnHeader m_objCol;
    }
}