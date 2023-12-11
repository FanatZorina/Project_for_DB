using Microsoft.EntityFrameworkCore;
using Project_for_DB.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_for_DB
{
    public partial class AddAudit : Form
    {
        public AddAudit()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            textBox3.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            using (DoorContext db = new DoorContext())
            {
                var q = from adress in db.Adresses.AsNoTracking()
                        select new TempClass()
                        {
                            Id = adress.Id,
                            street = adress.Street,
                            number = adress.Number,
                            building = adress.Building,
                            fulladress = ""
                        };
                var auditList = q.ToList();

                foreach (var adress in auditList)
                {
                    if (adress.building == null)
                        adress.fulladress = $" ул.{adress.street} {adress.number} ";
                    else
                        adress.fulladress = $" ул.{adress.street} {adress.number} корпус.{adress.building}";
                }

                comboBox1.DataSource = auditList;
                comboBox1.DisplayMember = "fulladress";
                comboBox1.ValueMember = "Id";
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;


            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            var form1 = (Form1)Tag;
            form1.Show();
            Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (DoorContext enty = new DoorContext())
            {
                if (enty.Users.AsNoTracking().Where(x => x.Login == textBox1.Text && x.Password == textBox2.Text).FirstOrDefault() != null)
                {
                    TempClass selectedlock = (TempClass)comboBox1.SelectedItem;
                    if (enty.Locks.AsNoTracking().Where(x => x.IdStreet == selectedlock.Id && x.Id == Convert.ToInt32(textBox3.Text)).FirstOrDefault() != null)
                    {
                        OpenOrClose ooc = new OpenOrClose(textBox1.Text, Convert.ToInt32(textBox3.Text), selectedlock.Id);
                        ooc.Tag = this;                     
                        ooc.Show(this);
                    }
                    else
                    { MessageBox.Show("Неверный адрес или номер двери"); };
                }
                else
                { MessageBox.Show("Неверный логин или пароль"); };
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
