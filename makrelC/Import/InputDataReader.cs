using makrelC.Model.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Import
{
    public class InputDataReader
    {
        public static List<DailyInputDto> ReadFromFolder(string folderName)
        {
            List<DailyInputDto> result = new List<DailyInputDto>();
            if (Directory.Exists(folderName))
            {
                var files = Directory.GetFiles(folderName);
                foreach (var file in files)
                {
                    result.AddRange(ReadFromFile(file));
                }
            }
            return result;
        }

        private static List<DailyInputDto> ReadFromFile(string fileName)
        {
            List<DailyInputDto> result = new List<DailyInputDto>();
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line = String.Empty;
            while ((line = file.ReadLine()) != null)
            {
                result.Add(ParseDailyInput(line));
            }
            file.Close();
            return result;
        }

        private static DailyInputDto ParseDailyInput(string line)
        {
            var values = line.Split(new char[] { ',' });
            var result = new DailyInputDto();
            result.CoName = values[0];
            result.Day = DateTime.ParseExact(values[1], "yyyyMMdd", CultureInfo.InvariantCulture);
            result.Open = Decimal.Parse(values[2]);
            result.Max = Decimal.Parse(values[3]);
            result.Min = Decimal.Parse(values[4]);
            result.Close = Decimal.Parse(values[5]);
            result.Volume = Decimal.Parse(values[6]);
            return result;
        }
    }
}
