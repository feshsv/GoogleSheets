using GoogleSheets.Models;
using System;
using System.Collections.Generic;

namespace GoogleSheets.Service
{
    class GetQueryStrings
    {
        public static QueryStrings GetQuery(IList<IList<Object>> sheetValues)
        {
            IList<object> titles = sheetValues[0];
            sheetValues.RemoveAt(0);

            string createTableQuery = GetCreateTableQueryString(titles);
            List<string> queryList = GetInsertDataQueryString(titles, sheetValues);

            return new QueryStrings(createTableQuery, queryList);
        }


        /// <summary>
        /// Метод подготавливает Query строку запроса в SQL для создания таблицы
        /// </summary>
        /// <param name="titles"></param> это первая строка в excel. обычно там названия столбцов.
        /// <returns></returns>
        private static string GetCreateTableQueryString(IList<Object> titles)
        {
            string createTableQuery = "CREATE TABLE DataFromGoogleSheet(";

            foreach (var title in titles)
            {
                createTableQuery += "[" + title + "]" + " NVARCHAR(MAX), ";
            }

            createTableQuery = createTableQuery.Trim(new char[] { ',', ' ' });
            createTableQuery += ")";

            //string createTableQuery = "CREATE TABLE DataFromGoogleSheet([Название] NVARCHAR(MAX), [Промокод] NVARCHAR(MAX), [адрес] NVARCHAR(MAX), [скидка] NVARCHAR(MAX), [Тип] NVARCHAR(MAX), [Краткое описание ] NVARCHAR(MAX), [Лучшее предложение] NVARCHAR(MAX), [Контактный номер телефона] NVARCHAR(MAX), [Часы работы] NVARCHAR(MAX), [Координаты широта] NVARCHAR(MAX), [Координаты долгота] NVARCHAR(MAX))";

            return createTableQuery;
        }


        /// <summary>
        /// Метод собирает в список SQL INSERT QUERY строки по 1000 строк и в конце суёт остатки. MS SQL больше 1000 за раз не ест.
        /// </summary>
        /// <param name="titles"></param> это первая строка в excel. обычно там названия столбцов.
        /// <param name="sheetValues"></param> это все оставшиеся строки с данными
        /// <returns></returns>
        private static List<string> GetInsertDataQueryString(IList<Object> titles, IList<IList<Object>> sheetValues)
        {
            List<string> eachThousandRows = new List<string>();
            int count = 0;
            string createTableQuery = "";

            while (sheetValues.Count > 0)
            {
                if (count > 999)
                {
                    createTableQuery = createTableQuery.Trim(new char[] { ',', '(' });
                    eachThousandRows.Add(createTableQuery);
                    createTableQuery = "";
                    count = 0;
                }

                if (count == 0)
                {
                    createTableQuery += "INSERT INTO DataFromGoogleSheet(";
                    foreach (var title in titles)
                    {
                        createTableQuery += "[" + title + "], ";
                    }
                    createTableQuery = createTableQuery.Trim(new char[] { ',', ' ' });
                    createTableQuery += ") VALUES (";
                }

                foreach (string onecell in sheetValues[0])
                {
                    string temponecell = onecell.Replace("'", " ");
                    createTableQuery += "'" + temponecell + "', ";
                }
                createTableQuery = createTableQuery.Trim(new char[] { ',', ' ' });
                createTableQuery += "),(";
                createTableQuery = createTableQuery.Replace("(),", "");

                sheetValues.RemoveAt(0);
                count++;
            }

            createTableQuery = createTableQuery.Trim(new char[] { ',', '(' });
            eachThousandRows.Add(createTableQuery);

            return eachThousandRows;
        }
    }
}
