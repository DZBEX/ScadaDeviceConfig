
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaDeviceConfig_Models
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class Equipments
    {
        public int SN { get; set; }
        public int EquipmentId { get; set; }//设备Id
        public int ProjectId { get; set; }//项目Id
        public string EquipmentName { get; set; }//设备名称
        public int ETypeId { get; set; }
        public int PTypeId { get; set; }

        //以下内容在UI中是选填项，所以要给默认值
        public string IPAddress { get; set; } = string.Empty;
        public string PortNo { get; set; } = string.Empty;   //端口号（针对以太网） 
        public string SerialNo { get; set; } = string.Empty;   //串口号 （针对串口）
        public int BaudRate { get; set; } = 0;//波特率（针对串口）
        public int DataBit { get; set; } = 0;//数据位（针对串口）
        public string ParityBit { get; set; } = string.Empty;//校验位（针对串口）
        public int StopBit { get; set; } = 0;//停止位（针对串口）      
        public string OPCNodeName { get; set; } = string.Empty; //节点名称：针对OPC
        public string OPCServerName { get; set; } = string.Empty; //服务名称：针对OPC
        public int IsEnable { get; set; } = 0;//否启用
        public string Comments { get; set; } = string.Empty; //备注

        //扩展属性1（不用给默认值）        
        public string ETypeName { get; set; }//设备类型
        public string PTypeName { get; set; }//协议类型

    }
}
