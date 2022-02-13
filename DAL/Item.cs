using System.Data;
using Dapper;
using System.Data.SqlClient;
namespace ConstructionApp.DAL
{
    public class Item
    {
        public static DataTable getAllItems(string itemName)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection=new SqlConnection("server=localhost;database=constructions;integrated security=true;"))
            {
               var dr= connection.ExecuteReader($"SELECT * FROM items");
                dt.Load(dr);
            }
            return dt;
        }
    }
}
