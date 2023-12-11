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
    public partial class FDelLock : Form
    {
        public DataGridView dg;
        int tempidstreet = 0;
        public FDelLock()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (DoorContext db = new DoorContext())
            {

                List<int> doors = db.Audits.AsNoTracking().Where(x => x.IdDoor == Convert.ToInt32(textBox1.Text) && x.IdStreet == tempidstreet).Select(x => x.IdDoor).ToList();
                foreach (int door in doors)
                {
                    db.Audits.Remove(db.Audits.AsNoTracking().Where(x => x.IdDoor == door && x.IdStreet == tempidstreet).FirstOrDefault());
                }             
                db.Locks.Remove(db.Locks.AsNoTracking().Where(x => x.Id == Convert.ToInt32(textBox1.Text) && x.IdStreet == tempidstreet).FirstOrDefault());
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                FromLockClass selectedlock = (FromLockClass)comboBox1.SelectedItem;

                textBox1.Text = Convert.ToString(selectedlock.Id);
                textBox2.Text = selectedlock.fulladress;
                textBox3.Text = Convert.ToString(selectedlock.Level);
                checkBox1.Checked = selectedlock.Closed;
                tempidstreet = selectedlock.Id_street;
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
            if (comboBox1.SelectedItem != null)
            {
                FromLockClass selectedlock = (FromLockClass)comboBox1.SelectedItem;
                checkBox1.Checked = selectedlock.Closed; ;
            }
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
