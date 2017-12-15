using makrelC.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Query
{
    public class QueryExecutor
    {
        public static QueryResult Execute(QueryDefinition query, List<Price> pattern, List<Price> set)
        {
            int steps = set.Count - pattern.Count;
            List<Match> matches = new List<Match>();
            for (int i = 0; i < steps; i++)
            {
                var toCompare = set
                    .Skip(i)
                    .Take(pattern.Count)
                    .ToList();
                var match = query.Compare(pattern, toCompare);
                if (match.IsMatch)
                {
                    matches.Add(match);
                }                
            }
            return new QueryResult(pattern, matches);
        }
    }
}
