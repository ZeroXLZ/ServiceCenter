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
    public partial class MainFManager : Form
    {
        private List<DataClasses.Application> apps = new();
        public Staff managerS;
        public MainFManager(Staff managerS)
        {
            
            InitializeComponent();
            this.managerS = managerS;
            setManager();
            MainManager manager = new();
            apps = manager.getApps();
            dataGridView1.DataSource = apps;
            label2.Text += dataGridView1.RowCount;
        }

        public void setManager()
        {
            label1.Text += managerS.surname + " " + managerS.name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindingSource bs = new();
            bs.DataSource = dataGridView1.DataSource;
            //bs.Filter = 
        }

        private string createFilter()
        {
            string filters = "";
            string fClient = richTextBox1.Text;
            string fMaster = richTextBox3.Text;
            string fCost = richTextBox2.Text;
            string fDevice = comboBox1.SelectedItem.ToString();
            string fStatus = comboBox2.SelectedItem.ToString();

            if (fClient.Length > 0)
                filters += "AND Clients.Surname = " + fClient;
            if (fMaster.Length > 0)
                filters += "AND Staff.Surname = " + fMaster;
            if (fCost.Length > 0)
                filters += "AND Orders.cost = " + fCost;
            if (fDevice.Length > 0)
                filters += "AND Devices.Type = " + fDevice;
            if (fStatus.Length > 0)
                filters += "AND Applications.status = " + fStatus;

            return filters;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthorizationF a = new();
            a.Show();
            Close();
        }
    }
}
