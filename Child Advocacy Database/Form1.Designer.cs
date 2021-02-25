
namespace Child_Advocacy_Database
{
    partial class Form1
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
            this.cNumTxt = new System.Windows.Forms.TextBox();
            this.cNumLbl = new System.Windows.Forms.Label();
            this.childFirstNameLbl = new System.Windows.Forms.Label();
            this.childFirstNameTxt = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.perpFirstTxt = new System.Windows.Forms.TextBox();
            this.perpFirstLbl = new System.Windows.Forms.Label();
            this.perpLastTxt = new System.Windows.Forms.TextBox();
            this.perpLastLbl = new System.Windows.Forms.Label();
            this.perpLstView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addPerpBtn = new System.Windows.Forms.Button();
            this.perpHeaderLbl = new System.Windows.Forms.Label();
            this.addCaseBtn = new System.Windows.Forms.Button();
            this.addPDF = new System.Windows.Forms.OpenFileDialog();
            this.addPdfBtn = new System.Windows.Forms.Button();
            this.pdfFilesListView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mp4ListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mp4Btn = new System.Windows.Forms.Button();
            this.addVideo = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // cNumTxt
            // 
            this.cNumTxt.Location = new System.Drawing.Point(95, 22);
            this.cNumTxt.Name = "cNumTxt";
            this.cNumTxt.Size = new System.Drawing.Size(100, 20);
            this.cNumTxt.TabIndex = 0;
            // 
            // cNumLbl
            // 
            this.cNumLbl.AutoSize = true;
            this.cNumLbl.Location = new System.Drawing.Point(18, 25);
            this.cNumLbl.Name = "cNumLbl";
            this.cNumLbl.Size = new System.Drawing.Size(71, 13);
            this.cNumLbl.TabIndex = 1;
            this.cNumLbl.Text = "Case Number";
            // 
            // childFirstNameLbl
            // 
            this.childFirstNameLbl.AutoSize = true;
            this.childFirstNameLbl.Location = new System.Drawing.Point(5, 84);
            this.childFirstNameLbl.Name = "childFirstNameLbl";
            this.childFirstNameLbl.Size = new System.Drawing.Size(83, 13);
            this.childFirstNameLbl.TabIndex = 2;
            this.childFirstNameLbl.Text = "Child First Name";
            // 
            // childFirstNameTxt
            // 
            this.childFirstNameTxt.Location = new System.Drawing.Point(95, 81);
            this.childFirstNameTxt.Name = "childFirstNameTxt";
            this.childFirstNameTxt.Size = new System.Drawing.Size(100, 20);
            this.childFirstNameTxt.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(95, 107);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Child Last Name";
            // 
            // perpFirstTxt
            // 
            this.perpFirstTxt.Location = new System.Drawing.Point(529, 26);
            this.perpFirstTxt.Name = "perpFirstTxt";
            this.perpFirstTxt.Size = new System.Drawing.Size(100, 20);
            this.perpFirstTxt.TabIndex = 7;
            // 
            // perpFirstLbl
            // 
            this.perpFirstLbl.AutoSize = true;
            this.perpFirstLbl.Location = new System.Drawing.Point(411, 29);
            this.perpFirstLbl.Name = "perpFirstLbl";
            this.perpFirstLbl.Size = new System.Drawing.Size(112, 13);
            this.perpFirstLbl.TabIndex = 6;
            this.perpFirstLbl.Text = "Perpetrator First Name";
            // 
            // perpLastTxt
            // 
            this.perpLastTxt.Location = new System.Drawing.Point(529, 51);
            this.perpLastTxt.Name = "perpLastTxt";
            this.perpLastTxt.Size = new System.Drawing.Size(100, 20);
            this.perpLastTxt.TabIndex = 9;
            // 
            // perpLastLbl
            // 
            this.perpLastLbl.AutoSize = true;
            this.perpLastLbl.Location = new System.Drawing.Point(411, 54);
            this.perpLastLbl.Name = "perpLastLbl";
            this.perpLastLbl.Size = new System.Drawing.Size(113, 13);
            this.perpLastLbl.TabIndex = 8;
            this.perpLastLbl.Text = "Perpetrator Last Name";
            // 
            // perpLstView
            // 
            this.perpLstView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.perpLstView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.perpLstView.HideSelection = false;
            this.perpLstView.Location = new System.Drawing.Point(414, 138);
            this.perpLstView.Name = "perpLstView";
            this.perpLstView.Size = new System.Drawing.Size(282, 97);
            this.perpLstView.TabIndex = 10;
            this.perpLstView.UseCompatibleStateImageBehavior = false;
            this.perpLstView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 278;
            // 
            // addPerpBtn
            // 
            this.addPerpBtn.Location = new System.Drawing.Point(493, 84);
            this.addPerpBtn.Name = "addPerpBtn";
            this.addPerpBtn.Size = new System.Drawing.Size(91, 23);
            this.addPerpBtn.TabIndex = 11;
            this.addPerpBtn.Text = "Add Perpetrator";
            this.addPerpBtn.UseVisualStyleBackColor = true;
            this.addPerpBtn.Click += new System.EventHandler(this.AddPerpBtn_Click);
            // 
            // perpHeaderLbl
            // 
            this.perpHeaderLbl.AutoSize = true;
            this.perpHeaderLbl.Location = new System.Drawing.Point(414, 119);
            this.perpHeaderLbl.Name = "perpHeaderLbl";
            this.perpHeaderLbl.Size = new System.Drawing.Size(64, 13);
            this.perpHeaderLbl.TabIndex = 12;
            this.perpHeaderLbl.Text = "Perpetrators";
            // 
            // addCaseBtn
            // 
            this.addCaseBtn.Location = new System.Drawing.Point(713, 415);
            this.addCaseBtn.Name = "addCaseBtn";
            this.addCaseBtn.Size = new System.Drawing.Size(75, 23);
            this.addCaseBtn.TabIndex = 13;
            this.addCaseBtn.Text = "Add Case";
            this.addCaseBtn.UseVisualStyleBackColor = true;
            // 
            // addPDF
            // 
            this.addPDF.DefaultExt = "pdf";
            this.addPDF.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            this.addPDF.Multiselect = true;
            this.addPDF.Title = "Add PDF File(s)";
            // 
            // addPdfBtn
            // 
            this.addPdfBtn.Location = new System.Drawing.Point(8, 312);
            this.addPdfBtn.Name = "addPdfBtn";
            this.addPdfBtn.Size = new System.Drawing.Size(75, 23);
            this.addPdfBtn.TabIndex = 14;
            this.addPdfBtn.Text = "Add PDF";
            this.addPdfBtn.UseVisualStyleBackColor = true;
            this.addPdfBtn.Click += new System.EventHandler(this.AddPdfBtn_Click);
            // 
            // pdfFilesListView
            // 
            this.pdfFilesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.pdfFilesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.pdfFilesListView.HideSelection = false;
            this.pdfFilesListView.Location = new System.Drawing.Point(8, 341);
            this.pdfFilesListView.Name = "pdfFilesListView";
            this.pdfFilesListView.Size = new System.Drawing.Size(282, 97);
            this.pdfFilesListView.TabIndex = 15;
            this.pdfFilesListView.UseCompatibleStateImageBehavior = false;
            this.pdfFilesListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 278;
            // 
            // mp4ListView
            // 
            this.mp4ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.mp4ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.mp4ListView.HideSelection = false;
            this.mp4ListView.Location = new System.Drawing.Point(302, 341);
            this.mp4ListView.Name = "mp4ListView";
            this.mp4ListView.Size = new System.Drawing.Size(282, 97);
            this.mp4ListView.TabIndex = 17;
            this.mp4ListView.UseCompatibleStateImageBehavior = false;
            this.mp4ListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 278;
            // 
            // mp4Btn
            // 
            this.mp4Btn.Location = new System.Drawing.Point(302, 312);
            this.mp4Btn.Name = "mp4Btn";
            this.mp4Btn.Size = new System.Drawing.Size(75, 23);
            this.mp4Btn.TabIndex = 16;
            this.mp4Btn.Text = "Add Video";
            this.mp4Btn.UseVisualStyleBackColor = true;
            this.mp4Btn.Click += new System.EventHandler(this.mp4Btn_Click);
            // 
            // openFileDialog1
            // 
            this.addVideo.FileName = "addVideo";
            this.addPDF.DefaultExt = "mp4";
            this.addPDF.Filter = "Video files (*.mp4;*.wmv;*.mkv;*.avi;*.flv;*.mov)|*.mp4;*.wmv;*.mkv;*.avi;*.flv;*.mov|All files (*.*)|*.*";
            this.addPDF.Multiselect = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mp4ListView);
            this.Controls.Add(this.mp4Btn);
            this.Controls.Add(this.pdfFilesListView);
            this.Controls.Add(this.addPdfBtn);
            this.Controls.Add(this.addCaseBtn);
            this.Controls.Add(this.perpHeaderLbl);
            this.Controls.Add(this.addPerpBtn);
            this.Controls.Add(this.perpLstView);
            this.Controls.Add(this.perpLastTxt);
            this.Controls.Add(this.perpLastLbl);
            this.Controls.Add(this.perpFirstTxt);
            this.Controls.Add(this.perpFirstLbl);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.childFirstNameTxt);
            this.Controls.Add(this.childFirstNameLbl);
            this.Controls.Add(this.cNumLbl);
            this.Controls.Add(this.cNumTxt);
            this.Name = "Form1";
            this.Text = "Add New Case";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cNumTxt;
        private System.Windows.Forms.Label cNumLbl;
        private System.Windows.Forms.Label childFirstNameLbl;
        private System.Windows.Forms.TextBox childFirstNameTxt;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox perpFirstTxt;
        private System.Windows.Forms.Label perpFirstLbl;
        private System.Windows.Forms.TextBox perpLastTxt;
        private System.Windows.Forms.Label perpLastLbl;
        private System.Windows.Forms.ListView perpLstView;
        private System.Windows.Forms.Button addPerpBtn;
        private System.Windows.Forms.Label perpHeaderLbl;
        private System.Windows.Forms.Button addCaseBtn;
        private System.Windows.Forms.OpenFileDialog addPDF;
        private System.Windows.Forms.Button addPdfBtn;
        private System.Windows.Forms.ListView pdfFilesListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView mp4ListView;     
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button mp4Btn;
        private System.Windows.Forms.OpenFileDialog addVideo;
    }
}