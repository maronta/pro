namespace BuyListCreator
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "2014/07/15",
            "マザーズ",
            "9999",
            "めいがらのなまえ",
            "999,999",
            "9,999",
            "買い"}, -1);
            this.dtpCalendar = new System.Windows.Forms.DateTimePicker();
            this.btnExec = new System.Windows.Forms.Button();
            this.chxSystems = new System.Windows.Forms.CheckedListBox();
            this.btnGetSystem = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnAllCheckOn = new System.Windows.Forms.Button();
            this.btnAllCheckOff = new System.Windows.Forms.Button();
            this.listViewResult = new System.Windows.Forms.ListView();
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMarket = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // dtpCalendar
            // 
            this.dtpCalendar.CustomFormat = "yyyy/MM/dd";
            this.dtpCalendar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCalendar.Location = new System.Drawing.Point(12, 37);
            this.dtpCalendar.Name = "dtpCalendar";
            this.dtpCalendar.Size = new System.Drawing.Size(200, 19);
            this.dtpCalendar.TabIndex = 0;
            // 
            // btnExec
            // 
            this.btnExec.Location = new System.Drawing.Point(218, 227);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(75, 23);
            this.btnExec.TabIndex = 1;
            this.btnExec.Text = "実行";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.button1_Click);
            // 
            // chxSystems
            // 
            this.chxSystems.FormattingEnabled = true;
            this.chxSystems.Location = new System.Drawing.Point(12, 65);
            this.chxSystems.Name = "chxSystems";
            this.chxSystems.Size = new System.Drawing.Size(200, 186);
            this.chxSystems.TabIndex = 2;
            // 
            // btnGetSystem
            // 
            this.btnGetSystem.Location = new System.Drawing.Point(218, 37);
            this.btnGetSystem.Name = "btnGetSystem";
            this.btnGetSystem.Size = new System.Drawing.Size(75, 23);
            this.btnGetSystem.TabIndex = 3;
            this.btnGetSystem.Text = "システム取得";
            this.btnGetSystem.UseVisualStyleBackColor = true;
            this.btnGetSystem.Click += new System.EventHandler(this.btnGetSystem_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(12, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(200, 19);
            this.txtFilePath.TabIndex = 4;
            this.txtFilePath.Text = "C:\\Users\\Atsushi\\ProtraTest";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(218, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "開く";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(310, 12);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(404, 121);
            this.txtResult.TabIndex = 6;
            this.txtResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResult_KeyDown);
            // 
            // btnAllCheckOn
            // 
            this.btnAllCheckOn.Location = new System.Drawing.Point(218, 66);
            this.btnAllCheckOn.Name = "btnAllCheckOn";
            this.btnAllCheckOn.Size = new System.Drawing.Size(75, 23);
            this.btnAllCheckOn.TabIndex = 7;
            this.btnAllCheckOn.Text = "すべて選択";
            this.btnAllCheckOn.UseVisualStyleBackColor = true;
            this.btnAllCheckOn.Click += new System.EventHandler(this.btnAllCheckOn_Click);
            // 
            // btnAllCheckOff
            // 
            this.btnAllCheckOff.Location = new System.Drawing.Point(218, 95);
            this.btnAllCheckOff.Name = "btnAllCheckOff";
            this.btnAllCheckOff.Size = new System.Drawing.Size(75, 23);
            this.btnAllCheckOff.TabIndex = 8;
            this.btnAllCheckOff.Text = "すべて解除";
            this.btnAllCheckOff.UseVisualStyleBackColor = true;
            this.btnAllCheckOff.Click += new System.EventHandler(this.btnAllCheckOff_Click);
            // 
            // listViewResult
            // 
            this.listViewResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colMarket,
            this.colCode,
            this.colName,
            this.colPrice,
            this.colNumber,
            this.colOrder});
            this.listViewResult.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listViewResult.Location = new System.Drawing.Point(310, 143);
            this.listViewResult.Name = "listViewResult";
            this.listViewResult.Size = new System.Drawing.Size(404, 108);
            this.listViewResult.TabIndex = 9;
            this.listViewResult.UseCompatibleStateImageBehavior = false;
            this.listViewResult.View = System.Windows.Forms.View.Details;
            // 
            // colDate
            // 
            this.colDate.Text = "日付";
            this.colDate.Width = 70;
            // 
            // colMarket
            // 
            this.colMarket.Text = "市場";
            this.colMarket.Width = 55;
            // 
            // colCode
            // 
            this.colCode.Text = "コード";
            this.colCode.Width = 40;
            // 
            // colName
            // 
            this.colName.Text = "銘柄名";
            this.colName.Width = 80;
            // 
            // colPrice
            // 
            this.colPrice.Text = "値段";
            // 
            // colNumber
            // 
            this.colNumber.Text = "枚数";
            this.colNumber.Width = 50;
            // 
            // colOrder
            // 
            this.colOrder.Text = "注文";
            this.colOrder.Width = 40;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 262);
            this.Controls.Add(this.listViewResult);
            this.Controls.Add(this.btnAllCheckOff);
            this.Controls.Add(this.btnAllCheckOn);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.btnGetSystem);
            this.Controls.Add(this.chxSystems);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.dtpCalendar);
            this.Name = "Form1";
            this.Text = "BuyListCreator ver1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpCalendar;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.CheckedListBox chxSystems;
        private System.Windows.Forms.Button btnGetSystem;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnAllCheckOn;
        private System.Windows.Forms.Button btnAllCheckOff;
        private System.Windows.Forms.ListView listViewResult;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colPrice;
        private System.Windows.Forms.ColumnHeader colOrder;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colNumber;
        private System.Windows.Forms.ColumnHeader colMarket;
    }
}

