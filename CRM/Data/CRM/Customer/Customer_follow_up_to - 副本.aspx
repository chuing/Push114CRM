<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
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
            if (getparastr("orderid")) {
                loadorder(getparastr("orderid"));
            }
            else {
                alert("系统无法在没有订单的情况下开票！");
                top.jQuery.ligerDialog.close();
            }
            $.metadata.setType("attr", "validate");
            XHD.validate($(form1));

            //$("#T_Contract_name").focus();
            $("form").ligerForm();
            initcombo();

            if (getparastr("receiveid")) {
                loadForm(getparastr("receiveid"));
            }

        });

        function initcombo() {
            /*
            d = $('#T_invoice_type').ligerComboBox({ width: 182, url: "../../Data_crm/Param_SysParam.ashx?Action=combo&parentid=5&rnd=" + Math.random() });
            a = $('#T_employee').ligerComboBox({ width: 82 });
            b = $('#T_department').ligerComboBox({
                width: 96,
                selectBoxWidth: 240,
                selectBoxHeight: 200,
                valueField: 'id',
                textField: 'text',
                treeLeafOnly: false,
                //readonly: true,
                tree: {
                    url: '../../Data_crm/hr_department.ashx?Action=tree&auth=1&rnd=' + Math.random(),
                    idFieldName: 'id',
                    parentIDFieldName: 'pid',
                    checkbox: false
                },
                onSelected: function (newvalue, newtext, newid) {
                    $.get("../../Data_crm/hr_employee.ashx?Action=combo&auth=1&did=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        a.setData(eval(data));
                        a.selectValue(newid);
                    });
                }
            });
            */
            e = $('#F_employee').ligerComboBox({ width: 82 });
            f = $('#F_department').ligerComboBox({
                width: 96,
                selectBoxWidth: 240,
                selectBoxHeight: 200,
                valueField: 'id',
                textField: 'text',
                treeLeafOnly: false,
                //readonly: true,
                tree: {
                    url: '../../Data_crm/hr_department.ashx?Action=tree&auth=1&rnd=' + Math.random(),
                    idFieldName: 'id',
                    parentIDFieldName: 'pid',
                    checkbox: false
                },
                onSelected: function (newvalue, newtext, newid) {
                    $.get("../../Data_crm/hr_employee.ashx?Action=combo&auth=1&did=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        e.setData(eval(data));
                        e.selectValue(newid);
                    });
                }
            });

        }

        function loadorder(orderid) {
            $.ajax({
                type: "GET",
                url: "../../Data_crm/Crm_order.ashx", /* 注意后面的名字对应CS的方法名称 */
                data: { Action: 'form', orderid: orderid, rnd: Math.random() }, /* 注意参数的格式和名称 */
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var obj = eval(result);
                    for (var n in obj) {

                    }
                    //alert(obj.constructor); //String 构造函数
                    $("#T_Customer").val(obj.Customer_name);
                    //$("#T_Customer_val").val(obj.Customer_id);
                    $("#T_Customer_id").val(obj.Customer_id);

                    $("#T_department0").val(obj.C_dep_name);
                    $("#T_employee0").val(obj.C_emp_name);

                    $("#T_order_amount").val(toMoney(obj.Order_amount));
                    $("#T_receive_amount").val(toMoney(obj.receive_money));
                    $("#T_arrears_amount").val(toMoney(obj.arrears_money));
                    $("#T_invoice_money").val(toMoney(obj.invoice_money));
                    $("#T_arrears_invoice").val(toMoney(obj.arrears_invoice));
                }
            });
        }
        function loadForm(oaid) {
            $.ajax({
                type: "GET",
                url: "../../Data_crm/CRM_receive.ashx", /* 注意后面的名字对应CS的方法名称 */
                data: { Action: 'form', receiveid: oaid, rnd: Math.random() }, /* 注意参数的格式和名称 */
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var obj = eval(result);
                    for (var n in obj) {

                    }
                    //alert(obj.constructor); //String 构造函数                    
                    $("#T_invoice_amount").val(toMoney(obj.receive_real));
                    $("#T_invoice_num").val(obj.Receive_num);

                    $("#T_invoice_date").val(formatTimebytype(obj.Receive_date, "yyyy-MM-dd"));
                    $("#T_content").val(obj.remarks);


                    //$("#T_invoice_type").ligerGetComboBoxManager().selectValue(obj.Pay_type_id);
                    //$("#T_department").ligerGetComboBoxManager().selectValue(obj.C_depid, obj.C_empid);
                    //$("#T_receive_direction").ligerGetComboBoxManager().selectValue(obj.receive_direction_id);
                    $("#T_department").val(obj.C_depname);
                    $("#T_employee").val(obj.C_empname);
                    $("#T_invoice_type").val(obj.Pay_type);
                    $("#T_receive_direction").val(obj.receive_direction_name);

                }
            });
        }

        function set_tomoney(value) {
            $("#T_invoice_amount").val(toMoney(value));
        }

        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=savefollow&receiveid=" + getparastr("receiveid") + "&customerid=" + $("#T_Customer_id").val();
                return $("#f_follow :input").fieldSerialize() + sendtxt;
            }
        }
    </script>

