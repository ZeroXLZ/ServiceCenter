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
    public partial class Tester : Form
    {
        public Tester()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthorizationF f = new(); // Экземпляр формы авторизации
            f.radioButton1.Checked = true; // Выбор типа авторизации сотрудника
            f.richTextBox1.Text = "smirnova@mail."; // Логин сотрудника
            f.richTextBox2.Text = "12345AA"; // Пароль сотрудника
            f.performClick(sender, e); // Вызов метода программного нажатия кнопки авторизации на форме
            f.Show();
            if(!f.label2.Visible) // Проверка, прошла ли авторизация успешно
            {
                label1.ForeColor = Color.Green;
                label1.Text += "Успех!";
            }
            else
            {
                label1.ForeColor = Color.Red;
                label1.Text += "Неудача.";
            }
            f.Hide();
        }
    }
}