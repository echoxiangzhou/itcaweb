﻿using System;
using System.Collections.Generic;
using iTCA.Yuwen.Core;
using iTCA.Yuwen.Entity;
using Natsuhime.Web;

namespace iTCA.Yuwen.Web
{
    public partial class comment : BasePage
    {
        protected override void Page_Show()
        {
            if (userinfo == null)
            {
                currentcontext.Response.Write("请登录后再留言评论.");
                currentcontext.Response.End();
                return;
            }
            string action = YRequest.GetQueryString("action");
            if (action == string.Empty)
            {
                currentcontext.Response.End();
            }
            if (action == "postcomment")
            {
                string content = YRequest.GetFormString("commentcontent");
                int articleid = YRequest.GetQueryInt("articleid", 0);
                if (content != string.Empty && articleid > 0)
                {
                    if (content != string.Empty)
                    {
                        CommentInfo info = new CommentInfo();
                        info.Articleid = articleid;
                        info.Uid = userinfo.Uid;
                        info.Username = userinfo.Username;
                        info.Postdate = DateTime.Now.ToString();
                        info.Del = 0;
                        info.Content = Utils.RemoveUnsafeHtml(content);
                        info.Goodcount = 0;
                        info.Badcount = 0;
                        Comments.CreateComment(info);
                        currentcontext.Response.Redirect(YRequest.GetUrlReferrer());
                    }
                }
                else
                {
                    currentcontext.Response.Write("参数为空.");
                    currentcontext.Response.End();
                    return;
                }
            }
            else if (action == "grade")
            {
                int commentid = YRequest.GetQueryInt("commentid", 0);
                if (commentid > 0)
                {
                    int type = YRequest.GetQueryInt("type", 0);
                    Comments.GradeComment(commentid, type);
                    currentcontext.Response.Redirect(YRequest.GetUrlReferrer());
                }
                else
                {
                    currentcontext.Response.Write("参数为空.");
                    currentcontext.Response.End();
                    return;
                }
            }
            else if (action == "del")
            {
                int commentid = YRequest.GetQueryInt("commentid", 0);
                if (commentid > 0)
                {
                    Comments.DeleteComment(commentid);
                    currentcontext.Response.Redirect(YRequest.GetUrlReferrer());
                }
                else
                {
                    currentcontext.Response.Write("参数为空.");
                    currentcontext.Response.End();
                    return;
                }
            }
            else
            {
                currentcontext.Response.Write("err");
                currentcontext.Response.End();
            }
        }
    }
}