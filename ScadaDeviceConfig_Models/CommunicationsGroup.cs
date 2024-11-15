using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaDeviceConfig_Models
{
    /// <summary>
    /// 通信组
    /// </summary>
    public class CommunicationsGroup
    {
        public int  SN  { get; set; }
        public int CGId { get; set; }
        public int EquipmentId { get; set; }  
        public string CGName { get; set; }
        public string StartAddress { get; set; }
        public int CGLength { get; set; }
        public int IsEnable { get; set; }
        public string Comments { get; set; }

        //扩展属性
        public string EquipmentName { get; set; }


    }
}
