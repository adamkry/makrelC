using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Model.Entity
{
    public class ImportHistory : IEntity
    {
        public int Id { get; set; }
        public DateTime PerformedDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string FolderName { get; set; }
    }
}
