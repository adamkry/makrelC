using makrelC.Data;
using makrelC.Model.Entity;
using makrelC.Model.EntityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Query
{
    public class QueryScenario
    {
        private List<Price> pattern = new List<Price>();
        private QueryDefinition query = null;
        protected Repository Repository { get; set; } = new Repository();

        public QueryScenario(QueryDefinition query, Company company, DateTime endDate, int length)
        {
            this.query = query;
            pattern = Repository
                .FindAll<Price>()
                .GetPrices(until: endDate, take: length, company: company);
        }

        public void Run(Func<Price, bool> filter = null)
        {
            var all = Repository
                .FindAll(filter);

            foreach (var fromCompany in all.GroupBy(p => p.CompanyId))
            {
                QueryExecutor.Execute(query, pattern, fromCompany.ToList());
            }
        }
    }
}
