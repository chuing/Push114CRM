<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iExcel.aspx.cs" Inherits="XHD.CRM.iExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>导入</title>
    <style type="text/css">
        body {font-size:12px;}
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td width="120">
                    部门：<br />
                    
                </td>
                <td width="160">
                    部门ID：<br />
                    <asp:TextBox ID="Dep_id" runat="server" Width="82px"></asp:TextBox>
                </td>
                <td width="120">
                    人员：<br />
                    <asp:TextBox ID="Emp" runat="server" Width="98px"></asp:TextBox>
                </td>
                <td width="160"></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    文件名：<br />
                    <asp:TextBox ID="Fname" runat="server" Width="98px"></asp:TextBox>
                </td>
                <td>开始：<br />
                    <asp:TextBox ID="start_N" runat="server" Width="82px" Text="0"></asp:TextBox>
                </td>
                <td>结束：<br />
                    <asp:TextBox ID="end_N" runat="server" Width="82px" Text="0"></asp:TextBox>
                </td>
                <td>
                    归属ID：<br />
                    <asp:TextBox ID="Emp_id" runat="server" Width="82px"></asp:TextBox>
                </td>
                <td><br />
                    <asp:Button ID="import" runat="server" Text="导入" Width="82px" OnClick="import_Click" />
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblInfo" runat="server" Text="Label"></asp:Label></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>&nbsp;</td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
        
    </form>
</body>
</html>