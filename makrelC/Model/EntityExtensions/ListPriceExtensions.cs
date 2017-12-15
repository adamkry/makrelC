using makrelC.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Model.EntityExtensions
{
    public static class ListPriceExtensions
    {
        public static List<Price> GetPrices(this IEnumerable<Price> prices, DateTime until, int take, Company company = null)
        {
            Price last = prices.FirstOrDefault(p => p.Day.Date == until && (company == null || p.CompanyId == company.Id));
            return prices
                .Where(p => p.CompanyId == last.CompanyId 
                    && p.Day.Number <= last.Day.Number 
                    && p.Day.Number > (last.Day.Number - take))
                .OrderBy(p => p.Day.Number)
                .ToList();
        }
    }
}
