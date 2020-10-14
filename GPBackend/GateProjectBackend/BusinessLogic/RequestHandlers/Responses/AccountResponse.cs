using GateProjectBackend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Responses
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public string AccountType { get; set; }
        public string ContactEmail { get; set; }
        public List<string> AdminEmails { get; set; }
    }
}
