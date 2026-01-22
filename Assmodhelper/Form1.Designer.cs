namespace Assmodhelper
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.AddAss = new System.Windows.Forms.Button();
            this.DelAss = new System.Windows.Forms.Button();
            this.AssList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveFilename = new System.Windows.Forms.TextBox();
            this.OutputFile = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddAss
            // 
            this.AddAss.Location = new System.Drawing.Point(387, 37);
            this.AddAss.Name = "AddAss";
            this.AddAss.Size = new System.Drawing.Size(43, 23);
            this.AddAss.TabIndex = 1;
            this.AddAss.Text = "+";
            this.AddAss.UseVisualStyleBackColor = true;
            this.AddAss.Click += new System.EventHandler(this.AddAss_Click);
            // 
            // DelAss
            // 
            this.DelAss.Location = new System.Drawing.Point(387, 66);
            this.DelAss.Name = "DelAss";
            this.DelAss.Size = new System.Drawing.Size(43, 23);
            this.DelAss.TabIndex = 2;
            this.DelAss.Text = "-";
            this.DelAss.UseVisualStyleBackColor = true;
            this.DelAss.Click += new System.EventHandler(this.DelAss_Click);
            // 
            // AssList
            // 
            this.AssList.AllowDrop = true;
            this.AssList.FormattingEnabled = true;
            this.AssList.ItemHeight = 12;
            this.AssList.Location = new System.Drawing.Point(18, 37);
            this.AssList.Name = "AssList";
            this.AssList.Size = new System.Drawing.Size(363, 88);
            this.AssList.TabIndex = 3;
            this.AssList.SelectedIndexChanged += new System.EventHandler(this.AssList_SelectedIndexChanged);
            this.AssList.DragDrop += new System.Windows.Forms.DragEventHandler(this.AssList_DragDrop);
            this.AssList.DragEnter += new System.Windows.Forms.DragEventHandler(this.AssList_DragEnter);
            this.AssList.MouseLeave += new System.EventHandler(this.AssList_MouseLeave);
            this.AssList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AssList_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Input";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Output";
            // 
            // SaveFilename
            // 
            this.SaveFilename.Location = new System.Drawing.Point(18, 202);
            this.SaveFilename.Name = "SaveFilename";
            this.SaveFilename.Size = new System.Drawing.Size(363, 21);
            this.SaveFilename.TabIndex = 6;
            // 
            // OutputFile
            // 
            this.OutputFile.Location = new System.Drawing.Point(387, 200);
            this.OutputFile.Name = "OutputFile";
            this.OutputFile.Size = new System.Drawing.Size(43, 23);
            this.OutputFile.TabIndex = 7;
            this.OutputFile.Text = "...";
            this.OutputFile.UseVisualStyleBackColor = true;
            this.OutputFile.Click += new System.EventHandler(this.OutputFile_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(18, 266);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(55, 23);
            this.Save.TabIndex = 8;
            this.Save.Text = "Do it";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(387, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "↓";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(387, 95);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(43, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "↑";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 373);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.OutputFile);
            this.Controls.Add(this.SaveFilename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AssList);
            this.Controls.Add(this.DelAss);
            this.Controls.Add(this.AddAss);
            this.Name = "Form1";
            this.Text = "Assmodhelper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddAss;
        private System.Windows.Forms.Button DelAss;
        private System.Windows.Forms.ListBox AssList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SaveFilename;
        private System.Windows.Forms.Button OutputFile;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

