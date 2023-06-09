﻿using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlazorApp1.Data
{
    public class Sql
    {
        //string myDb1ConnectionString = _configuration.GetConnectionString("myDb1");
        public static string connectionString;// = "Data Source= .;Initial Catalog = MemeDB; User ID = sa; Password=Passw0rd;";

        public static List<Meme> Read()
        {
            List<Meme> list = new List<Meme>();
            SqlConnection con = new(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM MemeTable", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Meme meme = new Meme() { Id = dr.GetInt32(0), MemeName = dr.GetString(1), Url = dr.GetString(2) };
                list.Add(meme);
            }
            con.Close();

            return list;
        }

        public static void Create(Meme meme) 
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("INSERT INTO MemeTable (MemeName, Url) VALUES (@memeName, @memeUrl)", conn);
                cmd.Parameters.Add("@memeName", SqlDbType.NVarChar).Value = meme.MemeName;
                cmd.Parameters.Add("@memeUrl", SqlDbType.NVarChar).Value = meme.Url;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void Delete(int id)
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("DELETE FROM MemeTable WHERE Id = @id", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}

