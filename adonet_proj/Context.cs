using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_proj
{
    public class Context
    {
        private string connectionString;
        private SqlConnection connection;

        public Context(string connectionStr)
        {
            connection = new SqlConnection(connectionStr);
            connection.Open();
        }
        public void AllInfoID8()
        {
            Console.WriteLine("> Show all info about the employee with ID 8.");
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from Employees where EmployeeID=8";
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 1; i < reader.FieldCount; i++)
            {
                stringBuilder.AppendLine($"\t{reader.GetName(i)}: {reader.GetValue(i).ToString()}");
            }
            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
