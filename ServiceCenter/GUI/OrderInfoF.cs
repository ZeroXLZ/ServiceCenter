using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceCenter.Logic;
using ServiceCenter.DataClasses;

namespace ServiceCenter.GUI
{
    public partial class OrderInfoF : Form
    {
        public Staff master;
        public DataClasses.Application app;
        public OrderInfoF(Staff master, DataClasses.Application app)
        {
            InitializeComponent();
            this.master = master;
            this.app = app;
            richTextBox1.Text = app.client.surname + " " + app.client.name + " " + app.client.patronymic;
            richTextBox2.Text = app.date.Date.ToShortDateString();
            richTextBox3.Text = app.client.phoneNum;
            richTextBox4.Text = app.order.device.type;
            richTextBox5.Text = app.order.device.model;
            foreach (string str in app.order.device.components)
            {
                richTextBox6.Text += str + "\n";
            }
            dataGridView1.DataSource = app.order.services;
            richTextBox7.Text = app.order.description;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы хотите принять эту заявку?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OrderInfo f = new();
                f.takeOrder(app, master);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainFMaster f = new(master);
            f.Show();
            Close();
        }
    }
}
