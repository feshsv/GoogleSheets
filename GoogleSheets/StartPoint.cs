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

            var valueList = GetSheetValues.GetValueList(sheetUrl, startRow);
            var dataToLoad = GetQueryStrings.GetQuery(valueList);
            UpLoadDataToDb.UpLoad(dataToLoad, connectionString);
        }
    }
}
