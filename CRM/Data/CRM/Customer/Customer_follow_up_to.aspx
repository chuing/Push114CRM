﻿<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title></title>
    <link href="../../Css_crm/input.css" rel="stylesheet" />
    <link href="../../lib/ligerUI/skins/ext/css/ligerui-all.css" rel="stylesheet" type="text/css" />


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
    <script src="../../lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>

    <script src="../../lib/jquery-validation/jquery.validate.js" type="text/javascript"></script>
    <script src="../../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/common.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../lib/jquery.form.js" type="text/javascript"></script>

    <script src="../../lib/json2.js" type="text/javascript"></script>

    <script src="../../Js_crm/CRM.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $.metadata.setType("attr", "validate");
            XHD.validate($(form1));

            //$("#T_Contract_name").focus();
            $("form").ligerForm();

            initcombo();
            if (getparastr("orderid")) {
                loadForm(getparastr("orderid"), getparastr("customerid"));
            }
            $("#maingrid4").ligerGrid({
                columns: [
                        //{ display: '序号', width: 30, render: function (item, i) { return i + 1;} },
                        { display: '产品名', name: 'product_name', width: 120 },
                        {
                            display: '单价', name: 'price', width: 80, type: 'float', align: 'right', editor: { type: 'float' }, render: function (item) {
                                return toMoney(item.price);
                            }
                        },
                        { display: '数量', name: 'quantity', width: 100, type: 'int', editor: { type: 'int', onChange: onChange } },
                        { display: '单位', name: 'unit', width: 40, editor: { type: 'text' } },
                        {
                            display: '总价', name: 'amount', width: 100, type: 'float', align: 'right', render: function (item) {
                                return toMoney(item.amount);
                            }
                        }

                ],
                //selectRowButtonOnly: true,
                //onAfterEdit: f_onAfterEdit,
                enabledEdit: false,
                usePager: false,
                checkbox: true,
                url: "../../Data_crm/CRM_order_details.ashx?Action=grid&orderid=" + getparastr("orderid"),
                width: '598px', height: '150px',
                heightDiff: 0
            })

        });
        function onChange(rowcell, newValue) {
            var g = $("#maingrid4").ligerGetGridManager();
            var row = $(rowcell).parent();
            var rowid = row.attr("rowid");
            var rowdata = g.getRow(rowid);

            g.updateCell('amount', rowdata.price * rowdata.quantity, rowdata);
            $("#T_amount").val(toMoney(g.getColumnDateByType('amount', 'sum') * 1.0));
        }
        function f_onAfterEdit(e) {
            var manager = $("#maingrid4").ligerGetGridManager();
            manager.updateCell('amount', e.record.price * e.record.quantity, e.rowObj);
            $("#T_amount").val(toMoney(manager.getColumnDateByType('amount', 'sum') * 1.0));
        }
        function initcombo() {
            e = $('#F_employee').ligerComboBox({ width: 96 });
            f = $('#F_department').ligerComboBox({
                width: 98,
                selectBoxWidth: 240,
                selectBoxHeight: 200,
                valueField: 'id',
                textField: 'text',
                treeLeafOnly: false,
                //readonly: true,
                tree: {
                    url: '../../Data_crm/hr_department.ashx?Action=tree&rnd=' + Math.random(),
                    idFieldName: 'id',
                    parentIDFieldName: 'pid',
                    checkbox: false
                },
                onSelected: function (newvalue, newtext, newid) {
                    $.get("../../Data_crm/hr_employee.ashx?Action=combo&did=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        e.setData(eval(data));
                        e.selectValue(newid);
                    });
                }
            });
        }

        function loadForm(oaid,caid) {
            $.ajax({
                type: "GET",
                url: "../../Data_crm/CRM_order.ashx", /* 注意后面的名字对应CS的方法名称 */
                data: { Action: 'form', orderid: oaid, rnd: Math.random() }, /* 注意参数的格式和名称 */
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var obj = eval(result);
                    for (var n in obj) {

                    }
                    //alert(UrlDecode("2013%2F4%2F14%200%3A00%3A00"));
                    //alert(obj.constructor); //String 构造函数
                    $("#T_Customer").val(obj.Customer_name);

                    $("#T_department").val(obj.C_dep_name);
                    $("#T_employee").val(obj.C_emp_name);
                    $("#T_department1").val(obj.F_dep_name);
                    $("#T_employee1").val(obj.F_emp_name);

                    $("#T_date").val(formatTimebytype(obj.Order_date, "yyyy-MM-dd"));
                    $("#T_details").val(obj.Order_details);
                    $("#T_amount").val(toMoney(obj.Order_amount));

                    $("#T_status").val(obj.Order_status);
                    $("#T_paytype").val(obj.pay_type);
                }
            });
            $.ajax({
                type: "GET",
                url: "../../Data_crm/crm_customer.ashx", /* 注意后面的名字对应CS的方法名称 */
                data: { Action: 'form', cid: caid, rnd: Math.random() }, /* 注意参数的格式和名称 */
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var obj = eval(result);
                    for (var n in obj) {
                        if (obj[n] == "null" || obj[n] == null)
                            obj[n] = "";
                    }
                    $("#F_department").ligerGetComboBoxManager().selectValue(obj.Follow_up_dep_id, obj.Follow_up_id);
                    $("#F_employee").val(obj.Follow_up_id);
                }
            });
        }
        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=savefollow&orderid=" + getparastr("orderid") + "&id=" + getparastr("customerid");
                return $("#f_follow :input").fieldSerialize() + sendtxt;
            }
        }
    </script>

