namespace ServiceCenter.DataClasses
{
    class Order
    {
        public int id;
        public float cost;
        public string description;
        public Device device;
        public Service[] services;

        public Order(int id, float cost, string description, Device device, Service[] services)
        {
            this.id = id;
            this.cost = cost;
            this.description = description;
            this.device = device;
            this.services = services;
        }
    }
}
