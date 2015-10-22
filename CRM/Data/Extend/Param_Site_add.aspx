<%@ Page Language="C#" AutoEventWireup="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
    <title></title>
    <link href="../lib/ligerUI/skins/ext/css/ligerui-all.css" rel="stylesheet" type="text/css" />    
    <link href="../Css_crm/input.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>    
    
    <script src="../lib/jquery-validation/jquery.validate.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/common.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../Js_crm/CRM.js" type="text/javascript"></script>
    <script src="../lib/jquery.form.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $.metadata.setType("attr", "validate");
            XHD.validate($(form1));

            //$("#T_Contract_name").focus();
            $("form").ligerForm();

            if (getparastr("pid")) {
                loadForm(getparastr("pid"));
            }


        });

        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=save&id=" + getparastr("pid");
                return $("form :input").fieldSerialize() + sendtxt;
            }
        }

        function loadForm(oaid) {
            $.ajax({
                type: "GET",
                url: "../Data_crm/Param_Site.ashx", /* 注意后面的名字对应CS的方法名称 */
                data: { Action: 'form', id: oaid, rnd: Math.random() }, /* 注意参数的格式和名称 */
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var obj = eval(result);
                    for (var n in obj) {

                    }
                    //alert(obj.constructor); //String 构造函数
                    $("#T_Site").val(obj.Site);
                    $("#T_Parent").ligerGetComboBoxManager().selectValue(obj.parentid);
                }
            });
        }

    </script>
</head>
<body style="margin:5px 5px 5px 5px">
    <form id="form1" >

        <table align="left" border="0" cellpadding="3" cellspacing="1" style="background: #fff;">
            <%--
            <tr>
                <td  width="65px"><div align="left" style="width: 61px">
                    上级机构：</div></td>
                <td>
                    <div style="float:left; height: 20px;">
                        <input type="text" id="T_Parent" name="T_Parent" ltype="select" ligerui="{width:180,url:'../Data_crm/Param_Site.ashx?Action=combo3'}" validate="{required:true}" />
                    </div>                    
                </td>
            </tr>
            --%>
            <tr>
                <td height="32">
                    <div align="left" style="width: 62px">网站名：</div></td>
                <td>
                    <div style="float:left; height: 20px;">
                        <input type="hidden" id="T_Parent" name="T_Parent" value="无" />
                        <input type="text" id="T_Site" name="T_Site" ltype='text' ligerui="{width:180}" validate="{required:true}" />
                    </div>
                </td>
            </tr>
            <tr>
                <td height="32">
                    <div align="left" style="width: 62px">网站网址：</div></td>
                <td>
                    <div style="float:left; height: 20px;">
                        <input type="text" id="T_SiteUrl" name="T_SiteUrl" ltype='text' ligerui="{width:180}" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="line-height:21px;">
                    <div style="float:left;">用户名1：<br />
                        <input type="text" id="T_User1" name="T_User1" ltype='text' ligerui="{width:90}" />
                    </div>
                    <div style="float:left; margin-left:10px;">用户名2：<br />
                        <input type="text" id="T_User2" name="T_User2" ltype='text' ligerui="{width:90}" />
                    </div>
                    <div style="float:left; margin-left:10px;">用户名3：<br />
                        <input type="text" id="T_User3" name="T_User3" ltype='text' ligerui="{width:90}" />
                    </div>
                    <div style="float:left; margin-left:10px;">用户名4：<br />
                        <input type="text" id="T_User4" name="T_User4" ltype='text' ligerui="{width:90}" />
                    </div>
                    <div style="float:left; margin-left:10px;">用户名5：<br />
                        <input type="text" id="T_User5" name="T_User5" ltype='text' ligerui="{width:90}" />
                    </div>
                </td>
            </tr>
            </table>

    </form>
</body>
</html>
