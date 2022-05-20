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
    public partial class MainFManager : Form
    {
        public MainFManager()
        {
            InitializeComponent();
        }

        public void setManager(string manager)
        {
            label1.Text += manager;
        }
    }
}