</head>
<body style="margin: 0">
    <form id="form1">
        <div>
            <table style="width: 620px; margin: 5px;" class='bodytable1'>
                <tr>
                    <td class="table_title1">订单信息</td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 550px">
                            <tr>
                                <td width="62px">
                                    <div align="right" style="width: 61px">
                                        客户：
                                    </div>
                                </td>
                                <td colspan="3">
                                    <input type="text" id="T_Customer" name="T_Customer" ltype="text" ligerui="{width: 450,disabled:true}" style="width: 452px;" />
                                    <input type="hidden" id="T_Customer_id" />
                                </td>
                            </tr>
                            <tr>
                                <td width="62px">
                                    <div align="right" style="width: 61px">
                                        应收金额：
                                    </div>
                                </td>
                                <td>
                                    <input type="text" id="T_order_amount" name="T_order_amount"  ltype="text" ligerui="{width:182,disabled:true}" style="width: 452px;text-align:right" />
                                </td>
                                <td width="62px">
                                    <div align="right" style="width: 61px">
                                        已开票额：
                                    </div>
                                </td>
                                <td>
                                    <input type="text" id="T_invoice_money" name="T_invoice_money" style="text-align:right" ltype="text" ligerui="{width:182,disabled: true}" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <div align="right" style="width: 61px">
                                        已收金额：
                                    </div>
                                </td>
                                <td>
                                    <input type="text" id="T_receive_amount" name="T_receive_amount" style="text-align:right" ltype="text" ligerui="{width:182,disabled:true}" />
                                </td>
                                <td>
                                    <div align="right" style="width: 61px">
                                        未开票额：
                                    </div>
                                </td>
                                <td>
                                    <input type="text" id="T_arrears_invoice" name="T_receive_invoice" style="text-align:right" ltype="text" ligerui="{width:182,disabled:true}" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <div align="right" style="width: 61px">
                                        未收金额：
                                    </div>
                                </td>
                                <td>

                                    <input type="text" id="T_arrears_amount" name="T_arrears_amount" style="text-align:right" ltype="text" ligerui="{width:182,disabled:true}" />

                                </td>
                                <td>
                                    <div align="right" style="width: 61px">
                                        订单归属：
                                    </div>
                                </td>
                                <td>
                                    <div style="width: 100px; float: left">
                                        <input id="T_department0" name="T_department0" type="text" ltype="text" ligerui="{width:96,disabled:true}" />
                                    </div>
                                    <div style="width: 98px; float: left">
                                        <input id="T_employee0" name="T_employee0" type="text" ltype="text" ligerui="{width:82,disabled:true}" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="table_title1">收款信息</td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 550px">
                            <tr>
                                <td width="62px">
                                    <div align="right" style="width: 61px">
                                        收款金额：
                                    </div>
                                </td>
                                <td>
                                    <input type="text" id="T_invoice_amount" name="T_invoice_amount" style="text-align:right" ltype="text" ligerui="{width:182,disabled:true,number:true}" />
                                </td>

                                <td>
                                    <div align="left" style="width: 60px">收款类别：</div>
                                </td>
                                <td>
                                    <div style="width: 182px; float: left">
                                        <input type="text" id="T_receive_direction" name="T_receive_direction" ltype="text" ligerui="{width:182,disabled:true}" />
                                        </div>
                                    <%--<div style="width:108px;float:left">
                                          <input type="text" id="T_receive_type" name="T_receive_type" validate="{required:true}" />
                                    </div>--%>
                                        </td>
                                </tr>
                            <tr>
                                <td width="62px">
                                    <div align="right" style="width: 61px">
                                        付款方式：
                                    </div>
                                </td>
                                <td>
                                    <input type="text" id="T_invoice_type" name="T_invoice_type" ltype="text" ligerui="{width:182,disabled:true}" />

                                </td>

                                <td>
                                    <div align="right" style="width: 61px">
                                        凭证号码：
                                    </div>
                                </td>
                                <td>
                                    <input type="text" id="T_invoice_num" name="T_invoice_num" ltype="text" ligerui="{width:182,disabled:true}" />
                                </td>
                            </tr>
                            <tr>
                                
                                <td>
                                    <div align="right" style="width: 61px">
                                        收款时间：
                                    </div>
                                </td>
                                <td>
                                    <input type="text" id="T_invoice_date" name="T_invoice_date" ltype="date" ligerui="{width:182,disabled:true}" />

                                </td>


                                <td>
                                    <div align="right" style="width: 61px">
                                        收款人：
                                    </div>
                                </td>
                                <td>
                                    <div style="width: 100px; float: left">
                                        <input id="T_department" name="T_department" type="text" ltype="text" ligerui="{width:96,disabled:true}" />
                                    </div>
                                    <div style="width: 98px; float: left">
                                        <input id="T_employee" name="T_employee" type="text" ltype="text" ligerui="{width:82,disabled:true}" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div align="right" style="width: 61px">
                                        收款内容：
                                    </div>
                                </td>
                                <td colspan="3">

                                    <textarea cols="100" id="T_content" name="T_content" rows="4" class="l-textarea" style="width: 453px" disabled ></textarea></td>
                            </tr>
                            <tr>
                                <td width="62px">&nbsp;</td>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="f_follow" style="width: 620px; margin: 5px;" class='bodytable1'>
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
        </div>
    </form>

</body>
</html>