</head>
<body style="margin: 0">  
        <div>
            <form id="view">
            <table style="width: 600px; margin: 5px;" class='bodytable1'>
                <tr>
                    <td colspan="4" class="table_title1">基本信息</td>
                </tr>
                <tr>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            客户：
                        </div>
                    </td>
                    <td colspan="3">
                        <input type="text" id="T_Customer" name="T_Customer" ltype="text" ligerui="{width:485,disabled:true}" />
                        <input type="hidden" id="T_Customer_id" />
                    </td>
                </tr>
                <tr>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            客户所属：
                        </div>
                    </td>
                    <td>
                        <div style="width: 100px; float: left">
                            <input id="T_department" name="T_department" type="text" ltype="text" ligerui="{width:97,disabled:true}" />
                        </div>
                        <div style="width: 98px; float: left">
                            <input id="T_employee" name="T_employee" type="text" ltype="text" ligerui="{width:96,disabled:true}" />
                        </div>
                    </td>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            成交时间：
                        </div>
                    </td>
                    <td>
                        <input type="text" id="T_date" name="T_date" ltype="text" ligerui="{width:182,disabled:true}" /></td>
                </tr>
                <tr>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            促成人员：
                        </div>
                    </td>
                    <td>
                        <div style="width: 100px; float: left">
                            <input id="T_department1" name="T_department1" type="text" ltype="text" ligerui="{width:97,disabled:true}" />
                        </div>
                        <div style="width: 98px; float: left">
                            <input id="T_employee1" name="T_employee1" type="text" ltype="text" ligerui="{width:96,disabled:true}" />
                        </div>
                    </td>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            订单状态：
                        </div>
                    </td>
                    <td>
                        <input type="text" id="T_status" name="T_status" ltype="text" ligerui="{width:182,disabled:true}" /></td>
                </tr>
                <tr>
                    <td>
                        <div align="right" style="width: 62px">订单详情：</div>
                    </td>
                    <td colspan="3">
                        <textarea cols="100" id="T_details" name="T_details" rows="4" class="l-textarea" style="width: 483px" disabled ></textarea></td>
                </tr>

                <tr>
                    <td>
                        <div align="right" style="width: 62px">支付方式：</div>
                    </td>
                    <td>
                        <input type="text" id="T_paytype" name="T_paytype" ltype="text" ligerui="{width:182,disabled:true}" />
                    </td>
                    <td>
                        <div align="right" style="width: 62px">金额：</div>
                    </td>
                    <td>
                        <input type="text" id="T_amount" name="T_amount" ltype="text" style="text-align: right" value="0" ligerui="{width:182,disabled:true}" /></td>
                </tr>
                <tr>
                    <td colspan="4" class="table_title1">订单产品</td>
                </tr>
            </table>
            <div id="maingrid4" style="margin: 5px;"></div>
            </form>
            <form id="form1">
            <table id="f_follow" style="width: 600px; margin: 5px;" class='bodytable1'>
                <tr>
                    <td width="67px" align="right">转后续人：</td>
                    <td>
                        <div style="width: 100px; padding:15px 0px; float: left">
                            <input id="F_department" name="F_department" type="text" validate="{required:true}" style="width: 97px" />
                        </div>
                        <div style="width: 98px; padding:15px 0px; float: left">
                            <input id="F_employee" name="F_employee" type="text" validate="{required:true}" style="width: 96px" />
                        </div>
                    </td>
                </tr>
            </table>
            </form>
        </div>

</body>
</html>
