
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaDeviceConfig_Models
{
    /// <summary>
    /// 项目信息
    /// </summary>
    public class Projects
    {
        public int SN { get; set; }  //仅供显示需要
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}
