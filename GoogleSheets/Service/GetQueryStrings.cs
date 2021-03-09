using GoogleSheets.Models;
using System.Collections.Generic;
using System.Linq;

namespace GoogleSheets.Service
{
    internal static class GetQueryStrings
    {

        /// <summary>
        /// Генерит insert query для загрузки в SQL
        /// </summary>
        /// <param name="sheetValues">данные таблицы</param>
        /// <returns>QueryStrings - команды SQL для загрузки данныех в БД</returns>
        public static QueryStrings GetQuery(IList<IList<object>> sheetValues)
        {
            var titles = sheetValues[0];
            sheetValues.RemoveAt(0);

            var createTableQuery = GetCreateTableQueryString(titles);
            var queryList = GetInsertDataQueryString(titles, sheetValues);
            return new QueryStrings(createTableQuery, queryList);
        }


        /// <summary>
        /// Метод подготавливает Query строку запроса в SQL для создания таблицы
        /// </summary>
        /// <param name="titles">первая строка в excel. обычно там названия столбцов.</param>
        /// <returns>Строка создания таблицы в БД</returns>
        private static string GetCreateTableQueryString(IEnumerable<object> titles)
        {
            var createTableQuery = titles.Aggregate("CREATE TABLE DataFromGoogleSheet(", (current, title) => current + ("[" + title + "]" + " NVARCHAR(MAX), "));

            createTableQuery = createTableQuery.Trim(',', ' ');
            createTableQuery += ")";

            return createTableQuery;
        }


        /// <summary>
        /// Метод собирает в список SQL INSERT QUERY строки по 1000 строк и в конце суёт остатки. MS SQL больше 1000 за раз не ест.
        /// </summary>
        /// <param name="titles">первая строка в excel. обычно там названия столбцов.</param>
        /// <param name="sheetValues">все оставшиеся строки с данными</param>
        /// <returns>Строки вставки данных в таблицу БД</returns>
        private static List<string> GetInsertDataQueryString(IList<object> titles, IList<IList<object>> sheetValues)
        {
            var eachThousandRows = new List<string>();
            var count = 0;
            var createTableQuery = "";

            while (sheetValues.Count > 0)
            {
                if (count > 999)
                {
                    createTableQuery = createTableQuery.Trim(',', '(');
                    eachThousandRows.Add(createTableQuery);
                    createTableQuery = "";
                    count = 0;
                }

                if (count == 0)
                {
                    createTableQuery += "INSERT INTO DataFromGoogleSheet(";
                    createTableQuery = titles.Aggregate(createTableQuery, (current, title) => current + ("[" + title + "], "));

                    createTableQuery = createTableQuery.Trim(',', ' ');
                    createTableQuery += ") VALUES (";
                }

                createTableQuery = (from string onecell in sheetValues[0] select onecell.Replace("'", " ")).Aggregate(createTableQuery, (current, temponecell) => current + ("'" + temponecell + "', "));

                createTableQuery = createTableQuery.Trim(',', ' ');
                createTableQuery += "),(";
                createTableQuery = createTableQuery.Replace("(),", "");

                sheetValues.RemoveAt(0);
                count++;
            }

            createTableQuery = createTableQuery.Trim(',', '(');
            eachThousandRows.Add(createTableQuery);

            return eachThousandRows;
        }
    }
}
