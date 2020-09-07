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
        public string Message { get; set; }
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
    }
}
