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
using ServiceCenter.Database;

namespace ServiceCenter.GUI
{
    public partial class MainFManager : Form
    {
        private List<DataClasses.Application> apps = new();
        public Staff managerS;
        public MainFManager(Staff managerS)
        {
            InitializeComponent();
            dateTimePicker1.Text = "01/01/1890 12:00:00 AM";
            //dateTimePicker2.Value = DateTime.MaxValue;
            this.managerS = managerS;
            setManager();
            MainManager manager = new();
            apps = manager.getApps();
            setTable(apps);
            label2.Text = "Всего записей: " + dataGridView1.RowCount;
        }

        public void setManager()
        {
            label1.Text += managerS.surname + " " + managerS.name;
        }

        private void setTable(List<DataClasses.Application> apps)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach (DataClasses.Application it in apps)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["id"].Value = it.id;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["date"].Value = it.date;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["fio"].Value = it.client.surname + " " + it.client.name + " " + it.client.patronymic;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["master"].Value = it.master.surname;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["cost"].Value = it.order.cost;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["type"].Value = it.order.device.type;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["status"].Value = it.status;
            }

            label2.Text = "Всего записей: " + dataGridView1.RowCount;
        }

        private DataClasses.Application getAppFromGrid(int id)
        {
            foreach (DataClasses.Application it in apps)
            {
                if (it.id == id)
                {
                    return it;
                }
            }
            return new DataClasses.Application();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBClass db = new();
            apps = db.getFilteredAppList(createFilter());
            setTable(apps);
        }

        private string createFilter()
        {
            string filters = "";
            string fdate1 = dateTimePicker1.Value.ToShortDateString();
            string fdate2 = dateTimePicker2.Value.ToShortDateString();
            string fClient = richTextBox1.Text;
            string fMaster = richTextBox3.Text;
            string fCost = richTextBox2.Text;
            string fStatus = "";
            if (comboBox2.SelectedIndex > 0)
                fStatus = comboBox2.SelectedItem.ToString();

            filters += $"Applications.Date BETWEEN '{fdate1}' AND '{fdate2}' ";
            if (fClient.Length > 0)
                filters += $"AND (Clients.Surname LIKE '%{fClient}%' OR Clients.Name LIKE '%{fClient}%' OR Clients.Patronymic LIKE '%{fClient}%') ";
            if (fMaster.Length > 0)
                filters += $"AND (Staff.Surname LIKE '%{fMaster}%' OR Staff.Name LIKE '%{fMaster}%' OR Staff.Patronymic LIKE '%{fMaster}%') ";
            if (fCost.Length > 0)
                filters += $"AND Orders.Cost = '{fCost}' ";
            if (fStatus.Length > 0)
                filters += $"AND Applications.status = '{fStatus}'";

            List<string> fs = filters.Split(" ").ToList<string>();
            if (fs[0].Equals("AND"))
            {
                fs.RemoveAt(0);
            }
            filters = "";
            foreach(string it in fs)
            {
                filters += it + " ";
            }

            return filters;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthorizationF a = new();
            a.Show();
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                DataClasses.Application application = getAppFromGrid(int.Parse(dataGridView1[e.ColumnIndex - 1, e.RowIndex].Value.ToString()));
                Close();
                AppEditF f = new(application, managerS);
                f.Show();
            }
        }
    }
}
