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
    public partial class FUpRecord : Form
    {
        public DataGridView dg;
        public FUpRecord()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            using (DoorContext db = new DoorContext())
            {
                var auditList1 = Metods.ViewRecord().ToList();
                comboBox4.DataSource = auditList1;
                comboBox4.DisplayMember = "number";
                comboBox4.ValueMember = "number";
                comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;

                var auditList2 = Metods.ViewUser().ToList();
                comboBox1.DataSource = auditList2;
                comboBox1.DisplayMember = "login";
                comboBox1.ValueMember = "login";
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

                var auditList3 = Metods.ViewAdress().ToList(); ;
               /* foreach (var adress in auditList3)
                {
                    if (adress.building == null)
                        adress.fulladress = $" ул.{adress.street} {adress.number} ";
                    else
                        adress.fulladress = $" ул.{adress.street} {adress.number} корпус.{adress.building}";
                }*/
                comboBox2.DataSource = auditList3;
                comboBox2.DisplayMember = "Id";
                comboBox2.ValueMember = "Id";
                comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (DoorContext db = new DoorContext())
            {
                if (comboBox2.SelectedValue != null)
                {
                    FromAdressClass selectedAddress = (FromAdressClass)comboBox2.SelectedItem;

                    var d = from locks in db.Locks.AsNoTracking()
                            where locks.IdStreet == selectedAddress.Id
                            select new FromLockClass()
                            {
                                Id = locks.Id,
                            };
                    var auditList4 = d.ToList();
                    comboBox3.DataSource = auditList4;
                    comboBox3.DisplayMember = "Id";
                    comboBox3.ValueMember = "Id";
                    comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
                }
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Trim() != ""))
            {
                using (DoorContext db = new DoorContext())
                {
                    var temp1 = db.Audits.FirstOrDefault(s => s.Number == Convert.ToInt32(comboBox4.Text));

                    if (temp1 != null)
                    {
                        var temp2 = db.Departaments.FirstOrDefault(s => s.Name == comboBox3.Text);
                        temp1.Login = comboBox1.Text;
                        temp1.IdDoor = Convert.ToInt32(comboBox3.Text);
                        temp1.IdStreet = Convert.ToInt32(comboBox2.SelectedValue);
                        temp1.Date = DateTime.Parse(textBox1.Text);
                        db.Audits.Update(temp1);
                        db.SaveChanges();
                    }
                    db.SaveChanges();

                   
                    var auditList = Metods.ViewRecord().ToList();
                    dg.DataSource = auditList;
                }
                Close();
            }
            else { MessageBox.Show("Заполните поля"); }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedValue != null)
            {
                FromAuditClass selectedrecord = (FromAuditClass)comboBox4.SelectedItem;

                textBox1.Text = Convert.ToString(selectedrecord.date);
                comboBox1.Text = Convert.ToString(selectedrecord.login);
                comboBox2.Text = Convert.ToString(selectedrecord.idstreet);
                comboBox3.Text = Convert.ToString(selectedrecord.iddoor);
                checkBox1.Checked = selectedrecord.closed;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
