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

        public void AllCities()
        {
            Console.WriteLine("\n> 11.Show the list of all cities where the employees are from.");
            command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT City FROM Employees;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["City"]}");
            }

            reader.Close();
        }

        public void EmployeesBirthdaysThisMonth()
        {
            Console.WriteLine("\n> 12. Show first, last names and dates of birth of the employees who celebrate their birthdays this month \n");
            command = connection.CreateCommand();
            command.CommandText = "SELECT FirstName, LastName, BirthDate FROM Employees WHERE MONTH(BirthDate) = 12;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]}, Birth date: {reader["BirthDate"]}");
            }

            reader.Close();
        }

        public void EmployeesSnippedToMadrid()
        {
            Console.WriteLine("\n> 13. Show first and last names of the employees who used to serve orders shipped to Madrid. \n");
            command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT FirstName, LastName FROM Employees JOIN Orders ON Employees.EmployeeID = Orders.EmployeeID WHERE ShipCity = 'Madrid';";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]}");
            }

            reader.Close();
        }

        public void EmployeesOrders1997()
        {
            Console.WriteLine("\n> 14. Show first and last names of the employees as well as the count of orders each of them have received during the year 1997 (use left join). \n");
            command = connection.CreateCommand();
            command.CommandText = "SELECT E.FirstName, E.LastName, COUNT(O.EmployeeID) AS OrdersAmount FROM Employees AS E LEFT JOIN Orders AS O ON O.EmployeeID = E.EmployeeID WHERE O.OrderDate BETWEEN '1997-01-01' AND '1997-12-31' GROUP BY E.FirstName, E.LastName;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]}, orders count: {reader["OrdersAmount"]}");
            }

            reader.Close();
        }

        public void EmployeesOrdersCount1997()
        {
            Console.WriteLine("\n> 15. Show first and last names of the employees as well as the count of " +
                "orders each of them have received during the year 1997\n");
            command = connection.CreateCommand();
            command.CommandText = @"SELECT Employees.FirstName, Employees.LastName, COUNT(Orders.EmployeeID) AS numOfOrders
            FROM Employees
            JOIN Orders ON Orders.EmployeeID = Employees.EmployeeID
            WHERE Orders.OrderDate BETWEEN '1997-01-01' AND '1997-12-31'
            GROUP BY Employees.FirstName, Employees.LastName;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]}, orders count: {reader["numOfOrders"]}");
            }

            reader.Close();
        }

        public void EmployeesOrdersCountAfterRequiredDate1997()
        {
            Console.WriteLine("\n> 16.Show first and last names of the employees as well as the count of their " +
                "orders shipped after required date during the year 1997 (use left join).\n");
            command = connection.CreateCommand();
            command.CommandText = @"SELECT Employees.FirstName, Employees.LastName, COUNT(Orders.EmployeeID) AS numOfOrders 
            FROM Employees 
            LEFT JOIN Orders ON Orders.EmployeeID = Employees.EmployeeID 
            WHERE Orders.OrderDate BETWEEN '1997-01-01' AND '1997-12-31' AND Orders.ShippedDate > Orders.RequiredDate 
            GROUP BY Employees.FirstName, Employees.LastName;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]}, orders count: {reader["numOfOrders"]}");
            }

            reader.Close();
        }

        public void OrdersCountFrance()
        {
            Console.WriteLine("\n> 17.Show the count of orders made by each customer from France.\n");
            command = connection.CreateCommand();
            command.CommandText = @"SELECT Customers.CustomerID, COUNT(Orders.CustomerID) AS numOfOrders 
            FROM Customers 
            JOIN Orders ON Orders.CustomerID = Customers.CustomerID 
            WHERE Customers.Country = 'France'
            GROUP BY Customers.CustomerID;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"CustomerID: {reader["CustomerID"]}, orders count {reader["numOfOrders"]}");
            }

            reader.Close();
        }

        public void FrenchCustomersMoreThan1OrderGrouping()
        {
            Console.WriteLine("\n> 18. Show the list of french customers’ names who have made more than one order(use grouping).\n");
            command = connection.CreateCommand();
            command.CommandText = @"SELECT C.ContactName 
            FROM Customers AS C 
            JOIN Orders AS O ON O.CustomerID = C.CustomerID
            WHERE C.Country = 'France'
            GROUP BY C.ContactName       
            HAVING COUNT (O.CustomerID) > 1;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Customer: {reader["ContactName"]}");
            }

            reader.Close();
        }

        public void FrenchCustomersMoreThan1Order()
        {
            Console.WriteLine("\n> 19.Show the list of french customers’ names who have made more than one order.\n");
            command = connection.CreateCommand();
            command.CommandText = @"select Customers.ContactName from Customers inner join Orders on Customers.CustomerID = Orders.CustomerID 
            where Customers.Country = 'France' Group By(Customers.ContactName)  HAVING(COUNT(Orders.CustomerID) > 1) ;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Customer: {reader["ContactName"]}");
            }

            reader.Close();
        }

        public void CustomersOrderedTofu()
        {
            Console.WriteLine("\n> 20.Show the list of customers’ names who used to order the ‘Tofu’ product.\n");
            command = connection.CreateCommand();
            command.CommandText = @"select Customers.ContactName from Customers inner join Orders  on Customers.CustomerID=Orders.CustomerID inner join [Order Details] on [Order Details].OrderID=Orders.OrderID 
            inner join Products on [Order Details].ProductID = Products.ProductID where Products.ProductName = 'Tofu';";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Customer: {reader["ContactName"]}");
            }

            reader.Close();
        }
    }
}
