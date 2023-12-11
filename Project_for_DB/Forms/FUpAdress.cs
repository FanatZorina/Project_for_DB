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

namespace Project_for_DB
{
    public partial class FUpAdress : Form
    {
        public DataGridView dg;
        public FUpAdress()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            textBox3.KeyPress += new KeyPressEventHandler(textBox3_KeyPress);
            textBox4.KeyPress += new KeyPressEventHandler(textBox4_KeyPress);

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
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

        private void FUpAdress_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                TempClass selectedAddress = (TempClass)comboBox1.SelectedItem;

                textBox1.Text = Convert.ToString(selectedAddress.Id);
                textBox2.Text = selectedAddress.street;
                textBox3.Text = Convert.ToString(selectedAddress.number);
                textBox4.Text = Convert.ToString(selectedAddress.building);
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

        private void textBox4_TextChanged(object sender, EventArgs e)
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
                using (DoorContext enty = new DoorContext())
                {

                    var temp1 = enty.Adresses.FirstOrDefault(s => s.Id == Convert.ToInt32(textBox1.Text));

                    if (temp1 != null)
                    {
                        int? tempb = null;
                        if (!string.IsNullOrWhiteSpace(textBox4.Text))
                        {
                            tempb = Convert.ToInt32(textBox4.Text);
                        }
                        temp1.Id = Convert.ToInt32(textBox1.Text);
                        temp1.Street = textBox2.Text;
                        temp1.Number = Convert.ToInt32(textBox3.Text);                   
                        temp1.Building = tempb;
                        enty.Adresses.Update(temp1);
                        enty.SaveChanges();
                    }

                    var q = from adress in enty.Adresses.AsNoTracking()
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
            else { MessageBox.Show("Заполните поля"); }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
