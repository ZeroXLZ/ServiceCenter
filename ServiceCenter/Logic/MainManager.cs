using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.DataClasses;
using ServiceCenter.Database;

namespace ServiceCenter.Logic
{
    class MainManager
    {
        public List<Application> getApps()
        {
            DBClass db = new();
            return db.getAppList();
        }
    }
}
