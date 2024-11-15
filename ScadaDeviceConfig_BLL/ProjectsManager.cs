using ScadaDeviceConfig_DAL;
using ScadaDeviceConfig_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaDeviceConfig_BLL
{
    public class ProjectsManager
    {
        private ProjectService _projectService = new ProjectService();
        
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="ScadaDeviceConfig_Models"></param>
        /// <returns></returns>
        public int Insert(Projects ScadaDeviceConfig_Models)
        {
            return _projectService.Insert(ScadaDeviceConfig_Models);
        }

        /// <summary>
        /// 添加项目，验证项目名称是否重复
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public bool IsRepeatProjectInsert(string pName)
        {
            return _projectService.IsRepeatForInsert(pName);
        }

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="ScadaDeviceConfig_Models"></param>
        /// <returns></returns>
        public int Update(Projects ScadaDeviceConfig_Models)
        {
            return _projectService.Update(ScadaDeviceConfig_Models);
        }
        /// <summary>
        /// 修改项目，验证项目名称是否重复
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        public bool IsRepeatForUpdate(string pName, int pId)
        {
            return _projectService.IsRepeatForUpdate(pName, pId);
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public int Delete(int projectId)
        {
            return _projectService.Delete(projectId);
        }

        /// <summary>
        /// 查询项目
        /// </summary>
        /// <returns></returns>
        public List<Projects>Query() 
        { 
            return _projectService.Query(); 
        }

    }
}
