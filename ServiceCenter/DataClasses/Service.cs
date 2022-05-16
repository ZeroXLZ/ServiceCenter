namespace ServiceCenter.DataClasses
{
    class Service
    {
        public int id;
        public string description;
        public float price;

        public Service(int id, string description, float price)
        {
            this.id = id;
            this.description = description;
            this.price = price;
        }
    }
}
