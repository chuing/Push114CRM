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

            $("#T_Customer").ligerComboBox({
                width: 489//,
                //onBeforeOpen: f_selectContact
            })
            initcombo();
            if (getparastr("orderid")) {
                loadForm(getparastr("orderid"));
            }


            if (getparastr("Customer_id")) {
                $.ajax({
                    type: "GET",
                    url: "../../Data_crm/crm_customer.ashx", /* 注意后面的名字对应CS的方法名称 */
                    data: { Action: 'form', cid: getparastr("Customer_id"), rnd: Math.random() }, /* 注意参数的格式和名称 */
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        var obj = eval(result);
                        for (var n in obj) {

                        }
                        //alert(obj.constructor); //String 构造函数
                        $("#T_Customer").val(obj.Customer);
                        $("#T_Customer_val").val(obj.id);
                        $("#T_department").ligerGetComboBoxManager().selectValue(obj.Department_id, obj.Employee_id);
                        $("#T_Customer").ligerGetComboBoxManager().setReadOnly();
                    }
                });
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
                        {
                            display: '底价', name: 'base_price', width: 80, type: 'float', align: 'right', editor: { type: 'float' }, hide: 'hide', render: function (item) {
                                return toMoney(item.base_price);
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
                onAfterEdit: f_onAfterEdit,
                enabledEdit: true,
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
            $('#T_status').ligerComboBox({ width: 182, url: "../../Data_crm/Param_SysParam.ashx?Action=combo&parentid=6&rnd=" + Math.random() });
            $('#T_paytype').ligerComboBox({ width: 196, url: "../../Data_crm/Param_SysParam.ashx?Action=combo&parentid=5&rnd=" + Math.random() });
            a = $('#T_employee').ligerComboBox({ readonly: true, width: 96 });
            b = $('#T_department').ligerComboBox({
                width: 96,
                valueField: 'id',
                textField: 'text',
                treeLeafOnly: false,
                readonly: true,
                tree: {
                    url: '../../Data_crm/hr_department.ashx?Action=tree&rnd=' + Math.random(),
                    idFieldName: 'id',
                    parentIDFieldName: 'pid',
                    checkbox: false
                },
                onSelected: function (newvalue, newtext, newid) {
                    $.get("../../Data_crm/hr_employee.ashx?Action=combo&did=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        a.setData(eval(data));
                        a.selectValue(newid);
                    });
                }
            });
            c = $('#T_employee1').ligerComboBox({ width: 96 });
            d = $('#T_department1').ligerComboBox({
                width: 96,
                selectBoxWidth: 240,
                selectBoxHeight: 200,
                valueField: 'id',
                textField: 'text',
                treeLeafOnly: false,
                tree: {
                    url: '../../Data_crm/hr_department.ashx?Action=tree&rnd=' + Math.random(),
                    idFieldName: 'id',
                    parentIDFieldName: 'pid',
                    checkbox: false
                },
                onSelected: function (newvalue, newtext, newid) {
                    $.get("../../Data_crm/hr_employee.ashx?Action=combo&did=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        c.setData(eval(data));
                        c.selectValue(newid);
                    });
                }
            });
            var pData = [{ id: 0, text: '不予通过' }, { id: 1, text: '通过审核' }, { id: 2, text: '待审核中' }, { id: 3, text: '作废' }];
            $("#T_Pass_status").ligerComboBox({ width: 96, data: pData });
        }

        function loadForm(oaid) {
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
                    $("#T_Customer_val").val(obj.Customer_id);

                    $("#T_department1").val(obj.F_dep_id);
                    $("#T_department1_val").val(obj.F_dep_name);
                    $("#T_employee1").val(obj.F_emp_name);
                    $("#T_employee1_val").val(obj.F_emp_id);

                    $("#T_date").val(formatTimebytype(obj.Order_date, "yyyy-MM-dd"));
                    $("#T_details").val(obj.Order_details);
                    $("#T_amount").val(toMoney(obj.Order_amount));

                    $("#T_department").ligerGetComboBoxManager().selectValue(obj.C_dep_id, obj.C_emp_id);
                    $("#T_department1").ligerGetComboBoxManager().selectValue(obj.F_dep_id, obj.F_emp_id);
                    $("#T_status").ligerGetComboBoxManager().selectValue(obj.Order_status_id);
                    $("#T_paytype").ligerGetComboBoxManager().selectValue(obj.pay_type_id);
                    $("#T_Pass_status").ligerGetComboBoxManager().selectValue(obj.Pass_status_id);
                    $("#T_Pass_details").val(obj.Pass_details);
                }
            });
        }

        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=pass&orderid=" + getparastr("orderid");
                return $("form :input").fieldSerialize() + sendtxt;
            }
        }
    </script>

