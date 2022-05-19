using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Logic
{
    class Confirm
    {
        public bool confirm(string operation)
        {
            if (operation.Equals("yes"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}