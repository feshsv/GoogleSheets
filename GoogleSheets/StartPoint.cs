using System;
using GoogleSheets.Service;


namespace GoogleSheets
{
    internal static class StartPoint
    {
        static void Main()
        {
            const string sheetUrl = "1NGbnQWozsCfE90Kq45dD83E7SApfG9EFNcVSYXdnNTY"; // это адрес таблицы из URL https://docs.google.com/spreadsheets/d/1z8SGeln_VLiyElvcZyqO-me1SJ3J4YE_rOG7UYGN7zQ/edit#gid=1823715284
            const string connectionString = "Data Source=MSK-FESHUKOVSV\\SQLEXPRESS;Initial Catalog=Autoservice;Integrated Security=True";
            const string startRow = "1"; // с какой строки читать данные? (включительно)

            try
            {
                UpLoadDataToDb.UpLoad(QueryStringsUtils.GetQuery(GoogleSheetsService.GetValueList(sheetUrl, startRow)), connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n\n" + e.StackTrace);
            }
        }
    }
}
