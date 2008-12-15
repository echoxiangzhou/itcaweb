using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using Natsuhime.Data;
using iTCA.Yuwen.Entity;

namespace iTCA.Yuwen.Data.Sqlite
{
    public partial class DataProvider : IDataProvider
    {
        #region ȡ���б�
        /// <summary>
        /// ȡ�������б�.
        /// </summary>
        /// <param name="cid">��Ŀid(���Ϊ0��ʾȡ��������Ŀ)</param>
        /// <param name="pagesize">��ҳ��С</param>
        /// <param name="currentpage">��ǰҳ</param>
        /// <returns>�б�</returns>
        public System.Data.IDataReader GetArticles(int cid, int pagesize, int currentpage)
        {
            IDataReader dr;
            int recordoffset = (currentpage - 1) * pagesize;

            DbParameter[] prams = 
		    {
			    DbHelper.MakeInParam("@columnid", DbType.Int32, 4,cid),
			    DbHelper.MakeInParam("@recordoffset", DbType.Int32, 4,recordoffset),
			    DbHelper.MakeInParam("@pagesize", DbType.Int32, 4,pagesize)
		    };
            if (cid > 0)
            {
                dr = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM wy_articles WHERE del=0 AND columnid=@columnid ORDER BY articleid DESC LIMIT @recordoffset,@pagesize", prams);
            }
            else
            {
                dr = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM wy_articles WHERE del=0 ORDER BY articleid DESC LIMIT @recordoffset,@pagesize", prams);
            }
            return dr;
        }
        public int GetArticleCollectionPageCount(int cid, int pagesize)
        {
            int recordcount;
            DbParameter[] prams = 
		    {
			    DbHelper.MakeInParam("@columnid", DbType.Int32, 4,cid)
		    };
            if (cid > 0)
            {
                recordcount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(articleid) FROM wy_articles WHERE del=0 AND columnid=@columnid", prams));
            }
            else
            {
                recordcount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(articleid) FROM wy_articles WHERE del=0", prams));
            }
            return recordcount % pagesize == 0 ? recordcount / pagesize : recordcount / pagesize + 1;
        }

        /// <summary>
        /// ͨ��cid�б�ȡ�������б�.
        /// </summary>
        /// <param name="cids">��Ŀid�ַ���(,�ָ�)Ϊ���ַ�����ʾ������Ŀ</param>
        /// <param name="pagesize">��ҳ��С</param>
        /// <param name="currentpage">��ǰҳ</param>
        /// <returns>�б�</returns>
        public IDataReader GetArticles(string cids, int pagesize, int currentpage)
        {
            IDataReader dr;
            int recordoffset = (currentpage - 1) * pagesize;

            DbParameter[] prams = 
		    {
			    DbHelper.MakeInParam("@columnids", DbType.String, 100,cids),
			    DbHelper.MakeInParam("@recordoffset", DbType.Int32, 4,recordoffset),
			    DbHelper.MakeInParam("@pagesize", DbType.Int32, 4,pagesize)
		    };
            if (cids.Trim() != "")
            {
                dr = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM wy_articles WHERE del=0 AND columnid IN (" + cids + ") ORDER BY articleid DESC LIMIT @recordoffset,@pagesize", prams);
            }
            else
            {
                dr = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM wy_articles WHERE del=0 ORDER BY articleid DESC LIMIT @recordoffset,@pagesize", prams);
            }
            return dr;
        }
        public int GetArticleCollectionPageCount(string cids, int pagesize)
        {
            int recordcount;
            DbParameter[] prams = 
		    {
			    DbHelper.MakeInParam("@columnids", DbType.String, 100,cids)
		    };
            if (cids.Trim() != "")
            {
                recordcount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(articleid) FROM wy_articles WHERE del=0 AND columnid IN (" + cids + ") ", prams));
            }
            else
            {
                recordcount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(articleid) FROM wy_articles WHERE del=0", prams));
            }
            return recordcount % pagesize == 0 ? recordcount / pagesize : recordcount / pagesize + 1;
        }

