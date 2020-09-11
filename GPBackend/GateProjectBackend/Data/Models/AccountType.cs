﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Models
{
    [Table("AccountTypes")]
    public class AccountType //Home = 0, Office = 1
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}