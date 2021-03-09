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
            try
            {
                using (var sqlConnect = new SqlConnection(connectionString))
                {
                    sqlConnect.Open();
                    var sqlCommandToCreateTable = new SqlCommand(query.CreateTableQuery, sqlConnect);
                    sqlCommandToCreateTable.ExecuteNonQuery();

                    foreach (var sqlCsqlCommandToInsertData in query.InsertQuery.Select(onequery => new SqlCommand(onequery, sqlConnect)))
                    {
                        sqlCsqlCommandToInsertData.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("\n******************* ALL DATA LOADED INTO DATABASE SUCCESSFULLY *******************\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n=================== Something wrong in SQL connection and upload block ===================\n\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
