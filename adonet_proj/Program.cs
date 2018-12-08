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

            Console.ReadKey();
        }
    }
}
