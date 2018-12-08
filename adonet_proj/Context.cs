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
        private SqlCommand command;
        private SqlDataReader reader;

        public Context(string connectionStr)
        {
            connection = new SqlConnection(connectionStr);
            connection.Open();
        }

        public void AllInfoID8()
        {
            Console.WriteLine("> 1.Show all info about the employee with ID 8.");
            command = connection.CreateCommand();
            command.CommandText = "select * from Employees where EmployeeID=8";
            reader = command.ExecuteReader();
            reader.Read();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 1; i < reader.FieldCount; i++)
            {
                stringBuilder.AppendLine($"\t{reader.GetName(i)}: {reader.GetValue(i).ToString()}");
            }

            Console.WriteLine(stringBuilder.ToString());
            reader.Close();
        }

        public void ListFLNameLondon()
        {
            Console.WriteLine("\n> 2.Show the list of first and last names of the employees from London.");
            command = connection.CreateCommand();
            command.CommandText = "select FirstName, LastName from Employees where City='London';";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]}, {reader["LastName"]}");
            }

            reader.Close();
        }

        public void ListFLNameA()
        {
            Console.WriteLine("\n> 3.Show the list of first and last names of the employees from London.");
            command = connection.CreateCommand();
            command.CommandText = "select FirstName, LastName from Employees where FirstName like 'A%';";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]}, {reader["LastName"]}");
            }

            reader.Close();
        }

        public void ListFLAge55()
        {
            Console.WriteLine("\n> 4.Show the list of first, last names and ages of the employees whose age is greater than 55. The result should be sorted by last name.");
            command = connection.CreateCommand();
            command.CommandText = "select FirstName, LastName, DATEDIFF(year, BirthDate, GETDATE()) as Age from Employees where DATEDIFF(year, BirthDate, GETDATE()) > 55 order by LastName;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]}, {reader["LastName"]}, {reader["Age"]}");
            }

            reader.Close();
        }

        public void CountEmployeesLondon()
        {
            Console.WriteLine("\n> 5.Calculate the count of employees from London.");
            command = connection.CreateCommand();
            command.CommandText = "select count(*) as countt from Employees where City='London';";
            reader = command.ExecuteReader();
            reader.Read();
            Console.WriteLine(reader["countt"]);
            reader.Close();
        }

        public void MaxMinAvgAgeLondon()
        {
            Console.WriteLine("\n> 6.Calculate the greatest, the smallest and the average age among the employees from London.");
            command = connection.CreateCommand();
            command.CommandText = "select MAX(DATEDIFF(year, BirthDate, GETDATE())) as MaxAge, MIN(DATEDIFF(year, BirthDate, GETDATE())) as MinAge, AVG(DATEDIFF(year, BirthDate, GETDATE())) as AvgAge from Employees where City = 'London';";
            reader = command.ExecuteReader();
            reader.Read();
            Console.WriteLine("Max\tMin\tAvg");
            Console.WriteLine($"{reader["MaxAge"]}\t{reader["MinAge"]}\t{reader["AvgAge"]}");
            reader.Close();
        }

        public void MaxMinAvgAgeEachCity()
        {
            Console.WriteLine("\n> 7.Calculate the greatest, the smallest and the average age of the employees for each city.");
            command = connection.CreateCommand();
            command.CommandText = "select City, MAX(DATEDIFF(year, BirthDate, GETDATE())) as MaxAge, MIN(DATEDIFF(year, BirthDate, GETDATE())) as MinAge, AVG(DATEDIFF(year, BirthDate, GETDATE())) as AvgAge from Employees GROUP BY City;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["City"]}: {reader["MaxAge"]}, {reader["MinAge"]}, {reader["AvgAge"]}");
            }

            reader.Close();
        }

        public void ListCitiesAvgAge60()
        {
            Console.WriteLine("\n> 8.Show the list of cities in which the average age of employees is greater than 60 (the average age is also to be shown).");
            command = connection.CreateCommand();
            command.CommandText = "select City, AVG(DATEDIFF(year, BirthDate, GETDATE())) as AvgBirth from Employees group by City having AVG(DATEDIFF(year, BirthDate, GETDATE())) > 60;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["City"]}: {reader["AvgBirth"]}");
            }

            reader.Close();
        }

        public void EldestEmployees()
        {
            Console.WriteLine("\n> 9.Show the first and last name(s) of the eldest employee(s).");
            command = connection.CreateCommand();
            command.CommandText = "select top 1 FirstName, LastName from Employees order by BirthDate;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]}");
            }

            reader.Close();
        }

        public void ThreeEldestEmployees()
        {
            Console.WriteLine("\n> 10.Show first, last names and ages of 3 eldest employees.");
            command = connection.CreateCommand();
            command.CommandText = "select top 3 FirstName, LastName, DATEDIFF(year, BirthDate, GETDATE()) as Age from Employees order by BirthDate;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]}, age: {reader["Age"]}");
            }

            reader.Close();
        }
    }
}