        public IDataReader GetUserArticles(int uid, int pagesize, int currentpage)
        {
            IDataReader dr;
            int recordoffset = (currentpage - 1) * pagesize;

            DbParameter[] prams = 
		    {
			    DbHelper.MakeInParam("@uid", DbType.Int32, 4,uid),
			    DbHelper.MakeInParam("@recordoffset", DbType.Int32, 4,recordoffset),
			    DbHelper.MakeInParam("@pagesize", DbType.Int32, 4,pagesize)
		    };
            dr = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM wy_articles WHERE del=0 AND uid=@uid ORDER BY articleid DESC LIMIT @recordoffset,@pagesize", prams);

            return dr;
        }
        public int GetUserArticleCollectionPageCount(int uid, int pagesize)
        {
            int recordcount;
            DbParameter[] prams = 
		    {
			    DbHelper.MakeInParam("@uid", DbType.Int32, 4,uid)
		    };
            recordcount = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "SELECT COUNT(articleid) FROM wy_articles WHERE del=0 AND uid=@uid", prams));

            return recordcount % pagesize == 0 ? recordcount / pagesize : recordcount / pagesize + 1;
        }
        #endregion

        /// <summary>
        /// ȡ����������
        /// </summary>
        /// <param name="articleid">����id</param>
        /// <returns>IDataReader</returns>
        public IDataReader GetArticleInfo(int articleid)
        {
            IDataReader dr;
            DbParameter[] prams = 
		    {
			    DbHelper.MakeInParam("@articleid", DbType.Int32, 4,articleid)
		    };
            dr = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM wy_articles WHERE articleid=@articleid", prams);
            return dr;
        }
        public void CreateArticle(ArticleInfo articleinfo)
        {
            DbParameter[] prams = 
		    {
			    DbHelper.MakeInParam("@title", DbType.String, 100,articleinfo.Title),
			    DbHelper.MakeInParam("@columnid", DbType.Int32, 4,articleinfo.Columnid),
			    DbHelper.MakeInParam("@highlight", DbType.String, 20,articleinfo.Highlight),
                DbHelper.MakeInParam("@summary", DbType.String, 160,articleinfo.Summary),
			    DbHelper.MakeInParam("@content", DbType.String, 5000,articleinfo.Content),
			    DbHelper.MakeInParam("@postdate", DbType.DateTime, 8,articleinfo.Postdate),
			    DbHelper.MakeInParam("@uid", DbType.Int32, 4,articleinfo.Uid),
			    DbHelper.MakeInParam("@username", DbType.String, 20,articleinfo.Username)
		    };
            DbHelper.ExecuteNonQuery(CommandType.Text, "INSERT INTO wy_articles(title,columnid,highlight,summary,content,postdate,uid,username) VALUES(@title,@columnid,@highlight,@summary,@content,@postdate,@uid,@username)", prams);
        }
        public void EditArticle(ArticleInfo articleinfo)
        {
            DbParameter[] prams = 
		    {
			    DbHelper.MakeInParam("@title", DbType.String, 100,articleinfo.Title),
			    DbHelper.MakeInParam("@columnid", DbType.Int32, 4,articleinfo.Columnid),
			    DbHelper.MakeInParam("@highlight", DbType.String, 20,articleinfo.Highlight),
			    DbHelper.MakeInParam("@summary", DbType.String, 160,articleinfo.Summary),
			    DbHelper.MakeInParam("@content", DbType.String, 5000,articleinfo.Content),
			    DbHelper.MakeInParam("@postdate", DbType.DateTime, 8,articleinfo.Postdate),
			    DbHelper.MakeInParam("@uid", DbType.Int32, 4,articleinfo.Uid),
			    DbHelper.MakeInParam("@username", DbType.String, 20,articleinfo.Username),
			    DbHelper.MakeInParam("@articleid", DbType.Int32, 4,articleinfo.Articleid)
		    };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE wy_articles SET title=@title,columnid=@columnid,highlight=@highlight,summary=@summary,content=@content,postdate=@postdate,uid=@uid,username=@username WHERE articleid=@articleid", prams);
        }
        public void DeleteArticle(int articleid)
        {
            DbParameter[] prams = 
		    {
			    DbHelper.MakeInParam("@articleid", DbType.Int32, 4,articleid)
		    };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE wy_articles SET del=1 WHERE articleid=@articleid", prams);
        }
    }
}
