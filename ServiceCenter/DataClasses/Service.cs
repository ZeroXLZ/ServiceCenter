namespace ServiceCenter.DataClasses
{
    public class Service
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }

        public Service(int id, string name, string description, float price)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
        }

        public Service()
        {

        }
    }
}
