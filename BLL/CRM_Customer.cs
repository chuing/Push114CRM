using System;
using System.Data;
using System.Collections.Generic;
using XHD.Common;
using XHD.Model;
namespace XHD.BLL
{
	/// <summary>
	/// CRM_Customer
	/// </summary>
	public partial class CRM_Customer
	{
		private readonly XHD.DAL.CRM_Customer dal=new XHD.DAL.CRM_Customer();
		public CRM_Customer()
		{}
		#region  Method

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(XHD.Model.CRM_Customer model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(XHD.Model.CRM_Customer model)
		{
			return dal.Update(model);
		}

        /// <summary>
        /// �������пͻ�����
        /// </summary>
        public void UpdateCustomer(XHD.Model.CRM_Customer model)
        {
            dal.UpdateCustomer(model);
        }
        
        /// <summary>
        /// ת�ƿͻ�����
        /// </summary>
        public int CustomerMove(XHD.Model.CRM_Customer model)
        {
            return dal.CustomerMove(model);
        }
        /// <summary>
        /// ת�ƿͻ����ݻع�
        /// </summary>
        public int CustomerMoveBack(XHD.Model.CRM_Customer model)
        {
            return dal.CustomerMoveBack(model);
        }

        /// <summary>
        /// ���º�����
        /// </summary>
        public bool UpdateFollow(XHD.Model.CRM_Customer model)
        {
            return dal.UpdateFollow(model);
        }

        /// <summary>
        /// ���Ѷ�
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isDelete"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool SetRead(int id, int empid)
        {
            return dal.SetRead(id, empid);
        }

        /// <summary>
        /// ����ת
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isDelete"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool SetImport(int id, int empid)
        {
            return dal.SetImport(id, empid);
        }

        public bool UpdateImp(string id, string imp)
        {
            return dal.UpdateImp(id, imp);
        }

        /// <summary>
        /// ����ת���ŵ���Ϣ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isDelete"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool UpdateToPost(XHD.Model.CRM_Customer model)
        {
            return dal.UpdateToPost(model);
        }

		/// <summary>
		/// Ԥɾ��
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
		/// ɾ��һ������
		/// </summary>
		public bool Delete(int id)
		{
			
			return dal.Delete(id);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public XHD.Model.CRM_Customer GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
		/// </summary>
		public XHD.Model.CRM_Customer GetModelByCache(int id)
		{
			
			string CacheKey = "CRM_CustomerModel-" + id;
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
			return (XHD.Model.CRM_Customer)objModel;
		}

        /// <summary>
        /// �����������
        /// </summary>
        public DataSet GetListTotal(string strWhere)
        {
            return dal.GetListTotal(strWhere);
        }

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<XHD.Model.CRM_Customer> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<XHD.Model.CRM_Customer> DataTableToList(DataTable dt)
		{
			List<XHD.Model.CRM_Customer> modelList = new List<XHD.Model.CRM_Customer>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				XHD.Model.CRM_Customer model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new XHD.Model.CRM_Customer();
					if(dt.Rows[n]["id"]!=null && dt.Rows[n]["id"].ToString()!="")
					{
						model.id=int.Parse(dt.Rows[n]["id"].ToString());
					}
					if(dt.Rows[n]["Serialnumber"]!=null && dt.Rows[n]["Serialnumber"].ToString()!="")
					{
					model.Serialnumber=dt.Rows[n]["Serialnumber"].ToString();
					}
					if(dt.Rows[n]["Customer"]!=null && dt.Rows[n]["Customer"].ToString()!="")
					{
					model.Customer=dt.Rows[n]["Customer"].ToString();
					}
					if(dt.Rows[n]["address"]!=null && dt.Rows[n]["address"].ToString()!="")
					{
					model.address=dt.Rows[n]["address"].ToString();
					}
					if(dt.Rows[n]["tel"]!=null && dt.Rows[n]["tel"].ToString()!="")
					{
					model.tel=dt.Rows[n]["tel"].ToString();
					}
					if(dt.Rows[n]["fax"]!=null && dt.Rows[n]["fax"].ToString()!="")
					{
					model.fax=dt.Rows[n]["fax"].ToString();
					}
					if(dt.Rows[n]["site"]!=null && dt.Rows[n]["site"].ToString()!="")
					{
					model.site=dt.Rows[n]["site"].ToString();
					}
					if(dt.Rows[n]["industry"]!=null && dt.Rows[n]["industry"].ToString()!="")
					{
					model.industry=dt.Rows[n]["industry"].ToString();
					}
					if(dt.Rows[n]["Provinces_id"]!=null && dt.Rows[n]["Provinces_id"].ToString()!="")
					{
						model.Provinces_id=int.Parse(dt.Rows[n]["Provinces_id"].ToString());
					}
					if(dt.Rows[n]["Provinces"]!=null && dt.Rows[n]["Provinces"].ToString()!="")
					{
					model.Provinces=dt.Rows[n]["Provinces"].ToString();
					}
					if(dt.Rows[n]["City_id"]!=null && dt.Rows[n]["City_id"].ToString()!="")
					{
						model.City_id=int.Parse(dt.Rows[n]["City_id"].ToString());
					}
					if(dt.Rows[n]["City"]!=null && dt.Rows[n]["City"].ToString()!="")
					{
					model.City=dt.Rows[n]["City"].ToString();
					}
					if(dt.Rows[n]["CustomerType_id"]!=null && dt.Rows[n]["CustomerType_id"].ToString()!="")
					{
						model.CustomerType_id=int.Parse(dt.Rows[n]["CustomerType_id"].ToString());
					}
					if(dt.Rows[n]["CustomerType"]!=null && dt.Rows[n]["CustomerType"].ToString()!="")
					{
					model.CustomerType=dt.Rows[n]["CustomerType"].ToString();
					}
					if(dt.Rows[n]["CustomerLevel_id"]!=null && dt.Rows[n]["CustomerLevel_id"].ToString()!="")
					{
						model.CustomerLevel_id=int.Parse(dt.Rows[n]["CustomerLevel_id"].ToString());
					}
					if(dt.Rows[n]["CustomerLevel"]!=null && dt.Rows[n]["CustomerLevel"].ToString()!="")
					{
					model.CustomerLevel=dt.Rows[n]["CustomerLevel"].ToString();
					}
					if(dt.Rows[n]["CustomerSource_id"]!=null && dt.Rows[n]["CustomerSource_id"].ToString()!="")
					{
						model.CustomerSource_id=int.Parse(dt.Rows[n]["CustomerSource_id"].ToString());
					}
					if(dt.Rows[n]["CustomerSource"]!=null && dt.Rows[n]["CustomerSource"].ToString()!="")
					{
					model.CustomerSource=dt.Rows[n]["CustomerSource"].ToString();
					}
					if(dt.Rows[n]["DesCripe"]!=null && dt.Rows[n]["DesCripe"].ToString()!="")
					{
					model.DesCripe=dt.Rows[n]["DesCripe"].ToString();
					}
					if(dt.Rows[n]["Remarks"]!=null && dt.Rows[n]["Remarks"].ToString()!="")
					{
					model.Remarks=dt.Rows[n]["Remarks"].ToString();
					}
					if(dt.Rows[n]["Department_id"]!=null && dt.Rows[n]["Department_id"].ToString()!="")
					{
						model.Department_id=int.Parse(dt.Rows[n]["Department_id"].ToString());
					}
					if(dt.Rows[n]["Department"]!=null && dt.Rows[n]["Department"].ToString()!="")
					{
					model.Department=dt.Rows[n]["Department"].ToString();
					}
					if(dt.Rows[n]["Employee_id"]!=null && dt.Rows[n]["Employee_id"].ToString()!="")
					{
						model.Employee_id=int.Parse(dt.Rows[n]["Employee_id"].ToString());
					}
					if(dt.Rows[n]["Employee"]!=null && dt.Rows[n]["Employee"].ToString()!="")
					{
					model.Employee=dt.Rows[n]["Employee"].ToString();
					}
					if(dt.Rows[n]["privatecustomer"]!=null && dt.Rows[n]["privatecustomer"].ToString()!="")
					{
					model.privatecustomer=dt.Rows[n]["privatecustomer"].ToString();
					}
					if(dt.Rows[n]["lastfollow"]!=null && dt.Rows[n]["lastfollow"].ToString()!="")
					{
						model.lastfollow=DateTime.Parse(dt.Rows[n]["lastfollow"].ToString());
					}
					if(dt.Rows[n]["Create_id"]!=null && dt.Rows[n]["Create_id"].ToString()!="")
					{
						model.Create_id=int.Parse(dt.Rows[n]["Create_id"].ToString());
					}
					if(dt.Rows[n]["Create_name"]!=null && dt.Rows[n]["Create_name"].ToString()!="")
					{
					model.Create_name=dt.Rows[n]["Create_name"].ToString();
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
                    if (dt.Rows[n]["isRead"] != null && dt.Rows[n]["isRead"].ToString() != "")
                    {
                        model.isRead = int.Parse(dt.Rows[n]["isRead"].ToString());
                    }
                    if (dt.Rows[n]["isImport"] != null && dt.Rows[n]["isImport"].ToString() != "")
                    {
                        model.isImport = int.Parse(dt.Rows[n]["isImport"].ToString());
                    }
                    if (dt.Rows[n]["Follow_up_dep_id"] != null && dt.Rows[n]["Follow_up_dep_id"].ToString() != "")
                    {
                        model.Follow_up_dep_id = int.Parse(dt.Rows[n]["Follow_up_dep_id"].ToString());
                    }
                    if (dt.Rows[n]["Follow_up_dep"] != null && dt.Rows[n]["Follow_up_dep"].ToString() != "")
                    {
                        model.Follow_up_dep = dt.Rows[n]["Follow_up_dep"].ToString();
                    }
                    if (dt.Rows[n]["Follow_up_id"] != null && dt.Rows[n]["Follow_up_id"].ToString() != "")
                    {
                        model.Follow_up_id = int.Parse(dt.Rows[n]["Follow_up_id"].ToString());
                    }
                    if (dt.Rows[n]["Follow_up"] != null && dt.Rows[n]["Follow_up"].ToString() != "")
                    {
                        model.Follow_up = dt.Rows[n]["Follow_up"].ToString();
                    }
                    if (dt.Rows[n]["To_follow_id"] != null && dt.Rows[n]["To_follow_id"].ToString() != "")
                    {
                        model.To_follow_id = int.Parse(dt.Rows[n]["To_follow_id"].ToString());
                    }
                    if (dt.Rows[n]["To_follow"] != null && dt.Rows[n]["To_follow"].ToString() != "")
                    {
                        model.To_follow = dt.Rows[n]["To_follow"].ToString();
                    }
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// ��ҳ��ȡ�����б�
		/// </summary>
		public DataSet GetList(int PageSize, int PageIndex, string strWhere, string filedOrder, out string Total)
		{
			return dal.GetList(PageSize, PageIndex, strWhere, filedOrder, out Total);
		}
        
        /// <summary>
        /// ����������
        /// </summary>
        public bool UpdateLastFollow(string id)
        {
            return dal.UpdateLastFollow(id);
        }

        public bool UpdateTop(string id, string top)
        {
            return dal.UpdateTop(id, top);
        }
        public DataSet Reports_year(string items, int year, string where)
        {
            return dal.Reports_year(items, year, where);
        }

        /// <summary>
        /// ͬ�Ȼ��ȡ��ͻ�������
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
        /// �ͻ�����ͳ��
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
