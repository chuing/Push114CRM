<%@ Page Language="C#" AutoEventWireup="true" %>

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
            var winfile;
            $.metadata.setType("attr", "validate");
            XHD.validate($(form1));

            $("form").ligerForm();
            initselect();

            $("#C_submit").click(function () {
                f_save();
            });

        })
        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=move";

                var issave = $("#form1 :input").fieldSerialize() + sendtxt;
                //$("#I_info").html(issave);
                $("#I_info").html("请稍后...");
                if (issave) {
                    top.$.ligerDialog.waitting('数据保存中,请稍候...');
                    $.ajax({
                        url: "../../Data_crm/CRM_Customer.ashx", type: "POST",
                        data: issave,
                        success: function (responseText) {
                            top.$.ligerDialog.closeWaitting();
                            //f_reload();
                            $("#I_info").html("转移了 " + responseText + " 条数据，完成！");
                        },
                        error: function () {
                            top.$.ligerDialog.closeWaitting();
                            top.$.ligerDialog.error('操作失败！');
                        }
                    });
                }
            }
        }
        var a; var b; var c; var d; var e; var f;
        function initselect() {
            a = $('#F_emp').ligerComboBox({ width: 96 });
            b = $('#F_dep').ligerComboBox({
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
                        a.setData(eval(data));
                        a.selectValue(newid);
                    });
                }
            });
            c = $('#T_emp').ligerComboBox({ width: 96 });
            d = $('#T_dep').ligerComboBox({
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
                        c.setData(eval(data));
                        c.selectValue(newid);
                    });
                }
            });
            e = $('#B_emp').ligerComboBox({ width: 96 });
            f = $('#B_dep').ligerComboBox({
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
                        e.setData(eval(data));
                        e.selectValue(newid);
                    });
                }
            });
        }

    </script>
    <style type="text/css">
        #fileicon {cursor:pointer;}
        .iconlist{ width:360px;padding:3px;}
        .iconlist li{ border:1px solid #FFFFFF; float:left; display:block; padding:2px; cursor:pointer; width:170px;}
        .iconlist li.over{border:1px solid #516B9F;}
        .iconlist li img{ height:16px; height:16px; padding:5px;}

    </style>
</head>
<body>
    <form id="form1">
        <table class="bodytable1">
            <tr>
                <td colspan="7" style="height:32px;" class="table_title1">客户数据转移</td>
            </tr>
            <tr>
                <td width="68"></td>
                <td width="240">
                    源ID：<br />
                    <div style="width: 100px; float: left">
                        <input id="F_dep" name="F_dep" type="text" validate="{required:true}" style="width: 97px" />
                    </div>
                    <div style="width: 98px; float: left">
                        <input id="F_emp" name="F_emp" type="text" validate="{required:true}" style="width: 96px" />
                    </div>
                </td>
                <td width="240">
                    目标ID：<br />
                    <div style="width: 100px; float: left">
                        <input id="T_dep" name="T_dep" type="text" validate="{required:true}" style="width: 97px" />
                    </div>
                    <div style="width: 98px; float: left">
                        <input id="T_emp" name="T_emp" type="text" validate="{required:true}" style="width: 96px" />
                    </div>
                </td>
                <td width="240">
                    原ID：<br />
                    <div style="width: 100px; float: left">
                        <input id="B_dep" name="B_dep" type="text" style="width: 97px" />
                    </div>
                    <div style="width: 98px; float: left">
                        <input id="B_emp" name="B_emp" type="text" style="width: 96px" />
                    </div>
                </td>
                <td align="left"><br />
                    <input id="C_submit" name="C_submit" type="button" value=" 转 移 " style="width:88px; height:24px;" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="6"></td>
            </tr>
        </table>
        <div id="I_info" style="margin: 30px; min-width: 700px;"></div>
    </form>
    <form id="form2">
        <table class="bodytable1">
            <tr>
                <td colspan="7" style="height:32px;" class="table_title1">合同数据转移</td>
            </tr>
            <tr>
                <td width="68"></td>
                <td width="130">
                    文件类型：<br />
                    <input id="Text1" name="T_type" type="text" ltype="select" ligerui="{width:82,data:[{id:'xls',text:'Excel文件'},{id:'txt',text:'Txt文件'},{id:'doc',text:'Word文档'}]}" validate="{required:true}" />
                </td>
                <td width="130" height="68">
                    文件名：<img style="width:16px;height:16px" id="Img1" /><br />
                    <input id="Text2" name="T_filename" type="text" ltype="text" ligerui="{width:98}" validate="{required:true}" />
                </td>
                <td width="130">开始：<br />
                    <input id="Text3" name="T_start" type="text" ltype="text" ligerui="{width:82}" validate="{required:true}" />
                </td>
                <td width="130">结束：<br />
                    <input id="Text4" name="T_end" type="text" ltype="text" ligerui="{width:82}" validate="{required:true}" />
                </td>
                <td width="240">
                    归属ID：<br />
                    <div style="width: 100px; float: left">
                        <input id="Text5" name="T_department" type="text" validate="{required:true}" style="width: 97px" />
                    </div>
                    <div style="width: 98px; float: left">
                        <input id="Text6" name="T_employee" type="text" validate="{required:true}" style="width: 96px" />
                        <input id="Hidden1" name="T_emp" type="hidden" />
                    </div>
                </td>
                <td align="left"><br />
                    <input id="Button1" name="T_submit" type="button" value=" 导 入 " style="width:88px; height:24px;" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="6"></td>
            </tr>
        </table>
        <div id="Div1" style="margin: 30px; min-width: 700px;"></div>
    </form>
</body>
</html>
