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


        })
        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=save&id=" + getparastr("cid");
                return $("form :input").fieldSerialize() + sendtxt;
            }
        }
        var a; var b; var c; var d; var e; var f; var g; var h; var i;
        function initselect() {
            i = $("#T_sitename").ligerComboBox({ width: 196, url: "../../Data_crm/Param_Site.ashx?Action=combo3&rnd=" + Math.random() });

        }

        function loadForm(oaid) {
            $("#tr_contact0,#tr_contact1,#tr_contact2,#tr_contact3,#tr_contact4,#tr_contact5").remove();
            $.ajax({
                type: "GET",
                url: "../../Data_crm/ext_task.ashx", /* 注意后面的名字对应CS的方法名称 */
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
                    //$("#T_sitename").val(obj.SiteName);
                    $("#T_url").val(obj.Url);
                    $("#T_descript").val(obj.DesCripe);

                    $("#T_sitename").val(obj.SiteName);
                }
            });
        }

        function remote() {
            var url = "../../Data_crm/Ext_Task.ashx?Action=validate&T_cid=" + getparastr("cid") + "&rnd=" + Math.random();
            return url;
        }

        function chkmobil() {
            var url = "../../data_crm/Ext_Task.ashx?Action=mobile&T_cid=" + getparastr("cid") + "&rnd=" + Math.random();
            return url;
        }

    </script>
</head>
<body>
    <form id="form1">
        <table style="width: 600px; margin: 5px;" class='bodytable1'>
            <tr>
                <td colspan="2" class="table_title1">贴子信息</td>
            </tr>
            <tr>
                <td height="48">
                    <div style="width:80px; text-align:right; float:right">发贴网站：</div>
                </td>
                <td>
                    <input type="text" id="T_sitename" name="T_sitename" />
                </td>
            </tr>
            <tr>
                <td height="48">
                    <div style="width:80px; text-align:right; float:right">贴子网址：</div>
                </td>
                <td>
                    <input id="T_url" name="T_url" type="text" ltype="text" ligerui="{width:495}" validate="{required:true,url:true}" />
                </td>
            </tr>
            <tr>
                <td height="48">
                    <div style="width:80px; text-align:right; float:right">备注：</div>
                </td>
                <td>
                    <input id="T_descript" name="T_descript" type="text" ltype="text" ligerui="{width:495}" />
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
