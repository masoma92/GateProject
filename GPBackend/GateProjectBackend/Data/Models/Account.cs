using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Models
{
    [Table("Accounts")]
    public class Account : ModelBase
    {
        public string Name { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
        public string ContactEmail { get; set; }
        public List<Gate> Gates { get; set; }
        public List<AccountAdmin> Admins { get; set; }
        public List<AccountUser> Users { get; set; }
        public List<Log> Logs { get; set; }
    }
}
