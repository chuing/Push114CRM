﻿<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../lib/ligerUI/skins/ext/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Css_crm/input.css" rel="stylesheet" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>

    <script src="../../lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script src="../../lib/jquery-validation/jquery.validate.js" type="text/javascript"></script>
    <script src="../../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/common.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../lib/jquery.form.js" type="text/javascript"></script>
    <script src="../../Js_crm/Toolbar.js" type="text/javascript"></script>
    <script src="../../Js_crm/CRM.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $.metadata.setType("attr", "validate");
            XHD.validate($(form1));

            $("form").ligerForm();
            initselect();

            if (getparastr("cid")) {
                loadForm(getparastr("cid"));
            }
            else {
                $.get("../../Data_crm/hr_employee.ashx?Action=init&rnd=" + Math.random(), function (data, textStatus) {
                    //alert(data);
                    var arrstr = new Array();
                    arrstr = data.split("|");

                    h.selectValue(arrstr[1], arrstr[0]);
                    //setTimeout("initemp(" + arrstr[0] + ")", 500);

                });
            }

            //$("#T_company").attr("validate", "{ required: true, remote: remote, messages: { required: '请输入客户名', remote: '此客户已存在!' } }");
            //$("#T_mobil").attr("validate", "{ required: true, remote: chkmobil, messages: { required: '请输入客户手机', remote: '此客户已手机已存在!' } }");

            $("#T_company_tel").blur(function () { var $tel = $("#T_tel"); $tel.val() == "" ? $tel.val($(this).val()) : $tel.val(); });

        })
        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=save&id=" + getparastr("cid");
                return $("form :input").fieldSerialize() + sendtxt;
            }
        }
        var a; var b; var c; var d; var e; var f; var g; var h; var i;
        function initselect() {
            b = $('#T_City').ligerComboBox({ width: 96 });
            c = $('#T_Provinces').ligerComboBox({
                width: 97,
                url: "../../Data_crm/Param_City.ashx?Action=combo1&rnd=" + Math.random(),
                onSelected: function (newvalue, newtext, newid) {
                    $.get("../../Data_crm/Param_City.ashx?Action=combo2&pid=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        b.setData(eval(data));
                        //alert(newid);
                        b.selectValue(newid)
                    });
                }
            });
            d = $('#T_customertype').ligerComboBox({ width: 97, url: "../../Data_crm/Param_SysParam.ashx?Action=combo&parentid=1&rnd=" + Math.random() });
            e = $('#T_customerlevel').ligerComboBox({ width: 96, url: "../../Data_crm/Param_SysParam.ashx?Action=combo&parentid=2&rnd=" + Math.random() });
            f = $('#T_CustomerSource').ligerComboBox({ width: 196, url: "../../Data_crm/Param_SysParam.ashx?Action=combo&parentid=3&rnd=" + Math.random() });
            i = $("#T_industry").ligerComboBox({ width: 196, url: "../../Data_crm/Param_SysParam.ashx?Action=combo&parentid=8&rnd=" + Math.random() });
            g = $('#T_employee').ligerComboBox({ width: 96 });
            h = $('#T_department').ligerComboBox({
                width: 96,
                selectBoxWidth: 240,
                selectBoxHeight: 200,
                valueField: 'id',
                textField: 'text',
                treeLeafOnly: false,
                tree: {
                    url: '../../Data_crm/hr_department.ashx?Action=tree&auth=1&rnd=' + Math.random(),
                    idFieldName: 'id',
                    parentIDFieldName: 'pid',
                    checkbox: false
                },
                onSelected: function (newvalue, newtext, newid) {
                    $.get("../../Data_crm/hr_employee.ashx?Action=combo&auth=1&did=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        g.setData(eval(data));
                        g.selectValue(newid);
                    });
                }
            });
            $("#T_private").ligerGetComboBoxManager().selectValue('私客')

        }

        function loadForm(oaid) {
            $("#tr_contact0,#tr_contact1,#tr_contact2,#tr_contact3,#tr_contact4,#tr_contact5").remove();
            $.ajax({
                type: "GET",
                url: "../../Data_crm/crm_customer.ashx", /* 注意后面的名字对应CS的方法名称 */
                data: { Action: 'form', cid: oaid, rnd: Math.random() }, /* 注意参数的格式和名称 */
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var obj = eval(result);
                    for (var n in obj) {
                        if (obj[n] == "null" || obj[n] == null)
                            obj[n] = "";
                    }
                    //alert(obj.constructor); //String 构造函数
                    $("#T_company").val(obj.Customer);
                    $("#O_company").val(obj.Customer);
                    $("#T_customername").val(obj.contact);
                    $("#T_address").val(obj.address);
                    $("#T_qq").val(obj.QQ);
                    $("#T_mobil").val(obj.mobil);
                    $("#T_tel").val(obj.tel);
                    $("#T_fax").val(obj.fax);
                    $("#T_Website").val(obj.site);
                    $("#T_email").val(obj.email);
                    $("#T_descript").val(obj.DesCripe);
                    $("#T_remarks").val(obj.Remarks);
                    $("#T_contact_dep").val(obj.contact_dep);
                    $("#T_contact_position").val(obj.contact_position);
                    //$("#T_industry").val(obj.industry);
                    $("#T_company_tel").val(obj.tel);

                    $("#T_Provinces").ligerGetComboBoxManager().selectValue(obj.Provinces_id, obj.City_id);
                    $("#T_industry").ligerGetComboBoxManager().selectValue(obj.industry_id);
                    $("#T_customertype").ligerGetComboBoxManager().selectValue(obj.CustomerType_id);
                    $("#T_customerlevel").ligerGetComboBoxManager().selectValue(obj.CustomerLevel_id);
                    $("#T_CustomerSource").ligerGetComboBoxManager().selectValue(obj.CustomerSource_id);
                    $("#T_private").ligerGetComboBoxManager().selectValue(obj.privatecustomer);
                    $("#T_department").ligerGetComboBoxManager().selectValue(obj.Department_id, obj.Employee_id);
                    $("#T_emp").val(obj.Employee_id);
                }
            });
        }

        function remote() {
            var url = "../../Data_crm/CRM_Customer.ashx?Action=validate&T_cid=" + getparastr("cid") + "&rnd=" + Math.random();
            return url;
        }

        function chkmobil() {
            var url = "../../data_crm/CRM_Customer.ashx?Action=mobile&T_cid=" + getparastr("cid") + "&rnd=" + Math.random();
            return url;
        }

    </script>
