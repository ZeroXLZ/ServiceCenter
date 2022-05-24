using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.DataClasses;
using System.Data;
using System.Diagnostics;

namespace ServiceCenter.Database
{
    public class DBClass
    {
        public SqliteConnection conn;
        public SqliteCommand cmd;
        public SqliteDataReader reader;

        public void connect()
        {
            string path = "Data Source=.\\DB\\services_db.db";
            //conn = new SqliteConnection("Data Source=D:\\Systemf\\Desktop\\Course work\\DB\\services_db.db");
            conn = new SqliteConnection(path);
            conn.Open();
        }
        public void close()
        {
            if (reader != null)
            {
                if (!reader.IsClosed)
                    reader.Close();

                if (!conn.State.ToString().Equals("Closed"))
                    conn.Close();
            }
        }

        public Staff getStaff(string login, string password)
        {
            Staff staff = new Staff();
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, Name, Surname, Role FROM Staff " +
                $"WHERE Email = '{login}' AND Password = '{password}'";
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    staff.id = int.Parse(reader.GetValue(0).ToString());
                    staff.name = reader.GetValue(1).ToString();
                    staff.surname = reader.GetValue(2).ToString();
                    staff.role = reader.GetValue(3).ToString();
                }
            }

            close();
            return staff;
        }

        public Staff getStaff(int id)
        {
            Staff staff = new Staff(id, "", "", "");
            SqliteDataReader reader2;
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, Name, Surname, Role FROM Staff WHERE id = " + id;
            reader2 = cmd.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    staff.name = reader2.GetValue(1).ToString();
                    staff.surname = reader2.GetValue(2).ToString();
                    staff.role = reader2.GetValue(3).ToString();
                }
            }

            reader2.Close();
            conn.Close();
            return staff;
        }

        public List<Staff> getMasterList()
        {
            List<Staff> masters = new List<Staff>();
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Staff WHERE role = 'Мастер'";
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = int.Parse(reader.GetValue(0).ToString());
                    string name = reader.GetValue(1).ToString();
                    string surname = reader.GetValue(2).ToString();
                    string role = reader.GetValue(3).ToString();
                    masters.Add(new Staff(id, name, surname, role));
                }
            }
            close();
            return masters;
        }

        public bool checkMaster(Staff master)
        {
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Applications WHERE id_master = {master.id} AND status <> 'Отказана' AND status <> 'Закрыта'";
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                close();
                return true;
            }
            else
            {

            }
            close();
            return false;
        }

        public Service getService(int id)
        {
            Service service = new Service(id, "", "", 0);
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Services WHERE id = " + id;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    service.name = reader.GetValue(1).ToString();
                    service.description = reader.GetValue(2).ToString();
                    service.price = float.Parse(reader.GetValue(3).ToString());
                }
            }
            close();
            return service;
        }

        public List<Service> getServicesOfOrder(int id)
        {
            List<Service> services = new List<Service>();
            SqliteDataReader reader2;
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Services.id, Name, Description, Price FROM AppServices INNER JOIN " +
                "Services ON AppServices.id_service = Services.id " +
                "WHERE id_order = " + id;
            reader2 = cmd.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    Service service = new Service(0, "", "", 0);
                    service.id = int.Parse(reader2.GetValue(0).ToString());
                    service.name = reader2.GetValue(1).ToString();
                    service.description = reader2.GetValue(2).ToString();
                    service.price = float.Parse(reader2.GetValue(3).ToString());
                    services.Add(service);
                }
            }
            reader2.Close();
            conn.Close();

            return services;
        }

        public List<Service> getServiceList()
        {
            List<Service> services = new List<Service>();
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Services";
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Service s = new Service();
                    s.id = int.Parse(reader.GetValue(0).ToString());
                    s.name = reader.GetValue(1).ToString();
                    s.description = reader.GetValue(2).ToString();
                    s.price = float.Parse(reader.GetValue(3).ToString());
                    services.Add(s);
                }
            }
            close();
            return services;
        }

        public Client getClient(int id)
        {
            Client client = new Client(id, "", "", "", "", "");
            SqliteDataReader reader2;
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Clients WHERE id = " + id;
            reader2 = cmd.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    client.name = reader2.GetValue(1).ToString();
                    client.surname = reader2.GetValue(2).ToString();
                    client.patronymic = reader2.GetValue(3).ToString();
                    client.phoneNum = reader2.GetValue(4).ToString();
                    client.passport = reader2.GetValue(5).ToString();
                }
            }
            reader2.Close();
            conn.Close();
            return client;
        }

        public Device getDevice(int id)
        {
            Device device = new Device(id, "", "", new List<string>(0));
            SqliteDataReader reader2;
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Devices WHERE id = " + id;
            reader2 = cmd.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {

                    device.type = reader2.GetValue(1).ToString();
                    device.model = reader2.GetValue(2).ToString();
                    device.components = reader2.GetValue(3).ToString().Split(",").ToList<string>();
                }
            }
            reader2.Close();
            conn.Close();
            return device;
        }

        public Order getOrder(int id)
        {
            Order order = new Order(id, 0, "", new Device(0, "", "", new List<string>(0)), new List<Service>(0));
            int id_device = -1;
            SqliteDataReader reader2;
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Orders WHERE id = " + id;
            reader2 = cmd.ExecuteReader();
            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    order.cost = float.Parse(reader2.GetValue(1).ToString());
                    order.description = reader2.GetValue(2).ToString();
                    id_device = int.Parse(reader2.GetValue(3).ToString());
                }
            }
            reader2.Close();
            conn.Close();
            order.device = getDevice(id_device);
            order.services = getServicesOfOrder(id);
            return order;
        }

        public Application getApp(int id)
        {
            Application application = new Application(id, new DateTime(2000, 1, 1), new Client(0, "", "", "", "", ""),
                "", new Staff(0, "", "", ""), new Order(0, 0, "", new Device(0, "", "", new List<string>(0)), new List<Service>(0)));
            int client_id = 0;
            int staff_id = 0;
            int order_id = 0;
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Applications WHERE id = " + id;
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    application.date = DateTime.Parse(reader.GetValue(1).ToString());
                    client_id = int.Parse(reader.GetValue(2).ToString());
                    application.status = reader.GetValue(3).ToString();
                    staff_id = int.Parse(reader.GetValue(4).ToString());
                    order_id = int.Parse(reader.GetValue(5).ToString());
                }
            }
            close();
            application.client = getClient(client_id);
            application.master = getStaff(staff_id);
            application.order = getOrder(order_id);
            return application;
        }

        public List<Application> getAppList()
        {
            List<Application> applications = new List<Application>();
            int client_id;
            int staff_id;
            int order_id;
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Applications";
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Application application = new Application(-1, new DateTime(2000, 1, 1), new Client(), "", new Staff(), new Order());
                    application.id = int.Parse(reader.GetValue(0).ToString());
                    application.date = DateTime.Parse(reader.GetValue(1).ToString());
                    client_id = int.Parse(reader.GetValue(2).ToString());
                    application.status = reader.GetValue(3).ToString();
                    if (!reader.GetValue(4).ToString().Equals(""))
                    {
                        staff_id = int.Parse(reader.GetValue(4).ToString());
                        application.master = getStaff(staff_id);
                    }
                    order_id = int.Parse(reader.GetValue(5).ToString());
                    application.client = getClient(client_id);
                    application.order = getOrder(order_id);
                    applications.Add(application);
                }
            }
            close();
            return applications;
        }

        public List<Application> getFilteredAppList(string filters)
        {
            List<Application> applications = new List<Application>();

            int client_id;
            int staff_id;
            int order_id;
            connect();
            cmd = conn.CreateCommand();
            if(filters.Length == 0)
            {
                cmd.CommandText = "SELECT * FROM Applications";
            }
            else if(filters.Contains("Staff.Surname"))
            {
                cmd.CommandText = "SELECT * FROM Applications " +
                "JOIN Clients ON Applications.id_client = Clients.id " +
                "JOIN Orders ON Applications.id_order = Orders.id " +
                "JOIN Staff ON Applications.id_master = Staff.id " +
                "WHERE " + filters;
            }
            else
            {
                cmd.CommandText = "SELECT * FROM Applications " +
                "JOIN Clients ON Applications.id_client = Clients.id " +
                "JOIN Orders ON Applications.id_order = Orders.id " +
                "WHERE " + filters;
            }
            
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Application application = new Application(-1, new DateTime(2000, 1, 1), new Client(), "", new Staff(), new Order());
                    application.id = int.Parse(reader.GetValue(0).ToString());
                    application.date = DateTime.Parse(reader.GetValue(1).ToString());
                    client_id = int.Parse(reader.GetValue(2).ToString());
                    application.status = reader.GetValue(3).ToString();
                    if (!reader.GetValue(4).ToString().Equals(""))
                    {
                        staff_id = int.Parse(reader.GetValue(4).ToString());
                        application.master = getStaff(staff_id);
                    }
                    order_id = int.Parse(reader.GetValue(5).ToString());
                    application.client = getClient(client_id);
                    application.order = getOrder(order_id);
                    applications.Add(application);
                }
            }
            close();
            return applications;
        }

        public List<Application> getActiveAppList(Staff master)
        {
            List<Application> applications = new List<Application>();
            
            int client_id;
            int order_id;
            bool masterWork = checkMaster(master);
            connect();
            cmd = conn.CreateCommand();
            if (masterWork)
            {
                cmd.CommandText = $"SELECT * FROM Applications WHERE id_master = {master.id} AND status <> 'Отказана' AND status <> 'Закрыта'";
            }
            else
            {
                cmd.CommandText = "SELECT * FROM Applications WHERE status = 'Согласована'";
            }
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Application application = new Application(-1, new DateTime(2000, 1, 1), new Client(), "", new Staff(), new Order());
                    application.id = int.Parse(reader.GetValue(0).ToString());
                    application.date = DateTime.Parse(reader.GetValue(1).ToString());
                    client_id = int.Parse(reader.GetValue(2).ToString());
                    application.status = reader.GetValue(3).ToString();
                    order_id = int.Parse(reader.GetValue(5).ToString());
                    application.client = getClient(client_id);
                    application.order = getOrder(order_id);
                    if (masterWork)
                    {
                        application.master = master;
                    }
                    applications.Add(application);
                }
            }
            close();
            return applications;
        }

        public int addClient(Client client)
        {
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO Clients (Name, Surname, Patronymic, PhoneNum, Passport) VALUES ('{client.name}', " +
                $"'{client.surname}', '{client.patronymic}', '{client.phoneNum}', '{client.passport}')";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"SELECT last_insert_rowid();";
            reader = cmd.ExecuteReader();
            reader.Read();
            int id;
            id = int.Parse(reader.GetValue(0).ToString());

            close();
            return id;
        }

        public int addDevice(Device device)
        {
            string comps = "";
            foreach (string it in device.components)
            {
                comps += it + "\n";
            }
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO Devices (Type, Model, Components) VALUES ('{device.type}', " +
                $"'{device.model}', '{comps}');";

            int number = cmd.ExecuteNonQuery();
            Console.WriteLine($"В таблицу Devices добавлено объектов: {number}");

            cmd.CommandText = $"SELECT last_insert_rowid();";
            reader = cmd.ExecuteReader();
            reader.Read();
            int id;
            id = int.Parse(reader.GetValue(0).ToString());

            close();
            return id;
        }

        public int addOrder(Order order)
        {
            int id = addDevice(order.device);
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO Orders (Cost, Description, id_device) VALUES ({order.cost}, " +
               $"'{order.description}', {id})";

            int number = cmd.ExecuteNonQuery();
            Console.WriteLine($"В таблицу Orders добавлено объектов: {number}");

            cmd.CommandText = $"SELECT last_insert_rowid();";
            reader = cmd.ExecuteReader();
            reader.Read();
            int id2;
            id2 = int.Parse(reader.GetValue(0).ToString());

            close();
            addOrderService(order, id2);
            return id2;
        }

        public void addOrderService(Order order, int id2)
        {
            foreach (Service it in order.services)
            {
                connect();
                cmd = conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO AppServices (id_order, id_service) VALUES ({id2}, {it.id})";
                cmd.ExecuteNonQuery();
                close();
            }
        }

        public void addApp(Application application)
        {
            int idCl = addClient(application.client);
            int idOr = addOrder(application.order);
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO Applications (Date, id_client, status, id_master, id_order) VALUES ('{application.date}', " +
               $"{idCl}, '{application.status}', null, {idOr})";

            cmd.ExecuteNonQuery();

            close();
        }

        public void editClient(Client client)
        {
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"UPDATE Clients SET Name = '{client.name}', Surname = '{client.surname}', Patronymic = '{client.patronymic}'," +
                $"PhoneNum = '{client.phoneNum}', Passport = '{client.passport}' WHERE id = {client.id}";
            cmd.ExecuteNonQuery();
            close();
        }

        public void editDevice(Device device)
        {
            string comps = "";
            foreach (string it in device.components)
            {
                comps += it + "\n";
            }
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"UPDATE Devices SET Type = '{device.type}', Model = '{device.model}', Components = '{comps}' WHERE id = {device.id}";
            cmd.ExecuteNonQuery();
            close();
        }

        public void editOrder(Order order)
        {
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"UPDATE Orders SET Cost = {order.cost}, Description = '{order.description}', id_device = {order.device.id} WHERE id = {order.id}";
            cmd.ExecuteNonQuery();
            close();
        }

        public void editApp(Application application)
        {
            editClient(application.client);
            editDevice(application.order.device);
            editOrder(application.order);
            string s = "";
            if (application.master.id == 0)
            {
                s = "null";
            }
            else
            {
                s = application.master.id.ToString();
            }
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"UPDATE Applications SET Date = '{application.date}', id_client = {application.client.id}," +
                $"status = '{application.status}', id_master = {s}, id_order = {application.order.id} WHERE id = {application.id}";
            cmd.ExecuteNonQuery();
            close();
        }
    }
}