using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Blog_Website.Models {
    public class UserRepository {
        private static SqlConnection createConnection() {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\chabd\source\repos\Blog Website\Blog Website\Data\Blog.mdf';Integrated Security=True";
            return new SqlConnection(connectionString);
        }
        public static bool registerUser(User user) {
            SqlConnection connection = createConnection();
            string query = $"insert into Users(fname, lname, email, password) values('{user.FName}','{user.LName}','{user.Email}','{user.Password}')";  
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            try{
            int rowsAffected = cmd.ExecuteNonQuery();
            } catch(Exception e) {
                connection.Close();
                return false;
            }
            connection.Close();
            return true;
        }
        public static User SignInUser(string email , string password) {
            SqlConnection connection = createConnection();
            string query = $"select * from Users where email='{email}'";  
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows) return null;
            User signedInUser = new User();
        
            while (reader.Read()) {
                
                signedInUser.userID = reader.GetInt32(0);
                signedInUser.FName = reader.GetString(1);
                signedInUser.LName = reader.GetString(2);
                signedInUser.Email = reader.GetString(3);
                signedInUser.Password = reader.GetString(4);
                if(signedInUser.Email.Equals(email) && signedInUser.Password.Equals(password)) {
                    return signedInUser;
                }
            }
            connection.Close();
            return null ;
        }

        public static User getUserById(string _ID) {
             SqlConnection connection = createConnection();
            string query = $"select * from Users where Id={_ID}";  
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows) return null;
            User signedInUser = new User();
        
            while (reader.Read()) {
                
                signedInUser.userID = reader.GetInt32(0);
                signedInUser.FName = reader.GetString(1);
                signedInUser.LName = reader.GetString(2);
                signedInUser.Email = reader.GetString(3);
                signedInUser.Password = reader.GetString(4);
                    
            }
            connection.Close();
            return signedInUser;
        }
        public static User getUserByEmail(string email) {
             SqlConnection connection = createConnection();
            string query = $"select * from Users where email='{email}'";  
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows) return null;
            User signedInUser = new User();
        
            while (reader.Read()) {
                
                signedInUser.userID = reader.GetInt32(0);
                signedInUser.FName = reader.GetString(1);
                signedInUser.LName = reader.GetString(2);
                signedInUser.Email = reader.GetString(3);
                signedInUser.Password = reader.GetString(4);
                    
            }
            connection.Close();
            return signedInUser;
        }


        public static User UpdateUser(User user) {
            SqlConnection connection = createConnection();
            string query = String.Empty;
            if(user.Password == null) {
                query = $"update Users set fname='{user.FName}', lname='{user.LName}' where email = '{user.Email}'";  

            }else{
                query = $"update Users set fname='{user.FName}', lname='{user.LName}', password='{user.Password}' where email = '{user.Email}'";  
            }
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            try{
            int rowsAffected = cmd.ExecuteNonQuery();
            } catch(Exception e) {
                connection.Close();
                return null;
            }
            connection.Close();
            User newUser = getUserByEmail(user.Email);
            return newUser;
        }
    }
}
