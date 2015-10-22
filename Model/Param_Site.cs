using System;
namespace XHD.Model
{
	/// <summary>
	/// Param_City:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Param_Site
	{
        public Param_Site()
		{}
		#region Model
		private int _id;
		private int? _parentid;
        private string _site;
        private string _siteurl;
        private int? _department_id;
        private string _department;
        private int? _employee_id;
        private string _employee;
		private int? _create_id;
		private DateTime? _create_date;
		private int? _update_id;
		private DateTime? _update_date;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string Site
		{
			set{ _site=value;}
			get{return _site;}
        }
        /// <summary>
        /// 
        /// </summary>
        public string SiteUrl
        {
            set { _siteurl = value; }
            get { return _siteurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Department_id
        {
            set { _department_id = value; }
            get { return _department_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Employee_id
        {
            set { _employee_id = value; }
            get { return _employee_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Employee
        {
            set { _employee = value; }
            get { return _employee; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int? Create_id
		{
			set{ _create_id=value;}
			get{return _create_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Create_date
		{
			set{ _create_date=value;}
			get{return _create_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Update_id
		{
			set{ _update_id=value;}
			get{return _update_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Update_date
		{
			set{ _update_date=value;}
			get{return _update_date;}
		}
		#endregion Model

	}
}

