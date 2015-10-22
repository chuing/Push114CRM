using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using XHD.DBUtility;//Please add references
namespace XHD.DAL
{
    /// <summary>
    /// 数据访问类:Ext_Task
    /// </summary>
    public partial class Ext_Task
    {
        public Ext_Task()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "Ext_Task");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Ext_Task");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XHD.Model.Ext_Task model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Ext_Task(");
            strSql.Append("Serialnumber,SiteName,Url,DesCripe,Department_id,Department,Employee_id,Employee,Create_id,Create_name,Create_date,isDelete,Delete_time)");
            strSql.Append(" values (");
            strSql.Append("@Serialnumber,@SiteName,@Url,@DesCripe,@Department_id,@Department,@Employee_id,@Employee,@Create_id,@Create_name,@Create_date,@isDelete,@Delete_time)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Serialnumber", SqlDbType.VarChar,250),
					new SqlParameter("@SiteName", SqlDbType.VarChar,250),
					new SqlParameter("@Url", SqlDbType.VarChar,250),
					new SqlParameter("@DesCripe", SqlDbType.VarChar,4000),
					new SqlParameter("@Department_id", SqlDbType.Int,4),
					new SqlParameter("@Department", SqlDbType.VarChar,250),
					new SqlParameter("@Employee_id", SqlDbType.Int,4),
					new SqlParameter("@Employee", SqlDbType.VarChar,250),
					new SqlParameter("@Create_id", SqlDbType.Int,4),
					new SqlParameter("@Create_name", SqlDbType.VarChar,250),
					new SqlParameter("@Create_date", SqlDbType.DateTime),
					new SqlParameter("@isDelete", SqlDbType.Int,4),
					new SqlParameter("@Delete_time", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.Serialnumber;
            parameters[1].Value = model.SiteName;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.DesCripe;
            parameters[4].Value = model.Department_id;
            parameters[5].Value = model.Department;
            parameters[6].Value = model.Employee_id;
            parameters[7].Value = model.Employee;
            parameters[8].Value = model.Create_id;
            parameters[9].Value = model.Create_name;
            parameters[10].Value = model.Create_date;
            parameters[11].Value = model.isDelete;
            parameters[12].Value = model.Delete_time;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(XHD.Model.Ext_Task model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Ext_Task set ");
			strSql.Append("Serialnumber=@Serialnumber,");
			strSql.Append("SiteName=@SiteName,");
			strSql.Append("Url=@Url,");
			strSql.Append("DesCripe=@DesCripe");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@Serialnumber", SqlDbType.VarChar,250),
					new SqlParameter("@SiteName", SqlDbType.VarChar,250),
					new SqlParameter("@Url", SqlDbType.VarChar,250),
					new SqlParameter("@DesCripe", SqlDbType.VarChar,4000),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.Serialnumber;
			parameters[1].Value = model.SiteName;
			parameters[2].Value = model.Url;
			parameters[3].Value = model.DesCripe;
			parameters[4].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
        
        /// <summary>
        /// 预删除
        /// </summary>
        public bool AdvanceDelete(int id, int isDelete, string time)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Ext_Task set ");
            strSql.Append("isDelete=" + isDelete);
            strSql.Append(",Delete_time='" + time + "'");
            strSql.Append(" where id=" + id);
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ext_Task ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Ext_Task ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XHD.Model.Ext_Task GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select  top 1 id,Serialnumber,SiteName,Url,DesCripe,Department_id,Department,Employee_id,Employee,Create_id,Create_name,Create_date,isDelete,Delete_time from Ext_Task ");
            strSql.Append("select  top 1 * from Ext_Task ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
};
            parameters[0].Value = id;

            XHD.Model.Ext_Task model = new XHD.Model.Ext_Task();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Serialnumber"] != null && ds.Tables[0].Rows[0]["Serialnumber"].ToString() != "")
                {
                    model.Serialnumber = ds.Tables[0].Rows[0]["Serialnumber"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SiteName"] != null && ds.Tables[0].Rows[0]["SiteName"].ToString() != "")
                {
                    model.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Url"] != null && ds.Tables[0].Rows[0]["Url"].ToString() != "")
                {
                    model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DesCripe"] != null && ds.Tables[0].Rows[0]["DesCripe"].ToString() != "")
                {
                    model.DesCripe = ds.Tables[0].Rows[0]["DesCripe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Department_id"] != null && ds.Tables[0].Rows[0]["Department_id"].ToString() != "")
                {
                    model.Department_id = int.Parse(ds.Tables[0].Rows[0]["Department_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Department"] != null && ds.Tables[0].Rows[0]["Department"].ToString() != "")
                {
                    model.Department = ds.Tables[0].Rows[0]["Department"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Employee_id"] != null && ds.Tables[0].Rows[0]["Employee_id"].ToString() != "")
                {
                    model.Employee_id = int.Parse(ds.Tables[0].Rows[0]["Employee_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Employee"] != null && ds.Tables[0].Rows[0]["Employee"].ToString() != "")
                {
                    model.Employee = ds.Tables[0].Rows[0]["Employee"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Create_id"] != null && ds.Tables[0].Rows[0]["Create_id"].ToString() != "")
                {
                    model.Create_id = int.Parse(ds.Tables[0].Rows[0]["Create_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Create_name"] != null && ds.Tables[0].Rows[0]["Create_name"].ToString() != "")
                {
                    model.Create_name = ds.Tables[0].Rows[0]["Create_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Create_date"] != null && ds.Tables[0].Rows[0]["Create_date"].ToString() != "")
                {
                    model.Create_date = DateTime.Parse(ds.Tables[0].Rows[0]["Create_date"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isDelete"] != null && ds.Tables[0].Rows[0]["isDelete"].ToString() != "")
                {
                    model.isDelete = int.Parse(ds.Tables[0].Rows[0]["isDelete"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Delete_time"] != null && ds.Tables[0].Rows[0]["Delete_time"].ToString() != "")
                {
                    model.Delete_time = DateTime.Parse(ds.Tables[0].Rows[0]["Delete_time"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListTotal(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) as Total from Ext_Task ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select id,Serialnumber,SiteName,Url,DesCripe,Department_id,Department,Employee_id,Employee,Create_id,Create_name,Create_date,isDelete,Delete_time ");
            strSql.Append("select * ");
            strSql.Append(" FROM Ext_Task ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            //strSql.Append(" id,Serialnumber,SiteName,Url,DesCripe,Department_id,Department,Employee_id,Employee,Create_id,Create_name,Create_date,isDelete,Delete_time ");
            strSql.Append(" * ");
            strSql.Append(" FROM Ext_Task ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize, int PageIndex, string strWhere, string filedOrder, out string Total)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            strSql.Append("select top " + PageSize + " * FROM Ext_Task WHERE id not in ( SELECT top " + (PageIndex - 1) * PageSize + " id FROM Ext_Task where " + strWhere + " order by " + filedOrder + " ) ");
            strSql1.Append(" select count(id) FROM Ext_Task ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and (" + strWhere + ")");
                strSql1.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            Total = DbHelperSQL.Query(strSql1.ToString()).Tables[0].Rows[0][0].ToString();
            return DbHelperSQL.Query(strSql.ToString());

        }

        public DataSet Reports_year(string items, int year, string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("if OBJECT_ID('Tempdb..#t') is not null ");
            strSql.Append("    drop TABLE  #t ");
            //strSql.Append("go");
            strSql.Append(" begin ");
            //strSql.Append("    --预统计表 #t");
            strSql.Append("    select ");
            strSql.Append("        " + items + ",'m'+convert(varchar,month(Create_date)) mm,count(id)tNum into #t ");
            strSql.Append("    from dbo.Ext_Task ");
            strSql.Append("    where datediff(YEAR,[Create_date],'" + year + "-1-1')=0 ");
            if (where.Trim() != "")
            {
                strSql.Append(" and " + where);
            }
            strSql.Append("    group by " + items + ",'m'+convert(varchar,month(Create_date)) ");

            //strSql.Append("    --生成SQL");
            strSql.Append("    declare @sql varchar(8000) ");
            strSql.Append("    set @sql='select " + items + " items ' ");
            strSql.Append("    select @sql = @sql + ',sum(case mm when ' + char(39) +mm+ char(39) + ' then tNum else 0 end) ['+ mm +']' ");
            strSql.Append("        from (select distinct mm from #t)as data ");
            strSql.Append("    set @sql = @sql + ' from #t group by " + items + "' ");

            strSql.Append("    exec(@sql) ");
            strSql.Append(" end ");
            //strSql.Append("go");

            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 同比环比【客户新增】
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public DataSet Compared(DateTime dt1, DateTime dt2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(id) as yy,");
            strSql.Append(" SUM(case when YEAR( Create_date)=YEAR('" + dt1 + "') and MONTH(Create_date)=month('" + dt1 + "') then 1 else 0 end) as dt1, ");
            strSql.Append(" SUM(case when YEAR( Create_date)=YEAR('" + dt2 + "') and MONTH(Create_date)=month('" + dt2 + "') then 1 else 0 end) as dt2 ");
            strSql.Append(" FROM Ext_Task");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 客户类型【同比环比】
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public DataSet Compared_type(DateTime dt1, DateTime dt2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select CustomerType as yy,count(CustomerType) as xx,");
            strSql.Append(" SUM(case when YEAR( Create_date)=YEAR('" + dt1 + "') and MONTH(Create_date)=month('" + dt1 + "') then 1 else 0 end) as dt1, ");
            strSql.Append(" SUM(case when YEAR( Create_date)=YEAR('" + dt2 + "') and MONTH(Create_date)=month('" + dt2 + "') then 1 else 0 end) as dt2 ");
            strSql.Append(" FROM Ext_Task group by CustomerType");

            return DbHelperSQL.Query(strSql.ToString());

        }

        /// <summary>
        /// 客户级别【同比环比】
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public DataSet Compared_level(DateTime dt1, DateTime dt2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select CustomerLevel as yy,count(CustomerLevel) as xx,");
            strSql.Append(" SUM(case when YEAR( Create_date)=YEAR('" + dt1 + "') and MONTH(Create_date)=month('" + dt1 + "') then 1 else 0 end) as dt1, ");
            strSql.Append(" SUM(case when YEAR( Create_date)=YEAR('" + dt2 + "') and MONTH(Create_date)=month('" + dt2 + "') then 1 else 0 end) as dt2 ");
            strSql.Append(" FROM Ext_Task group by CustomerLevel");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 客户来源【同比环比】
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public DataSet Compared_source(DateTime dt1, DateTime dt2)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select CustomerSource as yy,count(CustomerSource) as xx,");
            strSql.Append(" SUM(case when YEAR( Create_date)=YEAR('" + dt1 + "') and MONTH(Create_date)=month('" + dt1 + "') then 1 else 0 end) as dt1, ");
            strSql.Append(" SUM(case when YEAR( Create_date)=YEAR('" + dt2 + "') and MONTH(Create_date)=month('" + dt2 + "') then 1 else 0 end) as dt2 ");
            strSql.Append(" FROM Ext_Task group by CustomerSource");


            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet Compared_empcusadd(DateTime dt1, DateTime dt2, string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select hr_employee.name as yy,");
            strSql.Append(" SUM(case when YEAR( Ext_Task.Create_date)=YEAR('" + dt1 + "') and MONTH(Ext_Task.create_date)=month('" + dt1 + "') then 1 else 0 end) as dt1, ");
            strSql.Append(" SUM(case when YEAR( Ext_Task.Create_date)=YEAR('" + dt2 + "') and MONTH(Ext_Task.create_date)=month('" + dt2 + "') then 1 else 0 end) as dt2 ");
            strSql.Append(" FROM hr_employee left outer join  Ext_Task ");
            strSql.Append(" on hr_employee.ID=Ext_Task.Create_id ");
            strSql.Append(" where hr_employee.ID in " + idlist);
            strSql.Append(" group by hr_employee.name,hr_employee.ID ");
            strSql.Append(" order by hr_employee.ID ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 客户新增统计
        /// </summary>
        public DataSet report_empcus(int year, string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select name,yy,isnull([1],0) as 'm1',isnull([2],0) as 'm2',isnull([3],0) as 'm3',isnull([4],0) as 'm4',isnull([5],0) as 'm5',isnull([6],0) as 'm6',");
            strSql.Append(" isnull([7],0) as 'm7',isnull([8],0) as 'm8',isnull([9],0) as 'm9',isnull([10],0) as 'm10',isnull([11],0) as 'm11',isnull([12],0) as 'm12' ");
            strSql.Append(" from");
            strSql.Append(" (SELECT   hr_employee.ID, hr_employee.name, COUNT(derivedtbl_1.id) AS cn, YEAR(derivedtbl_1.Create_date) AS yy, ");
            strSql.Append(" MONTH(derivedtbl_1.Create_date) AS mm");
            strSql.Append(" FROM      hr_employee LEFT OUTER JOIN");
            strSql.Append("  (SELECT   id, Create_id, Create_date");
            strSql.Append("  FROM      Ext_Task");
            strSql.Append("  WHERE ISNULL(isdelete,0)=0 and  (YEAR(Create_date) = " + year + ")) AS derivedtbl_1 ON hr_employee.ID = derivedtbl_1.Create_id");
            strSql.Append(" WHERE hr_employee.ID in " + idlist);
            strSql.Append(" GROUP BY hr_employee.ID, hr_employee.name, YEAR(derivedtbl_1.Create_date), MONTH(derivedtbl_1.Create_date)) as tt");
            strSql.Append(" pivot");
            strSql.Append(" (sum(cn) for mm in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12]))");
            strSql.Append(" as pvt");

            return DbHelperSQL.Query(strSql.ToString());
        }
        
        #endregion  Method
    }
}

