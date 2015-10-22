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
    /// Param_SysParam 的摘要说明
    /// </summary>
    public class Param_SysParam : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;

            BLL.Param_SysParam psp = new BLL.Param_SysParam();
            Model.Param_SysParam model = new Model.Param_SysParam();

            BLL.hr_employee emp = new BLL.hr_employee();
            int emp_id = int.Parse(request.Cookies["UserID"].Value);
            DataSet dsemp = emp.GetList("id=" + emp_id);

            if (request["Action"] == "GetApp")
            {
                BLL.Param_SysParam_Type cpst = new BLL.Param_SysParam_Type();
                DataSet ds = cpst.GetList(0, "", "params_order");

                StringBuilder str = new StringBuilder();
                str.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Append("{id:" + ds.Tables[0].Rows[i]["id"].ToString() + ",pid:0,text:'" + ds.Tables[0].Rows[i]["params_name"] + "'},");
                }
                str.Replace(",", "", str.Length - 1, 1);
                str.Append("]");
                context.Response.Write(str);
            }
            if (request["Action"] == "GetParams")
            {

                string parentid = request["parentid"];
                if (!string.IsNullOrEmpty(parentid))
                {
                    DataSet ds = psp.GetList(0, " parentid=" + int.Parse(Common.PageValidate.IsNumber(parentid) ? parentid : "-1") , "params_order");
                    string dt = Common.GetGridJSON.DataTableToJSON(ds.Tables[0]);
                    context.Response.Write(dt);
                }

            }

            //combo
            if (request["Action"] == "combo")
            {
                string parentid = request["parentid"];
                if (!string.IsNullOrEmpty(parentid))
                {
                    DataSet ds = psp.GetList(0, " parentid=" + (Common.PageValidate.IsNumber(parentid) ? parentid : "-1") , "params_order");

                    StringBuilder str = new StringBuilder();

                    str.Append("[");
                    //str.Append("{id:0,text:'无'},");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        str.Append("{id:" + ds.Tables[0].Rows[i]["id"].ToString() + ",text:'" + ds.Tables[0].Rows[i]["params_name"] + "'},");
                    }
                    str.Replace(",", "", str.Length - 1, 1);
                    str.Append("]");

                    context.Response.Write(str);
                }
            }
            if (request["Action"] == "comboread")
            {
                string parentid = request["parentid"];
                if (!string.IsNullOrEmpty(parentid))
                {
                    StringBuilder str = new StringBuilder();

                    str.Append("[");
                    str.Append("{id:0,text:'未读'},");
                    str.Append("{id:1,text:'已读'}");
                    str.Append("]");

                    context.Response.Write(str);
                }
            }

            //Form JSON
            if (request["Action"] == "form")
            {
                DataSet ds = psp.GetList("id=" + int.Parse(request["paramid"]));

                string dt = Common.DataToJson.DataToJSON(ds);

                context.Response.Write(dt);
            }
            //save
            if (request["Action"] == "save")
            {
                model.params_name = Common.PageValidate.InputText(request["T_param_name"], 255);
                model.params_order = Common.PageValidate.InputText(request["T_param_order"], 255);


                string id = request["paramid"];

                if (!string.IsNullOrEmpty(id) && id != "null")
                {
                    DataSet ds = psp.GetList("id=" + int.Parse( id));
                    model.parentid = int.Parse(ds.Tables[0].Rows[0]["parentid"].ToString());
                    model.id = int.Parse(id);
                    psp.Update(model);
                }
                else
                {
                    model.parentid = int.Parse(request["parentid"]);
                    psp.Add(model);
                }
            }
            //del
            if (request["Action"] == "del")
            {
                bool isdel = psp.Delete(int.Parse(request["paramid"]));
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}