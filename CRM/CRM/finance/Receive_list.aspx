<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <link href="../../Css_crm/core.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/ligerUI/skins/ext/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../Css_crm/Toolbar.css" rel="stylesheet" type="text/css" />
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
    <script src="../../lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../Js_crm/CRM.js" type="text/javascript"></script>
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
                    { display: '客户名', name: 'Customer_name',width:140 },
                    { display: '凭证号码', name: 'Receive_num', width: 140 },
                    { display: '收款方式', name: 'Pay_type', width: 100 },
                    {
                        display: '收款金额(￥)', name: 'Receive_amount', width: 120, align: 'right', render: function (item) {
                            return toMoney(item.Receive_amount);
                        }
                    },
                    {
                        display: '收款人', width: 100, render: function (item) {
                            return item.C_depname + "." + item.C_empname;
                        }
                    },
                    {
                        display: '收款日期', name: 'Receive_date', width: 90, render: function (item) {
                            return formatTimebytype(item.Receive_date, 'yyyy-MM-dd');
                        }
                    },
                    { display: '录入人', name: 'create_name', width: 90 }

                ],
                dataAction: 'server', pageSize: 30, pageSizeOptions: [20, 30, 50, 100],
                //checkbox:true,
                url: "../../Data_crm/CRM_receive.ashx?Action=grid&rnd=" + Math.random(),
                width: '100%', height: '100%',
                heightDiff: -1,
                onRClickToSelect: true,
                onContextmenu: function (parm, e) {
                    actionCustomerID = parm.data.id;
                    menu.show({ top: e.pageY, left: e.pageX });
                    return false;
                }
            });

            $("#grid").height(document.documentElement.clientHeight - $(".toolbar").height());
            

            $('form').ligerForm();
            initSerchForm();
            toolbar();
            //toolbar1();

        });
        function toolbar() {
            $("#toolbar").ligerToolBar({
                items: [{
                    type: 'button',
                    text: '查看收款',
                    icon: '../../img_crm/folder-open.gif',
                    disable: true,
                    click: function () {
                        view();
                    }
                },
                {
                    type: 'serchbtn',
                    text: '高级搜索',
                    icon: '../../img_crm/search.gif',
                    disable: true,
                    click: function () {
                        serchpanel()
                    }
                }
                ]
            });
            menu = $.ligerMenu({
                width: 120, items:
                [
                    { text: '查看', click: view, icon: 'view' },
                ]
            });
        }

        function initSerchForm() {            
            var d = $('#pay_type').ligerComboBox({ width: 196, url: "../../Data_crm/Param_SysParam.ashx?Action=combo&parentid=5&rnd=" + Math.random() });
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
        }
        function serchpanel() {
            if ($(".az").css("display") == "none") {
                $("#grid").css("margin-top", $(".az").height() + "px");
                $("#maingrid4").ligerGetGridManager().onResize();
            }
            else {
                $("#grid").css("margin-top", "0px");
                $("#maingrid4").ligerGetGridManager().onResize();
            }
        }
        $(document).keydown(function (e) {
            if (e.keyCode == 13) {
                doserch();
            }
        });
        function doserch() {
            var sendtxt = "&Action=grid&rnd=" + Math.random();
            var serchtxt = $("#serchform :input").fieldSerialize() + sendtxt;
            //alert(serchtxt);
            var manager = $("#maingrid4").ligerGetGridManager();
            manager.setURL("../../Data_crm/CRM_receive.ashx?" + serchtxt);
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
            activeDialog = top.jQuery.ligerDialog.open(dialogOptions);
        }

        function view() {
            var manager = $("#maingrid4").ligerGetGridManager();
            var row = manager.getSelectedRow();
            if (row) {
                top.jQuery.ligerDialog.open({ width: 770, height: 490, title: "收款详情", url: "CRM/finance/Receive_add.aspx?orderid=" + row.order_id+"&receiveid=" + row.id, buttons: [{ text: '关闭', onclick: function (item, dialog) { dialog.close(); } }] });
            } else {
                $.ligerDialog.warn('请选择行！');
            }

        }
       
        function f_reload() {
            $("#maingrid4").ligerGetGridManager().loadData(true);
        };

    </script>
</head>
<body>
    <form id="form1">
        <div id="toolbar"></div>
        
        <div id="grid">
            <div id="maingrid4" style="margin: -1px; min-width: 800px;"></div>
        </div>


    </form>
    <div class="az">
        <form id='serchform'>
            <table style='width: 920px' class="bodytable1">
                <tr>
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>公司名称：</div>
                    </td>
                    <td>
                        <input type='text' id='company' name='company' ltype='text' ligerui='{width:120}' />
                    </td>
                    <td>
                         <div style='width: 60px; text-align: right; float: right'>收款人：</div>
                    </td>
                    <td>
                        <div style='width: 100px; float: left'>
                            <input type='text' id='department' name='department' />
                        </div>
                        <div style='width: 98px; float: left'>
                            <input type='text' id='employee' name='employee' />
                        </div>
                    </td>
                    
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>收款时间：</div>
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
                        
                    </td>
                    <td>
                                             
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>凭证号码：</div>
                    </td>
                    <td>
                        <input type='text' id='receive_num' name='receive_num' ltype='text' ligerui='{width:120}' />
                      </td>  
                    <td>
                        <div style='width: 60px; text-align: right; float: right'>收款方式：</div>
                    </td>
                    <td>
                        <div style='width: 100px; float: left'>
                            <input type='text' id='pay_type' name='pay_type' />
                        </div>   
                    </td>
                    <td>
                       
                        
                    </td>
                    <td>
                         <input id='Button2' type='button' value='重置' style='width: 80px; height: 24px'
                            onclick="doclear()" />
                        <input id='Button1' type='button' value='搜索' style='width: 80px; height: 24px' onclick="doserch()" />
                    </td>
                </tr>
               
            </table>
        </form>
    </div>
</body>
</html>
