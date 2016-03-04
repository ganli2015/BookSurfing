namespace BookSurfing
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
            this.listBox_ShowMyBooks = new System.Windows.Forms.ListBox();
            this.listBox_PreferredBook = new System.Windows.Forms.ListBox();
            this.textBox_MyUrl = new System.Windows.Forms.TextBox();
            this.button_Surfing = new System.Windows.Forms.Button();
            this.button_Load = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_AboveRating = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_AbovePages = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_BelowPages = new System.Windows.Forms.NumericUpDown();
            this.button_Stop = new System.Windows.Forms.Button();
            this.comboBox_Year = new System.Windows.Forms.ComboBox();
            this.comboBox_Class = new System.Windows.Forms.ComboBox();
            this.button_Classify = new System.Windows.Forms.Button();
            this.button_LoadBookInfo = new System.Windows.Forms.Button();
            this.button_OutputStat = new System.Windows.Forms.Button();
            this.button_OutputTitles = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AboveRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AbovePages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BelowPages)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox_ShowMyBooks
            // 
            this.listBox_ShowMyBooks.FormattingEnabled = true;
            this.listBox_ShowMyBooks.ItemHeight = 12;
            this.listBox_ShowMyBooks.Location = new System.Drawing.Point(21, 20);
            this.listBox_ShowMyBooks.Name = "listBox_ShowMyBooks";
            this.listBox_ShowMyBooks.Size = new System.Drawing.Size(235, 328);
            this.listBox_ShowMyBooks.TabIndex = 0;
            this.listBox_ShowMyBooks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_ShowMyBooks_MouseDoubleClick);
            // 
            // listBox_PreferredBook
            // 
            this.listBox_PreferredBook.FormattingEnabled = true;
            this.listBox_PreferredBook.ItemHeight = 12;
            this.listBox_PreferredBook.Location = new System.Drawing.Point(306, 70);
            this.listBox_PreferredBook.Name = "listBox_PreferredBook";
            this.listBox_PreferredBook.Size = new System.Drawing.Size(242, 88);
            this.listBox_PreferredBook.TabIndex = 1;
            this.listBox_PreferredBook.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox_PreferredBook_MouseDoubleClick);
            // 
            // textBox_MyUrl
            // 
            this.textBox_MyUrl.Location = new System.Drawing.Point(306, 20);
            this.textBox_MyUrl.Name = "textBox_MyUrl";
            this.textBox_MyUrl.Size = new System.Drawing.Size(242, 21);
            this.textBox_MyUrl.TabIndex = 2;
            // 
            // button_Surfing
            // 
            this.button_Surfing.Location = new System.Drawing.Point(385, 296);
            this.button_Surfing.Name = "button_Surfing";
            this.button_Surfing.Size = new System.Drawing.Size(75, 23);
            this.button_Surfing.TabIndex = 3;
            this.button_Surfing.Text = "Surfing";
            this.button_Surfing.UseVisualStyleBackColor = true;
            this.button_Surfing.Click += new System.EventHandler(this.button_Surfing_Click);
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(483, 296);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(75, 23);
            this.button_Load.TabIndex = 4;
            this.button_Load.Text = "Load Books";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "评分大于";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "页数大于";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(306, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "页数小于";
            // 
            // numericUpDown_AboveRating
            // 
            this.numericUpDown_AboveRating.DecimalPlaces = 1;
            this.numericUpDown_AboveRating.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_AboveRating.Location = new System.Drawing.Point(385, 163);
            this.numericUpDown_AboveRating.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_AboveRating.Name = "numericUpDown_AboveRating";
            this.numericUpDown_AboveRating.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_AboveRating.TabIndex = 11;
            // 
            // numericUpDown_AbovePages
            // 
            this.numericUpDown_AbovePages.Location = new System.Drawing.Point(385, 193);
            this.numericUpDown_AbovePages.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_AbovePages.Name = "numericUpDown_AbovePages";
            this.numericUpDown_AbovePages.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_AbovePages.TabIndex = 12;
            // 
            // numericUpDown_BelowPages
            // 
            this.numericUpDown_BelowPages.Location = new System.Drawing.Point(385, 222);
            this.numericUpDown_BelowPages.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_BelowPages.Name = "numericUpDown_BelowPages";
            this.numericUpDown_BelowPages.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_BelowPages.TabIndex = 13;
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(284, 296);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(75, 23);
            this.button_Stop.TabIndex = 14;
            this.button_Stop.Text = "Stop Surfing";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // comboBox_Year
            // 
            this.comboBox_Year.FormattingEnabled = true;
            this.comboBox_Year.Location = new System.Drawing.Point(171, 369);
            this.comboBox_Year.Name = "comboBox_Year";
            this.comboBox_Year.Size = new System.Drawing.Size(85, 20);
            this.comboBox_Year.TabIndex = 15;
            // 
            // comboBox_Class
            // 
            this.comboBox_Class.FormattingEnabled = true;
            this.comboBox_Class.Location = new System.Drawing.Point(21, 369);
            this.comboBox_Class.Name = "comboBox_Class";
            this.comboBox_Class.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Class.TabIndex = 16;
            // 
            // button_Classify
            // 
            this.button_Classify.Location = new System.Drawing.Point(284, 326);
            this.button_Classify.Name = "button_Classify";
            this.button_Classify.Size = new System.Drawing.Size(75, 23);
            this.button_Classify.TabIndex = 17;
            this.button_Classify.Text = "Classify";
            this.button_Classify.UseVisualStyleBackColor = true;
            this.button_Classify.Click += new System.EventHandler(this.button_Classify_Click);
            // 
            // button_LoadBookInfo
            // 
            this.button_LoadBookInfo.Location = new System.Drawing.Point(385, 325);
            this.button_LoadBookInfo.Name = "button_LoadBookInfo";
            this.button_LoadBookInfo.Size = new System.Drawing.Size(75, 23);
            this.button_LoadBookInfo.TabIndex = 18;
            this.button_LoadBookInfo.Text = "LoadBookInfo";
            this.button_LoadBookInfo.UseVisualStyleBackColor = true;
            this.button_LoadBookInfo.Click += new System.EventHandler(this.button_LoadBookInfo_Click);
            // 
            // button_OutputStat
            // 
            this.button_OutputStat.Location = new System.Drawing.Point(483, 326);
            this.button_OutputStat.Name = "button_OutputStat";
            this.button_OutputStat.Size = new System.Drawing.Size(75, 23);
            this.button_OutputStat.TabIndex = 19;
            this.button_OutputStat.Text = "OutputStat";
            this.button_OutputStat.UseVisualStyleBackColor = true;
            this.button_OutputStat.Click += new System.EventHandler(this.button_OutputStat_Click);
            // 
            // button_OutputTitles
            // 
            this.button_OutputTitles.Location = new System.Drawing.Point(284, 356);
            this.button_OutputTitles.Name = "button_OutputTitles";
            this.button_OutputTitles.Size = new System.Drawing.Size(75, 23);
            this.button_OutputTitles.TabIndex = 20;
            this.button_OutputTitles.Text = "OutputTitles";
            this.button_OutputTitles.UseVisualStyleBackColor = true;
            this.button_OutputTitles.Click += new System.EventHandler(this.button_OutputTitles_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 401);
            this.Controls.Add(this.button_OutputTitles);
            this.Controls.Add(this.button_OutputStat);
            this.Controls.Add(this.button_LoadBookInfo);
            this.Controls.Add(this.button_Classify);
            this.Controls.Add(this.comboBox_Class);
            this.Controls.Add(this.comboBox_Year);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.numericUpDown_BelowPages);
            this.Controls.Add(this.numericUpDown_AbovePages);
            this.Controls.Add(this.numericUpDown_AboveRating);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.button_Surfing);
            this.Controls.Add(this.textBox_MyUrl);
            this.Controls.Add(this.listBox_PreferredBook);
            this.Controls.Add(this.listBox_ShowMyBooks);
            this.Name = "MainForm";
            this.Text = "BookSurfing";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AboveRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AbovePages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BelowPages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_ShowMyBooks;
        private System.Windows.Forms.ListBox listBox_PreferredBook;
        private System.Windows.Forms.TextBox textBox_MyUrl;
        private System.Windows.Forms.Button button_Surfing;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_AboveRating;
        private System.Windows.Forms.NumericUpDown numericUpDown_AbovePages;
        private System.Windows.Forms.NumericUpDown numericUpDown_BelowPages;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.ComboBox comboBox_Year;
        private System.Windows.Forms.ComboBox comboBox_Class;
        private System.Windows.Forms.Button button_Classify;
        private System.Windows.Forms.Button button_LoadBookInfo;
        private System.Windows.Forms.Button button_OutputStat;
        private System.Windows.Forms.Button button_OutputTitles;
    }
}

