using System;
using System.Collections.Generic;
using System.Text;
using BDCMicrroService.Service.Contract;

namespace BDCMicrroService.Service
{
    public class CreateFlowService : ICreateFlowService
    {
        public string GetName(string name)
        {
            return name;
        }
    }
}
