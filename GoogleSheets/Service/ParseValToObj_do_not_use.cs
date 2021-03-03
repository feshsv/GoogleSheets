using GoogleSheets.Models;
using System;
using System.Collections.Generic;

namespace GoogleSheets.Service
{
    /// <summary>
    /// ЭТОТ КЛАСС НИКАК НЕ УЧАСТВУЕТ В ЭТОЙ РЕАЛИЗАЦИИ. ОСТАВИЛ ДЛЯ ИСТОРИИ.
    /// </summary>
    class ParseValToObj_do_not_use
    {
        public static List<OneLineSetFromSheet_do_not_use> GetObjectsListLineByLine(IList<IList<Object>> sheetValues)
        {
            List<OneLineSetFromSheet_do_not_use> listOfOneLineObjects = new List<OneLineSetFromSheet_do_not_use>();

            if (sheetValues != null && sheetValues.Count > 0)
            {
                foreach (var row in sheetValues)
                {
                    listOfOneLineObjects.Add(new OneLineSetFromSheet_do_not_use(row[0].ToString(), row[1].ToString(), row[2].ToString()
                        , row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString()
                        , row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString()));
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            return listOfOneLineObjects;
        }
    }
}
