using System.Data.SqlClient;
using System.IO;

// Author: Brian Clayton (brianpclayton@gmail.com)
namespace RPSQuestion1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Default UTF-8 encoding, no BOM
            using (StreamWriter csvFile = new StreamWriter(@"..\..\Ten Most Expensive Products.csv"))
            using (SqlConnection con = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;DataBase=Northwind;Integrated Security=SSPI"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Ten Most Expensive Products", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                // Write header containing column names
                for (int col = 0; col < reader.FieldCount - 1; col++)
                    csvFile.Write("\"{0}\",", reader.GetName(col).Replace("\"", "\"\"")); // enclose in double quotes and escape as needed
                csvFile.WriteLine("\"{0}\"", reader.GetName(reader.FieldCount - 1).Replace("\"", "\"\""));

                // Write results
                while (reader.Read())
                {
                    for (int col = 0; col < reader.FieldCount - 1; col++)
                        csvFile.Write("\"{0}\",", reader[col].ToString().Replace("\"", "\"\""));
                    csvFile.WriteLine("\"{0}\"", reader[reader.FieldCount - 1].ToString().Replace("\"", "\"\""));
                }
            }
        }
    }
}
