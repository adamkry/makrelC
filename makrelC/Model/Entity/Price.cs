using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace makrelC.Model.Entity
{
    [DataContract]
    public class Price : IEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CompanyId { get; set; }

        [DataMember]
        public Day Day { get; set; }

        [DataMember]
        public decimal Open { get; set; }

        [DataMember]
        public decimal Close { get; set; }

        [DataMember]
        public decimal Max { get; set; }

        [DataMember]
        public decimal Min { get; set; }

        [DataMember]
        public decimal Volume { get; set; }

        public decimal Percent { get => Open != 0m ? (((Close - Open) * 100.0m) / Open) : 0m; }
    }
}
