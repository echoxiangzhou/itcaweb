using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using Natsuhime.Data;
using LiteCMS.Entity;
using LiteCMS.Core;
using Natsuhime;

namespace LiteCMS.Web
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
        protected LiteCMS.Config.MainConfigInfo config;
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
        protected UserInfo userinfo;
        /// <summary>
        /// �������ֵ֮ǰ����ִ��IsAdminLogined()������ʼ��ֵ.
        /// </summary>
        protected AdminInfo admininfo;
        /// <summary>
        /// �������ֵ֮ǰ����ִ��IsAdminLogined()������ʼ��ֵ.
        /// </summary>
        protected string adminpath;
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
            config = LiteCMS.Config.MainConfigs.GetConfig();
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
                    userinfo = LiteCMS.Core.Users.GetUserInfo(uid, password);
                }
            }
        }
        protected virtual bool IsAdminLogined()
        {
            HttpCookie admincookie = System.Web.HttpContext.Current.Request.Cookies["cmsntadmin"];
            admininfo = null;
            if (admincookie != null && admincookie.Values["adminid"] != null && admincookie.Values["password"] != null)
            {
                int adminid = Convert.ToInt32(admincookie.Values["adminid"]);
                string password = admincookie.Values["password"].ToString().Trim();

                if (adminid > 0 && password != string.Empty)
                {
                    //admininfo todo
                    admininfo = Admins.GetAdminInfo(adminid, password);
                    if (admininfo != null && admininfo.Uid == userinfo.Uid)
                    {
                        adminpath = admincookie.Values["path"].ToString().Trim();
                        return true;
                    }
                }
            }
            //��¼ʧ��
            adminpath = "";
            return false;
        }
        protected virtual void InitBaseList()
        {
            InitHotArticleList();
            InitLatestCommentList();
            InitMostGradeCommentList();

            InitAllColumnArticleListD();
        }

        void InitAllColumnArticleListD()
        {
            TinyCache cache = new TinyCache();
            allcolumnarticlelistd = cache.RetrieveObject("articlelistdictionary_allcolumn") as Dictionary<ColumnInfo, List<ArticleInfo>>;
            if (allcolumnarticlelistd == null)
            {
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
                cache.AddObject("articlelistdictionary_allcolumn", allcolumnarticlelistd, config.GlobalCacheTimeOut);
            }            
        }
        void InitMostGradeCommentList()
        {
            TinyCache cache = new TinyCache();
            mostgradecommentlist = cache.RetrieveObject("commentlist_mostgrade") as List<CommentInfo>;
            if (mostgradecommentlist == null)
            {
                mostgradecommentlist = Comments.GetMostGradComments(6, 1);
                cache.AddObject("commentlist_mostgrade", mostgradecommentlist, config.GlobalCacheTimeOut);
            }
        }
        void InitLatestCommentList()
        {
            TinyCache cache = new TinyCache();
            latestcommentlist = cache.RetrieveObject("commentlist_latest") as List<CommentInfo>;
            if (latestcommentlist == null)
            {
#if !DEBUG
                if (Natsuhime.Web.YRequest.GetUrl().IndexOf("92acg.cn") < 0 && Natsuhime.Web.YRequest.GetUrl().IndexOf("litecms.cn") < 0)
                {
                    currentcontext.Response.End();
                }
#endif
                latestcommentlist = Comments.GetCommentCollection(0, 6, 1);
                cache.AddObject("commentlist_latest", latestcommentlist, config.GlobalCacheTimeOut);
            }
        }
        void InitHotArticleList()
        {
            TinyCache cache = new TinyCache();
            hotarticlelist = cache.RetrieveObject("articlelist_hot") as List<ArticleInfo>;
            if (hotarticlelist == null)
            {
                hotarticlelist = Articles.GetHotArticles(6, 1);
                cache.AddObject("articlelist_hot", hotarticlelist, config.GlobalCacheTimeOut);
            }
        }
    }
}
