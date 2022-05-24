using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.DataClasses;
using ServiceCenter.Database;
using ServiceCenter.GUI;

namespace ServiceCenter.Logic
{
    public class Authorization
    {
        public bool authorize(string login, string password)
        {
            DBClass db = new DBClass();
            Staff staff = db.getStaff(login, password);
            if (staff.id != 0 || staff.role != null)
            {
                if (staff.role.Equals("Менеджер"))
                {
                    MainFManager f = new(staff);
                    f.Show();
                    return true;
                }
                else if (staff.role.Equals("Мастер"))
                {
                    MainFMaster f = new(staff);
                    f.Show();
                    return true;
                }
            }
            return false;
        }
        public void authorize()
        {
            MainFClient f = new();
            f.Show();
        }
    }
}
