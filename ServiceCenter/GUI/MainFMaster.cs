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
        public Staff master;
        public MainFMaster(Staff master)
        {
            InitializeComponent();
            this.master = master;
            setMaster();
            DBClass db = new();
            dataGridView1.DataSource = db.getActiveAppList();
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
    }
}
