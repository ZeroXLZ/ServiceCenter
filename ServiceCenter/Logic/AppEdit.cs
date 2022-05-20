using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.DataClasses;
using ServiceCenter.Database;

namespace ServiceCenter.Logic
{
    class AppEdit
    {
        public void editApp(Application app)
        {
            DBClass db = new DBClass();
            db.editApp(app);
        }
    }
}
