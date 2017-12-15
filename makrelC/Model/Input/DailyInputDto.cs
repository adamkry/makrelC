using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Model.Input
{
    public class DailyInputDto
    {
        public string CoName { get; set; }

        public DateTime Day { get; set; }

        public decimal Open { get; set; }

        public decimal Close { get; set; }

        public decimal Min { get; set; }

        public decimal Max { get; set; }

        public decimal Volume { get; set; }
    }
}
