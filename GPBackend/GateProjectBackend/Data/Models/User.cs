using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Models
{
    [Table("Users")]
    public class User : ModelBase
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        public string RfidKey { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Log> Logs { get; set; }
        public List<UserGate> Gates { get; set; }
        public List<AccountAdmin> AdminAccounts { get; set; }
        public List<AccountUser> UserAccounts { get; set; }
    }
}
