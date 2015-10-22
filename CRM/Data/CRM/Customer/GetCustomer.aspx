<%@ Page Language="C#" AutoEventWireup="true" %>

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
                    { display: '���', width: 50, render: function (rowData, rowindex, value, column, rowid, page, pagesize) { return (page - 1) * pagesize + rowindex + 1; } },
                    { display: '��˾����', name: 'Customer', width: 160, align: 'left' },

                    {
                        display: '�ͻ�����', name: '', width: 120, render: function (item) {
                            return item.Department + "." + item.Employee;
                        }
                    },

                    {
                        display: '������', name: 'lastfollow', width: 90, render: function (item) {
                            return formatTimebytype(item.lastfollow, 'yyyy-MM-dd');
                        }
                    },
                    { display: '�绰', name: 'tel', width: 150 }

                ],
                onAfterShowData: function (grid) {
                    $("tr[rowtype='�ѳɽ�']").addClass("l-treeleve2").removeClass("l-grid-row-alt");                       
                },
                checkbox: false,
                dataAction: 'server',
                pageSize: 30,
                pageSizeOptions: [20, 30, 50, 100],
                url: "../../Data_crm/crm_customer.ashx?Action=grid&rnd=" + Math.random(),
                width: '100%',
                height: '100%',
                //title: "Ա���б�",
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

            items.push({ type: 'textbox', id: 'stext', text: '������' });
            items.push({ type: 'button', text: '����', icon: '../img_crm/search.gif', disable: true, click: function () { doserch() } });

            $("#toolbar").ligerToolBar({
                items: items

            });

            $("#stext").ligerTextBox({ width: 200, nullText: "������������" })
            $("#maingrid4").ligerGetGridManager().onResize();

            var toolbar1 = new Toolbar({
                renderTo: 'serchbar1',
                items: [
                {
                    type: 'textfield',
                    text: '�ؼ��֣�',
                    id: "company",
                    useable: 'enabled',
                    handler: function () {
                        //EditButton();
                    }
                }, {
                    type: 'button',
                    text: '��ѯ',
                    bodyStyle: 'search ',
                    useable: 'T',
                    handler: function () {
                        //alert($("#Serchtext").val())
                        doserch();
                    }
                }
                ]
                //�����ĸ�
            });
            toolbar1.render();

            $("#company").ligerTextBox({ width: 200, nullText: "����ؼ������������ͻ�" });
        }
        function doserch() {
            var sendtxt = "&Action=grid&rnd=" + Math.random();
            var serchtxt = $("#form1 :input").fieldSerialize() + sendtxt;
            //alert(serchtxt);
            $.ligerDialog.waitting('���ݲ�ѯ��,���Ժ�...');
            var manager = $("#maingrid4").ligerGetGridManager();

            manager.setURL("../../Data_crm/crm_customer.ashx?" + serchtxt);
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