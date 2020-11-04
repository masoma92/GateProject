using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Models
{
    [Table("Gates")]
    public class Gate : ModelBase
    {
        public string Name { get; set; }
        public int GateTypeId { get; set; }
        public GateType GateType { get; set; }
        public string ServiceId { get; set; }
        public string CharacteristicId { get; set; }
        public int? AccountId { get; set; }
        public Account Account { get; set; }
        public List<UserGate> Users { get; set; }
        public List<Log> Logs { get; set; }
    }
}
