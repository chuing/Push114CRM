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
            $("#fileicon").bind("click", f_selectExcel);
            $("#T_filename").bind("click", f_selectExcel);
            initselect();

            $("#T_submit").click(function () {
                var $valid = $(form1).valid();
                var $type = $("#T_type_val").val();
                var $filename = $("#T_filename").val();
                if (($valid && $type == "xls" && $filename.indexOf(".xls") != -1) || ($valid && $type == "txt" && $filename.indexOf(".txt") != -1)) {
                    f_save();
                }

            });

        })
        function f_save() {
            if ($(form1).valid()) {
                var sendtxt = "&Action=save";
                //return $("form :input").fieldSerialize() + sendtxt;

                var issave = $("form :input").fieldSerialize() + sendtxt;
                //$("#info").html(issave);
                $("#I_info").html("请稍后...");
                if (issave) {
                    top.$.ligerDialog.waitting('数据保存中,请稍候...');
                    $.ajax({
                        url: "../../Data_crm/sys_import.ashx", type: "POST",
                        data: issave,
                        success: function (responseText) {
                            top.$.ligerDialog.closeWaitting();
                            //f_reload();
                            $("#I_info").html(responseText);
                        },
                        error: function () {
                            top.$.ligerDialog.closeWaitting();
                            top.$.ligerDialog.error('操作失败！');
                        }
                    });

                }
            }
        }
        var a; var b;
        function initselect() {
            a = $('#T_employee').ligerComboBox({ width: 96 });
            b = $('#T_department').ligerComboBox({
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
            $("#T_type").ligerGetComboBoxManager().selectValue('xls');
        }
        function f_selectExcel() {
            var jfilelist;
            jfilelist = $("body > .iconlist:first");
            if (!jfilelist.length) jfilelist = $('<ul class="iconlist"></ul>').appendTo('body');
            winfile = $.ligerDialog.open({
                title: '选取文件',
                target: jfilelist,
                width: 400, height: 280, modal: true
            });
            var _t = $("#T_type_val").val(); //"xls";
            var _ft = "f_" + _t;
            //if (!jfilelist.attr("loaded")) {
                $.ajax({
                    url: "../../Data_crm/Base.ashx", type: "get",
                    data: { Action: "GetFile", filetype: _ft, rnd: Math.random() },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var obj = eval(data);
                        //alert(obj.length);
                        for (var i = 0, l = obj.length; i < l; i++) {
                            var src = obj[i];
                            if (src.Extension.indexOf(_t) != -1) {
                                var reg = /(img_crm\\impfile)(.+)/;
                                var match = reg.exec(src);
                                jfilelist.append("<li><img src='../../img_crm/ficon/" + _t + ".png' /><span>" + src.Name + "</span></li>");
                                if (!match) continue;
                            }
                        }
                        //jfilelist.attr("loaded", true);
                    }
                });
            //}
        }
        $(".iconlist li").live('mouseover', function () {
            $(this).addClass("over");
        }).live('mouseout', function () {
            $(this).removeClass("over");
        }).live('click', function () {
            if (!winfile) return;
            $("#fileicon").attr("src", $("img", this).attr("src"));
            $("#T_filename").val($("span", this).html());

            winfile.close();
        });

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
                <td colspan="7" style="height:32px;" class="table_title1">客户数据导入</td>
            </tr>
            <tr>
                <td width="68"></td>
                <td width="130">
                    文件类型：<br />
                    <input id="T_type" name="T_type" type="text" ltype="select" ligerui="{width:82,data:[{id:'xls',text:'Excel文件'},{id:'txt',text:'Txt文件'},{id:'doc',text:'Word文档'}]}" validate="{required:true}" />
                </td>
                <td width="130" height="68">
                    文件名：<img style="width:16px;height:16px" id="fileicon" /><br />
                    <input id="T_filename" name="T_filename" type="text" ltype="text" ligerui="{width:98}" validate="{required:true}" />
                </td>
                <td width="130">开始：<br />
                    <input id="T_start" name="T_start" type="text" ltype="text" ligerui="{width:82}" validate="{required:true}" />
                </td>
                <td width="130">结束：<br />
                    <input id="T_end" name="T_end" type="text" ltype="text" ligerui="{width:82}" validate="{required:true}" />
                </td>
                <td width="240">
                    归属ID：<br />
                    <div style="width: 100px; float: left">
                        <input id="T_department" name="T_department" type="text" validate="{required:true}" style="width: 97px" />
                    </div>
                    <div style="width: 98px; float: left">
                        <input id="T_employee" name="T_employee" type="text" validate="{required:true}" style="width: 96px" />
                        <input id="T_emp" name="T_emp" type="hidden" />
                    </div>
                </td>
                <td align="left"><br />
                    <input id="T_submit" name="T_submit" type="button" value=" 导 入 " style="width:88px; height:24px;" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="6"></td>
            </tr>
        </table>
        <div id="I_info" style="margin: 30px; min-width: 700px;"></div>
    </form>
</body>
</html>