</head>
<body>
    <form id="form1">
        <table style="width: 600px; margin: 5px;" class='bodytable1'>
            <tr>
                <td colspan="4" class="table_title1">基本信息</td>
            </tr>
            <tr>
                <td>
                    <div style="width: 80px; text-align: right; float: right">公司名称：</div>
                </td>
                <td>
                    <input type="text" id="T_company" name="T_company" ltype="text" ligerui="{width:196}" validate="{required:true}" />
                    <input type="hidden" id="O_company" name="O_company" />
                </td>
                <td>
                    <div style="width: 80px; text-align: right; float: right">公司网址：</div>
                </td>
                <td>
                    <input id="T_Website" name="T_Website" type="text" ltype="text" ligerui="{width:196}" validate="{required:false,url:true}" /></td>
            </tr>
            <tr>
                <td>

                    <div style="width: 80px; text-align: right; float: right">所属行业：</div>

                </td>
                <td>
                    <input type="text" id="T_industry" name="T_industry" /></td>
                <td>
                    <div style="width: 80px; text-align: right; float: right">所属地区：</div>
                </td>
                <td>
                    <div style="width: 100px; float: left">
                        <input id="T_Provinces" name="T_Provinces" type="text" style="width: 96px;" />
                    </div>
                    <div style="width: 98px; float: left">
                        <input id="T_City" name="T_City" type="text" style="width: 96px;" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>

                    <div style="width: 80px; text-align: right; float: right">公司电话：</div>

                </td>
                <td>

                    <input id="T_company_tel" name="T_company_tel" type="text" ltype="text" ligerui="{width:196}"  validate="{required:true}"/></td>
                <td>
                    <div style="width: 80px; text-align: right; float: right">传真：</div>
                </td>
                <td>
                    <input id="T_fax" name="T_fax" type="text" ltype="text" ligerui="{width:196}" /></td>
            </tr>
            <tr>
                <td>

                    <div style="width: 80px; text-align: right; float: right">公司地址：</div>

                </td>
                <td colspan="3">

                    <input type="text" id="T_address" name="T_address" ltype="text" ligerui="{width:495}"/></td>
            </tr>
            <tr id="tr_contact0">
                <td colspan="4" class="table_title1">主联系人</td>
            </tr>
            <tr id="tr_contact1">
                <td>
                    <div style="width: 80px; text-align: right; float: right">联系人：</div>
                </td>
                <td>
                    <%--<input type="text" id="T_customername" name="T_customername" style="width: 96px;" ltype="text" ligerui="{width:196}" validate="{required:true}" />--%>
                    <div style="width: 140px; float: left">
                        <input id="T_customername" name="T_customername" type="text" ltype="text" ligerui="{width:136}" style="width: 146px" validate="{required:true}" />
                    </div>
                    <div style="width: 58px; float: left">
                        <input type="text" id="T_sex" name="T_sex" style="width: 56px" ltype="select" ligerui="{width:56,data:[{id:'先生',text:'先生'},{id:'女士',text:'女士'}]}" validate="{required:true}" />
                    </div>
                </td>
                <td>
                    <div style="width: 80px; text-align: right; float: right">部门、职务：</div>
                </td>
                <td>
                    <div style="width: 100px; float: left">
                        <input type="text" id="T_contact_dep" name="T_contact_dep" ltype="text" ligerui="{width:96}" />
                    </div>
                    <div style="width: 98px; float: left">
                        <input type="text" id="T_contact_position" name="T_contact_position" ltype="text" ligerui="{width:96}" />
                    </div>
                </td>
            </tr>
            <tr id="tr_contact2">
                <td>
                    <div style="width: 80px; text-align: right; float: right">电子邮件：</div>
                </td>
                <td>
                    <input id="T_email" name="T_email" type="text" ltype="text" ligerui="{width:196}" validate="{required:false,email:true}" /></td>
                <td>
                    <div style="width: 80px; text-align: right; float: right">QQ：</div>
                </td>
                <td>

                    <input type="text" id="T_qq" name="T_qq" ltype="text" ligerui="{width:196}" /></td>
            </tr>
            <tr id="tr_contact3">
                <td>

                    <div style="width: 80px; text-align: right; float: right">联系电话：</div>

                </td>
                <td>

                    <input id="T_tel" name="T_tel" type="text" ltype="text" ligerui="{width:196}" /></td>
                <td>

                    <div style="width: 80px; text-align: right; float: right">手机：</div>

                </td>
                <td>
                    <input id="T_mobil" name="T_mobil" type="text" ltype="text" ligerui="{width:196}" validate="{required:true}" /></td>
            </tr>
            <tr id="tr_contact4">
                <td>
                    <div style="width: 80px; text-align: right; float: right">爱好：</div>
                </td>
                <td colspan="3">

                    <input id="T_hobby" name="T_hobby" type="text" ltype="text" ligerui="{width:495}" /></td>
            </tr>
            <tr id="tr_contact5">
                <td>
                    <div style="width: 80px; text-align: right; float: right">备注：</div>
                </td>
                <td colspan="3">
                    <input id="T_contact_remarks" name="T_contact_remarks" type="text" ltype="text" ligerui="{width:495}" /></td>
            </tr>
            <tr>
                <td colspan="4" class="table_title1">其他</td>
            </tr>
            <tr>
                <td>
                    <div style="width: 80px; text-align: right; float: right">客户意向：</div>
                </td>
                <td>
                    <div style="width: 100px; float: left">
                        <input id="T_customertype" name="T_customertype" type="text" style="width: 96px" />
                    </div>
                    <div style="width: 98px; float: left; display:none;">
                        <input id="T_customerlevel" name="T_customerlevel" type="text" style="width: 96px;" />
                    </div>
                </td>
                <td>
                    <div style="width: 80px; text-align: right; float: right">客户来源：</div>
                </td>
                <td>
                    <input id="T_CustomerSource" name="T_CustomerSource" type="text" />
                </td>
            </tr>

            <tr>
                <td>
                    <div style="width: 80px; text-align: right; float: right">客户描述：</div>
                </td>
                <td colspan="3">
                    <input id="T_descript" name="T_descript" type="text" ltype="text" ligerui="{width:495}" /></td>
            </tr>
            <tr>
                <td>
                    <div style="width: 80px; text-align: right; float: right">备&nbsp; 注：</div>
                </td>
                <td colspan="3">
                    <input id="T_remarks" name="T_remarks" type="text" ltype="text" ligerui="{width:495}" /></td>
            </tr>
            <tr>
                <td colspan="4" class="table_title1">归属</td>
            </tr>
            <tr>
                <td>
                    <div style="width: 80px; text-align: right; float: right">状态：</div>
                </td>
                <td>
                    <input id="T_private" name="T_private" type="text" ltype="select" ligerui="{width:196,data:[{id:'私客',text:'私客'},{id:'公客',text:'公客'}]}" validate="{required:true}" /></td>
                <td>
                    <div style="width: 80px; text-align: right; float: right">业务员：</div>
                </td>
                <td>
                    <div style="width: 100px; float: left">
                        <input id="T_department" name="T_department" type="text" validate="{required:true}" style="width: 97px" />
                    </div>
                    <div style="width: 98px; float: left">
                        <input id="T_employee" name="T_employee" type="text" validate="{required:true}" style="width: 96px" />
                        <input id="T_emp" name="T_emp" type="hidden" />
                    </div>
                </td>
            </tr>
            <%--<tr>
                <td colspan="4">
                    <div id="toolbar" style="width: 585px;"></div>
                    <div id="maingrid4" style="margin: -1px;"></div>
                </td>
            </tr>--%>
        </table>


    </form>
</body>
</html>
