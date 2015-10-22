using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Data;
using System.Text;

namespace XHD.CRM.Data
{
    /// <summary>
    /// Sys_role_emp 的摘要说明
    /// </summary>
    public class Sys_role_emp : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Params["Action"] == "save")
            {
                BLL.Sys_role_emp rm = new BLL.Sys_role_emp();
                Model.Sys_role_emp model = new Model.Sys_role_emp();
                string rid = context.Request.Params["rid"];
                string savestring = context.Request.Params["savestring"];
                model.RoleID = int.Parse(rid);

                rm.Delete("RoleID=" + rid);

                JavaScriptSerializer json = new JavaScriptSerializer();
                List<RoleEmp> _list = json.Deserialize<List<RoleEmp>>(savestring);

                foreach (RoleEmp emp1 in _list)
                {
                    model.empID = emp1.ID;
                    rm.Add(model);
                }
                //日志
                BLL.Sys_log log = new BLL.Sys_log();
                Model.Sys_log modellog = new Model.Sys_log();

                BLL.hr_employee emp = new BLL.hr_employee();
                DataSet dsemp = emp.GetList("id=" + context.Request.Cookies["UserID"].Value);
                modellog.EventDate = DateTime.Now;
                modellog.UserID = int.Parse(context.Request.Cookies["UserID"].Value);
                modellog.UserName = dsemp.Tables[0].Rows[0]["name"].ToString();
                modellog.IPStreet = context.Request.UserHostAddress;

                modellog.EventType = "权限人员调整";
                modellog.EventID = model.RoleID.ToString();
                log.Add(modellog);

                context.Response.Write("true");
            }

            if (context.Request.Params["Action"] == "get")
            {
                string rid = context.Request["rid"];
                if (!string.IsNullOrEmpty(rid))
                {
                    BLL.Sys_role_emp re = new BLL.Sys_role_emp();
                    DataSet ds = re.GetList("RoleID=" + int.Parse( rid));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string sql = "";

                        sql += "ID in (";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            sql += row["empID"] + ",";
                        }
                        sql = sql.Substring(0, sql.Length - 1);
                        sql += ")";

                        BLL.hr_employee emp = new BLL.hr_employee();
                        string dt = Common.GetGridJSON.DataTableToJSON(emp.GetList(sql).Tables[0]);
                        context.Response.Write(dt);
                    }
                    else
                    {
                        context.Response.Write("{ Rows: [] }");
                    }
                }
                else
                {
                    context.Response.Write("test" + rid);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public class RoleEmp
        {
            public int RoleID { get; set; }
            public int ID { get; set; }
        }
    }
}