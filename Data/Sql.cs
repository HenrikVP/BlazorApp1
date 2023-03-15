using System.Data.SqlClient;

namespace BlazorApp1.Data
{
    public class Sql
    {
        string connectionString = "Data Source=192.168.2.3;Initial Catalog = MemeDB; User ID = sa; Password=Passw0rd;";

        public List<Meme> Read()
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
    }
}

