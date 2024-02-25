namespace WinFormsApp1
{
    partial class AddChildItemForm
    {
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
            if (disposing && (components != null))
            {
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
            button1 = new Button();
            groupBox1 = new GroupBox();
            textBoxName = new TextBox();
            button2 = new Button();
            radioButtonFile = new RadioButton();
            groupBox2 = new GroupBox();
            radioButtonFolder = new RadioButton();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.DialogResult = DialogResult.OK;
            button1.Location = new Point(12, 104);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Ок";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(textBoxName);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(230, 86);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Введите имя:";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(6, 22);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(213, 23);
            textBoxName.TabIndex = 0;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(93, 104);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "Отмена";
            button2.UseVisualStyleBackColor = true;
            // 
            // radioButtonFile
            // 
            radioButtonFile.AutoSize = true;
            radioButtonFile.Location = new Point(6, 22);
            radioButtonFile.Name = "radioButtonFile";
            radioButtonFile.Size = new Size(54, 19);
            radioButtonFile.TabIndex = 2;
            radioButtonFile.TabStop = true;
            radioButtonFile.Text = "Файл";
            radioButtonFile.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(radioButtonFolder);
            groupBox2.Controls.Add(radioButtonFile);
            groupBox2.Location = new Point(253, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(131, 86);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Выберите тип:";
            // 
            // radioButtonFolder
            // 
            radioButtonFolder.AutoSize = true;
            radioButtonFolder.Location = new Point(6, 47);
            radioButtonFolder.Name = "radioButtonFolder";
            radioButtonFolder.Size = new Size(68, 19);
            radioButtonFolder.TabIndex = 3;
            radioButtonFolder.TabStop = true;
            radioButtonFolder.Text = "Каталог";
            radioButtonFolder.UseVisualStyleBackColor = true;
            // 
            // AddChildItemForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(396, 136);
            Controls.Add(groupBox2);
            Controls.Add(button2);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddChildItemForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddChildItemForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private GroupBox groupBox1;
        private TextBox textBoxName;
        private Button button2;
        private RadioButton radioButtonFile;
        private GroupBox groupBox2;
        private RadioButton radioButtonFolder;
    }
}