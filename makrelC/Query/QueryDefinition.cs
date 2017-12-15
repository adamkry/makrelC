using makrelC.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Query
{
    public class QueryDefinition
    {
        private int _minLength;

        private double _maxError;

        public QueryDefinition(int minLength, double maxError)
        {
            _minLength = minLength;
            _maxError = maxError;
        }

        public Match Compare(List<Price> pricesA, List<Price> pricesB)
        {
            double error = 0.0;            
            List<Price> matches = new List<Price>();
            for (int i = 0; i < pricesA.Count; i++)
            {
                error += ComparePrices(pricesA[i], pricesB[i]);
                if (error > _maxError)
                {
                    break;
                }
                matches.Add(pricesB[i]);
            }
            return new Match(matches, error, matches.Count >= _minLength);
        }

        protected double ComparePrices(Price priceA, Price priceB)
        {
            return Math.Pow(Math.Abs((Math.Pow((double)priceA.Percent, 2) - Math.Pow((double)priceB.Percent, 2))), 0.5);
        }
    }
}
