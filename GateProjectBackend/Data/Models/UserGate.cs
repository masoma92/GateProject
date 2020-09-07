using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Models
{
    [Table("UserGates")]
    public class UserGate : ModelBase
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int GateId { get; set; }
        public Gate Gate { get; set; }
        public bool AccessRight { get; set; }
        public bool AdminRight { get; set; }
    }
}
