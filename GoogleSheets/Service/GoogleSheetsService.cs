using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;

namespace GoogleSheets.Service
{
    internal static class GoogleSheetsService
    {
        private static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private const string ApplicationName = "Google Sheets API.NET";
        private static readonly SheetsService Service = GetGoogleService();

        
        /// <summary>
        /// Получает данные из таблицы GoogleSheets в виде списка списков обьектов object, которые представляют данные таблицы.
        /// </summary>
        /// <param name="sheetUrl">АДРЕС GOOGLE ТАБЛИЦЫ</param>
        /// <param name="startRow">ДИАПАЗОН ИЗ КОТОРОГО НУЖНО ПОЛУЧИТЬ ДАННЫЕ</param>
        /// <returns>Список списков объектов object, которые представляют данные таблицы</returns>
        public static IList<IList<object>> GetValueList(string sheetUrl, string startRow)
        {
            var values = GetValues(sheetUrl, startRow);
            if ("1".Equals(startRow))
            {
                return values;
            }

            var title = GetTitle(sheetUrl);

            foreach (var value in values)
            {
                title.Add(value);
            }
            return title;
        }
        
        
        /// <summary>
        /// Авторизация в Google таблицах. Тут есть обращение к полученному от гугла credentials.json (читай readme)
        /// При первой успешной авторизации автоматически сохраняется файл token.json. В последующем авторизация не требуется.
        /// </summary>
        /// <returns>GoogleSheetsService - сервис работы с гугл таблицами, необходимый для доступа к ним.</returns>
        private static SheetsService GetGoogleService()
        {
            UserCredential credential;
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            { 
                const string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }
        
        
        /// <summary>
        /// Получает первую строку. В ней как правило названия столбцов.
        /// </summary>
        /// <param name="sheetUrl"></param>
        /// <returns>Список списков объектов object, которые представляют данные таблицы</returns>
        private static IList<IList<object>> GetTitle(string sheetUrl)
        {
            var requestTitle = Service.Spreadsheets.Values.Get(sheetUrl, "A1:ZZZ1");
            var responseTitle = requestTitle.Execute();
            var titleList = responseTitle.Values;
            return titleList;
        }


        /// <summary>
        /// Получает все данные заданного диапазона.
        /// </summary>
        /// <param name="sheetUrl"></param>
        /// <param name="start"></param>
        /// <returns>Список списков объектов object, которые представляют данные таблицы</returns>
        private static IList<IList<object>> GetValues(string sheetUrl, string start)
        {
            var rangeData = "A" + start + ":ZZZ"; // Если нужна вся таблица то "A1:ZZZ"
            var request = Service.Spreadsheets.Values.Get(sheetUrl, rangeData);
            var response = request.Execute();
            var values = response.Values;
            return values;
        }
    }
}
