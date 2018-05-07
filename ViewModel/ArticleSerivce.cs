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
            var data = db.ExecuteQuery("select * from article where Id=@id", parameter);
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
            var result = db.ExecuteNonQuery("insert into article(`title`,`maincontent`,`createtime`,`Status`,`Nohtml`) values(@title,@content,'" + DateTime.Now + "',@status,@nohtml)", new MySqlParameter[] { new MySqlParameter("content", article.Content), new MySqlParameter("title", article.Title), new MySqlParameter("status", article.Status),new MySqlParameter("status", article.Nohtml) });
            return result ?Message.Success:Message.Error;
        }
        public List<Article> GetArticleList(DbContext db,int start,int count)
        {
            MySqlParameter[] parameters = { new MySqlParameter("start", start), new MySqlParameter("count", count) };
            List<Article> list = new List<Article>();
            var data = db.ExecuteQuery("select * from article  ORDER BY `CreateTime` DESC limit @start,@count;", parameters);

            foreach (DataRow item in data.Tables[0].Rows)
            {
                Article tt = new Article();
                var dt = new DateTime();
                tt.Id =Convert.ToInt32(item["Id"]);
                tt.Title = item["Title"].ToString();
                tt.Content = item["MainContent"].ToString();
                tt.Nohtml = item["Nohtml"].ToString();
                tt.Comments = new List<Comment>();
                tt.Histories = new List<ArticleHistory>();
                bool flag = DateTime.TryParse(item["CreateTime"].ToString(),out dt);
                tt.CreateTime = flag ? dt : DateTime.MinValue;
                list.Add(tt);
            }
            return list;
        }
    }
    
    public class TitleService
    {
        public List<Title> GetTitles(DbContext db,int start,int count)
        { 
            MySqlParameter[] parameters = { new MySqlParameter("start", start), new MySqlParameter("count", count) };
            List<Title> list = new List<Title>();
            var data = db.ExecuteQuery("select id,title,CreateTime from article  ORDER BY `CreateTime` DESC limit @start,@count;", parameters);

            foreach (DataRow item in data.Tables[0].Rows)
            {
                Title tt = new Title();
                var dt = new DateTime();
                tt.Id =Convert.ToInt32(item["Id"]);
                tt.Name = item["Title"].ToString();
                bool flag = DateTime.TryParse(item["CreateTime"].ToString(),out dt);
                tt.CreateTime = flag ? dt : DateTime.MinValue;
                list.Add(tt);
            }
            return list;
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
            var result = db.ExecuteNonQuery("insert into comment(`PosterId`,`content`,`ArticleId`,`CreateTime`) values('"+guest.Id+"',@content,@aid,'" + DateTime.Now + "')",new MySqlParameter[] { new MySqlParameter("content", content), new MySqlParameter("aid", aId) });
            return result ? Message.Success : Message.Error;
        }
        public List<Comment> GetComments(DbContext db ,int aId)
        {
            if (aId==0)
            {
                return new List<Comment>();
            }
            List<Comment> list = new List<Comment>();
            MySqlParameter[] parameters = { new MySqlParameter("aid", aId) };
            var data = db.ExecuteQuery("select * from `comment` left JOIN person on `comment`.PosterId=person.Id where `comment`.ArticleId=@aid;", parameters);

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
            return list;
        }
    }
}
