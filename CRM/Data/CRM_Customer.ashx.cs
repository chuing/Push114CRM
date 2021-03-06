﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using XHD.Common;

namespace XHD.CRM.Data
{
    /// <summary>
    /// CRM_Customer 的摘要说明
    /// </summary>
    public class CRM_Customer : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;

            BLL.CRM_Customer customer = new BLL.CRM_Customer();
            Model.CRM_Customer model = new Model.CRM_Customer();

            BLL.hr_employee emp = new BLL.hr_employee();
            int emp_id = int.Parse(request.Cookies["UserID"].Value);
            DataSet dsemp = emp.GetList("id=" + emp_id);
            string empname = dsemp.Tables[0].Rows[0]["name"].ToString();
            string uid = dsemp.Tables[0].Rows[0]["uid"].ToString();
            
            if (request["Action"] == "move")
            {
                string F_depid = request["F_dep_val"];
                if (string.IsNullOrEmpty(F_depid))
                    F_depid = "0";
                model.M_Department_id = int.Parse(F_depid);
                model.M_Department = PageValidate.InputText(request["F_dep"], 255);

                string F_empid = request["F_emp_val"];
                if (string.IsNullOrEmpty(F_empid))
                    F_empid = "0";
                model.M_Employee_id = int.Parse(F_empid);
                model.M_Employee = PageValidate.InputText(request["F_emp"], 255);

                string depid = request["T_dep_val"];
                if (string.IsNullOrEmpty(depid))
                    depid = "0";
                model.Department_id = int.Parse(depid);
                model.Department = PageValidate.InputText(request["T_dep"], 255);

                string empid = request["T_emp_val"];
                if (string.IsNullOrEmpty(empid))
                    empid = "0";
                model.Employee_id = int.Parse(empid);
                model.Employee = PageValidate.InputText(request["T_emp"], 255);
                
                string B_empid = request["B_emp_val"];
                if (string.IsNullOrEmpty(B_empid))
                    B_empid = "0";

                /*
                context.Response.Write(B_empid);
                */
                int ct;
                if (B_empid == "0")
                {
                    model.toBak_id = model.M_Employee_id;
                    model.toBak = model.M_Employee;
                    ct = customer.CustomerMove(model);
                }
                else
                {
                    model.toBak_id = int.Parse(B_empid);
                    model.toBak = PageValidate.InputText(request["B_emp"], 255);
                    ct = customer.CustomerMoveBack(model);
                }
                context.Response.Write(ct);
            }
            //save
            if (request["Action"] == "save")
            {
                model.Customer = PageValidate.InputText(request["T_company"], 255);
                model.address = PageValidate.InputText(request["T_address"], 255);
                model.fax = PageValidate.InputText(request["T_fax"], 255);
                model.site = PageValidate.InputText(request["T_Website"], 255);

                model.tel = PageValidate.InputText(request["T_company_tel"], 255);

                string industryid = request["T_industry_val"];
                if (string.IsNullOrEmpty(industryid) || industryid == "null")
                    industryid = "0";
                model.industry_id = int.Parse(industryid);
                model.industry = PageValidate.InputText(request["T_industry"], 255);

                string provincesid = request["T_Provinces_val"];
                if (string.IsNullOrEmpty(provincesid))
                    provincesid = "0";
                model.Provinces_id = int.Parse(provincesid);

                model.Provinces = PageValidate.InputText(request["T_Provinces"], 255);

                string cityid = request["T_City_val"];
                if (string.IsNullOrEmpty(cityid))
                    cityid = "0";
                model.City_id = int.Parse(cityid);
                model.City = PageValidate.InputText(request["T_City"], 255);

                string ctypeid = request["T_customertype_val"];
                if (string.IsNullOrEmpty(ctypeid))
                    ctypeid = "0";
                model.CustomerType_id = int.Parse(ctypeid);
                model.CustomerType = PageValidate.InputText(request["T_customertype"], 255);

                string clevelid = request["T_customerlevel_val"];
                if (string.IsNullOrEmpty(clevelid))
                    clevelid = "0";
                model.CustomerLevel_id = int.Parse(clevelid);
                model.CustomerLevel = PageValidate.InputText(request["T_customerlevel"], 255);

                string csourceid = request["T_CustomerSource_val"];
                if (string.IsNullOrEmpty(csourceid))
                    csourceid = "0";
                model.CustomerSource_id = int.Parse(csourceid);
                model.CustomerSource = PageValidate.InputText(request["T_CustomerSource"], 255);

                model.DesCripe = PageValidate.InputText(request["T_descript"], 4000);
                model.Remarks = PageValidate.InputText(request["T_remarks"], 4000);
                model.privatecustomer = PageValidate.InputText(request["T_private"], 255);

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


                string id = request["id"];
                if (!string.IsNullOrEmpty(id) && id != "null")
                {

                    string chkemp = request["T_emp"];
                    if (string.IsNullOrEmpty(chkemp))
                        chkemp = "0";
                    if (empid != chkemp && chkemp != "0")
                        model.isRead = 0;
                    else
                        model.isRead = 1;

                    DataSet ds = customer.GetList("id=" + int.Parse(id));
                    DataRow dr = ds.Tables[0].Rows[0];

                    model.Serialnumber = PageValidate.InputText(dr["Serialnumber"].ToString(), 255);

                    model.id = int.Parse(id);
                    customer.Update(model);

                    string oCustomer = PageValidate.InputText(request["O_company"], 255);
                    if (oCustomer != model.Customer)
                    {
                        customer.UpdateCustomer(model);
                        /*
                        List<String> list = customer.UpdateCustomer(model);
                        for (int i = 0; i < list.Count; i++)
                        {
                            context.Response.Write(list[i] + "=<br />");
                        }
                        context.Response.Write(customer.UpdateCustomer(model));
                        context.Response.Write(customer.UpdateCustomer(model));
                        */
                    }

                    //日志
                    C_Sys_log log = new C_Sys_log();

                    int UserID = emp_id;
                    string UserName = empname;
                    string IPStreet = request.UserHostAddress;
                    string EventTitle = model.Customer;
                    string EventType = "客户修改";
                    int EventID = model.id;

                    if (dr["Customer"].ToString() != request["T_company"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "公司名", dr["Customer"].ToString(), request["T_company"].ToString());

                    if (dr["address"].ToString() != request["T_address"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "地址", dr["address"].ToString(), request["T_address"].ToString());

                    if (dr["fax"].ToString() != request["T_fax"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "传真", dr["fax"].ToString(), request["T_fax"].ToString());

                    if (dr["site"].ToString() != request["T_Website"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "网址", dr["site"].ToString(), request["T_Website"].ToString());

                    if (dr["industry"].ToString() != request["T_industry"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "行业", dr["industry"].ToString(), request["T_industry"].ToString());

                    if (dr["Provinces"].ToString() != request["T_Provinces"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "省份", dr["Provinces"].ToString(), request["T_Provinces"].ToString());

                    if (dr["City"].ToString() != request["T_City"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "城市", dr["City"].ToString(), request["T_City"].ToString());

                    if (dr["CustomerType"].ToString() != request["T_customertype"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "客户类型", dr["CustomerType"].ToString(), request["T_customertype"].ToString());

                    if (dr["CustomerLevel"].ToString() != request["T_customerlevel"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "客户级别", dr["CustomerLevel"].ToString(), request["T_customerlevel"].ToString());

                    if (dr["CustomerSource"].ToString() != request["T_CustomerSource"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "客户来源", dr["CustomerSource"].ToString(), request["T_CustomerSource"].ToString());

                    if (dr["DesCripe"].ToString() != request["T_descript"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "客户描述", dr["DesCripe"].ToString(), request["T_descript"].ToString());

                    if (dr["Remarks"].ToString() != request["T_remarks"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "备注", dr["Remarks"].ToString(), request["T_remarks"].ToString());

                    if (dr["privatecustomer"].ToString() != request["T_private"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "公私", dr["privatecustomer"].ToString(), request["T_private"].ToString());

                    if (dr["Department"].ToString() != request["T_department"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "部门", dr["Department"].ToString(), request["T_department"].ToString());

                    if (dr["Employee"].ToString() != request["T_employee"])
                        log.Add_log(UserID, UserName, IPStreet, EventTitle, EventType, EventID, "员工", dr["Employee"].ToString(), request["T_employee"].ToString());
                }
                else
                {
                    model.isDelete = 0;
                    DateTime nowtime = DateTime.Now;
                    model.Create_date = nowtime;
                    model.Serialnumber = nowtime.AddMilliseconds(3).ToString("yyyyMMddHHmmssfff").Trim();
                    model.Create_id = int.Parse(request.Cookies["UserID"].Value);
                    model.Create_name = Common.PageValidate.InputText(empname, 255);
                    string isread = "0";
                    if (int.Parse(request.Cookies["UserID"].Value) == int.Parse(empid))
                        isread = "1";
                    model.isRead = int.Parse(isread);
                    model.isImport = 0;
                    
                    int customerid = customer.Add(model);

                    BLL.CRM_Contact contact = new BLL.CRM_Contact();
                    Model.CRM_Contact modelcontact = new Model.CRM_Contact();
                    modelcontact.isDelete = 0;
                    modelcontact.C_name = PageValidate.InputText(request["T_customername"], 255);
                    modelcontact.C_sex = PageValidate.InputText(request["T_sex"], 255);
                    modelcontact.C_department = PageValidate.InputText(request["T_dep"], 255);
                    modelcontact.C_position = PageValidate.InputText(request["T_position"], 255);
                    modelcontact.C_QQ = PageValidate.InputText(request["T_qq"], 255);
                    modelcontact.C_tel = PageValidate.InputText(request["T_tel"], 255);
                    modelcontact.C_mob = PageValidate.InputText(request["T_mobil"], 255);
                    modelcontact.C_email = Common.PageValidate.InputText(request["T_email"], 255);
                    modelcontact.C_customerid = customerid;
                    modelcontact.C_customername = model.Customer;
                    modelcontact.C_createId = emp_id;
                    modelcontact.C_createDate = DateTime.Now;
                    modelcontact.C_hobby = PageValidate.InputText(request["T_hobby"], 1000);
                    modelcontact.C_remarks = PageValidate.InputText(request["T_contact_remarks"], 4000);
                    int ct = contact.Add(modelcontact);

                    context.Response.Write(ct);

                }
            }
            //save follow up
            if (request["Action"] == "savefollow")
            {
                string depid = request["F_department_val"];
                if (string.IsNullOrEmpty(depid))
                    depid = "0";
                model.Follow_up_dep_id = int.Parse(depid);
                model.Follow_up_dep = PageValidate.InputText(request["F_department"], 255);

                string empid = request["F_employee_val"];
                if (string.IsNullOrEmpty(empid))
                    empid = "0";
                model.Follow_up_id = int.Parse(empid);
                model.Follow_up = PageValidate.InputText(request["F_employee"], 255);

                model.To_follow_id = int.Parse(request.Cookies["UserID"].Value);
                model.To_follow = Common.PageValidate.InputText(empname, 255);


                string id = request["id"];
                if (!string.IsNullOrEmpty(id) && id != "null")
                {
                    model.id = int.Parse(id);
                    customer.UpdateFollow(model);
                string oaid = request["orderid"];
                if (!string.IsNullOrEmpty(oaid) && oaid != "null")
                {

                    BLL.CRM_order order = new BLL.CRM_order();
                    Model.CRM_order omodel = new Model.CRM_order();

                    omodel.Follow_up_dep = model.Follow_up_dep;
                    omodel.Follow_up = model.Follow_up;

                    omodel.id = int.Parse(oaid);
                    order.UpdateFollow(omodel);
                }
                }
            }

            if (request["Action"] == "grid")
            {
                int PageIndex = int.Parse(request["page"] == null ? "1" : request["page"]);
                int PageSize = int.Parse(request["pagesize"] == null ? "30" : request["pagesize"]);
                string sortname = request["sortname"];
                string sortorder = request["sortorder"];

                if (string.IsNullOrEmpty(sortname))
                    sortname = " isTop"; //sortname = " id";
                if (string.IsNullOrEmpty(sortorder))
                    sortorder = " desc";

                //string sorttext = " " + sortname + " " + sortorder;
                string sorttext = " isTop desc, id desc";

                string Total;
                string serchtxt = null;
                string serchtype = request["isdel"];
                if (serchtype == "1")
                    serchtxt += " isDelete=1 ";
                else
                    serchtxt += " isDelete=0 ";
                
                string import = request["import"];
                if (import == "1")
                {
                    serchtxt += " and isImport = 1";
                }
                else
                {
                    serchtxt += " and (isImport <> 1 Or isImport is null)";
                }

                string serchstr = null;
                if (!string.IsNullOrEmpty(request["companyid"]))
                    serchstr += " and id =" + int.Parse(request["companyid"]);

                if (!string.IsNullOrEmpty(request["company"]))
                    serchstr += " and Customer like N'%" + PageValidate.InputText(request["company"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["address"]))
                    serchstr += " and address like N'%" + PageValidate.InputText(request["address"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["industry"]))
                    serchstr += " and industry like N'%" + PageValidate.InputText(request["industry"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["tel"]))
                    serchstr += " and tel like N'%" + PageValidate.InputText(request["tel"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["mobil"]))
                    serchstr += " and mobil like N'%" + PageValidate.InputText(request["mobil"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["qq"]))
                    serchstr += " and QQ like N'%" + PageValidate.InputText(request["qq"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["website"]))
                    serchstr += " and site like N'%" + PageValidate.InputText(request["website"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["customertype"]))
                    serchstr += " and CustomerType_id = " + int.Parse(request["customertype_val"]);

                if (!string.IsNullOrEmpty(request["customerlevel"]))
                    serchstr += " and CustomerLevel_id = " + int.Parse(request["customerlevel_val"]);

                if (!string.IsNullOrEmpty(request["T_Provinces"]))
                    serchstr += " and Provinces_id = " + int.Parse(request["T_Provinces_val"]);

                if (!string.IsNullOrEmpty(request["T_City"]))
                    serchstr += " and City_id = " + int.Parse(request["T_City_val"]);

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

                if (!string.IsNullOrEmpty(request["startfollow"]))
                    serchstr += " and lastfollow >= '" + PageValidate.InputText(request["startfollow"], 255) + "'";

                if (!string.IsNullOrEmpty(request["endfollow"]))
                {
                    DateTime enddate = DateTime.Parse(request["endfollow"]).AddHours(23).AddMinutes(59).AddSeconds(59);
                    serchstr += " and lastfollow <= '" + enddate + "'";
                }
                if (!string.IsNullOrEmpty(request["isread"]))
                    serchstr += " and isRead = " + int.Parse(request["isread_val"]);

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

                    if (import != "1")
                        serchtxt += " or Create_id=" + int.Parse(request.Cookies["UserID"].Value);
                }

                //context.Response.Write(serchtxt);

                DataSet ds = customer.GetList(PageSize, PageIndex, serchtxt, sorttext, out Total);

                string dt = Common.GetGridJSON.DataTableToJSON1(ds.Tables[0], Total);
                context.Response.Write(dt);
            }

            //后续列表
            if (request["Action"] == "gridfollowup")
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

                string import = request["import"];
                if (import == "1")
                {
                    serchtxt += " and isImport = 1";
                }
                else
                {
                    serchtxt += " and (isImport <> 1 Or isImport is null)";
                }

                string serchstr = null;
                if (!string.IsNullOrEmpty(request["companyid"]))
                    serchstr += " and id =" + int.Parse(request["companyid"]);

                if (!string.IsNullOrEmpty(request["company"]))
                    serchstr += " and Customer like N'%" + PageValidate.InputText(request["company"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["address"]))
                    serchstr += " and address like N'%" + PageValidate.InputText(request["address"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["industry"]))
                    serchstr += " and industry like N'%" + PageValidate.InputText(request["industry"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["tel"]))
                    serchstr += " and tel like N'%" + PageValidate.InputText(request["tel"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["mobil"]))
                    serchstr += " and mobil like N'%" + PageValidate.InputText(request["mobil"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["qq"]))
                    serchstr += " and QQ like N'%" + PageValidate.InputText(request["qq"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["website"]))
                    serchstr += " and site like N'%" + PageValidate.InputText(request["website"], 255) + "%'";

                if (!string.IsNullOrEmpty(request["customertype"]))
                    serchstr += " and CustomerType_id = " + int.Parse(request["customertype_val"]);

                if (!string.IsNullOrEmpty(request["customerlevel"]))
                    serchstr += " and CustomerLevel_id = " + int.Parse(request["customerlevel_val"]);

                if (!string.IsNullOrEmpty(request["T_Provinces"]))
                    serchstr += " and Provinces_id = " + int.Parse(request["T_Provinces_val"]);

                if (!string.IsNullOrEmpty(request["T_City"]))
                    serchstr += " and City_id = " + int.Parse(request["T_City_val"]);

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

                if (!string.IsNullOrEmpty(request["startfollow"]))
                    serchstr += " and lastfollow >= '" + PageValidate.InputText(request["startfollow"], 255) + "'";

                if (!string.IsNullOrEmpty(request["endfollow"]))
                {
                    DateTime enddate = DateTime.Parse(request["endfollow"]).AddHours(23).AddMinutes(59).AddSeconds(59);
                    serchstr += " and lastfollow <= '" + enddate + "'";
                }
                if (!string.IsNullOrEmpty(request["isread"]))
                    serchstr += " and isRead = " + int.Parse(request["isread_val"]);
                
                //权限
                serchtxt += DataAuthFollow(request.Cookies["UserID"].Value);

                serchtxt += serchstr;

                //context.Response.Write(serchtxt);

                DataSet ds = customer.GetList(PageSize, PageIndex, serchtxt, sorttext, out Total);

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
                DataSet ds = customer.GetList("id=" + int.Parse(id) + DataAuth(request.Cookies["UserID"].Value));

                bool isRead = customer.SetRead(int.Parse(id), int.Parse(request.Cookies["UserID"].Value));

                string dt = Common.DataToJson.DataToJSON(ds);

                context.Response.Write(dt);
            }
            if (request["Action"] == "read")
            {
                string UserID = request["u"];
                DataSet ds = customer.GetListTotal("Employee_id=" + UserID.ToString() + " And isRead=0");

                string dt = Common.DataToJson.DataToJSON(ds);

                context.Response.Write(dt);
            }
            if (request["Action"] == "setread")
            {
                bool isRead = customer.SetRead(int.Parse(request["customer_id"]), int.Parse(request.Cookies["UserID"].Value));
                if (isRead)
                {
                    context.Response.Write("true");
                }
                else
                {
                    context.Response.Write("false");
                }
            }
            if (request["Action"] == "setimport")
            {
                bool isImport = customer.SetImport(int.Parse(request["id"]), int.Parse(request.Cookies["UserID"].Value));
                if (isImport)
                {
                    context.Response.Write("true");
                }
                else
                {
                    context.Response.Write("false");
                }
            }
            //转换正常客户
            if (request["Action"] == "upimp")
            {
                string id = request["id"];
                string imp = request["imp"];
                //imp = imp == "1" ? "0" : "1";
                imp = "2";

                DataSet ds = customer.GetList("id=" + int.Parse(id));

                bool upimp = true;
                if (uid != "admin")
                {
                    Data.GetDataAuth dataauth = new Data.GetDataAuth();
                    string txt = dataauth.GetDataAuthByid("1", "Sys_del", emp_id.ToString());

                    string[] arr = txt.Split(':');
                    switch (arr[0])
                    {
                        case "none":
                            upimp = false;
                            break;
                        case "my":
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i]["Employee_id"].ToString() == arr[1])
                                    //upimp = imp == "0" ? false : true;
                                    upimp = true;
                                else
                                    upimp = false;
                            }
                            break;
                        case "dep":
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i]["Department_id"].ToString() == arr[1])
                                    upimp = true;
                                else
                                    upimp = false;
                            }
                            break;
                        case "all":
                            upimp = true;
                            break;
                    }
                }
                if (upimp)
                {
                    bool isup = customer.UpdateImp(id, imp);
                    if (isup)
                    {
                        //日志
                        string EventType = "转换为正常客户";
                        //if (imp == "1")
                        //    EventType = "取消为正常客户";
                        int UserID = emp_id;
                        string UserName = empname;
                        string IPStreet = request.UserHostAddress;
                        int EventID = int.Parse(id);
                        string EventTitle = ds.Tables[0].Rows[0]["Customer"].ToString();
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
                    context.Response.Write("upfalse");
                }
            }
            if (request["Action"] == "count")
            {
                string id = request["id"];
                DataSet ds = customer.GetList("id=" + int.Parse(id));

                BLL.CRM_Contact contact = new BLL.CRM_Contact();
                BLL.CRM_contract contract = new BLL.CRM_contract();
                BLL.CRM_order order = new BLL.CRM_order();
                BLL.CRM_Follow follow = new BLL.CRM_Follow();

                int contactcount = 0, contractcount = 0, followcount = 0, ordercount = 0;
                contractcount = contract.GetList(" Customer_id=" + int.Parse(id)).Tables[0].Rows.Count;
                contactcount = contact.GetList(" C_customerid=" + int.Parse(id)).Tables[0].Rows.Count;
                followcount = follow.GetList(" Customer_id=" + int.Parse(id)).Tables[0].Rows.Count;
                ordercount = order.GetList(" Customer_id=" + int.Parse(id)).Tables[0].Rows.Count;

                context.Response.Write(string.Format("{0}联系人, {2}跟进, {3}订单，{1}合同 ", contactcount, contractcount, followcount, ordercount));
            }
            //预删除
            if (request["Action"] == "AdvanceDelete")
            {
                string id = request["id"];

                DataSet ds = customer.GetList("id=" + int.Parse(id));

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
                    bool isdel = customer.AdvanceDelete(int.Parse(request["id"]), 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (isdel)
                    {
                        //日志
                        string EventType = "客户预删除";

                        int UserID = emp_id;
                        string UserName = empname;
                        string IPStreet = request.UserHostAddress;
                        int EventID = int.Parse(id);
                        string EventTitle = ds.Tables[0].Rows[0]["Customer"].ToString();
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
            //读取重点信息
            if (request["Action"] == "gettop")
            {
                string id = request["id"];
                DataSet ds = customer.GetList("id=" + int.Parse(id));
                
                context.Response.Write(string.Format("{0}",ds.Tables[0].Rows[0]["isTop"].ToString()));
            }
            //标记重点客户
            if (request["Action"] == "UpTop")
            {
                string id = request["id"];
                string top = request["top"];
                top = top == "1" ? "0" : "1";

                DataSet ds = customer.GetList("id=" + int.Parse(id));

                bool uptop = true;
                if (uid != "admin")
                {
                    Data.GetDataAuth dataauth = new Data.GetDataAuth();
                    string txt = dataauth.GetDataAuthByid("1", "Sys_del", emp_id.ToString());

                    string[] arr = txt.Split(':');
                    switch (arr[0])
                    {
                        case "none":
                            uptop = false;
                            break;
                        case "my":
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i]["Employee_id"].ToString() == arr[1])
                                    uptop = top == "0" ? false : true;
                                else
                                    uptop = false;
                            }
                            break;
                        case "dep":
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i]["Department_id"].ToString() == arr[1])
                                    uptop = true;
                                else
                                    uptop = false;
                            }
                            break;
                        case "all":
                            uptop = true;
                            break;
                    }
                }
                if (uptop)
                {
                    bool isup = customer.UpdateTop(id, top);
                    if (isup)
                    {
                        //日志
                        string EventType = "标记重点客户";
                        if(top=="1")
                            EventType = "取消标记重点客户";
                        int UserID = emp_id;
                        string UserName = empname;
                        string IPStreet = request.UserHostAddress;
                        int EventID = int.Parse(id);
                        string EventTitle = ds.Tables[0].Rows[0]["Customer"].ToString();
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
                    context.Response.Write("upfalse");
                }
            }

            //regain            
            if (request["Action"] == "regain")
            {
                string idlist = PageValidate.InputText(request["idlist"], 100000);
                string[] arr = idlist.Split(',');

                DataSet ds = customer.GetList("id in (" + idlist.Trim() + ")");

                //日志   
                string EventType = "恢复删除商家";
                int UserID = emp_id;
                string UserName = empname;

                string IPStreet = request.UserHostAddress;
                string Original_txt = null;
                string Current_txt = null;

                int success = 0, failure = 0;   //计数
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    bool isregain = customer.AdvanceDelete(int.Parse(arr[i]), 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (isregain)
                    {
                        C_Sys_log log = new C_Sys_log();
                        int EventID = int.Parse(ds.Tables[0].Rows[i]["id"].ToString());
                        string EventTitle = ds.Tables[0].Rows[i]["Customer"].ToString();
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

                    string EventType = "彻底删除客户";

                    DataSet ds = customer.GetList("id in (" + idlist.Trim() + ")");

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
                        BLL.CRM_Contact contact = new BLL.CRM_Contact();
                        BLL.CRM_contract contract = new BLL.CRM_contract();
                        BLL.CRM_order order = new BLL.CRM_order();
                        BLL.CRM_Follow follow = new BLL.CRM_Follow();

                        int contactcount = 0, contractcount = 0, followcount = 0, ordercount = 0, success = 0, failure = 0;

                        //日志    
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string cid = ds.Tables[0].Rows[i]["id"].ToString();

                            contractcount = contract.GetList(" Customer_id=" + int.Parse(cid)).Tables[0].Rows.Count;
                            contactcount = contact.GetList(" C_customerid=" + int.Parse(cid)).Tables[0].Rows.Count;
                            followcount = follow.GetList(" Customer_id=" + int.Parse(cid)).Tables[0].Rows.Count;
                            ordercount = order.GetList(" Customer_id=" + int.Parse(cid)).Tables[0].Rows.Count;

                            //context.Response.Write( string.Format("{0}联系人, {2}跟进, {3}订单，{1}合同 ", contactcount, contractcount, followcount, ordercount)+":"+(contactcount > 0 || contractcount > 0 || followcount > 0 || ordercount > 0)+" ");

                            if (contactcount > 0 || contractcount > 0 || followcount > 0 || ordercount > 0)
                            {
                                failure++;

                            }
                            else
                            {
                                bool isdel = customer.Delete(int.Parse(cid));
                                if (isdel)
                                {
                                    success++;
                                    int UserID = emp_id;
                                    string UserName = empname;
                                    string IPStreet = request.UserHostAddress;
                                    int EventID = int.Parse(cid);
                                    string EventTitle = ds.Tables[0].Rows[i]["Customer"].ToString();
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
                string customerid = request["T_cid"];
                if (string.IsNullOrEmpty(customerid) || customerid == "null")
                    customerid = "0";

                DataSet ds = customer.GetList("Customer = N'" + Common.PageValidate.InputText(company, 255) + "' and id!=" + int.Parse(customerid));
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
                string customerid = request["T_cid"];
                if (string.IsNullOrEmpty(customerid) || customerid == "null")
                    customerid = "0";

                DataSet ds = customer.GetList("T_mobil = N'" + Common.PageValidate.InputText(company, 255) + "' and id!=" + int.Parse(customerid));
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

                DataSet ds = customer.Compared(DateTime.Parse(dt1), DateTime.Parse(dt2));

                string dt = GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);
            }

            if (request["Action"] == "Compared_type")
            {
                string dt1 = request["date1"];
                string dt2 = request["date2"];

                DataSet ds = customer.Compared_type(DateTime.Parse(dt1), DateTime.Parse(dt2));

                string dt = GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);

            }

            if (request["Action"] == "Compared_level")
            {
                string dt1 = request["date1"];
                string dt2 = request["date2"];

                DataSet ds = customer.Compared_level(DateTime.Parse(dt1), DateTime.Parse(dt2));

                string dt = GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);
            }

            if (request["Action"] == "Compared_source")
            {
                string dt1 = request["date1"];
                string dt2 = request["date2"];

                DataSet ds = customer.Compared_source(DateTime.Parse(dt1), DateTime.Parse(dt2));

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

                DataSet ds = customer.Compared_empcusadd(DateTime.Parse(dt1), DateTime.Parse(dt2), emplist);

                string dt = Common.GetGridJSON.DataTableToJSON(ds.Tables[0]);
                context.Response.Write(dt);
            }

            if (request["Action"] == "emp_customer")
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

                DataSet ds = customer.report_empcus(int.Parse(syear), emplist);

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
                            returntxt = " and ( privatecustomer='公客' or Employee_id=" + int.Parse(arr[1]) + ")";
                            break;
                        case "dep":
                            if (string.IsNullOrEmpty(arr[1]))
                                returntxt = " and ( privatecustomer='公客' or Employee_id=" + int.Parse(uid) + ")";
                            else
                                returntxt = " and ( privatecustomer='公客' or Department_id=" + int.Parse(arr[1]) + ")";
                            break;
                        case "depall":
                            BLL.hr_department dep = new BLL.hr_department();
                            DataSet ds = dep.GetAllList();
                            string deptask = GetDepTask(int.Parse(arr[1]), ds.Tables[0]);
                            string intext = arr[1] + "," + deptask;
                            returntxt = " and ( privatecustomer='公客' or Department_id in (" + intext.TrimEnd(',') + "))";
                            break;
                    }
                }
            }
            return returntxt;
        }
        /// <summary>
        /// 后续跟进权限
        /// </summary>
        private string DataAuthFollow(string uid)
        {
            //权限
            BLL.hr_employee emp = new BLL.hr_employee();
            DataSet dsemp = emp.GetList("ID=" + int.Parse(uid));

            string returntxt = " and 1=1 and Follow_up_id<>''";
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
                            returntxt = " and Follow_up_id=" + int.Parse(arr[1]);
                            break;
                        case "dep":
                            if (string.IsNullOrEmpty(arr[1]))
                                returntxt = " and Follow_up_id=" + int.Parse(uid);
                            else
                                returntxt = " and Follow_up_dep_id=" + int.Parse(arr[1]);
                            break;
                        case "depall":
                            BLL.hr_department dep = new BLL.hr_department();
                            DataSet ds = dep.GetAllList();
                            string deptask = GetDepTask(int.Parse(arr[1]), ds.Tables[0]);
                            string intext = arr[1] + "," + deptask;
                            returntxt = " and Follow_up_dep_id in (" + intext.TrimEnd(',') + ")";
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