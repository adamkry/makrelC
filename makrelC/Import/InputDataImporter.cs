using makrelC.Data;
using makrelC.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using makrelC.Model.Input;

namespace makrelC.Import
{
    public class InputDataImporter
    {
        public Repository Repository = new Repository();

        public ImportHistory RunImport(string folderName)
        {
            var import = new ImportHistory
            {
                PerformedDate = DateTime.Now,
                FolderName = folderName
            };
            var dailyInputs = InputDataReader.ReadFromFolder(folderName);
            import.DateFrom = dailyInputs.Min(di => di.Day);
            import.DateTo = dailyInputs.Max(di => di.Day);

            ImportDays(dailyInputs);
            ImportCompanies(dailyInputs);
            ImportPrices(dailyInputs);

            Repository.Insert(import);
            return import;
        }

        private void ImportDays(List<DailyInputDto> dailyInputs)
        {
            //Get days
            var days = dailyInputs
                .Select(di => di.Day)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            //Get db days
            var dbDays = Repository
                .FindAll<Day>()
                .ToList();

            //Select new days
            var newDays = days
                .Where(d => !dbDays.Any(dbd => dbd.Date == d))
                .Select(d => new Day { Date = d })
                .ToList();
            Repository.InsertAll(newDays);

            //Get all
            dbDays = Repository
                .FindAll<Day>()
                .ToList();

            //Update numbering
            int no = 1;
            foreach (var day in dbDays.OrderBy(d => d.Date))
            {
                day.Number = no++;
            }

            //Save changes
            Repository.UpdateAll<Day>();
        }

        private void ImportPrices(List<DailyInputDto> dailyInputs)
        {
            foreach (var coPrices in dailyInputs.GroupBy(di => di.CoName))
            {
                var company = ImportCompany(coPrices.Key);
                var prices = coPrices
                    .Select(p => GetPriceFromInput(company.Id, p))
                    .ToList();
                Repository.InsertAll(prices);
            }
        }

        private Price GetPriceFromInput(int companyId, DailyInputDto p)
        {
            return new Price
            {
                CompanyId = companyId,
                Day = Repository.Find<Day>(d => d.Date == p.Day),
                Close = p.Close,
                Max = p.Max,
                Min = p.Min,
                Open = p.Open,
                Volume = p.Volume
            };
        }

        private Company ImportCompany(string coName)
        {
            var company = Repository
                    .Find<Company>(c => c.Name == coName);
            if (company == null)
            {
                company = new Company
                {
                    Name = coName
                };
                Repository.Insert(company);
            }
            return company;
        }

        private void ImportCompanies(List<DailyInputDto> dailyInputs)
        {
            var coNames = dailyInputs.Select(di => di.CoName).Distinct();
            var dbNames = Repository.FindAll<Company>().Select(c => c.Name).ToList();
            var toCreate = coNames
                .Where(n => !dbNames.Any(dbn => dbn == n))
                .Select(cn => new Company { Name = cn })
                .ToList();
            if (toCreate.Count > 0)
            {
                Repository.InsertAll(toCreate);
            }
        }
    }
}
