﻿<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title> 
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <link href="../../lib/ligerUI/skins/ext/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Css_crm/input.css" rel="stylesheet" />

    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>

    <script src="../../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>

    <script src="../../lib/jquery.form.js" type="text/javascript"></script>      
    <script src="../../Js_crm/CRM.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var str1 = getparastr("rid");
            $("#maingrid4").ligerGrid({
                columns: [
                    { display: '序号', width: 50, render: function (rowData, rowindex, value, column, rowid, page, pagesize) { return (page - 1) * pagesize + rowindex + 1; } },
                    { display: '客户名称', name: 'Customer_name', width: 160, align: 'left' },

                    {
                        display: '客户所属', name: '', width: 120, render: function (item) {
                            return item.C_dep_name + "." + item.C_emp_name;
                        }
                    },
                    { display: '电话', name: 'tel', width: 150 },
                    {
                        display: '审核状态', name: 'Pass_status_id', width: 90, render: function (item) {
                            var html = "<div class='passstatus'>"
                            if (item.Pass_status_id == "0")
                                html += "未通过";
                            else if (item.Pass_status_id == "1")
                                html += "已通过";
                            else html += "审核中";
                            html += "</div>";
                            //html = "";
                            return html;
                        }
                    }

                ],
                onAfterShowData: function (grid) {
                    $("tr[rowtype='已成交']").addClass("l-treeleve2").removeClass("l-grid-row-alt");
                },
                checkbox: false,
                dataAction: 'server',
                pageSize: 30,
                pageSizeOptions: [20, 30, 50, 100],
                url: "../../Data_crm/crm_order.ashx?Action=gridorder&pass=1&rnd=" + Math.random(),
                width: '100%',
                height: '100%',
                //title: "员工列表",
                heightDiff: 0
            });
            toolbar();

            $(document).keydown(function (e) {
                if (e.keyCode == 13) {
                    doserch();
                }
            });
        });
        function toolbar() {

            var items = [];

            items.push({ type: 'textbox', id: 'company', text: '客户名称：' });
            items.push({ type: 'textbox', id: 'tel', text: '电话：' });
            items.push({ type: 'button', text: '搜索', icon: '../../img_crm/search.gif', disable: true, click: function () { doserch() } });

            $("#serchbar1").ligerToolBar({
                items: items

            });

            $("#company").ligerTextBox({ width: 120 })
            $("#tel").ligerTextBox({ width: 120 })
            $("#maingrid4").ligerGetGridManager().onResize();
        }
        function doserch() {
            var sendtxt = "&Action=gridorder&pass=1&rnd=" + Math.random();
            var serchtxt = $("#form1 :input").fieldSerialize() + sendtxt;
            alert(serchtxt);
            $.ligerDialog.waitting('数据查询中,请稍候...');
            var manager = $("#maingrid4").ligerGetGridManager();

            manager.setURL("../../Data_crm/crm_order.ashx?" + serchtxt);
            manager.loadData(true);
            $.ligerDialog.closeWaitting();
        }
        function f_select() {
            var manager = $("#maingrid4").ligerGetGridManager();
            var rows = manager.getSelectedRow();
            //alert(rows);
            return rows;
        }


    </script>
    <style type="text/css">
        .passstatus {color:#f00; font-weight:bold; font-size:14px;}
    </style>

</head>
<body>

    <form id="form1">
        <div>
            <div id="serchbar1"></div>

            <div id="maingrid4" style="margin: -1px;"></div>
        </div>
    </form>
    

</body>
</html>
