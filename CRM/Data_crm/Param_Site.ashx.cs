using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace XHD.CRM.Data
{
    /// <summary>
    /// Param_Site 的摘要说明
    /// </summary>
    public class Param_Site : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;

            BLL.Param_Site ps = new BLL.Param_Site(); 
            Model.Param_Site model = new Model.Param_Site();

            if (request["Action"] == "grid")
            {
                int PageIndex = int.Parse(request["page"] == null ? "1" : request["page"]);
                int PageSize = int.Parse(request["pagesize"] == null ? "30" : request["pagesize"]);
                string sortname = request["sortname"];
                string sortorder = request["sortorder"];

                if (string.IsNullOrEmpty(sortname))
                    sortname = " id";
                if (string.IsNullOrEmpty(sortorder))
                    sortorder = " desc";

                string sorttext = " " + sortname + " " + sortorder;

                string Total;
                string serchtxt = " 1=1";

                //权限
                serchtxt += DataAuth(request.Cookies["UserID"].Value);

                //serchtxt += " or Create_id=" + int.Parse(request.Cookies["UserID"].Value);

               // context.Response.Write(serchtxt + " - " + PageIndex.ToString() + " - " + PageSize.ToString());

                DataSet ds = ps.GetList(PageSize, PageIndex, serchtxt, sorttext, out Total);

                string dt = Common.GetGridJSON.DataTableToJSON1(ds.Tables[0], Total);
                context.Response.Write(dt);
            }
            if (request["Action"] == "treegrid")
            {
                DataSet ds = ps.GetAllList();
                string dt = "{Rows:[" + GetTasksString(0, ds.Tables[0]) + "]}";
                context.Response.Write(dt);
            }
            if (request["Action"] == "tree")
            {
                DataSet ds = ps.GetAllList();  
                StringBuilder str = new StringBuilder();
                str.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Append("{id:" + ds.Tables[0].Rows[i]["id"].ToString() + ",pid:" + ds.Tables[0].Rows[i]["parentid"].ToString() + ",text:'" + ds.Tables[0].Rows[i]["Site"]  + "'},");
                }
                str.Replace(",", "", str.Length - 1, 1);
                str.Append("]");
                context.Response.Write(str);
            }
            //save
            if (request["Action"] == "save")
            {

                model.Site = Common.PageValidate.InputText(request["T_Site"], 255);
                model.SiteUrl = Common.PageValidate.InputText(request["T_SiteUrl"], 255);
                string pid = request["T_Parent_val"];
                if (string.IsNullOrEmpty(pid))
                {
                    pid = "0";
                }
                model.parentid = int.Parse(pid);

                string id = request["id"];

                if (!string.IsNullOrEmpty(id) && id != "null")
                {
                    model.id = int.Parse(id);
                    ps.Update(model);
                }
                else
                {
                    BLL.hr_employee emp = new BLL.hr_employee();
                    int emp_id = int.Parse(request.Cookies["UserID"].Value);
                    DataSet dsemp = emp.GetList("id=" + emp_id);
                    string empname = dsemp.Tables[0].Rows[0]["name"].ToString();
                    string did = dsemp.Tables[0].Rows[0]["d_id"].ToString();
                    string dname = dsemp.Tables[0].Rows[0]["dname"].ToString();

                    model.Department_id = int.Parse(did);
                    model.Department = dname;

                    model.Employee_id = emp_id;
                    model.Employee = empname;

                    DateTime nowtime = DateTime.Now;
                    model.Create_date = nowtime;

                    model.Create_id = emp_id;

                    ps.Add(model);
                }
            }
            //Form JSON
            if (request["Action"] == "form")
            {
                
                DataSet ds = ps.GetList("id=" + int.Parse( request["id"]));

                string dt = Common.DataToJson.DataToJSON(ds);

                context.Response.Write(dt);
            }
            //del
            if (request["Action"] == "del")
            {
                string c_id = request["id"];
                DataSet ds = ps.GetList(" parentid=" + int.Parse(c_id));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    context.Response.Write("false:parent");
                }
                else
                {
                    bool isdel = ps.Delete(int.Parse(c_id));
                    if (isdel)
                    {
                        context.Response.Write("true");
                    }
                    else
                    {
                        context.Response.Write("false");
                    }
                }
            }
            if (request["Action"] == "combo")
            {
                DataSet ds = ps.GetList("parentid=0");

                StringBuilder str = new StringBuilder();

                str.Append("[");
                str.Append("{id:0,text:'无'},");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Append("{id:" + ds.Tables[0].Rows[i]["id"].ToString() + ",text:'" + ds.Tables[0].Rows[i]["Site"] + "'},");
                }
                str.Replace(",", "", str.Length - 1, 1);
                str.Append("]");

                context.Response.Write(str);
            }
            if (request["Action"] == "combo1")
            {
                DataSet ds = ps.GetList("parentid=0");

                StringBuilder str = new StringBuilder();

                str.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Append("{id:" + ds.Tables[0].Rows[i]["id"].ToString() + ",text:'" + ds.Tables[0].Rows[i]["Site"] + "'},");
                }
                str.Replace(",", "", str.Length - 1, 1);
                str.Append("]");

                context.Response.Write(str);
            }
            if (request["Action"] == "combo2")
            {
                DataSet ds = ps.GetList("parentid=" + int.Parse( request["pid"]));

                StringBuilder str = new StringBuilder();

                str.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Append("{id:" + ds.Tables[0].Rows[i]["id"].ToString() + ",text:'" + ds.Tables[0].Rows[i]["Site"] + "'},");
                }
                str.Replace(",", "", str.Length - 1, 1);
                str.Append("]");

                context.Response.Write(str);
            }
            if (request["Action"] == "combo3")
            {
                DataSet ds = ps.GetList("parentid=0 and Employee_id=" + request.Cookies["UserID"].Value);

                StringBuilder str = new StringBuilder();

                str.Append("[");
                str.Append("{id:0,text:'无'},");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Append("{id:" + ds.Tables[0].Rows[i]["id"].ToString() + ",text:'" + ds.Tables[0].Rows[i]["Site"] + "'},");
                }
                str.Replace(",", "", str.Length - 1, 1);
                str.Append("]");

                context.Response.Write(str);
            }
        }

        private string DataAuth(string uid)
        {
            //权限
            BLL.hr_employee emp = new BLL.hr_employee();
            DataSet dsemp = emp.GetList("ID=" + int.Parse(uid));

            string returntxt = " and 1=1";
            if (dsemp.Tables[0].Rows.Count > 0)
            {
                if (dsemp.Tables[0].Rows[0]["uid"].ToString() != "admin")
                {
                    Data.GetDataAuth dataauth = new Data.GetDataAuth();
                    string txt = dataauth.GetDataAuthByid("1", "Sys_view", uid);

                    string[] arr = txt.Split(':');

                    switch (arr[0])
                    {
                        case "none": returntxt = " and 1=2 ";
                            break;
                        case "my":
                            returntxt = " and Employee_id=" + int.Parse(arr[1]);
                            break;
                        case "dep":
                            if (string.IsNullOrEmpty(arr[1]))
                                returntxt = " and Employee_id=" + int.Parse(uid);
                            else
                                returntxt = " and Department_id=" + int.Parse(arr[1]);
                            break;
                        case "depall":
                            BLL.hr_department dep = new BLL.hr_department();
                            DataSet ds = dep.GetAllList();
                            string deptask = GetDepTask(int.Parse(arr[1]), ds.Tables[0]);
                            string intext = arr[1] + "," + deptask;
                            returntxt = " and Department_id in (" + intext.TrimEnd(',') + ")";
                            break;
                    }
                }
            }
            return returntxt;
        }
        private static string GetDepTask(int Id, DataTable table)
        {
            DataRow[] rows = table.Select("parentid=" + Id.ToString());

            if (rows.Length == 0) return string.Empty; ;
            StringBuilder str = new StringBuilder();

            foreach (DataRow row in rows)
            {
                str.Append(row["id"] + ",");
                if (GetDepTask((int)row["id"], table).Length > 0)
                    str.Append(GetDepTask((int)row["id"], table));
            }
            return str.ToString();
        }

        private static string GetTasksString(int Id, DataTable table)
        {
            DataRow[] rows = table.Select("parentid=" + Id.ToString());

            if (rows.Length == 0) return string.Empty; ;
            StringBuilder str = new StringBuilder();

            foreach (DataRow row in rows)
            {
                str.Append("{");
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (i != 0) str.Append(",");
                    str.Append(row.Table.Columns[i].ColumnName);
                    str.Append(":'");
                    str.Append(row[i].ToString());
                    str.Append("'");
                }
                if (GetTasksString((int)row["id"], table).Length > 0)
                {
                    str.Append(",children:[");
                    str.Append(GetTasksString((int)row["id"], table));
                    str.Append("]},");
                }
                else
                {
                    str.Append("},");
                }
            }
            return str[str.Length - 1] == ',' ? str.ToString(0, str.Length - 1) : str.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}