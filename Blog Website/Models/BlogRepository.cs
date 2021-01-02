using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog_Website.Models {
    public static class BlogRepository {
        private static SqlConnection createConnection() {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\chabd\source\repos\Blog Website\Blog Website\Data\Blog.mdf';Integrated Security=True";
            return new SqlConnection(connectionString);
        }
        public static List<Blog> GetBlogsForUser(string ID) {
            SqlConnection connection = createConnection();
            string query = $"select * from Blogs where userID={ID}";  
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows) return null;
            List<Blog> blogs = new List<Blog>();
        
            while (reader.Read()) {
                Blog temp = new Blog();
                temp.blogID = reader.GetInt32(0);
                temp.userID = reader.GetInt32(1);
                temp.blogTitle = reader.GetString(2);
                temp.blogBody = reader.GetString(3);
                temp.blogDate = reader.GetDateTime(4);
                temp.userName = reader.GetString(5);
                blogs.Add(temp);
            }
            connection.Close();
            return blogs;
        }

        public static Blog getBlogByID(string id) {
             SqlConnection connection = createConnection();
            string query = $"select * from Blogs where Id={id}";  
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows) return null;
            while (reader.Read()) {
                Blog temp = new Blog();
                temp.blogID = reader.GetInt32(0);
                temp.userID = reader.GetInt32(1);
                temp.blogTitle = reader.GetString(2);
                temp.blogBody = reader.GetString(3);
                temp.blogDate = reader.GetDateTime(4);
                connection.Close();
                return temp;
            }

            return null;
        }
        public static void AddBlog(Blog blog) {
             SqlConnection connection = createConnection();
            string query = $"insert into Blogs(userID, title, body, date, userName) values({blog.userID},'{blog.blogTitle}','{blog.blogBody}','{blog.blogDate.ToString("MM/dd/yyyy")}', '{blog.userName}')";  
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            Regex trimmer = new Regex(@"\s\s+");
            blog.blogBody = trimmer.Replace(blog.blogBody, " ");
            string q = blog.blogBody;
            blog.blogBody= String.Join(" ", q.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            int rowsAffected = cmd.ExecuteNonQuery();

            //try{
            //} catch(Exception e) {
            //    connection.Close();
            //    throw e;
            //}
            connection.Close();
        }

        public static void DeleteBlog(int id) {
             SqlConnection connection = createConnection();
            string query = $"delete from Blogs where Id={id}";  
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            //try{
            //} catch(Exception e) {
            //    connection.Close();
            //    throw e;
            //}
            connection.Close();
        }
        public static void EditBlog(Blog blog) {
            SqlConnection connection = createConnection();
            string query = $"update Blogs set title='{blog.blogTitle}', body='{blog.blogBody}'  where Id={blog.blogID}";  
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            //try{
            //} catch(Exception e) {
            //    connection.Close();
            //    throw e;
            //}
            connection.Close();
        }
        public static List<Blog> GetAllBlogs() {
            SqlConnection connection = createConnection();
            string query = $"select * from Blogs";  
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows) return null;
            List<Blog> blogs = new List<Blog>();
        
            while (reader.Read()) {
                Blog temp = new Blog();
                temp.blogID = reader.GetInt32(0);
                temp.userID = reader.GetInt32(1);
                temp.blogTitle = reader.GetString(2);
                temp.blogBody = reader.GetString(3);
                temp.blogDate = reader.GetDateTime(4);
                temp.userName = reader.GetString(5);
                blogs.Add(temp);
            }
            connection.Close();
            return blogs;
        }
    }
}
