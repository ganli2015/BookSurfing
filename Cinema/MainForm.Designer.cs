namespace Cinema
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox_Titles = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox_Titles
            // 
            this.listBox_Titles.FormattingEnabled = true;
            this.listBox_Titles.ItemHeight = 12;
            this.listBox_Titles.Location = new System.Drawing.Point(54, 24);
            this.listBox_Titles.Name = "listBox_Titles";
            this.listBox_Titles.Size = new System.Drawing.Size(229, 292);
            this.listBox_Titles.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 357);
            this.Controls.Add(this.listBox_Titles);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_Titles;
    }
}

