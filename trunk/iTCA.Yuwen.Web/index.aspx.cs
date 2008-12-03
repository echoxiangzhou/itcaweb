using System;
using System.Collections.Generic;
using iTCA.Yuwen.Core;
using iTCA.Yuwen.Entity;

namespace iTCA.Yuwen.Web
{
    public partial class index : BasePage
    {
        /// <summary>
        /// Э�������б�
        /// </summary>
        protected List<ArticleInfo> newslist;
        /// <summary>
        /// Э�ṫ���б�
        /// </summary>
        protected List<ArticleInfo> annlist;
        /// <summary>
        /// �Ƽ�����ҳ��
        /// </summary>
        protected int pagecount;
        protected string pagecounthtml;
        protected override void Page_Show()
        {
            pagetitle = "��ӭ���� iTCA ���칤ѧԺ�����Э�� - iTCA ���칤ѧԺ�����Э��";
            annlist = Articles.GetArticleCollection(1, 12, 1);
            newslist = Articles.GetArticleCollection("2,3,4", 5, 1);

            pagecount = Articles.GetArticleCollectionPageCount(1, 12);
            pagecounthtml = Natsuhime.Utils.GetStaticPageNumbersHtml(1, pagecount, string.Format("showcolumn-{0}", 1), ".aspx", 8);
        }
    }
}
