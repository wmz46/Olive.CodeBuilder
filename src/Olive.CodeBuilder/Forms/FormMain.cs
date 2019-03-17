using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EnvDTE;
using Olive.CodeBuilder.Core;

namespace Olive.CodeBuilder.Forms
{
    public partial class FormMain : Form
    {
        private SqlDbObject _dbObject = null;
        private readonly DTE _dte;
        private DataTable _table;
        /// <summary>
        /// 获取选中文件夹完整路径
        /// </summary>
        /// <returns></returns>
        private string GetSelectedFolderPath()
        {
            return _dte.SelectedItems.Item(1).ProjectItem.FileNames[0];
        }

        private Project GetSelectedProject()
        {
            return _dte.SelectedItems.Item(1).ProjectItem.ContainingProject;
            //return _dte.SelectedItems.Item(1).ProjectItem.ContainingProject.FullName;
        }

        private string GetSelectedNamespace()
        {
            var properties = _dte.SelectedItems.Item(1).ProjectItem.Properties;
            for (var i = 1; i <= properties.Count; i++)
            {
                if (properties.Item(i).Name == "DefaultNamespace")
                {
                    return properties.Item(i).Value.ToString();
                }
            }
            return "";
        }
        public FormMain(DTE dte)
        {
            InitializeComponent();
            _dte = dte;
            var config = ConfigManager.GetConfig();
            if (config != null)
            {
                txt_conn.Text = config.ConnStr;
                txt_tpl.Text = config.TplPath;
            }

            this.HelpButtonClicked += FormMain_HelpButtonClicked;
            this.Text = this.Text + "(" + GetSelectedFolderPath() + ")";
        }

        private void FormMain_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("钢翼(659240788@qq.com)\nhttp://www.iceolive.com", "插件作者");
            e.Cancel = true;
        }

