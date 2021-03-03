Программа забирает таблицу произвольного размера с GoogleSheets, создаёт таблицу в указанной БД SQL и загружает в неё данные.

Как юзать:
1. https://developers.google.com/sheets/api/quickstart/dotnet войти по ссылке, нажать на кнопку и получить json. Там нужна авторизация в гугле. Грузится credentials.json
2. Установить google apis sheets в visual studio NuGet Package Manager Console, если с проектом не зайдёт.
3. Положить credentials.json в папку ...(папка приложения)\bin\Debug
4. В строке 10 StartPoint указать таблицу GoogleSheets sheetURL
5. В строке 11 StartPoint указать connection string для соединения с базой данных SQL
6. В строке 12 StartPoint указать с какой строки начинать читать таблицу
7. Название создаваемой таблицы можно поменять в строке 28 GoogleSheets.Service.UpLoadDataToDB 
string createTableQuery = "CREATE TABLE DataFromGoogleSheet"
