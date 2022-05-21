using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceCenter.Database;
using ServiceCenter.DataClasses;
using ServiceCenter.Logic;

namespace ServiceCenter.GUI
{
    public partial class AppCreateF : Form
    {
        DBClass db = new();
        List<Service> listS = new List<Service>();
        public AppCreateF()
        {
            InitializeComponent();
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            listS = db.getServiceList();
            comboBox2.DataSource = listS;
            comboBox2.DisplayMember = "name";
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

                    AppCreate app = new();
                    Client cl = new Client(-1, richTextBox8.Text, richTextBox1.Text, richTextBox9.Text, maskedTextBox1.Text, maskedTextBox2.Text);
                    Device device = new Device(-1, comboBox1.SelectedItem.ToString(), richTextBox5.Text, richTextBox6.Text.Split(",").ToList<string>());
                    Order order = new Order(-1, float.Parse(richTextBox4.Text.Replace(" руб.", "")), richTextBox7.Text, device, services);

                    DataClasses.Application application = new(-1, DateTime.Now.Date, cl, "Создана", new Staff(), order);

                    app.createApp(application);
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
                MainFClient f = new();
                f.Show();
                Close();
            }
        }

        private bool checkInputs()
        {
            if(richTextBox1.TextLength > 1 && richTextBox8.TextLength > 1
                && richTextBox9.TextLength > 1 && maskedTextBox1.TextLength == 15
                && maskedTextBox2.TextLength == 11 && richTextBox5.TextLength > 1
                && richTextBox6.TextLength > 1 && comboBox1.SelectedIndex != 0
                && dataGridView1.RowCount > 0)
            {
                return true;
            }
            else
            {
                label12.Visible = true;
                return false;
            }
        }
    }
}
