using GoogleSheets.Service;


namespace GoogleSheets
{
    class StartPoint
    {
        static void Main(string[] args)
        {
            string sheetURL = "1z8SGeln_VLiyElvcZyqO-me1SH0J4YE_rOG7UYGN7zQ";// это адрес таблицы из URL https://docs.google.com/spreadsheets/d/1z8SGeln_VLiyElvcZyqO-me1SH0J4YE_rOG7UYGN7zQ/edit#gid=1823715284
            string connectionString = "Data Source=SOMESERVER\\SQLEXPRESS;Initial Catalog=Somedatabace;Integrated Security=True";
            string startRow = "1"; // с какой строки читать данные? (включительно)

            //List<OneLineSetFromSheet> listOfOneLineObjects = ParseValToObj.GetObjectsListLineByLine(values); // класс ParseValToObj подходит только для конкретного случая. Для загрузки данных без ограничений он не подходит (оставил для истории)
            //UpLoadToDB.UpLoad(listOfOneLineObjects, connectionString); // ввиду того, что класс ParseValToObj из Service больше не нужен, и класс UpLoadToDB из Service не актуален, как и OneLineSetFromSheet из Models (оставил для истории)

            UpLoadDataToDB.UpLoad(GetQueryStrings.GetQuery(GetSheetValues.GetValueList(sheetURL, startRow)), connectionString);
        }
    }
}
