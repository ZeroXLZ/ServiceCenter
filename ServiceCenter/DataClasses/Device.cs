namespace ServiceCenter.DataClasses
{
    class Device
    {
        public int id;
        public string type;
        public string model;
        public string[] components;

        public Device(int id, string type, string model, string[] components)
        {
            this.id = id;
            this.type = type;
            this.model = model;
            this.components = components;
        }
    }
}
