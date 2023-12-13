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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_for_DB.Forms
{
    public partial class FAddRecord : Form
    {
        public DataGridView dg;
        public FAddRecord()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            var auditList1 = Metods.ViewAdress().ToList(); 
            foreach (var adress in auditList1)
            {
                if (adress.building == null)
                    adress.fulladress = $" ул.{adress.street} {adress.number} ";
                else
                    adress.fulladress = $" ул.{adress.street} {adress.number} корпус.{adress.building}";
            }
            comboBox1.DataSource = auditList1;
            comboBox1.DisplayMember = "fulladress";
            comboBox1.ValueMember = "Id";
            comboBox1.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            var auditList3 = Metods.ViewUser().ToList();
            comboBox3.DataSource = auditList3;
            comboBox3.DisplayMember = "login";
            comboBox3.ValueMember = "login";
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (DoorContext db = new DoorContext())
            {
                if (comboBox1.SelectedValue != null)
                {
                    FromAdressClass selectedAddress = (FromAdressClass)comboBox1.SelectedItem;

                    var d = from locks in db.Locks.AsNoTracking()
                            where locks.IdStreet == selectedAddress.Id
                            select new FromLockClass()
                            {
                                Id = locks.Id,
                            };
                    var auditList4 = d.ToList();
                    comboBox2.DataSource = auditList4;
                    comboBox2.DisplayMember = "Id";
                    comboBox2.ValueMember = "Id";
                    comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (DoorContext db = new DoorContext())
            {
                FromUserClass selecteduser = (FromUserClass)comboBox3.SelectedItem;
                FromLockClass selectedlock = (FromLockClass)comboBox2.SelectedItem;
                FromAdressClass selectedadress = (FromAdressClass)comboBox1.SelectedItem;
                bool cls;
                if (db.Locks.AsNoTracking().Where(x => x.Id == selectedlock.Id && x.IdStreet == selectedadress.Id).Select(x => x.Closed).FirstOrDefault() == true)
                    cls = false; 
                else
                    cls = true; 
                Audit audit = new Audit()
                {
                    Login = selecteduser.login,
                    Date = DateTime.Now,
                    IdDoor = selectedlock.Id,
                    IdStreet = selectedadress.Id,
                    Closed = cls
                };
                db.Audits.Add(audit);
                var temp1 = db.Locks.FirstOrDefault(s => s.Id == selectedlock.Id && s.IdStreet == selectedadress.Id);
                if (temp1 != null)
                {
                    if (cls == false)
                    {
                        temp1.Closed = false;
                        db.Locks.Update(temp1);
                    }
                    else
                    {
                        temp1.Closed = true;
                        db.Locks.Update(temp1);
                    }

                }
                db.SaveChanges();
            }
            using (DoorContext db = new DoorContext())
            {
                var auditList = Metods.ViewRecord().ToList();
                dg.DataSource = auditList;
            }
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
