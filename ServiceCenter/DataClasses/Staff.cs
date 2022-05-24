using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.DataClasses
{
    public class Staff
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string role { get; set; }

        public Staff(int id, string name, string surname, string role)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.role = role;
        }

        public Staff()
        {

        }

        public override bool Equals(object other)
        {
            var toCompareWith = other as Staff;
            if (toCompareWith == null)
                return false;
            return this.id == toCompareWith.id &&
                this.name == toCompareWith.name &&
                this.surname == toCompareWith.surname &&
                this.role == toCompareWith.role;
        }
    }
}
