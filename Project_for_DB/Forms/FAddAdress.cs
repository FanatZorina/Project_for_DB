using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_for_DB
{

    public partial class FAddAdress : Form
    {
        public DataGridView dg;
        public FAddAdress()
        {
            InitializeComponent();
            textBox2.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
            textBox3.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Trim() != "") && (textBox2.Text.Trim() != ""))
            {
                using (DoorContext enty = new DoorContext())
                {
                    int? tempb = null;
                    if (!string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        tempb = Convert.ToInt32(textBox3.Text);
                    }
                    Adress adress = new Adress()
                    {
                        Street = textBox1.Text,
                        Number = Convert.ToInt32(textBox2.Text),
                        Building = tempb
                    };
                    enty.Adresses.Add(adress);
                    enty.SaveChanges();

                }

                using (DoorContext db = new DoorContext())
                {
                    var q = from adress in db.Adresses.AsNoTracking()
                            select new TempClass()
                            {
                                Id = adress.Id,
                                street = adress.Street,
                                number = adress.Number,
                                building = adress.Building

                            };
                    var auditList = q.ToList();
                    dg.DataSource = auditList;

                }
                Close();
            }
            else
            { MessageBox.Show("Заполните поля"); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
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

