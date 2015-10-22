using System;
using System.Data;
using System.Collections.Generic;
using XHD.Common;
using XHD.Model;
namespace XHD.BLL
{
	/// <summary>
	/// hr_post
	/// </summary>
	public partial class hr_post
	{
		private readonly XHD.DAL.hr_post dal=new XHD.DAL.hr_post();
		public hr_post()
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
		public bool Exists(int post_id)
		{
			return dal.Exists(post_id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(XHD.Model.hr_post model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(XHD.Model.hr_post model)
		{
			return dal.Update(model);
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
		public bool Delete(int post_id)
		{
			
			return dal.Delete(post_id);
		}
		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public bool DeleteList(string post_idlist )
		{
			return dal.DeleteList(post_idlist );
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public XHD.Model.hr_post GetModel(int post_id)
		{
			
			return dal.GetModel(post_id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ�����
		/// </summary>
		public XHD.Model.hr_post GetModelByCache(int post_id)
		{
			
			string CacheKey = "hr_postModel-" + post_id;
			object objModel = XHD.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(post_id);
					if (objModel != null)
					{
						int ModelCache = XHD.Common.ConfigHelper.GetConfigInt("ModelCache");
						XHD.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (XHD.Model.hr_post)objModel;
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
		public List<XHD.Model.hr_post> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<XHD.Model.hr_post> DataTableToList(DataTable dt)
		{
			List<XHD.Model.hr_post> modelList = new List<XHD.Model.hr_post>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				XHD.Model.hr_post model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new XHD.Model.hr_post();
					if(dt.Rows[n]["post_id"]!=null && dt.Rows[n]["post_id"].ToString()!="")
					{
						model.post_id=int.Parse(dt.Rows[n]["post_id"].ToString());
					}
					if(dt.Rows[n]["post_name"]!=null && dt.Rows[n]["post_name"].ToString()!="")
					{
					model.post_name=dt.Rows[n]["post_name"].ToString();
					}
					if(dt.Rows[n]["position_id"]!=null && dt.Rows[n]["position_id"].ToString()!="")
					{
						model.position_id=int.Parse(dt.Rows[n]["position_id"].ToString());
					}
					if(dt.Rows[n]["position_name"]!=null && dt.Rows[n]["position_name"].ToString()!="")
					{
					model.position_name=dt.Rows[n]["position_name"].ToString();
					}
					if(dt.Rows[n]["position_order"]!=null && dt.Rows[n]["position_order"].ToString()!="")
					{
					model.position_order=dt.Rows[n]["position_order"].ToString();
					}
					if(dt.Rows[n]["dep_id"]!=null && dt.Rows[n]["dep_id"].ToString()!="")
					{
						model.dep_id=int.Parse(dt.Rows[n]["dep_id"].ToString());
					}
					if(dt.Rows[n]["depname"]!=null && dt.Rows[n]["depname"].ToString()!="")
					{
					model.depname=dt.Rows[n]["depname"].ToString();
					}
					if(dt.Rows[n]["emp_id"]!=null && dt.Rows[n]["emp_id"].ToString()!="")
					{
						model.emp_id=int.Parse(dt.Rows[n]["emp_id"].ToString());
					}
					if(dt.Rows[n]["emp_name"]!=null && dt.Rows[n]["emp_name"].ToString()!="")
					{
					model.emp_name=dt.Rows[n]["emp_name"].ToString();
					}
					if(dt.Rows[n]["default_post"]!=null && dt.Rows[n]["default_post"].ToString()!="")
					{
						model.default_post=int.Parse(dt.Rows[n]["default_post"].ToString());
					}
					if(dt.Rows[n]["note"]!=null && dt.Rows[n]["note"].ToString()!="")
					{
					model.note=dt.Rows[n]["note"].ToString();
					}
					if(dt.Rows[n]["post_descript"]!=null && dt.Rows[n]["post_descript"].ToString()!="")
					{
					model.post_descript=dt.Rows[n]["post_descript"].ToString();
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
        /// ���¸�λ��Ա
        /// </summary>
        public bool UpdatePostEmp(XHD.Model.hr_post model)
        {
            return dal.UpdatePostEmp(model);
        }
        /// <summary>
        /// ��ո��¸�λ��Ա
        /// </summary>
        public bool UpdatePostEmpbyEid(int empid)
        {
            return dal.UpdatePostEmpbyEid(empid);
        }
		#endregion  Method
	}
}