</head>
<body style="margin: 0">
        <div>
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
                        <input type="text" id="T_Customer" name="T_Customer" validate="{required:true}" /></td>
                </tr>
                <tr>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            客户所属：
                        </div>
                    </td>
                    <td>
                        <div style="width: 100px; float: left">
                            <input id="T_department" name="T_department" type="text" validate="{required:true}" style="width: 97px" />
                        </div>
                        <div style="width: 98px; float: left">
                            <input id="T_employee" name="T_employee" type="text" validate="{required:true}" style="width: 96px" />
                        </div>
                    </td>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            成交时间：
                        </div>
                    </td>
                    <td>
                        <input type="text" id="T_date" name="T_date" ltype="date" validate="{required:true,disabled:true}" ligerui="{width:182}" /></td>
                </tr>
                <tr>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            促成人员：
                        </div>
                    </td>
                    <td>
                        <div style="width: 100px; float: left">
                            <input id="T_department1" name="T_department1" type="text" validate="{required:true}" style="width: 97px" />
                        </div>
                        <div style="width: 98px; float: left">
                            <input id="T_employee1" name="T_employee1" type="text" validate="{required:true}" style="width: 96px" />
                        </div>
                    </td>
                    <td width="62px">
                        <div align="right" style="width: 61px">
                            订单状态：
                        </div>
                    </td>
                    <td>

                        <input type="text" id="T_status" name="T_status" validate="{required:true}" /></td>
                </tr>
                <tr>
                    <td>
                        <div align="right" style="width: 62px">订单详情：</div>
                    </td>
                    <td colspan="3">
                        <textarea cols="100" id="T_details" name="T_details" rows="3" class="l-textarea" style="width: 488px"></textarea></td>
                </tr>

                <tr>
                    <td>
                        <div align="right" style="width: 62px">支付方式：</div>
                    </td>
                    <td>
                        <input type="text" id="T_paytype" name="T_paytype" validate="{required:true}" ligerui="{ReadOnly:true}" />
                    </td>
                    <td>
                        <div align="right" style="width: 62px">金额：</div>
                    </td>
                    <td>
                        <input type="text" id="T_amount" name="T_amount" ltype="text" style="text-align: right" value="0" validate="{required:true}" ligerui="{width:182,disabled:true}" /></td>
                </tr>
                <tr>
                    <td colspan="4" class="table_title1">订单产品</td>
                </tr>
            </table>
            <div id="maingrid4" style="margin: 5px;"></div>
        </div>
    <form id="form1">            
            <table style="width: 600px; margin: 15px 5px 5px 5px;" class='bodytable1'>
                <tr>
                    <td width="66px"><b>审核状态：</b></td>
                    <td><input type="text" id="T_Pass_status" name="T_Pass_status" validate="{required:true}" /></td>
                </tr>
                <tr>
                    <td width="66px"><b>审核备注：</b></td>
                    <td><textarea cols="100" id="T_Pass_details" name="T_Pass_details" rows="3" class="l-textarea" style="width: 488px" ></textarea></td>
                </tr>
            </table>
        </form>
</body>
</html>
