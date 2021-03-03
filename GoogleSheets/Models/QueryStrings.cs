using System.Collections.Generic;

namespace GoogleSheets.Models
{
    class QueryStrings
    {
        public string createTableQuery { get; set; }
        public List<string> insertQuery { get; set; }

        public QueryStrings(string createQuery, List<string> insertQuery)
        {
            this.createTableQuery = createQuery;
            this.insertQuery = insertQuery;
        }
    }
}
