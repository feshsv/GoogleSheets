using GoogleSheets.Models;
using System;
using System.Data.SqlClient;

namespace GoogleSheets.Service
{
    class UpLoadDataToDB
    {
        public static void UpLoad(QueryStrings query, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConnect = new SqlConnection(connectionString))
                {
                    sqlConnect.Open();
                    SqlCommand sqlCommandToCreateTable = new SqlCommand(query.createTableQuery, sqlConnect);
                    sqlCommandToCreateTable.ExecuteNonQuery();

                    foreach (var onequery in query.insertQuery)
                    {
                       SqlCommand sqlCsqlCommandToInsertData = new SqlCommand(onequery, sqlConnect);
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
