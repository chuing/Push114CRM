using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using XHD.Common;

namespace XHD.CRM.Data
{
    /// <summary>
    /// sys_import 的摘要说明
    /// </summary>
    public class sys_import : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;

            DataTable dt = new DataTable();
            string msg = "";
            string filename = request["T_filename"];
            string path = "/img_crm/impfile/";
            if (File.Exists(context.Server.MapPath(path + filename)))
            {
                dt = ExcelToDataTable(context.Server.MapPath(path + filename), "Sheet1");

                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    BLL.CRM_Customer customer = new BLL.CRM_Customer();
                    Model.CRM_Customer model = new Model.CRM_Customer();

                    BLL.hr_employee emp = new BLL.hr_employee();

                    int u_id = int.Parse(request.Cookies["UserID"].Value);
                    DataSet dsuser = emp.GetList("id=" + u_id);
                    string uname = dsuser.Tables[0].Rows[0]["name"].ToString();
                    string uid = dsuser.Tables[0].Rows[0]["uid"].ToString();

                    string empid = request["T_employee_val"];
                    if (string.IsNullOrEmpty(empid))
                        empid = "0";
                    int emp_id = int.Parse(empid);
                    DataSet dsemp = emp.GetList("id=" + emp_id);
                    string depid = dsemp.Tables[0].Rows[0]["d_id"].ToString();
                    string depname = dsemp.Tables[0].Rows[0]["dname"].ToString();
                    string empname = dsemp.Tables[0].Rows[0]["name"].ToString();

                    int s = int.Parse(request["T_start"]);
                    int e = int.Parse(request["T_end"]);
                    if (e == 0)
                        e = dt.Rows.Count;

                    int suc_N = 0, fal_N = 0;
                    for (int i = s; i <= e; i++)
                    {
                        model.Customer = PageValidate.InputText(dt.Rows[i][0].ToString(), 255);
                        model.address = PageValidate.InputText(dt.Rows[i][1].ToString(), 255);
                        model.fax = PageValidate.InputText(request["T_fax"], 255);
                        model.site = PageValidate.InputText(request["T_Website"], 255);

                        int ct = 0;
                        string mobil = dt.Rows[i][2].ToString();
                        if (string.IsNullOrEmpty(mobil))
                            mobil = dt.Rows[i][4].ToString();
                        if (!string.IsNullOrEmpty(mobil))
                        {
                            model.tel = PageValidate.InputText(mobil, 255);

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
                            //model.privatecustomer = PageValidate.InputText(request["T_private"], 255);
                            model.privatecustomer = "私客";

                            //string depid = request["T_department_val"];
                            //if (string.IsNullOrEmpty(depid))
                            //    depid = "0";
                            //model.Department_id = int.Parse(depid);
                            //model.Department = PageValidate.InputText(request["T_department"], 255);
                            model.Department_id = int.Parse(depid);
                            model.Department = PageValidate.InputText(depname, 255);

                            //string empid = request["T_employee_val"];
                            //if (string.IsNullOrEmpty(empid))
                            //    empid = "0";
                            //model.Employee_id = int.Parse(empid);
                            //model.Employee = PageValidate.InputText(request["T_employee"], 255);
                            model.Employee_id = emp_id;
                            model.Employee = PageValidate.InputText(empname, 255);


                            model.isDelete = 0;
                            DateTime nowtime = DateTime.Now;
                            model.Create_date = nowtime;
                            model.Serialnumber = nowtime.AddMilliseconds(3).ToString("yyyyMMddHHmmssfff").Trim();
                            model.Create_id = u_id;
                            model.Create_name = Common.PageValidate.InputText(uname, 255);
                            //string isread = "0";
                            model.isRead = 0;
                            model.isImport = 1;

                            int customerid = customer.Add(model);

                            BLL.CRM_Contact contact = new BLL.CRM_Contact();
                            Model.CRM_Contact modelcontact = new Model.CRM_Contact();
                            modelcontact.isDelete = 0;
                            modelcontact.C_name = PageValidate.InputText(dt.Rows[i][3].ToString(), 255);
                            modelcontact.C_sex = "-";
                            modelcontact.C_department = PageValidate.InputText(request["T_dep"], 255);
                            modelcontact.C_position = PageValidate.InputText(request["T_position"], 255);
                            modelcontact.C_QQ = PageValidate.InputText(request["T_qq"], 255);
                            modelcontact.C_tel = PageValidate.InputText(dt.Rows[i][4].ToString(), 255);
                            modelcontact.C_mob = PageValidate.InputText(mobil, 255);
                            modelcontact.C_email = Common.PageValidate.InputText(request["T_email"], 255);
                            modelcontact.C_customerid = customerid;
                            modelcontact.C_customername = model.Customer;
                            modelcontact.C_createId = u_id;
                            modelcontact.C_createDate = DateTime.Now;
                            modelcontact.C_hobby = PageValidate.InputText(request["T_hobby"], 1000);
                            modelcontact.C_remarks = PageValidate.InputText(request["T_contact_remarks"], 4000);
                            ct = contact.Add(modelcontact);
                            //context.Response.Write(ct);
                        }
                        if (ct > 0)
                        {
                            suc_N += 1;
                            sb.Append(i + ". " + dt.Rows[i][3].ToString() + "(" + model.Customer + ") - 导入 成功 <br />");
                        }
                        else
                        {
                            fal_N += 1;
                            sb.Append(i + ". " + dt.Rows[i][3].ToString() + "(" + model.Customer + ") - 导入 <font color=#ff0000>失败</font> <br />");
                        }

                    }
                    msg = "导入情况：" + suc_N.ToString() + " 成功，" + fal_N.ToString() + " 失败，共 " + (e - s + 1).ToString() + " <br />" + sb.ToString();
                }
                else
                {
                    msg = "文件里没有你所需要的数据！";
                }
            }
            else
            {
                msg="文件不存在！";
            }
            context.Response.Write(msg);
        }

        public static DataTable ExcelToDataTable(string FileFullPath, string strSheetName)
        {
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + FileFullPath + ";" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + FileFullPath + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'";

            string strExcel = string.Format("select * from [{0}$]", strSheetName);
            //string strExcel = "select * from [Sheet1$]";

            DataSet ds = new DataSet();

            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, strConn);
            adapter.Fill(ds, strSheetName);

            conn.Close();

            return ds.Tables[strSheetName];
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