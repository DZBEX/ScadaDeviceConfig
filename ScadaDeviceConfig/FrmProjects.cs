using ScadaDeviceConfig_BLL;
using ScadaDeviceConfig_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ScadaDeviceConfig
{
    public partial class FrmProjects : Form
    {
        private ProjectsManager projectsManager = new ProjectsManager();
        Projects projects =null;

        public FrmProjects()
        {
            InitializeComponent();
        }

        public FrmProjects(Projects model):this()
        {
            this.btn_Save.Text = "重命名项目";
            this.Text = "[项目信息管理]--重命名项目";
            this.txt_ProjectName.Text = model.ProjectName;
            this.projects = model;//保存当前的对象
        }

        /// <summary>
        /// 保存项目按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            //数据验证
            if (string.IsNullOrEmpty(txt_ProjectName.Text))
            {
                MessageBox.Show("项目名称不能为空");
                this.txt_ProjectName.Focus();
                return;
            }
            //封装对象
            Projects model = new Projects()
            {
                ProjectName = txt_ProjectName.Text.Trim()
            };
            if (this.projects == null)//新增
            {
                //调用数据访问层的方法
                if (projectsManager.IsRepeatProjectInsert(this.txt_ProjectName.Text.Trim()))
                {
                    MessageBox.Show("项目名称重复，请重新输入");
                    //清空文本框
                    this.txt_ProjectName.SelectAll();
                    return;
                }
                InsertModel(model);
            }
            else 
            {
                if (projectsManager.IsRepeatForUpdate(this.txt_ProjectName.Text.Trim(), this.projects.ProjectId))
                {
                    MessageBox.Show("其他项目已经使用此名称，请重新输入！");
                    //清空文本框
                    this.txt_ProjectName.SelectAll();
                    return;
                }
                model.ProjectId = this.projects.ProjectId;//项目编号
                UpdateModel(model);
            }

        }


        #region 保存项目独立窗口——新增，重命名
        private void InsertModel(Projects model)
        {
            try 
            {
                model.ProjectId = projectsManager.Insert(model);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            };
            //返回数据
            this.Tag = model;
            this.DialogResult = DialogResult.OK;
        }

        private void UpdateModel(Projects model)
        {
            try
            {
                projectsManager.Update(model);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            };
            //返回数据
            this.Tag = model.ProjectName;
            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
