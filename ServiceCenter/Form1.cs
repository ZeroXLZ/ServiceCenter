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

        private void button2_Click(object sender, EventArgs e)
        {
            AppEditF f = new AppEditF();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AuthorizationF f = new AuthorizationF();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConfirmF f = new ConfirmF();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainFClient f = new MainFClient();
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainFManager f = new MainFManager();
            f.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainFMaster f = new MainFMaster();
            f.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OrderInfoF f = new OrderInfoF();
            f.Show();
        }
    }
}
