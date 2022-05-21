namespace ServiceCenter.DataClasses
{
    public class Client
    {
        public int id;
        public string name;
        public string surname;
        public string patronymic;
        public string phoneNum;
        public string passport;

        public Client(int id, string name, string surname, string patronymic, string phoneNum, string passport)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.patronymic = patronymic;
            this.phoneNum = phoneNum;
            this.passport = passport;
        }

        public Client()
        {

        }
    }
}
