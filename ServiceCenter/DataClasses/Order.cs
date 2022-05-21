using System.Collections.Generic;

namespace ServiceCenter.DataClasses
{
    public class Order
    {
        public int id;
        public float cost;
        public string description;
        public Device device;
        public List<Service> services;

        public Order(int id, float cost, string description, Device device, List<Service> services)
        {
            this.id = id;
            this.cost = cost;
            this.description = description;
            this.device = device;
            this.services = services;
        }

        public Order()
        {

        }
    }
}
