using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.DataClasses;
using ServiceCenter.Database;

namespace ServiceCenter.Logic
{
    class OrderInfo
    {
        public void takeOrder(Application app, Staff master)
        {
            DBClass db = new DBClass();
            app.master = master;
            db.editApp(app);
        }
    }
}
