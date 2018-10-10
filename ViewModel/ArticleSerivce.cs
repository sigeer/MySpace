using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Utility;
using Utility.DbHelper;

namespace ViewModel
{
    public class ArticleSerivce
    {
        public Article GetArticle(DbContext db,int id)
        {
            MySqlParameter parameter = new MySqlParameter("id", id);
            var data = db.ExecuteQuery("select * from article where Id=@id;update article set Viewed=Viewed + 1 where Id=@id", parameter);
            var temp = data.Tables[0];
            DataRow dr;
            if (temp.Rows.Count>0)
            {
                dr = temp.Rows[0];
            }
            else
            {
                return new Article();
            }
            DateTime dt;
            Article article = new Article();
            article.Title = dr["Title"].ToString();
            article.Content = dr["MainContent"].ToString();
             bool flag = DateTime.TryParse(dr["CreateTime"].ToString(), out dt);
            article.CreateTime = flag?dt:DateTime.MinValue;
            return article;
        }
        public string SaveToDb(DbContext db, Article article)
        {
            var result = db.ExecuteNonQuery("insert into article(`title`,`maincontent`,`createtime`,`Status`,`Nohtml`,`Viewed`) values(@title,@content,'" + DateTime.Now + "',@status,@nohtml,0)", new MySqlParameter[] { new MySqlParameter("content", article.Content), new MySqlParameter("title", article.Title), new MySqlParameter("status", article.Status),new MySqlParameter("status", article.Nohtml) });
            return result ?Message.Success:Message.Error;
        }
        public ResponseModel<List<ArticleSimple>> GetArticleList(DbContext db,int start,int count)
        {
            MySqlParameter[] parameters = { new MySqlParameter("start", start), new MySqlParameter("count", count) };
            var dataCount = db.ExecuteQuery("select Count(*) from article");
            var Count =Convert.ToInt32(dataCount.Tables[0].Rows[0].ItemArray[0]);
            List<ArticleSimple> list = new List<ArticleSimple>();
            var data = db.ExecuteQuery("SELECT  article.*,COUNT(comment.articleid) AS commentcount  FROM article LEFT JOIN comment ON article.`Id` = comment.`ArticleId` GROUP BY article.id  ORDER BY `CreateTime` DESC limit @start,@count;", parameters);

            foreach (DataRow item in data.Tables[0].Rows)
            {
                ArticleSimple tt = new ArticleSimple();
                var dt = new DateTime();
                tt.Id =Convert.ToInt32(item["Id"]);
                tt.Title = item["Title"].ToString();
                tt.Content = item["MainContent"].ToString();
                var nohtml = item["Nohtml"].ToString();
                tt.Nohtml = nohtml.Length>13?nohtml.Substring(0,10)+"...":nohtml;
                tt.CommentCount = Convert.ToInt32(item["commentcount"]);
                tt.Histories = new List<ArticleHistory>();
                tt.Viewed = Convert.ToInt32(item["Viewed"]);
                bool flag = DateTime.TryParse(item["CreateTime"].ToString(),out dt);
                tt.CreateTime = flag ? dt : DateTime.MinValue;
                list.Add(tt);
            }
            return new ResponseModel<List<ArticleSimple>>(list,Count);
        }
    }
    
