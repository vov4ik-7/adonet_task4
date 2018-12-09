using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace adonet_proj
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            Context context = new Context(connectionString);

            context.AllInfoID8();
            context.ListFLNameLondon();
            context.ListFLNameA();
            context.ListFLAge55();
            context.CountEmployeesLondon();
            context.MaxMinAvgAgeLondon();
            context.MaxMinAvgAgeEachCity();
            context.ListCitiesAvgAge60();
            context.EldestEmployees();
            context.ThreeEldestEmployees();
            context.AllCities();
            context.EmployeesBirthdaysThisMonth();
            context.EmployeesSnippedToMadrid();
            context.EmployeesOrders1997();
            context.EmployeesOrdersCount1997();
            context.EmployeesOrdersCountAfterRequiredDate1997();
            context.OrdersCountFrance();
            context.FrenchCustomersMoreThan1OrderGrouping();
            context.FrenchCustomersMoreThan1Order();
            context.CustomersOrderedTofu();
            context.CustomersOrderedTofuWithTotalAmountSum();
            context.FrenchCustomersNonFrenchProductsLeft();
            context.FrenchCustomersNonFrenchProducts();
            context.FrenchCustomersFrenchProducts();
            context.TotalSum();
            context.TotalSumNDomestic();
            context.ProductCategories1997();
            context.ProductsHistoryUnitPrices();
            context.EmployeesAndChiefs();
            context.CitiesWhereOrdersMadeTo();

            Console.ReadKey();
        }
    }
}
