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

namespace ServiceCenter.GUI
{
    public partial class MainFMaster : Form
    {
        public List<DataClasses.Application> apps;
        public Staff master;
        public MainFMaster(Staff master)
        {
            InitializeComponent();
            this.master = master;
            setMaster();
            DBClass db = new();
            List<DataClasses.Application> applications = db.getActiveAppList(master);
            apps = applications;

            foreach (DataClasses.Application it in applications)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["id"].Value = it.id;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["type"].Value = it.order.device.type;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["model"].Value = it.order.device.model;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["problem"].Value = it.order.description;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["date"].Value = it.date;
            }

            label2.Text += dataGridView1.RowCount;

        }

        public void setMaster()
        {
            label1.Text += master.surname + " " + master.name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthorizationF f = new();
            f.Show();
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
                OrderInfoF f = new(master, application);
                f.Show();
            }
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
    }
}
