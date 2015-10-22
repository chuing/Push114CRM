using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using XHD.Common;

namespace XHD.CRM.Data
{
    /// <summary>
    /// Ext_Task 的摘要说明
    /// </summary>
    public class Ext_Task : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;

            BLL.Ext_Task task = new BLL.Ext_Task();
            Model.Ext_Task model = new Model.Ext_Task();

            BLL.hr_employee emp = new BLL.hr_employee();
            int emp_id = int.Parse(request.Cookies["UserID"].Value);
            DataSet dsemp = emp.GetList("id=" + emp_id);
            string empname = dsemp.Tables[0].Rows[0]["name"].ToString();
            string uid = dsemp.Tables[0].Rows[0]["uid"].ToString();
            string did = dsemp.Tables[0].Rows[0]["d_id"].ToString();
            string dname = dsemp.Tables[0].Rows[0]["dname"].ToString();

            //save
            if (request["Action"] == "save")
            {
                //T_sitename=%E8%B5%B6%E9%9B%86%E7%BD%91&T_sitename_val=&T_url=http%3A%2F%2Fi.hc.com%2Fmain.aspx&T_descript=dd&Action=save&id=5
                model.SiteName = PageValidate.InputText(request["T_sitename"], 255);
                model.Url = PageValidate.InputText(request["T_url"], 255);
                model.DesCripe = PageValidate.InputText(request["T_descript"], 4000);
                /*
                string depid = request["T_department_val"];
                if (string.IsNullOrEmpty(depid))
                    depid = "0";
                model.Department_id = int.Parse(depid);
                model.Department = PageValidate.InputText(request["T_department"], 255);

                string empid = request["T_employee_val"];
                if (string.IsNullOrEmpty(empid))
                    empid = "0";
                model.Employee_id = int.Parse(empid);
                model.Employee = PageValidate.InputText(request["T_employee"], 255);
                */

                string id = request["id"];
                int UserID = emp_id;
                if (!string.IsNullOrEmpty(id) && id != "null")
                {

                    DataSet ds = task.GetList("id=" + int.Parse(id));
                    DataRow dr = ds.Tables[0].Rows[0];

                    model.Serialnumber = PageValidate.InputText(dr["Serialnumber"].ToString(), 255);

                    model.id = int.Parse(id);
                    task.Update(model);

                    //日志
                    C_Sys_log log = new C_Sys_log();

                    string UserName = empname;
                    string IPStreet = request.UserHostAddress;
                    string EventTitle = model.SiteName;
                    string EventType = "发贴修改";
                    int EventID = model.id;

                    if (dr["SiteName"].ToString() != request["T_sitename"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "发贴网站", dr["SiteName"].ToString(), request["T_sitename"].ToString());

                    if (dr["Url"].ToString() != request["T_url"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "发贴网址", dr["Url"].ToString(), request["T_url"].ToString());

                    if (dr["DesCripe"].ToString() != request["T_descript"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "发贴描述", dr["DesCripe"].ToString(), request["T_descript"].ToString());

                }
                else
                {

                    model.Department_id = int.Parse(did);
                    model.Department = dname;

                    model.Employee_id = UserID;
                    model.Employee = empname;

                    model.isDelete = 0;
                    DateTime nowtime = DateTime.Now;
                    model.Create_date = nowtime;
                    model.Serialnumber = nowtime.AddMilliseconds(3).ToString("yyyyMMddHHmmssfff").Trim();
                    model.Create_id = int.Parse(request.Cookies["UserID"].Value);
                    model.Create_name = Common.PageValidate.InputText(empname, 255);
                    
                    int taskid = task.Add(model);

                    context.Response.Write(taskid);

                }
            }

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
                string serchtxt = null;
                string serchtype = request["isdel"];
                if (serchtype == "1")
                    serchtxt += " isDelete=1 ";
                else
                    serchtxt += " isDelete=0 ";

                string serchstr = null;
                if (!string.IsNullOrEmpty(request["id"]))
                    serchstr += " and id =" + int.Parse(request["id"]);

                if (!string.IsNullOrEmpty(request["sitesame"]))
                    serchstr += " and SiteName like N'%" + PageValidate.InputText(request["sitesame"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["turl"]))
                    serchstr += " and Url like N'%" + PageValidate.InputText(request["tUrl"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["department"]))
                    serchstr += " and Department_id = " + int.Parse(request["department_val"]);

                if (!string.IsNullOrEmpty(request["employee"]))
                    serchstr += " and Employee_id = " + int.Parse(request["employee_val"]);

                if (!string.IsNullOrEmpty(request["startdate"]))
                    serchstr += " and Create_date >= '" + PageValidate.InputText(request["startdate"], 255) + "'";

                if (!string.IsNullOrEmpty(request["enddate"]))
                {
                    DateTime enddate = DateTime.Parse(request["enddate"]).AddHours(23).AddMinutes(59).AddSeconds(59);
                    serchstr += " and Create_date <= '" + enddate + "'";
                }

                if (!string.IsNullOrEmpty(request["startdate_del"]))
                    serchstr += " and Delete_time >= '" + PageValidate.InputText(request["startdate_del"], 255) + "'";

                if (!string.IsNullOrEmpty(request["enddate_del"]))
                {
                    DateTime enddatedel = DateTime.Parse(request["enddate_del"]).AddHours(23).AddMinutes(59).AddSeconds(59);
                    serchstr += " and Delete_time <= '" + enddatedel + "'";
                }

                if (!string.IsNullOrEmpty(request["C_employee"]))
                    serchstr += " and Create_id = " + int.Parse(request["C_employee_val"]);
                else
                {
                    //权限
                    serchtxt += DataAuth(request.Cookies["UserID"].Value);
                }
                serchtxt += serchstr;

                if (string.IsNullOrEmpty(serchstr))
                {
                    //创建者
                    if (!string.IsNullOrEmpty(serchtxt))
                        serchtxt = "(" + serchtxt + ")";

                    serchtxt += " or Create_id=" + int.Parse(request.Cookies["UserID"].Value);
                }

                //context.Response.Write(serchtxt);

                DataSet ds = task.GetList(PageSize, PageIndex, serchtxt, sorttext, out Total);

                string dt = Common.GetGridJSON.DataTableToJSON1(ds.Tables[0], Total);
                context.Response.Write(dt);
            }

            if (request["Action"] == "continue")
            {
            }

            //Form JSON
            if (request["Action"] == "form")
            {
                string id = request["cid"];
                DataSet ds = task.GetList("id=" + int.Parse(id) + DataAuth(request.Cookies["UserID"].Value));
                
                string dt = Common.DataToJson.DataToJSON(ds);

                context.Response.Write(dt);
            }
            if (request["Action"] == "count")
            {
                string id = request["id"];
                DataSet ds = task.GetList("id=" + int.Parse(id));

                context.Response.Write(string.Format("{0}记录 ", ds.Tables[0].Rows.Count));
            }
            //预删除
            if (request["Action"] == "AdvanceDelete")
            {
                string id = request["id"];

                DataSet ds = task.GetList("id=" + int.Parse(id));

                bool canedel = true;
                if (uid != "admin")
                {
                    Data.GetDataAuth dataauth = new Data.GetDataAuth();
                    string txt = dataauth.GetDataAuthByid("1", "Sys_del", emp_id.ToString());

                    string[] arr = txt.Split(':');
                    switch (arr[0])
                    {
                        case "none":
                            canedel = false;
                            break;
                        case "my":
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i]["Employee_id"].ToString() == arr[1])
                                    canedel = true;
                                else
                                    canedel = false;
                            }
                            break;
                        case "dep":
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i]["Department_id"].ToString() == arr[1])
                                    canedel = true;
                                else
                                    canedel = false;
                            }
                            break;
                        case "all":
                            canedel = true;
                            break;
                    }
                }
                if (canedel)
                {
                    bool isdel = task.AdvanceDelete(int.Parse(request["id"]), 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (isdel)
                    {
                        //日志
                        string EventType = "发贴预删除";

                        int UserID = emp_id;
                        string UserName = empname;
                        string IPStreet = request.UserHostAddress;
                        int EventID = int.Parse(id);
                        string EventTitle = ds.Tables[0].Rows[0]["task"].ToString();
                        string Original_txt = null;
                        string Current_txt = null;

                        C_Sys_log log = new C_Sys_log();

                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, null, Original_txt, Current_txt);

                        context.Response.Write("true");
                    }
                    else
                    {
                        context.Response.Write("false");
                    }
                }
                else
                {
                    context.Response.Write("delfalse");
                }
            }
            //regain            
            if (request["Action"] == "regain")
            {
                string idlist = PageValidate.InputText(request["idlist"], 100000);
                string[] arr = idlist.Split(',');

                DataSet ds = task.GetList("id in (" + idlist.Trim() + ")");

                //日志   
                string EventType = "恢复删除贴子";
                int UserID = emp_id;
                string UserName = empname;

                string IPStreet = request.UserHostAddress;
                string Original_txt = null;
                string Current_txt = null;

                int success = 0, failure = 0;   //计数
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    bool isregain = task.AdvanceDelete(int.Parse(arr[i]), 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (isregain)
                    {
                        C_Sys_log log = new C_Sys_log();
                        int EventID = int.Parse(ds.Tables[0].Rows[i]["id"].ToString());
                        string EventTitle = ds.Tables[0].Rows[i]["task"].ToString();
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, null, Original_txt, Current_txt);
                        success++;
                    }
                    else
                    {
                        failure++;
                    }
                }
                context.Response.Write(string.Format("{0}恢复成功,{1}失败", success, failure));

            }

            if (request.Params["Action"] == "del")
            {
                bool canDel = false;
                if (dsemp.Tables[0].Rows.Count > 0)
                {
                    if (dsemp.Tables[0].Rows[0]["uid"].ToString() == "admin")
                    {
                        canDel = true;
                    }
                    else
                    {
                        Data.GetAuthorityByUid getauth = new Data.GetAuthorityByUid();
                        string delauth = getauth.GetBtnAuthority(request.Cookies["UserID"].Value, "60");
                        if (delauth == "false")
                            canDel = false;
                        else
                            canDel = true;
                    }
                }
                if (canDel)
                {
                    string idlist = PageValidate.InputText(request["idlist"], 100000);
                    string[] arr = idlist.Split(',');

                    string EventType = "彻底删除发贴";

                    DataSet ds = task.GetList("id in (" + idlist.Trim() + ")");

                    bool canedel = true;
                    if (uid != "admin")
                    {
                        Data.GetDataAuth dataauth = new Data.GetDataAuth();
                        string txt = dataauth.GetDataAuthByid("1", "Sys_del", emp_id.ToString());

                        string[] arr1 = txt.Split(':');
                        switch (arr1[0])
                        {
                            case "none":
                                canedel = false;
                                break;
                            case "my":
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (ds.Tables[0].Rows[i]["Employee_id"].ToString() == arr1[1])
                                        canedel = true;
                                    else
                                        canedel = false;
                                }
                                break;
                            case "dep":
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (ds.Tables[0].Rows[i]["Department_id"].ToString() == arr1[1])
                                        canedel = true;
                                    else
                                        canedel = false;
                                }
                                break;
                            case "all":
                                canedel = true;
                                break;
                        }
                    }
                    if (canedel)
                    {

                        int success = 0, failure = 0;

                        //日志    
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string cid = ds.Tables[0].Rows[i]["id"].ToString();
                            bool isdel = task.Delete(int.Parse(cid));
                            if (isdel)
                            {
                                success++;
                                int UserID = emp_id;
                                string UserName = empname;
                                string IPStreet = request.UserHostAddress;
                                int EventID = int.Parse(cid);
                                string EventTitle = ds.Tables[0].Rows[i]["SiteName"].ToString();
                                string Original_txt = null;
                                string Current_txt = null;

                                C_Sys_log log = new C_Sys_log();
                                
                                log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, null, Original_txt, Current_txt);
                            }
                            else
                            {
                                failure++;
                            }
                        }
                        context.Response.Write(string.Format("{0}条数据成功删除，{1}条失败。|{1}", success, failure));

                    }
                    else
                    {
                        context.Response.Write("delfalse");
                    }
                }
                else
                {
                    context.Response.Write("auth");
                }
            }

            //validate website
            if (request["Action"] == "validate")
            {
                string company = request["T_company"];
                string taskid = request["T_cid"];
                if (string.IsNullOrEmpty(taskid) || taskid == "null")
                    taskid = "0";

                DataSet ds = task.GetList("task = N'" + Common.PageValidate.InputText(company, 255) + "' and id!=" + int.Parse(taskid));
                //context.Response.Write(" Count:" + ds.Tables[0].Rows.Count);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    context.Response.Write("false");
                }
                else
                {
                    context.Response.Write("true");
                }
            }

            if (request["Action"] == "mobile")
            {
                string company = request["T_mobil"];
                string taskid = request["T_cid"];
                if (string.IsNullOrEmpty(taskid) || taskid == "null")
                    taskid = "0";

                DataSet ds = task.GetList("T_mobil = N'" + Common.PageValidate.InputText(company, 255) + "' and id!=" + int.Parse(taskid));
                //context.Response.Write(" Count:" + ds.Tables[0].Rows.Count);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    context.Response.Write("false");
                }
                else
                {
                    context.Response.Write("true");
                }
            }

            if (request["Action"] == "Compared")
            {
                string dt1 = request["date1"];
                string dt2 = request["date2"];

                DataSet ds = task.Compared(DateTime.Parse(dt1), DateTime.Parse(dt2));

                string dt = GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);
            }

            if (request["Action"] == "Compared_type")
            {
                string dt1 = request["date1"];
                string dt2 = request["date2"];

                DataSet ds = task.Compared_type(DateTime.Parse(dt1), DateTime.Parse(dt2));

                string dt = GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);

            }

            if (request["Action"] == "Compared_level")
            {
                string dt1 = request["date1"];
                string dt2 = request["date2"];

                DataSet ds = task.Compared_level(DateTime.Parse(dt1), DateTime.Parse(dt2));

                string dt = GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);
            }

            if (request["Action"] == "Compared_source")
            {
                string dt1 = request["date1"];
                string dt2 = request["date2"];

                DataSet ds = task.Compared_source(DateTime.Parse(dt1), DateTime.Parse(dt2));

                string dt = GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);
            }

            if (request["Action"] == "Compared_empcusadd")
            {
                var idlist = PageValidate.InputText(request["idlist"].Replace(";", ",").Replace("-", ""), 100000);
                string dt1 = request["date1"];
                string dt2 = request["date2"];

                BLL.hr_post post = new BLL.hr_post();
                DataSet dspost = post.GetList(" post_id in(" + idlist + ")");

                string emplist = "(";

                for (int i = 0; i < dspost.Tables[0].Rows.Count - 1; i++)
                {
                    emplist += dspost.Tables[0].Rows[i]["emp_id"] + ",";
                }
                emplist += dspost.Tables[0].Rows[dspost.Tables[0].Rows.Count - 1]["emp_id"] + ")";

                //context.Response.Write(emplist);

                DataSet ds = task.Compared_empcusadd(DateTime.Parse(dt1), DateTime.Parse(dt2), emplist);

                string dt = Common.GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);
            }

            if (request["Action"] == "emp_task")
            {
                var idlist = PageValidate.InputText(request["idlist"].Replace(";", ",").Replace("-", ""), 100000);
                var syear = request["syear"];

                BLL.hr_post post = new BLL.hr_post();
                DataSet dspost = post.GetList("post_id in(" + idlist + ")");

                string emplist = "(";

                for (int i = 0; i < dspost.Tables[0].Rows.Count - 1; i++)
                {
                    emplist += dspost.Tables[0].Rows[i]["emp_id"] + ",";
                }
                emplist += dspost.Tables[0].Rows[dspost.Tables[0].Rows.Count - 1]["emp_id"] + ")";

                //context.Response.Write(emplist);

                DataSet ds = task.report_empcus(int.Parse(syear), emplist);

                string dt = Common.GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);
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

        public string urlstr(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                string a = url.Replace("http://", "").Replace("ftp://", "");
                string b = a.Split('/')[0];
                string[] c = b.Split('.');
                string d = c.ToString();
                if (c.Length >= 3)
                {
                    if (c[c.Length - 2] == "com" && c[c.Length - 1] == "cn")
                        d = c[c.Length - 3] + c[c.Length - 2] + "." + c[c.Length - 1];
                    else
                        d = c[c.Length - 2] + "." + c[c.Length - 1];
                }

                return d;
            }
            else
            {
                return "";
            }
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