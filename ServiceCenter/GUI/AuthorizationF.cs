using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceCenter
{
    public partial class AuthorizationF : Form
    {
        public AuthorizationF()
        {
            InitializeComponent();
        }

        public void RemoveText(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "Логин" && richTextBox1.Focused)
            {
                richTextBox1.ForeColor = Color.Black;
                richTextBox1.Text = "";
            }
            if (richTextBox2.Text == "Пароль" && richTextBox2.Focused)
            {
                richTextBox2.ForeColor = Color.Black;
                richTextBox2.Text = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                richTextBox1.ForeColor = Color.Gray;
                richTextBox1.Text = "Логин";
            }

            if (string.IsNullOrWhiteSpace(richTextBox2.Text))
            {
                richTextBox2.ForeColor = Color.Gray;
                richTextBox2.Text = "Пароль";
            }
        }

        private void AuthorizationF_Activated(object sender, EventArgs e)
        {
            richTextBox1.GotFocus += RemoveText;
            richTextBox1.LostFocus += AddText;
            richTextBox2.GotFocus += RemoveText;
            richTextBox2.LostFocus += AddText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                bool auth = new Logic.Authorization().authorize(richTextBox1.Text, richTextBox2.Text);
                if (auth)
                {
                    Hide();
                }
                else
                {
                    label2.Visible = true;
                }
            }
            else
            {
                Hide();
                new Logic.Authorization().authorize();
            }
        }

        public void performClick(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                richTextBox1.ReadOnly = true;
                richTextBox2.ReadOnly = true;
            }
            else
            {
                richTextBox1.ReadOnly = false;
                richTextBox2.ReadOnly = false;
            }
        }
    }
}