<%@ Page Language="C#" AutoEventWireup="true" %>

<!--���ҳ��->
<%
	
	//�ж��Ƿ��½
	HttpCookie cookie = Request.Cookies["UserID"];
	if (Request.IsAuthenticated && null!=cookie)
		Response.Redirect("/main.aspx");

	else
		Response.Redirect("/hcin.aspx");
		//Response.Redirect("/hcin.aspx");
 %>
