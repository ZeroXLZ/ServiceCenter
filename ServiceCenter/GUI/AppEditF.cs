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
            foreach(object it in comboBox1.Items)
            {
                if(application.order.device.type == it.ToString())
                {
                    comboBox1.SelectedItem = it;
                }
            }
            richTextBox5.Text = application.order.device.model;
            foreach (string str in application.order.device.components)
            {
                richTextBox6.Text += str + "\n";
            }

            dataGridView1.DataSource = application.order.services;
            richTextBox7.Text = application.order.description;

        }

        private bool checkInputs()
        {
            if (richTextBox1.TextLength > 1 && richTextBox8.TextLength > 1
                && richTextBox9.TextLength > 1 && maskedTextBox1.TextLength == 11
                && maskedTextBox2.TextLength == 10 && richTextBox5.TextLength > 1
                && richTextBox6.TextLength > 1 && comboBox1.SelectedIndex != 0
                && comboBox3.SelectedIndex != 0
                && comboBox4.SelectedIndex != 0
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listS = db.getServiceList();
            comboBox2.DataSource = listS;
            comboBox2.DisplayMember = "name";
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSt = db.getMasterList();
            comboBox2.DataSource = listSt;
            comboBox2.DisplayMember = "surname";
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Service ss = (Service)comboBox2.SelectedItem;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["id"].Value = ss.id;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["name"].Value = ss.name;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["description"].Value = ss.description;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["price"].Value = ss.price;

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
                if (MessageBox.Show("Вы хотите создать заявку с внесёнными данными?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                    Client cl = new Client(-1, richTextBox8.Text, richTextBox1.Text, richTextBox9.Text, maskedTextBox1.Text, maskedTextBox2.Text);
                    Device device = new Device(-1, comboBox1.SelectedItem.ToString(), richTextBox5.Text, richTextBox6.Text.Split(",").ToList<string>());
                    Order order = new Order(-1, float.Parse(richTextBox4.Text.Replace(" руб.", "")), richTextBox7.Text, device, services);

                    DataClasses.Application application = new(app1.id, DateTime.Now.Date, cl, "Создана", new Staff(), order);

                    db.editApp(application);
                    MainFClient f = new();
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
    }
}
