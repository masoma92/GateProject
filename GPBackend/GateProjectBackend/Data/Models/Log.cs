using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Models
{
    [Table("Logs")]
    public class Log : ModelBase
    {
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? AccountId { get; set; }
        public Account Account { get; set; }
        public int? GateId { get; set; }
        public Gate Gate { get; set; }
        public string Action { get; set; }
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
    }
}
