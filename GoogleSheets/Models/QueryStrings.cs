using System.Collections.Generic;

namespace GoogleSheets.Models
{
    internal class QueryStrings
    {
        public string CreateTableQuery { get; }
        public List<string> InsertQuery { get; }

        public QueryStrings(string createQuery, List<string> insertQuery)
        {
            CreateTableQuery = createQuery;
            InsertQuery = insertQuery;
        }
    }
}
