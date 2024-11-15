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
    public partial class FrmMain : Form
    {
        private ProjectsManager projectsManager =new ProjectsManager();
        public FrmMain()
        {
            InitializeComponent();
            this.dgv_Project.AutoGenerateColumns = false;
            //默认显示全部项目信息
            this.projectsList = projectsManager.Query();
            if (this.projectsList.Count !=0  )
            {
                this.dgv_Project.DataSource = projectsList;
                this.btn_RenameProject.Enabled = true;
                this.btn_DeleteProject.Enabled = true;
                this.button10.Enabled = true;

            }
            else
            {
                this.dgv_Project.DataSource = null;
                //禁止功能按钮
                this.btn_RenameProject.Enabled = false;
                this.btn_DeleteProject.Enabled = false;
                this.button10.Enabled = false;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否退出系统？", "退出系统", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }


        #region 项目添加
        //保存当前新增项目
        private List <Projects> projectsList= new List<Projects>();
        // 添加项目
        private void btn_AddProject_Click(object sender, EventArgs e)
        {
            FrmProjects frm = new FrmProjects();
            DialogResult result = frm.ShowDialog();

            if (result == DialogResult.OK) 
            {
                Projects newProject = (Projects)frm.Tag;
                newProject.SN = this.projectsList.Count+1;
                //添加到集合显示
                projectsList.Add(newProject);
                this.dgv_Project.DataSource = null;
                this.dgv_Project.DataSource = projectsList;
            }
              
            
        }
        #endregion

        #region 项目重命名
        private void btn_RenameProject_Click(object sender, EventArgs e)
        {
            int projectsid = Convert.ToInt32( this.dgv_Project.CurrentRow.Cells["ProjectId"].Value);
            //string projectsname = this.dgv_Project.CurrentRow.Cells["ProjectName"].Value.ToString(); 
            Projects model = null;
            for (int i = 0; i < this.projectsList.Count ; i++)
            {
                if (projectsList[i].ProjectId == projectsid)
                {
                    model = projectsList[i];
                    break;
                }
            }
            //显示修改窗体
            FrmProjects frm = new FrmProjects(model);
            DialogResult result = frm.ShowDialog();
            //如果修改成功
            if (result == DialogResult.OK)
            {
                model.ProjectName = frm.Tag.ToString();// 显示修改后的名称
                this.dgv_Project.Refresh();
            }
        }
        #endregion

        #region 项目删除
        private void btn_DeleteProject_Click(object sender, EventArgs e)
        {
            //获取项目ID
            int projectsid = Convert.ToInt32(this.dgv_Project.CurrentRow.Cells["ProjectId"].Value);
            Projects deletproject = null;
            foreach (Projects item in this.projectsList )
            {
                if (item.ProjectId == projectsid)
                {
                    deletproject = item;
                    break;
                }              
            }
            //删除确认
            if (MessageBox.Show("是否删除？", "删除警告",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;
           //从数据库中删除
            try
            {
                projectsManager.Delete(projectsid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            //从集合中删除
            this.projectsList.Remove(deletproject);
            //更新显示项目列表
            this.dgv_Project.DataSource = null;
            //判断集合中是否有数据（索引-1处没有值）
            if(this.projectsList.Count!=0)
            {
                this.dgv_Project.DataSource = projectsList;
            }
            
        }
        #endregion
    }
}