        private void btn_loaddb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_conn.Text))
            {
                MessageBox.Show("请输入数据库连接");
            }
            else
            {
                try
                {
                    _dbObject = new SqlDbObject(txt_conn.Text);
                    _table = _dbObject.GetTables("");
                    tvw_table.ImageIndex = 0;
                    tvw_table.CheckBoxes = true;
                    tvw_table.ImageList = imgList;
                    tvw_table.HotTracking = true;
                    tvw_table.SelectedImageIndex = 0;
                    tvw_table.ShowNodeToolTips = true;
                    tvw_table.Nodes.Clear();
                    var tableNode = new TreeNode("表", 2, 2);
                    var viewNode = new TreeNode("视图", 2, 2);
                    foreach (DataRow row in _table.Rows)
                    {
                        var name = row["name"].ToString();
                        if (row["type"].ToString() == "U ")
                        {
                            var tn = new TreeNode(name, 3, 3);
                            tableNode.Nodes.Add(tn);
                        }
                        else if (row["type"].ToString() == "V ")
                        {
                            var tn = new TreeNode(name, 4, 4);
                            viewNode.Nodes.Add(tn);
                        }
                    }
                    tvw_table.Nodes.Add(tableNode);
                    tvw_table.Nodes.Add(viewNode);
                    tvw_table.ExpandAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请检查连接是否正常");
                }

            }
        }

        private void tvw_table_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //只处理鼠标点击引起的状态变化
            if (e.Action == TreeViewAction.ByMouse)
            {
                //更新子节点状态
                foreach (TreeNode child in e.Node.Nodes)
                {
                    child.Checked = e.Node.Checked;
                }

                var parent = e.Node.Parent;
                if (parent != null)
                {
                    //设置父节点状态
                    var isAllChecked = true;
                    foreach (TreeNode node in parent.Nodes)
                    {
                        if (node.Checked == false)
                        {
                            isAllChecked = false;
                            break;
                        }
                    }

                    parent.Checked = isAllChecked;
                }

            }

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var config = ConfigManager.GetConfig();
            config.ConnStr = txt_conn.Text;
            config.TplPath = txt_tpl.Text;
            ConfigManager.SetConfig(config);
        }

        private void btn_loadtpl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_tpl.Text))
            {
                tvw_tpl.CheckBoxes = true;
                tvw_tpl.ImageIndex = 0;
                tvw_tpl.ImageList = imgList1;
                tvw_tpl.HotTracking = true;
                tvw_tpl.SelectedImageIndex = 0;
                tvw_tpl.ShowNodeToolTips = true;
                tvw_tpl.Nodes.Clear();
                var files = Directory.GetFiles(txt_tpl.Text, "*.tt");
                foreach (var file in files)
                {
                    var item = new FileInfo(file);
                    var tn = new TreeNode(item.Name, 1, 1);
                    tn.Tag = file;
                    tvw_tpl.Nodes.Add(tn);
                }
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            fbd_tpl.ShowNewFolderButton = false;
            fbd_tpl.Description = "请选择T4模板所在文件夹";
            if (!string.IsNullOrEmpty(txt_tpl.Text))
            {
                fbd_tpl.SelectedPath = txt_tpl.Text;
            }

            if (fbd_tpl.ShowDialog() == DialogResult.OK)
            {
                txt_tpl.Text = fbd_tpl.SelectedPath;
            }
        }

        private void btn_buid_Click(object sender, EventArgs e)
        {
            var templateFileName = "";
            var flag = false;
            foreach (TreeNode tn in tvw_tpl.Nodes)
            {
                if (tn.Checked)
                {
                    templateFileName = (string)tn.Tag;
                    flag = true;
                }
            }
            if (!flag)
            {
                MessageBox.Show("请选择要生成的T4模板");
                return;
            }
            flag = false;
            var allSuccess = true;
            foreach (TreeNode tn in tvw_table.Nodes)
            {
                foreach (TreeNode tn2 in tn.Nodes)
                {
                    if (tn2.Checked)
                    {
                        var tableName = tn2.Text;
                        var columns = _dbObject.GetColumnInfoList("", tableName);

                        var host = new TableHost
                        {
                            TableName = tableName,
                            NameSpace = GetSelectedNamespace(),
                            Columns = columns,
                            TemplateFileValue = templateFileName,
                        };
                        var input = File.ReadAllText(host.TemplateFileValue);
                        var output = new Core.CodeBuilder().Process(host);
                        if (!host.Errors.HasErrors)
                        {

                            var reg = new Regex("class\\s+([\\w\\d$_]+)s*");
                            var className = tableName;
                            if (reg.IsMatch(output))
                            {
                                className = reg.Match(output).Groups[1].Value;
                            }

                            reg = new Regex("output\\s+extension=\"(.*?)\"");
                            var fileExtension = host.FileExtension;
                            if (reg.IsMatch(input))
                            {
                                fileExtension = reg.Match(input).Groups[1].Value;
                            }
                            var outputFileName = Path.Combine(GetSelectedFolderPath(), className + fileExtension);
                            File.WriteAllText(outputFileName, output, host.FileEncoding);
                            var project = GetSelectedProject();
                            project.ProjectItems.AddFromFile(outputFileName);
                            project.Save();
                        }
                        else
                        {
                            MessageBox.Show(output);
                            allSuccess = false;
                        }
                        flag = true;
                    }
                }
            }

            if (!flag)
            {
                MessageBox.Show("请选择要生成的表或视图");
                return;
            }
            if (allSuccess)
            {
                MessageBox.Show("生成成功");
            }

        }


        private void txt_name_TextChanged(object sender, EventArgs e)
        {
            tvw_table.Nodes.Clear();
            var tableNode = new TreeNode("表", 2, 2);
            var viewNode = new TreeNode("视图", 2, 2);

            foreach (DataRow row in _table.Rows)
            {
                var name = row["name"].ToString();
                if (row["type"].ToString() == "U ")
                {
                    var tn = new TreeNode(name, 3, 3);
                    if (string.IsNullOrEmpty(txt_name.Text) || name.ToLower().Contains(txt_name.Text.ToLower()))
                    {
                        tableNode.Nodes.Add(tn);
                    }
                }
                else if (row["type"].ToString() == "V ")
                {
                    var tn = new TreeNode(name, 4, 4);
                    if (string.IsNullOrEmpty(txt_name.Text) || name.ToLower().Contains(txt_name.Text.ToLower()))
                    {
                        viewNode.Nodes.Add(tn);
                    }
                }
            }
            tvw_table.Nodes.Add(tableNode);
            tvw_table.Nodes.Add(viewNode);
            tvw_table.ExpandAll();
        }

        private void tvw_tpl_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //只处理鼠标点击引起的状态变化
            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Checked)//只能有一个选中
                {
                    foreach (TreeNode tn in tvw_tpl.Nodes)
                    {
                        if (tn != e.Node)
                        {
                            tn.Checked = false;
                        }
                    }
                }
            }
        }
    }
}
