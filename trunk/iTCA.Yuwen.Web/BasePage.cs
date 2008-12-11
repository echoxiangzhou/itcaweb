using System;
using System.Data;
using System.Web;
using Natsuhime.Data;

namespace iTCA.Yuwen.Web
{
    public class BasePage : System.Web.UI.Page
    {
        #region ҳ�����
        /// <summary>
        /// ��ǰҳ�����
        /// </summary>
        protected string pagetitle = "";
        /// <summary>
        /// ҳ������
        /// </summary>
        protected System.Text.StringBuilder templateBuilder = new System.Text.StringBuilder();
        /// <summary>
        /// ҳ��ִ�м�ʱ��
        /// </summary>
        protected System.Diagnostics.Stopwatch sw;
        /// <summary>
        /// ��ǰҳ��ִ��ʱ��(����)
        /// </summary>
        protected string processtime;
        /// <summary>
        /// ҳ���ѯ��
        /// </summary>
        protected int querycount;
        /// <summary>
        /// ҳ���ѯSql����
        /// </summary>
        protected string querydetail;
        /// <summary>
        /// �����Ƿ���post
        /// </summary>
        protected bool ispost;
        /// <summary>
        /// ��ǰ�û���Ϣ(���Ϊ�ձ�ʾδ��¼)
        /// </summary>
        protected iTCA.Yuwen.Entity.UserInfo userinfo;
        #endregion

        protected BasePage()
        {
            //ҳ��ͳ�ƿ�ʼ
            sw = System.Diagnostics.Stopwatch.StartNew();
            DbHelper.QueryCount = 0;
            DbHelper.QueryDetail = "";
            ispost = Natsuhime.Web.YRequest.IsPost();
            //��֤��¼
            CheckLogin();
            //ҳ��ִ��
            Page_Show();

            //ҳ��ͳ�ƽ���
            querycount = DbHelper.QueryCount;
            querydetail = DbHelper.QueryDetail;
            processtime = sw.Elapsed.TotalSeconds.ToString("F6");
        }
        /// <summary>
        /// ҳ�洦���鷽��
        /// </summary>
        protected virtual void Page_Show()
        {
            return;
        }
        /// <summary>
        /// ��֤��¼
        /// </summary>
        protected virtual void CheckLogin()
        {
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["cmsnt"];
            userinfo = null;
            if (cookie != null && cookie.Values["userid"] != null && cookie.Values["password"] != null)
            {
                int uid = Convert.ToInt32(cookie.Values["userid"]);
                string password = cookie.Values["password"].ToString().Trim();

                if (uid > 0 && password != string.Empty)
                {
                    userinfo = iTCA.Yuwen.Core.Users.GetUserInfo(uid.ToString(), password, 3);
                }
            }
        }
    }
}
