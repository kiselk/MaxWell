using System;
using System.Collections.Generic;
using System.Text;

namespace MaxWell.Services
{
    public interface IEulaService
    {


        bool IsAccepted();
        void Accept();
    }
}
