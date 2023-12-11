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

namespace Project_for_DB.Forms
{
    public partial class FDelUser : Form
    {
        public DataGridView dg;
        public FDelUser()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            using (DoorContext db = new DoorContext())
            {
                var q = from user in db.Users.AsNoTracking()
                        join Departament in db.Departaments.AsNoTracking()
                        on user.DepartamentId equals Departament.Id
                        select new FromUserClass()
                        {
                            login = user.Login,
                            password = user.Password,
                            name = user.Name,
                            sname = user.Sname,
                            level = user.Level,
                            departament_name = Departament.Name
                        };
                var auditList = q.ToList();
                comboBox1.DataSource = auditList;
                comboBox1.DisplayMember = "login";
                comboBox1.ValueMember = "login";
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                FromUserClass selecteduser = (FromUserClass)comboBox1.SelectedItem;

                textBox1.Text = selecteduser.login;
                textBox2.Text = selecteduser.name;
                textBox3.Text = selecteduser.sname;
                textBox4.Text = Convert.ToString(selecteduser.departament_name);
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
        private void button2_Click(object sender, EventArgs e)
        {
            using (DoorContext db = new DoorContext())
            {

                List<string> users = db.Audits.AsNoTracking().Where(x => x.Login == textBox1.Text).Select(x => x.Login).ToList();
                foreach (string user in users)
                {
                    db.Audits.Remove(db.Audits.AsNoTracking().Where(x => x.Login == user).FirstOrDefault());
                }
                db.Users.Remove(db.Users.AsNoTracking().Where(x => x.Login == textBox1.Text).FirstOrDefault());
                db.SaveChanges();

                var q = from user in db.Users.AsNoTracking()
                        join Departament in db.Departaments.AsNoTracking()
                        on user.DepartamentId equals Departament.Id
                        select new FromUserClass()
                        {
                            login = user.Login,
                            password = user.Password,
                            name = user.Name,
                            sname = user.Sname,
                            level = user.Level,
                            departament_name = Departament.Name
                        };
                var auditList = q.ToList();
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
