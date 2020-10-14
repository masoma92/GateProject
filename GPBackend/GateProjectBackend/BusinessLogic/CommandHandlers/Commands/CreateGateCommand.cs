﻿using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers.Commands
{
    public class CreateGateCommand : IRequest<Result<bool>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string GateTypeName { get; set; }
        public string AccountName { get; set; }
        public string CreatedBy { get; set; }
    }
}
