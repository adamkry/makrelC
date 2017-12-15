using makrelC.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Query
{
    public class QueryResult
    {
        public List<Price> Input { get; private set; }

        public List<Match> Matches { get; private set; }
 
        public QueryResult(List<Price> input, List<Match> matches)
        {
            Input = input;
            Matches = matches;
        }
    }
}
