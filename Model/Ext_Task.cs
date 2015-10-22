using System;
namespace XHD.Model
{
	/// <summary>
    /// Ext_Task:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class Ext_Task
	{
		public Ext_Task()
		{}
		#region Model
        private int _id;
        private string _serialnumber;
        private string _sitename;
        private string _url;
        private string _descripe;
        private int? _department_id;
        private string _department;
        private int? _employee_id;
        private string _employee;
        private int? _create_id;
        private string _create_name;
        private DateTime? _create_date;
        private int? _isdelete;
        private DateTime? _delete_time;
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
        public string Serialnumber
        {
            set { _serialnumber = value; }
            get { return _serialnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SiteName
        {
            set { _sitename = value; }
            get { return _sitename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DesCripe
        {
            set { _descripe = value; }
            get { return _descripe; }
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
            set { _create_id = value; }
            get { return _create_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Create_name
        {
            set { _create_name = value; }
            get { return _create_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Create_date
        {
            set { _create_date = value; }
            get { return _create_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Delete_time
        {
            set { _delete_time = value; }
            get { return _delete_time; }
        }
		#endregion Model

	}
}

