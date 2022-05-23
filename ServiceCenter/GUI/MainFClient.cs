using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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

        private void button1_Click(object sender, EventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"D:\Systemf\Desktop\Course work\ServiceCenter\Help\Памятка клиента.chm")
            {
                UseShellExecute = true
            };
            p.Start();
        }
    }
}