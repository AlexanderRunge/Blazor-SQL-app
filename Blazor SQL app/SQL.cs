using System.Data;
using System.Data.SqlClient;

namespace Blazor_SQL_app
{
    public class SQL
    {
        SqlConnection con = new("Data Source=DESKTOP-Q6E964L\\SQLEXPRESS;" +
            "Initial Catalog=BreakfastH1;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False");
        public bool CreateFood(Food f)
        {
            using (con)
            {
                SqlCommand cmd = new("INSERT INTO [food] VALUES (@item, @amount, @price)", con);
                cmd.Parameters.Add("@item", SqlDbType.NVarChar).Value = f.Item;
                cmd.Parameters.Add("@amount", SqlDbType.Int).Value = f.Amount;
                cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = f.Price;
                con.Open();
                if (cmd.ExecuteNonQuery() == 1) 
                {
                    con.Close();
                    return true;
                }
                con.Close();
                return false;
            }
        }

        public bool DeletedFood(int Id)
        {
            using (con)
            {
                SqlCommand cmd = new($"DELETE FROM [food] WHERE [id]=@id", con);
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = Id;
                con.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                con.Close();
                return false;
            }
        }

        public List<Food> ReadFood()
        {
            List<Food> list = new();
            SqlCommand cmd = new("SELECT * FROM [food]", con);
            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Food f = new()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Item = reader["item"].ToString(),
                        Price = double.Parse(reader["price"].ToString()),
                        Amount = int.Parse(reader["amount"].ToString())
                    };
                    list.Add(f);
                }
            }
            con.Close();
            return list;
        }
    }
}
