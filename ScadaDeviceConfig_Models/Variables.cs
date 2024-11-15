
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaDeviceConfig_Models
{
    /// <summary>
    /// 变量信息
    /// </summary>
    public class Variables
    {
        public int SN { get; set; }//仅供显示使用
        public int VariableId { get; set; }//变量ID       
        public string VariableName { get; set; }//变量名称
        public int CGId { get; set; }//通信组ID
        public string StartAddress { get; set; }//开始地址
        public string DataType { get; set; }//数据类型
        public bool IsMaxAlarm { get; set; } = false;//是否启用上限报警
        public bool IsMinAlarm { get; set; } = false;//是否启用下限报警
        public string AlarmMax { get; set; } = string.Empty;//报警上限 (实际要求是浮点数)
        public string AlarmMin { get; set; } = string.Empty;//报警下限  (实际要求是浮点数)
        public bool IsFiled { get; set; }//是否归档
        public float Scale { get; set; }//线性比例
        public float Offset { get; set; }//线性偏移

        public string AlarmMaxNote { get; set; } = string.Empty; //报警上限节点说明
        public string AlarmMinNote { get; set; } = string.Empty; //报警下限节点说明

        public string Comments { get; set; }//备注 
    }
}
