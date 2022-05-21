using System.Collections.Generic;

namespace ServiceCenter.DataClasses
{
    public class Device
    {
        public int id;
        public string type;
        public string model;
        public List<string> components;

        public Device(int id, string type, string model, List<string> components)
        {
            this.id = id;
            this.type = type;
            this.model = model;
            this.components = components;
        }

        public Device()
        {

        }
    }
}
