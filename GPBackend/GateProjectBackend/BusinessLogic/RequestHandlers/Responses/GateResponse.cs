using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Responses
{
    public class UserGateResponse
    {
        public string Email { get; set; }
        public bool AccessRight { get; set; }
        public bool AdminRight { get; set; }
    }
    public class GateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GateTypeName { get; set; }
        public string ServiceId { get; set; }
        public string CharacteristicId { get; set; }
        public string AccountName { get; set; }
        public bool AdminAccess { get; set; }
        public List<UserGateResponse> Users { get; set; }
    }
}
