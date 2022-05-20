using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceCenter.GUI
{
    public partial class MainFMaster : Form
    {
        public MainFMaster()
        {
            InitializeComponent();
            DataTable tb = new();
            //tb.Load()
        }

        public void setMaster(string master)
        {
            label1.Text += master;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthorizationF f = new();
            f.Show();
            Close();
        }
    }
}
