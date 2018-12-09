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

        public void CustomersOrderedTofuWithTotalAmountSum()
        {
            Console.WriteLine("\n> 21.	*Show the list of customers’ names who used to order the ‘Tofu’ product, along with the total amount of the product they have ordered " +
                "and with the total sum for ordered product calculated.\n");
            command = connection.CreateCommand();
            command.CommandText = @"SELECT C.ContactName, SUM(OD.Quantity) AS Count, SUM(OD.UnitPrice * OD.Quantity) AS PriceSum 
                                                 FROM Customers AS C 
                                                 LEFT JOIN Orders AS O ON C.CustomerID = O.CustomerID 
                                                 LEFT JOIN [Order Details] AS OD ON OD.OrderId = O.OrderId
                                                 LEFT JOIN [Products] AS P ON P.ProductID = OD.ProductID 
                                                 WHERE P.ProductName = 'Tofu'
                                                 GROUP BY C.ContactName;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ContactName"]}, orders count: {reader["Count"]}, sum: {reader["PriceSum"]}");
            }

            reader.Close();
        }

        public void FrenchCustomersNonFrenchProductsLeft()
        {
            Console.WriteLine("\n> 22.*Show the list of french customers’ names who used to order non-french products (use left join).\n");
            command = connection.CreateCommand();
            command.CommandText = @"SELECT DISTINCT C.ContactName 
                                                 FROM Customers as C 
                                                 LEFT JOIN Orders AS O ON C.CustomerID = O.CustomerID 
                                                 LEFT JOIN [Order Details] AS OD ON O.OrderID = OD.OrderID 
                                                 LEFT JOIN [Products] AS P ON OD.ProductID = P.ProductID 
                                                 LEFT JOIN [Suppliers] AS S ON P.SupplierID = S.SupplierID 
                                                 WHERE S.Country <> 'France' AND C.Country = 'France'";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ContactName"]}");
            }

            reader.Close();
        }

        public void FrenchCustomersNonFrenchProducts()
        {
            Console.WriteLine("\n> 23.*Show the list of french customers’ names who used to order non-french products.\n");
            command = connection.CreateCommand();
            command.CommandText = @"select distinct Customers.ContactName 
             from Customers 
             inner join Orders  
             on Customers.CustomerID=Orders.CustomerID 
             inner join [Order Details] 
             on [Order Details].OrderID=Orders.OrderID 
             inner join Products 
             on [Order Details].ProductID = Products.ProductID
             inner join Suppliers
             on Products.SupplierID = Suppliers.SupplierID 
             where Customers.Country = 'France' AND Suppliers.Country != 'France' AND Customers.ContactName not in 
             (select distinct Customers.ContactName 
             from Customers 
             inner join Orders  
             on Customers.CustomerID=Orders.CustomerID 
             inner join [Order Details] 
             on [Order Details].OrderID=Orders.OrderID 
             inner join Products 
             on [Order Details].ProductID = Products.ProductID
             inner join Suppliers
             on Products.SupplierID = Suppliers.SupplierID 
             where Customers.Country = 'France' AND Suppliers.Country = 'France')";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ContactName"]}");
            }

            reader.Close();
        }

        public void FrenchCustomersFrenchProducts()
        {
            Console.WriteLine("\n> 24.*Show the list of french customers’ names who used to order french products.\n");
            command = connection.CreateCommand();
            command.CommandText = @"select distinct Customers.ContactName from Customers inner join Orders on Customers.CustomerID=Orders.CustomerID 
            inner join [Order Details] on [Order Details].OrderID=Orders.OrderID inner join Products on [Order Details].ProductID = Products.ProductID 
            inner join Suppliers on Products.SupplierID = Suppliers.SupplierID where Customers.Country = 'France' AND Suppliers.Country = 'France'";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ContactName"]}");
            }

            reader.Close();
        }

        public void TotalSum()
        {
            Console.WriteLine("\n> 25.*Show the total ordering sum calculated for each country of customer.\n");
            command = connection.CreateCommand();
            command.CommandText = @"SELECT Customers.Country, SUM(([Order Details].UnitPrice*[Order Details].Quantity)*(1-[Order Details].Discount)) AS sumOfOrders 
            FROM Customers 
            JOIN Orders ON Orders.CustomerID = Customers.CustomerID 
            Join [Order Details] on [Order Details].OrderID = Orders.OrderID
            GROUP BY Customers.Country;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Country"]}, sum: {reader["sumOfOrders"]}");
            }

            reader.Close();
        }

        public void TotalSumNDomestic()
        {
            Console.WriteLine("\n> 26.*Show the total ordering sums calculated for each customer’s" +
                " country for domestic and non-domestic products separately (e.g.: “France – " +
                "French products ordered – Non-french products ordered” and so on for each country).\n");
            command = connection.CreateCommand();
            command.CommandText = @"SELECT Dom.Country, Dom.DomSum, NonDom.NonDomSum 
            FROM (select Customers.Country, SUM(([Order Details].UnitPrice*[Order Details].Quantity)*(1-[Order Details].Discount)) AS DomSUM
            From Customers 
            join Orders On Customers.CustomerID = Orders.CustomerID
            join [Order Details] ON [Order Details].OrderID = Orders.OrderID
            join Products On Products.ProductID = [Order Details].ProductID
            join Suppliers On Suppliers.SupplierID = Products.SupplierID
            where Suppliers.Country = Customers.Country
            Group BY Customers.Country) As Dom
            Join (select Customers.Country, SUM(([Order Details].UnitPrice*[Order Details].Quantity)*(1-[Order Details].Discount)) AS NonDomSUM
            From Customers 
            join Orders On Customers.CustomerID = Orders.CustomerID
            join [Order Details] ON [Order Details].OrderID = Orders.OrderID
            join Products On Products.ProductID = [Order Details].ProductID
            join Suppliers On Suppliers.SupplierID = Products.SupplierID
            where Suppliers.Country <> Customers.Country
            Group BY Customers.Country) As NonDom
            On Dom.Country = NonDom.Country;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Country"]}, sum of domestic products: {reader["DomSum"]}, sum of non-domestic: {reader["NonDomSum"]}");
            }

            reader.Close();
        }

        public void ProductCategories1997()
        {
            Console.WriteLine("\n> 27.	*Show the list of product categories along with total ordering sums " +
                "calculated for the orders made for the products of each category, during the year 1997.\n");
            command = connection.CreateCommand();
            command.CommandText = @"Select Categories.CategoryName, SUM(([Order Details].UnitPrice*[Order Details].Quantity)*(1-[Order Details].Discount)) AS sumOfOrders 
            From Categories
            Join Products On Categories.CategoryID = Products.CategoryID
            join [Order Details] On [Order Details].ProductID = products.ProductID
            join Orders On Orders.OrderID = [Order Details].OrderID
            where Orders.OrderDate BETWEEN '1997-01-01' AND '1997-12-31'
            Group By Categories.CategoryName;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["CategoryName"]} {reader["sumOfOrders"]}");
            }

            reader.Close();
        }

        public void ProductsHistoryUnitPrices()
        {
            Console.WriteLine("\n> 28.*Show the list of product names along with unit prices and the history of " +
                "unit prices taken from the orders (show ‘Product name – Unit price – Historical price’)." +
                " The duplicate records should be eliminated. If no orders were made " +
                "for a certain product, then the result for this " +
                "product should look like ‘Product name – Unit price – NULL’. Sort the list by the product name.\n");
            command = connection.CreateCommand();
            command.CommandText = @"Select distinct Products.ProductName,Products.UnitPrice, [Order Details].UnitPrice As HistoricalPrice 
            From Products
            Left Join [Order Details] On Products.ProductID = [Order Details].ProductID
            Group by Products.ProductName,Products.UnitPrice,[Order Details].UnitPrice
            Order by Products.ProductName;";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ProductName"]} {reader["UnitPrice"]} {reader["HistoricalPrice"]}");
            }

            reader.Close();
        }

        public void EmployeesAndChiefs()
        {
            Console.WriteLine("\n> 29.*Show the list of employees’ names along with names of their chiefs (use left join with the same table).\n");
            command = connection.CreateCommand();
            command.CommandText = "select e.FirstName + ' ' + e.LastName as FullNameEmployee," +
                            " c.FirstName + ' '+c.LastName as FullNameChief " +
                            "from Employees as e " +
                            "left join employees as c " +
                            "on e.reportsto = c.employeeid";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FullNameEmployee"]}, chief {reader["FullNameChief"]}");
            }

            reader.Close();
        }

        public void CitiesWhereOrdersMadeTo()
        {
            Console.WriteLine("\n> 30.*Show the list of cities where employees and customers are from and where orders have been made to. Duplicates should be eliminated.\n");
            command = connection.CreateCommand();
            command.CommandText = "(select city from Customers) " +
                            "union " +
                            "(select city from Employees) " +
                            "union " +
                            "(select s.city from products as p " +
                            "inner join suppliers as s on " +
                            "p.supplierid = s.SupplierID); ";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["city"]}");
            }

            reader.Close();
        }
    }
}
