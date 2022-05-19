namespace ServiceCenter.DataClasses
{
    class Service
    {
        public int id;
        public string name;
        public string description;
        public float price;

        public Service(int id, string name, string description, float price)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
        }
    }
}
