<%@ Page Language="C#" AutoEventWireup="true" %>

<!--入口页面->
<%
	
	//判断是否登陆
	HttpCookie cookie = Request.Cookies["UserID"];
	if (Request.IsAuthenticated && null!=cookie)
		Response.Redirect("/main.aspx");

	else
		Response.Redirect("/hcin.aspx");
		//Response.Redirect("/hcin.aspx");
 %>
