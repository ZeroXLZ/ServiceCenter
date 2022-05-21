using System;

namespace ServiceCenter.DataClasses
{
    public class Application
    {
        public int id;
        public DateTime date;
        public Client client;
        public string status;
        public Staff master;
        public Order order;

        public Application(int id, DateTime date, Client client, string status, Staff master, Order order)
        {
            this.id = id;
            this.date = date;
            this.client = client;
            this.status = status;
            this.master = master;
            this.order = order;
        }

        public Application()
        {

        }
    }
}
