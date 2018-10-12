using System;
using System.Collections.Generic;
using System.Text;

namespace BDCMicrroService.Service.Contract
{
    public interface ICreateFlowService: IService
    {
        string GetName(string name);
    }
}
