using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using Natsuhime.Data;
using iTCA.Yuwen.Entity;
using iTCA.Yuwen.Core;

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
        protected iTCA.Yuwen.Config.MainConfigInfo config;
        /// <summary>
        /// System.Web.HttpContext.Current
        /// </summary>
        protected HttpContext currentcontext = System.Web.HttpContext.Current;
        /// <summary>
        /// �����Ƿ���post
        /// </summary>
        protected bool ispost;
        /// <summary>
        /// ��ǰ�û���Ϣ(���Ϊ�ձ�ʾδ��¼)
        /// </summary>
        protected iTCA.Yuwen.Entity.UserInfo userinfo;
        /// <summary>
        /// ��������(�ظ�������,��ҳ��Ϊ6��)
        /// </summary>
        protected List<ArticleInfo> hotarticlelist;
        /// <summary>
        /// ���»ظ�
        /// </summary>
        protected List<CommentInfo> latestcommentlist;
        /// <summary>
        /// ��������
        /// </summary>
        protected List<CommentInfo> mostgradecommentlist;

        //protected List<List<ArticleInfo>> allcolumnarticlelist;
        protected Dictionary<ColumnInfo, List<ArticleInfo>> allcolumnarticlelistd;
        #endregion

        protected BasePage()
        {
            //ҳ��ͳ�ƿ�ʼ
            sw = System.Diagnostics.Stopwatch.StartNew();
            DbHelper.QueryCount = 0;
            DbHelper.QueryDetail = "";
            config = iTCA.Yuwen.Config.MainConfigs.GetConfig();
            ispost = Natsuhime.Web.YRequest.IsPost();
            //��֤��¼
            CheckLogin();
            //��ʼ�������б�
            InitBaseList();
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
                    userinfo = iTCA.Yuwen.Core.Users.GetUserInfo(uid, password);
                }
            }
        }

        protected virtual void InitBaseList()
        {
            hotarticlelist = Articles.GetHotArticles(6, 1);
            latestcommentlist = Comments.GetCommentCollection(0, 6, 1);
            mostgradecommentlist = Comments.GetMostGradComments(6, 1);

            allcolumnarticlelistd = new Dictionary<ColumnInfo, List<ArticleInfo>>();
            //allcolumnarticlelist = new List<List<ArticleInfo>>();
            List<ColumnInfo> columnlist = Columns.GetColumnCollection();
            foreach (ColumnInfo info in columnlist)
            {
                
                if (info.Parentid == 0)
                {
                    allcolumnarticlelistd.Add(info, Articles.GetArticleCollection(info.Columnid, 6, 1));
                    //allcolumnarticlelist.Add(Articles.GetArticleCollection(info.Columnid, 6, 1));
                }
            }

            //foreach (KeyValuePair<ColumnInfo, List<ArticleInfo>> b in a)
            //{
            //    System.Diagnostics.Debug.WriteLine(b.Key.Columnname + ":" + b.Value.Count);
            //}
        }
    }
}
