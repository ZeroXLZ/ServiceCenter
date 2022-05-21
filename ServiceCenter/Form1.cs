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

namespace ServiceCenter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppCreateF f = new AppCreateF();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AuthorizationF f = new AuthorizationF();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainFClient f = new MainFClient();
            f.Show();
        }
    }
}