    public class CommentService
    {
        public string PostComment(DbContext db, string content,GuestModel guest,int aId)
        {
            if (aId==0||guest.Id==0)
            {
                return Message.Error;
            }
            var parameters = new MySqlParameter[] { new MySqlParameter("content", content), new MySqlParameter("aid", aId) };
            var result = db.ExecuteNonQuery("insert into comment(`PosterId`,`content`,`ArticleId`,`CreateTime`,`Status`) values('" + guest.Id+"',@content,@aid,'" + DateTime.Now + "',1 );",parameters);
            return result ? Message.Success : Message.Error;
        }
        public ResponseModel<List<Comment>> GetCommentsInArticle(DbContext db ,int aId)
        {
            if (aId==0)
            {
                return new ResponseModel<List<Comment>>();
            }
            List<Comment> list = new List<Comment>();
            MySqlParameter[] parameters = { new MySqlParameter("aid", aId) };
            var dataCount = db.ExecuteQuery("select Count(*) from comment where `comment`.ArticleId=@aid; ",parameters);
            var Count =Convert.ToInt32(dataCount.Tables[0].Rows[0].ItemArray[0]);

            var data = db.ExecuteQuery("select * from `comment` a  left JOIN person b on a.PosterId=b.Id where a.ArticleId=@aid and  a.status = 1;", parameters);

            foreach (DataRow item in data.Tables[0].Rows)
            {
                Comment tt = new Comment();
                var dt = new DateTime();
                tt.Guest = new GuestModel() { ContactInfo=item["ContactInfo"].ToString(),Id=int.Parse(item["PosterId"].ToString()),IP=item["IP"].ToString(),FirstVisitedTime=Convert.ToDateTime( item["FirstVisitedTime"]),Status=1 };
                tt.Content = item["Content"].ToString();
                bool flag = DateTime.TryParse(item["CreateTime"].ToString(), out dt);
                tt.CreateTime = flag ? dt : DateTime.MinValue;
                list.Add(tt);
            }
            return new ResponseModel<List<Comment>>(list,Count);
        }
        public ResponseModel<List<Comment>> GetCommentList(DbContext db,QueryModel queryModel)
        {
            var start = (queryModel.Index - 1) * queryModel.Count;

            List<Comment> list = new List<Comment>();
            MySqlParameter[] parameters = { new MySqlParameter("aid", queryModel.Filter.ArticleId),
                    new MySqlParameter("pid", queryModel.Filter.PosterId),
                    new MySqlParameter("str", "%"+queryModel.Filter.Str+"%"),
                    new MySqlParameter("order", queryModel.Order),
                    new MySqlParameter("status", queryModel.Filter.Status),
                    new MySqlParameter("start", start),
                    new MySqlParameter("count", queryModel.Count)};
            string whereSql = "";
            if (queryModel.Filter.ArticleId!=0)
            {
                whereSql += "`comment`.ArticleId = @aid";
            }
            if (queryModel.Filter.Status!=0)
            {
                whereSql += "`comment`.status = @status";
            }
            if (queryModel.Filter.PosterId!=0)
            {
                whereSql += whereSql.Length > 0 ? " and `comment`.PosterId = @pid" : " `comment`.PosterId = @pid";
            }
            if (!string.IsNullOrEmpty(queryModel.Filter.Str))
            {
                whereSql += " `comment`.Content like @str";
            }
            if (whereSql.Length>0)
            {
                whereSql = " where " + whereSql;
            }
            var dataCount = db.ExecuteQuery("select Count(*) from comment "+whereSql,parameters);
            var Count =Convert.ToInt32(dataCount.Tables[0].Rows[0].ItemArray[0]);
            var data = db.ExecuteQuery("select * from `comment` left JOIN person on `comment`.PosterId=person.Id left join article on `comment`.ArticleId = article.Id " + whereSql +" ORDER BY `comment`.`CreateTime` DESC limit @start,@count;", parameters);

            foreach (DataRow item in data.Tables[0].Rows)
            {
                Comment comment = new Comment();
                var dt = new DateTime();
                int temp;
                comment.Guest = new GuestModel() { ContactInfo = item["ContactInfo"].ToString(), Id = int.Parse(item["PosterId"].ToString()), IP = item["IP"].ToString(), FirstVisitedTime = Convert.ToDateTime(item["FirstVisitedTime"]), Status = 1 };
                var content = item["Content"].ToString();
                comment.Content = content.Length>13?content.Substring(0,10)+"...":content;
                
                comment.Id = Convert.ToInt32(item["Id"]);
                comment.Article = new Article() { Id = Convert.ToInt32(item["ArticleId"]), Title = item["Title"].ToString() };
                bool flag = DateTime.TryParse(item["CreateTime"].ToString(), out dt);
                comment.CreateTime = flag ? dt : DateTime.MinValue;
                flag = int.TryParse(item["Status"].ToString(),out temp);
                comment.Status = temp;
                list.Add(comment);
            }
            return new ResponseModel<List<Comment>>(list,Count);
        }

        public static bool Delete(DbContext dbContext,int id)
        {
            var sqlStr = "UPDATE COMMENT a SET STATUS =  ( CASE WHEN a.`Status`<=-5 THEN a.`Status` ELSE a.`Status`-1 END )  WHERE a.id = " + id;
            var exeResult = dbContext.ExecuteNonQuery(sqlStr);
            return exeResult;
        }

        public static List<KeyValue> GetBaseSettings(DbContext dbContext)
        {
            var sqlStr = "select * from systemdetail where baseid = 2";
            var queryResult = dbContext.ExecuteQuery(sqlStr);
            List<KeyValue> list = new List<KeyValue>();
            foreach (DataRow item in queryResult.Tables[0].Rows)
            {
                KeyValue kv = new KeyValue();
                kv.Key = Convert.ToInt32(item["DetailId"]);
                kv.Value = item["DetailName"].ToString();
                list.Add(kv);
            }
            return list;
        }

        public static bool Modify(DbContext dbContext, int id ,int status)
        {
            var sqlStr = "update comment set status = " + status + " where id = " + id;
            return dbContext.ExecuteNonQuery(sqlStr);
        }
    }
}
