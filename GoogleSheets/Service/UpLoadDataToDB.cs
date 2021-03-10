using GoogleSheets.Models;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace GoogleSheets.Service
{
    internal static class UpLoadDataToDb
    {
        /// <summary>
        /// Грузит данные в БД.
        /// </summary>
        /// <param name="query">Объект со строками SQL запросов</param>
        /// <param name="connectionString"></param>
        public static void UpLoad(QueryStrings query, string connectionString)
        {
            using (var sqlConnect = new SqlConnection(connectionString))
            {
                sqlConnect.Open();
                var sqlCommandToCreateTable = new SqlCommand(query.CreateTableQuery, sqlConnect);
                sqlCommandToCreateTable.ExecuteNonQuery();

                foreach (var sqlMsqlCommandToInsertData in query.InsertQuery.Select(rquery => new SqlCommand(rquery, sqlConnect)))
                {
                    sqlMsqlCommandToInsertData.ExecuteNonQuery();
                }
            }
            Console.WriteLine("\n******************* ALL DATA LOADED INTO DATABASE SUCCESSFULLY *******************\n\n");
        }
    }
}
