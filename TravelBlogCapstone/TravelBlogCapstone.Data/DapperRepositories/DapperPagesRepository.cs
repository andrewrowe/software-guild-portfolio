using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.DapperRepositories
{
    public static class DapperPagesRepository
    {
        public static void Add(StaticPage newPage)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                try
                {
                    cn.Open();
                    var p = new DynamicParameters();
                    p.Add("Title", newPage.Title);
                    p.Add("PageContent", newPage.PageContent);
                   
                    cn.Execute("INSERT INTO StaticPages (Title, PageContent) VALUES (@Title, @PageContent)", p);
                }
                catch (Exception ex)
                {
                    // do something...
                }

            }
        }

        public static List<StaticPage> GetAll()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                    List<StaticPage> pages = new List<StaticPage>();
                    cn.Open();
                    pages = cn.Query<StaticPage>("SELECT * FROM StaticPages").ToList();
                    return pages;
            }
        }

        public static void Delete(int ID)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "DELETE StaticPages WHERE ID = @ID";
                var p = new DynamicParameters();
                p.Add("ID", ID);
                cn.Execute(query, p);
            }
        }

        public static StaticPage Get(int ID)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                StaticPage page = new StaticPage();
                cn.Open();
                var p = new DynamicParameters();
                p.Add("ID", ID);
                page = cn.Query<StaticPage>("SELECT * FROM StaticPages WHERE StaticPages.ID = @ID", p).FirstOrDefault();
                return page;
            }
        }

        public static void Update(StaticPage page)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                cn.Open();
                var p = new DynamicParameters();
                p.Add("ID", page.ID);
                p.Add("PageContent", page.PageContent);
                p.Add("Title", page.Title);
                cn.Execute("UPDATE StaticPages SET PageContent = @PageContent, Title = @Title WHERE StaticPages.ID = @ID", p);
            }
        }
    }
}
