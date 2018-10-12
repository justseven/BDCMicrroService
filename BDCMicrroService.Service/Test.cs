using BDCMicrroService.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDCMicrroService.Service
{
    public class Test : ITest
    {
        public int GetSum(int i, int j)
        {
            return i + j;
        }
    }
}
