﻿<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <link href="../../Css_crm/core.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/ligerUI/skins/ext/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Css_crm/input.css" rel="stylesheet" />

    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../lib/jquery.form.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../Js_crm/CRM.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script type="text/javascript">
        var manager;
        var manager1;
        $(function () {

            initLayout();
            $(window).resize(function () {
                initLayout();
            });

            $("#maingrid4").ligerGrid({
                columns: [
                    { display: '序号', width: 50, render: function (rowData, rowindex, value, column, rowid, page, pagesize) { return (page - 1) * pagesize + rowindex + 1; } },
                    {
                        display: '公司名称', name: 'Customer', width: 200, align: 'left', render: function (item) {
                            var html = "<div class='abc'>"
                            if (item.Customer)
                                html += item.Customer;
                            html += "</div>";
                            return html;
                        }
                    },
                    { display: '电话', name: 'tel', width: 120, align: 'right' },
                    { display: '客户意向', name: 'CustomerType', width: 80 },
                    { display: '客户类别', name: 'CustomerLevel', width: 80, hide: 'hide' },
                    { display: '客户来源', name: 'CustomerSource', width: 80 },
                    {
                        display: '省份', name: 'CustomerFrom', width: 80, render: function (item) {
                            return item.Provinces + "." + item.City;
                        }
                    },
                    { display: '所属行业', name: 'industry', width: 80, hide: 'hide' },
					{
					    display: '客户描述', name: 'DesCripe', width: 280, render: function (item) {
					        var html = "<span class='showtip'>" + item.DesCripe + "</span>";
					        return html;
					    }
					},
                    {
                        display: '客户所属', name: '', width: 120, render: function (item) {
                            return item.Department + "." + item.Employee;
                        }
                    },
                    { display: '客源状态', name: 'privatecustomer', width: 60 },
                    {
                        display: '最后跟进', name: 'lastfollow', width: 90, render: function (item) {
                            var lastfollow = formatTimebytype(item.lastfollow, 'yyyy-MM-dd');
                            if (lastfollow == "1900-01-01")
                                lastfollow = "";
                            return lastfollow;
                        }
                    },
                    {
                        display: '创建时间', name: 'Create_date', width: 180, render: function (item) {
                            var createdate = formatTimebytype(item.Create_date, 'yyyy-MM-dd hh:mm:ss');
                            if (createdate == "1900-01-01 00:00:00")
                                createdate = "";
                            return createdate;
                        }
                    },
                    {
                        display: '信息状态', name: 'isDelete', width: 60, render: function (item) {
                            var html = "<div class='isRead'>"
                            if (item.isRead == "0")
                                html += "未读";
                            html += "</div>";
                            return html;
                        }
                    },
                    {
                        display: '创建者', name: '', width: 120, render: function (item) {
                            return item.Create_name + "." + item.Create_id + "-" + item.isImport;
                        }
                    },
                    { display: 'T', name: 'isTop', width: 10, hide: 'hide' },
                    { display: 'I', name: 'isImport', width: 10, hide: 'hide' }

                ], onBeforeShowData: function (grid, data) {
                    startTime = new Date();
                },

                //fixedCellHeight:false,
                onSelectRow: function (data, rowindex, rowobj) {
                    var manager = $("#maingrid5").ligerGetGridManager();
                    manager.showData({ Rows: [], Total: 0 });
                    var url = "../../Data_crm/CRM_Follow.ashx?Action=grid&customer_id=" + data.id;
                    manager.GetDataByURL(url);

                    if (!data.isRead)
                        $.getJSON("/data_crm/crm_customer.ashx?Action=setread&customer_id=" + data.id + "&rnd=" + Math.random(), function (data, textStatus) { });
                },

                rowtop: "isTop",
                rowtype: "CustomerType",
                dataAction: 'server', pageSize: 30, pageSizeOptions: [20, 30, 50, 100],
                url: "../../Data_crm/crm_customer.ashx?Action=grid&import=1&rnd=" + Math.random(),
                width: '100%', height: '65%',
                heightDiff: -1,
                checkbox: false,
                onRClickToSelect: true,
                onContextmenu: function (parm, e) {
                    actionCustomerID = parm.data.id;
                    menu.show({ top: e.pageY, left: e.pageX });
                    return false;
                },
                onAfterShowData: function (grid) {
                    $("tr[rowtop='1']").addClass("l-treeleve4").removeClass("l-grid-row-alt");
                    $("tr[rowtype='已成交']").addClass("l-treeleve1").removeClass("l-treeleve4").removeClass("l-grid-row-alt");
                    var nowTime = new Date();
                    //alert('加载数据耗时：' + (nowTime - startTime));

                    $(".l-grid-row .showtip").hover(function () {
                        $(this).ligerTip({ content: $(this).html() });
                    }, function () {
                        $(this).ligerHideTip();
                    });
                },
                onDblClickRow: function (data, rowindex, rowobj) {
                    view();
                },
                detail: {
                    onShowDetail: function (r, p) {
                        for (var n in r) {
                            if (r[n] == null) r[n] = "";
                        }
                        var grid = document.createElement('div');
                        $(p).append(grid);
                        $(grid).css('margin', 3).ligerGrid({
                            columns: [
                            { display: '行号', width: 50, render: function (item, i) { return i + 1; } },
                            { display: '联系人', name: 'C_name', width: 100 },
                            { display: '职务', name: 'C_position', width: 100 },
                            { display: '性别', name: 'C_sex', width: 50 },
                            //{ display: '所属公司', name: 'C_companyname', width: 180 },
                            { display: '手机', name: 'C_mob', width: 120 },
                            { display: 'QQ', name: 'C_QQ', width: 100 },
                            { display: 'Email', name: 'C_email', width: 180 }
                            ],

                            usePager: false,
                            checkbox: true,
                            url: "../../Data_crm/CRM_Contact.ashx?Action=grid&customerid=" + r.id,
                            width: '1022px', height: '100px',
                            heightDiff: 0
                        })

                    }
                }
            });
            $("#maingrid5").ligerGrid({
                columns: [
                        { display: '序号', width: 40, render: function (item, i) { return i + 1; } },
                        {
                            display: '跟进内容', name: 'Follow', align: 'left', width: 590, render: function (item) {
                                var html = "<div class='abc'>"
                                if (item.Follow)
                                    html += item.Follow;
                                html += "</div>";
                                return html;
                            }
                        },
                        {
                            display: '跟进时间', name: 'Follow_date', width: 140, render: function (item) {
                                return formatTimebytype(item.Follow_date, 'yyyy-MM-dd hh:mm');
                            }
                        },
                        { display: '跟进方式', name: 'Follow_Type', width: 60 },
                        {
                            display: '跟进人', name: '', width: 100, render: function (item) {
                                return item.department_name + "." + item.employee_name;
                            }
                        }
                ],
                onAfterShowData: function (grid) {

                },

                dataAction: 'local', pageSize: 30, pageSizeOptions: [20, 30, 50, 100],
                //checkbox:true,
                url: "../../Data_crm/CRM_Follow.ashx?Action=grid&customer_id=0",
                width: '100%', height: '100%',
                //title: "跟进信息",
                heightDiff: -1,
                onRClickToSelect: true,
                onContextmenu: function (parm, e) {
                    actionCustomerID = parm.data.id;
                    menu1.show({ top: e.pageY, left: e.pageX });
                    return false;
                }
            });

            $("#grid").height(document.documentElement.clientHeight - $(".toolbar").height());

            $('form').ligerForm();
            initSerchForm();
            toolbar();
            //toolbar1();

        });
        var view_auth = false;
        function toolbar() {
            $.getJSON("../../Data_crm/toolbar.ashx?Action=GetSys&mid=93&rnd=" + Math.random(), function (data, textStatus) {
                //alert(data);
                var items = [];
                var arr = data.Items;
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].text == '查看' && arr[i].disable) view_auth = true;
                    arr[i].icon = "../../" + arr[i].icon;
                    items.push(arr[i]);
                }
                /*
                items.push({
                    type: 'button',
                    text: '标记重点',
                    icon: '../../img_crm/icon/3.png',
                    disable: true,
                    click: function () {
                        uptop();
                    }
                });
                */
                items.push({
                    type: 'serchbtn',
                    text: '高级搜索',
                    icon: '../../img_crm/search.gif',
                    disable: true,
                    click: function () {
                        serchpanel();
                    }
                });
                $("#toolbar").ligerToolBar({
                    items: items

                });
                menu = $.ligerMenu({
                    width: 120, items: getMenuItems(data)
                });

            });
            $.getJSON("../../Data_crm/toolbar.ashx?Action=GetSys&mid=6&rnd=" + Math.random(), function (data, textStatus) {
                //alert(data);
                var items = [];
                var arr = data.Items;
                for (var i = 0; i < arr.length; i++) {
                    arr[i].icon = "../../" + arr[i].icon;
                    items.push(arr[i]);
                }
                $("#toolbar1").ligerToolBar({
                    items: items

                });
                menu1 = $.ligerMenu({
                    width: 120, items: getMenuItems(data)
                });

                $("#maingrid4").ligerGetGridManager().onResize();
                $("#maingrid5").ligerGetGridManager().onResize();
            });
        }
        function initSerchForm() {
            var a = $('#T_City').ligerComboBox({ width: 96 });
            var b = $('#T_Provinces').ligerComboBox({
                width: 97,
                url: "../../Data_crm/Param_City.ashx?Action=combo1&rnd=" + Math.random(),
                onSelected: function (newvalue) {
                    $.get("../../Data_crm/Param_City.ashx?Action=combo2&pid=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        a.setData(eval(data));
                    });
                }
            });
            var c = $('#customertype').ligerComboBox({ width: 97, url: "../../Data_crm/Param_SysParam.ashx?Action=combo&parentid=1&rnd=" + Math.random() });
            var d = $('#customerlevel').ligerComboBox({ width: 96, url: "../../Data_crm/Param_SysParam.ashx?Action=combo&parentid=2&rnd=" + Math.random() });
            var c = $('#isread').ligerComboBox({ width: 97, url: "../../Data_crm/Param_SysParam.ashx?Action=comboread&parentid=1&rnd=" + Math.random() });
            var g = $('#C_employee').ligerComboBox({ width: 96 });
            var e = $('#employee').ligerComboBox({ width: 96 });
            var f = $('#department').ligerComboBox({
                width: 97,
                selectBoxWidth: 240,
                selectBoxHeight: 200,
                valueField: 'id',
                textField: 'text',
                treeLeafOnly: false,
                tree: {
                    url: '../../Data_crm/hr_department.ashx?Action=tree&auth=0&rnd=' + Math.random(),
                    idFieldName: 'id',
                    parentIDFieldName: 'pid',
                    checkbox: false
                },
                onSelected: function (newvalue) {
                    $.get("../../Data_crm/hr_employee.ashx?Action=combo&auth=0&did=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        e.setData(eval(data));
                    });
                }
            });
            var g = $('#C_employee').ligerComboBox({ width: 96 });
            var h = $('#C_department').ligerComboBox({
                width: 97,
                selectBoxWidth: 240,
                selectBoxHeight: 200,
                valueField: 'id',
                textField: 'text',
                treeLeafOnly: false,
                tree: {
                    url: '../../Data_crm/hr_department.ashx?Action=tree&auth=0&rnd=' + Math.random(),
                    idFieldName: 'id',
                    parentIDFieldName: 'pid',
                    checkbox: false
                },
                onSelected: function (newvalue) {
                    $.get("../../Data_crm/hr_employee.ashx?Action=combo&auth=0&did=" + newvalue + "&rnd=" + Math.random(), function (data, textStatus) {
                        g.setData(eval(data));
                    });
                }
            });
        }
        function serchpanel() {
            if ($(".az").css("display") == "none") {
                $("#grid").css("margin-top", $(".az").height() + "px");
                $("#maingrid4").ligerGetGridManager().onResize();
                $("#maingrid5").ligerGetGridManager().onResize();
            }
            else {
                $("#grid").css("margin-top", "0px");
                $("#maingrid4").ligerGetGridManager().onResize();
                $("#maingrid5").ligerGetGridManager().onResize();
            }
        }
        $(document).keydown(function (e) {
            if (e.keyCode == 13) {
                doserch();
            }
        });
        function doserch() {
            var sendtxt = "&Action=grid&import=1&rnd=" + Math.random();
            var serchtxt = $("#serchform :input").fieldSerialize() + sendtxt;
            //alert(serchtxt);
            //$("#infomsg").html(serchtxt);
            var manager = $("#maingrid4").ligerGetGridManager();

            manager.setURL("../../Data_crm/crm_customer.ashx?" + serchtxt);
            manager.loadData(true);
        }
        function doclear() {
            //var serchtxt = $("#serchform :input").reset();
            $("#serchform").each(function () {
                this.reset();
                $(".l-selected").removeClass("l-selected");
            });
        }

        var activeDialog = null;
        function f_openWindow(url, title, width, height) {
            var dialogOptions = {
                width: width, height: height, title: title, url: url, buttons: [
                        {
                            text: '保存', onclick: function (item, dialog) {
                                f_save(item, dialog);
                            }
                        },
                        {
                            text: '关闭', onclick: function (item, dialog) {
                                dialog.close();
                            }
                        }
                ], isResize: true, timeParmName: 'a'
            };
            activeDialog = parent.jQuery.ligerDialog.open(dialogOptions);
        }

        function view() {
            if (view_auth) {
                var manager = $("#maingrid4").ligerGetGridManager();
                var row = manager.getSelectedRow();
                if (row) {
                    parent.jQuery.ligerDialog.open({ width: 770, height: 490, title: "客户详情", url: "CRM/Customer/Customer_info.aspx?cid=" + row.id, buttons: [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); } }] });
                } else {
                    $.ligerDialog.warn('请选择行！');
                }
            }
            else {
                $.ligerDialog.warn('权限不够！');
            }
        }
        function add() {
            f_openWindow("CRM/Customer/Customer_add.aspx", "新增客户", 770, 495);
        }

        function edit() {
            var manager = $("#maingrid4").ligerGetGridManager();
            var row = manager.getSelectedRow();
            if (row) {
                f_openWindow('CRM/Customer/Customer_add.aspx?cid=' + row.id, "修改客户", 770, 490);
            }
            else {
                $.ligerDialog.warn('请选择行！');
            }
        }

        function del() {
            var manager = $("#maingrid4").ligerGetGridManager();
            var row = manager.getSelectedRow();
            if (row) {
                $.ajax({
                    url: "../../Data_crm/CRM_Customer.ashx", type: "POST",
                    data: { Action: "count", id: row.id, rnd: Math.random() },
                    success: function (responseText) {
                        $.ligerDialog.confirm("此客户下含有 " + responseText + ",您确定放入回收站？", function (yes) {
                            if (yes) {
                                $.ajax({
                                    url: "../../Data_crm/CRM_Customer.ashx", type: "POST",
                                    data: { Action: "AdvanceDelete", id: row.id, rnd: Math.random() },
                                    success: function (responseText) {
                                        if (responseText == "true") {
                                            f_reload();
                                            f_followreload();
                                        }
                                        else {
                                            top.$.ligerDialog.error('删除失败！');
                                        }

                                    },
                                    error: function () {
                                        top.$.ligerDialog.error('删除失败！');
                                    }
                                });
                            }
                        })
                    },
                    error: function () {
                        top.$.ligerDialog.error('数据错误！');
                    }
                });
            }
            else {
                $.ligerDialog.warn("请选择客户");
            }
        }

        function uptop() {
            var manager = $("#maingrid4").ligerGetGridManager();
            var row = manager.getSelectedRow();
            if (row) {
                var txt = row.isTop == '1' ? "此客户已标记为重点客户，<br />您确定取消标记吗？" : "标记此客户为重点客户，<br />您确定标记吗？";
                $.ligerDialog.confirm("<div class=msgtop>" + txt + "</div>", function (yes) {
                    if (yes) {
                        $.ajax({
                            url: "../../Data_crm/CRM_Customer.ashx", type: "POST",
                            data: { Action: "UpTop", id: row.id, top: row.isTop, rnd: Math.random() },
                            success: function (responseText) {
                                if (responseText == "true") {
                                    f_reload();
                                } else {
                                    top.$.ligerDialog.error('标记失败！');
                                }
                            },
                            error: function () {
                                top.$.ligerDialog.error('标记失败！');
                            }
                        });
                    }
                });
            }
            else {
                $.ligerDialog.warn("请选择客户");
            }
        }

        function upimp() {
            var manager = $("#maingrid4").ligerGetGridManager();
            var row = manager.getSelectedRow();
            if (row) {
                var txt = "此客户转换正常客户，<br />您确定转换吗？";
                $.ligerDialog.confirm("<div class=msgtop>" + txt + "</div>", function (yes) {
                    if (yes) {
                        $.ajax({
                            url: "../../Data_crm/CRM_Customer.ashx", type: "POST",
                            data: { Action: "upimp", id: row.id, imp: row.isImport, rnd: Math.random() },
                            success: function (responseText) {
                                if (responseText == "true") {
                                    f_reload();
                                } else {
                                    top.$.ligerDialog.error('转换失败！');
                                }
                            },
                            error: function () {
                                top.$.ligerDialog.error('转换失败！');
                            }
                        });
                    }
                });
            }
            else {
                $.ligerDialog.warn("请选择客户");
            }
        }


        function f_save(item, dialog) {
            var issave = dialog.frame.f_save();
            //$("#info").html(issave);
            if (issave) {
                dialog.close();
                top.$.ligerDialog.waitting('数据保存中,请稍候...');
                $.ajax({
                    url: "../../Data_crm/CRM_Customer.ashx", type: "POST",
                    data: issave,
                    success: function (responseText) {
                        top.$.ligerDialog.closeWaitting();
                        f_reload();
                    },
                    error: function () {
                        top.$.ligerDialog.closeWaitting();
                        top.$.ligerDialog.error('操作失败！');
                    }
                });

            }
        }
        function f_reload() {
            var manager = $("#maingrid4").ligerGetGridManager();
            manager.loadData(true);
        };

        //follow
        function follow_openWindow(url, title, width, height) {
            var dialogOptions = {
                width: width, height: height, title: title, url: url, buttons: [
                        {
                            text: '保存', onclick: function (item, dialog) {
                                f_savefollow(item, dialog);
                            }
                        },
                        {
                            text: '关闭', onclick: function (item, dialog) {
                                dialog.close();
                            }
                        }
                ], isResize: true, timeParmName: 'b'
            };
            activeDialog1 = top.jQuery.ligerDialog.open(dialogOptions);
        }
        function addfollow() {
            var manager = $("#maingrid4").ligerGetGridManager();
            var row = manager.getSelectedRow();
            if (row) {
                follow_openWindow("CRM/Customer/Customer_follow_add.aspx?cid=" + row.id, "新增跟进", 530, 400);
            } else {
                $.ligerDialog.warn('请选择客户！');
            }
        }
        function editfollow() {
            var manager = $("#maingrid5").ligerGetGridManager();
            var row = manager.getSelectedRow();
            if (row) {
                follow_openWindow('CRM/Customer/Customer_follow_add.aspx?fid=' + row.id + "&cid=" + row.Customer_id, "修改跟进", 530, 400);
            } else {
                $.ligerDialog.warn('请选择跟进！');
            }
        }
        function delfollow() {
            var manager = $("#maingrid5").ligerGetGridManager();
            var row = manager.getSelectedRow();
            if (row) {
                $.ligerDialog.confirm("确定删除？", function (yes) {
                    if (yes) {
                        $.ajax({
                            url: "../../Data_crm/CRM_Follow.ashx", type: "POST",
                            data: { Action: "AdvanceDelete", id: row.id, rnd: Math.random() },
                            success: function (responseText) {
                                if (responseText == "true") {
                                    f_followreload();
                                    f_reload();
                                }
                                else {
                                    top.$.ligerDialog.error('删除失败！');
                                }

                            },
                            error: function () {
                                top.$.ligerDialog.error('删除失败！');
                            }
                        });
                    }
                })
            }
            else {
                $.ligerDialog.warn("请选择跟进");
            }
        }
        function f_savefollow(item, dialog) {
            var issave = dialog.frame.f_save();
            if (issave) {
                dialog.close();
                $.ligerDialog.waitting('数据保存中,请稍候...');
                $.ajax({
                    url: "../../Data_crm/CRM_Follow.ashx", type: "POST",
                    data: issave,
                    success: function (responseText) {
                        $.ligerDialog.closeWaitting();
                        f_followreload();
                        f_reload();
                    },
                    error: function () {
                        $.ligerDialog.closeWaitting();
                        $.ligerDialog.error('操作失败！');
                    }
                });

            }
        }
        function f_followreload() {
            var manager = $("#maingrid5").ligerGetGridManager();
            manager.loadData(true);
        };
    </script>
    <style type="text/css">
        .l-treeleve1 { background: yellow; }
        .l-treeleve2 { background: yellow; }
        .l-treeleve3 { background: #eee; }
        .l-treeleve4 { background: #ccf6b3;}
        .isRead {color:#f00; font-weight:bold; font-size:14px;}
        .msgtop {line-height:23px; letter-spacing:1px; padding-left:10px; font-size:14px; font-weight:bold; color:#444;}
    </style>
</head>
<body>
    <form id="form1">
        <div id="toolbar"></div>
        <span id="info"></span>
        <div id="grid">
            <div id="maingrid4" style="margin: -1px; min-width: 800px;"></div>
            <div id="toolbar1"></div>
            <div id="Div1" style="position: relative;">
                <div id="maingrid5" style="margin: -1px -1px;"></div>
            </div>
        </div>


    </form>
    <div class="az">
        <form id='serchform'>
            <table style='width: 1120px' class="bodytable1">
                <tr>
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>公司名称：</div>
                    </td>
                    <td>
                        <input type='text' id='company' name='company' ltype='text' ligerui='{width:120}' /></td>

                    <td>
                        <div style='width: 60px; text-align: right; float: right'>客户意向：</div>
                    </td>
                    <td>
                        <div style='width: 100px; float: left'>
                            <input type='text' id='customertype' name='customertype' />
                        </div>
                        <div style='width: 98px; float: left; display:none;'>
                            <input type='text' id='customerlevel' name='customerlevel' />
                        </div>
                    </td>
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>录入时间：</div>
                    </td>
                    <td>
                        <div style='width: 100px; float: left'>
                            <input type='text' id='startdate' name='startdate' ltype='date' ligerui='{width:97}' />
                        </div>
                        <div style='width: 98px; float: left'>
                            <input type='text' id='enddate' name='enddate' ltype='date' ligerui='{width:96}' />
                        </div>
                    </td>
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>创建人：</div>
                    </td>
                    <td>
                        <div style='width: 100px; float: left'>
                            <input type='text' id='C_department' name='C_department' />
                        </div>
                        <div style='width: 98px; float: left'>
                            <input type='text' id='C_employee' name='C_employee' />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>客户描述：</div>
                    </td>
                    <td>
                        <input id='industry' name="industry" type='text' ltype='text' ligerui='{width:120}' /></td>

                    <td>
                        <div style='width: 60px; text-align: right; float: right'>所属地区：</div>
                    </td>
                    <td>
                        <div style='width: 100px; float: left'>
                            <input type='text' id='T_Provinces' name='T_Provinces' />
                        </div>
                        <div style='width: 98px; float: left'>
                            <input type='text' id='T_City' name='T_City' />
                        </div>
                    </td>
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>最后跟进：</div>
                    </td>
                    <td>
                        <div style='width: 100px; float: left'>
                            <input type='text' id='startfollow' name='startfollow' ltype='date' ligerui='{width:97}' />
                        </div>
                        <div style='width: 98px; float: left'>
                            <input type='text' id='endfollow' name='endfollow' ltype='date' ligerui='{width:96}' />
                        </div>
                    </td>
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>查看状态：</div>
                    </td>
                    <td>
                        <div style='width: 100px; float: left'>
                            <input type='text' id='isread' name='isread' />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>电话：</div>
                    </td>
                    <td>
                        <input type='text' id='tel' name='tel' ltype='text' ligerui='{width:120}' />
                    </td>

                    <td>
                        <div style='width: 60px; text-align: right; float: right'>公司网址：</div>
                    </td>
                    <td>
                        <input id='website' name="website" type='text' ltype='text' ligerui='{width:196}' /></td>
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>业务员：</div>
                    </td>
                    <td>
                        <div style='width: 100px; float: left'>
                            <input type='text' id='department' name='department' />
                        </div>
                        <div style='width: 98px; float: left'>
                            <input type='text' id='employee' name='employee' />
                        </div>
                    </td>
                    <td></td>
                    <td>
                        <input id='Button2' type='button' value='重置' style='width: 80px; height: 24px' onclick="doclear()" />
                        <input id='Button1' type='button' value='搜索' style='width: 80px; height: 24px' onclick="doserch()" />
                    </td>
                </tr>
            </table>
        </form>
        <span id="infomsg"></span>
    </div>
</body>
</html>
