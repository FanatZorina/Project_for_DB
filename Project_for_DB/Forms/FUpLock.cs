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
    public partial class FUpLock : Form
    {
        public DataGridView dg;
        int tempidstreet = 0;
        int tempid = 0;
        public FUpLock()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            using (DoorContext db = new DoorContext())
            {
                var q = from Lock in db.Locks.AsNoTracking()
                        join Adress in db.Adresses.AsNoTracking()
                        on Lock.IdStreet equals Adress.Id
                        select new FromLockClass()
                        {
                            Id = Lock.Id,
                            Id_street = Lock.IdStreet,
                            Level = Lock.Level,
                            Closed = Lock.Closed,
                            fulladress = Convert.ToString("Номер" + Lock.Id + "ул." + Adress.Street + " дом." + Adress.Number + " " + Adress.Building)
                        };
                var auditList = q.ToList();

                comboBox1.DataSource = auditList;
                comboBox1.DisplayMember = "fulladress";
                comboBox1.ValueMember = "Id";
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
            List<int> level = new List<int> { 1, 2, 3, 4, 5 };
            comboBox2.DataSource = level;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                FromLockClass selectedlock = (FromLockClass)comboBox1.SelectedItem;

                textBox1.Text = Convert.ToString(selectedlock.Id);
                textBox2.Text = selectedlock.fulladress;
                comboBox2.Text = Convert.ToString(selectedlock.Level);
                checkBox1.Checked = selectedlock.Closed;
                tempidstreet = selectedlock.Id_street;
                tempid = selectedlock.Id;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Trim() != "") && (textBox2.Text.Trim() != ""))
            {
                using (DoorContext db = new DoorContext())
                {

                    var temp1 = db.Locks.FirstOrDefault(s => s.Id == tempid && s.IdStreet == tempidstreet);

                    if (temp1 != null)
                    {
                        temp1.Level = Convert.ToInt32(comboBox2.Text);
                        temp1.Closed = checkBox1.Checked;
                        db.Locks.Update(temp1);
                        db.SaveChanges();
                    }
                    db.SaveChanges();

                    var q = from Lock in db.Locks.AsNoTracking()
                            join Adress in db.Adresses.AsNoTracking()
                            on Lock.IdStreet equals Adress.Id
                            select new FromLockClass()
                            {
                                Id = Lock.Id,
                                Id_street = Lock.IdStreet,
                                Level = Lock.Level,
                                Closed = Lock.Closed,
                                fulladress = Convert.ToString("ул." + Adress.Street + " дом." + Adress.Number + " " + Adress.Building)
                            };
                    var auditList = q.ToList();
                    dg.DataSource = auditList;

                }
                Close();
            }
            else { MessageBox.Show("Заполните поля"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
