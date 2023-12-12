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
    public partial class FAddUser : Form
    {
        public DataGridView dg;
        public FAddUser()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            
            var auditList = Metods.ViewDept().ToList(); ;
            comboBox2.DataSource = auditList;
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "Id";
            comboBox2.SelectedIndexChanged += comboBox1_SelectedIndexChanged;          

            List<int> level = new List<int> { 1, 2, 3, 4, 5 };
            comboBox1.DataSource = level;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Trim() != "") && (textBox2.Text.Trim() != "") && (textBox3.Text.Trim() != "") && (textBox4.Text.Trim() != ""))
            {
                using (DoorContext enty = new DoorContext())
                {
                    FromDeptClass selecteddept = (FromDeptClass)comboBox2.SelectedItem;
                    int intlvl = (int)comboBox1.SelectedItem;
                    User users = new User()
                    {
                        Login = textBox1.Text,
                        Password = textBox2.Text,
                        Name = textBox3.Text,
                        Sname = textBox4.Text,
                        Level = intlvl,
                        DepartamentId = selecteddept.Id

                    };
                    enty.Users.Add(users);
                    enty.SaveChanges();

                }

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
                    dg.DataSource = auditList;

                }
                Close();
            }
            else { MessageBox.Show("Заполните поля"); };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    
    }
}
