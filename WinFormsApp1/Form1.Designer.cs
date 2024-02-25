namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ModesMenuStrip = new ContextMenuStrip(components);
            добавитьToolStripMenuItem = new ToolStripMenuItem();
            удалитьToolStripMenuItem = new ToolStripMenuItem();
            treeListView1 = new BrightIdeasSoftware.TreeListView();
            bindingSource1 = new BindingSource(components);
            ModesMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)treeListView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // ModesMenuStrip
            // 
            ModesMenuStrip.Items.AddRange(new ToolStripItem[] { добавитьToolStripMenuItem, удалитьToolStripMenuItem });
            ModesMenuStrip.Name = "ModesMenuStrip";
            ModesMenuStrip.Size = new Size(127, 48);
            // 
            // добавитьToolStripMenuItem
            // 
            добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            добавитьToolStripMenuItem.Size = new Size(126, 22);
            добавитьToolStripMenuItem.Text = "Добавить";
            добавитьToolStripMenuItem.Click += добавитьToolStripMenuItem_Click;
            // 
            // удалитьToolStripMenuItem
            // 
            удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            удалитьToolStripMenuItem.Size = new Size(126, 22);
            удалитьToolStripMenuItem.Text = "Удалить";
            удалитьToolStripMenuItem.Click += удалитьToolStripMenuItem_Click;
            // 
            // treeListView1
            // 
            treeListView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeListView1.FullRowSelect = true;
            treeListView1.IsSimpleDragSource = true;
            treeListView1.IsSimpleDropSink = true;
            treeListView1.LabelEdit = true;
            treeListView1.Location = new Point(12, 12);
            treeListView1.Name = "treeListView1";
            treeListView1.ShowGroups = false;
            treeListView1.Size = new Size(776, 426);
            treeListView1.Sorting = SortOrder.Ascending;
            treeListView1.TabIndex = 1;
            treeListView1.UseHotItem = true;
            treeListView1.View = View.Details;
            treeListView1.VirtualMode = true;
            treeListView1.CellEditFinishing += TreeListView_CellEditFinishing;
            treeListView1.CellEditStarting += TreeListView_CellEditStarting;
            treeListView1.CellRightClick += treeListView1_CellRightClick;
            treeListView1.ModelCanDrop += treeListView1_ModelCanDrop;
            treeListView1.ModelDropped += HandleModelDropped;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(treeListView1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ModesMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)treeListView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem добавитьToolStripMenuItem;
        private ContextMenuStrip ModesMenuStrip;
        private BrightIdeasSoftware.TreeListView treeListView1;
        private BindingSource bindingSource1;
        private ToolStripMenuItem удалитьToolStripMenuItem;
    }
}