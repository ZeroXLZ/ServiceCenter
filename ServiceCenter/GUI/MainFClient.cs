using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceCenter.GUI;

namespace ServiceCenter.GUI
{
    public partial class MainFClient : Form
    {
        public MainFClient()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AppCreateF f = new();
            f.Show();
            Close();
        }
    }
}
