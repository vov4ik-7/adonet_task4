using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adonet_proj;
using Moq;
using System.Configuration;
using System.Data.SqlClient;


namespace UnitTest
{
    /// <summary>
    /// Unit tests
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Calculate strings
        /// </summary>
        /// <param name="l"> list of strings </param>
        /// <returns> number of strings in list </returns>
        public int Calculate(List<string[]> l)
        {
            return l.Count;
        }
        //імітуємо те, ніби ми знаємо правильний результат нашого запиту, і знаємо, що поверне нам наш запит і так з усіма тестами

        /// <summary>
        /// First test
        /// </summary>
        private string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Northwind; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
        [TestMethod]
        public void TestCount18()
        {
            List<string[]> list = new List<string[]>();
            list.Add(new string[] { "Annette Roulet" });
            list.Add(new string[] { "Carine Schmitt" });
            list.Add(new string[] { "Daniel Tonini" });
            list.Add(new string[] { "Dominique Perrier" });
            list.Add(new string[] { "Frederique Citeaux" });
            list.Add(new string[] { "Janine Labrune" });
            list.Add(new string[] { "Laurence Lebihan" });
            list.Add(new string[] { "Martine Rance" });
            list.Add(new string[] { "Mary Saveley" });
            list.Add(new string[] { "Paul Henriot" });

            var context = new Context(connectionString);
            var contextMock = new Mock<Context>();
            contextMock.Setup(c => c.FrenchCustomersMoreThan1OrderGrouping()).Returns(list);

            var forTest = contextMock.Object;

            Assert.AreEqual<int>(this.Calculate(contextMock.Object.FrenchCustomersMoreThan1OrderGrouping()), 10);
        }

        [TestMethod]
        public void TestData18()
        {
            List<string[]> list = new List<string[]>();
            list.Add(new string[] { "Annette Roulet" });
            list.Add(new string[] { "Carine Schmitt" });
            list.Add(new string[] { "Daniel Tonini" });
            list.Add(new string[] { "Dominique Perrier" });
            list.Add(new string[] { "Frederique Citeaux" });
            list.Add(new string[] { "Janine Labrune" });
            list.Add(new string[] { "Laurence Lebihan" });
            list.Add(new string[] { "Martine Rance" });
            list.Add(new string[] { "Mary Saveley" });
            list.Add(new string[] { "Paul Henriot" });

            string query18 = @"SELECT C.ContactName 
                                                FROM Customers AS C 
                                                JOIN Orders AS O ON O.CustomerID = C.CustomerID
                                                WHERE C.Country = 'France'
                                                GROUP BY C.ContactName       
                                                HAVING COUNT (O.CustomerID) > 1;";
            var context = new Context(connectionString);
            var contextMock = new Mock<Context>();
            contextMock.Setup(c => c.FrenchCustomersMoreThan1OrderGrouping()).Returns(list);

            var forTest = contextMock.Object;

            Assert.AreEqual<string>(contextMock.Object.FrenchCustomersMoreThan1OrderGrouping()[9][0], "Paul Henriot");
        }
    }
}
