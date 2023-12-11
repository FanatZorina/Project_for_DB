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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_for_DB.Forms
{

    public partial class FUpDept : Form
    {
        int iddept = 0;
        public DataGridView dg;
        public FUpDept()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            textBox3.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            using (DoorContext db = new DoorContext())
            {
                var q = from departament in db.Departaments.AsNoTracking()
                        select new FromDeptClass()
                        {
                            Id = departament.Id,
                            name = departament.Name,
                            number = departament.Number,

                        };
                var auditList = q.ToList();

                comboBox1.DataSource = auditList;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "Id";
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                FromDeptClass selecteddept = (FromDeptClass)comboBox1.SelectedItem;
                iddept = selecteddept.Id;
                textBox2.Text = selecteddept.name;
                textBox3.Text = Convert.ToString(selecteddept.number);
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox2.Text.Trim() != "") && (textBox3.Text.Trim() != ""))
            {
                using (DoorContext enty = new DoorContext())
                {

                    var temp1 = enty.Departaments.FirstOrDefault(s => s.Id == iddept);
                    if (temp1 != null)
                    {
                        temp1.Name = textBox2.Text;
                        temp1.Number = Convert.ToInt32(textBox3.Text);
                        enty.Departaments.Update(temp1);
                        enty.SaveChanges();
                    }

                    var q = from departament in enty.Departaments.AsNoTracking()
                            select new FromDeptClass()
                            {
                                Id = departament.Id,
                                name = departament.Name,
                                number = departament.Number,
                            };
                    var auditList = q.ToList();
                    dg.DataSource = auditList;
                }
                Close();
            }
            else { MessageBox.Show("Заполните поля"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
