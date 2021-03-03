using Dapper;
using GoogleSheets.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GoogleSheets.Service
{
    /// <summary>
    /// ЭТОТ КЛАСС НИКАК НЕ УЧАСТВУЕТ В ЭТОЙ РЕАЛИЗАЦИИ. ОСТАВИЛ ДЛЯ ИСТОРИИ.
    /// </summary>
    class UpLoadToDB_do_not_use
    {
        public static void UpLoad(List<OneLineSetFromSheet_do_not_use> inputList, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConnect = new SqlConnection(connectionString))
                {
                    sqlConnect.Open();
                    foreach (var oneLine in inputList)
                    {
                        using (var sqlTransaction = sqlConnect.BeginTransaction())
                        {
                            sqlConnect.Execute(oneLine.InsertQuery, oneLine, sqlTransaction);
                            sqlTransaction.Commit();
                        }
                    }
                }
                Console.WriteLine("\n******************* ALL DATA LOADED INTO DATABASE SUCCESSFULLY *******************\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n=================== Something wrong in SQL connection and upload block ===================\n\n" + ex.Message + "\n");
            }
        }
    }
}
