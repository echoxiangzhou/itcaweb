﻿using System;
using System.Collections.Generic;

using iTCA.Yuwen.Core;
using iTCA.Yuwen.Entity;
using Natsuhime.Web;

namespace iTCA.Yuwen.Web
{
    public partial class postarticle : BasePage
    {
        protected List<ColumnInfo> columnlist;
        protected override void Page_Show()
        {
            if (userinfo == null)
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('请登录后再投递文章,谢谢~');history.back();</script>");
            }
            columnlist = Articles.GetColumnCollection();
            if (!YRequest.IsPost())
            {

            }
            else
            {
                int columnid = YRequest.GetInt("columnid", 0);
                string title = Utils.RemoveHtml(YRequest.GetString("title"));
                string summary = Utils.RemoveHtml(YRequest.GetString("summary"));
                string content = Utils.RemoveUnsafeHtml(YRequest.GetString("content"));

                ArticleInfo articleinfo = new ArticleInfo();
                articleinfo.Columnid = columnid;
                articleinfo.Title = title;
                //articleinfo.Highlight = ddlHightlight.SelectedValue;
                articleinfo.Summary = summary.Length > 160 ? summary.Substring(0, 159) : summary;
                articleinfo.Content = content;
                articleinfo.Postdate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                articleinfo.Uid = userinfo.Uid;
                articleinfo.Username = userinfo.Username;
                Articles.CreateArticle(articleinfo);

                System.Web.HttpContext.Current.Response.Redirect(string.Format("showcolumn-{0}-1.aspx", articleinfo.Columnid));
            }
        }
    }
}
