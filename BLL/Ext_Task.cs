
using System;
using System.Data;
using System.Collections.Generic;
using XHD.Common;
using XHD.Model;
namespace XHD.BLL
{
	/// <summary>
	/// Ext_Task
	/// </summary>
	public partial class Ext_Task
	{
		private readonly XHD.DAL.Ext_Task dal=new XHD.DAL.Ext_Task();
		public Ext_Task()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(XHD.Model.Ext_Task model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(XHD.Model.Ext_Task model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 预删除
		/// </summary>
		/// <param name="id"></param>
		/// <param name="isDelete"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public bool AdvanceDelete(int id, int isDelete, string time)
		{
			return dal.AdvanceDelete(id, isDelete, time);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XHD.Model.Ext_Task GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public XHD.Model.Ext_Task GetModelByCache(int id)
		{
			
			string CacheKey = "Ext_TaskModel-" + id;
			object objModel = XHD.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(id);
					if (objModel != null)
					{
						int ModelCache = XHD.Common.ConfigHelper.GetConfigInt("ModelCache");
						XHD.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (XHD.Model.Ext_Task)objModel;
		}

        /// <summary>
        /// 获得数据总数
        /// </summary>
        public DataSet GetListTotal(string strWhere)
        {
            return dal.GetListTotal(strWhere);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<XHD.Model.Ext_Task> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<XHD.Model.Ext_Task> DataTableToList(DataTable dt)
		{
			List<XHD.Model.Ext_Task> modelList = new List<XHD.Model.Ext_Task>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				XHD.Model.Ext_Task model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new XHD.Model.Ext_Task();
					if(dt.Rows[n]["id"]!=null && dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["Serialnumber"]!=null && dt.Rows[n]["Serialnumber"].ToString()!="")
					{
                        model.Serialnumber = dt.Rows[n]["Serialnumber"].ToString();
					}
                    if (dt.Rows[n]["SiteName"] != null && dt.Rows[n]["SiteName"].ToString() != "")
					{
                        model.SiteName = dt.Rows[n]["SiteName"].ToString();
					}
                    if (dt.Rows[n]["Url"] != null && dt.Rows[n]["Url"].ToString() != "")
					{
                        model.Url = dt.Rows[n]["Url"].ToString();
					}
					if(dt.Rows[n]["DesCripe"]!=null && dt.Rows[n]["DesCripe"].ToString()!="")
					{
                        model.DesCripe = dt.Rows[n]["DesCripe"].ToString();
					}
					if(dt.Rows[n]["Department_id"]!=null && dt.Rows[n]["Department_id"].ToString()!="")
					{
						model.Department_id=int.Parse(dt.Rows[n]["Department_id"].ToString());
					}
					if(dt.Rows[n]["Department"]!=null && dt.Rows[n]["Department"].ToString()!="")
					{
                        model.Department = dt.Rows[n]["Department"].ToString();
					}
					if(dt.Rows[n]["Employee_id"]!=null && dt.Rows[n]["Employee_id"].ToString()!="")
					{
						model.Employee_id=int.Parse(dt.Rows[n]["Employee_id"].ToString());
					}
					if(dt.Rows[n]["Employee"]!=null && dt.Rows[n]["Employee"].ToString()!="")
					{
                        model.Employee = dt.Rows[n]["Employee"].ToString();
					}
					if(dt.Rows[n]["Create_id"]!=null && dt.Rows[n]["Create_id"].ToString()!="")
					{
						model.Create_id=int.Parse(dt.Rows[n]["Create_id"].ToString());
					}
					if(dt.Rows[n]["Create_name"]!=null && dt.Rows[n]["Create_name"].ToString()!="")
					{
                        model.Create_name = dt.Rows[n]["Create_name"].ToString();
					}
					if(dt.Rows[n]["Create_date"]!=null && dt.Rows[n]["Create_date"].ToString()!="")
					{
						model.Create_date=DateTime.Parse(dt.Rows[n]["Create_date"].ToString());
					}
					if(dt.Rows[n]["isDelete"]!=null && dt.Rows[n]["isDelete"].ToString()!="")
					{
						model.isDelete=int.Parse(dt.Rows[n]["isDelete"].ToString());
					}
					if(dt.Rows[n]["Delete_time"]!=null && dt.Rows[n]["Delete_time"].ToString()!="")
					{
						model.Delete_time=DateTime.Parse(dt.Rows[n]["Delete_time"].ToString());
                    }
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize, int PageIndex, string strWhere, string filedOrder, out string Total)
        {
            return dal.GetList(PageSize, PageIndex, strWhere, filedOrder, out Total);
        }
        
        public DataSet Reports_year(string items, int year, string where)
        {
            return dal.Reports_year(items, year, where);
        }

        /// <summary>
        /// 同比环比【客户新增】
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="project_id"></param>
        /// <returns></returns>
        public DataSet Compared(DateTime dt1, DateTime dt2)
        {
            return dal.Compared(dt1, dt2);
        }

        public DataSet Compared_type(DateTime dt1, DateTime dt2)
        {
            return dal.Compared_type(dt1, dt2);
        }

        public DataSet Compared_level(DateTime dt1, DateTime dt2)
        {
            return dal.Compared_level(dt1, dt2);
        }

        public DataSet Compared_source(DateTime dt1, DateTime dt2)
        {
            return dal.Compared_source(dt1, dt2);
        }

        public DataSet Compared_empcusadd(DateTime dt1, DateTime dt2, string idlist)//, string idlist)
        {
            return dal.Compared_empcusadd(dt1, dt2, idlist);//, idlist);
        }

        /// <summary>
        /// 客户新增统计
        /// </summary>
        /// <param name="year"></param>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public DataSet report_empcus(int year, string idlist)
        {
            return dal.report_empcus(year, idlist);
        }
		#endregion  Method
	}
}

