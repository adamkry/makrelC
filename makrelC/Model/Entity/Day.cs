using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Model.Entity
{
    public class Day : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
    }
}
