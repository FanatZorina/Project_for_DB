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
    public partial class FDelAdress : Form
    {
        public DataGridView dg;
        public FDelAdress()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (DoorContext enty = new DoorContext())
            {
               

                List<int> doors = enty.Locks.AsNoTracking().Where(x => x.IdStreet == Convert.ToInt32(textBox1.Text)).Select(x => x.IdStreet).ToList();
                foreach (int door in doors)
                {
                    List<int> idstreet = enty.Audits.AsNoTracking().Where(x => x.IdStreet == door).Select(x => x.Number).ToList();
                    foreach (int id in idstreet)
                    {
                        enty.Audits.Remove(enty.Audits.AsNoTracking().Where(x => x.Number == id).FirstOrDefault());
                    }
                    enty.Locks.Remove(enty.Locks.AsNoTracking().Where(x => x.IdStreet == door).FirstOrDefault());
                }
                enty.Adresses.Remove(enty.Adresses.AsNoTracking().Where(x => x.Id == Convert.ToInt32(textBox1.Text)).FirstOrDefault());
                enty.SaveChanges();
                
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
    }
}
