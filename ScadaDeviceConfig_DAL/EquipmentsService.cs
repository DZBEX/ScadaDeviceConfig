using ScadaDeviceConfig_Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ScadaDeviceConfig_DAL
{
    /// <summary>
    /// 设备数据访问类
    /// </summary>
    public class EquipmentsService
    {

        #region 设备类型和协议类型查询

        /// <summary>
        /// 获取所有的设备类型
        /// </summary>
        /// <returns></returns>
        public List<EquipmentType> GetAllEtype()
        {
            string sql = "select EtypeId,ETypeName from EquipmentType";
            List<EquipmentType> list = new List<EquipmentType>();
            SqlDataReader reader = SQLHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                list.Add(new EquipmentType()
                {
                    ETypeId = (int)reader["EtypeId"],
                    ETypeName = reader["ETypeName"].ToString()
                });
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 根据设备类型获取对应的协议类型
        /// </summary>
        /// <param name="eTyeId"></param>
        /// <returns></returns>
        public List<ProtocolType> GetPTypeByETypes(int eTyeId)
        {
            string sql = "select PTypeld,PTypeName from ProtocolType where ETypeId=@EtypeId";
            List<ProtocolType> list = new List<ProtocolType>();
            SqlParameter [] param = new SqlParameter[] { new SqlParameter("@EtypeId", eTyeId) };
            SqlDataReader reader = SQLHelper.ExecuteReader(sql, param);
            while (reader.Read())
            {
                list.Add(new ProtocolType()
                {
                    PTypeId = (int)reader["PTypeld"],
                    PTypeName = reader["PTypeName"].ToString()
                });
            }
           
            reader.Close();
            return list;
        }
        #endregion

        #region 新增加设备
        public int Insert(Equipments ScadaDeviceConfig_Models)
        {
            string sql = "insert into Equipments(ProjectId, ElypeId, PlypeId, EquipmentName,";
            sql += "IPAddress, PortNo, SerialNo, BaudRate, DataBit, ParityBit, StopBit, OPCNodeName,";
            sql += "OPCServerName, IsEnable, Comments)";
            sql += "values(@ProjectId, @ETypeId, @PTypeId, @EquipmentName,";
            sql += "@IPAddress, @PortNo, @SerialNo, @BaudRate, @DataBit, @ParityBit, @StopBit, @OPCNodeName,";
            sql += "@OPCServerName, @IsEnable, @Comments):SELECT @@Identity";


            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ProjectId",ScadaDeviceConfig_Models.ProjectId),
                new SqlParameter("@ETypeId",ScadaDeviceConfig_Models.ETypeId),
                new SqlParameter("@PTypeId",ScadaDeviceConfig_Models.PTypeId),
                new SqlParameter("@EquipmentName",ScadaDeviceConfig_Models.EquipmentName),
                new SqlParameter("@SerialNo",ScadaDeviceConfig_Models.IPAddress),
                new SqlParameter("@SerialNo",ScadaDeviceConfig_Models.SerialNo),
                new SqlParameter("@BaudRate",ScadaDeviceConfig_Models.BaudRate),
                new SqlParameter("@DataBit",ScadaDeviceConfig_Models.DataBit),
                new SqlParameter("@ParityBit",ScadaDeviceConfig_Models.ParityBit),
                new SqlParameter("@StopBit",ScadaDeviceConfig_Models.StopBit),
                new SqlParameter("@OPCServerName",ScadaDeviceConfig_Models.OPCNodeName),
                new SqlParameter("@OPCServerName",ScadaDeviceConfig_Models.OPCServerName),
                new SqlParameter("@IsEnable",ScadaDeviceConfig_Models.IsEnable),
                new SqlParameter("@Comments",ScadaDeviceConfig_Models.Comments),
                new SqlParameter("@PortNo",ScadaDeviceConfig_Models.PortNo),
            };
            return Convert.ToInt32(SQLHelper.ExecuteScalar(sql, param));
        }

        public bool IsRepeatForInsert(string eName,int pId)
        {
            string sql = "select count(1) from Equipments where EquipmentName=@EquipmentName and ProjectId=@ProjectId";
            SqlParameter[] param = new SqlParameter[] 
            { 
                new SqlParameter("@EquipmentName", eName),
                new SqlParameter("@ProjectId", pId)

            };
            object obj = SQLHelper.ExecuteScalar(sql, param);
            return Convert.ToInt32(obj) > 0;
        }
        #endregion

        #region 查询设备
        public Dictionary<int ,List<Equipments> > QuerEquipments(int projectId)
        {
            string sql = "select EquipmentId, ProjectId, Equipments.ElypeId, Equipments.PlypeId, Equipments.EquipmentName," +
                "IPAddress, PortNo, SerialNo, BaudRate, DataBit, ParityBit, StopBit, " +
                "OPCNodeName, OPCServerName, IsEnable, Comments" +
                "EquipmentType,PTypeName from Equipments";
            sql += "inner join EquipmentType on EquipmentType.ETypeId=Equipments.ETypeId";
            sql += "inner join ProtocolType on ProtocolType.PTypeld=Equipments.PTypeld";
            sql += "where ProjectId=@ProjectId)";
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@ProjectId", projectId) };
            SqlDataReader reader = SQLHelper.ExecuteReader(sql, param);
            //定义3个集合，分别来封装不同设备类型的设备信息
            List<Equipments> list_Internet = new List<Equipments>();
            List<Equipments> list_SerialPort = new List<Equipments>();
            List<Equipments> list_OPC = new List<Equipments>();
            
            //定义字典集合，封装上面3个集合
            Dictionary<int, List<Equipments>> dicResult = new Dictionary<int, List<Equipments>>
            {
                [10]=list_Internet,
                [11]=list_SerialPort,
                [12]=list_OPC           
            };

            while (reader.Read())
            {
                Equipments model = new Equipments();
                //封装公共属性
                model.EquipmentId = (int)reader["EquipmentId"];
                model.EquipmentName = reader["EquipmentName"].ToString();
                model.ETypeId = (int)reader["EquipmentId"];
                model.PTypeId = (int)reader["EquipmentId"];
                model.ProjectId = (int)reader["EquipmentId"];
                model.IsEnable = (int)reader["EquipmentId"];
                model.Comments = reader["EquipmentId"].ToString();
                model.ETypeName = reader["EquipmentId"].ToString();
                model.PTypeName = reader["EquipmentId"].ToString();
                //封装专有属性
                if(model.ETypeId ==10)
                {
                    model.SN =list_Internet.Count+1;
                   
                }


            }



        }







        #endregion



    }
}
