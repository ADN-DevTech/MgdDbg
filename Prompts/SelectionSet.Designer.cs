namespace MgdDbg.ObjTests.Forms {
    partial class SelectionSet {
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
            this.m_bnCancel = new System.Windows.Forms.Button();
            this.m_bnOk = new System.Windows.Forms.Button();
            this.m_puMainSelectionMethod = new System.Windows.Forms.ComboBox();
            this.m_txtMainSelectionMethod = new System.Windows.Forms.Label();
            this.m_grpFlags = new System.Windows.Forms.GroupBox();
            this.m_cbSinglePickInSpace = new System.Windows.Forms.CheckBox();
            this.m_cbSingleOnly = new System.Windows.Forms.CheckBox();
            this.m_cbSelectEverythingInAperature = new System.Windows.Forms.CheckBox();
            this.m_cbRejectPaperSpaceViewport = new System.Windows.Forms.CheckBox();
            this.m_cbForceSubSelections = new System.Windows.Forms.CheckBox();
            this.m_cbAllowSubSelections = new System.Windows.Forms.CheckBox();
            this.m_cbAllowDuplicates = new System.Windows.Forms.CheckBox();
            this.m_cbRejectNonCurrentSpace = new System.Windows.Forms.CheckBox();
            this.m_cbFilterLockedLayers = new System.Windows.Forms.CheckBox();
            this.m_grpMessages = new System.Windows.Forms.GroupBox();
            this.m_cbMsgRemove = new System.Windows.Forms.CheckBox();
            this.m_cbMsgAdd = new System.Windows.Forms.CheckBox();
            this.m_ebMsgRemove = new System.Windows.Forms.TextBox();
            this.m_ebMsgAdd = new System.Windows.Forms.TextBox();
            this.m_grpFilter = new System.Windows.Forms.GroupBox();
            this.m_puClassType = new System.Windows.Forms.ComboBox();
            this.m_cbDoIsKindOfTest = new System.Windows.Forms.CheckBox();
            this.m_txtClassType = new System.Windows.Forms.Label();
            this.m_cbApplyFilter = new System.Windows.Forms.CheckBox();
            this.m_grpEvents = new System.Windows.Forms.GroupBox();
            this.m_cbUnknownEvent = new System.Windows.Forms.CheckBox();
            this.m_cbKeywordEvent = new System.Windows.Forms.CheckBox();
            this.m_grpFlags.SuspendLayout();
            this.m_grpMessages.SuspendLayout();
            this.m_grpFilter.SuspendLayout();
            this.m_grpEvents.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_bnCancel
            // 
            this.m_bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_bnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.m_bnCancel.Location = new System.Drawing.Point(261, 345);
            this.m_bnCancel.Name = "m_bnCancel";
            this.m_bnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_bnCancel.TabIndex = 1;
            this.m_bnCancel.Text = "Cancel";
            this.m_bnCancel.UseVisualStyleBackColor = true;
            // 
            // m_bnOk
            // 
            this.m_bnOk.Location = new System.Drawing.Point(181, 345);
            this.m_bnOk.Name = "m_bnOk";
            this.m_bnOk.Size = new System.Drawing.Size(75, 23);
            this.m_bnOk.TabIndex = 0;
            this.m_bnOk.Text = "< Run Test";
            this.m_bnOk.UseVisualStyleBackColor = true;
            this.m_bnOk.Click += new System.EventHandler(this.OnRunTest);
            // 
            // m_puMainSelectionMethod
            // 
            this.m_puMainSelectionMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_puMainSelectionMethod.FormattingEnabled = true;
            this.m_puMainSelectionMethod.Items.AddRange(new object[] {
            "User",
            "All",
            "Implied",
            "Crossing",
            "Crossing Polygon",
            "Fence",
            "Last",
            "Previous",
            "Window",
            "Window Polygon"});
            this.m_puMainSelectionMethod.Location = new System.Drawing.Point(138, 23);
            this.m_puMainSelectionMethod.MaxDropDownItems = 13;
            this.m_puMainSelectionMethod.Name = "m_puMainSelectionMethod";
            this.m_puMainSelectionMethod.Size = new System.Drawing.Size(219, 21);
            this.m_puMainSelectionMethod.TabIndex = 3;
            this.m_puMainSelectionMethod.SelectedIndexChanged += new System.EventHandler(this.OnSelectionMethodChanged);
            // 
            // m_txtMainSelectionMethod
            // 
            this.m_txtMainSelectionMethod.AutoSize = true;
            this.m_txtMainSelectionMethod.Location = new System.Drawing.Point(13, 26);
            this.m_txtMainSelectionMethod.Name = "m_txtMainSelectionMethod";
            this.m_txtMainSelectionMethod.Size = new System.Drawing.Size(119, 13);
            this.m_txtMainSelectionMethod.TabIndex = 2;
            this.m_txtMainSelectionMethod.Text = "Main Selection Method:";
            // 
            // m_grpFlags
            // 
            this.m_grpFlags.Controls.Add(this.m_cbSinglePickInSpace);
            this.m_grpFlags.Controls.Add(this.m_cbSingleOnly);
            this.m_grpFlags.Controls.Add(this.m_cbSelectEverythingInAperature);
            this.m_grpFlags.Controls.Add(this.m_cbRejectPaperSpaceViewport);
            this.m_grpFlags.Controls.Add(this.m_cbForceSubSelections);
            this.m_grpFlags.Controls.Add(this.m_cbAllowSubSelections);
            this.m_grpFlags.Controls.Add(this.m_cbAllowDuplicates);
            this.m_grpFlags.Controls.Add(this.m_cbRejectNonCurrentSpace);
            this.m_grpFlags.Controls.Add(this.m_cbFilterLockedLayers);
            this.m_grpFlags.Location = new System.Drawing.Point(16, 64);
            this.m_grpFlags.Name = "m_grpFlags";
            this.m_grpFlags.Size = new System.Drawing.Size(195, 259);
            this.m_grpFlags.TabIndex = 4;
            this.m_grpFlags.TabStop = false;
            this.m_grpFlags.Text = "Options";
            // 
            // m_cbSinglePickInSpace
            // 
            this.m_cbSinglePickInSpace.AutoSize = true;
            this.m_cbSinglePickInSpace.Location = new System.Drawing.Point(7, 211);
            this.m_cbSinglePickInSpace.Name = "m_cbSinglePickInSpace";
            this.m_cbSinglePickInSpace.Size = new System.Drawing.Size(125, 17);
            this.m_cbSinglePickInSpace.TabIndex = 8;
            this.m_cbSinglePickInSpace.Text = "Single Pick In Space";
            this.m_cbSinglePickInSpace.UseVisualStyleBackColor = true;
            // 
            // m_cbSingleOnly
            // 
            this.m_cbSingleOnly.AutoSize = true;
            this.m_cbSingleOnly.Location = new System.Drawing.Point(7, 187);
            this.m_cbSingleOnly.Name = "m_cbSingleOnly";
            this.m_cbSingleOnly.Size = new System.Drawing.Size(79, 17);
            this.m_cbSingleOnly.TabIndex = 7;
            this.m_cbSingleOnly.Text = "Single Only";
            this.m_cbSingleOnly.UseVisualStyleBackColor = true;
            // 
            // m_cbSelectEverythingInAperature
            // 
            this.m_cbSelectEverythingInAperature.AutoSize = true;
            this.m_cbSelectEverythingInAperature.Location = new System.Drawing.Point(7, 163);
            this.m_cbSelectEverythingInAperature.Name = "m_cbSelectEverythingInAperature";
            this.m_cbSelectEverythingInAperature.Size = new System.Drawing.Size(164, 17);
            this.m_cbSelectEverythingInAperature.TabIndex = 6;
            this.m_cbSelectEverythingInAperature.Text = "Select Everything In Aperture";
            this.m_cbSelectEverythingInAperature.UseVisualStyleBackColor = true;
            // 
            // m_cbRejectPaperSpaceViewport
            // 
            this.m_cbRejectPaperSpaceViewport.AutoSize = true;
            this.m_cbRejectPaperSpaceViewport.Location = new System.Drawing.Point(7, 69);
            this.m_cbRejectPaperSpaceViewport.Name = "m_cbRejectPaperSpaceViewport";
            this.m_cbRejectPaperSpaceViewport.Size = new System.Drawing.Size(166, 17);
            this.m_cbRejectPaperSpaceViewport.TabIndex = 5;
            this.m_cbRejectPaperSpaceViewport.Text = "Reject Paper Space Viewport";
            this.m_cbRejectPaperSpaceViewport.UseVisualStyleBackColor = true;
            // 
            // m_cbForceSubSelections
            // 
            this.m_cbForceSubSelections.AutoSize = true;
            this.m_cbForceSubSelections.Location = new System.Drawing.Point(7, 140);
            this.m_cbForceSubSelections.Name = "m_cbForceSubSelections";
            this.m_cbForceSubSelections.Size = new System.Drawing.Size(127, 17);
            this.m_cbForceSubSelections.TabIndex = 4;
            this.m_cbForceSubSelections.Text = "Force Sub-Selections";
            this.m_cbForceSubSelections.UseVisualStyleBackColor = true;
            this.m_cbForceSubSelections.CheckedChanged += new System.EventHandler(this.OnAllowSubSelectionChecked);
            // 
            // m_cbAllowSubSelections
            // 
            this.m_cbAllowSubSelections.AutoSize = true;
            this.m_cbAllowSubSelections.Location = new System.Drawing.Point(7, 116);
            this.m_cbAllowSubSelections.Name = "m_cbAllowSubSelections";
            this.m_cbAllowSubSelections.Size = new System.Drawing.Size(125, 17);
            this.m_cbAllowSubSelections.TabIndex = 3;
            this.m_cbAllowSubSelections.Text = "Allow Sub-Selections";
            this.m_cbAllowSubSelections.UseVisualStyleBackColor = true;
            this.m_cbAllowSubSelections.CheckedChanged += new System.EventHandler(this.OnAllowSubSelectionChecked);
            // 
            // m_cbAllowDuplicates
            // 
            this.m_cbAllowDuplicates.AutoSize = true;
            this.m_cbAllowDuplicates.Location = new System.Drawing.Point(7, 92);
            this.m_cbAllowDuplicates.Name = "m_cbAllowDuplicates";
            this.m_cbAllowDuplicates.Size = new System.Drawing.Size(104, 17);
            this.m_cbAllowDuplicates.TabIndex = 2;
            this.m_cbAllowDuplicates.Text = "Allow Duplicates";
            this.m_cbAllowDuplicates.UseVisualStyleBackColor = true;
            // 
            // m_cbRejectNonCurrentSpace
            // 
            this.m_cbRejectNonCurrentSpace.AutoSize = true;
            this.m_cbRejectNonCurrentSpace.Location = new System.Drawing.Point(7, 44);
            this.m_cbRejectNonCurrentSpace.Name = "m_cbRejectNonCurrentSpace";
            this.m_cbRejectNonCurrentSpace.Size = new System.Drawing.Size(151, 17);
            this.m_cbRejectNonCurrentSpace.TabIndex = 1;
            this.m_cbRejectNonCurrentSpace.Text = "Reject Non-Current Space";
            this.m_cbRejectNonCurrentSpace.UseVisualStyleBackColor = true;
            // 
            // m_cbFilterLockedLayers
            // 
            this.m_cbFilterLockedLayers.AutoSize = true;
            this.m_cbFilterLockedLayers.Location = new System.Drawing.Point(7, 20);
            this.m_cbFilterLockedLayers.Name = "m_cbFilterLockedLayers";
            this.m_cbFilterLockedLayers.Size = new System.Drawing.Size(121, 17);
            this.m_cbFilterLockedLayers.TabIndex = 0;
            this.m_cbFilterLockedLayers.Text = "Filter Locked Layers";
            this.m_cbFilterLockedLayers.UseVisualStyleBackColor = true;
            // 
            // m_grpMessages
            // 
            this.m_grpMessages.Controls.Add(this.m_cbMsgRemove);
            this.m_grpMessages.Controls.Add(this.m_cbMsgAdd);
            this.m_grpMessages.Controls.Add(this.m_ebMsgRemove);
            this.m_grpMessages.Controls.Add(this.m_ebMsgAdd);
            this.m_grpMessages.Location = new System.Drawing.Point(234, 64);
            this.m_grpMessages.Name = "m_grpMessages";
            this.m_grpMessages.Size = new System.Drawing.Size(268, 77);
            this.m_grpMessages.TabIndex = 5;
            this.m_grpMessages.TabStop = false;
            this.m_grpMessages.Text = "Messages";
            // 
            // m_cbMsgRemove
            // 
            this.m_cbMsgRemove.AutoSize = true;
            this.m_cbMsgRemove.Location = new System.Drawing.Point(6, 46);
            this.m_cbMsgRemove.Name = "m_cbMsgRemove";
            this.m_cbMsgRemove.Size = new System.Drawing.Size(69, 17);
            this.m_cbMsgRemove.TabIndex = 5;
            this.m_cbMsgRemove.Text = "Remove:";
            this.m_cbMsgRemove.UseVisualStyleBackColor = true;
            this.m_cbMsgRemove.CheckedChanged += new System.EventHandler(this.OnMessageRemoveChecked);
            // 
            // m_cbMsgAdd
            // 
            this.m_cbMsgAdd.AutoSize = true;
            this.m_cbMsgAdd.Location = new System.Drawing.Point(7, 20);
            this.m_cbMsgAdd.Name = "m_cbMsgAdd";
            this.m_cbMsgAdd.Size = new System.Drawing.Size(48, 17);
            this.m_cbMsgAdd.TabIndex = 4;
            this.m_cbMsgAdd.Text = "Add:";
            this.m_cbMsgAdd.UseVisualStyleBackColor = true;
            this.m_cbMsgAdd.CheckedChanged += new System.EventHandler(this.OnMessageAddChecked);
            // 
            // m_ebMsgRemove
            // 
            this.m_ebMsgRemove.Location = new System.Drawing.Point(78, 46);
            this.m_ebMsgRemove.Name = "m_ebMsgRemove";
            this.m_ebMsgRemove.Size = new System.Drawing.Size(172, 20);
            this.m_ebMsgRemove.TabIndex = 3;
            // 
            // m_ebMsgAdd
            // 
            this.m_ebMsgAdd.Location = new System.Drawing.Point(78, 20);
            this.m_ebMsgAdd.Name = "m_ebMsgAdd";
            this.m_ebMsgAdd.Size = new System.Drawing.Size(172, 20);
            this.m_ebMsgAdd.TabIndex = 0;
            // 
            // m_grpFilter
            // 
            this.m_grpFilter.Controls.Add(this.m_puClassType);
            this.m_grpFilter.Controls.Add(this.m_cbDoIsKindOfTest);
            this.m_grpFilter.Controls.Add(this.m_txtClassType);
            this.m_grpFilter.Controls.Add(this.m_cbApplyFilter);
            this.m_grpFilter.Location = new System.Drawing.Point(234, 147);
            this.m_grpFilter.Name = "m_grpFilter";
            this.m_grpFilter.Size = new System.Drawing.Size(268, 100);
            this.m_grpFilter.TabIndex = 6;
            this.m_grpFilter.TabStop = false;
            this.m_grpFilter.Text = "Filter";
            // 
            // m_puClassType
            // 
            this.m_puClassType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_puClassType.FormattingEnabled = true;
            this.m_puClassType.Location = new System.Drawing.Point(78, 41);
            this.m_puClassType.Name = "m_puClassType";
            this.m_puClassType.Size = new System.Drawing.Size(171, 21);
            this.m_puClassType.TabIndex = 4;
            // 
            // m_cbDoIsKindOfTest
            // 
            this.m_cbDoIsKindOfTest.AutoSize = true;
            this.m_cbDoIsKindOfTest.Location = new System.Drawing.Point(10, 71);
            this.m_cbDoIsKindOfTest.Name = "m_cbDoIsKindOfTest";
            this.m_cbDoIsKindOfTest.Size = new System.Drawing.Size(140, 17);
            this.m_cbDoIsKindOfTest.TabIndex = 3;
            this.m_cbDoIsKindOfTest.Text = "Include Derived Classes";
            this.m_cbDoIsKindOfTest.UseVisualStyleBackColor = true;
            // 
            // m_txtClassType
            // 
            this.m_txtClassType.AutoSize = true;
            this.m_txtClassType.Location = new System.Drawing.Point(10, 44);
            this.m_txtClassType.Name = "m_txtClassType";
            this.m_txtClassType.Size = new System.Drawing.Size(62, 13);
            this.m_txtClassType.TabIndex = 1;
            this.m_txtClassType.Text = "Class Type:";
            // 
            // m_cbApplyFilter
            // 
            this.m_cbApplyFilter.AutoSize = true;
            this.m_cbApplyFilter.Location = new System.Drawing.Point(10, 20);
            this.m_cbApplyFilter.Name = "m_cbApplyFilter";
            this.m_cbApplyFilter.Size = new System.Drawing.Size(77, 17);
            this.m_cbApplyFilter.TabIndex = 0;
            this.m_cbApplyFilter.Text = "Apply Filter";
            this.m_cbApplyFilter.UseVisualStyleBackColor = true;
            this.m_cbApplyFilter.CheckedChanged += new System.EventHandler(this.OnApplyFilterChecked);
            // 
            // m_grpEvents
            // 
            this.m_grpEvents.Controls.Add(this.m_cbUnknownEvent);
            this.m_grpEvents.Controls.Add(this.m_cbKeywordEvent);
            this.m_grpEvents.Location = new System.Drawing.Point(234, 253);
            this.m_grpEvents.Name = "m_grpEvents";
            this.m_grpEvents.Size = new System.Drawing.Size(268, 70);
            this.m_grpEvents.TabIndex = 7;
            this.m_grpEvents.TabStop = false;
            this.m_grpEvents.Text = "Events";
            // 
            // m_cbUnknownEvent
            // 
            this.m_cbUnknownEvent.AutoSize = true;
            this.m_cbUnknownEvent.Location = new System.Drawing.Point(7, 44);
            this.m_cbUnknownEvent.Name = "m_cbUnknownEvent";
            this.m_cbUnknownEvent.Size = new System.Drawing.Size(103, 17);
            this.m_cbUnknownEvent.TabIndex = 1;
            this.m_cbUnknownEvent.Text = "Unknown Event";
            this.m_cbUnknownEvent.UseVisualStyleBackColor = true;
            // 
            // m_cbKeywordEvent
            // 
            this.m_cbKeywordEvent.AutoSize = true;
            this.m_cbKeywordEvent.Location = new System.Drawing.Point(7, 20);
            this.m_cbKeywordEvent.Name = "m_cbKeywordEvent";
            this.m_cbKeywordEvent.Size = new System.Drawing.Size(239, 17);
            this.m_cbKeywordEvent.TabIndex = 0;
            this.m_cbKeywordEvent.Text = "Keyword Event    (will use \"Red Green Blue\")";
            this.m_cbKeywordEvent.UseVisualStyleBackColor = true;
            // 
            // SelectionSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_bnCancel;
            this.ClientSize = new System.Drawing.Size(517, 380);
            this.Controls.Add(this.m_grpEvents);
            this.Controls.Add(this.m_grpFilter);
            this.Controls.Add(this.m_grpMessages);
            this.Controls.Add(this.m_grpFlags);
            this.Controls.Add(this.m_txtMainSelectionMethod);
            this.Controls.Add(this.m_puMainSelectionMethod);
            this.Controls.Add(this.m_bnOk);
            this.Controls.Add(this.m_bnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectionSet";
            this.Text = "SelectionSet";
            this.m_grpFlags.ResumeLayout(false);
            this.m_grpFlags.PerformLayout();
            this.m_grpMessages.ResumeLayout(false);
            this.m_grpMessages.PerformLayout();
            this.m_grpFilter.ResumeLayout(false);
            this.m_grpFilter.PerformLayout();
            this.m_grpEvents.ResumeLayout(false);
            this.m_grpEvents.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_bnCancel;
        private System.Windows.Forms.Button m_bnOk;
        private System.Windows.Forms.ComboBox m_puMainSelectionMethod;
        private System.Windows.Forms.Label m_txtMainSelectionMethod;
        private System.Windows.Forms.GroupBox m_grpFlags;
        private System.Windows.Forms.CheckBox m_cbForceSubSelections;
        private System.Windows.Forms.CheckBox m_cbAllowSubSelections;
        private System.Windows.Forms.CheckBox m_cbAllowDuplicates;
        private System.Windows.Forms.CheckBox m_cbRejectNonCurrentSpace;
        private System.Windows.Forms.CheckBox m_cbFilterLockedLayers;
        private System.Windows.Forms.GroupBox m_grpMessages;
        private System.Windows.Forms.TextBox m_ebMsgAdd;
        private System.Windows.Forms.TextBox m_ebMsgRemove;
        private System.Windows.Forms.CheckBox m_cbSinglePickInSpace;
        private System.Windows.Forms.CheckBox m_cbSingleOnly;
        private System.Windows.Forms.CheckBox m_cbSelectEverythingInAperature;
        private System.Windows.Forms.CheckBox m_cbRejectPaperSpaceViewport;
        private System.Windows.Forms.GroupBox m_grpFilter;
        private System.Windows.Forms.CheckBox m_cbDoIsKindOfTest;
        private System.Windows.Forms.Label m_txtClassType;
        private System.Windows.Forms.CheckBox m_cbApplyFilter;
        private System.Windows.Forms.GroupBox m_grpEvents;
        private System.Windows.Forms.CheckBox m_cbKeywordEvent;
        private System.Windows.Forms.CheckBox m_cbUnknownEvent;
        private System.Windows.Forms.ComboBox m_puClassType;
        private System.Windows.Forms.CheckBox m_cbMsgRemove;
        private System.Windows.Forms.CheckBox m_cbMsgAdd;
    }
}