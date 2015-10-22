using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using XHD.DBUtility;//Please add references
namespace XHD.DAL
{
	/// <summary>
	/// ���ݷ�����:CRM_order_details
	/// </summary>
	public partial class CRM_order_details
	{
		public CRM_order_details()
		{}
		#region  Method



		/// <summary>
		/// ����һ������
		/// </summary>
		public void Add(XHD.Model.CRM_order_details model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CRM_order_details(");
            strSql.Append("order_id,product_id,product_name,price,base_price,quantity,unit,amount,pway)");
			strSql.Append(" values (");
            strSql.Append("@order_id,@product_id,@product_name,@price,@base_price,@quantity,@unit,@amount,@pway)");
			SqlParameter[] parameters = {
					new SqlParameter("@order_id", SqlDbType.Int,4),
					new SqlParameter("@product_id", SqlDbType.Int,4),
					new SqlParameter("@product_name", SqlDbType.VarChar,250),
					new SqlParameter("@price", SqlDbType.Float,8),
					new SqlParameter("@base_price", SqlDbType.Float,8),
					new SqlParameter("@quantity", SqlDbType.Int,4),
					new SqlParameter("@unit", SqlDbType.VarChar,250),
					new SqlParameter("@amount", SqlDbType.Float,8),
					new SqlParameter("@pway", SqlDbType.VarChar,250)};
			parameters[0].Value = model.order_id;
			parameters[1].Value = model.product_id;
            parameters[2].Value = model.product_name;
            parameters[3].Value = model.price;
			parameters[4].Value = model.base_price;
			parameters[5].Value = model.quantity;
			parameters[6].Value = model.unit;
			parameters[7].Value = model.amount;
            parameters[8].Value = model.pway;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// ����һ������
		/// </summary>
		public bool Update(XHD.Model.CRM_order_details model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CRM_order_details set ");
			strSql.Append("order_id=@order_id,");
			strSql.Append("product_id=@product_id,");
			strSql.Append("product_name=@product_name,");
            strSql.Append("price=@price,");
            strSql.Append("base_price=@base_price,");
			strSql.Append("quantity=@quantity,");
			strSql.Append("unit=@unit,");
			strSql.Append("amount=@amount,");
            strSql.Append("pway=@pway");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@order_id", SqlDbType.Int,4),
					new SqlParameter("@product_id", SqlDbType.Int,4),
					new SqlParameter("@product_name", SqlDbType.VarChar,250),
					new SqlParameter("@price", SqlDbType.Float,8),
					new SqlParameter("@base_price", SqlDbType.Float,8),
					new SqlParameter("@quantity", SqlDbType.Int,4),
					new SqlParameter("@unit", SqlDbType.VarChar,250),
					new SqlParameter("@amount", SqlDbType.Float,8),
					new SqlParameter("@pway", SqlDbType.VarChar,250)};
			parameters[0].Value = model.order_id;
			parameters[1].Value = model.product_id;
            parameters[2].Value = model.product_name;
            parameters[3].Value = model.price;
			parameters[4].Value = model.base_price;
			parameters[5].Value = model.quantity;
			parameters[6].Value = model.unit;
            parameters[7].Value = model.amount;
            parameters[8].Value = model.pway;

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
		/// ɾ��һ������
		/// </summary>
		public bool Delete(string wherestr)
		{
			//�ñ���������Ϣ�����Զ�������/�����ֶ�
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CRM_order_details ");
			strSql.Append(" where "+wherestr);
			SqlParameter[] parameters = {
};

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
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select order_id,product_id,product_name,price,base_price,quantity,unit,amount,pway ");
			strSql.Append(" FROM CRM_order_details ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" order_id,product_id,product_name,price,base_price,quantity,unit,amount,pway ");
			strSql.Append(" FROM CRM_order_details ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
		#endregion  Method
	}
}
