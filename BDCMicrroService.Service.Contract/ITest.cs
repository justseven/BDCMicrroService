using System;
using System.Collections.Generic;
using System.Text;

namespace BDCMicrroService.Service.Contract
{
    public interface ITest:IService
    {
        int GetSum(int i, int j);
    }
}
