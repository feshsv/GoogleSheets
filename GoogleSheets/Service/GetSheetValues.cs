using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace GoogleSheets.Service
{    
    class GetSheetValues
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API.NET";
        
        //ЭТОТ МЕТОД ПРИНИМАЕТ АДРЕС ТАБЛИЦЫ И ДИАПАЗОН ИЗ КОТОРОГО НУЖНО ПОЛУЧИТЬ ДАННЫЕ И ВОЗВРАЩАЕТ СПИСОК ОБЪЕКТОВ
        public static IList<IList<Object>> GetValueList(string sheetURL, string startRow)
        {
            UserCredential credential;
            string rangeData = "A" + startRow + ":ZZZ"; // это диапазон запроса данных из таблицы. Если нужна вся таблица то "A1:ZZZ" Взять всё начиная со столбца А строки 1 и до максимально возможного столбца ZZZ включительно.


            // Это блок авторизации в Google таблицах. Тут есть обращение к полученному от гугла credentials.json (читай readme)
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // При первой успешной авторизации автоматически сохраняется файл token.json. В последующем авторизация не требуется. 
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Определение параметров запроса. 
            SpreadsheetsResource.ValuesResource.GetRequest requestTitle = service.Spreadsheets.Values.Get(sheetURL, "A1:ZZZ1");
            ValueRange responseTitle = requestTitle.Execute(); // получаем данные заголовка таблицы
            IList<IList<Object>> valuesAll = responseTitle.Values; // список списков в виде объектов

            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(sheetURL, rangeData);                        
            ValueRange response = request.Execute(); // получаем данные всей необходимой выборки
            IList<IList<Object>> values = response.Values; // список списков в виде объектов
            
            foreach (var onevalue in values)
            {
                valuesAll.Add(onevalue);
            }

            return valuesAll;
        }
    }
}
