using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.DataClasses
{
    class Staff
    {
        public int id;
        public string name;
        public string surname;
        public string role;

        public Staff(int id, string name, string surname, string role)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.role = role;
        }
    }
}
