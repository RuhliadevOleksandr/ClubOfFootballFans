using System;
using System.Data.Common;
using System.Configuration;

namespace FootballFans
{
	internal static class DataFromDB
	{
		public static void Connect()
		{
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    Console.WriteLine("\nCan't connect to database!");
                    return;
                }
                connection.ConnectionString = connectionString;
                connection.Open();
            }
        }
	}
}
