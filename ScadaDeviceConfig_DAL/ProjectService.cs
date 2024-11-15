using ScadaDeviceConfig_Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaDeviceConfig_DAL
{
    /// <summary>
    /// 项目数据访问类
    /// </summary>
    public class ProjectService
    {
        /// <summary>
        /// 添加项目   
        /// </summary>
        /// <param name="ScadaDeviceConfig_Models"></param>
        /// <returns></returns>
        public int Insert(Projects ScadaDeviceConfig_Models)
        {
            string sql = "INSERT INTO Projects (ProjectName) VALUES (@ProjectName); SELECT @@Identity";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ProjectName",ScadaDeviceConfig_Models.ProjectName)
            };
            return Convert.ToInt32(SQLHelper.ExecuteScalar(sql, param));
        }
        /// <summary>
        /// 验证项目名是否重复
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public bool IsRepeatForInsert(string pName)
        {
            string sql = "select count(*) from Projects where ProjectName=@pName";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@pName",pName)
            };
            object obj = SQLHelper.ExecuteScalar(sql, param);
            return Convert.ToInt32(obj) > 0;
        }

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="ScadaDeviceConfig_Models"></param>
        /// <returns></returns>
        public int Update(Projects ScadaDeviceConfig_Models)
        {
            string sql = "UPDATE Projects SET ProjectName=@ProjectName WHERE ProjectId=@ProjectId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ProjectName",ScadaDeviceConfig_Models.ProjectName),
                new SqlParameter("@ProjectId",ScadaDeviceConfig_Models.ProjectId)
            };
            return SQLHelper.ExecuteNonQuery(sql, param);
        }

        /// <summary>
        /// 验证项目名是否重复
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        public bool IsRepeatForUpdate(string pName, int pId)
        {
            string sql = "select count(*) from Projects where ProjectName=@pName and ProjectId<>@ProjectId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@pName",pName),
                new SqlParameter("@ProjectId",pId)
            };
            object obj = SQLHelper.ExecuteScalar(sql, param);
            return Convert.ToInt32(obj) > 0;
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public int Delete(int pId)
        {
            string sql = "delete from Projects where ProjectId=@ProjectId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ProjectId",pId)
            };
            return SQLHelper.ExecuteNonQuery(sql, param);
        }

        /// <summary>
        /// 查询项目信息
        /// </summary>
        /// <returns></returns>
        public List<Projects> Query()
        {
            string sql = "select ProjectName,ProjectId from Projects order by ProjectId ASC";
            SqlDataReader reader = SQLHelper.ExecuteReader(sql);
            List<Projects> list = new List<Projects>();
            int sn = 0;
            while (reader.Read())
            {
                sn++;
                list.Add(new Projects()
                {
                    SN = sn,
                    ProjectId = (int)reader["ProjectId"],
                    ProjectName = reader["ProjectName"].ToString()
                });
            }
            reader.Close(); 
            return list;
        }
        
       
    }
}
