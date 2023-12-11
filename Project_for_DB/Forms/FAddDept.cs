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

namespace Project_for_DB.Forms
{
    public partial class FAddDept : Form
    {
        public DataGridView dg;
        public FAddDept()
        {
            InitializeComponent();          
            textBox3.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox2.Text.Trim() != "") && (textBox3.Text.Trim() != ""))
            {
                using (DoorContext db = new DoorContext())
                {

                    Departament departaments = new Departament()
                    {
                        Name = textBox2.Text,
                        Number = Convert.ToInt32(textBox3.Text)

                    };
                    db.Departaments.Add(departaments);
                    db.SaveChanges();

                }

                using (DoorContext db = new DoorContext())
                {

                    var q = from departament in db.Departaments.AsNoTracking()
                            select new FromDeptClass()
                            {
                                Id = departament.Id,
                                name = departament.Name,
                                number = departament.Number

                            };
                    var auditList = q.ToList();
                    dg.DataSource = auditList;

                }
                Close();
            }
            else
            { MessageBox.Show("Заполните поля"); }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
