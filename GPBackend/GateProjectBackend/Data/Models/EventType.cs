﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Models
{
    [Table("EventTypes")]
    public class EventType // User = 1, Error = 2, Info = 3
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
