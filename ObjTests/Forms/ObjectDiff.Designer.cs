namespace MgdDbg.Test
{
    partial class ObjectDiff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
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
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container();
            this.m_treeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAsComparerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAsCompareeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_gpBxDiff = new System.Windows.Forms.GroupBox();
            this.m_compareBx2 = new System.Windows.Forms.ListBox();
            this.m_labelVs = new System.Windows.Forms.Label();
            this.m_compareBx1 = new System.Windows.Forms.ListBox();
            this.m_btmCompare = new System.Windows.Forms.Button();
            this.m_listView = new System.Windows.Forms.ListView();
            this.m_lvCol_label = new System.Windows.Forms.ColumnHeader();
            this.m_lvCol_value1 = new System.Windows.Forms.ColumnHeader();
            this.m_lvCol_value2 = new System.Windows.Forms.ColumnHeader();
            this.m_btnEntChoose = new System.Windows.Forms.Button();
            this.m_labelObjs = new System.Windows.Forms.Label();
            this.m_gpBxSelect = new System.Windows.Forms.GroupBox();
            this.m_labelOr = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.m_gpBxDiff.SuspendLayout();
            this.m_gpBxSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_treeView
            // 
            this.m_treeView.AllowDrop = true;
            this.m_treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_treeView.ContextMenuStrip = this.contextMenuStrip1;
            this.m_treeView.Location = new System.Drawing.Point(41, 26);
            this.m_treeView.Name = "m_treeView";
            this.m_treeView.Size = new System.Drawing.Size(275, 445);
            this.m_treeView.TabIndex = 0;
            this.m_treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_treeView_MouseClick);
            this.m_treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_treeView_AfterSelect);
            this.m_treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_treeView_ItemDrag);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAsComparerToolStripMenuItem,
            this.addAsCompareeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 48);
            // 
            // addAsComparerToolStripMenuItem
            // 
            this.addAsComparerToolStripMenuItem.Name = "addAsComparerToolStripMenuItem";
            this.addAsComparerToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.addAsComparerToolStripMenuItem.Text = "Add as Comparer";
            this.addAsComparerToolStripMenuItem.Click += new System.EventHandler(this.addAsComparerToolStripMenuItem_Click);
            // 
            // addAsCompareeToolStripMenuItem
            // 
            this.addAsCompareeToolStripMenuItem.Name = "addAsCompareeToolStripMenuItem";
            this.addAsCompareeToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.addAsCompareeToolStripMenuItem.Text = "Add as Comparee";
            this.addAsCompareeToolStripMenuItem.Click += new System.EventHandler(this.addAsCompareeToolStripMenuItem_Click);
            // 
            // m_gpBxDiff
            // 
            this.m_gpBxDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_gpBxDiff.Controls.Add(this.m_compareBx2);
            this.m_gpBxDiff.Controls.Add(this.m_labelVs);
            this.m_gpBxDiff.Controls.Add(this.m_compareBx1);
            this.m_gpBxDiff.Location = new System.Drawing.Point(12, 534);
            this.m_gpBxDiff.Name = "m_gpBxDiff";
            this.m_gpBxDiff.Size = new System.Drawing.Size(338, 73);
            this.m_gpBxDiff.TabIndex = 4;
            this.m_gpBxDiff.TabStop = false;
            this.m_gpBxDiff.Text = "Diff";
            // 
            // m_compareBx2
            // 
            this.m_compareBx2.AllowDrop = true;
            this.m_compareBx2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_compareBx2.FormattingEnabled = true;
            this.m_compareBx2.ItemHeight = 12;
            this.m_compareBx2.Location = new System.Drawing.Point(192, 15);
            this.m_compareBx2.Name = "m_compareBx2";
            this.m_compareBx2.Size = new System.Drawing.Size(122, 52);
            this.m_compareBx2.TabIndex = 9;
            this.m_compareBx2.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_compareBx2_DragEnter);
            this.m_compareBx2.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_compareBx2_DragDrop);
            // 
            // m_labelVs
            // 
            this.m_labelVs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_labelVs.AutoSize = true;
            this.m_labelVs.Location = new System.Drawing.Point(160, 42);
            this.m_labelVs.Name = "m_labelVs";
            this.m_labelVs.Size = new System.Drawing.Size(22, 13);
            this.m_labelVs.TabIndex = 5;
            this.m_labelVs.Text = "Vs.";
            // 
            // m_compareBx1
            // 
            this.m_compareBx1.AllowDrop = true;
            this.m_compareBx1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_compareBx1.FormattingEnabled = true;
            this.m_compareBx1.ItemHeight = 12;
            this.m_compareBx1.Location = new System.Drawing.Point(39, 15);
            this.m_compareBx1.Name = "m_compareBx1";
            this.m_compareBx1.Size = new System.Drawing.Size(115, 52);
            this.m_compareBx1.TabIndex = 8;
            this.m_compareBx1.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_compareBx1_DragEnter);
            this.m_compareBx1.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_compareBx1_DragDrop);
            // 
            // m_btmCompare
            // 
            this.m_btmCompare.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btmCompare.Location = new System.Drawing.Point(668, 586);
            this.m_btmCompare.Name = "m_btmCompare";
            this.m_btmCompare.Size = new System.Drawing.Size(75, 23);
            this.m_btmCompare.TabIndex = 5;
            this.m_btmCompare.Text = "Compare";
            this.m_btmCompare.UseVisualStyleBackColor = true;
            this.m_btmCompare.Click += new System.EventHandler(this.m_btmCompare_Click);
            // 
            // m_listView
            // 
            this.m_listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listView.AutoArrange = false;
            this.m_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_lvCol_label,
            this.m_lvCol_value1,
            this.m_lvCol_value2});
            this.m_listView.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_listView.FullRowSelect = true;
            this.m_listView.GridLines = true;
            this.m_listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(379, 12);
            this.m_listView.MultiSelect = false;
            this.m_listView.Name = "m_listView";
            this.m_listView.ShowItemToolTips = true;
            this.m_listView.Size = new System.Drawing.Size(646, 568);
            this.m_listView.TabIndex = 6;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            this.m_listView.Click += new System.EventHandler(this.DataItemSelected);
            // 
            // m_lvCol_label
            // 
            this.m_lvCol_label.Text = "Field";
            this.m_lvCol_label.Width = 148;
            // 
            // m_lvCol_value1
            // 
            this.m_lvCol_value1.Text = "Value 1";
            this.m_lvCol_value1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_lvCol_value1.Width = 253;
            // 
            // m_lvCol_value2
            // 
            this.m_lvCol_value2.Text = "Value 2";
            this.m_lvCol_value2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_lvCol_value2.Width = 273;
            // 
            // m_btnEntChoose
            // 
            this.m_btnEntChoose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnEntChoose.Location = new System.Drawing.Point(63, 491);
            this.m_btnEntChoose.Name = "m_btnEntChoose";
            this.m_btnEntChoose.Size = new System.Drawing.Size(229, 23);
            this.m_btnEntChoose.TabIndex = 7;
            this.m_btnEntChoose.Text = "Entities...";
            this.m_btnEntChoose.UseVisualStyleBackColor = true;
            this.m_btnEntChoose.Click += new System.EventHandler(this.m_btnEntChoose_Click);
            // 
            // m_labelObjs
            // 
            this.m_labelObjs.AutoSize = true;
            this.m_labelObjs.Location = new System.Drawing.Point(104, 10);
            this.m_labelObjs.Name = "m_labelObjs";
            this.m_labelObjs.Size = new System.Drawing.Size(46, 13);
            this.m_labelObjs.TabIndex = 8;
            this.m_labelObjs.Text = "Objects:";
            // 
            // m_gpBxSelect
            // 
            this.m_gpBxSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_gpBxSelect.Controls.Add(this.m_labelOr);
            this.m_gpBxSelect.Controls.Add(this.m_treeView);
            this.m_gpBxSelect.Controls.Add(this.m_btnEntChoose);
            this.m_gpBxSelect.Controls.Add(this.m_labelObjs);
            this.m_gpBxSelect.Location = new System.Drawing.Point(10, 3);
            this.m_gpBxSelect.Name = "m_gpBxSelect";
            this.m_gpBxSelect.Size = new System.Drawing.Size(340, 525);
            this.m_gpBxSelect.TabIndex = 10;
            this.m_gpBxSelect.TabStop = false;
            this.m_gpBxSelect.Text = "Select";
            // 
            // m_labelOr
            // 
            this.m_labelOr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_labelOr.AutoSize = true;
            this.m_labelOr.Location = new System.Drawing.Point(162, 475);
            this.m_labelOr.Name = "m_labelOr";
            this.m_labelOr.Size = new System.Drawing.Size(18, 13);
            this.m_labelOr.TabIndex = 10;
            this.m_labelOr.Text = "Or";
            // 
            // ObjectDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 619);
            this.Controls.Add(this.m_gpBxSelect);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_gpBxDiff);
            this.Controls.Add(this.m_btmCompare);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(575, 225);
            this.Name = "ObjectDiff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ObjectDiff";
            this.contextMenuStrip1.ResumeLayout(false);
            this.m_gpBxDiff.ResumeLayout(false);
            this.m_gpBxDiff.PerformLayout();
            this.m_gpBxSelect.ResumeLayout(false);
            this.m_gpBxSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_treeView;
        private System.Windows.Forms.GroupBox m_gpBxDiff;
        private System.Windows.Forms.Label m_labelVs;
        private System.Windows.Forms.Button m_btmCompare;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addAsComparerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAsCompareeToolStripMenuItem;
        protected System.Windows.Forms.ListView m_listView;
        protected System.Windows.Forms.ColumnHeader m_lvCol_label;
        protected System.Windows.Forms.ColumnHeader m_lvCol_value1;
        protected System.Windows.Forms.ColumnHeader m_lvCol_value2;
        private System.Windows.Forms.Button m_btnEntChoose;
        private System.Windows.Forms.ListBox m_compareBx1;
        private System.Windows.Forms.ListBox m_compareBx2;
        private System.Windows.Forms.Label m_labelObjs;
        private System.Windows.Forms.GroupBox m_gpBxSelect;
        private System.Windows.Forms.Label m_labelOr;
    }
}