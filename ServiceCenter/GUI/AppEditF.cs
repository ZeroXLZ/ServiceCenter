using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceCenter.DataClasses;
using ServiceCenter.Database;

namespace ServiceCenter.GUI
{
    public partial class AppEditF : Form
    {
        DBClass db = new();
        List<Service> listS = new List<Service>();
        List<Staff> listSt = new List<Staff>();
        DataClasses.Application app1;
        Staff manager;

        public AppEditF(DataClasses.Application application, Staff manager)
        {
            InitializeComponent();
            this.manager = manager;
            label1.Text += manager.surname + " " + manager.name;
            app1 = application;
            richTextBox1.Text = application.client.surname;
            richTextBox8.Text = application.client.name;
            richTextBox9.Text = application.client.patronymic;
            maskedTextBox1.Text = application.client.phoneNum;
            maskedTextBox2.Text = application.client.passport;
            richTextBox10.Text = application.date.Date.ToShortDateString();
            foreach (object it in comboBox1.Items)
            {
                if (application.order.device.type == it.ToString())
                {
                    comboBox1.SelectedItem = it;
                }
            }
            foreach (object it in comboBox4.Items)
            {
                if (application.status == it.ToString())
                {
                    comboBox4.SelectedItem = it;
                }
            }

            listSt.Add(new Staff());
            listSt.AddRange(db.getMasterList());
            comboBox3.DataSource = listSt;
            comboBox3.DisplayMember = "surname";

            foreach (Staff it in comboBox3.Items)
            {
                if (application.master.surname == it.surname)
                {
                    comboBox3.SelectedItem = it;
                }
            }
            richTextBox5.Text = application.order.device.model;
            foreach (string str in application.order.device.components)
            {
                richTextBox6.Text += str;
            }



            foreach (Service it in application.order.services)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["id"].Value = it.id;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["name"].Value = it.name;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["description"].Value = it.description;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["price"].Value = it.price;
            }
            countSum();

            richTextBox7.Text = application.order.description;

        }

        private bool checkInputs()
        {
            if (richTextBox1.TextLength > 1 && richTextBox8.TextLength > 1
                && richTextBox9.TextLength > 1 && maskedTextBox1.TextLength == 15
                && maskedTextBox2.TextLength == 11 && richTextBox5.TextLength > 1
                && richTextBox6.TextLength > 1 && comboBox1.SelectedIndex != 0
                && dataGridView1.RowCount > 0)
            {
                return true;
            }
            else
            {
                label17.Visible = true;
                return false;
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Service ss = (Service)comboBox2.SelectedItem;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["id"].Value = ss.id;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["name"].Value = ss.name;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["description"].Value = ss.description;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["price"].Value = ss.price;
            countSum();
            
        }

        private void countSum()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells["price"].Value);
            }

            richTextBox4.Text = sum + " руб.";
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells["price"].Value);
            }

            richTextBox4.Text = sum + " руб.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkInputs())
            {
                if (MessageBox.Show("Вы хотите сохранить заявку с внесёнными данными?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    List<Service> services = new List<Service>();

                    foreach (DataGridViewRow dr in dataGridView1.Rows)
                    {
                        Service item = new Service();
                        item.id = int.Parse(dr.Cells["id"].Value.ToString());
                        item.name = dr.Cells["name"].Value.ToString();
                        item.description = dr.Cells["description"].Value.ToString();
                        item.price = float.Parse(dr.Cells["price"].Value.ToString());
                        services.Add(item);
                    }

                    Client cl = new Client(app1.client.id, richTextBox8.Text, richTextBox1.Text, richTextBox9.Text, maskedTextBox1.Text, maskedTextBox2.Text);
                    Device device = new Device(app1.order.device.id, comboBox1.SelectedItem.ToString(), richTextBox5.Text, richTextBox6.Text.Split(",").ToList<string>());
                    Order order = new Order(app1.order.id, float.Parse(richTextBox4.Text.Replace(" руб.", "")), richTextBox7.Text, device, services);
                    Staff master = new();
                    if(comboBox3.SelectedIndex > 0)
                    {
                        master = (Staff)comboBox3.SelectedItem;
                    }

                    DataClasses.Application application = new(app1.id, app1.date, cl, comboBox4.SelectedItem.ToString(), master, order);

                    db.editApp(application);
                    MainFManager f = new(manager);
                    f.Show();
                    Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти без сохранения своей заявки?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MainFManager f = new(manager);
                f.Show();
                Close();
            }
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            listS = db.getServiceList();
            comboBox2.DataSource = listS;
            comboBox2.DisplayMember = "name";
        }
    }
}