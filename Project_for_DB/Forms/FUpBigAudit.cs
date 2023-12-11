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
    public partial class FUpBigAudit : Form
    {
        public DataGridView dg;
        public FUpBigAudit(string number)
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            using (DoorContext db = new DoorContext())
            {
                var q = from audit in db.Audits.AsNoTracking()
                        join user in db.Users.AsNoTracking()
                        on audit.Login equals user.Login
                        join departament in db.Departaments.AsNoTracking()
                        on user.DepartamentId equals departament.Id
                        join locks in db.Locks.AsNoTracking()
                        on new { IdDoor = audit.IdDoor, IdStreet = audit.IdStreet } equals new { IdDoor = locks.Id, IdStreet = locks.IdStreet }
                        join adress in db.Adresses.AsNoTracking()
                        on locks.IdStreet equals adress.Id
                        where audit.Number == Convert.ToInt32(number)
                        select new FromBigAuditClass()
                        {
                            number = audit.Number,
                            login = audit.Login,
                            name = user.Name,
                            sname = user.Sname,
                            departament = departament.Name,
                            date = audit.Date,
                            iddoor = locks.Id,
                            closed = audit.Closed,
                            street = adress.Street,
                            numberst = adress.Number,
                            building = adress.Building,
                            idadress = adress.Id
                        };
                var auditList1 = q.ToList();
                foreach (var adress in auditList1)
                {
                    if (adress.building == null)
                        adress.fulladress = $" ул.{adress.street} {adress.numberst} ";
                    else
                        adress.fulladress = $" ул.{adress.street} {adress.numberst} корпус.{adress.building}";
                }
                textBox1.Text = Convert.ToString(auditList1[0].number);
                textBox8.Text = Convert.ToString(auditList1[0].date);

                var u = from user in db.Users.AsNoTracking()
                        select new FromUserClass()
                        {
                            login = user.Login,

                        };
                var auditList2 = u.ToList();
                comboBox1.DataSource = auditList2;
                comboBox1.DisplayMember = "login";
                comboBox1.ValueMember = "login";
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
                comboBox1.Text = Convert.ToString(auditList1[0].login);

                var s = from adress in db.Adresses.AsNoTracking()
                        select new TempClass()
                        {
                            Id = adress.Id,
                            street = adress.Street,
                            number = adress.Number,
                            building = adress.Building,
                            fulladress = ""

                        };
                var auditList3 = s.ToList();
                foreach (var adress in auditList3)
                {
                    if (adress.building == null)
                        adress.fulladress = $" ул.{adress.street} {adress.number} ";
                    else
                        adress.fulladress = $" ул.{adress.street} {adress.number} корпус.{adress.building}";
                }
                comboBox2.DataSource = auditList3;
                comboBox2.DisplayMember = "fulladress";
                comboBox2.ValueMember = "Id";
                comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
                comboBox2.Text = Convert.ToString(auditList1[0].fulladress);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (DoorContext db = new DoorContext())
            {
                if (comboBox2.SelectedValue != null)
                {
                    TempClass selectedAddress = (TempClass)comboBox2.SelectedItem;

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

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox8.Text.Trim() != ""))
            {
                using (DoorContext db = new DoorContext())
                {
                    var temp1 = db.Audits.FirstOrDefault(s => s.Number == Convert.ToInt32(textBox1.Text));

                    if (temp1 != null)
                    {
                        var temp2 = db.Departaments.FirstOrDefault(s => s.Name == comboBox3.Text);
                        temp1.Login = comboBox1.Text;
                        temp1.IdDoor = Convert.ToInt32(comboBox3.Text);
                        temp1.IdStreet = Convert.ToInt32(comboBox2.SelectedValue);
                        temp1.Date = DateTime.Parse(textBox8.Text);                      
                        db.Audits.Update(temp1);
                        db.SaveChanges();
                    }
                    db.SaveChanges();

                    var q = from audit in db.Audits.AsNoTracking()
                            join user in db.Users.AsNoTracking()
                            on audit.Login equals user.Login
                            join departament in db.Departaments.AsNoTracking()
                            on user.DepartamentId equals departament.Id
                            join locks in db.Locks.AsNoTracking()
                            on new { IdDoor = audit.IdDoor, IdStreet = audit.IdStreet } equals new { IdDoor = locks.Id, IdStreet = locks.IdStreet }
                            join adress in db.Adresses.AsNoTracking()
                            on locks.IdStreet equals adress.Id
                            select new FromBigAuditClass()
                            {
                                number = audit.Number,
                                login = audit.Login,
                                name = user.Name,
                                sname = user.Sname,
                                departament = departament.Name,
                                date = audit.Date,
                                iddoor = locks.Id,
                                closed = audit.Closed,
                                street = adress.Street,
                                numberst = adress.Number,
                                building = adress.Building
                            };
                    var auditList = q.ToList();
                    foreach (var adress in auditList)
                    {
                        if (adress.building == null)
                            adress.fulladress = $" ул.{adress.street} {adress.numberst} ";
                        else
                            adress.fulladress = $" ул.{adress.street} {adress.numberst} корпус.{adress.building}";
                    }
                    dg.DataSource = auditList;                                   
                }
                Close();
            }
            else { MessageBox.Show("Заполните поля"); }
        }
    }
}
