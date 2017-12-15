using makrelC.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Query
{
    public class Match
    {
        public List<Price> Prices { get; private set; } = new List<Price>();

        public double Error { get; private set; } = Double.MaxValue;

        public bool IsMatch { get; private set; }

        public Match(List<Price> prices, double error, bool isMatch)
        {
            Prices = prices;
            Error = error;
            IsMatch = isMatch;
        }
    }
}
