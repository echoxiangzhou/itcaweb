﻿using System;


namespace iTCA.Yuwen.Web
{
    public partial class loginout : BasePage
    {
        protected override void Page_Show()
        {
            currentcontext.Response.Cookies["cmsnt"].Expires = DateTime.Now.AddDays(-1d);
            currentcontext.Response.Write("<script>alert('注销成功,跳转到首页')</script>");
            currentcontext.Response.Redirect("index.aspx");
        }
    }
}
