﻿<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="iTCA.Yuwen.Web.register" %>
<%@ Import namespace="iTCA.Yuwen.Data" %>
<%@ Import namespace="iTCA.Yuwen.Entity" %>
<script runat="server">
override protected void OnInit(EventArgs e)
{
	/*
	This is a cached-file of template("\templates\register.htm"), it was created by LiteCMS.CN Template Engine.
	Please do NOT edit it.
	此文件为模板文件的缓存("\templates\register.htm"),由 LiteCMS.CN 模板引擎生成.
	所以请不要编辑此文件.
	*/
	base.OnInit(e);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	templateBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	templateBuilder.Append("<head>\r\n");
	templateBuilder.Append("	<link rel=\"stylesheet\" type=\"text/css\" href=\"templates/main.css\" />\r\n");
	templateBuilder.Append("	<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />\r\n");

	if (pagetitle=="")
	{

	templateBuilder.Append("<title>" + config.Websitename.ToString().Trim() + " " + config.Seotitle.ToString().Trim() + " - Powered by 盛夏之地</title>\r\n");

	}
	else
	{

	templateBuilder.Append("<title>" + pagetitle.ToString() + " - " + config.Websitename.ToString().Trim() + " " + config.Seotitle.ToString().Trim() + " - Powered by 盛夏之地</title>\r\n");

	}	//end if

	templateBuilder.Append("</head>\r\n");
	templateBuilder.Append("<body>\r\n");
	templateBuilder.Append("<div id=\"wrap\">\r\n");
	templateBuilder.Append("	<!--头部开始-->\r\n");
	templateBuilder.Append("	<div id=\"header\">\r\n");
	templateBuilder.Append("		<div id=\"main-menu\">\r\n");
	templateBuilder.Append("			<ul>\r\n");
	templateBuilder.Append("				<li><a href=\"index.aspx\">首页</a></li>\r\n");
	templateBuilder.Append("				<li><a href=\"showcolumn.aspx\">新闻</a></li>\r\n");
	templateBuilder.Append("				<li><a href=\"postarticle.aspx\">投递</a></li>\r\n");
	templateBuilder.Append("				<li><a href=\"#\">博客</a></li>\r\n");
	templateBuilder.Append("				<li><a href=\"bbs/\">论坛</a></li>\r\n");

	if (userinfo==null)
	{

	templateBuilder.Append("				<li><a href=\"login.aspx\">登录</a></li>\r\n");
	templateBuilder.Append("				<li><a href=\"register.aspx\">注册</a></li>\r\n");

	}
	else
	{

	templateBuilder.Append("				<li><a href=\"usercontrolpanel.aspx\">用户中心[" + userinfo.Username.ToString().Trim() + "]</a></li>\r\n");

	if (userinfo.Adminid>0)
	{

	templateBuilder.Append("				<li><a href=\"admincp.aspx\" target=\"_blank\">系统设置</a></li>\r\n");

	}	//end if

	templateBuilder.Append("				<li><a href=\"loginout.aspx\">注销</a></li>\r\n");

	}	//end if

	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<!--头部结束-->\r\n");


	templateBuilder.Append("	<!--内容开始-->\r\n");

	templateBuilder.Append("	<!--右栏开始-->\r\n");
	templateBuilder.Append("	<div id=\"right-side\">\r\n");
	templateBuilder.Append("		<div class=\"div-header\">站内搜索</div>\r\n");
	templateBuilder.Append("		<div id=\"search\"><form action=\"search.aspx\" method=\"get\">标题搜索&nbsp;:&nbsp;<input type=\"text\" id=\"searchkey\" name=\"searchkey\" />&nbsp;&nbsp;<input type=\"submit\" value=\"搜索\" /></form></div>	\r\n");
	templateBuilder.Append("		<div id=\"hot-article\" class=\"right-list\">\r\n");

	if (config.Urlrewrite==1)
	{

	templateBuilder.Append("			<div class=\"div-header\"><a href=\"showcolumn-hot" + config.Urlrewriteextname.ToString().Trim() + "\">热门新闻</a></div>\r\n");

	}
	else
	{

	templateBuilder.Append("			<div class=\"div-header\"><a href=\"showcolumn.aspx?type=hot\">热门新闻</a></div>\r\n");

	}	//end if

	templateBuilder.Append("			<ul>\r\n");

	int hotarticleinfo__loop__id=0;
	foreach(ArticleInfo hotarticleinfo in hotarticlelist)
	{
		hotarticleinfo__loop__id++;


	if (config.Urlrewrite==1)
	{

	templateBuilder.Append("				<li><h2><a href=\"showarticle-" + hotarticleinfo.Articleid.ToString().Trim() + "" + config.Urlrewriteextname.ToString().Trim() + "\">" + hotarticleinfo.Title.ToString().Trim() + "</a></h2></li>\r\n");

	}
	else
	{

	templateBuilder.Append("				<li><h2><a href=\"showarticle.aspx?id=" + hotarticleinfo.Articleid.ToString().Trim() + "\">" + hotarticleinfo.Title.ToString().Trim() + "</a></h2></li>\r\n");

	}	//end if


	}	//end loop

	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<div id=\"latest-comment\" class=\"right-list\">\r\n");
	templateBuilder.Append("			<div class=\"div-header\">最新评论</div>\r\n");
	templateBuilder.Append("			<ul>\r\n");

	int latestcommentinfo__loop__id=0;
	foreach(CommentInfo latestcommentinfo in latestcommentlist)
	{
		latestcommentinfo__loop__id++;

	templateBuilder.Append("				<li>\r\n");
	templateBuilder.Append("					<span class=\"content\">" + latestcommentinfo.Content.ToString().Trim() + "</span>\r\n");
	templateBuilder.Append("					<div class=\"comment-info\"><span class=\"comment-author\">" + latestcommentinfo.Username.ToString().Trim() + " </span>对 <span class=\"from-article\">\"<a href=\"\r\n");

	if (config.Urlrewrite==1)
	{

	templateBuilder.Append("showarticle-" + latestcommentinfo.Articleid.ToString().Trim() + "" + config.Urlrewriteextname.ToString().Trim() + "\r\n");

	}
	else
	{

	templateBuilder.Append("showarticle.aspx?id=" + latestcommentinfo.Articleid.ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("\">" + latestcommentinfo.Articletitle.ToString().Trim() + "</a>\"</span> 的评论</div>\r\n");
	templateBuilder.Append("				</li>\r\n");

	}	//end loop

	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<div id=\"hot-comment\" class=\"right-list\">\r\n");
	templateBuilder.Append("			<div class=\"div-header\">热门评论</div>\r\n");
	templateBuilder.Append("			<ul>\r\n");

	int mostgradecommentinfo__loop__id=0;
	foreach(CommentInfo mostgradecommentinfo in mostgradecommentlist)
	{
		mostgradecommentinfo__loop__id++;

	templateBuilder.Append("				<li>\r\n");
	templateBuilder.Append("					<span class=\"content\">" + mostgradecommentinfo.Content.ToString().Trim() + "</span>\r\n");
	templateBuilder.Append("					<div class=\"comment-info\"><span class=\"comment-author\">" + mostgradecommentinfo.Username.ToString().Trim() + " </span>对 <span class=\"from-article\"><a href=\"\r\n");

	if (config.Urlrewrite==1)
	{

	templateBuilder.Append("showarticle-" + mostgradecommentinfo.Articleid.ToString().Trim() + "" + config.Urlrewriteextname.ToString().Trim() + "\r\n");

	}
	else
	{

	templateBuilder.Append("showarticle.aspx?id=" + mostgradecommentinfo.Articleid.ToString().Trim() + "\r\n");

	}	//end if

	templateBuilder.Append("\">" + mostgradecommentinfo.Articletitle.ToString().Trim() + "</a></span> 的评论</div>\r\n");
	templateBuilder.Append("				</li>\r\n");

	}	//end loop

	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<!--右栏结束-->	\r\n");


	templateBuilder.Append("	<!--左栏开始-->\r\n");
	templateBuilder.Append("	<div id=\"left-side\">\r\n");
	templateBuilder.Append("		<!--文章开始-->\r\n");
	templateBuilder.Append("		<div class=\"div-header\">用户注册</div>\r\n");
	templateBuilder.Append("		<br />\r\n");
	templateBuilder.Append("		<br />\r\n");
	templateBuilder.Append("		<form action=\"\" method=\"post\" id=\"login\">\r\n");
	templateBuilder.Append("		    <table>\r\n");
	templateBuilder.Append("		        <tr>\r\n");
	templateBuilder.Append("		            <th>帐号: </th><td><input id=\"email\" name=\"email\" type=\"text\" />(请填写使用中的邮箱地址,本站将采用Email登陆.)</td>\r\n");
	templateBuilder.Append("		        </tr>\r\n");
	templateBuilder.Append("		        <tr>\r\n");
	templateBuilder.Append("		            <th>密码: </th><td><input id=\"password\" name=\"password\" type=\"password\" /></td>\r\n");
	templateBuilder.Append("		        </tr>\r\n");
	templateBuilder.Append("		        <tr>\r\n");
	templateBuilder.Append("		            <th>其他信息<br /><br /></th><td></td>\r\n");
	templateBuilder.Append("		        </tr>\r\n");
	templateBuilder.Append("		        <tr>\r\n");
	templateBuilder.Append("		            <th>用户名: </th><td><input id=\"username\" name=\"username\" type=\"text\" />(注册后无法修改,请确定填写正确.)</td>\r\n");
	templateBuilder.Append("		        </tr>\r\n");
	templateBuilder.Append("		        <tr>\r\n");
	templateBuilder.Append("		            <th></th><td><input type=\"submit\" value=\"注册\" /></td>\r\n");
	templateBuilder.Append("		        </tr>\r\n");
	templateBuilder.Append("		    </table>\r\n");
	templateBuilder.Append("		</form>\r\n");
	templateBuilder.Append("		<!--文章结束-->\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<!--左栏结束-->\r\n");
	templateBuilder.Append("	<!--内容结束-->\r\n");

	templateBuilder.Append("	<div id=\"footer\" title=\"执行时间:" + processtime.ToString() + ",查询数:" + querycount.ToString() + "\">\r\n");
	templateBuilder.Append("		<ul>\r\n");
	templateBuilder.Append("			<li><a href=\"#\">关于本站</a></li>\r\n");
	templateBuilder.Append("			<li><a href=\"#\">联系我们</a></li>		\r\n");
	templateBuilder.Append("			<li><a href=\"#\">广告服务</a></li>	\r\n");
	templateBuilder.Append("			<li>版权所有 © 2004-2008 <a href=\"\">盛夏之地</a></li>\r\n");
	templateBuilder.Append("		</ul>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</body>\r\n");
	templateBuilder.Append("</html>\r\n");



	Response.Write(templateBuilder.ToString());
}
</script>