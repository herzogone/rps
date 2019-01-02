using Microsoft.VisualBasic.FileIO;
using System;
using System.Data.SqlClient;
using System.IO;

// Author: Brian Clayton (brianpclayton@gmail.com)
namespace RPSQuestion2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TextFieldParser parser = new TextFieldParser(new StreamReader(@"..\..\Customers.csv")))
            using (SqlConnection con = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;DataBase=Northwind;Integrated Security=SSPI"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;
                con.Open();

                // Need to make sure the Add Customer stored procedure exists by running "Add Customer.sql" once
                SqlCommand cmd = new SqlCommand("Add Customer", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                string[] fields;
                while (!parser.EndOfData)
                {
                    cmd.Parameters.Clear();
                    fields = parser.ReadFields();
                    try
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", fields[0]);
                        cmd.Parameters.AddWithValue("@CompanyName", fields[1]);
                        cmd.Parameters.AddWithValue("@ContactName", fields[2]);
                        cmd.Parameters.AddWithValue("@ContactTitle", fields[3]);
                        cmd.Parameters.AddWithValue("@Address", fields[4]);
                        cmd.Parameters.AddWithValue("@City", fields[5]);
                        cmd.Parameters.AddWithValue("@Region", fields[6]);
                        cmd.Parameters.AddWithValue("@PostalCode", fields[7]);
                        cmd.Parameters.AddWithValue("@Country", fields[8]);
                        cmd.Parameters.AddWithValue("@Phone", fields[9]);
                        cmd.Parameters.AddWithValue("@Fax", fields[10]);
                        Console.WriteLine("Adding customer {0}", fields[0]);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Microsoft.VisualBasic.FileIO.MalformedLineException ex)
                    {
                        Console.WriteLine("Invalid line: {0}", ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unexpected error: {0}", ex.Message);
                    }
                }
            }
        }
    }
}
