namespace Olive.CodeBuilder.Forms
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_conn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tvw_table = new System.Windows.Forms.TreeView();
            this.btn_loaddb = new System.Windows.Forms.Button();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.btn_loadtpl = new System.Windows.Forms.Button();
            this.txt_tpl = new System.Windows.Forms.TextBox();
            this.btn_buid = new System.Windows.Forms.Button();
            this.fbd_tpl = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_select = new System.Windows.Forms.Button();
            this.tvw_tpl = new System.Windows.Forms.TreeView();
            this.imgList1 = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库连接：";
            // 
            // txt_conn
            // 
            this.txt_conn.Location = new System.Drawing.Point(108, 27);
            this.txt_conn.Name = "txt_conn";
            this.txt_conn.Size = new System.Drawing.Size(581, 21);
            this.txt_conn.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "T4模板路径：";
            // 
            // tvw_table
            // 
            this.tvw_table.Location = new System.Drawing.Point(32, 138);
            this.tvw_table.Name = "tvw_table";
            this.tvw_table.Size = new System.Drawing.Size(360, 260);
            this.tvw_table.TabIndex = 4;
            this.tvw_table.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvw_table_AfterCheck);
            // 
            // btn_loaddb
            // 
            this.btn_loaddb.Location = new System.Drawing.Point(695, 26);
            this.btn_loaddb.Name = "btn_loaddb";
            this.btn_loaddb.Size = new System.Drawing.Size(75, 23);
            this.btn_loaddb.TabIndex = 5;
            this.btn_loaddb.Text = "载入数据库";
            this.btn_loaddb.UseVisualStyleBackColor = true;
            this.btn_loaddb.Click += new System.EventHandler(this.btn_loaddb_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "cs2.ICO");
            this.imgList.Images.SetKeyName(1, "db.ico");
            this.imgList.Images.SetKeyName(2, "folder.ICO");
            this.imgList.Images.SetKeyName(3, "table.ICO");
            this.imgList.Images.SetKeyName(4, "view.ICO");
            this.imgList.Images.SetKeyName(5, "sp.ICO");
            this.imgList.Images.SetKeyName(6, "key.ICO");
            this.imgList.Images.SetKeyName(7, "column.ICO");
            this.imgList.Images.SetKeyName(8, "index.ICO");
            this.imgList.Images.SetKeyName(9, "trigger.ICO");
            this.imgList.Images.SetKeyName(10, "check.ICO");
            // 
            // btn_loadtpl
            // 
            this.btn_loadtpl.Location = new System.Drawing.Point(695, 74);
            this.btn_loadtpl.Name = "btn_loadtpl";
            this.btn_loadtpl.Size = new System.Drawing.Size(75, 23);
            this.btn_loadtpl.TabIndex = 6;
            this.btn_loadtpl.Text = "载入T4模板";
            this.btn_loadtpl.UseVisualStyleBackColor = true;
            this.btn_loadtpl.Click += new System.EventHandler(this.btn_loadtpl_Click);
            // 
            // txt_tpl
            // 
            this.txt_tpl.Location = new System.Drawing.Point(108, 75);
            this.txt_tpl.Name = "txt_tpl";
            this.txt_tpl.Size = new System.Drawing.Size(500, 21);
            this.txt_tpl.TabIndex = 7;
            // 
            // btn_buid
            // 
            this.btn_buid.Location = new System.Drawing.Point(695, 406);
            this.btn_buid.Name = "btn_buid";
            this.btn_buid.Size = new System.Drawing.Size(75, 23);
            this.btn_buid.TabIndex = 8;
            this.btn_buid.Text = "生成代码";
            this.btn_buid.UseVisualStyleBackColor = true;
            this.btn_buid.Click += new System.EventHandler(this.btn_buid_Click);
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(614, 74);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(75, 23);
            this.btn_select.TabIndex = 9;
            this.btn_select.Text = "选择文件夹";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // tvw_tpl
            // 
            this.tvw_tpl.Location = new System.Drawing.Point(410, 138);
            this.tvw_tpl.Name = "tvw_tpl";
            this.tvw_tpl.Size = new System.Drawing.Size(360, 260);
            this.tvw_tpl.TabIndex = 10;
            this.tvw_tpl.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvw_tpl_AfterCheck);
            // 
            // imgList1
            // 
            this.imgList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList1.ImageStream")));
            this.imgList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList1.Images.SetKeyName(0, "templateRoot.png");
            this.imgList1.Images.SetKeyName(1, "templateDir.png");
            this.imgList1.Images.SetKeyName(2, "templateFile.png");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "表和视图：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(412, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "T4模板：";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(108, 109);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(284, 21);
            this.txt_name.TabIndex = 13;
            this.txt_name.TextChanged += new System.EventHandler(this.txt_name_TextChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tvw_tpl);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.btn_buid);
            this.Controls.Add(this.txt_tpl);
            this.Controls.Add(this.btn_loadtpl);
            this.Controls.Add(this.btn_loaddb);
            this.Controls.Add(this.tvw_table);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_conn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "T4代码生成器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_conn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView tvw_table;
        private System.Windows.Forms.Button btn_loaddb;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Button btn_loadtpl;
        private System.Windows.Forms.TextBox txt_tpl;
        private System.Windows.Forms.Button btn_buid;
        private System.Windows.Forms.FolderBrowserDialog fbd_tpl;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.TreeView tvw_tpl;
        private System.Windows.Forms.ImageList imgList1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_name;
    }
}